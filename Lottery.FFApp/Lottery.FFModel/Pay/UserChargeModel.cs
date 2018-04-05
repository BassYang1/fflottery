using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel.Pay
{
    public class UserChargeModel
    {
        [Description("充值订单ID")]
        public string SsId { get; set; }

        [Description("充值银行ID")]
        public Nullable<int> BankId { get; set; }

        [Description("充值用户ID")]
        public Nullable<int> UserId { get; set; }

        [Description("支付code")]
        public string CheckCode { get; set; }

        [Description("充值金额")]
        public Nullable<decimal> InMoney { get; set; }

        [Description("到账金额")]
        public Nullable<decimal> DzMoney { get; set; }

        [Description("充值时间")]
        public Nullable<System.DateTime> STime { get; set; }

        [Description("充值状态")]
        public Nullable<int> State { get; set; }

        [Description("实际充值状态")]
        public Nullable<int> ActState { get; set; }
    }
}
