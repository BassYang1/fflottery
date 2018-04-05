// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.NewsDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System.Data;

namespace Lottery.DAL
{
  public class NewsDAL : ComData
  {
    public void GetListJSONFlex(int _thispage, int _pagesize, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = " IsUsed=1";
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Sys_News").ToString() + " as totalcount,row_number() over (order by STime desc) as rowid,Convert(varchar(10),STime,120) as shortTime,substring(title,0,18)+'...' as shortTitle,*", "Sys_News", "STime", _pagesize, _thispage, "desc", " IsUsed=1");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON2(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListIndexJSONFlex(int _thispage, int _pagesize, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = " IsUsed=1 and IsIndex=1";
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Sys_News").ToString() + " as totalcount,row_number() over (order by STime desc) as rowid,Convert(varchar(10),STime,120) as shortTime,substring(title,0,18)+'...' as shortTitle,*", "Sys_News", "STime", _pagesize, _thispage, "desc", " IsUsed=1 and IsIndex=1");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON2(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetNewsInfoFlex(string Id, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select * from Sys_News where Id=" + Id;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = dataTable.Rows[0]["content"].ToString();
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Sys_News");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid,Convert(varchar(10),STime,120) as shortTime,substring(title,0,18)+'...' as shortTitle,Substring(Convert(varchar(10),STime,120),6,2) as tmonth,Substring(Convert(varchar(10),STime,120),9,2) as tday,*", "Sys_News", "STime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON(string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        string sql0 = SqlHelp.GetSql0("title,[Content],[STime]", "Sys_News", "STime", 1, 1, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON_Top1(ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 * from Sys_News where IsIndex=1 order by Id desc";
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0)
        {
          _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable1) + "}";
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = "select top 1 * from Act_ActiveSet where IsIndex=1 order by Id desc";
          DataTable dataTable2 = dbOperHandler.GetDataTable();
          _jsonstr = dataTable2.Rows.Count <= 0 ? "{\"result\" :\"0\",\"returnval\" :\"操作成功\"}" : "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable2) + "}";
        }
        dataTable1.Clear();
        dataTable1.Dispose();
      }
    }
  }
}
