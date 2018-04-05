// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.news.newsinfo
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.IPhone.news
{
  public partial class newsinfo : UserCenterSession
  {
    public string L_Title;
    public string L_Month;
    public string L_Day;
    public string L_Time;
    public string L_Detail;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.Page.IsPostBack)
        return;
      string whereStr = " Id=" + this.Str2Str(this.q("id"));
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      string sql0 = SqlHelp.GetSql0("Substring(Convert(varchar(10),STime,120),6,2) as tmonth,Substring(Convert(varchar(10),STime,120),9,2) as tday,*", "Sys_News", "STime", 999, 1, "desc", whereStr);
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
