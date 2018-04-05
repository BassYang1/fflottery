// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_DDBDD
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class Check11X5_DDBDD
  {
    public static int P_BDD(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split('_');
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (strArray2[index].Length != 2 || Convert.ToInt32(strArray2[index]) > 11 || Convert.ToInt32(strArray2[index]) < 1)
          return 0;
      }
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (strArray1[0].IndexOf(strArray2[index]) != -1 || strArray1[1].IndexOf(strArray2[index]) != -1 || strArray1[2].IndexOf(strArray2[index]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_DD(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length != 3)
        return 0;
      for (int index1 = 0; index1 < strArray2.Length; ++index1)
      {
        if (!string.IsNullOrEmpty(strArray2[index1]))
        {
          string[] strArray3 = strArray2[index1].Split('_');
          for (int index2 = 0; index2 < strArray3.Length; ++index2)
          {
            if (strArray3[index2].Length != 2 || Convert.ToInt32(strArray3[index2]) > 11)
              return 0;
          }
        }
      }
      if (strArray2.Length == 3)
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
      }
      return num;
    }
  }
}
