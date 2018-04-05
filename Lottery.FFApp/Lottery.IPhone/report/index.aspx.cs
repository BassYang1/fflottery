// Decompiled with JetBrains decompiler
// Type: Lottery.Web.report.index
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Data;

namespace Lottery.Web.report
{
  public partial class index : UserCenterSession
  {
    public string act1 = "style=\"display:none;\"";

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT AgentId FROM [N_User] where Id=" + this.AdminId;
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count <= 0 || Convert.ToInt32(dataTable.Rows[0]["AgentId"]) == 0)
        return;
      this.act1 = "";
    }
  }
}
