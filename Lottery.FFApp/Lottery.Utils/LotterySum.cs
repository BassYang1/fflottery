// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.LotterySum
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public class LotterySum
  {
    public static int IsDS(int SumNumber)
    {
      return SumNumber % 2 == 0 ? 1 : -1;
    }

    public static int IsDX(int SumNumber, int ComNum, int IsDsDx)
    {
      if (SumNumber == ComNum)
        return IsDsDx;
      return SumNumber > ComNum ? 1 : -1;
    }

    public static string ShowDsStr(int DS)
    {
      switch (DS)
      {
        case -1:
          return "<font color='#FF6600'>单</font>";
        case 1:
          return "<font color='#33FF00'>双</font>";
        default:
          return "";
      }
    }

    public static string ShowDxDsStr(int DX, int DS, int IsShow)
    {
      string str = "";
      if (DX == -1)
        str = "小";
      if (DX == 1)
        str = "大";
      if (DX == 0 && IsShow == 1)
        str = "和";
      if (DS == -1)
        str += " 单";
      if (DS == 1)
        str += " 双";
      return str;
    }

    public static string ShowDxStr(int DX, int IsShow)
    {
      switch (DX)
      {
        case -1:
          return "<font color='#FF6600'>小</font>";
        case 1:
          return "<font color='#33FF00'>大</font>";
        default:
          return IsShow == 1 ? "<font color='Red'>和</font>" : "";
      }
    }

    public static string ShowHappy10Num(string Number)
    {
      string str1 = "<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">" + "<tr>";
      string str2 = "";
      string[] strArray = Regex.Split(Number, ",");
      for (int index = 0; index < strArray.Length; ++index)
        str2 = !(str2 == "") ? str2 + "<td class=\"Sf_Num_s\">&nbsp;</td><td class=\"Sf_Num\">" + strArray[index] + "</td>" : "<td class=\"Sf_Num\">" + strArray[index] + "</td>";
      return str1 + str2 + "</tr></table>";
    }

    public static string ShowHappy8Num(string Number)
    {
      string str1 = "<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">" + "<tr>";
      string str2 = "";
      string[] strArray = Regex.Split(Number, ",");
      for (int index = 0; index < strArray.Length; ++index)
        str2 = !(str2 == "") ? str2 + "<td class=\"KL_Num_s\">&nbsp;</td><td class=\"KL_Num\">" + strArray[index] + "</td>" : "<td class=\"KL_Num\">" + strArray[index] + "</td>";
      return str1 + str2 + "</tr></table>";
    }

    public static int SumNumber(string Num)
    {
      int num = 0;
      string str1 = Num;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
        num += Convert.ToInt32(str2);
      return num;
    }
  }
}
