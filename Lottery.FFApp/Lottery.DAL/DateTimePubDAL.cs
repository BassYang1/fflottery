// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.DateTimePubDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;

namespace Lottery.DAL
{
  public class DateTimePubDAL : ComData
  {
    public DateTime GetDateTime()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT CONVERT(VARCHAR(100),GETDATE(),120) as d";
        return Convert.ToDateTime(dbOperHandler.GetDataTable().Rows[0]["d"]);
      }
    }
  }
}
