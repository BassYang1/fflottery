// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.lotteryNoPlay
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class lotteryNoPlay : AdminCenter
  {
    protected HtmlForm form1;
    protected Button btnSave;
    protected HiddenField hfLotteryId;
    protected Literal ltAdminSetting;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "");
      string lotId = this.Str2Str(this.q("id"));
      this.hfLotteryId.Value = lotId;
      if (this.IsPostBack)
        return;
      this.BindInfo(lotId);
    }

    public void BindInfo(string lotId)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = " LotteryId=" + lotId;
      string str1 = this.doh.GetField("Sys_LotteryPlaySetting", "Setting").ToString();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<table cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\">");
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],case [TypeId] when 1 then '时时彩' when 2 then '11选5'  when 3 then '低频彩'  when 4 then 'PK10' end LotName,[Title] FROM Sys_PlayBigType where TypeId=" + lotId.ToString().Substring(0, 1) + " ORDER BY id";
      DataTable dataTable1 = this.doh.GetDataTable();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
      {
        string str2 = dataTable1.Rows[index1]["Id"].ToString();
        string str3 = dataTable1.Rows[index1]["Title"].ToString();
        stringBuilder.Append("<tr><th>" + str3 + "</th>");
        stringBuilder.Append("<td>");
        this.doh.Reset();
        this.doh.SqlCmd = "SELECT [Id],lotteryId,Radio,[Title0],[Title]  FROM [Sys_PlaySmallType]  where lotteryId<5 and type is not null and Radio=" + str2 + " ORDER BY id";
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          string str4 = dataTable2.Rows[index2]["Id"].ToString();
          string str5 = dataTable2.Rows[index2]["Title"].ToString();
          stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"" + str4 + "\"");
          if (str1.Contains("," + str4 + ","))
            stringBuilder.Append(" checked");
          stringBuilder.Append("> <span style='margin-right:10px;'>" + str5 + "</span>");
        }
        stringBuilder.Append("</td></tr>");
      }
      stringBuilder.Append("</table>");
      this.ltAdminSetting.Text = stringBuilder.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string str1 = ",";
      if (this.Request.Form["admin_power"] != null)
        str1 = "," + this.Request.Form["admin_power"].ToString() + ",";
      string str2 = this.hfLotteryId.Value.ToString();
      this.doh.Reset();
      this.doh.ConditionExpress = "LotteryId=" + str2;
      if (this.doh.Count("Sys_LotteryPlaySetting") > 0)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "LotteryId=" + str2;
        this.doh.AddFieldItem("Setting", (object) str1);
        this.doh.Update("Sys_LotteryPlaySetting");
      }
      else
      {
        this.doh.Reset();
        this.doh.AddFieldItem("LotteryId", (object) str2);
        this.doh.AddFieldItem("Setting", (object) str1);
        this.doh.AddFieldItem("IsUsed", (object) 0);
        this.doh.Insert("Sys_LotteryPlaySetting");
      }
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "添加了彩种玩法限制");
      this.FinalMessage("正确保存!", "close.htm", 0);
    }
  }
}
