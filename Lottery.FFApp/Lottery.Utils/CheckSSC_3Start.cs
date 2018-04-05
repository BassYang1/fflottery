// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckSSC_3Start
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lottery.Utils
{
  public static class CheckSSC_3Start
  {
    public static int P_3ZX(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      if (strArray3.Length >= 3 && strArray3[0].IndexOf(strArray2[0]) != -1 && (strArray3[1].IndexOf(strArray2[1]) != -1 && strArray3[2].IndexOf(strArray2[2]) != -1))
        ++num;
      return num;
    }

    public static int P_3DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
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

    public static int P_3Z3(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      if ((strArray2[0] == strArray2[1] || strArray2[1] == strArray2[2] || strArray2[0] == strArray2[2]) && (!(strArray2[0] == strArray2[1]) || !(strArray2[0] == strArray2[2]) || !(strArray2[1] == strArray2[2])))
      {
        string[] strArray3 = CheckNumber.Split(',');
        for (int index = 0; index < strArray3.Length; ++index)
        {
          if (strArray2[0] == strArray2[1])
          {
            if (strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[2]) != -1)
              ++num;
          }
          else if (strArray2[0] == strArray2[2])
          {
            if (strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[1]) != -1)
              ++num;
          }
          else if (strArray2[1] == strArray2[2] && strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[1]) != -1)
            ++num;
        }
      }
      return num;
    }

    public static int P_3Z3_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray3.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          for (int index3 = 0; index3 < strArray3.Length; ++index3)
          {
            if (index1 != index2 && index2 != index3 && index1 != index3 && (strArray3[index1] == strArray3[index2] || strArray3[index2] == strArray3[index3] || strArray3[index1] == strArray3[index3]) && (!(strArray3[index1] == strArray3[index2]) || !(strArray3[index2] == strArray3[index3]) || !(strArray3[index1] == strArray3[index3])))
            {
              string str = strArray3[index1] + strArray3[index2] + strArray3[index3];
              if (!hashtable.Contains((object) str))
              {
                hashtable.Add((object) str, (object) str);
                for (int index4 = 0; index4 < strArray2.Length; ++index4)
                {
                  if (str == strArray2[index4].Replace(",", ""))
                    ++num;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int P_3Z6(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      if (strArray2[0] != strArray2[1] && strArray2[0] != strArray2[2] && strArray2[1] != strArray2[2])
      {
        string[] strArray3 = CheckNumber.Split(',');
        for (int index = 0; index < strArray3.Length; ++index)
        {
          if (strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[1]) != -1 && strArray3[index].IndexOf(strArray2[2]) != -1)
            ++num;
        }
      }
      return num;
    }

    public static int P_3Z6_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray3.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray3.Length; ++index2)
        {
          for (int index3 = 0; index3 < strArray3.Length; ++index3)
          {
            if (index1 != index2 && index2 != index3 && index1 != index3)
            {
              string str = strArray3[index1] + strArray3[index2] + strArray3[index3];
              if (!hashtable.Contains((object) str))
              {
                hashtable.Add((object) str, (object) str);
                for (int index4 = 0; index4 < strArray2.Length; ++index4)
                {
                  if (str == strArray2[index4].Replace(",", ""))
                    ++num;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int P_3HX(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray2 = LotteryNumber.Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      for (int index = 0; index < strArray3.Length; ++index)
      {
        if (strArray3[index].Length != 3)
          return 0;
        if (strArray2[0] != strArray2[1] && strArray2[1] != strArray2[2] && strArray2[0] != strArray2[2])
        {
          if (strArray3[index].IndexOf(strArray2[0]) != -1 && strArray3[index].IndexOf(strArray2[1]) != -1 && strArray3[index].IndexOf(strArray2[2]) != -1)
            ++num;
        }
        else if (strArray2[0] == strArray2[1])
        {
          if (Func.SearchStrNum(strArray3[index], strArray2[1]) == 2 && Func.SearchStrNum(strArray3[index], strArray2[2]) == 1)
            ++num;
        }
        else if (strArray2[1] == strArray2[2])
        {
          if (Func.SearchStrNum(strArray3[index], strArray2[0]) == 1 && Func.SearchStrNum(strArray3[index], strArray2[1]) == 2)
            ++num;
        }
        else if (Func.SearchStrNum(strArray3[index], strArray2[0]) == 2 && Func.SearchStrNum(strArray3[index], strArray2[1]) == 1)
          ++num;
      }
      return num;
    }

    public static int R_3FS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string str = "";
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        str = str + strArray1[int32] + ",";
      }
      string[] strArray4 = str.Substring(0, str.Length - 1).Split(',');
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray4.Length; ++index3)
          {
            string[] strArray5 = (strArray4[index1] + "," + strArray4[index2] + "," + strArray4[index3]).Split(',');
            if (strArray2[0].Contains(strArray5[0]) && strArray2[1].Contains(strArray5[1]) && strArray2[2].Contains(strArray5[2]))
              ++num;
          }
        }
      }
      return num;
    }

    public static int R_3DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string str1 = "";
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        str1 = str1 + strArray1[int32] + ",";
      }
      string[] strArray4 = str1.Substring(0, str1.Length - 1).Split(',');
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray4.Length; ++index3)
          {
            string str2 = strArray4[index1] + strArray4[index2] + strArray4[index3];
            for (int index4 = 0; index4 < strArray2.Length; ++index4)
            {
              if (str2 == strArray2[index4].Replace(",", ""))
                ++num;
            }
          }
        }
      }
      return num;
    }

    public static int R_3Z3_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string[] strArray4 = new string[strArray3.Length];
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        strArray4[index] = strArray1[int32];
      }
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = 0; index3 < strArray4.Length; ++index3)
          {
            if ((strArray4[index1] == strArray4[index2] || strArray4[index2] == strArray4[index3] || strArray4[index1] == strArray4[index3]) && (!(strArray4[index1] == strArray4[index2]) || !(strArray4[index2] == strArray4[index3]) || !(strArray4[index1] == strArray4[index3])))
            {
              string str = strArray4[index1] + strArray4[index2] + strArray4[index3];
              if (!hashtable.Contains((object) str))
              {
                hashtable.Add((object) str, (object) str);
                for (int index4 = 0; index4 < strArray2.Length; ++index4)
                {
                  if (str == strArray2[index4].Replace(",", ""))
                    ++num;
                }
              }
            }
          }
        }
      }
      return num;
    }

    public static int R_3Z3(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string[] strArray4 = new string[strArray3.Length];
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        strArray4[index] = strArray1[int32];
      }
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray4.Length; ++index3)
          {
            if ((strArray4[index1] == strArray4[index2] || strArray4[index2] == strArray4[index3] || strArray4[index1] == strArray4[index3]) && (!(strArray4[index1] == strArray4[index2]) || !(strArray4[index2] == strArray4[index3]) || !(strArray4[index1] == strArray4[index3])))
            {
              for (int index4 = 0; index4 < strArray2.Length; ++index4)
              {
                if (strArray2[index4].IndexOf(strArray4[index1]) != -1 && strArray2[index4].IndexOf(strArray4[index2]) != -1 && strArray2[index4].IndexOf(strArray4[index3]) != -1)
                  ++num;
              }
            }
          }
        }
      }
      return num;
    }

    public static int R_3Z6(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string[] strArray4 = new string[strArray3.Length];
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        strArray4[index] = strArray1[int32];
      }
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray4.Length; ++index3)
          {
            if (index1 != index2 && index1 != index3 && index2 != index3 && (!(strArray4[index1] == strArray4[index2]) || !(strArray4[index2] == strArray4[index3]) || !(strArray4[index1] == strArray4[index3])) && (strArray4[index1] != strArray4[index2] && strArray4[index1] != strArray4[index3] && strArray4[index2] != strArray4[index3]))
            {
              for (int index4 = 0; index4 < strArray2.Length; ++index4)
              {
                if (strArray2[index4].IndexOf(strArray4[index1]) != -1 && strArray2[index4].IndexOf(strArray4[index2]) != -1 && strArray2[index4].IndexOf(strArray4[index3]) != -1)
                  ++num;
              }
            }
          }
        }
      }
      return num;
    }

    public static int R_3Z6_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      string[] strArray3 = Pos.Split(',');
      string[] strArray4 = new string[strArray3.Length];
      for (int index = 0; index < strArray3.Length; ++index)
      {
        int int32 = Convert.ToInt32(strArray3[index]);
        strArray4[index] = strArray1[int32];
      }
      Hashtable hashtable = new Hashtable();
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = index1 + 1; index2 < strArray4.Length; ++index2)
        {
          for (int index3 = index2 + 1; index3 < strArray4.Length; ++index3)
          {
            if ((!(strArray4[index1] == strArray4[index2]) || !(strArray4[index2] == strArray4[index3]) || !(strArray4[index1] == strArray4[index3])) && (strArray4[index1] != strArray4[index2] && strArray4[index1] != strArray4[index3] && strArray4[index2] != strArray4[index3]))
            {
              string str = strArray4[index1] + strArray4[index2] + strArray4[index3];
              for (int index4 = 0; index4 < strArray2.Length; ++index4)
              {
                if (str == strArray2[index4].Replace(",", ""))
                  ++num;
              }
            }
          }
        }
      }
      return num;
    }

    public static int P_3HE(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray = LotteryNumber.Split(',');
      int num2 = 0;
      if (strArray.Length == 3)
      {
        num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
      }
      else
      {
        if (Pos == "L")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        if (Pos == "C")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "R")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WQB")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        if (Pos == "WQS")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]);
        if (Pos == "WQG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WBS")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "WBG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WSG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "QBS")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "QBG")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        if (Pos == "QSG")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "BSG")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
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

    public static int P_3KD(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
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

    public static int P_3Z3DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      if (strArray3[0] == strArray3[1])
      {
        if (CheckNumber.Contains(strArray3[0] + strArray3[0] + strArray3[2]))
          ++num;
        if (CheckNumber.Contains(strArray3[0] + strArray3[2] + strArray3[0]))
          ++num;
        if (CheckNumber.Contains(strArray3[2] + strArray3[0] + strArray3[0]))
          ++num;
      }
      if (strArray3[0] == strArray3[2])
      {
        if (CheckNumber.Contains(strArray3[0] + strArray3[0] + strArray3[1]))
          ++num;
        if (CheckNumber.Contains(strArray3[0] + strArray3[1] + strArray3[0]))
          ++num;
        if (CheckNumber.Contains(strArray3[1] + strArray3[0] + strArray3[0]))
          ++num;
      }
      if (strArray3[1] == strArray3[2])
      {
        if (CheckNumber.Contains(strArray3[1] + strArray3[1] + strArray3[0]))
          ++num;
        if (CheckNumber.Contains(strArray3[1] + strArray3[0] + strArray3[1]))
          ++num;
        if (CheckNumber.Contains(strArray3[0] + strArray3[1] + strArray3[1]))
          ++num;
      }
      return num;
    }

    public static int P_3Z6DS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      string[] strArray4 = CheckNumber.Split(',');
      if (strArray3[0] != strArray3[1] && strArray3[0] != strArray3[2] && strArray3[1] != strArray3[2])
      {
        for (int index = 0; index < strArray4.Length; ++index)
        {
          if (strArray4[index].Contains(strArray3[0]) && strArray4[index].Contains(strArray3[1]) && strArray4[index].Contains(strArray3[2]))
            ++num;
        }
      }
      return num;
    }

    public static int P_3QTWS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num1 = 0;
      string[] strArray = LotteryNumber.Split(',');
      int num2 = 0;
      if (strArray.Length == 3)
      {
        num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
      }
      else
      {
        if (Pos == "L")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        if (Pos == "C")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "R")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WQB")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]);
        if (Pos == "WQS")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]);
        if (Pos == "WQG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WBS")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "WBG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        if (Pos == "WSG")
          num2 = Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "QBS")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]);
        if (Pos == "QBG")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[4]);
        if (Pos == "QSG")
          num2 = Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
        if (Pos == "BSG")
          num2 = Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]);
      }
      string str1 = CheckNumber;
      char[] chArray = new char[1]{ '_' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (Convert.ToInt32(str2) == num2 % 10)
          ++num1;
      }
      return num1;
    }

    public static int P_3QTTS(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      if (strArray3[0] == strArray3[1] && strArray3[0] == strArray3[2] && (strArray3[1] == strArray3[2] && CheckNumber.Contains("豹子")))
        ++num;
      if ((strArray3[0] == strArray3[1] && strArray3[0] != strArray3[2] || strArray3[0] == strArray3[2] && strArray3[0] != strArray3[1] || strArray3[1] == strArray3[2] && strArray3[0] != strArray3[2]) && CheckNumber.Contains("对子"))
        ++num;
      if (Convert.ToInt32(strArray3[0]) + 1 == Convert.ToInt32(strArray3[1]) && Convert.ToInt32(strArray3[1]) + 1 == Convert.ToInt32(strArray3[2]) && CheckNumber.Contains("顺子"))
        ++num;
      return num;
    }

    public static int P_3ZBDZ3(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      if (strArray3[0] == strArray3[1] && strArray3[0] == strArray3[2])
        return 0;
      if (strArray3[0] == strArray3[1] || strArray3[0] == strArray3[2] || strArray3[1] == strArray3[2])
      {
        if ((strArray3[0] + "," + strArray3[1] + "," + strArray3[2]).Contains(CheckNumber))
          ++num;
      }
      return num;
    }

    public static int P_3ZBDZ6(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = new string[3];
      if (strArray1.Length == 3)
      {
        LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
      }
      else
      {
        if (Pos == "L")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "C")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "R")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "WQB")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[2];
        if (Pos == "WQS")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[3];
        if (Pos == "WQG")
          LotteryNumber = strArray1[0] + "," + strArray1[1] + "," + strArray1[4];
        if (Pos == "WBS")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "WBG")
          LotteryNumber = strArray1[0] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "WSG")
          LotteryNumber = strArray1[0] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "QBS")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[3];
        if (Pos == "QBG")
          LotteryNumber = strArray1[1] + "," + strArray1[2] + "," + strArray1[4];
        if (Pos == "QSG")
          LotteryNumber = strArray1[1] + "," + strArray1[3] + "," + strArray1[4];
        if (Pos == "BSG")
          LotteryNumber = strArray1[2] + "," + strArray1[3] + "," + strArray1[4];
      }
      string[] strArray3 = LotteryNumber.Split(',');
      if (strArray3[0] == strArray3[1] && strArray3[0] == strArray3[2])
        return 0;
      if (strArray3[0] != strArray3[1] && strArray3[0] != strArray3[2] && strArray3[1] != strArray3[2])
      {
        if ((strArray3[0] + "," + strArray3[1] + "," + strArray3[2]).Contains(CheckNumber))
          ++num;
      }
      return num;
    }

    public static int P_3ZH_3(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 3 && (strArray2[0].IndexOf(strArray1[0]) != -1 && strArray2[1].IndexOf(strArray1[1]) != -1) && strArray2[2].IndexOf(strArray1[2]) != -1)
        ++num;
      if (Pos == "C" && strArray2.Length >= 3 && (strArray2[0].IndexOf(strArray1[1]) != -1 && strArray2[1].IndexOf(strArray1[2]) != -1) && strArray2[2].IndexOf(strArray1[3]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length >= 3 && (strArray2[0].IndexOf(strArray1[2]) != -1 && strArray2[1].IndexOf(strArray1[3]) != -1) && strArray2[2].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }

    public static int P_3ZH_2(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 3 && (strArray2[1].IndexOf(strArray1[1]) != -1 && strArray2[2].IndexOf(strArray1[2]) != -1))
        ++num;
      if (Pos == "C" && strArray2.Length >= 3 && (strArray2[1].IndexOf(strArray1[2]) != -1 && strArray2[2].IndexOf(strArray1[3]) != -1))
        ++num;
      if (Pos == "R" && strArray2.Length >= 3 && (strArray2[1].IndexOf(strArray1[3]) != -1 && strArray2[2].IndexOf(strArray1[4]) != -1))
        ++num;
      return num;
    }

    public static int P_3ZH_1(string LotteryNumber, string CheckNumber, string Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      string[] strArray2 = CheckNumber.Split(',');
      if (Pos == "L" && strArray2.Length >= 3 && strArray2[2].IndexOf(strArray1[2]) != -1)
        ++num;
      if (Pos == "C" && strArray2.Length >= 3 && strArray2[2].IndexOf(strArray1[3]) != -1)
        ++num;
      if (Pos == "R" && strArray2.Length >= 3 && strArray2[2].IndexOf(strArray1[4]) != -1)
        ++num;
      return num;
    }
  }
}
