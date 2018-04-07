using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 用户充值
    /// </summary>
    [Description("用户充值")]
    public class UserChargeModel
    {
        /// <summary>
        /// 第三方唯一充值订单号, 可为空
        /// </summary>
        [Description("第三方唯一充值订单号, 可为空")]
        public string OrderId { get; set; }

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
        /// 充值金额，保留小数位后4位
        /// </summary>
        [Description("充值金额，保留小数位后4位")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 签名字符串, 按顺序(充值订单号&商户Id&会员用户名&充值金额(4位小数)&商户安全码)MD5加密串
        /// </summary>
        [Description("签名字符串, 按顺序(充值订单号&商户Id&会员用户名&充值金额(4位小数)&商户安全码)MD5加密串")]
        public string SignKey { get; set; }
    }
}
