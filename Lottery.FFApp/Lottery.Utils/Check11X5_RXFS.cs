// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_RXFS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class Check11X5_RXFS
  {
    public static int P11_RXFS_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray = CheckNumber.Split('_');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray[index]) > 1 || strArray[index].Length != 2)
          return 0;
      }
      for (int index = 0; index < strArray.Length; ++index)
      {
        string str = strArray[index];
        if (LotteryNumber.IndexOf(str) != -1)
          ++num;
      }
      return num;
    }

    public static int P11_RXFS_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 2)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          string[] strArray2 = (strArray1[index1] + "," + strArray1[index2]).Split(',');
          if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1)
            ++num;
        }
      }
      return num;
    }

    public static int P11_RXFS_3(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 3)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            string[] strArray2 = (strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3]).Split(',');
            if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1 && LotteryNumber.IndexOf(strArray2[2]) != -1)
              ++num;
          }
        }
      }
      return num;
    }

    public static int P11_RXFS_4(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 4)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < strArray1.Length; ++index4)
            {
              string[] strArray2 = (strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4]).Split(',');
              if (LotteryNumber.IndexOf(strArray2[0]) != -1 && LotteryNumber.IndexOf(strArray2[1]) != -1 && (LotteryNumber.IndexOf(strArray2[2]) != -1 && LotteryNumber.IndexOf(strArray2[3]) != -1))
                ++num;
            }
          }
        }
      }
      return num;
    }

    public static int P11_RXFS_5(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 5)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < strArray1.Length; ++index4)
            {
              for (int index5 = index4 + 1; index5 < strArray1.Length; ++index5)
              {
                string str = strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4] + "," + strArray1[index5];
                string[] strArray2 = LotteryNumber.Split(',');
                if (str.IndexOf(strArray2[0]) != -1 && str.IndexOf(strArray2[1]) != -1 && (str.IndexOf(strArray2[2]) != -1 && str.IndexOf(strArray2[3]) != -1) && str.IndexOf(strArray2[4]) != -1)
                  ++num;
              }
            }
          }
        }
      }
      return num;
    }

    public static int P11_RXFS_6(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 6)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < strArray1.Length; ++index4)
            {
              for (int index5 = index4 + 1; index5 < strArray1.Length; ++index5)
              {
                for (int index6 = index5 + 1; index6 < strArray1.Length; ++index6)
                {
                  string str = strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4] + "," + strArray1[index5] + "," + strArray1[index6];
                  string[] strArray2 = LotteryNumber.Split(',');
                  if (str.IndexOf(strArray2[0]) != -1 && str.IndexOf(strArray2[1]) != -1 && (str.IndexOf(strArray2[2]) != -1 && str.IndexOf(strArray2[3]) != -1) && str.IndexOf(strArray2[4]) != -1)
                    ++num;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int P11_RXFS_7(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 7)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < strArray1.Length; ++index4)
            {
              for (int index5 = index4 + 1; index5 < strArray1.Length; ++index5)
              {
                for (int index6 = index5 + 1; index6 < strArray1.Length; ++index6)
                {
                  for (int index7 = index6 + 1; index7 < strArray1.Length; ++index7)
                  {
                    string str = strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4] + "," + strArray1[index5] + "," + strArray1[index6] + "," + strArray1[index7];
                    string[] strArray2 = LotteryNumber.Split(',');
                    if (str.IndexOf(strArray2[0]) != -1 && str.IndexOf(strArray2[1]) != -1 && (str.IndexOf(strArray2[2]) != -1 && str.IndexOf(strArray2[3]) != -1) && str.IndexOf(strArray2[4]) != -1)
                      ++num;
                  }
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int P11_RXFS_8(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      if (strArray1.Length < 8)
        return 0;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || Check11X5_RXFS.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = index3 + 1; index4 < strArray1.Length; ++index4)
            {
              for (int index5 = index4 + 1; index5 < strArray1.Length; ++index5)
              {
                for (int index6 = index5 + 1; index6 < strArray1.Length; ++index6)
                {
                  for (int index7 = index6 + 1; index7 < strArray1.Length; ++index7)
                  {
                    for (int index8 = index7 + 1; index8 < strArray1.Length; ++index8)
                    {
                      string str = strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4] + "," + strArray1[index5] + "," + strArray1[index6] + "," + strArray1[index7] + "," + strArray1[index8];
                      string[] strArray2 = LotteryNumber.Split(',');
                      if (str.IndexOf(strArray2[0]) != -1 && str.IndexOf(strArray2[1]) != -1 && (str.IndexOf(strArray2[2]) != -1 && str.IndexOf(strArray2[3]) != -1) && str.IndexOf(strArray2[4]) != -1)
                        ++num;
                    }
                  }
                }
              }
            }
          }
        }
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
