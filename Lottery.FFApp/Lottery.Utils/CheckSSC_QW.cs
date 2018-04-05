// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_QW
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils
{
  public static class CheckSSC_QW
  {
    public static string ReplaceStr(string str)
    {
      return str.Replace("0", "一区").Replace("1", "一区").Replace("2", "二区").Replace("3", "二区").Replace("4", "三区").Replace("5", "三区").Replace("6", "四区").Replace("7", "四区").Replace("8", "五区").Replace("9", "五区");
    }

    public static int P_5QJ3_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 5 && strArray2[0].IndexOf(CheckSSC_QW.ReplaceStr(strArray1[0])) != -1 && (strArray2[1].IndexOf(CheckSSC_QW.ReplaceStr(strArray1[1])) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_5QJ3_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 5 && strArray2[2].IndexOf(strArray1[2]) != -1 && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_4QJ3_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 4 && strArray2[0].IndexOf(CheckSSC_QW.ReplaceStr(strArray1[1])) != -1 && (strArray2[1].IndexOf(strArray1[2]) != -1 && strArray2[2].IndexOf(strArray1[3]) != -1) && strArray2[3].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_4QJ3_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 4 && strArray2[1].IndexOf(strArray1[2]) != -1 && (strArray2[2].IndexOf(strArray1[3]) != -1 && strArray2[3].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_3QJ2_1(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length == 3 && (strArray2[0].IndexOf(CheckSSC_QW.ReplaceStr(strArray1[0])) != -1 && strArray2[1].IndexOf(strArray1[1]) != -1) && strArray2[2].IndexOf(strArray1[2]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length == 3 && (strArray2[0].IndexOf(CheckSSC_QW.ReplaceStr(strArray1[2])) != -1 && strArray2[1].IndexOf(strArray1[3]) != -1) && strArray2[2].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_3QJ2_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length == 3 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1))
        ++num;
      if (Pos == "R" && strArray2.Length == 3 && (strArray2[1].IndexOf(strArray1[3]) != -1 && strArray2[2].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static string ReplaceDX(string str)
    {
      return str.Replace("0", "小").Replace("1", "小").Replace("2", "小").Replace("3", "小").Replace("4", "小").Replace("5", "大").Replace("6", "大").Replace("7", "大").Replace("8", "大").Replace("9", "大");
    }

    public static int P_5QW3_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 5 && strArray2[0].IndexOf(CheckSSC_QW.ReplaceDX(strArray1[0])) != -1 && (strArray2[1].IndexOf(CheckSSC_QW.ReplaceDX(strArray1[1])) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_5QW3_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 5 && strArray2[2].IndexOf(strArray1[2]) != -1 && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_4QW3_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 4 && strArray2[0].IndexOf(CheckSSC_QW.ReplaceDX(strArray1[1])) != -1 && (strArray2[1].IndexOf(strArray1[2]) != -1 && strArray2[2].IndexOf(strArray1[3]) != -1) && strArray2[3].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_4QW3_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 4 && strArray2[1].IndexOf(strArray1[2]) != -1 && (strArray2[2].IndexOf(strArray1[3]) != -1 && strArray2[3].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_3QW2_1(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length == 3 && (strArray2[0].IndexOf(CheckSSC_QW.ReplaceDX(strArray1[0])) != -1 && strArray2[1].IndexOf(strArray1[1]) != -1) && strArray2[2].IndexOf(strArray1[2]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length == 3 && (strArray2[0].IndexOf(CheckSSC_QW.ReplaceDX(strArray1[2])) != -1 && strArray2[1].IndexOf(strArray1[3]) != -1) && strArray2[2].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_3QW2_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length == 3 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1))
        ++num;
      if (Pos == "R" && strArray2.Length == 3 && (strArray2[1].IndexOf(strArray1[3]) != -1 && strArray2[2].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }
  }
}
