// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.MD5
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Web.Security;

namespace Lottery.Utils
{
  public static class MD5
  {
    public static string Last64(string s)
    {
      if (s.Length != 32)
        return "";
      return MD5.Lower32(s.Substring(0, 16)) + MD5.Lower32(s.Substring(16, 16));
    }

    public static string Upper32(string s)
    {
      s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
      return s.ToUpper();
    }

    public static string Lower32(string s)
    {
      s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
      return s.ToLower();
    }

    public static string Upper16(string s)
    {
      s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
      return s.ToUpper().Substring(8, 16);
    }

    public static string Lower16(string s)
    {
      s = FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
      return s.ToLower().Substring(8, 16);
    }
  }
}
