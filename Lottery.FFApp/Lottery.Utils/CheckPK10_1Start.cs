// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckPK10_1Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class CheckPK10_1Start
  {
    public static int PK10_1FS(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      if (!new Regex("^[_0-9]+$").IsMatch(CheckNumber))
        return 0;
      string[] strArray1 = CheckNumber.Split('_');
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (string.IsNullOrEmpty(strArray1[index]) || CheckPK10_1Start.SubstringCount(CheckNumber, strArray1[index]) > 1 || strArray1[index].Length != 2)
          return 0;
      }
      string[] strArray2 = LotteryNumber.Split(',');
      for (int index = 0; index < strArray1.Length; ++index)
      {
        string str = strArray1[index];
        if (strArray2[0].IndexOf(str) != -1)
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
