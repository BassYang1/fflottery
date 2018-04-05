// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxMyinfo
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxMyinfo : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxCheckBank":
          this.ajaxCheckBank();
          break;
        case "ajaxBankList":
          this.ajaxBankList();
          break;
        case "ajaxBankBind":
          this.ajaxBankBind();
          break;
        case "changepass":
          this.ajaxChangePass();
          break;
        case "moneypass":
          this.ajaxMoneyPass();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this._response = this.JsonResult(0, "未知操作");
    }

    private void ajaxCheckBank()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id and PayAccount<>'' and PayName<>''";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      if (this.doh.Exist("N_User"))
        this._response = this.JsonResult(0, "绑定");
      else
        this._response = this.JsonResult(1, "未绑定");
    }

    private void ajaxBankList()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],[Bank] FROM [Sys_Bank] where IsUsed=0 ORDER BY Id asc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxBankBind()
    {
      string s = this.f("pass");
      string str1 = this.f("bank");
      string str2 = this.f("address");
      string str3 = this.f("payaccount");
      string str4 = this.f("payname");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) str1);
      object field = this.doh.GetField("Sys_Bank", "Bank");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      this.doh.AddFieldItem("PayMethod", (object) str1);
      this.doh.AddFieldItem("PayBank", field);
      this.doh.AddFieldItem("PayBankAddress", (object) str2);
      this.doh.AddFieldItem("PayAccount", (object) str3);
      this.doh.AddFieldItem("PayName", (object) str4);
      this.doh.AddFieldItem("PayPass", (object) MD5.Last64(s));
      this.doh.AddFieldItem("IP", (object) Const.GetUserIp);
      this.doh.Update("N_User");
      this._response = this.JsonResult(1, "银行绑定成功");
    }

    private void ajaxChangePass()
    {
      string s1 = this.f("oldpass");
      string s2 = this.f("newpass");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("Sys_Admin", "Password");
      if (field != null)
      {
        if (field.ToString().ToLower() == MD5.Last64(s1))
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=@Id";
          this.doh.AddConditionParameter("@Id", (object) this.AdminId);
          this.doh.AddFieldItem("Password", (object) MD5.Last64(s2));
          this.doh.AddFieldItem("IP", (object) Const.GetUserIp);
          this.doh.Update("Sys_Admin");
          this._response = this.JsonResult(1, "密码修改成功");
        }
        else
          this._response = this.JsonResult(0, "旧密码错误");
      }
      else
        this._response = this.JsonResult(0, "未登录");
    }

    private void ajaxMoneyPass()
    {
      string s1 = this.f("oldpass");
      string s2 = this.f("newpass");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("N_User", "PayPass");
      if (field != null)
      {
        if (field.ToString().ToLower() == MD5.Last64(s1))
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=@Id";
          this.doh.AddConditionParameter("@Id", (object) this.AdminId);
          this.doh.AddFieldItem("PayPass", (object) MD5.Last64(s2));
          this.doh.AddFieldItem("IP", (object) Const.GetUserIp);
          this.doh.Update("N_User");
          this._response = this.JsonResult(1, "密码修改成功");
        }
        else
          this._response = this.JsonResult(0, "旧密码错误");
      }
      else
        this._response = this.JsonResult(0, "未登录");
    }
  }
}
