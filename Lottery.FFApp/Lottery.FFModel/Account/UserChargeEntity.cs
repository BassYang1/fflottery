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
    /// 充值记录扩展实体
    /// </summary>
    [Description("充值记录扩展实体")]
    public class UserChargeEntity: N_UserCharge
    {
        public UserChargeEntity(N_UserCharge charge)
        {
            Id = charge.Id;
            SsId = charge.SsId;
            UserId = charge.UserId;
            BankId = charge.BankId;
            CheckCode = charge.CheckCode;
            InMoney = charge.InMoney;
            DzMoney = charge.DzMoney;
            STime = charge.STime;
            State = charge.State;
            ActState = charge.ActState;
            Ss3Id = charge.Ss3Id;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }
    }
}
