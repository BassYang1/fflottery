// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.SendMessage
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
  public partial class SendMessage : AdminCenter
  {
    protected HtmlForm form1;
    protected DropDownList ddlType;
    protected TextBox txtName;
    protected TextBox txtMessage;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.ddlType.SelectedValue == "1")
      {
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1000 Id from V_User with(nolock) where IsOnline=1 order by Id asc";
        DataTable dataTable = this.doh.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          new UserMessageDAL().Save(dataTable.Rows[index]["Id"].ToString(), "即时信息", this.txtMessage.Text);
      }
      if (this.ddlType.SelectedValue == "2")
      {
        if (string.IsNullOrEmpty(this.txtName.Text))
        {
          this.FinalMessage("会员账号不能为空", "/admin/SendMessage.aspx", 0);
          return;
        }
        this.doh.Reset();
        this.doh.ConditionExpress = "UserName=@UserName";
        this.doh.AddConditionParameter("@UserName", (object) this.txtName.Text);
        object field = this.doh.GetField("N_User", "Id");
        if (string.IsNullOrEmpty(string.Concat(field)))
        {
          this.FinalMessage("会员账号不存在", "/admin/SendMessage.aspx", 0);
          return;
        }
        new UserMessageDAL().Save(field.ToString(), "即时信息", this.txtMessage.Text);
      }
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "发送即时信息");
      this.FinalMessage("信息发送成功", "/admin/close.htm", 0);
    }
  }
}
