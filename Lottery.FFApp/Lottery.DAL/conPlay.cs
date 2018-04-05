// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.conPlay
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;
using System.Web;

namespace Lottery.DAL
{
  public class conPlay : ComData
  {
    public void Create()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=0";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          DataRow row = dataTable.Rows[index];
          string str = "ssc";
          if (Convert.ToInt32(row["LotteryId"]) == 1)
            str = "ssc";
          if (Convert.ToInt32(row["LotteryId"]) == 2)
            str = "11x5";
          if (Convert.ToInt32(row["LotteryId"]) == 3)
            str = "dpc";
          if (Convert.ToInt32(row["LotteryId"]) == 4)
            str = "pk10";
          XmlControl xmlControl = new XmlControl(HttpContext.Current.Server.MapPath("~/WEB-INF/" + str + "/" + row["Title2"] + ".xml"));
          xmlControl.Update("play/id", row["Id"].ToString());
          xmlControl.Update("play/type", row["type"].ToString());
          xmlControl.Update("play/typename", row["title0"].ToString());
          xmlControl.Update("play/name", row["Title"].ToString());
          xmlControl.Update("play/minbonus", row["Minbonus"].ToString());
          xmlControl.Update("play/posbonus", row["Posbonus"].ToString());
          xmlControl.Update("play/maxbonus", row["Maxbonus"].ToString());
          xmlControl.Update("play/remark", row["remark"].ToString());
          xmlControl.Update("play/example", row["example"].ToString());
          xmlControl.Update("play/help", row["help"].ToString());
          xmlControl.Save();
          xmlControl.Dispose();
        }
      }
    }
  }
}
