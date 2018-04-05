// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.useradd
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public class useradd : AdminCenter
  {
    protected HtmlForm form1;
    protected DropDownList ddlGroup;
    protected DropDownList ddlPoint;
    protected TextBox txtAdminName;
    protected TextBox txtId;
    protected TextBox txtAdminPass1;
    protected TextBox txtAdminPass2;
    protected TextBox txtAdminQuota;
    protected TextBox txtAdminQuota2;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getEditDropDownList(ref this.ddlPoint, 0);
      this.getGroupDropDownList(ref this.ddlGroup, 0);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      string s = this.txtAdminPass1.Text;
      if (s == "")
        s = "123456";
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id FROM [N_User] WHERE [UserName]='" + this.txtAdminName.Text + "'";
      if (this.doh.GetDataTable().Rows.Count > 0)
        this.FinalMessage("用户名重复", "", 1);
      ListItem selectedItem = this.ddlPoint.SelectedItem;
      int num = new UserDAL().Register("0", this.txtAdminName.Text, MD5.Lower32(s), Convert.ToDecimal(selectedItem.Value));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + (object) num;
      this.doh.AddFieldItem("UserGroup", (object) this.ddlGroup.SelectedValue);
      this.doh.AddFieldItem("UserCode", (object) Strings.PadLeft(num.ToString()));
      if (this.doh.Update("N_User") > 0)
      {
        new LogAdminOperDAL().SaveLog(this.AdminId, string.Concat((object) num), "会员管理", "添加了会员" + this.txtAdminName.Text);
        if (this.ddlGroup.SelectedValue == "0")
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + (object) num + " and IsEnable=0 and IsDel=0";
          DataTable dataTable1 = this.doh.GetDataTable();
          for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
          {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + (object) Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
            DataTable dataTable2 = this.doh.GetDataTable();
            for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
            {
              if (Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) == Convert.ToDecimal(dataTable1.Rows[index1]["Point"]))
                new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Convert.ToInt32(this.txtAdminQuota2.Text));
              else
                new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Convert.ToInt32(this.txtAdminQuota.Text));
            }
            new LogAdminOperDAL().SaveLog(this.AdminId, "0", "会员管理", "自动生成了Id为" + dataTable1.Rows[index1]["Id"] + "的会员的配额");
          }
        }
      }
      this.FinalMessage("操作成功", "/admin/close.htm", 0);
    }
  }
}
