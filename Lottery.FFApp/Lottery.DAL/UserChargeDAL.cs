// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserChargeDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class UserChargeDAL : ComData
  {
    protected SiteModel site;

    public UserChargeDAL()
    {
      this.site = new conSite().GetSite();
    }

    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("V_ChargeRecord");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "V_ChargeRecord", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public int Save(string orderno, string userId, string bankId, string checkCode, Decimal money)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        int num = 0;
        if (bankId == "888")
          num = 1;
        DateTime dateTime = new DateTimePubDAL().GetDateTime();
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) orderno);
        dbOperHandler.AddFieldItem("UserId", (object) userId);
        dbOperHandler.AddFieldItem("BankId", (object) bankId);
        dbOperHandler.AddFieldItem("CheckCode", (object) checkCode);
        dbOperHandler.AddFieldItem("InMoney", (object) money);
        dbOperHandler.AddFieldItem("DzMoney", (object) money);
        dbOperHandler.AddFieldItem("STime", (object) dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("State", (object) num);
        return dbOperHandler.Insert("N_UserCharge");
      }
    }

    public void DeleteLogs()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("N_UserCharge");
      }
    }

    public void GetListUpChargeJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("N_UserChargeLog");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetUserName(ToUserId) as ToUserName,*", "N_UserChargeLog", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }
  }
}
