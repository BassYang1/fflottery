// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userBankEdit
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
  public partial class userBankEdit : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtName;
    protected TextBox txtId;
    protected TextBox txtUserId;
    protected TextBox txtPayBank;
    protected TextBox txtPayName;
    protected TextBox txtPayAccount;
    protected TextBox txtPayBankAddress;
    protected TextBox txtQuestion;
    protected TextBox txtAnswer;
    protected Label lblmsg;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from N_UserBank where Id=" + str + "order by Id desc";
      DataTable dataTable1 = this.doh.GetDataTable();
      if (dataTable1.Rows.Count > 0)
      {
        this.txtPayBank.Text = dataTable1.Rows[0]["PayBank"].ToString();
        this.txtPayAccount.Text = dataTable1.Rows[0]["PayAccount"].ToString();
        this.txtPayBankAddress.Text = dataTable1.Rows[0]["PayBankAddress"].ToString();
        this.txtUserId.Text = dataTable1.Rows[0]["UserId"].ToString();
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1 * from V_User where Id=" + dataTable1.Rows[0]["UserId"].ToString();
        DataTable dataTable2 = this.doh.GetDataTable();
        this.txtId.Text = dataTable2.Rows[0]["Id"].ToString();
        this.txtName.Text = dataTable2.Rows[0]["UserName"].ToString();
        this.txtPayName.Text = dataTable2.Rows[0]["TrueName"].ToString();
        if (string.IsNullOrEmpty(dataTable2.Rows[0]["Question"].ToString()) || string.IsNullOrEmpty(dataTable2.Rows[0]["Answer"].ToString()))
          this.FinalMessage("会员未绑定密保，不能编辑！", "close.htm", 0);
        else
          this.txtQuestion.Text = dataTable2.Rows[0]["Question"].ToString();
      }
      else
      {
        this.txtPayBank.Text = "---";
        this.txtPayAccount.Text = "---";
        this.txtPayBankAddress.Text = "---";
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.txtQuestion.Text.Trim()) || string.IsNullOrEmpty(this.txtAnswer.Text.Trim()))
      {
        this.FinalMessage("会员安全答案不正确，不能编辑！", "close.htm", 0);
      }
      else
      {
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1 * from N_User where Answer='" + this.txtAnswer.Text.Trim() + "' and Id=" + this.txtUserId.Text;
        if (this.doh.GetDataTable().Rows.Count > 0)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=" + this.txtId.Text;
          this.doh.AddFieldItem("PayName", (object) this.txtPayName.Text);
          this.doh.AddFieldItem("PayAccount", (object) this.txtPayAccount.Text);
          this.doh.AddFieldItem("PayBankAddress", (object) this.txtPayBankAddress.Text);
          this.doh.AddFieldItem("IsLock", (object) 1);
          if (this.doh.Update("N_UserBank") > 0)
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "编辑会员银行信息");
          this.FinalMessage("成功保存", "close.htm", 0);
        }
        else
          this.FinalMessage("会员安全答案不正确，不能编辑！", "close.htm", 0);
      }
    }
  }
}
