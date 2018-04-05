// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxCharge
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxCharge : AdminCenter
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
        case "ajaxGetListOfNo":
          this.ajaxGetListOfNo();
          break;
        case "ajaxGetTranAccList":
          this.ajaxGetTranAccList();
          break;
        case "ajaxGetCashList":
          this.ajaxGetCashList();
          break;
        case "ajaxGetCashCheck":
          this.ajaxGetCashCheck();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxChargeSetList":
          this.ajaxChargeSetList();
          break;
        case "ajaxChargeSetStates":
          this.ajaxChargeSetStates();
          break;
        case "ajaxGetActChargeList":
          this.ajaxGetActChargeList();
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
      this.q("keys");
      string str1 = this.q("bank");
      string str2 = this.q("state");
      string str3 = this.q("sel");
      string str4 = this.q("u");
      string str5 = this.q("money");
      string str6 = this.q("d1");
      string str7 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str8 = "";
      string whereStr = "";
      if (str6.Trim().Length == 0)
        str6 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      if (str7.Trim().Length == 0)
        str7 = this.EndTime;
      if (Convert.ToDateTime(str6) > Convert.ToDateTime(str7))
        str6 = str7;
      string str9 = this.q("id");
      if (!string.IsNullOrEmpty(str9))
      {
        whereStr = whereStr + " ssid ='" + str9 + "'";
      }
      else
      {
        if (str6.Trim().Length > 0 && str7.Trim().Length > 0)
          whereStr = whereStr + " STime >='" + str6 + "' and STime <'" + str7 + "'";
        if (!string.IsNullOrEmpty(str1))
          whereStr = whereStr + " and BankId=" + str1;
        if (!string.IsNullOrEmpty(str2))
          whereStr = whereStr + " and state=" + str2;
        if (!string.IsNullOrEmpty(str4))
        {
          if (str3.Equals("username"))
            whereStr = whereStr + " and dbo.f_GetUserName(UserId) like '" + str4 + "%'";
          if (str3.Equals("ssid"))
            whereStr = whereStr + " and ssid like '" + str4 + "%'";
          if (str3.Equals("checkcode"))
            whereStr = whereStr + " and checkcode like '" + str4 + "%'";
        }
        if (!string.IsNullOrEmpty(str5))
          whereStr = whereStr + " and Inmoney >=" + str5;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_ChargeRecord");
      string str10 = str8 + " SELECT '' as UserName,'0' as [Id],'全部合计' as [ssid],'' as [UserId],'0' as [money],'' as [BankId],\r\n                        '' as [BankName],'' as [CheckCode],isnull(sum([InMoney]),0) as[InMoney],isnull(sum([DzMoney]),0) as[DzMoney],getdate() as [STime],\r\n                        '-1' as [State],'' as [StateName],'' as [ActState] FROM [V_ChargeRecord] where " + whereStr + " union all " + " select * from ( " + SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", num2, num1, "desc", whereStr) + " ) YouleTable order by Id desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str10;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetListOfNo()
    {
      this.q("keys");
      string str1 = this.q("bank");
      string str2 = this.q("state");
      string str3 = this.q("sel");
      string str4 = this.q("u");
      string str5 = this.q("money");
      string str6 = this.q("d1");
      string str7 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str6.Trim().Length == 0)
        str6 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      if (str7.Trim().Length == 0)
        str7 = this.EndTime;
      if (Convert.ToDateTime(str6) > Convert.ToDateTime(str7))
        str6 = str7;
      string str8 = this.q("id");
      if (!string.IsNullOrEmpty(str8))
      {
        whereStr = whereStr + " ssid ='" + str8 + "'";
      }
      else
      {
        if (str6.Trim().Length > 0 && str7.Trim().Length > 0)
          whereStr = whereStr + " STime >='" + str6 + "' and STime <'" + str7 + "'";
        if (!string.IsNullOrEmpty(str1))
          whereStr = whereStr + " and BankId=" + str1;
        if (!string.IsNullOrEmpty(str2))
          whereStr = whereStr + " and state=" + str2;
        if (!string.IsNullOrEmpty(str4))
        {
          if (str3.Equals("username"))
            whereStr = whereStr + " and dbo.f_GetUserName(UserId) like '" + str4 + "%'";
          if (str3.Equals("ssid"))
            whereStr = whereStr + " and ssid like '" + str4 + "%'";
          if (str3.Equals("checkcode"))
            whereStr = whereStr + " and checkcode like '" + str4 + "%'";
        }
        if (!string.IsNullOrEmpty(str5))
          whereStr = whereStr + " and Inmoney >=" + str5;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_ChargeRecordOfNo");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecordOfNo", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetActChargeList()
    {
      this.q("keys");
      string str1 = this.q("bank");
      string str2 = this.q("sel");
      string str3 = this.q("u");
      string str4 = this.q("d1");
      string str5 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str6 = "Id in (SELECT Min(Id) FROM [N_UserCharge] where InMoney>=100 and State=1 ";
      if (str4.Trim().Length == 0)
        str4 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      if (str5.Trim().Length == 0)
        str5 = this.EndTime;
      if (Convert.ToDateTime(str4) > Convert.ToDateTime(str5))
        str4 = str5;
      if (str4.Trim().Length > 0 && str5.Trim().Length > 0)
        str6 = str6 + " and STime >='" + str4 + "' and STime <'" + str5 + "'";
      if (!string.IsNullOrEmpty(str1))
        str6 = str6 + " and BankId=" + str1;
      if (!string.IsNullOrEmpty(str3))
        str6 = !str2.Equals("username") ? str6 + " and ssid like '%" + str3 + "%'" : str6 + " and dbo.f_GetUserName(UserId) like '%" + str3 + "%'";
      string whereStr = str6 + " group by UserId) and ActState=0  ";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_ChargeRecord");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetTranAccList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel");
      string str4 = this.q("u");
      string str5 = this.q("money");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str6 = "";
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str7 = this.q("id");
      if (!string.IsNullOrEmpty(str7))
      {
        whereStr = whereStr + " ssid ='" + str7 + "'";
      }
      else
      {
        if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
          whereStr = whereStr + " STime >='" + str1 + "' and STime <'" + str2 + "'";
        if (!string.IsNullOrEmpty(str4))
          whereStr = whereStr + " and dbo.f_GetUserName(" + str3 + ") like '%" + str4 + "%'";
        if (!string.IsNullOrEmpty(str5))
          whereStr = whereStr + " and Inmoney >=" + str5;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserChargeLog");
      string str8 = str6 + " SELECT  '' as UserName,'' as ToUserName,'0' as [Id],'全部合计' as [ssid],'' as [Type],'' as [UserId],'' as [ToUserId],isnull(sum([MoneyChange]),0) as [MoneyChange],\r\n                        getdate() as [STime],'' as [Remark] FROM [N_UserChargeLog] where " + whereStr + " union all " + " select * from ( " + SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserName(ToUserId) as ToUserName,*", "N_UserChargeLog", "Id", num2, num1, "desc", whereStr) + " ) YouleTable order by Id desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str8;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetCashList()
    {
      this.q("keys");
      string str1 = this.q("issoft");
      string str2 = this.q("u");
      string str3 = this.q("sel");
      string str4 = this.q("u2");
      string str5 = this.q("sel2");
      string str6 = this.q("d1");
      string str7 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str8 = "";
      string whereStr = "";
      if (str6.Trim().Length == 0)
        str6 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
      if (str7.Trim().Length == 0)
        str7 = this.EndTime;
      if (Convert.ToDateTime(str6) > Convert.ToDateTime(str7))
        str6 = str7;
      string str9 = this.q("id");
      if (!string.IsNullOrEmpty(str9))
      {
        whereStr = whereStr + " ssid ='" + str9 + "'";
      }
      else
      {
        if (str6.Trim().Length > 0 && str7.Trim().Length > 0)
          whereStr = whereStr + " STime >='" + str6 + "' and STime <'" + str7 + "'";
        if (!string.IsNullOrEmpty(str1))
          whereStr = whereStr + " and State=" + str1;
        if (!string.IsNullOrEmpty(str2))
          whereStr = whereStr + " and " + str3 + "  like '%" + str2 + "%'";
        if (!string.IsNullOrEmpty(str4))
          whereStr = whereStr + " and " + str5 + " >=" + str4;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserGetCash");
      string str10 = str8 + " SELECT '0' as [Id],'全部合计' as [ssid],'' as [UserId],'' as [UserName],'' as [BankId],'' as [PayMethod],\r\n                        '' as [PayBank],'' as [PayName],'' as [tPayName],'' as [PayAccount],'' as [tPayAccount],isnull(sum([CashMoney]),0) as [CashMoney],\r\n                        '0' as [Money],getdate() as [STime],'-1' as [State],'' as [StateName],getdate() as [STime2],'' as [Msg],'0' as [bet] \r\n                        FROM [V_UserGetCash] where " + whereStr + " union all " + " select * from ( " + SqlHelp.GetSql0("*", "V_UserGetCash", "Id", num2, num1, "desc", whereStr) + " ) YouleTable order by Id desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str10;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetCashCheck()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("u");
      string str4 = this.q("sel");
      string str5 = this.q("u2");
      string str6 = this.q("sel2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "(State=0 or State=99)";
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object[] fields = this.doh.GetFields("Sys_Admin", "GroupId,MinCash,MaxCash");
      if (Convert.ToInt32(fields[0]) == 2)
        whereStr = whereStr + " and (CashMoney>=" + fields[1] + " and CashMoney<" + fields[2] + ")";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and " + str4 + "  like '%" + str3 + "%'";
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and " + str6 + " >=" + str5;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserGetCash");
      string sql0 = SqlHelp.GetSql0("*", "V_UserGetCash", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStates()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("State", (object) 99);
      int num = this.doh.Update("N_UserGetCash");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员充值", "忽略了Id为" + userid + "的提现申请");
      if (num > 0)
        this._response = this.JsonResult(1, "忽略成功");
      else
        this._response = this.JsonResult(0, "忽略失败");
    }

    private void ajaxChargeSetList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_ChargeSet");
      string sql0 = SqlHelp.GetSql0("*", "Sys_ChargeSet", "Sort", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxChargeSetStates()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_ChargeSet", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_ChargeSet");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员充值", "编辑Id为" + userid + "的充值配置启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }
  }
}
