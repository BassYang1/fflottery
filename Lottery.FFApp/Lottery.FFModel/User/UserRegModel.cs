using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 用户注册
    /// </summary>
    [Description("用户注册")]
    public class UserRegModel
    {
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
        /// 注册时间，格式yyyyMMddHHmmss
        /// </summary>
        [Description("注册时间，格式yyyyMMddHHmmss")]
        public string Time { get; set; }

        /// <summary>
        /// 签名字符串, 按顺序(商户Id&amp;会员用户名&amp;商户安全码)MD5加密串")
        /// </summary>
        [Description("签名字符串, 按顺序(商户Id&会员用户名&商户安全码)MD5加密串")]
        public string SignKey { get; set; }
    }
}
