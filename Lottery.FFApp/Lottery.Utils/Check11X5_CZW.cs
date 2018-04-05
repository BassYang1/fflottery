// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_CZW
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class Check11X5_CZW
  {
    private static int min;

    public static int P_CZW(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray = Check11X5_CZW.Sort(LotteryNumber.Split(','));
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.IndexOf(string.Concat((object) int.Parse(strArray[2]))) != -1)
          ++num;
      }
      return num;
    }

    public static string[] Sort(string[] arr)
    {
      for (int index1 = 0; index1 < arr.Length - 1; ++index1)
      {
        Check11X5_CZW.min = index1;
        for (int index2 = index1 + 1; index2 < arr.Length; ++index2)
        {
          if (Convert.ToInt32(arr[index2]) < Convert.ToInt32(arr[Check11X5_CZW.min]))
            Check11X5_CZW.min = index2;
        }
        string str = arr[Check11X5_CZW.min];
        arr[Check11X5_CZW.min] = arr[index1];
        arr[index1] = str;
      }
      return arr;
    }
  }
}
