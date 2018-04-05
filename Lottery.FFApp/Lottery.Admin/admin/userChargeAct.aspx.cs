// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userChargeAct
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class userChargeAct : AdminCenter
  {
    public string url = "";
    protected HtmlForm form1;
    protected Label lblSsid;
    protected Label lblUserName;
    protected Label lblPayMoney;
    protected TextBox txtPwd;
    protected Label lblMsg;
    protected TextBox txtId;
    protected TextBox txtUserId;
    protected TextBox txtTime;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 *,dbo.f_GetUserName(UserID) as UserName from N_UserCharge where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.txtUserId.Text = dataTable.Rows[0]["UserId"].ToString();
      this.lblUserName.Text = dataTable.Rows[0]["UserName"].ToString();
      this.lblPayMoney.Text = dataTable.Rows[0]["InMoney"].ToString();
      this.lblSsid.Text = dataTable.Rows[0]["SsId"].ToString();
      this.txtTime.Text = dataTable.Rows[0]["STime"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("Sys_Admin", "Password");
      if (field == null)
        return;
      if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtPwd.Text)))
      {
        this.doh.Reset();
        this.doh.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='Charge' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) this.txtUserId.Text);
        DataTable dataTable = this.doh.GetDataTable();
        if (dataTable.Rows.Count > 0 && Convert.ToInt32(dataTable.Rows[0]["count"]) > 0)
          this.lblMsg.Text = "已派发，不能继续派发！";
        string act = SsId.Act;
        Decimal Money = new Decimal(50);
        this.doh.Reset();
        this.doh.AddFieldItem("SsId", (object) act);
        this.doh.AddFieldItem("UserId", (object) this.txtUserId.Text);
        this.doh.AddFieldItem("ActiveType", (object) "Charge");
        this.doh.AddFieldItem("ActiveName", (object) "首充佣金");
        this.doh.AddFieldItem("InMoney", (object) Money);
        this.doh.AddFieldItem("Remark", (object) "首充佣金");
        this.doh.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        this.doh.AddFieldItem("CheckIp", (object) "后台派发");
        this.doh.AddFieldItem("CheckMachine", (object) "后台派发");
        if (this.doh.Insert("Act_ActiveRecord") > 0)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=" + this.txtId.Text;
          this.doh.AddFieldItem("ActState", (object) 1);
          this.doh.Update("N_UserCharge");
          new UserTotalTran().MoneyOpers(act, this.txtUserId.Text, Money, 0, 0, 0, 9, 99, "", "", "首充佣金派发", "");
          new LogAdminOperDAL().SaveLog(this.AdminId, this.txtUserId.Text, "会员管理", "对会员" + this.txtUserId.Text + "首充佣金派发");
          this.FinalMessage("您成功派发首充佣金" + (object) Money + "元", "/admin/close.htm", 0);
        }
        else
          this.lblMsg.Text = "首充佣金派发失败！";
      }
      else
        this.lblMsg.Text = "安全密码错误！";
    }
  }
}
