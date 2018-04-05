// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LogSysDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;

namespace Lottery.DAL
{
  public class LogSysDAL : ComData
  {
    public void Save(string _title, string _info)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("Title", (object) _title);
        dbOperHandler.AddFieldItem("Content", (object) _info);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString());
        dbOperHandler.Insert("Log_Sys");
      }
    }

    public void DeleteLogs()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("Log_Sys");
      }
    }

    public void DeleteUserLogs()
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
