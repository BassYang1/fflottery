// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxWarn
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxWarn : AdminCenter
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
        case "ajaxWarnCount":
          this.ajaxWarnCount();
          break;
        case "ajaxBetOfWinWarn":
          this.ajaxBetOfWinWarn();
          break;
        case "ajaxBetOfPointWarn":
          this.ajaxBetOfPointWarn();
          break;
        case "ajaxStatOfRealWarn":
          this.ajaxStatOfRealWarn();
          break;
        case "ajaxStatOfActiveWarn":
          this.ajaxStatOfActiveWarn();
          break;
        case "ajaxStatOfFhWarn":
          this.ajaxStatOfFhWarn();
          break;
        case "ajaxBetOfYLLWarn":
          this.ajaxBetOfYLLWarn();
          break;
        case "ajaxUserOfIpWarn":
          this.ajaxUserOfIpWarn();
          break;
        case "ajaxGetCashWarn":
          this.ajaxGetCashWarn();
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

    private void ajaxWarnCount()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "select * from V_WarnCount";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxBetOfWinWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "WarnTotal");
      string whereStr = "State=3 and WinBonus>=" + (object) (string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString()));
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) LIKE '%" + str5 + "%'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and PlayId ='" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime2 >='" + str1 + "' and STime2 <'" + str2 + "'";
      string FldName = this.q("order");
      if (!string.IsNullOrEmpty(FldName))
      {
        if (FldName.Equals("bet"))
          FldName = "Times*Total";
      }
      else
        FldName = "winbonus";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBet");
      string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxBetOfPointWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "PointWarnTotal");
      string whereStr = "PointMoney>=" + (object) (string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString()));
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) LIKE '%" + str5 + "%'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and PlayId ='" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime2 >='" + str1 + "' and STime2 <'" + str2 + "'";
      string FldName = this.q("order");
      if (!string.IsNullOrEmpty(FldName))
      {
        if (FldName.Equals("bet"))
          FldName = "Times*Total";
      }
      else
        FldName = "PointMoney";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBet");
      string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,Times*Total as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStatOfRealWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("u");
      string FldName = this.q("order");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.StartTime);
        dateTime = dateTime.AddDays(0.0);
        str1 = dateTime.ToString("yyyy-MM-dd");
      }
      if (str2.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.EndTime);
        dateTime = dateTime.AddDays(1.0);
        str2 = dateTime.ToString("yyyy-MM-dd");
      }
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (string.IsNullOrEmpty(FldName))
        FldName = "total";
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "RealWarnTotal");
      Decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString());
      string whereStr = " STime >=Convert(varchar(10),'" + str1 + "',120)  and STime <Convert(varchar(10),'" + str2 + "',120) and total>=" + (object) num3;
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) = '" + str3 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStatOfActiveWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("u");
      string FldName = this.q("order");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.StartTime);
        dateTime = dateTime.AddDays(0.0);
        str1 = dateTime.ToString("yyyy-MM-dd");
      }
      if (str2.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.EndTime);
        dateTime = dateTime.AddDays(1.0);
        str2 = dateTime.ToString("yyyy-MM-dd");
      }
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (string.IsNullOrEmpty(FldName))
        FldName = "Give";
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "ActiveWarnTotal");
      Decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString());
      string whereStr = " STime >=Convert(varchar(10),'" + str1 + "',120)  and STime <Convert(varchar(10),'" + str2 + "',120) and Give>=" + (object) num3;
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) = '" + str3 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStatOfFhWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("u");
      string FldName = this.q("order");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.StartTime);
        dateTime = dateTime.AddDays(0.0);
        str1 = dateTime.ToString("yyyy-MM-dd");
      }
      if (str2.Trim().Length == 0)
      {
        DateTime dateTime = Convert.ToDateTime(this.EndTime);
        dateTime = dateTime.AddDays(1.0);
        str2 = dateTime.ToString("yyyy-MM-dd");
      }
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (string.IsNullOrEmpty(FldName))
        FldName = "STime";
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "FhWarnTotal");
      Decimal num3 = string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString());
      string whereStr = " STime >=Convert(varchar(10),'" + str1 + "',120)  and STime <Convert(varchar(10),'" + str2 + "',120) and Other>=" + (object) num3;
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) = '" + str3 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "V_UserMoneyStatAllDayOfUser", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxBetOfYLLWarn()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "YLLWarnTotal");
      string whereStr = "State=3 and WinBonus>=(Times*Total)*" + (object) ((string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString())) / new Decimal(100));
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) LIKE '%" + str5 + "%'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and PlayId ='" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime2 >='" + str1 + "' and STime2 <'" + str2 + "'";
      string FldName = this.q("order");
      if (!string.IsNullOrEmpty(FldName))
      {
        if (FldName.Equals("bet"))
          FldName = "Times*Total";
      }
      else
        FldName = "STime2";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserBet");
      string sql0 = SqlHelp.GetSql0("*", "V_UserBet", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxUserOfIpWarn()
    {
      string str1 = this.q("ip");
      string str2 = this.q("uname");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=0 and Ip in(select Ip from N_User with(nolock) where isDel=0 group by Ip having count(Ip)>1)";
      string FldName = "Id";
      if (!string.IsNullOrEmpty(str2))
      {
        whereStr = whereStr + " and UserName like '%" + str2.Trim() + "%'";
        FldName = "UserName";
      }
      if (!string.IsNullOrEmpty(str1))
      {
        whereStr = whereStr + " and ip like '%" + str1.Trim() + "%'";
        FldName = "Ip";
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", FldName, num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetCashWarn()
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
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      object field = this.doh.GetField("Sys_Info", "GetCashWarnTotal");
      string whereStr = "(State=0 or State=99) and CashMoney>(SELECT isnull(sum(Charge),0) FROM [N_UserMoneyStatAll] where UserId=V_UserGetCash.UserId)*" + (object) (string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(field.ToString()));
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and " + str4 + " like '%" + str3 + "%'";
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
  }
}
