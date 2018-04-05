// Decompiled with JetBrains decompiler
// Type: Lottery.Web.temp.WebSite
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

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
