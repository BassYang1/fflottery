using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 彩票下注状态
    /// </summary>
    [Description]
    public enum BetStateEnum
    {
        /// <summary>
        /// 彩票下注状态
        /// </summary>
        [Description]
        NONE = 0,

        /// <summary>
        /// 未开奖
        /// </summary>
        [Description]
        NOT_ISSUE = 1,

        /// <summary>
        /// 已撤销
        /// </summary>
        [Description]
        WITHDRAWNED = 2,

        /// <summary>
        /// 未中奖
        /// </summary>
        [Description]
        NOT_WIN = 3,

        /// <summary>
        /// 已中奖
        /// </summary>
        [Description]
        WINNED = 4
    }
}
