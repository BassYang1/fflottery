// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userUpdatePoint
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
  public partial class userUpdatePoint : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtName;
    protected TextBox txtId;
    protected TextBox txtPoint;
    protected DropDownList ddlPoint;
    protected DropDownList ddlGroup;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      this.getEditDropDownList(ref this.ddlPoint, 0);
      this.getGroupDropDownList(ref this.ddlGroup, 0);
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from V_User with(nolock) where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
      this.txtPoint.Text = dataTable.Rows[0]["Point"].ToString();
      this.ddlGroup.SelectedValue = Convert.ToInt32(dataTable.Rows[0]["UserGroup"]).ToString();
      this.ddlPoint.SelectedValue = dataTable.Rows[0]["UPoint"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      ListItem selectedItem1 = this.ddlGroup.SelectedItem;
      ListItem selectedItem2 = this.ddlPoint.SelectedItem;
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=" + this.txtId.Text;
      this.doh.AddFieldItem("Point", (object) Convert.ToDecimal(selectedItem2.Value));
      this.doh.AddFieldItem("UserGroup", (object) selectedItem1.Value);
      if (this.doh.Update("N_User") > 0)
        new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员返点", "修改了" + this.txtName.Text + "的返点，类型信息");
      this.FinalMessage("成功保存", "close.htm", 0);
    }
  }
}
