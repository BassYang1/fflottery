// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_5Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class CheckSSC_5Start
  {
    public static int P_5FS(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[0].IndexOf(strArray1[0]) != -1 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_5DS(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      LotteryNumber.Split(',');
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string str = "";
      for (int index = 0; index < strArray1.Length; ++index)
        str += strArray1[index];
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (str == strArray2[index].Replace(",", ""))
          ++num;
      }
      return num;
    }

    public static int P_5ZX120(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split('_');
      string[] strArray2 = LotteryNumber.Split(',');
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray1.Length; ++index2)
        {
          for (int index3 = 0; index3 < strArray1.Length; ++index3)
          {
            for (int index4 = 0; index4 < strArray1.Length; ++index4)
            {
              for (int index5 = 0; index5 < strArray1.Length; ++index5)
              {
                if (index1 != index2 && index2 != index3 && (index1 != index3 && index3 != index4) && index4 != index5)
                {
                  if (new string[1]
                  {
                    strArray1[index1] + "," + strArray1[index2] + "," + strArray1[index3] + "," + strArray1[index4] + "," + strArray1[index5]
                  }[0].Replace(",", "").Distinct<char>().Count<char>() == 5 && strArray2[0].IndexOf(strArray1[index1]) != -1 && (strArray2[1].IndexOf(strArray1[index2]) != -1 && strArray2[2].IndexOf(strArray1[index3]) != -1) && (strArray2[3].IndexOf(strArray1[index4]) != -1 && strArray2[4].IndexOf(strArray1[index5]) != -1))
                    ++num;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int P_5ZX60(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str = "";
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 4)
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
        if (strArray1[1].IndexOf(list[0]) != -1 && strArray1[1].IndexOf(list[1]) != -1 && strArray1[1].IndexOf(list[2]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_5ZX30(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str1 = "";
      string str2 = "";
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
      {
        for (int index1 = 0; index1 < strArray2.Length; ++index1)
        {
          if (Regex.Matches(LotteryNumber, strArray2[index1]).Count == 2)
          {
            for (int index2 = 0; index2 < strArray2.Length; ++index2)
            {
              if (Regex.Matches(LotteryNumber, strArray2[index2]).Count == 2 && index1 != index2)
              {
                flag = true;
                str1 = strArray2[index1];
                str2 = strArray2[index2];
              }
            }
          }
        }
      }
      if (flag)
      {
        List<string> list = ((IEnumerable<string>) LotteryNumber.Split(',')).ToList<string>();
        list.Remove(str1);
        list.Remove(str1);
        list.Remove(str2);
        list.Remove(str2);
        if (strArray1[1].IndexOf(list[0]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_5ZX20(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str = "";
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 3)
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
        if (strArray1[1].IndexOf(list[0]) != -1 && strArray1[1].IndexOf(list[1]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_5ZX10(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag1 = false;
      string str1 = "";
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
      {
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (Regex.Matches(LotteryNumber, strArray2[index]).Count == 3)
          {
            flag1 = true;
            str1 = strArray2[index];
          }
        }
      }
      if (flag1)
      {
        List<string> list = ((IEnumerable<string>) LotteryNumber.Split(',')).ToList<string>();
        list.Remove(str1);
        list.Remove(str1);
        list.Remove(str1);
        bool flag2 = false;
        string str2 = "";
        for (int index = 0; index < list.Count; ++index)
        {
          if (Regex.Matches(LotteryNumber, list[index]).Count == 2)
          {
            flag2 = true;
            str2 = strArray2[index];
          }
        }
        if (flag2 && strArray1[1].IndexOf(str2) != -1)
          ++num;
      }
      return num;
    }

    public static int P_5ZX5(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = CheckNumber.Split(',');
      string[] strArray2 = strArray1[0].Split('_');
      bool flag = false;
      string str = "";
      if (LotteryNumber.Replace(",", "").Distinct<char>().Count<char>() == 2)
      {
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (Regex.Matches(LotteryNumber, strArray2[index]).Count == 4)
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
        list.Remove(str);
        if (strArray1[1].IndexOf(list[0]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_5TS(string LotteryNumber, string CheckNumber, int count)
    {
      int num = 0;
      if (CheckNumber.Length > 1 && !CheckNumber.Contains("_"))
        return 0;
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        char[] cc = str2.ToCharArray();
        if (LotteryNumber.Count<char>((Func<char, bool>) (c => (int) c == (int) cc[0])) == count)
          ++num;
      }
      return num;
    }

    public static int P_5ZH_5(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[0].IndexOf(strArray1[0]) != -1 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1) && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_5ZH_4(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[1].IndexOf(strArray1[1]) != -1 && (strArray2[2].IndexOf(strArray1[2]) != -1 && strArray2[3].IndexOf(strArray1[3]) != -1) && strArray2[4].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_5ZH_3(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[2].IndexOf(strArray1[2]) != -1 && (strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_5ZH_2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[3].IndexOf(strArray1[3]) != -1 && strArray2[4].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_5ZH_1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length >= 5 && strArray2[4].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }
  }
}
