// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LogAdminOperDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Web;

namespace Lottery.DAL
{
  public class LogAdminOperDAL : ComData
  {
    public void SaveLog(string adminid, string userid, string title, string info)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        IPScaner ipScaner = new IPScaner();
        ipScaner.DataPath = HttpContext.Current.Server.MapPath("/statics/database/QQWry.Dat");
        ipScaner.IP = IPHelp.ClientIP;
        string str = ipScaner.IPLocation() + ipScaner.ErrMsg;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("AdminId", (object) adminid);
        dbOperHandler.AddFieldItem("UserId", (object) userid);
        dbOperHandler.AddFieldItem("OperTitle", (object) title);
        dbOperHandler.AddFieldItem("OperInfo", (object) info);
        dbOperHandler.AddFieldItem("OperTime", (object) DateTime.Now.ToString());
        dbOperHandler.AddFieldItem("OperIP", (object) IPHelp.ClientIP);
        dbOperHandler.Insert("Log_AdminOper");
      }
    }

    public void DeleteLogs()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("Log_AdminOper");
      }
    }
  }
}
