// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.center.bankInfo
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.WebApp.center
{
  public partial class bankInfo : UserCenterSession
  {
    protected HtmlForm form1;
    protected DropDownList ddlBank;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      this.getBankDropDownList(ref this.ddlBank, 0);
    }
  }
}
