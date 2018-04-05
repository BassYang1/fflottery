// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userNoPlay
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
  public partial class userNoPlay : AdminCenter
  {
    private string id = "0";
    protected HtmlForm form1;
    protected DropDownList ddlLot;
    protected Button btnSave;
    protected HiddenField hfAdminId;
    protected HiddenField hfLotteryId;
    protected Literal ltAdminSetting;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "");
      this.getLotteryDropDownList(ref this.ddlLot, 0);
      this.id = this.Str2Str(this.q("id"));
      this.hfAdminId.Value = this.id;
      if (this.IsPostBack)
        return;
      this.BindInfo("1001");
      this.hfLotteryId.Value = "1001";
    }

    public void BindInfo(string lotId)
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "UserId=" + this.id + " and LotteryId=" + lotId;
      string str1 = this.doh.GetField("N_UserPlaySetting", "Setting").ToString();
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
      this.id = this.hfAdminId.Value.ToString();
      string str2 = this.hfLotteryId.Value.ToString();
      this.doh.Reset();
      this.doh.ConditionExpress = "UserId=" + this.id + " and LotteryId=" + str2;
      if (this.doh.Count("N_UserPlaySetting") > 0)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "UserId=" + this.id + " and LotteryId=" + str2;
        this.doh.AddFieldItem("Setting", (object) str1);
        this.doh.Update("N_UserPlaySetting");
      }
      else
      {
        this.doh.Reset();
        this.doh.AddFieldItem("UserId", (object) this.id);
        this.doh.AddFieldItem("LotteryId", (object) str2);
        this.doh.AddFieldItem("Setting", (object) str1);
        this.doh.AddFieldItem("IsUsed", (object) 0);
        this.doh.Insert("N_UserPlaySetting");
      }
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "添加了玩法限制");
      this.FinalMessage("正确保存!", "close.htm", 0);
    }

    protected void ddlLot_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.hfLotteryId.Value = this.ddlLot.SelectedValue;
      this.BindInfo(this.ddlLot.SelectedValue);
    }
  }
}
