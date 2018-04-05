// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userQuotasEdit
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
  public partial class userQuotasEdit : AdminCenter
  {
    protected HtmlForm form1;
    protected RadioButton rbo1;
    protected RadioButton rbo2;
    protected TextBox txtId;
    protected RadioButton rbo3;
    protected RadioButton rbo4;
    protected RadioButton rbo5;
    protected RadioButton rbo6;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.rbo1.Checked)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=" + this.txtId.Text;
        this.doh.AddFieldItem("CheckTime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        this.doh.AddFieldItem("State", (object) 1);
        this.doh.Update("N_UserQuotas");
        this.doh.Reset();
        this.doh.ConditionExpress = "id=" + this.txtId.Text;
        object field = this.doh.GetField("N_UserQuotas", "UserId");
        if (this.rbo3.Checked)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "id=" + field;
          this.doh.GetField("N_User", "ParentId");
          this.doh.Reset();
          this.doh.SqlCmd = "select Id from N_User with(nolock) where ParentId=" + field;
          DataTable dataTable = this.doh.GetDataTable();
          int num = 0;
          while (num < dataTable.Rows.Count)
            ++num;
        }
        if (this.rbo4.Checked)
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select Id from N_User with(nolock) where ParentId=" + field;
          DataTable dataTable = this.doh.GetDataTable();
          int num = 0;
          while (num < dataTable.Rows.Count)
            ++num;
        }
        if (this.rbo5.Checked)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "id=" + field;
          this.doh.GetFields("N_User", "ParentId,Money");
        }
        if (!this.rbo6.Checked)
          ;
      }
      if (this.rbo2.Checked)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=" + this.txtId.Text;
        this.doh.AddFieldItem("CheckTime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        this.doh.AddFieldItem("State", (object) 2);
        this.doh.Update("N_UserQuotas");
      }
      new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员管理", "同意了Id为" + this.txtId.Text + "的会员回收申请");
      this.FinalMessage("操作成功", "/admin/close.htm", 0);
    }
  }
}
