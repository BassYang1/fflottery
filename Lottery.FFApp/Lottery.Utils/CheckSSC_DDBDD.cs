// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_DDBDD
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Collections;

namespace Lottery.Utils
{
  public static class CheckSSC_DDBDD
  {
    public static int P_BDD(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length > 1)
        return 0;
      if (Pos == "")
        Pos = "L";
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (strArray1.Length == 3)
        {
          if (strArray2[index].IndexOf(strArray1[0]) != -1)
            ++num;
          if (strArray2[index].IndexOf(strArray1[1]) != -1)
            ++num;
          if (strArray2[index].IndexOf(strArray1[2]) != -1)
            ++num;
        }
        else
        {
          if (Pos == "L")
          {
            if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
            {
              if (strArray1[0] == strArray1[1] && strArray1[0] == strArray1[2])
              {
                if (strArray2[index].IndexOf(strArray1[0]) != -1)
                  ++num;
              }
              else
              {
                if (strArray1[0] == strArray1[1])
                {
                  if (strArray2[index].IndexOf(strArray1[1]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                }
                if (strArray1[0] == strArray1[2])
                {
                  if (strArray2[index].IndexOf(strArray1[0]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[1]) != -1)
                    ++num;
                }
                if (strArray1[1] == strArray1[2])
                {
                  if (strArray2[index].IndexOf(strArray1[0]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                }
              }
            }
            else
            {
              if (strArray2[index].IndexOf(strArray1[0]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[1]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[2]) != -1)
                ++num;
            }
          }
          if (Pos == "C")
          {
            if (strArray1[1] == strArray1[2] || strArray1[2] == strArray1[3] || strArray1[1] == strArray1[3])
            {
              if (strArray1[1] == strArray1[2] && strArray1[1] == strArray1[3])
              {
                if (strArray2[index].IndexOf(strArray1[1]) != -1)
                  ++num;
              }
              else
              {
                if (strArray1[1] == strArray1[2])
                {
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[3]) != -1)
                    ++num;
                }
                if (strArray1[1] == strArray1[3])
                {
                  if (strArray2[index].IndexOf(strArray1[1]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                }
                if (strArray1[2] == strArray1[3])
                {
                  if (strArray2[index].IndexOf(strArray1[1]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[3]) != -1)
                    ++num;
                }
              }
            }
            else
            {
              if (strArray2[index].IndexOf(strArray1[1]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[2]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[3]) != -1)
                ++num;
            }
          }
          if (Pos == "R")
          {
            if (strArray1[2] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[2] == strArray1[4])
            {
              if (strArray1[2] == strArray1[3] && strArray1[2] == strArray1[4])
              {
                if (strArray2[index].IndexOf(strArray1[2]) != -1)
                  ++num;
              }
              else
              {
                if (strArray1[2] == strArray1[3])
                {
                  if (strArray2[index].IndexOf(strArray1[3]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[4]) != -1)
                    ++num;
                }
                if (strArray1[2] == strArray1[4])
                {
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[3]) != -1)
                    ++num;
                }
                if (strArray1[3] == strArray1[4])
                {
                  if (strArray2[index].IndexOf(strArray1[2]) != -1)
                    ++num;
                  if (strArray2[index].IndexOf(strArray1[4]) != -1)
                    ++num;
                }
              }
            }
            else
            {
              if (strArray2[index].IndexOf(strArray1[2]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[3]) != -1)
                ++num;
              if (strArray2[index].IndexOf(strArray1[4]) != -1)
                ++num;
            }
          }
        }
      }
      return num;
    }

    public static int P_DD(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length != 3 && strArray2.Length != 5)
        return 0;
      if (strArray1.Length == 3)
      {
        if (strArray2[0].IndexOf(strArray1[0]) != -1)
          ++num;
        if (strArray2[1].IndexOf(strArray1[1]) != -1)
          ++num;
        if (strArray2[2].IndexOf(strArray1[2]) != -1)
          ++num;
      }
      if (strArray1.Length == 5)
      {
        if (strArray2[0].IndexOf(strArray1[0]) != -1)
          ++num;
        if (strArray2[1].IndexOf(strArray1[1]) != -1)
          ++num;
        if (strArray2[2].IndexOf(strArray1[2]) != -1)
          ++num;
        if (strArray2[3].IndexOf(strArray1[3]) != -1)
          ++num;
        if (strArray2[4].IndexOf(strArray1[4]) != -1)
          ++num;
      }
      return num;
    }

    public static int P_DD2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray2.Length == 1 && strArray2[0].IndexOf(strArray1[0]) != -1)
        ++num;
      if (strArray2.Length == 2 && strArray2[0].IndexOf(strArray1[0]) != -1 && strArray2[1].IndexOf(strArray1[1]) != -1)
        ++num;
      if (strArray2.Length == 3 && strArray2[0].IndexOf(strArray1[0]) != -1 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1))
        ++num;
      return num;
    }

    public static int P_3BDD1(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string str1 = "";
      if (Pos == "L")
        str1 = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      if (Pos == "R")
        str1 = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      string[] strArray2 = str1.Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      foreach (string str2 in (string[]) arrayList.ToArray(typeof (string)))
      {
        if (CheckNumber.Contains(str2))
          ++num;
      }
      return num;
    }

    public static int P_3BDD2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string str = "";
      if (Pos == "L")
        str = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      if (Pos == "R")
        str = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      string[] strArray2 = str.Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      string[] array = (string[]) arrayList.ToArray(typeof (string));
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
        {
          if (CheckNumber.Contains(array[index1]) && CheckNumber.Contains(array[index2]))
            ++num;
        }
      }
      return num;
    }

    public static int P_4BDD1(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = (strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4]).Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      foreach (string str in (string[]) arrayList.ToArray(typeof (string)))
      {
        if (CheckNumber.Contains(str))
          ++num;
      }
      return num;
    }

    public static int P_4BDD2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = (strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4]).Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      string[] array = (string[]) arrayList.ToArray(typeof (string));
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
        {
          if (CheckNumber.Contains(array[index1]) && CheckNumber.Contains(array[index2]))
            ++num;
        }
      }
      return num;
    }

    public static int P_5BDD2(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = (strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4]).Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      string[] array = (string[]) arrayList.ToArray(typeof (string));
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
        {
          if (CheckNumber.Contains(array[index1]) && CheckNumber.Contains(array[index2]))
            ++num;
        }
      }
      return num;
    }

    public static int P_5BDD3(string LotteryNumber, string CheckNumber)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = (strArray1[0] + "," + strArray1[1] + "," + strArray1[2] + "," + strArray1[3] + "," + strArray1[4]).Split(',');
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < strArray2.Length; ++index)
      {
        if (!arrayList.Contains((object) strArray2[index]))
          arrayList.Add((object) strArray2[index]);
      }
      string[] array = (string[]) arrayList.ToArray(typeof (string));
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < array.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < array.Length; ++index3)
          {
            if (CheckNumber.Contains(array[index1]) && CheckNumber.Contains(array[index2]) && CheckNumber.Contains(array[index3]))
              ++num;
          }
        }
      }
      return num;
    }
  }
}
