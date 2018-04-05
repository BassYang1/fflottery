// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LogUserLoginDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class LogUserLoginDAL : ComData
  {
    public void Save(string _userid, string _address, string _browser, string _system, string _IP)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("UserId", (object) _userid);
        dbOperHandler.AddFieldItem("Address", (object) _address);
        dbOperHandler.AddFieldItem("Browser", (object) _browser);
        dbOperHandler.AddFieldItem("System", (object) _system);
        dbOperHandler.AddFieldItem("LoginTime", (object) DateTime.Now.ToString());
        dbOperHandler.AddFieldItem("IP", (object) _IP);
        dbOperHandler.Insert("Log_UserLogin");
      }
    }

    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Log_UserLogin");
        string sql0 = SqlHelp.GetSql0("*", "Log_UserLogin", "LoginTime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void Deletes()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("Log_UserLogin");
      }
    }
  }
}
