// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.TiskDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
  public class TiskDAL : ComData
  {
    public void TiskOper()
    {
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        sqlDataAdapter.SelectCommand = sqlCommand;
        try
        {
          sqlDataAdapter.SelectCommand.CommandText = "SELECT * FROM [Sys_TaskSet] where getdate()>=StartTime and getdate()<=EndTime and IsUsed=1 and (BeforeTime is null or Convert(varchar(10),BeforeTime,120)<>Convert(varchar(10),getdate(),120)) order by Sort asc";
          DataTable dataTable = new DataTable();
          sqlDataAdapter.Fill(dataTable);
          if (dataTable.Rows.Count <= 0)
            return;
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            sqlCommand.CommandText = dataTable.Rows[index]["StrSql"].ToString();
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "update Sys_TaskSet set BeforeTime=getdate() where id=" + dataTable.Rows[index]["Id"].ToString();
            sqlCommand.ExecuteNonQuery();
          }
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
        }
      }
    }
  }
}
