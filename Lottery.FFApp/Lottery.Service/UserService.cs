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

namespace Lottery.Service
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 代理商登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns>用户登录Token</returns>
        public string GetUserToken(UserLoginModel model)
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
                    Log.Error("商户不存在。");
                    throw new KeyNotFoundException("商户不存在。");
                }

                if (string.IsNullOrEmpty(merchantEntity.Code))
                {
                    Log.Error("无效的商户");
                    throw new KeyNotFoundException("无效的商户");
                }

                //2, 验证加密串
                var signKey = MD5Cryptology.Encrypt(string.Format("{0}&{1}{2}", model.MerchantId, model.UserName, merchantEntity.Code));
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new KeyNotFoundException("无效的商户安全码");
                }

                //3,验证用户
                var userEntity = dbContext.N_User.FirstOrDefault(it => it.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));
                
                if (userEntity == null)
                {
                    Log.Error("用户不存在。");
                    throw new KeyNotFoundException("用户不存在。");
                }

                var token = this.GenerateToken(); // 获取用户登录Token 
                userEntity.Token =   token;             
                userEntity.ExpirationTime = DateTime.Now.AddDays(2); // 设置Token有效期
                SaveDbChanges(dbContext);

                return token;
            }
        }

        private Random rnd = new Random();
        private int seed = 0;
        /// <summary>
        /// 生成用户Token
        /// </summary>
        /// <returns></returns>
        private string GenerateToken()
        {
            var rndData = new byte[48];
            rnd.NextBytes(rndData);
            var seedValue = Interlocked.Add(ref seed, 1);
            var seedData = BitConverter.GetBytes(seedValue);
            var tokenData = rndData.Concat(seedData).OrderBy(_ => rnd.Next());

            return Convert.ToBase64String(tokenData.ToArray()).TrimEnd('=');
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

    }
}