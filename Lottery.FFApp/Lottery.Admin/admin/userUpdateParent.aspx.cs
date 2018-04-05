// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userUpdateParent
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
  public partial class userUpdateParent : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtName;
    protected TextBox txtPoint;
    protected TextBox txtGroup;
    protected TextBox txtToName;
    protected TextBox txtId;
    protected TextBox txtCode;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from N_User with(nolock) where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
      this.txtPoint.Text = Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[0]["Point"].ToString()) / new Decimal(10)).ToString("0.00");
      this.txtGroup.Text = dataTable.Rows[0]["UserGroup"].ToString();
      this.txtCode.Text = dataTable.Rows[0]["UserCode"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string pageMsg = new UserDAL().UpdateParentId(this.txtId.Text, this.txtToName.Text, this.txtPoint.Text, this.txtGroup.Text, this.txtCode.Text);
      new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员切线", "对会员" + this.txtName.Text + "进行切线，切到" + this.txtToName.Text);
      this.FinalMessage(pageMsg, "close.htm", 0);
    }
  }
}
