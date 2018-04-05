// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxSysBank
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxSysBank : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.CheckFormUrl())
        this.Response.End();
      this.Admin_Load("master", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxStates2":
          this.ajaxStates2();
          break;
        case "ajaxGetUserBankList":
          this.ajaxGetUserBankList();
          break;
        case "ajaxGetUserBankAllList":
          this.ajaxGetUserBankAllList();
          break;
        case "ajaxUnLock":
          this.ajaxUnLock();
          break;
        case "ajaxDel":
          this.ajaxDel();
          break;
        case "ajaxGetUserBankLog":
          this.ajaxGetUserBankLog();
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

    private void ajaxGetList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      int num3 = this.Str2Int(this.q("flag"), 0);
      string str = "";
      string whereStr = num3 != 0 ? str + " IsGetCash=0" : str + " IsCharge=0";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_Bank");
      string sql0 = SqlHelp.GetSql0("*", "Sys_Bank", "id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Bank", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_Bank");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "编辑了Id为" + str + "的银行启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxStates2()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Bank", "flag"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("flag", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_Bank");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员银行", "编辑了Id为" + userid + "的提现银行启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetUserBankList()
    {
      int num1 = this.Str2Int(this.q("Id"), 0);
      this.Session["returnlink"] = (object) num1;
      int num2 = this.Int_ThisPage();
      int num3 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "UserId =" + (object) num1;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBank");
      string sql0 = SqlHelp.GetSql0("*", "N_UserBank", "Id", num3, num2, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num3, num2, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetUserBankLog()
    {
      int num1 = this.Str2Int(this.q("Id"), 0);
      this.Session["returnlink"] = (object) num1;
      int num2 = this.Int_ThisPage();
      int num3 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "UserId =" + (object) num1;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBankLog");
      string sql0 = SqlHelp.GetSql0("*", "N_UserBankLog", "Id", num3, num2, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num3, num2, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetUserBankAllList()
    {
      string str1 = this.q("u");
      string str2 = this.q("payname");
      string str3 = this.q("payaccount");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str1))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) like '%" + str1 + "%'";
      if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + " and payname like '%" + str2 + "%'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and payaccount like '%" + str3 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBank");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as username,*", "N_UserBank", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxUnLock()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("IsLock", (object) 0);
      int num = this.doh.Update("N_UserBank");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员银行", "解锁Id为" + userid + "的会员银行信息");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxDel()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      int num = this.doh.Delete("N_UserBank");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员银行", "删除了Id为" + userid + "的会员银行信息");
      if (num > 0)
        this._response = this.JsonResult(1, "删除成功");
      else
        this._response = this.JsonResult(0, "删除失败");
    }
  }
}
