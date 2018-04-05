// Decompiled with JetBrains decompiler
// Type: Lottery.Web.nav
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Data;

namespace Lottery.Web
{
    public partial class nav : UserCenterSession
    {
        public string tId = "1";
        public string loId = "1001";
        public string display = "";
        public string MinTimes = "1";
        public string MaxTimes = "1000";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");

            //彩种Id
            if (!string.IsNullOrEmpty(this.Request.QueryString["id"] ?? ""))
                this.loId = this.Request.QueryString["id"];

            //彩种分类Id
            if (!string.IsNullOrEmpty(this.Request.QueryString["tid"] ?? ""))
                this.tId = this.Request.QueryString["tid"];

            this.doh.Reset();
            this.doh.SqlCmd = "select MinTimes,MaxTimes from Sys_Lottery with(nolock) where Id=" + this.loId;
            DataTable dataTable = this.doh.GetDataTable();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                this.MinTimes = string.Concat(dataTable.Rows[0]["MinTimes"]);
                this.MaxTimes = string.Concat(dataTable.Rows[0]["MaxTimes"]);
                dataTable.Dispose();
                dataTable = null;
            }
        }
    }
}
