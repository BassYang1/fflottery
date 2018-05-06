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
using Lottery.DAL.Flex;

namespace Lottery.Service
{
    public class AccountService : BaseService, IAccountService
    {
        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="model">充值信息</param>
        /// <returns>账户余额</returns>
        public UserChargeResultModel Charge(UserChargeModel model)
        {
            using (var dbContext = new TicketEntities())
            {
                if (string.IsNullOrEmpty(model.MerchantId) && string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.SignKey))
                {
                    throw new InvalidOperationException("无效的用户登录信息");
                }

                if (model.Amount <= 0)
                {
                    throw new InvalidOperationException("无效的充值金额");                    
                }

                if (string.IsNullOrEmpty(model.OrderId))
                {
                    throw new InvalidOperationException("第三方交易ID无效");
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

                //2,验证用户
                var userEntity = dbContext.N_User.FirstOrDefault(it => it.MerchantId.Equals(model.MerchantId, StringComparison.OrdinalIgnoreCase)
                    && it.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));

                if (userEntity == null)
                {
                    Log.Error("用户不存在");
                    throw new InvalidOperationException("用户不存在");
                }

                //3,验证订单是否存在
                var chargeEntity = dbContext.N_UserCharge.FirstOrDefault(it => it.UserId == userEntity.Id
                    && it.State.HasValue && it.State.Value == 1
                    && model.OrderId.Equals(it.Ss3Id, StringComparison.OrdinalIgnoreCase));

                if (chargeEntity != null)
                {
                    Log.Error("用户充值订单已经存在");
                    throw new InvalidOperationException("用户充值订单已经存在");
                }

                //4, 验证加密串
                //按顺序(商户Id&会员用户名&商户安全码)MD5加密串
                var signKey = Core.MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}&{3}&{4}", model.OrderId, model.MerchantId, model.UserName, model.Amount.ToString("f4"), merchantEntity.Code).ToLower(), "gb2312");
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new InvalidOperationException("无效的商户安全码");
                }

                //5, 支付
                string orderId = SsId.Charge;
                UserChargeResultModel result = new UserChargeResultModel()
                {
                    SsId = orderId,
                    OrderId = model.OrderId,
                    MerchantId = model.MerchantId,
                    UserName = model.UserName,
                    Money = userEntity.Money ?? 0M + model.Amount,
                    BeforeMoney = userEntity.Money ?? 0M
                };


                int num = (new DAL.UserChargeDAL()).Save3(orderId, model.OrderId, userEntity.Id.ToString(), model.MerchantId, model.Amount);

                //N_UserCharge entity = new N_UserCharge()
                //{
                //    SsId = orderId,
                //    UserId = Convert.ToInt32(userEntity.Id),
                //    BankId = 888,
                //    CheckCode = model.MerchantId,
                //    Ss3Id = model.OrderId,
                //    InMoney = model.Amount,
                //    DzMoney = model.Amount,
                //    STime = DateTime.Now,
                //    State = 1,
                //};

                //dbContext.N_UserCharge.Add(entity);


                if (new DAL.Flex.UserChargeDAL().Update(orderId) == false)
                {
                    Log.ErrorFormat("充值失败，订单号: {0}", orderId);
                    throw new InvalidOperationException("充值失败");
                }
                else
                {
                    Log.InfoFormat("充值成功，订单号: {0}", orderId);
                }

