// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userMoneyCFLog
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class userMoneyCFLog : AdminCenter
  {
    public string url = "";
    protected HtmlForm form1;
    protected Button btnSave;
    protected TextBox TextBox1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string str = string.Format("select UserId from [Act_ActiveRecord] \r\n\twhere CONVERT(varchar(10),STime,120)='{0}'\r\n\tgroup by UserId", (object) this.TextBox1.Text);
      this.doh.Reset();
      this.doh.SqlCmd = str;
      DataTable dataTable = this.doh.GetDataTable();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        this.doh.Reset();
        this.doh.ExecuteSql(string.Format("Insert into N_UserMoneyStatAll(UserId,[Charge],STime) values ({0},0,'{1} 00:00:00')", (object) dataTable.Rows[index]["UserId"].ToString(), (object) this.TextBox1.Text));
      }
    }
  }
}
