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
    /// 投注记录
    /// </summary>
    [Description("投注记录")]
    public class UserBetRecordModel
    {
        /// <summary>
        /// 设注记录ID
        /// </summary>
        [Description("设注记录ID")]
        public String SsId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public String UserName { get; set; }

        /// <summary>
        /// 彩种
        /// </summary>
        [Description("彩种")]
        public String LotteryName { get; set; }

        /// <summary>
        /// 期号
        /// </summary>
        [Description("期号")]
        public String IssueNo { get; set; }

        /// <summary>
        /// 投注数量
        /// </summary>
        [Description("投注数量")]
        public Int32 Num { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        [Description("倍数")]
        public Int32 Times { get; set; }

        /// <summary>
        /// 每注金额
        /// </summary>
        [Description("每注金额")]
        public Decimal Price { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        [Description("投注金额")]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 投注赔率
        /// </summary>
        [Description("投注赔率")]
        public Decimal Bouns { get; set; }

        /// <summary>
        /// 中奖注数
        /// </summary>
        [Description("中奖注数")]

        public Int32 WinNum { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        [Description("中奖金额")]

        public Decimal WinAmount { get; set; }

        /// <summary>
        /// 投注设备
        /// </summary>
        [Description("投注设备")]
        public String Source { get; set; }

        /// <summary>
        /// 记录状态
        /// </summary>
        [Description("记录状态")]
        public String State { get; set; }
    }
}
