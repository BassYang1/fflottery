// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Cookie
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Specialized;
using System.Web;

namespace Lottery.Utils
{
  public static class Cookie
  {
    public static void SetObj(string strCookieName, string strValue)
    {
      Cookie.SetObj(strCookieName, 1, strValue, "", "/");
    }

    public static void SetObj(string strCookieName, int iExpires, string strValue)
    {
      Cookie.SetObj(strCookieName, iExpires, strValue, "", "/");
    }

    public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains)
    {
      Cookie.SetObj(strCookieName, iExpires, strValue, strDomains, "/");
    }

    public static void SetObj(string strCookieName, int iExpires, string strValue, string strDomains, string strPath)
    {
      string str = Cookie.SelectDomain(strDomains);
      HttpCookie cookie = new HttpCookie(strCookieName.Trim());
      cookie.Value = HttpUtility.UrlEncode(strValue.Trim());
      if (str.Length > 0)
        cookie.Domain = str;
      if (iExpires > 0)
        cookie.Expires = iExpires != 1 ? DateTime.Now.AddSeconds((double) iExpires) : DateTime.MaxValue;
      HttpContext.Current.Response.Cookies.Add(cookie);
    }

    public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue)
    {
      Cookie.SetObj(strCookieName, iExpires, KeyValue, "", "/");
    }

    public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains)
    {
      Cookie.SetObj(strCookieName, iExpires, KeyValue, strDomains, "/");
    }

    public static void SetObj(string strCookieName, int iExpires, NameValueCollection KeyValue, string strDomains, string strPath)
    {
      string str = Cookie.SelectDomain(strDomains);
      HttpCookie cookie = new HttpCookie(strCookieName.Trim());
      foreach (string allKey in KeyValue.AllKeys)
        cookie[allKey] = HttpUtility.UrlEncode(KeyValue[allKey].Trim());
      if (str.Length > 0)
        cookie.Domain = str;
      cookie.Path = strPath.Trim();
      if (iExpires > 0)
        cookie.Expires = iExpires != 1 ? DateTime.Now.AddSeconds((double) iExpires) : DateTime.MaxValue;
      HttpContext.Current.Response.Cookies.Add(cookie);
    }

    public static string GetValue(string strCookieName)
    {
      if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        return (string) null;
      return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
    }

    public static string GetValue(string strCookieName, string strKeyName)
    {
      if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        return (string) null;
      if (!HttpContext.Current.Request.Cookies[strCookieName].Value.Contains(strKeyName + "="))
        return (string) null;
      return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName][strKeyName]);
    }

    public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires)
    {
      return Cookie.Edit(strCookieName, strKeyName, KeyValue, iExpires, "", "/");
    }

    public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strPath)
    {
      return Cookie.Edit(strCookieName, strKeyName, KeyValue, iExpires, "", strPath);
    }

    public static string Edit(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomains, string strPath)
    {
      if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        return (string) null;
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strCookieName];
      cookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
      if (iExpires > 0)
        cookie.Expires = iExpires != 1 ? DateTime.Now.AddSeconds((double) iExpires) : DateTime.MaxValue;
      HttpContext.Current.Response.Cookies.Add(cookie);
      return "success";
    }

    public static void Del(string strCookieName)
    {
      Cookie.Del(strCookieName, "", "/");
    }

    public static void Del(string strCookieName, string strDomains)
    {
      Cookie.Del(strCookieName, strDomains, "/");
    }

    public static void Del(string strCookieName, string strDomains, string strPath)
    {
      string str = Cookie.SelectDomain(strDomains);
      HttpCookie cookie = new HttpCookie(strCookieName.Trim());
      if (str.Length > 0)
        cookie.Domain = str;
      cookie.Path = strPath.Trim();
      cookie.Expires = DateTime.Now.AddYears(-1);
      HttpContext.Current.Response.Cookies.Add(cookie);
    }

    public static string DelKey(string strCookieName, string strKeyName, int iExpires)
    {
      if (HttpContext.Current.Request.Cookies[strCookieName] == null)
        return (string) null;
      HttpCookie cookie = HttpContext.Current.Request.Cookies[strCookieName];
      cookie.Values.Remove(strKeyName);
      if (iExpires > 0)
        cookie.Expires = iExpires != 1 ? DateTime.Now.AddSeconds((double) iExpires) : DateTime.MaxValue;
      HttpContext.Current.Response.Cookies.Add(cookie);
      return "success";
    }

    private static string SelectDomain(string strDomains)
    {
      bool flag = false;
      if (strDomains.Trim().Length == 0)
        return "";
      string str1 = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
      if (!str1.Contains("."))
        flag = true;
      string str2 = "www.abc.com";
      string[] strArray = strDomains.Split(';');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (str1.Contains(strArray[index].Trim()))
        {
          str2 = !flag ? strArray[index].Trim() : "";
          break;
        }
      }
      return str2;
    }
  }
}
