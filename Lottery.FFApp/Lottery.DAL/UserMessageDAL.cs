// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserMessageDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
  public class UserMessageDAL : ComData
  {
    public void Save(string _adminid, string _title, string _info)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("UserId", (object) _adminid);
        dbOperHandler.AddFieldItem("Title", (object) _title);
        dbOperHandler.AddFieldItem("Msg", (object) _info);
        dbOperHandler.AddFieldItem("Second", (object) 8);
        dbOperHandler.Insert("N_UserMessage");
      }
    }

    public void Save(SqlCommand cmd, int _userId, string _title, string _msg)
    {
      try
      {
        SqlParameter[] values = new SqlParameter[3]
        {
          new SqlParameter("@UserId", (object) _userId),
          new SqlParameter("@Title", (object) _title),
          new SqlParameter("@Msg", (object) _msg)
        };
        cmd.CommandText = "insert into N_UserMessage(UserId,Title,Msg,Second) values (@UserId,@Title,@Msg,8)";
        cmd.Parameters.AddRange(values);
        cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void GetListJSON(ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 100 UserId,Msg from N_UserMessage with(nolock) where IsRead=0 order by Id desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = dataTable.Rows.Count <= 0 ? "" : "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void Update(DataTable dt)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        for (int index = 0; index < dt.Rows.Count; ++index)
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "Id=@Id";
          dbOperHandler.AddConditionParameter("@Id", (object) dt.Rows[index]["Id"].ToString());
          dbOperHandler.AddFieldItem("IsRead", (object) "1");
          dbOperHandler.Update("N_UserMessage");
        }
      }
    }

    public void Delete()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("N_UserMessage");
      }
    }
  }
}
