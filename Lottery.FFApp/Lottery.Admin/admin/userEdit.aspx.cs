// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.useredit
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
  public class useredit : AdminCenter
  {
    private string trueName = "";
    protected HtmlForm form1;
    protected TextBox txtId;
    protected DropDownList ddlGroup;
    protected TextBox txtGroup;
    protected TextBox txtName;
    protected TextBox txtLoginPwd;
    protected TextBox txtParent;
    protected TextBox txtBankPwd;
    protected TextBox txtCode;
    protected DropDownList ddlPoint;
    protected TextBox txtPoint;
    protected TextBox txtOnline;
    protected TextBox txtMoney;
    protected TextBox txtOntime;
    protected TextBox txtScore;
    protected TextBox txtIp;
    protected DropDownList ddlIsEnable;
    protected TextBox txtRegtime;
    protected DropDownList ddlIsBet;
    protected TextBox txtPayBank;
    protected DropDownList ddlIsGetcash;
    protected TextBox txtPayBankAddress;
    protected DropDownList ddlIsTranAcc;
    protected TextBox txtPayAccount;
    protected TextBox txtQuestion;
    protected TextBox txtPayName;
    protected TextBox tipPayName;
    protected TextBox txtAnswer;
    protected TextBox tipAnswer;
    protected TextBox txtEmail;
    protected TextBox txtMobile;
    protected TextBox txtEnableSeason;
    protected Label lblmsg;
    protected TextBox lblCookie;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      string str1 = this.txtId.Text = this.Str2Str(this.q("id"));
      this.getEditDropDownList(ref this.ddlPoint, 0);
      this.getGroupDropDownList(ref this.ddlGroup, 0);
      if (this.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from V_User where Id=" + str1;
      DataTable dataTable1 = this.doh.GetDataTable();
      this.txtId.Text = dataTable1.Rows[0]["Id"].ToString();
      this.txtName.Text = dataTable1.Rows[0]["UserName"].ToString();
      this.txtParent.Text = dataTable1.Rows[0]["ParentName"].ToString();
      this.ddlGroup.SelectedValue = Convert.ToInt32(dataTable1.Rows[0]["UserGroup"]).ToString();
      this.txtGroup.Text = dataTable1.Rows[0]["UserGroup"].ToString();
      this.txtMoney.Text = dataTable1.Rows[0]["Money"].ToString();
      this.txtScore.Text = dataTable1.Rows[0]["Score"].ToString();
      this.ddlPoint.SelectedValue = dataTable1.Rows[0]["UPoint"].ToString();
      this.txtPoint.Text = dataTable1.Rows[0]["UPoint"].ToString();
      this.txtQuestion.Text = dataTable1.Rows[0]["Question"].ToString();
      this.txtAnswer.Text = string.IsNullOrEmpty(dataTable1.Rows[0]["Answer"].ToString()) ? "" : dataTable1.Rows[0]["Answer"].ToString().Substring(0, 1) + "*";
      this.tipAnswer.Text = dataTable1.Rows[0]["Answer"].ToString();
      this.txtRegtime.Text = dataTable1.Rows[0]["RegTime"].ToString();
      this.txtOntime.Text = dataTable1.Rows[0]["OnTime"].ToString();
      this.txtIp.Text = dataTable1.Rows[0]["IP"].ToString();
      this.txtOnline.Text = Convert.ToInt32(dataTable1.Rows[0]["IsOnline"].ToString()) == 0 ? "离线" : "在线";
      this.lblCookie.Text = dataTable1.Rows[0]["SessionId"].ToString();
      this.ddlIsEnable.SelectedValue = dataTable1.Rows[0]["IsEnable"].ToString();
      this.ddlIsBet.SelectedValue = dataTable1.Rows[0]["IsBet"].ToString();
      this.ddlIsGetcash.SelectedValue = dataTable1.Rows[0]["IsGetcash"].ToString();
      this.ddlIsTranAcc.SelectedValue = dataTable1.Rows[0]["IsTranAcc"].ToString();
      this.txtEnableSeason.Text = dataTable1.Rows[0]["EnableSeason"].ToString();
      this.trueName = dataTable1.Rows[0]["TrueName"].ToString();
      this.txtPayName.Text = this.trueName;
      this.tipPayName.Text = this.trueName;
      this.txtMobile.Text = string.IsNullOrEmpty(dataTable1.Rows[0]["Mobile"].ToString()) ? "" : dataTable1.Rows[0]["Mobile"].ToString().Substring(0, dataTable1.Rows[0]["Mobile"].ToString().Length - 3) + "***";
      this.txtEmail.Text = string.IsNullOrEmpty(dataTable1.Rows[0]["Email"].ToString()) ? "" : dataTable1.Rows[0]["Email"].ToString().Substring(0, dataTable1.Rows[0]["Email"].ToString().Length - 3) + "***";
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from N_UserBank where IsLock=1 and UserId=" + str1 + "order by Id desc";
      DataTable dataTable2 = this.doh.GetDataTable();
      if (dataTable2.Rows.Count < 1)
      {
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1 * from N_UserBank where IsLock=1 and UserId=" + str1 + "order by Id desc";
        dataTable2 = this.doh.GetDataTable();
      }
      if (dataTable2.Rows.Count > 0)
      {
        string str2 = dataTable2.Rows[0]["PayBank"].ToString();
        this.txtPayBank.Text = string.IsNullOrEmpty(str2) ? "" : str2.Substring(0, 1) + "***" + str2.Substring(str2.Length - 1, 1);
        string str3 = dataTable2.Rows[0]["PayAccount"].ToString();
        this.txtPayAccount.Text = string.IsNullOrEmpty(str3) ? "" : str3.Substring(0, 4) + "***" + str3.Substring(str3.Length - 4, 3);
        string str4 = dataTable2.Rows[0]["PayBankAddress"].ToString();
        this.txtPayBankAddress.Text = string.IsNullOrEmpty(str4) ? "" : str4.Substring(0, 1) + "***" + str4.Substring(str4.Length - 1, 1);
      }
      else
      {
        this.txtPayBank.Text = "---";
        this.txtPayAccount.Text = "---";
        this.txtPayBankAddress.Text = "---";
      }
      string[] strArray = dataTable1.Rows[0]["UserCode"].ToString().Replace(",,", "_").Replace(",", "").Split('_');
      if (strArray.Length > 1)
      {
        for (int index = 0; index < strArray.Length - 1; ++index)
        {
          if (!string.IsNullOrEmpty(strArray[index]))
          {
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=" + strArray[index];
            TextBox txtCode = this.txtCode;
            txtCode.Text = txtCode.Text + this.doh.GetField("N_User", "UserName") + "||";
          }
        }
        this.txtCode.Text = this.txtCode.Text.Substring(0, this.txtCode.Text.Length - 2);
      }
      else
        this.txtCode.Text = "---";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      ListItem selectedItem1 = this.ddlGroup.SelectedItem;
      ListItem selectedItem2 = this.ddlPoint.SelectedItem;
      if (!this.CheckUserGroup(this.txtId.Text, selectedItem1.Value))
        this.lblmsg.Text = "会员类型不能高于上级或和上级持平！";
      else if (!this.CheckUserPoint(this.txtId.Text, selectedItem2.Value))
      {
        this.lblmsg.Text = "会员返点不能高于上级返点！";
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "Id=" + this.txtId.Text;
        if (!string.IsNullOrEmpty(this.txtLoginPwd.Text))
        {
          this.doh.AddFieldItem("Password", (object) MD5.Last64(MD5.Lower32(this.txtLoginPwd.Text)));
          new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "登录密码", "修改登录密码");
        }
        if (!string.IsNullOrEmpty(this.txtBankPwd.Text))
        {
          this.doh.AddFieldItem("PayPass", (object) MD5.Last64(MD5.Lower32(this.txtBankPwd.Text)));
          new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "资金密码", "修改资金密码");
        }
        this.doh.AddFieldItem("Score", (object) this.txtScore.Text);
        this.doh.AddFieldItem("Point", (object) Convert.ToDecimal(selectedItem2.Value));
        this.doh.AddFieldItem("UserGroup", (object) selectedItem1.Value);
        if (!this.tipAnswer.Text.Trim().Equals(this.txtAnswer.Text.Trim()))
        {
          this.doh.AddFieldItem("Answer", (object) this.txtAnswer.Text);
          new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "密保答案", "密保答案从 " + this.tipAnswer.Text + " 修改为 " + this.txtAnswer.Text);
        }
        if (!this.tipPayName.Text.Trim().Equals(this.txtPayName.Text.Trim()))
        {
          this.doh.AddFieldItem("TrueName", (object) this.txtPayName.Text);
          new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员银行", "真实姓名从 " + this.tipPayName.Text + " 修改为 " + this.txtPayName.Text);
        }
        this.doh.AddFieldItem("IsEnable", (object) this.ddlIsEnable.SelectedValue);
        this.doh.AddFieldItem("IsBet", (object) this.ddlIsBet.SelectedValue);
        this.doh.AddFieldItem("IsGetcash", (object) this.ddlIsGetcash.SelectedValue);
        this.doh.AddFieldItem("IsTranAcc", (object) this.ddlIsTranAcc.SelectedValue);
        this.doh.AddFieldItem("EnableSeason", (object) this.txtEnableSeason.Text);
        if (this.doh.Update("N_User") > 0)
        {
          Decimal num1 = Convert.ToDecimal(this.txtGroup.Text.Trim()) - Convert.ToDecimal(selectedItem1.Value);
          Decimal num2 = Convert.ToDecimal(this.txtPoint.Text.Trim()) - Convert.ToDecimal(selectedItem2.Value);
          if (num2 != new Decimal(0))
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员返点", "修改会员返点");
          if (num1 != new Decimal(0))
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员类型", "修改会员类型");
          if (num2 > new Decimal(0))
          {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,Point FROM [N_User] where usercode like '%" + Strings.PadLeft(this.txtId.Text.Trim()) + "%' and Id<>" + this.txtId.Text.Trim();
            DataTable dataTable = this.doh.GetDataTable();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (Convert.ToDecimal(dataTable.Rows[index]["Point"]) > Convert.ToDecimal(num2))
              {
                this.doh.Reset();
                this.doh.ConditionExpress = "Id=" + dataTable.Rows[index]["Id"];
                this.doh.AddFieldItem("Point", (object) (Convert.ToDecimal(dataTable.Rows[index]["Point"]) - num2));
                this.doh.Update("N_User");
              }
              else
              {
                this.doh.Reset();
                this.doh.ConditionExpress = "Id=" + dataTable.Rows[index]["Id"];
                this.doh.AddFieldItem("Point", (object) 0);
                this.doh.Update("N_User");
              }
            }
          }
          if (num1 > new Decimal(0))
          {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,UserGroup FROM [N_User] where usercode like '%" + Strings.PadLeft(this.txtId.Text.Trim()) + "%' and Id<>" + this.txtId.Text.Trim();
            DataTable dataTable = this.doh.GetDataTable();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
              if (Convert.ToDecimal(dataTable.Rows[index]["UserGroup"]) - Convert.ToDecimal(num1) >= new Decimal(0))
              {
                this.doh.Reset();
                this.doh.ConditionExpress = "Id=" + dataTable.Rows[index]["Id"];
                this.doh.AddFieldItem("UserGroup", (object) (Convert.ToDecimal(dataTable.Rows[index]["UserGroup"]) - num1));
                this.doh.Update("N_User");
              }
              else
              {
                this.doh.Reset();
                this.doh.ConditionExpress = "Id=" + dataTable.Rows[index]["Id"];
                this.doh.AddFieldItem("UserGroup", (object) 0);
                this.doh.Update("N_User");
              }
            }
          }
        }
        this.FinalMessage("成功保存", "close.htm", 0);
      }
    }

    private bool CheckUserGroup(string userId, string group)
    {
      if (Convert.ToInt32(group) > 0)
      {
        this.doh.Reset();
        this.doh.SqlCmd = "SELECT top 1 UserGroup FROM N_User where Id=(select ParentId from N_User where Id=" + userId + ")";
        DataTable dataTable = this.doh.GetDataTable();
        if (dataTable.Rows.Count > 0 && Convert.ToInt32(group) >= Convert.ToInt32(dataTable.Rows[0]["UserGroup"]))
          return false;
      }
      return true;
    }

    private bool CheckUserPoint(string userId, string point)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 1 Point FROM N_User where Id=(select ParentId from N_User where Id=" + userId + ")";
      DataTable dataTable = this.doh.GetDataTable();
      return dataTable.Rows.Count <= 0 || !(Convert.ToDecimal(point) > Convert.ToDecimal(dataTable.Rows[0]["Point"]));
    }
  }
}
