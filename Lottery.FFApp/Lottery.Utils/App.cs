// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.App
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Web;

namespace Lottery.Utils
{
  public static class App
  {
    public static string Url
    {
      get
      {
        if (HttpContext.Current.Request.Url.Port == 80)
          return "http://" + HttpContext.Current.Request.Url.Host;
        return "http://" + HttpContext.Current.Request.Url.Host + ":" + (object) HttpContext.Current.Request.Url.Port;
      }
    }

    public static string Path
    {
      get
      {
        string applicationPath = HttpContext.Current.Request.ApplicationPath;
        if (applicationPath != "/")
          applicationPath += "/";
        return applicationPath;
      }
    }
  }
}
