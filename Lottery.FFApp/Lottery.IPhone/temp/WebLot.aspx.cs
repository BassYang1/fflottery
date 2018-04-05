// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebLot
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Lottery.Web.temp
{
  public partial class WebLot : UserCenterSession
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by Id asc";
      DataTable dataTable1 = this.doh.GetDataTable();
      this.loStr = "var lotteryJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable1) + "}";
      dataTable1.Clear();
      dataTable1.Dispose();
      this.SaveJsFile(this.loStr, HttpContext.Current.Server.MapPath("~/statics/json/Lottery_Json.js"));
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id,Title,Ltype FROM Sys_Lottery where IsOpen=0 ORDER BY Id asc";
      DataTable dataTable2 = this.doh.GetDataTable();
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id,LotteryId,Title FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
      DataTable dataTable3 = this.doh.GetDataTable();
      string TxtStr = "var PlayData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON2(dataTable2, dataTable3) + "}";
      dataTable2.Clear();
      dataTable2.Dispose();
      dataTable3.Clear();
      dataTable3.Dispose();
      this.SaveJsFile(TxtStr, HttpContext.Current.Server.MapPath("~/statics/json/LotAndSmalldata.js"));
      this.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
  }
}
