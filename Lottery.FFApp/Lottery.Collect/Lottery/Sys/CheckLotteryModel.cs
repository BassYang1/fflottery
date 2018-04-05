using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Collect.Sys
{
    public class CheckLotteryModel
    {
        /// <summary>
        /// 所有开奖号码
        /// </summary>
        public string NumberAll { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 收益
        /// </summary>
        public decimal Income { get; set; }

    }
}