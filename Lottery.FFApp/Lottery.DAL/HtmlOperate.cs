// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.HtmlOperate
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using System.IO;
using System.Net;
using System.Text;

namespace Lottery.DAL
{
  public class HtmlOperate
  {
    public static string GetHtml(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }

    public static string GetHtmlGB2132(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }

    public static string GetHtmlGB2132_2(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding("GBK")).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }
  }
}
