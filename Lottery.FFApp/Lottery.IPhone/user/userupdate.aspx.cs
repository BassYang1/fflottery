// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.user.userupdate
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.user
{
  public partial class userupdate : UserCenterSession
  {
    private Decimal pointBefore = new Decimal(0);
    protected HtmlForm form1;
    protected TextBox txtUserName;
    protected TextBox txtUserId;
    protected DropDownList ddlPoint;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.IsPostBack)
        return;
      string str = "1";
      if (this.Request.QueryString["id"] != null)
        str = this.Request.QueryString["id"].ToString();
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      object[] fields = this.doh.GetFields("N_User", "UserName,Point");
      this.txtUserId.Text = str;
      this.txtUserName.Text = fields[0].ToString();
      this.getEditDropDownList(ref this.ddlPoint, Convert.ToDecimal(string.Concat(fields[1])), 0);
      this.ddlPoint.SelectedValue = string.Concat(fields[1]);
      this.pointBefore = Convert.ToDecimal(string.Concat(fields[1]));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 1 ParentId FROM [N_User] where Id=" + this.txtUserId.Text;
      DataTable dataTable1 = this.doh.GetDataTable();
      if (dataTable1.Rows.Count > 0 && !this.AdminId.Equals(dataTable1.Rows[0]["ParentId"].ToString()))
      {
        this.FinalMessage("不是您的直属下级不能修改其返点！", "/statics/html/close.htm", 0);
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=@id";
        this.doh.AddConditionParameter("@id", (object) dataTable1.Rows[0]["ParentId"].ToString());
        object field = this.doh.GetField("N_User", "Point");
        ListItem selectedItem = this.ddlPoint.SelectedItem;
        if (this.pointBefore >= Convert.ToDecimal(selectedItem.Value) || Convert.ToDecimal(selectedItem.Value) > Convert.ToDecimal(field))
        {
          this.FinalMessage("下属返点只能升不能降而且不能大于您的返点！", "/statics/html/close.htm", 0);
        }
        else
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "id=" + this.txtUserId.Text;
          this.doh.AddFieldItem("Point", (object) Convert.ToDecimal(selectedItem.Value));
          this.doh.Update("N_User");
          this.doh.Reset();
          this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + (object) Convert.ToDecimal(selectedItem.Value) + " order by [Point] desc";
          DataTable dataTable2 = this.doh.GetDataTable();
          for (int index = 0; index < dataTable2.Rows.Count; ++index)
          {
            if (!new UserQuotaDAL().Exists("UserId=" + this.txtUserId.Text + " and UserLevel=" + (object) (Convert.ToDecimal(dataTable2.Rows[index]["Point"]) / new Decimal(10))))
              new UserQuotaDAL().SaveUserQuota(this.txtUserId.Text, Convert.ToDecimal(dataTable2.Rows[index]["Point"]) / new Decimal(10), 0);
          }
          this.FinalMessage("返点修改成功", "/statics/html/close.htm", 0);
        }
      }
    }
  }
}
