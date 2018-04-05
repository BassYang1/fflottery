// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.smsHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lottery.Utils
{
  public static class smsHelp
  {
      public static string ApiUid = XmlCOM.ReadConfig("~/statics/config/sms", "ApiUid");
      public static string ApiKey = XmlCOM.ReadConfig("~/statics/config/sms", "ApiKey");

    public static string SendSMS(string smsMob, string smsText)
    {
      string htmlFromUrl = smsHelp.GetHtmlFromUrl("http://utf8.sms.webchinese.cn/?Uid=" + smsHelp.ApiUid + "&Key=" + smsHelp.ApiKey + "&smsMob=" + smsMob + "&smsText=" + smsText);
      switch (htmlFromUrl)
      {
        case "-1":
          return "没有该用户账号";
        case "-2":
          return "密钥不正确";
        case "-3":
          return "短信数量不足";
        case "-11":
          return "该用户被禁用";
        case "-14":
          return "短信内容出现非法字符";
        case "-41":
          return "手机号码为空";
        case "-42":
          return "短信内容为空";
        default:
          return "成功发送短信" + htmlFromUrl + "条";
      }
    }

    private static string GetHtmlFromUrl(string url)
    {
      string str = (string) null;
      if (url == null || url.Trim().ToString() == "")
        return str;
      string requestUriString = url.Trim().ToString();
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
        httpWebRequest.Timeout = 19600;
        httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
        httpWebRequest.Method = "GET";
        return new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }
    }
  }
}
