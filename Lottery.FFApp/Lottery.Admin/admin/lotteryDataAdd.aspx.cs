// Decompiled with JetBrains decompiler
// Type: Lottery.AdminFile.Admin.lotteryDataAdd
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.AdminFile.Admin
{
  public partial class lotteryDataAdd : AdminCenter
  {
    protected HtmlForm form1;
    protected DropDownList ddlType;
    protected TextBox txtTitle;
    protected TextBox txtNumber;
    protected TextBox txtOpenTime;
    protected Button btnSave;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getLotteryDropDownList(ref this.ddlType, 0);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(this.ddlType.SelectedValue);
      string Number = this.txtNumber.Text;
      string NumberAll = Number.Replace("+", ",").Replace(" ", ",");
      if (int32 == 1010 || int32 == 1011 || (int32 == 1012 || int32 == 1013) || (int32 == 1014 || int32 == 1015 || int32 == 1016) || int32 == 1017)
      {
        Number = Number.Replace("+", ",").Replace(" ", ",");
        string[] strArray = Number.Split(',');
        if (strArray.Length >= 20)
          Number = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
        else
          this.FinalMessage("开奖号码不正确！", "/admin/close.htm", 0);
      }
      if (Number.Split(',').Length > 10)
      {
        this.FinalMessage("开奖号码不正确！", "/admin/close.htm", 0);
      }
      else
      {
        if (new LotteryDataDAL().Add(int32, this.txtTitle.Text.Trim(), Number, this.txtOpenTime.Text, NumberAll))
          LotteryCheck.RunOper(Convert.ToInt32(this.ddlType.SelectedValue), this.txtTitle.Text.Trim());
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "添加了" + this.txtTitle.Text + "开奖号码");
        this.FinalMessage("操作成功", "/admin/close.htm", 0);
      }
    }
  }
}
