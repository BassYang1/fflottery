using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 用户取现
    /// </summary>
    [Description("用户取现")]
    public class UserWithdrawModel
    {
        /// <summary>
        /// 第三方唯一取现订单号, 可为空
        /// </summary>
        [Description("第三方唯一取现订单号, 可为空")]
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
        /// 取现金额，保留小数位后4位
        /// </summary>
        [Description("取现金额，保留小数位后4位")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 签名字符串, 按顺序(取现订单号&amp;商户Id&amp;会员用户名&amp;取现金额(4位小数)&amp;商户安全码)MD5加密串
        /// </summary>
        [Description("签名字符串, 按顺序(取现订单号&商户Id&会员用户名&取现金额(4位小数)&商户安全码)MD5加密串")]
        public string SignKey { get; set; }
    }
}
