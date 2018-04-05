// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.HistoryDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System.Data;

namespace Lottery.DAL.Flex
{
  public class HistoryDAL : ComData
  {
    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        string sql0 = SqlHelp.GetSql0(dbOperHandler.CountId("Flex_History").ToString() + " as totalcount,row_number() over (order by STime desc) as rowid,case when moneychange>0 then Convert(varchar(20),moneychange) else '---' end inmoney,case when moneychange<0 then Convert(varchar(20),moneychange) else '---' end outmoney,*", "Flex_History", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }
  }
}
