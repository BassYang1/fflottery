// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.plus.auto
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Lottery.WebApp.plus
{
  public partial class auto : Page
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      new UserDAL().ChkAutoLoginWebApp(this.Request.QueryString["Id"].Trim(), this.Request.QueryString["SessionId"].Trim(), 604800);
      this.Response.Redirect("http://localhost:50092/Index.aspx");
    }
  }
}
