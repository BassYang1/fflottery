using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Lottery.Entity
{
    /// <summary>
    /// 提现
    /// </summary>
    public class CashGetModel
    {
        /// <summary>
        /// 提现单号
        /// </summary>
        public String SsId { get; set; }
        
        /// <summary>
        /// 提现用户名
        /// </summary>
        public String UserName { get; set; }
        
        /// <summary>
        /// 银行
        /// </summary>
        public String PayBank { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public String PayAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PayName { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Msg { get; set; }

    }
}
