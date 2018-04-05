using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Collect.Boyi
{
    /// <summary>
    /// 博易网彩票开奖数据
    /// </summary>
    public class ByResponse
    {
        public List<ByLottery> data { get; set; }
    }

    /// <summary>
    /// 开奖信息
    /// </summary>
    public class ByLottery
    {
        /// <summary>
        /// 开奖时间
        /// </summary>
        public string opentime { get; set; }

        /// <summary>
        /// 开奖期号
        /// </summary>
        public string expect { get; set; }

        /// <summary>
        /// 开奖号码
        /// </summary>
        public string opencode { get; set; }
    }
}