// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxLotterydata
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp
{
  public partial class ajaxLotterydata : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this._response = this.JsonResult(0, "未知操作");
    }

    private void ajaxGetList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("u");
      this.Str2Int(this.q("gId"), 0);
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      int mode = this.Str2Int(this.q("flag"), 0);
      string whereStr = "[Type]=" + (object) mode;
      if (!string.IsNullOrEmpty(str1))
        whereStr = whereStr + "and Convert(varchar(10),STime,120)='" + str1 + "'";
      if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + "and Title like '" + str2 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_LotteryData");
      string sql0 = SqlHelp.GetSql0("Id,Title,Number,NumberAll,Total,OpenTime,STime", "Sys_LotteryData", "STime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "jsonpCallback({\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(mode, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "})";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
