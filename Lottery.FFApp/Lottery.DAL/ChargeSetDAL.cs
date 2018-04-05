// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.ChargeSetDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System.Data;

namespace Lottery.DAL
{
  public class ChargeSetDAL
  {
    public DataTable getChargeSetDataTable(string Id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id=" + Id;
        return dbOperHandler.GetDataTable();
      }
    }
  }
}
