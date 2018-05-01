using Lottery.FFData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 提现记录扩展实体
    /// </summary>
    [Description("提现记录扩展实体")]
    public class UserWithdrawEntity: N_UserGetCash
    {
        public UserWithdrawEntity(N_UserGetCash charge)
        {
            Id = charge.Id;
            SsId = charge.SsId;
            UserId = charge.UserId;
            BankId = charge.BankId;
            PayBank = charge.PayBank;
            PayAccount = charge.PayAccount;
            PayName = charge.PayName;
            Money = charge.Money;
            Msg = charge.Msg;
            STime = charge.STime;
            State = charge.State;
            STime2 = charge.STime2;
            Ss3Id = charge.Ss3Id;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }
    }
}
