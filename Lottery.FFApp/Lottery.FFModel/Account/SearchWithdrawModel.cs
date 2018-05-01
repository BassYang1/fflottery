﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lottery.FFModel;

namespace Lottery.FFModel
{
    /// <summary>
    /// 提现查询条件
    /// </summary>
    [Description("提现查询条件")]
    public class SearchWithdrawModel
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
    }
}
