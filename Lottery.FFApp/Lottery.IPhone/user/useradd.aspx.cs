// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.user.useradd
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.IPhone.user
{
  public partial class useradd : UserCenterSession
  {
    protected HtmlForm form1;
    protected DropDownList ddlPoint;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getEditDropDownList(ref this.ddlPoint, new Decimal(0));
    }
  }
}
