// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userTrueNameEdit
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
  public partial class userTrueNameEdit : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtName;
    protected TextBox txtId;
    protected TextBox txtQuestion;
    protected TextBox txtAnswer;
    protected TextBox txtTrueName;
    protected Label lblmsg;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      if (this.IsPostBack)
        return;
      this.txtId.Text = str;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from V_User where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.txtName.Text = dataTable.Rows[0]["UserName"].ToString();
      this.txtTrueName.Text = dataTable.Rows[0]["TrueName"].ToString();
      if (string.IsNullOrEmpty(dataTable.Rows[0]["Question"].ToString()) || string.IsNullOrEmpty(dataTable.Rows[0]["Answer"].ToString()))
        this.FinalMessage("会员未绑定密保，不能修改！", "close.htm", 0);
      else
        this.txtQuestion.Text = dataTable.Rows[0]["Question"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtQuestion.Text.Trim()) || string.IsNullOrEmpty(this.txtAnswer.Text.Trim()))
      {
        this.FinalMessage("会员安全答案不正确，不能修改！", "close.htm", 0);
      }
      else
      {
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1 * from N_User where Answer='" + this.txtAnswer.Text.Trim() + "' and Id=" + this.txtId.Text;
        if (this.doh.GetDataTable().Rows.Count > 0)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=" + this.txtId.Text;
          this.doh.AddFieldItem("TrueName", (object) this.txtTrueName.Text);
          if (this.doh.Update("N_User") > 0)
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "修改真实姓名");
          this.FinalMessage("成功保存", "close.htm", 0);
        }
        else
          this.FinalMessage("会员安全答案不正确，不能修改！", "close.htm", 0);
      }
    }
  }
}
