// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserBankDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class UserBankDAL : ComData
  {
    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("N_UserBank");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by Id asc) as rowid,'************'+substring(Payaccount,len(Payaccount)-3,4) as tPayaccount,substring(PayName,1,1)+'**' as tPayName,*", "N_UserBank", "Id", _pagesize, _thispage, "asc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public int Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        new DateTimePubDAL().GetDateTime();
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("UserId", (object) userId);
        dbOperHandler.AddFieldItem("PayMethod", (object) PayMethod);
        dbOperHandler.AddFieldItem("PayBank", (object) PayBank);
        dbOperHandler.AddFieldItem("PayBankAddress", (object) PayBankAddress);
        dbOperHandler.AddFieldItem("PayAccount", (object) PayAccount);
        dbOperHandler.AddFieldItem("PayName", (object) PayName);
        dbOperHandler.AddFieldItem("AddTime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("IsLock", (object) 0);
        return dbOperHandler.Insert("N_UserBank");
      }
    }

    public bool Exists(string _wherestr)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr;
        if (dbOperHandler.Exist("N_UserBank"))
          num = 1;
      }
      return num == 1;
    }

    public void Delete(string Id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=" + Id;
        dbOperHandler.Delete("N_UserBank");
      }
    }
  }
}
