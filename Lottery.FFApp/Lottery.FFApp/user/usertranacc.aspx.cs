// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.user.usertranacc
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.WebApp.user
{
  public partial class usertranacc : UserCenterSession
  {
    public string strUserName = "";
    public string strUserMoney = "";
    protected HtmlForm form1;
    protected Label lblMsg;
    protected TextBox txtId;
    protected TextBox txtMoney;
    protected RadioButton rdo1;
    protected RadioButton rdo2;
    protected TextBox txtNewPass1;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.IsPostBack)
        return;
      string str = "1";
      if (this.Request.QueryString["id"] != null)
        str = this.Request.QueryString["id"].ToString();
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.txtId.Text = str;
      object[] fields1 = this.doh.GetFields("N_User", "UserName,UserCode");
      this.strUserName = string.Concat(fields1[0]);
      if (string.Concat(fields1[1]).IndexOf("," + this.AdminId + ",") == -1)
      {
        this.FinalMessage("转账的会员不是您的下级不能转账！", "/statics/include/close.htm", 0);
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "Id=" + this.AdminId;
        object[] fields2 = this.doh.GetFields("N_User", "Money,IsTranAcc");
        this.strUserMoney = string.Concat(fields2[0]);
        if (Convert.ToInt32(fields2[1]) != 1)
          return;
        this.FinalMessage("您的账号禁止转账！", "/statics/include/close.htm", 0);
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.txtId.Text;
      if (string.Concat(this.doh.GetField("N_User", "UserCode")).IndexOf("," + this.AdminId + ",") == -1)
      {
        this.lblMsg.Text = "转账的会员不是您的下级不能转账！";
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=@id";
        this.doh.AddConditionParameter("@id", (object) this.AdminId);
        object[] fields = this.doh.GetFields("N_User", "Money,PayPass,IsTranAcc");
        if (fields.Length > 0)
        {
          if (Convert.ToInt32(fields[2]) == 1)
            this.lblMsg.Text = "您的账号禁止转账！";
          else if (Convert.ToDecimal(this.txtMoney.Text) < new Decimal(1))
            this.lblMsg.Text = "转账失败,转账金额错误！";
          else if (Convert.ToDecimal(this.txtMoney.Text) > Convert.ToDecimal(fields[0]))
            this.lblMsg.Text = "转账失败,您的可用余额不足";
          else if (!MD5.Last64(MD5.Lower32(this.txtNewPass1.Text.Trim())).Equals(fields[1].ToString()))
            this.lblMsg.Text = "转账失败,您的资金密码错误";
          else if (new Lottery.DAL.Flex.UserChargeDAL().SaveUpCharge(this.rdo1.Checked ? "0" : "1", this.AdminId, this.txtId.Text, Convert.ToDecimal(this.txtMoney.Text)) > 0)
          {
            new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员转账给Id为" + this.txtId.Text + "的会员！");
            this.FinalMessage("转账成功", "/statics/include/close.htm", 0);
          }
          else
            this.FinalMessage("转账失败", "/statics/include/close.htm", 0);
        }
      }
    }
  }
}
