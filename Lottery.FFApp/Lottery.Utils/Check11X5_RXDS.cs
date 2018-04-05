// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_RXDS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils
{
  public static class Check11X5_RXDS
  {
    public static int P11_RXDS_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Length != 2)
          return 0;
        if (LotteryNumber.IndexOf(str2) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(' ');
        if (strArray2.Length != 2)
          return 0;
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray1[index1], strArray2[index2]) > 1)
            return 0;
        }
        if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_3(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(' ');
        if (strArray2.Length != 3)
          return 0;
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray1[index1], strArray2[index2]) > 1)
            return 0;
        }
        if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1 && LotteryNumber.IndexOf(strArray2[2]) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_4(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(' ');
        if (strArray2.Length != 4)
          return 0;
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray1[index1], strArray2[index2]) > 1)
            return 0;
        }
        if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1 && (LotteryNumber.IndexOf(strArray2[2]) != -1 && LotteryNumber.IndexOf(strArray2[3]) != -1))
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_5(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        string[] strArray3 = strArray2[index1].Split(' ');
        if (strArray3.Length != 5)
          return 0;
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray2[index1], strArray3[index2]) > 1)
            return 0;
        }
        if (strArray2[index1].IndexOf(strArray1[0]) != -1 && strArray2[index1].IndexOf(strArray1[1]) != -1 && (strArray2[index1].IndexOf(strArray1[2]) != -1 && strArray2[index1].IndexOf(strArray1[3]) != -1) && strArray2[index1].IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_6(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        string[] strArray3 = strArray2[index1].Split(' ');
        if (strArray3.Length != 6)
          return 0;
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray2[index1], strArray3[index2]) > 1)
            return 0;
        }
        string str = strArray2[index1];
        if (str.IndexOf(strArray1[0]) != -1 && str.IndexOf(strArray1[1]) != -1 && (str.IndexOf(strArray1[2]) != -1 && str.IndexOf(strArray1[3]) != -1) && str.IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_7(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        string[] strArray3 = strArray2[index1].Split(' ');
        if (strArray3.Length != 7)
          return 0;
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray2[index1], strArray3[index2]) > 1)
            return 0;
        }
        string str = strArray2[index1];
        if (str.IndexOf(strArray1[0]) != -1 && str.IndexOf(strArray1[1]) != -1 && (str.IndexOf(strArray1[2]) != -1 && str.IndexOf(strArray1[3]) != -1) && str.IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXDS_8(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        string[] strArray3 = strArray2[index1].Split(' ');
        if (strArray3.Length != 8)
          return 0;
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          if (Check11X5_RXDS.SubstringCount(strArray2[index1], strArray3[index2]) > 1)
            return 0;
        }
        string str = strArray2[index1];
        if (str.IndexOf(strArray1[0]) != -1 && str.IndexOf(strArray1[1]) != -1 && (str.IndexOf(strArray1[2]) != -1 && str.IndexOf(strArray1[3]) != -1) && str.IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    private static int SubstringCount(string str, string substring)
    {
      if (!str.Contains(substring))
        return 0;
      string str1 = str.Replace(substring, "");
      return (str.Length - str1.Length) / substring.Length;
    }
  }
}
