// Decompiled with JetBrains decompiler
// Type: Lottery.AdminFile.Admin.sysNewsadd
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.AdminFile.Admin
{
  public class sysNewsadd : AdminCenter
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
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.AddFieldItem("Title", (object) this.txtTitle.Text);
      this.doh.AddFieldItem("Content", (object) this.txtContent.Value);
      this.doh.AddFieldItem("Color", (object) this.ddlColor.SelectedValue);
      this.doh.AddFieldItem("STime", (object) DateTime.Now);
      this.doh.AddFieldItem("IsUsed", (object) 1);
      this.doh.Insert("Sys_News");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "添加了" + this.txtTitle.Text + "系统公告");
      this.FinalMessage("操作成功", "/admin/close.htm", 0);
    }
  }
}
