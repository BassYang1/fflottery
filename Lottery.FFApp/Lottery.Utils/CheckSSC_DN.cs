// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_DN
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;

namespace Lottery.Utils
{
  public static class CheckSSC_DN
  {
    public static int P_Wj(string LotteryNumber, int flag)
    {
      int num1 = 0;
      int num2 = CheckSSC_DN.CheckNNumInt(LotteryNumber);
      int num3 = CheckSSC_DN.CheckNNumInt(CheckSSC_DN.AddDnNum(LotteryNumber, flag));
      if (num3 > num2)
        num1 = num3 != 50 ? (num3 != 40 ? (num3 != 30 ? (num3 < 8 || num3 > 9 ? (num3 < 1 || num3 > 7 ? 0 : 2) : 3) : 4) : 5) : 6;
      return num1;
    }

    public static int CheckNNumInt(string LotteryNumber)
    {
      ArrayList arrayList = new ArrayList();
      string[] strArray1 = LotteryNumber.Split(',');
      string str1 = "0,1,2@0,1,3@0,1,4@0,2,3@0,2,4@0,3,4@1,2,3@1,2,4@1,3,4@2,3,4";
      char[] chArray1 = new char[1]{ '@' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ ',' };
        string[] strArray2 = str2.Split(chArray2);
        for (int index = 0; index < strArray1.Length; ++index)
        {
          arrayList.Add((object) int.Parse(strArray1[0]));
          arrayList.Add((object) int.Parse(strArray1[1]));
          arrayList.Add((object) int.Parse(strArray1[2]));
          arrayList.Add((object) int.Parse(strArray1[3]));
          arrayList.Add((object) int.Parse(strArray1[4]));
          int num = int.Parse(strArray1[0]) + int.Parse(strArray1[1]) + int.Parse(strArray1[2]) + int.Parse(strArray1[3]) + int.Parse(strArray1[4]);
          if (num <= 10)
            return 50;
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 10)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 10) % 10 == 0)
              return 30;
            if (arrayList[0] == arrayList[1])
              return 40;
            return (num - 10) % 10;
          }
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 20)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 20) % 10 == 0)
              return 30;
            if (arrayList[0] == arrayList[1])
              return 40;
            return (num - 20) % 10;
          }
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 30)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 30) % 10 == 0)
              return 30;
            if (arrayList[0] == arrayList[1])
              return 40;
            return (num - 30) % 10;
          }
        }
      }
      return 0;
    }

    public static string AddDnNum(string LotteryNumber, int n)
    {
      string str1 = "";
      string str2 = LotteryNumber;
      char[] chArray = new char[1]{ ',' };
      foreach (string str3 in str2.Split(chArray))
      {
        int num = Convert.ToInt32(str3) + n;
        if (num >= 10)
          num %= 10;
        str1 = str1 + (object) num + ",";
      }
      return str1.Substring(0, str1.Length - 1);
    }

    public static string CheckNNum(string LotteryNumber)
    {
      ArrayList arrayList = new ArrayList();
      string[] strArray1 = LotteryNumber.Split(',');
      string str1 = "0,1,2@0,1,3@0,1,4@0,2,3@0,2,4@0,3,4@1,2,3@1,2,4@1,3,4@2,3,4";
      char[] chArray1 = new char[1]{ '@' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ ',' };
        string[] strArray2 = str2.Split(chArray2);
        for (int index = 0; index < strArray1.Length; ++index)
        {
          arrayList.Add((object) int.Parse(strArray1[0]));
          arrayList.Add((object) int.Parse(strArray1[1]));
          arrayList.Add((object) int.Parse(strArray1[2]));
          arrayList.Add((object) int.Parse(strArray1[3]));
          arrayList.Add((object) int.Parse(strArray1[4]));
          int num = int.Parse(strArray1[0]) + int.Parse(strArray1[1]) + int.Parse(strArray1[2]) + int.Parse(strArray1[3]) + int.Parse(strArray1[4]);
          if (num <= 10)
            return "五小";
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 10)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 10) % 10 == 0)
              return "牛牛";
            if (arrayList[0] == arrayList[1])
              return "牛对子";
            return "牛" + (object) ((num - 10) % 10);
          }
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 20)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 20) % 10 == 0)
              return "牛牛";
            if (arrayList[0] == arrayList[1])
              return "牛对子";
            return "牛" + (object) ((num - 20) % 10);
          }
          if (int.Parse(strArray1[int.Parse(strArray2[0])]) + int.Parse(strArray1[int.Parse(strArray2[1])]) + int.Parse(strArray1[int.Parse(strArray2[2])]) == 30)
          {
            arrayList.RemoveAt(int.Parse(strArray2[2]));
            arrayList.RemoveAt(int.Parse(strArray2[1]));
            arrayList.RemoveAt(int.Parse(strArray2[0]));
            arrayList.Sort();
            if ((num - 30) % 10 == 0)
              return "牛牛";
            if (arrayList[0] == arrayList[1])
              return "牛对子";
            return "牛" + (object) ((num - 30) % 10);
          }
        }
      }
      return "无牛";
    }

    private static int SubstringCount(string str, string substring)
    {
      string[] strArray = substring.Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (str.Contains(strArray[index]))
        {
          string str1 = str.Replace(strArray[index], "");
          return (str.Length - str1.Length) / strArray[index].Length;
        }
      }
      return 0;
    }
  }
}
