using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Lottery.Entity
{
    /// <summary>
    /// 随笔付类型映射
    /// </summary>
    public class ChannelMapModel
    {
        /// <summary>
        /// 平台支付类型码
        /// </summary>
        public String SysCode { get; set; }

        /// <summary>
        /// 随笔付类型Id
        /// </summary>
        public String SbfChannel { get; set; }

        /// <summary>
        /// 随笔付类型码
        /// </summary>
        public String SbfCode { get; set; }
    }
}
