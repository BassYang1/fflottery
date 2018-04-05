// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.SysDelDataDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;

namespace Lottery.DAL
{
  public class SysDelDataDAL : ComData
  {
    public void DeleteUserBet(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime2<='" + d2 + "' and Stime2>='" + d1 + "'";
        dbOperHandler.Delete("N_UserBet");
      }
    }

    public void DeleteUserGetCash(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("N_UserGetCash");
      }
    }

    public void DeleteUserMoneyLog(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("N_UserMoneyLog");
      }
    }

    public void DeleteUserMoneyStat(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("N_UserMoneyStatAll");
      }
    }

    public void DeleteLotteryData(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("Sys_LotteryData");
      }
    }

    public void DeleteUserLogs(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "LoginTime<='" + d2 + "' and LoginTime>='" + d1 + "'";
        dbOperHandler.Delete("Log_UserLogin");
      }
    }

    public void DeleteLogs(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("Log_Sys");
      }
    }

    public void DeleteUserBetZh(string d1, string d2)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Stime<='" + d2 + "' and Stime>='" + d1 + "'";
        dbOperHandler.Delete("N_UserBetZh");
      }
    }

    public void ClearUser()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "datediff(minute,ontime ,getdate())>5";
        if (!dbOperHandler.Exist("N_User"))
          return;
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "datediff(minute,ontime ,getdate())>5";
        dbOperHandler.AddFieldItem("IsOnline", (object) "0");
        dbOperHandler.AddFieldItem("SessionId", (object) Guid.NewGuid().ToString().Replace("-", ""));
        dbOperHandler.Update("N_User");
      }
    }
  }
}
