// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userUpdatePoints
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class userUpdatePoints : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtId;
    protected DropDownList ddlPoint;
    protected DropDownList ddlGroup;
    protected Button Button1;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "html");
      this.txtId.Text = this.q("id");
      this.getEditDropDownList(ref this.ddlPoint, 0);
      this.getGroupDropDownList(ref this.ddlGroup, 0);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      ListItem selectedItem1 = this.ddlGroup.SelectedItem;
      ListItem selectedItem2 = this.ddlPoint.SelectedItem;
      string[] strArray = this.txtId.Text.Split(',');
      if (strArray.Length > 0)
      {
        for (int index = 0; index < strArray.Length; ++index)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=" + strArray[index];
          this.doh.AddFieldItem("Point", (object) Convert.ToDecimal(selectedItem2.Value));
          this.doh.AddFieldItem("UserGroup", (object) selectedItem1.Value);
          this.doh.Update("N_User");
          new LogAdminOperDAL().SaveLog(this.AdminId, strArray[index], "会员返点", "修改了" + strArray[index] + "的返点，类型信息");
        }
      }
      this.FinalMessage("成功保存", "close.htm", 0);
    }
  }
}
