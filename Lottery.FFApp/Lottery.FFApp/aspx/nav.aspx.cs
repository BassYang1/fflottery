// Decompiled with JetBrains decompiler
// Type: Lottery.Web.nav
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;

namespace Lottery.Web
{
  public partial class nav : UserCenterSession
  {
    public string tId = "1";
    public string loId = "1001";

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (!string.IsNullOrEmpty(this.Request.QueryString["id"] ?? ""))
        this.loId = this.Request.QueryString["id"];
      if (string.IsNullOrEmpty(this.Request.QueryString["tid"] ?? ""))
        return;
      this.tId = this.Request.QueryString["tid"];
    }
  }
}
