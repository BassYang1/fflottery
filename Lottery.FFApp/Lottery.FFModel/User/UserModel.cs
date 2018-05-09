using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 会员
    /// </summary>
    [Description("会员")]
    public class UserModel
    {
        /// <summary>
        /// Token
        /// </summary>
        [Description("Token")]
        public string Token { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Description("用户Id")]
        public int Id { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        [Description("商户Id")]
        public string MerchantId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Description("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 用户返点
        /// </summary>
        [Description("用户返点")]
        public Decimal Point { get; set; }

        /// <summary>
        /// 账户金额
        /// </summary>
        [Description("账户金额")]
        public Decimal Amount { get; set; }
    }
}
