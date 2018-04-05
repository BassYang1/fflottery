// Decompiled with JetBrains decompiler
// Type: Lottery.AdminFile.Admin.sysNewsedit
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.AdminFile.Admin
{
  public class sysNewsedit : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtTitle;
    protected TextBox txtId;
    protected HtmlTextArea txtContent;
    protected DropDownList ddlColor;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from Sys_News where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
      this.txtContent.Value = dataTable.Rows[0]["Content"].ToString();
      this.ddlColor.SelectedValue = dataTable.Rows[0]["Color"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.txtId.Text;
      this.doh.AddFieldItem("Title", (object) this.txtTitle.Text);
      this.doh.AddFieldItem("Content", (object) this.txtContent.Value);
      this.doh.AddFieldItem("Color", (object) this.ddlColor.SelectedValue);
      this.doh.Update("Sys_News");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "编辑了Id为" + this.txtId.Text + "的系统公告");
      this.FinalMessage("操作成功", "/admin/close.htm", 0);
    }
  }
}
