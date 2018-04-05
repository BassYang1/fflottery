// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserEmailDAL
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
  public class UserEmailDAL : ComData
  {
    protected SiteModel site;

    public UserEmailDAL()
    {
      this.site = new conSite().GetSite();
    }

    public void GetListCount(string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int num = dbOperHandler.Count("N_UserEmail");
        _jsonstr = "{\"result\" :\"1\",\"count\" :\"" + (object) num + "\"}";
      }
    }

    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string _userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) _userId);
        string str = string.Concat(dbOperHandler.GetField("N_User", "ParentId"));
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("N_UserEmail");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid," + str + " as parentid,dbo.f_GetUserName(SendId) as SendName,dbo.f_GetUserName(ReceiveId) as ReceiveName,*", "N_UserEmail", "STime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public int Save(string SendId, string ReceiveId, string Title, string Contents)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SendId", (object) SendId);
        dbOperHandler.AddFieldItem("ReceiveId", (object) ReceiveId);
        dbOperHandler.AddFieldItem("Title", (object) Title);
        dbOperHandler.AddFieldItem("Contents", (object) Contents);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("IsRead", (object) "0");
        return dbOperHandler.Insert("N_UserEmail");
      }
    }

    public void UpdateState(string _id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=@id";
        dbOperHandler.AddConditionParameter("@id", (object) _id);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        dbOperHandler.Update("N_UserEmail");
      }
    }

    public void Deletes(string _id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=@id";
        dbOperHandler.AddConditionParameter("@id", (object) _id);
        dbOperHandler.AddFieldItem("IsDel", (object) "1");
        dbOperHandler.Update("N_UserEmail");
      }
    }
  }
}
