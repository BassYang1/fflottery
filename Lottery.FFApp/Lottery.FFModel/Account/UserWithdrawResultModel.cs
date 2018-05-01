using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 用户取现结果
    /// </summary>
    [Description("用户取现结果")]
    public class UserWithdrawResultModel
    {
        /// <summary>
        /// 第三方唯一取现订单号, 可为空
        /// </summary>
        [Description("第三方唯一取现订单号, 可为空")]
        public string OrderId { get; set; }

        /// <summary>
        /// 非凡充值记录Id
        /// </summary>
        [Description("非凡取现记录Id")]
        public string SsId { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        [Description("商户Id")]
        public string MerchantId { get; set; }

        /// <summary>
        /// 会员用户名
        /// </summary>
        [Description("会员用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 充值前余额
        /// </summary>
        [Description("取现前余额")]
        public Decimal BeforeMoney { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        [Description("账户余额")]
        public Decimal Money { get; set; }
    }
}
