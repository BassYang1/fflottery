// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebSite
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Lottery.Web.temp
{
  public partial class WebSite : UserCenterSession
  {
    protected HtmlForm form1;

    protected void Page_Load(object sender, EventArgs e)
    {
      new SiteDAL().CreateSiteConfig();
      new SiteDAL().CreateSiteFiles();
      HttpContext.Current.Application["Lottery"] = (object) null;
      this.Response.Write("更新完成：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }
  }
}
