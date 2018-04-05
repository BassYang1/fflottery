// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.PublicDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;

namespace Lottery.DAL.Flex
{
  public static class PublicDAL
  {
    public static DateTime GetServerTime()
    {
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        string str2 = "select GETDATE() AS ServerTime";
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = str2;
        DataTable dataTable = dbOperHandler.GetDataTable();
        str1 = dataTable.Rows[0]["ServerTime"].ToString();
        dataTable.Clear();
        dataTable.Dispose();
      }
      return Convert.ToDateTime(str1);
    }
  }
}
