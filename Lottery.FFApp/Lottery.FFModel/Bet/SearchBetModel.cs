using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.FFModel;

namespace Lottery.FFModel
{
    /// <summary>
    /// 投注查询条件
    /// </summary>
    [Description("投注查询条件")]
    public class SearchBetModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 下注状态
        /// </summary>
        [Description("下注状态")]
        public BetStateEnum State { get; set; }
    }
}
