//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lottery.FFData
{
    using System;
    using System.Collections.Generic;
    
    public partial class N_UserMoneyLog
    {
        public int Id { get; set; }
        public string SsId { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> LotteryId { get; set; }
        public Nullable<int> PlayId { get; set; }
        public Nullable<int> SysId { get; set; }
        public Nullable<decimal> MoneyChange { get; set; }
        public Nullable<decimal> MoneyAgo { get; set; }
        public Nullable<decimal> MoneyAfter { get; set; }
        public Nullable<int> IsOk { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> STime { get; set; }
        public Nullable<System.DateTime> STime2 { get; set; }
        public Nullable<int> IsSoft { get; set; }
        public Nullable<int> IsFlag { get; set; }
        public Nullable<int> IsFlag2 { get; set; }
        public string Remark { get; set; }
        public string Md5Code { get; set; }
    }
}
