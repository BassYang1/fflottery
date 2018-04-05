// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.IPHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.Utils
{
  public class IPHelp
  {
    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(int dest, int host, ref long mac, ref int length);

    [DllImport("Ws2_32.dll")]
    private static extern int inet_addr(string ip);

    public static long IP2Long(IPAddress ip)
    {
      int num1 = 3;
      long num2 = 0;
      foreach (byte addressByte in ip.GetAddressBytes())
        num2 += (long) addressByte << 8 * num1--;
      return num2;
    }

    public static IPAddress Long2IP(long l)
    {
      byte[] address = new byte[4];
      for (int index = 0; index < 4; ++index)
        address[3 - index] = (byte) ((ulong) (l >> 8 * index) & (ulong) byte.MaxValue);
      return new IPAddress(address);
    }

    public static string ClientIP
    {
        get
        {
            bool flag = false;
            string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString() : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            
            if (str.Length > 15)
            {
                flag = true;
            }
            else
            {
                string[] strArray = str.Split('.');
                if (strArray.Length == 4)
                {
                    for (int index = 0; index < strArray.Length; ++index)
                    {
                        if (strArray[index].Length > 3)
                            flag = true;
                    }
                }
                else
                    flag = true;
            }

            if (flag)
                return "1.1.1.1";

            return str;
        }
    }

    public static string GetMac()
    {
      try
      {
        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
        string ip = HttpContext.Current.Request.UserHostAddress.ToString().Trim();
        int dest = IPHelp.inet_addr(ip);
        IPHelp.inet_addr(ip);
        long mac = 0;
        int length = 6;
        IPHelp.SendARP(dest, 0, ref mac, ref length);
        string str1 = mac.ToString("X");
        if (str1 == "0")
          return "错误";
        while (str1.Length < 12)
          str1 = str1.Insert(0, "0");
        string str2 = "";
        for (int startIndex = 0; startIndex < 11; ++startIndex)
        {
          if (startIndex % 2 == 0)
            str2 = startIndex != 10 ? "-" + str2.Insert(0, str1.Substring(startIndex, 2)) : str2.Insert(0, str1.Substring(startIndex, 2));
        }
        return str2;
      }
      catch (Exception ex)
      {
        return ex.ToString();
      }
    }

    public static string GetBrowser()
    {
      HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
      return browser.Browser + browser.Version;
    }

    public static string GetOSVersion()
    {
      string serverVariable = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
      string str = "未知";
      if (serverVariable.Contains("NT 6.1"))
        str = "Windows 7";
      else if (serverVariable.Contains("NT 6.0"))
        str = "Windows Vista/Server 2008";
      else if (serverVariable.Contains("NT 5.2"))
        str = "Windows Server 2003";
      else if (serverVariable.Contains("NT 5.1"))
        str = "Windows XP";
      else if (serverVariable.Contains("NT 5"))
        str = "Windows 2000";
      else if (serverVariable.Contains("NT 4"))
        str = "Windows NT4";
      else if (serverVariable.Contains("Me"))
        str = "Windows Me";
      else if (serverVariable.Contains("98"))
        str = "Windows 98";
      else if (serverVariable.Contains("95"))
        str = "Windows 95";
      else if (serverVariable.Contains("Mac"))
        str = "Mac";
      else if (serverVariable.Contains("Unix"))
        str = "UNIX";
      else if (serverVariable.Contains("Linux"))
        str = "Linux";
      else if (serverVariable.Contains("SunOS"))
        str = "SunOS";
      return str;
    }

    public static string GetIP()
    {
      string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
      if (string.IsNullOrEmpty(str))
        str = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
      if (string.IsNullOrEmpty(str))
        str = HttpContext.Current.Request.UserHostAddress;
      if (string.IsNullOrEmpty(str))
        return "0.0.0.0";
      return str;
    }

    public static string GetIPAddress
    {
      get
      {
        string str1 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(str1))
        {
          if (str1.IndexOf(".") == -1)
            str1 = (string) null;
          else if (str1.IndexOf(",") != -1)
          {
            str1 = str1.Replace("  ", "").Replace("'", "");
            string[] strArray = str1.Split(",;".ToCharArray());
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (IPHelp.IsIPAddress(strArray[index]) && strArray[index].Substring(0, 3) != "10." && (strArray[index].Substring(0, 7) != "192.168" && strArray[index].Substring(0, 7) != "172.16."))
                return strArray[index];
            }
          }
          else
          {
            if (IPHelp.IsIPAddress(str1))
              return str1;
            str1 = (string) null;
          }
        }
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || !(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != string.Empty))
        {
          string serverVariable1 = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
        }
        else
        {
          string serverVariable2 = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        if (string.IsNullOrEmpty(str1))
          str1 = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
        if (string.IsNullOrEmpty(str1))
          str1 = HttpContext.Current.Request.UserHostAddress;
        return str1;
      }
    }

    public static bool IsIPAddress(string str1)
    {
      if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15)
        return false;
      return new Regex("^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$", RegexOptions.IgnoreCase).IsMatch(str1);
    }

    public static string GetNetIP()
    {
      string str = "";
      try
      {
        Stream responseStream = WebRequest.Create("http://city.ip138.com/ip2city.asp").GetResponse().GetResponseStream();
        StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
        string end = streamReader.ReadToEnd();
        int startIndex = end.IndexOf("[") + 1;
        int num = end.IndexOf("]", startIndex);
        str = end.Substring(startIndex, num - startIndex);
        streamReader.Close();
        responseStream.Close();
      }
      catch
      {
        if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 1)
          str = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
        if (string.IsNullOrEmpty(str))
          return IPHelp.GetIP();
      }
      return str;
    }

    public static string domain2ip(string str)
    {
      try
      {
        return Dns.GetHostByName(str).AddressList[0].ToString();
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }
  }
}
