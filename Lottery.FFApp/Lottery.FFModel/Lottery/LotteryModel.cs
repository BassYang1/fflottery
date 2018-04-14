using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.FFModel
{
    /// <summary>
    /// 彩票种类
    /// </summary>
    [Description("彩票种类")]
    public class LotteryModel
    {
        /// <summary>
        /// 彩票种类Id
        /// </summary>
        [Description("彩票种类Id")]
        public int Id { get; set; }

        /// <summary>
        /// 彩票名称
        /// </summary>
        [Description("彩票名称")]
        public string Title { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Description("编码")]
        public string Code { get; set; }

        /// <summary>
        /// 最小倍数
        /// </summary>
        [Description("最小倍数")]
        public decimal MinTimes { get; set; }

        /// <summary>
        /// 最大倍数
        /// </summary>
        [Description("最大倍数")]
        public decimal MaxTimes { get; set; }

        /// <summary>
        /// 是否允许使用
        /// </summary>
        [Description("是否允许使用")]
        public int IsOpen { get; set; }

        /// <summary>
        /// 封单时间
        /// </summary>
        [Description("封单时间")]
        public int CloseTime { get; set; }

        ///// <summary>
        ///// 彩票种类
        ///// </summary>
        //[Description("彩票种类")]
        //public int second { get; set; }

        /// <summary>
        /// 彩票排序
        /// </summary>
        [Description("彩票排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 彩票种类
        /// </summary>
        [Description("彩票种类")]
        public int Ltype { get; set; }

        ///// <summary>
        ///// 是否自动开奖
        ///// </summary>
        //[Description("是否自动开奖")]
        //public int IsAuto { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[Description("")]
        //public int IndexType { get; set; }

        ///// <summary>
        ///// 开奖URL
        ///// </summary>
        //[Description("开奖URL")]
        //public string Url { get; set; }

        ///// <summary>
        ///// 开奖URL
        ///// </summary>
        //[Description("开奖URL")]
        //public int AutoUrl { get; set; }

        ///// <summary>
        ///// 手机端是否允许使用
        ///// </summary>
        //[Description("手机端是否允许使用")]
        //public int IphoneIsOpen { get; set; }

        /// <summary>
        /// 手机端排序
        /// </summary>
        [Description("手机端排序")]
        public int IphoneSort { get; set; }

        ///// <summary>
        ///// 手机端票描述
        ///// </summary>
        //[Description("手机端票描述")]
        //public string IphoneRemark { get; set; }

        ///// <summary>
        ///// 手机端图标
        ///// </summary>
        //[Description("手机端图标")]
        //public string IphoneImg { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        [Description("期数")]
        public int IssNum { get; set; }

        ///// <summary>
        ///// 彩票种类
        ///// </summary>
        //[Description("彩票种类")]
        //public string ApiUrl { get; set; } 
    }
}
