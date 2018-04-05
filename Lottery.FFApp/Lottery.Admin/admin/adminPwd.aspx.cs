// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.adminPwd
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class adminPwd : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtOldPass;
    protected TextBox txtNewPass1;
    protected TextBox txtNewPass2;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("Sys_Admin", "Password");
      if (field != null)
      {
        if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtOldPass.Text)))
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=@Id";
          this.doh.AddConditionParameter("@Id", (object) this.AdminId);
          this.doh.AddFieldItem("Password", (object) MD5.Last64(MD5.Lower32(this.txtNewPass2.Text)));
          this.doh.AddFieldItem("IP", (object) Const.GetUserIp);
          this.doh.Update("Sys_Admin");
          new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "修改了管理员的密码");
          this.FinalMessage("密码修改成功", "/admin/close.htm", 0);
        }
        else
          this.FinalMessage("旧密码错误", "/admin/adminPwd2.aspx", 0);
      }
      else
        this.FinalMessage("未登录", "/admin/close.htm", 0);
    }
  }
}
