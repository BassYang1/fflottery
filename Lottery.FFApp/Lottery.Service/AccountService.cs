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

                //3, 验证加密串
                //按顺序(商户Id&会员用户名&商户安全码)MD5加密串
                var signKey = MD5Cryptology.GetMD5(string.Format("{0}&{1}&{2}&{3}&{4}", model.OrderId, model.MerchantId, model.UserName, model.Amount.ToString("f4"), merchantEntity.Code), "gb2312");
                if (string.Compare(signKey, model.SignKey, true) != 0)
                {
                    Log.Error("无效的商户安全码");
                    throw new InvalidOperationException("无效的商户安全码");
                }

                //4, 支付
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

                int num = (new UserChargeDAL()).Save3(orderId, model.OrderId, userEntity.Id.ToString(), model.MerchantId, model.Amount);

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
    }
}