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
    
    public partial class N_UserCharge
    {
        public int Id { get; set; }
        public string SsId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> BankId { get; set; }
        public string CheckCode { get; set; }
        public Nullable<decimal> InMoney { get; set; }
        public Nullable<decimal> DzMoney { get; set; }
        public Nullable<System.DateTime> STime { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> ActState { get; set; }
    }
}
