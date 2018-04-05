// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.news.info
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp.news
{
  public partial class info : UserCenterSession
  {
    public string L_Title;
    public string L_Month;
    public string L_Day;
    public string L_Time;
    public string L_Detail;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.Page.IsPostBack)
        return;
      string whereStr = " Id=" + this.Str2Str(this.q("id"));
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      string sql0 = SqlHelp.GetSql0("Substring(Convert(varchar(10),STime,120),6,2) as tmonth,Substring(Convert(varchar(10),STime,120),9,2) as tday,*", "Sys_News", "STime", 1, 1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count > 0)
      {
        DataRow row = dataTable.Rows[0];
        this.L_Title = row["Title"].ToString();
        this.L_Month = row["tmonth"].ToString();
        this.L_Day = row["tday"].ToString();
        this.L_Time = row["STime"].ToString();
        this.L_Detail = row["Content"].ToString();
      }
      else
      {
        this.Response.Write("参数错误");
        this.Response.End();
      }
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
