using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 充值记录
    /// </summary>
    [Description("充值记录")]
    public class UserChargeRecordModel
    {
        /// <summary>
        /// 提现记录ID
        /// </summary>
        [Description("提现记录ID")]
        public String SsId { get; set; }

        /// <summary>
        /// 第三方唯一充值订单号, 可为空
        /// </summary>
        [Description("第三方唯一充值订单号, 可为空")]
        public string OrderId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [Description("充值金额")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        [Description("到账金额")]
        public Decimal InAmount { get; set; }

        /// <summary>
        /// 记录状态
        /// </summary>
        [Description("记录状态")]
        public String State { get; set; }

        /// <summary>
        /// 提现时间
        /// </summary>
        [Description("提现时间")]
        public String STime { get; set; }
    }
}
