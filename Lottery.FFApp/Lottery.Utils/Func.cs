// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Func
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public class Func
  {
    public static string Addstr(string Old, string New)
    {
      if (Old == "")
        return New;
      return Old + "," + New;
    }

    public static string AddZero(int Num, int Len)
    {
      string str1 = "";
      for (int index = 1; index <= Len; ++index)
        str1 += "0";
      string str2 = str1 + Num.ToString();
      return str2.Substring(str2.Length - Len);
    }

    public static string Delstr(string Old, string delStr)
    {
      string Old1 = "";
      string[] strArray = Old.Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index] != delStr)
          Old1 = Func.Addstr(Old1, strArray[index]);
      }
      return Old1;
    }

    public static string ExistUser(string Users, string Users2)
    {
      if (Users2 == "")
        return Users;
      string[] strArray = Users.Split(',');
      string str = "";
      foreach (string UserId in strArray)
      {
        if (!Func.IsExistUser(Users2, UserId))
          str = !(str == "") ? str + "," + UserId : UserId;
      }
      return str;
    }

    public static int GetLenNum(string str)
    {
      return str.Length;
    }

    public static int GetLettoryNum(string Lettory)
    {
      if (Lettory.IndexOf(",") == -1)
        return 0;
      string[] strArray1 = Lettory.Split('|');
      int num1 = 0;
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(',');
        int num2 = 1;
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
          num2 *= Func.GetLenNum(strArray2[index2]);
        num1 += num2;
      }
      return num1;
    }

    public static int GetLottoryNumZ3(string Lettory)
    {
      string[] strArray = Lettory.Split('|');
      int num = 0;
      for (int index = 0; index < strArray.Length; ++index)
        num += Func.GetLenNum(strArray[index]) * (Func.GetLenNum(strArray[index]) - 1) * 3;
      return num;
    }

    public static int GetLottoryNumZ6(string Lettory)
    {
      string[] strArray = Lettory.Split('|');
      int num1 = 0;
      for (int index = 0; index < strArray.Length; ++index)
      {
        int num2 = 0;
        switch (strArray[index].Length)
        {
          case 3:
            num2 = 6;
            break;
          case 4:
            num2 = 24;
            break;
          case 5:
            num2 = 60;
            break;
          case 6:
            num2 = 120;
            break;
          case 7:
            num2 = 210;
            break;
          case 8:
            num2 = 336;
            break;
          case 9:
            num2 = 504;
            break;
          case 10:
            num2 = 720;
            break;
        }
        num1 += num2;
      }
      return num1;
    }

    public static string GetOSNameByUserAgent(string userAgent)
    {
      string str = "未知";
      if (userAgent.Contains("NT 6.0"))
        return "Windows Vista/Server 2008";
      if (userAgent.Contains("NT 6.1"))
        return "Windows 7";
      if (userAgent.Contains("NT 5.2"))
        return "Windows Server 2003";
      if (userAgent.Contains("NT 5.1"))
        return "Windows XP";
      if (userAgent.Contains("NT 5"))
        return "Windows 2000";
      if (userAgent.Contains("NT 4"))
        return "Windows NT4";
      if (userAgent.Contains("Me"))
        return "Windows Me";
      if (userAgent.Contains("98"))
        return "Windows 98";
      if (userAgent.Contains("95"))
        return "Windows 95";
      if (userAgent.Contains("Mac"))
        return "Mac";
      if (userAgent.Contains("Unix"))
        return "UNIX";
      if (userAgent.Contains("Linux"))
        return "Linux";
      if (userAgent.Contains("SunOS"))
        str = "SunOS";
      return str;
    }

    public static bool IsDateTime(string source)
    {
      try
      {
        Convert.ToDateTime(source);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static bool IsExistUser(string Users, string UserId)
    {
      bool flag = true;
      if (("," + Users + ",").IndexOf("," + UserId + ",") == -1)
        flag = false;
      return flag;
    }

    public static string MoneyColor(Decimal money)
    {
      if (money < new Decimal(0))
        return "<font color='#339900'>" + money.ToString("0.00") + "</font>";
      if (money > new Decimal(0))
        return "<font color='#CCFF00'>+" + money.ToString("0.00") + "</font>";
      return "<font color='#000000'>" + money.ToString("0.00") + "</font>";
    }

    public static string NumberPos(string Pos)
    {
      string str1 = "";
      string[] strArray = "万位,千位,百位,十位,个位".Split(',');
      string str2 = Pos;
      char[] chArray = new char[1]{ ',' };
      foreach (string str3 in str2.Split(chArray))
        str1 = !(str1 == "") ? str1 + "，" + strArray[Convert.ToInt32(str3)] : strArray[Convert.ToInt32(str3)];
      return str1;
    }

    public static int SearchStrNum(string strs, string str)
    {
      int num = 0;
      for (int startIndex = 0; startIndex < strs.Length; ++startIndex)
      {
        if (strs.Substring(startIndex, 1) == str)
          ++num;
      }
      return num;
    }

    public static string ShowMoney(Decimal Money)
    {
      if (Money > new Decimal(0))
        return "<font color='#FF3300'>" + Money.ToString("0.00") + "</font>";
      if (Money < new Decimal(0))
        return "<font color='#00FF00'>" + Money.ToString("0.00") + "</font>";
      return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
    }

    public static string ShowMoneyPT(Decimal Money)
    {
      return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
    }

    public static string ShowMoneyCheck(Decimal Money)
    {
      if (Money > new Decimal(0))
        return "<font color='#FF3300'>" + Money.ToString("0.00") + "(盈)</font>";
      if (Money < new Decimal(0))
        return "<font color='#00FF00'>" + Money.ToString("0.00") + "(亏)</font>";
      return "<font color='#000000'>" + Money.ToString("0.00") + "</font>";
    }

    public static string UserCode(int UserId, string ParentCode)
    {
      return ParentCode + Func.UserCodeLength(UserId);
    }

    public static string UserCodeLength(int UserId)
    {
      string str = "00000000" + UserId.ToString();
      return str.Substring(str.Length - 8);
    }
  }
}
