// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckPK10_DDBDD
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class CheckPK10_DDBDD
  {
    public static int P_DD1_5(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length != 5)
        return 0;
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(strArray2[index1]))
        {
          string[] strArray3 = strArray2[index1].Split('_');
          for (int index2 = 0; index2 < strArray3.Length; ++index2)
          {
            if (strArray3[index2].Length != 2 || Convert.ToInt32(strArray3[index2]) > 10)
              return 0;
          }
        }
      }
      if (strArray2.Length == 5)
      {
        if (strArray2[0].Length > 2)
        {
          if (strArray2[0].Contains("_") && strArray2[0].IndexOf(strArray1[0]) != -1)
            ++num;
        }
        else if (strArray2[0].IndexOf(strArray1[0]) != -1)
          ++num;
        if (strArray2[1].Length > 2)
        {
          if (strArray2[1].Contains("_") && strArray2[1].IndexOf(strArray1[1]) != -1)
            ++num;
        }
        else if (strArray2[1].IndexOf(strArray1[1]) != -1)
          ++num;
        if (strArray2[2].Length > 2)
        {
          if (strArray2[2].Contains("_") && strArray2[2].IndexOf(strArray1[2]) != -1)
            ++num;
        }
        else if (strArray2[2].IndexOf(strArray1[2]) != -1)
          ++num;
        if (strArray2[3].Length > 2)
        {
          if (strArray2[3].Contains("_") && strArray2[3].IndexOf(strArray1[3]) != -1)
            ++num;
        }
        else if (strArray2[3].IndexOf(strArray1[3]) != -1)
          ++num;
        if (strArray2[4].Length > 2)
        {
          if (strArray2[4].Contains("_") && strArray2[4].IndexOf(strArray1[4]) != -1)
            ++num;
        }
        else if (strArray2[4].IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_DD6_10(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length != 5)
        return 0;
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(strArray2[index1]))
        {
          string[] strArray3 = strArray2[index1].Split('_');
          for (int index2 = 0; index2 < strArray3.Length; ++index2)
          {
            if (strArray3[index2].Length != 2 || Convert.ToInt32(strArray3[index2]) > 10)
              return 0;
          }
        }
      }
      if (strArray2.Length == 5)
      {
        if (strArray2[0].Length > 2)
        {
          if (strArray2[0].Contains("_") && strArray2[0].IndexOf(strArray1[5]) != -1)
            ++num;
        }
        else if (strArray2[0].IndexOf(strArray1[5]) != -1)
          ++num;
        if (strArray2[1].Length > 2)
        {
          if (strArray2[1].Contains("_") && strArray2[1].IndexOf(strArray1[6]) != -1)
            ++num;
        }
        else if (strArray2[1].IndexOf(strArray1[6]) != -1)
          ++num;
        if (strArray2[2].Length > 2)
        {
          if (strArray2[2].Contains("_") && strArray2[2].IndexOf(strArray1[7]) != -1)
            ++num;
        }
        else if (strArray2[2].IndexOf(strArray1[7]) != -1)
          ++num;
        if (strArray2[3].Length > 2)
        {
          if (strArray2[3].Contains("_") && strArray2[3].IndexOf(strArray1[8]) != -1)
            ++num;
        }
        else if (strArray2[3].IndexOf(strArray1[8]) != -1)
          ++num;
        if (strArray2[4].Length > 2)
        {
          if (strArray2[4].Contains("_") && strArray2[4].IndexOf(strArray1[9]) != -1)
            ++num;
        }
        else if (strArray2[4].IndexOf(strArray1[9]) != -1)
          ++num;
      }
      return num;
    }
  }
}
