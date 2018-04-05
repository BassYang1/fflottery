// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.AgentFHDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;

namespace Lottery.DAL
{
  public class AgentFHDAL : ComData
  {
    public bool Exists(string _wherestr)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr;
        if (dbOperHandler.Exist("Act_AgentFHRecord"))
          num = 1;
      }
      return num == 1;
    }

    public void Save(string Adminid, string AgentId, string StartTime, string EndTime, Decimal Bet, Decimal Total, Decimal Per, Decimal InMoney, string Remark)
    {
      using (new ComData().Doh())
        ;
    }

    public void Delete()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("Act_AgentFHRecord");
      }
    }

    public void UpdateStime()
    {
      using (new ComData().Doh())
        ;
    }
  }
}
