// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserQuotaDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using System;

namespace Lottery.DAL
{
  public class UserQuotaDAL : ComData
  {
    protected SiteModel site;

    public UserQuotaDAL()
    {
      this.site = new conSite().GetSite();
    }

    public bool Exists(string _wherestr)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr;
        if (dbOperHandler.Exist("N_UserQuota"))
          num = 1;
      }
      return num == 1;
    }

    public void SaveUserQuota(string UserId, Decimal UserLevel, int ChildNums)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("UserLevel", (object) UserLevel);
        dbOperHandler.AddFieldItem("ChildNums", (object) ChildNums);
        dbOperHandler.Insert("N_UserQuota");
      }
    }
  }
}
