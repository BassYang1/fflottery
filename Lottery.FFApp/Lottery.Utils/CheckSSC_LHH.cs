// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_LHH
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class CheckSSC_LHH
  {
    public static int P_LHH(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray = LotteryNumber.Split(',');
      CheckNumber.Split('_');
      int num2 = 0;
      int num3 = 0;
      if (Pos == "WQ")
      {
        num2 = Convert.ToInt32(strArray[0]);
        num3 = Convert.ToInt32(strArray[1]);
      }
      if (Pos == "WB")
      {
        num2 = Convert.ToInt32(strArray[0]);
        num3 = Convert.ToInt32(strArray[2]);
      }
      if (Pos == "WS")
      {
        num2 = Convert.ToInt32(strArray[0]);
        num3 = Convert.ToInt32(strArray[3]);
      }
      if (Pos == "WG")
      {
        num2 = Convert.ToInt32(strArray[0]);
        num3 = Convert.ToInt32(strArray[4]);
      }
      if (Pos == "QB")
      {
        num2 = Convert.ToInt32(strArray[1]);
        num3 = Convert.ToInt32(strArray[2]);
      }
      if (Pos == "QS")
      {
        num2 = Convert.ToInt32(strArray[1]);
        num3 = Convert.ToInt32(strArray[3]);
      }
      if (Pos == "QG")
      {
        num2 = Convert.ToInt32(strArray[1]);
        num3 = Convert.ToInt32(strArray[4]);
      }
      if (Pos == "BS")
      {
        num2 = Convert.ToInt32(strArray[2]);
        num3 = Convert.ToInt32(strArray[3]);
      }
      if (Pos == "BG")
      {
        num2 = Convert.ToInt32(strArray[2]);
        num3 = Convert.ToInt32(strArray[4]);
      }
      if (Pos == "SG")
      {
        num2 = Convert.ToInt32(strArray[3]);
        num3 = Convert.ToInt32(strArray[4]);
      }
      string str = "";
      if (num2 > num3)
        str = "龙";
      if (num2 == num3)
        str = "和";
      if (num2 < num3)
        str = "虎";
      if (CheckNumber.IndexOf(str) != -1)
        ++num1;
      return num1;
    }
  }
}
