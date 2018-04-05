// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserRegLinkDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;

namespace Lottery.DAL
{
  public class UserRegLinkDAL : ComData
  {
    public void SaveUserRegLink(string UserId, Decimal Point, string Url)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "UserId=" + UserId + " and Point=" + (object) Point;
        if (dbOperHandler.Exist("N_UserRegLink"))
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "UserId=" + UserId + " and Point=" + (object) Point;
          dbOperHandler.AddFieldItem("UserId", (object) UserId);
          dbOperHandler.AddFieldItem("Point", (object) Point);
          dbOperHandler.AddFieldItem("Url", (object) Url);
          dbOperHandler.Update("N_UserRegLink");
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.AddFieldItem("UserId", (object) UserId);
          dbOperHandler.AddFieldItem("Point", (object) Point);
          dbOperHandler.AddFieldItem("Url", (object) Url);
          dbOperHandler.Insert("N_UserRegLink");
        }
      }
    }

    public void SaveUserRegLink(string UserId, Decimal Point, string YxTime, string Times, string Url)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "UserId=" + UserId + " and Point=" + (object) Point;
        if (dbOperHandler.Exist("N_UserRegLink"))
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "UserId=" + UserId + " and Point=" + (object) Point;
          dbOperHandler.AddFieldItem("UserId", (object) UserId);
          dbOperHandler.AddFieldItem("Point", (object) Point);
          dbOperHandler.AddFieldItem("YxTime", (object) YxTime);
          dbOperHandler.AddFieldItem("Times", (object) Times);
          dbOperHandler.AddFieldItem("Url", (object) Url);
          dbOperHandler.Update("N_UserRegLink");
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.AddFieldItem("UserId", (object) UserId);
          dbOperHandler.AddFieldItem("Point", (object) Point);
          dbOperHandler.AddFieldItem("YxTime", (object) YxTime);
          dbOperHandler.AddFieldItem("Times", (object) Times);
          dbOperHandler.AddFieldItem("Url", (object) Url);
          dbOperHandler.Insert("N_UserRegLink");
        }
      }
    }
  }
}
