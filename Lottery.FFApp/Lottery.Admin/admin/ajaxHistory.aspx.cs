// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxHistory
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxHistory : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxGetListRG":
          this.ajaxGetListRG();
          break;
        case "ajaxGetListById":
          this.ajaxGetListById();
          break;
        case "ajaxGetAdminList":
          this.ajaxGetAdminList();
          break;
        case "ajaxGetRepeatList":
          this.ajaxGetRepeatList();
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
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel");
      string str4 = this.q("u");
      string str5 = this.q("tid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str6 = "";
      string whereStr = " STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and Code ='" + str5 + "'";
      if (!string.IsNullOrEmpty(str4))
      {
        if (str3.Equals("Remark"))
          whereStr = whereStr + " and " + str3 + " like '%" + str4 + "%'";
        else
          whereStr = whereStr + " and " + str3 + " = '" + str4 + "'";
      }
      string str7 = str6 + " select * from ( ";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_History");
      string str8 = str7 + SqlHelp.GetSql0("*", "V_History", "STime", num2, num1, "desc", whereStr) + " ) YouleTable order by STime desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str8;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetRepeatList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("tid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str4 = "Id in(select MIN(Id) from [N_UserMoneyLog] a where ";
      string str5 = string.IsNullOrEmpty(str3) ? str4 + " Code = 5" : str4 + " Code ='" + str3 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        str5 = str5 + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      string whereStr = str5 + " group by a.SysId,UserId,MoneyChange having count(a.SysId)>1)";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_History");
      string sql0 = SqlHelp.GetSql0("*", "V_History", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetListRG()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel");
      string str4 = this.q("u");
      this.q("tid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = "IsSoft=0 and Code <> 7 and Code <>8 and Code <>9 and Code <>11";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and " + str3 + " = '" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_History");
      string sql0 = SqlHelp.GetSql0("*", "V_History", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetListById()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      this.q("u");
      string str3 = this.q("id");
      string str4 = this.q("tid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = "UserId=" + str3;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and Code ='" + str4 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_History");
      string sql0 = SqlHelp.GetSql0("Id,UserId,UName,LotteryId,LotteryName,PlayId,PlayName,SysId,IssueNum,SingleMoney,MoneyChange,MoneyAgo,MoneyAfter,IsOk,Content,STime,Code,CodeName,IsSoft,IsSoftName,IsFlag,IsFlag2,Remark", "V_History", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetAdminList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = "";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " STime >='" + str1 + "' and STime <'" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_AdminHistory");
      string sql0 = SqlHelp.GetSql0("*", "V_AdminHistory", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
