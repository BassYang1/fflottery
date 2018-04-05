// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.betCancelOfNO
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class betCancelOfNO : AdminCenter
  {
    protected HtmlForm form1;
    protected DropDownList ddlLottery;
    protected TextBox txtIssue;
    protected RadioButton rbo1;
    protected RadioButton rbo2;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getLotteryDropDownList(ref this.ddlLottery, 0);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (this.rbo1.Checked)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "IssueNum='" + this.txtIssue.Text.Trim() + "'";
        if (this.doh.Count("N_UserBet") > 0)
        {
          new LotteryCheck().Cancel(Convert.ToInt32(this.ddlLottery.SelectedItem.Value), this.txtIssue.Text.Trim(), 0);
          new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + this.txtIssue.Text + "期进行撤单");
          this.FinalMessage("撤单成功", "/admin/close.htm", 0);
        }
        else
          this.FinalMessage("该期号不存在投注记录，不能撤单", "/admin/betCancel.aspx", 0);
      }
      else
        this.FinalMessage("请您选中确认撤单选项", "/admin/betCancel.aspx", 0);
    }
  }
}
