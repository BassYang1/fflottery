// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebLot
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
    public partial class WebLot : UserCenterSession
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by sort asc";
            DataTable dataTable1 = this.doh.GetDataTable();
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where Id=" + dataTable1.Rows[index]["Id"];
                this.loStr = "var lotJson={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(this.doh.GetDataTable()) + "}";
                this.SaveJsFile(this.loStr, HttpContext.Current.Server.MapPath("~/statics/json/" + dataTable1.Rows[index]["Id"] + ".js"));
            }
            this.SaveJsFile("var lotteryJsonData={\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable1) + "}", HttpContext.Current.Server.MapPath("~/statics/json/Lottery.js"));
            dataTable1.Clear();
            dataTable1.Dispose();
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,Title,Ltype FROM Sys_Lottery where IsOpen=0 ORDER BY Sort asc";
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
