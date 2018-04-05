// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.adminPower
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
  public partial class adminPower : AdminCenter
  {
    private string id = "0";
    protected HtmlForm form1;
    protected Literal ltAdminSetting;
    protected Button btnSave;
    protected HiddenField hfAdminId;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "");
      this.id = this.Str2Str(this.q("id"));
      this.hfAdminId.Value = this.id;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.id;
      string str1 = this.doh.GetField("Sys_Role", "Setting").ToString();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<table cellspacing=\"0\" cellpadding=\"0\" class=\"formtable\">");
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=0 ORDER BY id";
      DataTable dataTable1 = this.doh.GetDataTable();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
      {
        string str2 = dataTable1.Rows[index1]["Id"].ToString();
        string str3 = dataTable1.Rows[index1]["Name"].ToString();
        stringBuilder.Append("<tr><th>" + str3 + "</th>");
        stringBuilder.Append("<td>");
        this.doh.Reset();
        this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=" + str2 + " ORDER BY id";
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          string str4 = dataTable2.Rows[index2]["Id"].ToString();
          string str5 = dataTable2.Rows[index2]["Name"].ToString();
          stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"" + str4 + "\"");
          if (str1.Contains("," + str4 + ","))
            stringBuilder.Append(" checked");
          stringBuilder.Append("> <span style='margin-right:10px;'>" + str5 + "</span>");
        }
        stringBuilder.Append("</td></tr>");
      }
      stringBuilder.Append("<tr><th>通知区域</th>");
      stringBuilder.Append("<td>");
      stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99001\"");
      if (str1.Contains(",99001,"))
        stringBuilder.Append(" checked");
      stringBuilder.Append("> <span style='margin-right:10px;'>提现提示</span>");
      stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99002\"");
      if (str1.Contains(",99001,"))
        stringBuilder.Append(" checked");
      stringBuilder.Append("> <span style='margin-right:10px;'>警告提示</span>");
      stringBuilder.Append("<input type=checkbox class='checkbox' name=\"admin_power\" value=\"99003\"");
      if (str1.Contains(",99003,"))
        stringBuilder.Append(" checked");
      stringBuilder.Append("> <span style='margin-right:10px;'>活动提示</span>");
      stringBuilder.Append("</td></tr>");
      stringBuilder.Append("</table>");
      this.ltAdminSetting.Text = stringBuilder.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string str = ",";
      if (this.Request.Form["admin_power"] != null)
        str = "," + this.Request.Form["admin_power"].ToString() + ",";
      this.id = this.hfAdminId.Value.ToString();
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.id;
      this.doh.AddFieldItem("Setting", (object) str);
      this.doh.Update("Sys_Role");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + this.id + "的角色权限");
      this.FinalMessage("正确保存!", "close.htm", 0);
    }
  }
}
