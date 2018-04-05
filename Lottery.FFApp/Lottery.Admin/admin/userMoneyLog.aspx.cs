// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userMoneyLog
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class userMoneyLog : AdminCenter
  {
    public string url = "";
    protected HtmlForm form1;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT   UserId,SUM(MoneyChange) as Money\r\n                         FROM         N_UserMoneyLog\r\n                        WHERE     (MoneyAgo IS NULL)\r\n                        group by UserId\r\n                        order by SUM(MoneyChange) desc";
      DataTable dataTable = this.doh.GetDataTable();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        new UserTotalTran().MoneyOpers(SsId.MoneyLog, dataTable.Rows[index]["UserId"].ToString(), Convert.ToDecimal(dataTable.Rows[index]["Money"].ToString()), 0, 0, 0, 10, 1, "", "", "流水问题，系统自动补差", "");
    }
  }
}
