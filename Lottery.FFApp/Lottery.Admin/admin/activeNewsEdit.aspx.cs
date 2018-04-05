// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.activeNewsEdit
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
  public partial class activeNewsEdit : AdminCenter
  {
    protected HtmlForm form1;
    protected Label lblName;
    protected TextBox txtTitle;
    protected TextBox txtId;
    protected TextBox txtContent;
    protected HtmlTextArea txtRemark;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.IsPostBack)
        return;
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from Act_ActiveSet where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.lblName.Text = dataTable.Rows[0]["Name"].ToString();
      this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
      this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
      this.txtRemark.Value = dataTable.Rows[0]["Remark"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.txtId.Text;
      this.doh.AddFieldItem("Title", (object) this.txtTitle.Text);
      this.doh.AddFieldItem("Content", (object) this.txtContent.Text);
      this.doh.AddFieldItem("Remark", (object) this.txtRemark.Value);
      this.doh.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
      this.doh.Update("Act_ActiveSet");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "添加了活动公告！");
      this.FinalMessage("操作成功", "/admin/close.htm", 0);
    }
  }
}
