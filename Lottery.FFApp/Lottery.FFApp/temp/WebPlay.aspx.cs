// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebPlay
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Lottery.Web.temp
{
  public partial class WebPlay : UserCenterSession
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
      DataTable dataTable1 = this.doh.GetDataTable();
      string TxtStr1 = "var PlayData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable1) + "}";
      dataTable1.Clear();
      dataTable1.Dispose();
      this.SaveJsFile(TxtStr1, HttpContext.Current.Server.MapPath("~/statics/json/PlayBigdate.js"));
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
      DataTable dataTable2 = this.doh.GetDataTable();
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
      DataTable dataTable3 = this.doh.GetDataTable();
      string TxtStr2 = "var lotteryData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable2, dataTable3) + "}";
      dataTable2.Clear();
      dataTable2.Dispose();
      dataTable3.Clear();
      dataTable3.Dispose();
      this.SaveJsFile(TxtStr2, HttpContext.Current.Server.MapPath("~/statics/json/BigAndSmalldata.js"));
      this.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
  }
}
