// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.urlHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Web;

namespace Lottery.Utils
{
  public static class urlHelp
  {
    public static string GetUrlPrefix
    {
      get
      {
        HttpRequest request = HttpContext.Current.Request;
        string serverVariable = HttpContext.Current.Request.ServerVariables["Url"];
        if (HttpContext.Current.Request.QueryString.Count == 0 || HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))
          return serverVariable + "?page=";
        string[] strArray = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[1]
        {
          "page="
        }, StringSplitOptions.None);
        if (strArray.Length == 1)
          return serverVariable + "?" + strArray[0] + "&page=";
        return serverVariable + "?" + strArray[0] + "page=";
      }
    }
  }
}
