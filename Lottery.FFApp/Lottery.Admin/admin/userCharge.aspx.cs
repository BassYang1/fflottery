// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.usercharge
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
  public class usercharge : AdminCenter
  {
    protected HtmlForm form1;
    protected DropDownList ddlType;
    protected TextBox txtName;
    protected TextBox txtId;
    protected TextBox txtMoney;
    protected TextBox txtPwd;
    protected TextBox txtRemark;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from N_User with(nolock) where Id=" + str;
      this.txtName.Text = this.doh.GetDataTable().Rows[0]["UserName"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("Sys_Admin", "Password");
      if (field != null)
      {
        if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtPwd.Text)))
        {
          DateTime now = DateTime.Now;
          Random random = new Random();
          string charge = SsId.Charge;
          Decimal num = Convert.ToDecimal(this.txtMoney.Text);
          switch (this.ddlType.SelectedValue)
          {
            case "1":
              if (new UserTotalTran().MoneyOpers(SsId.Charge, this.txtId.Text, num, 0, 0, 0, 1, 0, "平台资金操作", "您成功充值" + this.txtMoney.Text + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", this.txtRemark.Text, "") > 0)
              {
                new UserChargeDAL().Save(charge, this.txtId.Text, "888", "", num);
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "充值了" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "2":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 2, 0, "平台资金操作", "平台取款扣款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "取款扣款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "3":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, -num, 0, 0, 0, 3, 0, "平台资金操作", "平台投注扣款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "投注扣款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "5":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 5, 0, "平台资金操作", "平台奖金加款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "奖金加款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "4":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 4, 0, "平台资金操作", "平台返点加款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "返点加款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "9":
              if (new UserTotalTran().MoneyOpers(SsId.Act, this.txtId.Text, num, 0, 0, 0, 9, 0, "平台资金操作", "平台活动加款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "活动加款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "6":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 6, 0, "平台资金操作", "平台撤单加款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "撤单返款" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "12":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 12, 0, "平台资金操作", "平台分红加款" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "增加其他金额" + this.txtMoney.Text + "元");
                break;
              }
              break;
            case "10":
              if (new UserTotalTran().MoneyOpers(SsId.MoneyLog, this.txtId.Text, num, 0, 0, 0, 10, 0, "平台资金操作", "平台其他资金" + this.txtMoney.Text + "元", this.txtRemark.Text, "") > 0)
              {
                new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金操作", "平台为" + this.txtName.Text + "增加其他金额" + this.txtMoney.Text + "元");
                break;
              }
              break;
          }
          this.FinalMessage("操作成功！", "close.htm", 0);
        }
        else
          this.FinalMessage("管理员密码错误！", "close.htm", 0);
      }
      else
        this.FinalMessage("管理员密码错误！", "close.htm", 0);
    }
  }
}
