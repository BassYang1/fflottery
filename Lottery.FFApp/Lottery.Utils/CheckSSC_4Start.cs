// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_4Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class CheckSSC_4Start
  {
    public static int P_4FS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "R")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      if (strArray3.Length >= 4 && strArray3[0].IndexOf(strArray2[0]) != -1 && (strArray3[1].IndexOf(strArray2[1]) != -1 && strArray3[2].IndexOf(strArray2[2]) != -1) && strArray3[3].IndexOf(strArray2[3]) != -1)
        ++num;
      return num;
    }

    public static int P_4DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "R")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
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

    public static int P_4ZX24(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "R")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      string[] strArray2 = LotteryNumber.Split(',');
      if (Convert.ToInt32(strArray2[0]) != Convert.ToInt32(strArray2[1]) && Convert.ToInt32(strArray2[0]) != Convert.ToInt32(strArray2[2]) && (Convert.ToInt32(strArray2[0]) != Convert.ToInt32(strArray2[3]) && Convert.ToInt32(strArray2[1]) != Convert.ToInt32(strArray2[2])) && (Convert.ToInt32(strArray2[1]) != Convert.ToInt32(strArray2[3]) && Convert.ToInt32(strArray2[2]) != Convert.ToInt32(strArray2[3]) && (CheckNumber.Contains(strArray2[0]) && CheckNumber.Contains(strArray2[1]))) && (CheckNumber.Contains(strArray2[2]) && CheckNumber.Contains(strArray2[3])))
        ++num;
      return num;
    }

    public static int P_4ZX12(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str = "";
      string[] strArray3 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[3];
      if (Pos == "R")
        LotteryNumber = strArray3[1] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray3[0] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray3[1] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
      {
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (Regex.Matches(LotteryNumber, strArray2[index]).Count == 2)
          {
            flag = true;
            str = strArray2[index];
          }
        }
      }
      if (flag)
      {
        List<string> list = ((IEnumerable<string>) LotteryNumber.Split(',')).ToList<string>();
        list.Remove(str);
        list.Remove(str);
        if (strArray1[1].IndexOf(list[0]) != -1 && strArray1[1].IndexOf(list[1]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_4ZX6(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',')[0].Split('_');
      bool flag = false;
      string[] strArray2 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3];
      if (Pos == "R")
        LotteryNumber = strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," + strArray2[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray2[0] + "," + strArray2[1] + "," + strArray2[3] + "," + strArray2[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray2[0] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray2[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4];
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
      {
        for (int index1 = 0; index1 < strArray1.Length; ++index1)
        {
          if (Regex.Matches(LotteryNumber, strArray1[index1]).Count == 2)
          {
            for (int index2 = 0; index2 < strArray1.Length; ++index2)
            {
              if (Regex.Matches(LotteryNumber, strArray1[index2]).Count == 2 && index1 != index2)
              {
                flag = true;
                string str1 = strArray1[index1];
                string str2 = strArray1[index2];
              }
            }
          }
        }
      }
      if (flag)
        ++num;
      return num;
    }

    public static int P_4ZX4(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str = "";
      string[] strArray3 = LotteryNumber.Split(',');
      if (Pos == "L")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[3];
      if (Pos == "R")
        LotteryNumber = strArray3[1] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "WQBS")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[3];
      if (Pos == "WQBG")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[2] + "," + strArray3[4];
      if (Pos == "WQSG")
        LotteryNumber = strArray3[0] + "," + strArray3[1] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "WBSG")
        LotteryNumber = strArray3[0] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (Pos == "QBSG")
        LotteryNumber = strArray3[1] + "," + strArray3[2] + "," + strArray3[3] + "," + strArray3[4];
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
      {
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (Regex.Matches(LotteryNumber, strArray2[index]).Count == 3)
          {
            flag = true;
            str = strArray2[index];
          }
        }
      }
      if (flag)
      {
        List<string> list = ((IEnumerable<string>) LotteryNumber.Split(',')).ToList<string>();
        list.Remove(str);
        list.Remove(str);
        list.Remove(str);
        if (strArray1[1].IndexOf(list[0]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_4ZH_4(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 4 && (strArray2[0].IndexOf(strArray1[0]) != -1 && strArray2[1].IndexOf(strArray1[1]) != -1) && (strArray2[2].IndexOf(strArray1[2]) != -1 && strArray2[3].IndexOf(strArray1[3]) != -1))
        ++num;
      if (Pos == "R" && strArray2.Length >= 4 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_4ZH_3(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 4 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && strArray2[3].IndexOf(strArray1[3]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length >= 4 && (strArray2[2].IndexOf(strArray1[2]) != -1 && strArray2[2].IndexOf(strArray1[3]) != -1) && strArray2[3].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_4ZH_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 4 && (strArray2[2].IndexOf(strArray1[2]) != -1 && strArray2[3].IndexOf(strArray1[3]) != -1))
        ++num;
      if (Pos == "R" && strArray2.Length >= 4 && (strArray2[2].IndexOf(strArray1[3]) != -1 && strArray2[3].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_4ZH_1(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 4 && strArray2[3].IndexOf(strArray1[3]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length >= 4 && strArray2[3].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }
  }
}
