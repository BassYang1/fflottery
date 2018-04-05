// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Session
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Web;

namespace Lottery.Utils
{
  public static class Session
  {
    public static void Add(string strSessionName, string strValue)
    {
      HttpContext.Current.Session[strSessionName] = (object) strValue;
      HttpContext.Current.Session.Timeout = 20;
    }

    public static void Adds(string strSessionName, string[] strValues)
    {
      HttpContext.Current.Session[strSessionName] = (object) strValues;
      HttpContext.Current.Session.Timeout = 20;
    }

    public static void Add(string strSessionName, string strValue, int iExpires)
    {
      HttpContext.Current.Session[strSessionName] = (object) strValue;
      HttpContext.Current.Session.Timeout = iExpires;
    }

    public static void Adds(string strSessionName, string[] strValues, int iExpires)
    {
      HttpContext.Current.Session[strSessionName] = (object) strValues;
      HttpContext.Current.Session.Timeout = iExpires;
    }

    public static string Get(string strSessionName)
    {
      if (HttpContext.Current.Session[strSessionName] == null)
        return (string) null;
      return HttpContext.Current.Session[strSessionName].ToString();
    }

    public static string[] Gets(string strSessionName)
    {
      if (HttpContext.Current.Session[strSessionName] == null)
        return (string[]) null;
      return (string[]) HttpContext.Current.Session[strSessionName];
    }

    public static void Del(string strSessionName)
    {
      HttpContext.Current.Session[strSessionName] = (object) null;
    }
  }
}