                return result;
            }
        }

        /// <summary>
        /// 会员取现
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserWithdrawResultModel Withdraw(UserWithdrawModel model)
        {
            using (var dbContext = new TicketEntities())
            {
                if (string.IsNullOrEmpty(model.MerchantId) && string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.SignKey))
                {
                    throw new InvalidOperationException("无效的用户登录信息");
                }

                if (string.IsNullOrEmpty(model.OrderId))
                {
                    throw new InvalidOperationException("第三方交易ID无效");
                }

                if (model.Amount <= 0)
                {
                    throw new InvalidOperationException("无效的取现金额");
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

                //2,验证用户
                var userEntity = dbContext.N_User.FirstOrDefault(it => it.UserName.Equals(model.UserName, StringComparison.OrdinalIgnoreCase));

                if (userEntity == null)
                {
                    Log.Error("用户不存在");
                    throw new InvalidOperationException("用户不存在");
                }

                //3,验证订单是否存在
                var withdrawEntity = dbContext.N_UserGetCash.FirstOrDefault(it => it.UserId == userEntity.Id
                    && it.State.HasValue && it.State.Value == 1
                    && model.OrderId.Equals(it.Ss3Id, StringComparison.OrdinalIgnoreCase));

                if (withdrawEntity != null)
                {
                    Log.Error("用户提现订单已经存在");
                    throw new InvalidOperationException("用户提现订单已经存在");
                }

                //4, 验证加密串
                //按顺序(商户Id&会员用户名&商户安全码)MD5加密串
                var signKey = Core.MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}&{3}&{4}", model.OrderId, model.MerchantId, model.UserName, model.Amount.ToString("f4"), merchantEntity.Code).ToLower(), "gb2312");
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new InvalidOperationException("无效的商户安全码");
                }

                //5, 取现
                string orderId = SsId.GetCash;
                UserWithdrawResultModel result = new UserWithdrawResultModel()
                {
                    SsId = orderId,
                    OrderId = model.OrderId,
                    MerchantId = model.MerchantId,
                    UserName = model.UserName,
                    Money = (userEntity.Money - model.Amount) ?? 0M,
                    BeforeMoney = userEntity.Money ?? 0M
                };

                int num = (new UserGetCashDAL()).Save3Withdraw(orderId, model.MerchantId, merchantEntity.Name, userEntity.Id.ToString(), model.OrderId, model.Amount);

                if (num == 0)
                {
                    Log.ErrorFormat("取现失败，订单号: {0}", orderId);
                    throw new InvalidOperationException("取现失败");
                }
                else
                {
                    new LogSysDAL().Save("会员管理", "Id为" + userEntity.Id.ToString() + "的会员申请提现！");
                    Log.InfoFormat("取现成功，订单号: {0}", orderId);
                }
                return result;
            }
        }

        /// <summary>
        /// 分页查询充值记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>充值记录</returns>
        public PageData<UserChargeRecordModel> SearchCharge(SearchChargeModel query, int pageSize, int pageIndex)
        {
            query.StartTime = Convert.ToDateTime(query.StartTime.HasValue == false
                ? DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.StartTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));

            query.EndTime = Convert.ToDateTime(query.EndTime.HasValue == false
                ? DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.EndTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));

            using (var db = new TicketEntities())
            {
                var entities = db.N_UserCharge.Where(c => c.STime.HasValue == false || (c.STime >= query.StartTime && c.STime <= query.EndTime));

                //用户名
                var chargeEntities = entities.Join(db.N_User.Where(u => string.IsNullOrEmpty(query.UserName) || u.UserName.Contains(query.UserName)),
                    c => c.UserId, u => u.Id,
                    (c, u) => new UserChargeEntity(c)
                    {
                        UserName = u.UserName
                    });


                //分页处理
                int totalCount;
                var pgData = GetPagingData(chargeEntities.ToList(), pageSize, pageIndex, out totalCount);

                //转换实体
                var records = new List<UserChargeRecordModel>();
                pgData.ForEach(entity => records.Add(entity.ToModel()));

                return new PageData<UserChargeRecordModel>(records, pageSize, pageIndex, totalCount);
            }
        }

        /// <summary>
        /// 分页查询提现记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>提现记录</returns>
        public PageData<UserWithdrawRecordModel> SearchWithdraw(SearchWithdrawModel query, int pageSize, int pageIndex)
        {
            query.StartTime = Convert.ToDateTime(query.StartTime.HasValue == false
                ? DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.StartTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));

            query.EndTime = Convert.ToDateTime(query.EndTime.HasValue == false
                ? DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00"
                : query.EndTime.Value.ToString("yyyy-MM-dd HH:ss:mm"));

            using (var db = new TicketEntities())
            {
                var entities = db.N_UserGetCash.Where(c => c.STime.HasValue == false || (c.STime >= query.StartTime && c.STime <= query.EndTime));

                //用户名
                var chargeEntities = entities.Join(db.N_User.Where(u => string.IsNullOrEmpty(query.UserName) || u.UserName.Contains(query.UserName)),
                    c => c.UserId, u => u.Id,
                    (c, u) => new UserWithdrawEntity(c)
                    {
                        UserName = u.UserName
                    });


                //分页处理
                int totalCount;
                var pgData = GetPagingData(chargeEntities.ToList(), pageSize, pageIndex, out totalCount);

                //转换实体
                var records = new List<UserWithdrawRecordModel>();
                pgData.ForEach(entity => records.Add(entity.ToModel()));

                return new PageData<UserWithdrawRecordModel>(records, pageSize, pageIndex, totalCount);
            }
        }
    }
}