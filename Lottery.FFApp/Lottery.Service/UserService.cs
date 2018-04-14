using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using Lottery.FFModel;
using Lottery.Core;
using Lottery.FFData;
using System.Data;
using Lottery.Utils;
using Lottery.DAL;

namespace Lottery.Service
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="model">注册信息</param>
        /// <returns>用户登录凭证Token</returns>
        public string RegiterUser(UserRegModel model)
        {
            using (var dbContext = new TicketEntities())
            {
                if (string.IsNullOrEmpty(model.MerchantId) && string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.SignKey))
                {
                    throw new InvalidOperationException("无效的用户登录信息");
                }

                //1, 判断用户是否存在
                var merchantEntity = dbContext.N_Merchant.FirstOrDefault(it => (it.MerchantId.Equals(model.MerchantId, StringComparison.OrdinalIgnoreCase)));

                if (merchantEntity == null)
                {
                    Log.Error("商户不存在");
                    throw new InvalidOperationException("商户不存在");
                }

                if (string.IsNullOrEmpty(merchantEntity.Code))
                {
                    Log.Error("无效的商户信息");
                    throw new InvalidOperationException("无效的商户信息");
                }

                //2, 验证加密串
                //按顺序(商户Id&会员用户名&商户安全码)MD5加密串
                var signKey = MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}", model.MerchantId, model.UserName, merchantEntity.Code), "gb2312");
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new InvalidOperationException("无效的商户安全码");
                }

                var result = ajaxRegiter(model);
                if (!string.IsNullOrEmpty(result))
                {
                    Log.ErrorFormat("注册失败: {0}", result);
                    throw new InvalidOperationException(result);
                }

                //3,验证用户
                var userEntity = dbContext.N_User.FirstOrDefault(it => it.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));

                if (userEntity == null)
                {
                    Log.Error("新用户注册失败");
                    throw new InvalidOperationException("新用户注册失败");
                }

                var token = this.GenerateToken(); // 获取用户登录Token 
                userEntity.Token = token;
                userEntity.ExpirationTime = DateTime.Now.AddDays(2); // 设置Token有效期
                SaveDbChanges(dbContext);

                return token;
            }
        }

        /// <summary>
        /// 代理商登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns>用户登录Token</returns>
        public string GetUserToken(UserAddModel model)
        {
            using (var dbContext = new TicketEntities())
            {
                if (string.IsNullOrEmpty(model.MerchantId) && string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.SignKey))
                {
                    throw new KeyNotFoundException("无效的用户登录信息");
                }

                //1, 判断用户是否存在
                var merchantEntity = dbContext.N_Merchant.FirstOrDefault(it => (it.MerchantId.Equals(model.MerchantId, StringComparison.OrdinalIgnoreCase)));

                if (merchantEntity == null)
                {
                    Log.Error("商户不存在");
                    throw new KeyNotFoundException("商户不存在");
                }

                if (string.IsNullOrEmpty(merchantEntity.Code))
                {
                    Log.Error("无效的商户");
                    throw new KeyNotFoundException("无效的商户");
                }

                //2, 验证加密串
                var signKey = MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}", model.MerchantId, model.UserName, merchantEntity.Code), "gb2312");
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new KeyNotFoundException("无效的商户安全码");
                }

                //3,验证用户
                var userEntity = dbContext.N_User.FirstOrDefault(it => it.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));
                
                if (userEntity == null)
                {
                    Log.Error("用户不存在");
                    throw new KeyNotFoundException("用户不存在");
                }

                var token = this.GenerateToken(); // 获取用户登录Token 
                userEntity.Token =   token;             
                userEntity.ExpirationTime = DateTime.Now.AddDays(2); // 设置Token有效期
                SaveDbChanges(dbContext);

                return token;
            }
        }

        /// <summary>
        /// 根据Token获取用户详细信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns>用户详细信息</returns>
        public UserModel GetUserDetailByToken(string token)
        {
            using (var dbContext = new TicketEntities())
            {
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var member = dbContext.N_User.FirstOrDefault(item => item.Token.Equals(token, StringComparison.OrdinalIgnoreCase));

                if (member == null)
                {
                    //Log.Debug("登录用户无效");
                    return null;
                }

                if (member.IsTokenExpired())
                {
                    member.Token = string.Empty;
                    SaveDbChanges(dbContext);

                    Log.Debug("登录凭证过期");
                    return null;
                }

                var model = member.ToModel();

                return model;
            }
        }

        /// <summary>
        /// 获取用户下注，最新20条
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="lotteryId">彩票种类Id</param>
        /// <returns>用户下注，最新20条</returns>
        public string GetUserBets(int userId, int lotteryId)
        {
            string _wherestr1 = "" + " UserId = " + userId;
            _wherestr1 = " AND LotteryId = " + lotteryId;
            string _jsonstr = "";
            new WebAppListOper().GetListJSON_ZH(0, 20, _wherestr1, "", ref _jsonstr);

            return _jsonstr;
        }

        private string ajaxRegiter(UserRegModel model)
        {
            string s = "123456";
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id FROM [N_User] WHERE [UserName]='" + model.UserName + "'";
            if (this.doh.GetDataTable().Rows.Count > 0)
                return "用户名重复";

            int userId = new UserDAL().Register("0", model.UserName, MD5.Lower32(s), 0M, model.MerchantId);
            this.doh.Reset();
            this.doh.ConditionExpress = "id=" + (object)userId;
            this.doh.AddFieldItem("UserGroup", "6");
            this.doh.AddFieldItem("UserCode", Strings.PadLeft(userId.ToString()));
            if (this.doh.Update("N_User") > 0)
            {
                new LogAdminOperDAL().SaveLog(model.MerchantId, userId.ToString(), "会员管理", "添加了会员" + model.UserName);
                return "";
            }

            return "用户注册失败";
        }
    }
}