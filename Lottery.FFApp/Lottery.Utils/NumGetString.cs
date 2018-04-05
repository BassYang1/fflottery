// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.NumGetString
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public class NumGetString
  {
    private static string[] Ls_ShZ = new string[11]
    {
      "零",
      "壹",
      "贰",
      "叁",
      "肆",
      "伍",
      "陆",
      "柒",
      "捌",
      "玖",
      "拾"
    };
    private static string[] Ls_DW_Zh = new string[13]
    {
      "元",
      "拾",
      "佰",
      "仟",
      "万",
      "拾",
      "佰",
      "仟",
      "亿",
      "拾",
      "佰",
      "仟",
      "万"
    };
    private static string[] Num_DW = new string[13]
    {
      "",
      "拾",
      "佰",
      "仟",
      "万",
      "拾",
      "佰",
      "仟",
      "亿",
      "拾",
      "佰",
      "仟",
      "万"
    };
    private static string[] Ls_DW_X = new string[2]
    {
      "角",
      "分"
    };

    public static string NumGetStr(double Num)
    {
      bool flag1 = false;
      bool flag2 = true;
      string s1 = "";
      string str1 = "";
      Num = Math.Round(Num, 2);
      if (Num < 0.0)
        return "不转换欠条";
      if (Num > 9999999999999.99)
        return "很难想象谁会有这么多钱！";
      if (Num == 0.0)
        return NumGetString.Ls_ShZ[0];
      if (Num < 1.0)
        flag2 = false;
      string str2 = Num.ToString();
      string Rstr = str2;
      if (Rstr.Contains("."))
      {
        Rstr = str2.Substring(0, str2.IndexOf("."));
        s1 = str2.Substring(str2.IndexOf(".") + 1, str2.Length - str2.IndexOf(".") - 1);
        flag1 = true;
      }
      if (s1 == "" || int.Parse(s1) <= 0)
        flag1 = false;
      if (flag2)
      {
        string str3 = NumGetString.Reversion_Str(Rstr);
        for (int startIndex = 0; startIndex < str3.Length; ++startIndex)
        {
          string s2 = str3.Substring(startIndex, 1);
          if (int.Parse(s2) != 0)
            str1 = NumGetString.Ls_ShZ[int.Parse(s2)] + NumGetString.Ls_DW_Zh[startIndex] + str1;
          else if (startIndex == 0 || startIndex == 4 || startIndex == 8)
          {
            if (str3.Length <= 8 || startIndex != 4)
              str1 = NumGetString.Ls_DW_Zh[startIndex] + str1;
          }
          else if (int.Parse(str3.Substring(startIndex - 1, 1)) != 0)
            str1 = NumGetString.Ls_ShZ[int.Parse(s2)] + str1;
        }
        if (!flag1)
          return str1 + "整";
      }
      for (int startIndex = 0; startIndex < s1.Length; ++startIndex)
      {
        string s2 = s1.Substring(startIndex, 1);
        if (int.Parse(s2) != 0)
          str1 = str1 + NumGetString.Ls_ShZ[int.Parse(s2)] + NumGetString.Ls_DW_X[startIndex];
        else if (startIndex != 1 && flag2)
          str1 += NumGetString.Ls_ShZ[int.Parse(s2)];
      }
      return str1;
    }

    public static string LowercaseGetCap(string NumStr, bool Dw)
    {
      string str = "";
      if (NumStr == string.Empty)
        return string.Empty;
      if (Dw)
        NumStr = NumGetString.Reversion_Str(NumStr);
      try
      {
        for (int startIndex = 0; startIndex < NumStr.Length; ++startIndex)
        {
          string s = NumStr.Substring(startIndex, 1);
          if (Dw)
          {
            if (int.Parse(s) != 0)
              str = NumGetString.Ls_ShZ[int.Parse(s)] + NumGetString.Num_DW[startIndex] + str;
            else if (startIndex == 0 || startIndex == 4 || startIndex == 8)
            {
              if (s.Length <= 8 || startIndex != 4)
                str = NumGetString.Num_DW[startIndex] + str;
            }
            else if (int.Parse(NumStr.Substring(startIndex - 1, 1)) != 0)
              str = NumGetString.Ls_ShZ[int.Parse(s)] + str;
          }
          else
            str += NumGetString.Ls_ShZ[int.Parse(s)];
        }
        return str;
      }
      catch (Exception ex)
      {
        return "转换错误！" + ex.Message;
      }
    }

    private static string Reversion_Str(string Rstr)
    {
      char[] charArray = Rstr.ToCharArray();
      Array.Reverse((Array) charArray);
      return new string(charArray);
    }
  }
}
