// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.HttpHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lottery.Utils
{
  public static class HttpHelp
  {
    public static string Get_Http(string url, int timeout, Encoding EnCodeType)
    {
      string empty = string.Empty;
      if (url.Length < 10)
        return "$UrlIsFalse$";
      string str;
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Timeout = timeout;
        httpWebRequest.Method = "Get";
        Stream responseStream = httpWebRequest.GetResponse().GetResponseStream();
        StreamReader streamReader = new StreamReader(responseStream, EnCodeType);
        str = streamReader.ReadToEnd();
        responseStream.Close();
        streamReader.Close();
      }
      catch (Exception ex)
      {
        str = "$GetFalse$";
      }
      return str;
    }

    public static string Post_Http(string url, string postData, string encodeType)
    {
      try
      {
        byte[] bytes = Encoding.GetEncoding(encodeType).GetBytes(postData);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Timeout = 19600;
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        httpWebRequest.ContentLength = (long) bytes.Length;
        Stream requestStream = httpWebRequest.GetRequestStream();
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
        return new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
}
