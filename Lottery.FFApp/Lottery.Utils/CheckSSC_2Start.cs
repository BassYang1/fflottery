// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_2Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Utils
{
  public static class CheckSSC_2Start
  {
    public static int P_2ZX(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
        if (Pos == "WQ")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "WB")
          LotteryNumber = strArray1[0] + "," + strArray1[2];
        if (Pos == "WS")
          LotteryNumber = strArray1[0] + "," + strArray1[3];
        if (Pos == "WG")
          LotteryNumber = strArray1[0] + "," + strArray1[4];
        if (Pos == "QB")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
        if (Pos == "QS")
          LotteryNumber = strArray1[1] + "," + strArray1[3];
        if (Pos == "QG")
          LotteryNumber = strArray1[1] + "," + strArray1[4];
        if (Pos == "BS")
          LotteryNumber = strArray1[2] + "," + strArray1[3];
        if (Pos == "BG")
          LotteryNumber = strArray1[2] + "," + strArray1[4];
        if (Pos == "SG")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      if (strArray3.Length >= 2 && strArray3[0].IndexOf(strArray2[0]) != -1 && strArray3[1].IndexOf(strArray2[1]) != -1)
        ++num;
      return num;
    }

    public static int P_2DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
        if (Pos == "WQ")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "WB")
          LotteryNumber = strArray1[0] + "," + strArray1[2];
        if (Pos == "WS")
          LotteryNumber = strArray1[0] + "," + strArray1[3];
        if (Pos == "WG")
          LotteryNumber = strArray1[0] + "," + strArray1[4];
        if (Pos == "QB")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
        if (Pos == "QS")
          LotteryNumber = strArray1[1] + "," + strArray1[3];
        if (Pos == "QG")
          LotteryNumber = strArray1[1] + "," + strArray1[4];
        if (Pos == "BS")
          LotteryNumber = strArray1[2] + "," + strArray1[3];
        if (Pos == "BG")
          LotteryNumber = strArray1[2] + "," + strArray1[4];
        if (Pos == "SG")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      string str = "";
      for (int index = 0; index < strArray2.Length; ++index)
        str += strArray2[index];
      for (int index = 0; index < strArray3.Length; ++index)
      {
        if (str == strArray3[index].Replace(",", ""))
          ++num;
      }
      return num;
    }

    public static int P_2Z2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "R")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
        if (Pos == "WQ")
          LotteryNumber = strArray1[0] + "," + strArray1[1];
        if (Pos == "WB")
          LotteryNumber = strArray1[0] + "," + strArray1[2];
        if (Pos == "WS")
          LotteryNumber = strArray1[0] + "," + strArray1[3];
        if (Pos == "WG")
          LotteryNumber = strArray1[0] + "," + strArray1[4];
        if (Pos == "QB")
          LotteryNumber = strArray1[1] + "," + strArray1[2];
        if (Pos == "QS")
          LotteryNumber = strArray1[1] + "," + strArray1[3];
        if (Pos == "QG")
          LotteryNumber = strArray1[1] + "," + strArray1[4];
        if (Pos == "BS")
          LotteryNumber = strArray1[2] + "," + strArray1[3];
        if (Pos == "BG")
          LotteryNumber = strArray1[2] + "," + strArray1[4];
        if (Pos == "SG")
          LotteryNumber = strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      if (strArray2[0] != strArray2[1])
      {
        for (int index = 0; index < strArray3.Length; ++index)
        {
          if (strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[1]) != -1)
            ++num;
        }
      }
      return num;
    }

    public static int P_2HE(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray = LotteryNumber.Split(',');
      int num2 = 0;
      if (strArray.Length == 3)
      {
        if (Pos == "L")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        if (Pos == "R")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
      }
      else
      {
        if (Pos == "L")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        if (Pos == "R")
          num2 = Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WQ")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        if (Pos == "WB")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]);
        if (Pos == "WS")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[3]);
        if (Pos == "WG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[4]);
        if (Pos == "QB")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        if (Pos == "QS")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]);
        if (Pos == "QG")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[4]);
        if (Pos == "BS")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "BG")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        if (Pos == "SG")
          num2 = Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
      }
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (Convert.ToInt32(str2) == num2)
          ++num1;
      }
      return num1;
    }

    public static int P_2ZHE(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray = LotteryNumber.Split(',');
      int num2 = 0;
      if (strArray.Length == 3)
      {
        if (Pos == "L")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        if (Pos == "R")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
      }
      else
      {
        if (Pos == "L")
        {
          if (Convert.ToInt32(strArray[0]) == Convert.ToInt32(strArray[1]))
            return 0;
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        }
        if (Pos == "R")
        {
          if (Convert.ToInt32(strArray[3]) == Convert.ToInt32(strArray[4]))
            return 0;
          num2 = Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        }
        if (Pos == "WQ")
        {
          if (Convert.ToInt32(strArray[0]) == Convert.ToInt32(strArray[1]))
            return 0;
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]);
        }
        if (Pos == "WB")
        {
          if (Convert.ToInt32(strArray[0]) == Convert.ToInt32(strArray[2]))
            return 0;
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]);
        }
        if (Pos == "WS")
        {
          if (Convert.ToInt32(strArray[0]) == Convert.ToInt32(strArray[3]))
            return 0;
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[3]);
        }
        if (Pos == "WG")
        {
          if (Convert.ToInt32(strArray[0]) == Convert.ToInt32(strArray[4]))
            return 0;
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[4]);
        }
        if (Pos == "QB")
        {
          if (Convert.ToInt32(strArray[1]) == Convert.ToInt32(strArray[2]))
            return 0;
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        }
        if (Pos == "QS")
        {
          if (Convert.ToInt32(strArray[1]) == Convert.ToInt32(strArray[3]))
            return 0;
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]);
        }
        if (Pos == "QG")
        {
          if (Convert.ToInt32(strArray[1]) == Convert.ToInt32(strArray[4]))
            return 0;
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[4]);
        }
        if (Pos == "BS")
        {
          if (Convert.ToInt32(strArray[2]) == Convert.ToInt32(strArray[3]))
            return 0;
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        }
        if (Pos == "BG")
        {
          if (Convert.ToInt32(strArray[2]) == Convert.ToInt32(strArray[4]))
            return 0;
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        }
        if (Pos == "SG")
        {
          if (Convert.ToInt32(strArray[3]) == Convert.ToInt32(strArray[4]))
            return 0;
          num2 = Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        }
      }
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (Convert.ToInt32(str2) == num2)
          ++num1;
      }
      return num1;
    }

    public static int P_2KD(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[2];
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "R")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      if (Pos == "WQ")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "WB")
        LotteryNumber = strArray1[0] + "," + strArray1[2];
      if (Pos == "WS")
        LotteryNumber = strArray1[0] + "," + strArray1[3];
      if (Pos == "WG")
        LotteryNumber = strArray1[0] + "," + strArray1[4];
      if (Pos == "QB")
        LotteryNumber = strArray1[1] + "," + strArray1[2];
      if (Pos == "QS")
        LotteryNumber = strArray1[1] + "," + strArray1[3];
      if (Pos == "QG")
        LotteryNumber = strArray1[1] + "," + strArray1[4];
      if (Pos == "BS")
        LotteryNumber = strArray1[2] + "," + strArray1[3];
      if (Pos == "BG")
        LotteryNumber = strArray1[2] + "," + strArray1[4];
      if (Pos == "SG")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      string[] strArray3 = LotteryNumber.Split(',');
      int num2 = Convert.ToInt32(((IEnumerable<string>) strArray3).Max<string>()) - Convert.ToInt32(((IEnumerable<string>) strArray3).Min<string>());
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (Convert.ToInt32(str2) == num2)
          ++num1;
      }
      return num1;
    }

    public static int P_2ZBD(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[2];
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "R")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      if (Pos == "WQ")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "WB")
        LotteryNumber = strArray1[0] + "," + strArray1[2];
      if (Pos == "WS")
        LotteryNumber = strArray1[0] + "," + strArray1[3];
      if (Pos == "WG")
        LotteryNumber = strArray1[0] + "," + strArray1[4];
      if (Pos == "QB")
        LotteryNumber = strArray1[1] + "," + strArray1[2];
      if (Pos == "QS")
        LotteryNumber = strArray1[1] + "," + strArray1[3];
      if (Pos == "QG")
        LotteryNumber = strArray1[1] + "," + strArray1[4];
      if (Pos == "BS")
        LotteryNumber = strArray1[2] + "," + strArray1[3];
      if (Pos == "BG")
        LotteryNumber = strArray1[2] + "," + strArray1[4];
      if (Pos == "SG")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      string[] strArray3 = LotteryNumber.Split(',');
      if (strArray3[0] != strArray3[1] && (strArray3[0] + "," + strArray3[1]).Contains(CheckNumber))
        ++num;
      return num;
    }

    public static int P_2ZDS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[2];
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "R")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      if (Pos == "WQ")
        LotteryNumber = strArray1[0] + "," + strArray1[1];
      if (Pos == "WB")
        LotteryNumber = strArray1[0] + "," + strArray1[2];
      if (Pos == "WS")
        LotteryNumber = strArray1[0] + "," + strArray1[3];
      if (Pos == "WG")
        LotteryNumber = strArray1[0] + "," + strArray1[4];
      if (Pos == "QB")
        LotteryNumber = strArray1[1] + "," + strArray1[2];
      if (Pos == "QS")
        LotteryNumber = strArray1[1] + "," + strArray1[3];
      if (Pos == "QG")
        LotteryNumber = strArray1[1] + "," + strArray1[4];
      if (Pos == "BS")
        LotteryNumber = strArray1[2] + "," + strArray1[3];
      if (Pos == "BG")
        LotteryNumber = strArray1[2] + "," + strArray1[4];
      if (Pos == "SG")
        LotteryNumber = strArray1[3] + "," + strArray1[4];
      string[] strArray3 = LotteryNumber.Split(',');
      if (CheckNumber.Contains(strArray3[0] + strArray3[1]))
        ++num;
      if (CheckNumber.Contains(strArray3[1] + strArray3[0]))
        ++num;
      return num;
    }
  }
}
