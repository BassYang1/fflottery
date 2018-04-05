// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Check11X5_CDS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class Check11X5_CDS
  {
    public static int P_CDS(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      int[] numArray = Check11X5_CDS.Sort(LotteryNumber.Split(','));
      string str1 = numArray[0].ToString() + "单" + (object) numArray[1] + "双";
      string str2 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str3 in str2.Split(chArray))
      {
        if (str3.IndexOf(str1) != -1)
          ++num;
      }
      return num;
    }

    public static int[] Sort(string[] arr)
    {
      int num1 = 0;
      int num2 = 0;
      int[] numArray = new int[2];
      for (int index = 0; index < arr.Length; ++index)
      {
        if (Convert.ToInt32(arr[index]) % 2 == 0)
          ++num2;
        else
          ++num1;
      }
      numArray[0] = num1;
      numArray[1] = num2;
      return numArray;
    }
  }
}
