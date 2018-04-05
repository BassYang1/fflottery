// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.SysActiveRecordDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;

namespace Lottery.DAL
{
  public class SysActiveRecordDAL : ComData
  {
    public bool Exists(string _wherestr)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr;
        if (dbOperHandler.Exist("Act_ActiveRecord"))
          num = 1;
      }
      return num == 1;
    }

    public void SaveLog(string _adminid, string _type, string _name, Decimal _money, string _remark)
    {
      string clientIp = IPHelp.ClientIP;
      string mnum = MachineCode.getMNum();
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("UserId", (object) _adminid);
        dbOperHandler.AddFieldItem("ActiveType", (object) _type);
        dbOperHandler.AddFieldItem("ActiveName", (object) _name);
        dbOperHandler.AddFieldItem("InMoney", (object) _money);
        dbOperHandler.AddFieldItem("Remark", (object) _remark);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) mnum);
        dbOperHandler.Insert("Act_ActiveRecord");
      }
    }

    public void DeleteLogs()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("Act_ActiveRecord");
      }
    }
  }
}
