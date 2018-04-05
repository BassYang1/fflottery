// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.NumberCode
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Lottery.Utils
{
  public class NumberCode
  {
    public static string[] CreateCode20()
    {
      string[] randomNumNoRepeat = NumberCode.getRandomNumNoRepeat(20, 1, 99);
      NumberCode.SortByCount(randomNumNoRepeat);
      return randomNumNoRepeat;
    }

    public static string CreateCodeNew(int codeLength)
    {
      string str = "";
      Random random = new Random((int) DateTime.Now.Ticks);
      for (int index = 0; index <= codeLength - 1; ++index)
      {
        int num = random.Next(100, 199);
        str += (string) (object) (num % 100);
        if (index != codeLength - 1)
          str += ",";
      }
      return str;
    }

    public static string CreateCode(int codeLength)
    {
      string str1 = "";
      Random random = new Random();
      for (int index = 0; index < codeLength + 5; ++index)
      {
        str1 += random.Next().ToString();
        if (str1.Length > 10)
          break;
      }
      string str2 = str1.Substring(random.Next(0, 4), codeLength);
      for (int index = 0; index < str2.Length; ++index)
        str1 = index != 0 ? str1 + "," + (object) str2[index] : str2[index].ToString();
      return str1;
    }

    public static string CreateCode11X5(int codeLength)
    {
      string str = "";
      Hashtable hashtable = new Hashtable();
      Random random = new Random();
      int num1 = 5;
      int num2 = 0;
      while (hashtable.Count < num1)
      {
        int num3 = random.Next(12);
        if (!hashtable.ContainsValue((object) num3) && num3 != 0)
          hashtable.Add((object) num3, (object) num3);
        ++num2;
      }
      foreach (DictionaryEntry dictionaryEntry in hashtable)
        str = dictionaryEntry.Value.ToString().Length >= 2 ? str + dictionaryEntry.Value.ToString() + "," : str + "0" + dictionaryEntry.Value.ToString() + ",";
      return str.Substring(0, str.Length - 1);
    }

    public static string CreateCode20(int codeLength)
    {
      string str = "";
      Hashtable hashtable = new Hashtable();
      Random random = new Random();
      int num1 = 20;
      int num2 = 0;
      while (hashtable.Count < num1)
      {
        int num3 = random.Next(99);
        if (!hashtable.ContainsValue((object) num3) && num3 != 0)
          hashtable.Add((object) num3, (object) num3);
        ++num2;
      }
      foreach (DictionaryEntry dictionaryEntry in hashtable)
        str = dictionaryEntry.Value.ToString().Length >= 2 ? str + dictionaryEntry.Value.ToString() + "," : str + "0" + dictionaryEntry.Value.ToString() + ",";
      return str.Substring(0, str.Length - 1);
    }

    public static string CreateCodePk10(int codeLength)
    {
      string[] strArray = new string[10]
      {
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10"
      };
      List<string> myList = new List<string>();
      for (int index = 0; index < strArray.Length; ++index)
        myList.Add(strArray[index]);
      return string.Join(",", NumberCode.ListRandom(myList).ToArray());
    }

    private static List<string> ListRandom(List<string> myList)
    {
      Random random = new Random();
      for (int index1 = 0; index1 < myList.Count; ++index1)
      {
        int index2 = random.Next(0, myList.Count - 1);
        if (index2 != index1)
        {
          string my = myList[index1];
          myList[index1] = myList[index2];
          myList[index2] = my;
        }
      }
      return myList;
    }

    public static string CreateCodeDN(int codeLength)
    {
      string[] strArray = new string[40]
      {
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10"
      };
      string str = "";
      Hashtable hashtable1 = new Hashtable();
      Hashtable hashtable2 = new Hashtable();
      Random random = new Random();
      int num1 = 5;
      int num2 = 0;
      while (hashtable1.Count < num1)
      {
        int index = random.Next(0, 39);
        if (!hashtable2.ContainsValue((object) index) && index != 0)
        {
          hashtable1.Add((object) index, (object) strArray[index]);
          hashtable2.Add((object) index, (object) index);
        }
        ++num2;
      }
      foreach (DictionaryEntry dictionaryEntry in hashtable1)
        str = dictionaryEntry.Value.ToString().Length >= 2 ? str + dictionaryEntry.Value.ToString() + "," : str + "0" + dictionaryEntry.Value.ToString() + ",";
      return str.Substring(0, str.Length - 1);
    }

    public int[] getRandomNum(int num, int minValue, int maxValue)
    {
      Random random = new Random((int) DateTime.Now.Ticks);
      int[] numArray = new int[num];
      for (int index = 0; index <= num - 1; ++index)
      {
        int num1 = random.Next(minValue, maxValue);
        numArray[index] = num1;
      }
      return numArray;
    }

    public static string[] getRandomNumNoRepeat(int num, int minValue, int maxValue)
    {
      Random ra = new Random((int) DateTime.Now.Ticks);
      int[] arrNum = new int[num];
      string[] strArray = new string[num];
      for (int index = 0; index <= num - 1; ++index)
      {
        int tmp = ra.Next(minValue, maxValue);
        int num1 = NumberCode.getNum(arrNum, tmp, minValue, maxValue, ra);
        arrNum[index] = num1;
        strArray[index] = num1 >= 10 ? string.Concat((object) num1) : "0" + (object) num1;
      }
      return strArray;
    }

    public static int getNum(int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
    {
      for (int index = 0; index <= arrNum.Length - 1; ++index)
      {
        if (arrNum[index] == tmp)
        {
          tmp = ra.Next(minValue, maxValue);
          NumberCode.getNum(arrNum, tmp, minValue, maxValue, ra);
        }
      }
      return tmp;
    }

    public static void SortByCount(string[] source)
    {
      Comparison<string> comparison = new Comparison<string>(NumberCode.function);
      Array.Sort<string>(source, comparison);
    }

    private static int function(string s1, string s2)
    {
      if (Convert.ToInt32(s1) - Convert.ToInt32(s2) != 0)
        return Convert.ToInt32(s1) - Convert.ToInt32(s2);
      return string.Compare(s1, s2);
    }
  }
}
