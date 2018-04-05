// Decompiled with JetBrains decompiler
// Type: Lottery.Web.money.charge
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Web.money
{
  public partial class charge : UserCenterSession
  {
    protected HtmlForm form1;
    protected DropDownList ddlLine;
    protected DropDownList ddlBank;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getZXChargeSetDropDownList(ref this.ddlLine, 0);
      this.getZXBankDropDownList(ref this.ddlBank, 0);
    }
  }
}
