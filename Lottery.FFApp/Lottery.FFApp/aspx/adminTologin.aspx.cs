// Decompiled with JetBrains decompiler
// Type: Lottery.FFApp.aspx.adminTologin
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Lottery.FFApp.aspx
{
  public partial class adminTologin : Page
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (this.IsPostBack)
        return;
      new UserDAL().ChkAutoLoginWebApp(this.Request.QueryString["id"].Trim(), this.Request.QueryString["cookiess"]);
      this.Response.Redirect("/");
    }
  }
}
