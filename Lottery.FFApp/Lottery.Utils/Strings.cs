// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Strings
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class Strings
  {
    public static string[] aryChar = new string[16]
    {
      "\\",
      "^",
      "$",
      "{",
      "}",
      "[",
      "]",
      ".",
      "(",
      ")",
      "*",
      "+",
      "?",
      "!",
      "#",
      "|"
    };

    public static string EncryptStr(string rs)
    {
      byte[] numArray = new byte[rs.Length];
      for (int index = 0; index <= rs.Length - 1; ++index)
        numArray[index] = (byte) ((uint) (byte) rs[index] + 1U);
      rs = "";
      for (int index = numArray.Length - 1; index >= 0; --index)
        rs += ((char) numArray[index]).ToString();
      return rs;
    }

    public static string DecryptStr(string rs)
    {
      byte[] numArray = new byte[rs.Length];
      for (int index = 0; index <= rs.Length - 1; ++index)
        numArray[index] = (byte) ((uint) (byte) rs[index] - 1U);
      rs = "";
      for (int index = numArray.Length - 1; index >= 0; --index)
        rs += ((char) numArray[index]).ToString();
      return rs;
    }

    public static string Escape(string str)
    {
      if (str == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      int length = str.Length;
      for (int index = 0; index < length; ++index)
      {
        char ch = str[index];
        if (char.IsLetterOrDigit(ch) || (int) ch == 45 || ((int) ch == 95 || (int) ch == 47) || ((int) ch == 92 || (int) ch == 46))
          stringBuilder.Append(ch);
        else
          stringBuilder.Append(Uri.HexEscape(ch));
      }
      return stringBuilder.ToString();
    }

    public static string UnEscape(string str)
    {
      if (str == null)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      int length = str.Length;
      int index = 0;
      while (index != length)
      {
        if (Uri.IsHexEncoding(str, index))
          stringBuilder.Append(Uri.HexUnescape(str, ref index));
        else
          stringBuilder.Append(str[index++]);
      }
      return stringBuilder.ToString();
    }

    public static string PadLeft(string inputString)
    {
      return "," + inputString + ",";
    }

    public static string Left(string inputString, int len)
    {
      if (inputString.Length < len)
        return inputString;
      return inputString.Substring(0, len);
    }

    public static string Right(string inputString, int len)
    {
      if (inputString.Length < len)
        return inputString;
      return inputString.Substring(inputString.Length - len, len);
    }

    public static string CutString(string inputString, int len)
    {
      ASCIIEncoding asciiEncoding = new ASCIIEncoding();
      int num = 0;
      string str = "";
      byte[] bytes = asciiEncoding.GetBytes(inputString);
      for (int startIndex = 0; startIndex < bytes.Length; ++startIndex)
      {
        if ((int) bytes[startIndex] == 63)
          num += 2;
        else
          ++num;
        try
        {
          str += inputString.Substring(startIndex, 1);
        }
        catch
        {
          break;
        }
        if (num >= len)
          break;
      }
      return str;
    }

    public static string RemoveSpaceStr(string original)
    {
      return Regex.Replace(original, "\\s{2,}", " ");
    }

    public static string ToSummary(string Htmlstring)
    {
      return Strings.RemoveSpaceStr(Strings.NoHTML(Htmlstring)).Replace("[Jumbot_PageBreak]", " ");
    }

    public static string NoHTML(string Htmlstring)
    {
      Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&ldquo;", "“", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&rdquo;", "”", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
      Htmlstring = Htmlstring.Replace("<", "&lt;");
      Htmlstring = Htmlstring.Replace(">", "&gt;");
      return Htmlstring;
    }

    public static string ReplaceEx(string original, string pattern, string replacement)
    {
      int num1;
      int num2 = num1 = 0;
      int startIndex = num1;
      int length = num1;
      string upper1 = original.ToUpper();
      string upper2 = pattern.ToUpper();
      int val2 = original.Length / pattern.Length * (replacement.Length - pattern.Length);
      char[] chArray = new char[original.Length + Math.Max(0, val2)];
      int num3;
      for (; (num3 = upper1.IndexOf(upper2, startIndex)) != -1; startIndex = num3 + pattern.Length)
      {
        for (int index = startIndex; index < num3; ++index)
          chArray[length++] = original[index];
        for (int index = 0; index < replacement.Length; ++index)
          chArray[length++] = replacement[index];
      }
      if (startIndex == 0)
        return original;
      for (int index = startIndex; index < original.Length; ++index)
        chArray[length++] = original[index];
      return new string(chArray, 0, length);
    }

    public static string HtmlEncode(string theString)
    {
      theString = theString.Replace(">", "&gt;");
      theString = theString.Replace("<", "&lt;");
      theString = theString.Replace("  ", " &nbsp;");
      theString = theString.Replace("\"", "&quot;");
      theString = theString.Replace("'", "&#39;");
      theString = theString.Replace("\r\n", "<br/> ");
      return theString;
    }

    public static string HtmlDecode(string theString)
    {
      theString = theString.Replace("&gt;", ">");
      theString = theString.Replace("&lt;", "<");
      theString = theString.Replace(" &nbsp;", "  ");
      theString = theString.Replace("&quot;", "\"");
      theString = theString.Replace("&#39;", "'");
      theString = theString.Replace("<br/> ", "\r\n");
      theString = theString.Replace("&mdash;", "—");
      return theString;
    }

    public static string ToMoney(double _value)
    {
      return string.Format("{0:F2}", (object) _value);
    }

    public static string ToMoney(string _value)
    {
      return string.Format("{0:F2}", (object) Convert.ToDouble(_value));
    }

    public static string ToMoney(int _value)
    {
      return string.Format("{0:F2}", (object) Convert.ToDouble(_value));
    }

    public static string ToSBC(string input)
    {
      char[] charArray = input.ToCharArray();
      for (int index = 0; index < charArray.Length; ++index)
      {
        if ((int) charArray[index] == 32)
          charArray[index] = '　';
        else if ((int) charArray[index] < (int) sbyte.MaxValue)
          charArray[index] = (char) ((uint) charArray[index] + 65248U);
      }
      return new string(charArray);
    }

    public static string ToDBC(string input)
    {
      char[] charArray = input.ToCharArray();
      for (int index = 0; index < charArray.Length; ++index)
      {
        if ((int) charArray[index] == 12288)
          charArray[index] = ' ';
        else if ((int) charArray[index] > 65280 && (int) charArray[index] < 65375)
          charArray[index] = (char) ((uint) charArray[index] - 65248U);
      }
      return new string(charArray);
    }

    public static string SimpleLineSummary(string theString)
    {
      theString = theString.Replace("&gt;", "");
      theString = theString.Replace("&lt;", "");
      theString = theString.Replace(" &nbsp;", "  ");
      theString = theString.Replace("&quot;", "\"");
      theString = theString.Replace("&#39;", "'");
      theString = theString.Replace("<br/> ", "\r\n");
      theString = theString.Replace("\"", "");
      theString = theString.Replace("\t", " ");
      theString = theString.Replace("\r", " ");
      theString = theString.Replace("\n", " ");
      theString = Regex.Replace(theString, "\\s{2,}", " ");
      return theString;
    }

    public static string UBB2HTML(string content)
    {
      content = Regex.Replace(content, "\\[b\\](.+?)\\[/b\\]", "<b>$1</b>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[i\\](.+?)\\[/i\\]", "<i>$1</i>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[u\\](.+?)\\[/u\\]", "<u>$1</u>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[p\\](.+?)\\[/p\\]", "<p>$1</p>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[align=left\\](.+?)\\[/align\\]", "<align='left'>$1</align>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[align=center\\](.+?)\\[/align\\]", "<align='center'>$1</align>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[align=right\\](.+?)\\[/align\\]", "<align='right'>$1</align>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[url=(?<url>.+?)]\\[/url]", "<a href='${url}' target=_blank>${url}</a>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[url=(?<url>.+?)](?<name>.+?)\\[/url]", "<a href='${url}' target=_blank>${name}</a>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[quote](?<text>.+?)\\[/quote]", "<div class=\"quote\">${text}</div>", RegexOptions.IgnoreCase);
      content = Regex.Replace(content, "\\[img](?<img>.+?)\\[/img]", "<a href='${img}' target=_blank><img src='${img}' alt=''/></a>", RegexOptions.IgnoreCase);
      return content;
    }

    public static string Html2Js(string source)
    {
      return string.Format("document.write(\"{0}\");", (object) string.Join("\");\r\ndocument.write(\"", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Split(new char[2]
      {
        '\r',
        '\n'
      }, StringSplitOptions.RemoveEmptyEntries)));
    }

    public static string Html2JsStr(string source)
    {
      return string.Format("{0}", (object) string.Join(" ", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\t", "").Split(new char[2]
      {
        '\r',
        '\n'
      }, StringSplitOptions.RemoveEmptyEntries)));
    }

    public static string FilterSymbol(string theString)
    {
      string[] strArray = new string[24]
      {
        "'",
        "\"",
        "\r",
        "\n",
        "<",
        ">",
        "(",
        ")",
        "{",
        "}",
        "%",
        "?",
        ",",
        ".",
        "=",
        "+",
        "-",
        "_",
        ";",
        "|",
        "[",
        "]",
        "&",
        "/"
      };
      foreach (string oldValue in strArray)
        theString = theString.Replace(oldValue, string.Empty);
      return theString;
    }

    public static string DelSymbol(string theString)
    {
      string[] strArray = new string[16]
      {
        "'",
        "\"",
        "\r",
        "\n",
        "<",
        ">",
        "%",
        "?",
        "=",
        "-",
        "_",
        "|",
        "[",
        "]",
        "&",
        "/"
      };
      foreach (string oldValue in strArray)
        theString = theString.Replace(oldValue, string.Empty);
      return theString;
    }

    public static string SafetyTitle(string theString)
    {
      string[] strArray = new string[5]
      {
        "'",
        ";",
        "\"",
        "\r",
        "\n"
      };
      foreach (string oldValue in strArray)
        theString = theString.Replace(oldValue, string.Empty);
      return theString;
    }

    public static string SafetyQueryS(string theString)
    {
      string[] strArray = new string[7]
      {
        "'",
        ";",
        "\"",
        "\r",
        "\n",
        "<",
        ">"
      };
      foreach (string oldValue in strArray)
        theString = theString.Replace(oldValue, string.Empty);
      return theString;
    }

    public static string SafetyLikeValue(string theString)
    {
      string[] strArray = new string[11]
      {
        "'",
        ";",
        "\"",
        "\r",
        "\n",
        "%",
        "-",
        "[",
        "]",
        "(",
        ")"
      };
      foreach (string oldValue in strArray)
        theString = theString.Replace(oldValue, string.Empty);
      return theString;
    }

    public static string[] GetRegValue(string HtmlCode, string RegexString, string GroupKey, bool RightToLeft)
    {
      MatchCollection matchCollection = (!RightToLeft ? new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline) : new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft)).Matches(HtmlCode);
      string[] strArray = new string[matchCollection.Count];
      for (int index = 0; index < matchCollection.Count; ++index)
        strArray[index] = matchCollection[index].Groups[GroupKey].Value;
      return strArray;
    }

    public static string AttributeValue(string HtmlTag, string AttributeName)
    {
      string RegexString = (HtmlTag.StartsWith(AttributeName + "=") ? "(.{0})" : "([\"'\\s\\|:]{1})") + AttributeName + "=(\"|')(?<" + AttributeName + ">.*?[^\\\\]{1})(\\2)";
      string[] regValue = Strings.GetRegValue(HtmlTag, RegexString, AttributeName, false);
      if (regValue.Length > 0)
        return regValue[0].ToString();
      return "";
    }

    public static string DateStringFromNow(DateTime dt)
    {
      TimeSpan timeSpan = DateTime.Now - dt;
      if (timeSpan.TotalDays > 60.0)
        return dt.ToShortDateString();
      if (timeSpan.TotalDays > 30.0)
        return "1个月前";
      if (timeSpan.TotalDays > 14.0)
        return "2周前";
      if (timeSpan.TotalDays > 7.0)
        return "1周前";
      if (timeSpan.TotalDays > 1.0)
        return string.Format("{0}天前", (object) (int) Math.Floor(timeSpan.TotalDays));
      if (timeSpan.TotalHours > 1.0)
        return string.Format("{0}小时前", (object) (int) Math.Floor(timeSpan.TotalHours));
      if (timeSpan.TotalMinutes > 1.0)
        return string.Format("{0}分钟前", (object) (int) Math.Floor(timeSpan.TotalMinutes));
      if (timeSpan.TotalSeconds >= 1.0)
        return string.Format("{0}秒前", (object) (int) Math.Floor(timeSpan.TotalSeconds));
      return "1秒前";
    }

    public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd)
    {
      return Strings.getArray(sHtml, strStart, strEnd);
    }

    public static ArrayList GetHtmls(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
    {
      return Strings.getArray(sHtml, strStart, strEnd, getStart, getEnd);
    }

    public static string GetHtml(string sHtml, string strStart, string strEnd)
    {
      return Strings.getResult(sHtml, strStart, strEnd);
    }

    public static string GetHtml(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
    {
      return Strings.getResult(sHtml, strStart, strEnd, getStart, getEnd);
    }

    private static string enReplaceStr(string str)
    {
      if (str == null || str == "")
        return "superstring_空值";
      return str.Replace("\r", "superstring_回车").Replace("\n", "superstring_换行").Replace("\"", "superstring_双引").Replace("\\", "superstring_反斜");
    }

    private static string deReplaceStr(string str)
    {
      return str.Replace("superstring_回车", "\r").Replace("superstring_换行", "\n").Replace("superstring_双引", "\"").Replace("superstring_反斜", "\\").Replace("superstring_空值", "").Replace("superstring_空头", "").Replace("superstring_空尾", "");
    }

    private static ArrayList getArray(string sHtml, string strStart, string strEnd)
    {
      return Strings.getArray(sHtml, strStart, strEnd, false, false);
    }

    private static ArrayList getArray(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
    {
      if (strEnd == null || strEnd == "")
      {
        sHtml += "superstring_空尾";
        strEnd = "superstring_空尾";
      }
      if (strStart == null || strStart == "")
      {
        sHtml = "superstring_空头" + sHtml;
        strStart = "superstring_空头";
      }
      ArrayList arrayList = new ArrayList();
      MatchCollection matchCollection = new Regex(Strings.RegexStr(Strings.enReplaceStr(strStart), Strings.enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline).Matches(Strings.enReplaceStr(sHtml));
      for (int index = 0; index < matchCollection.Count; ++index)
      {
        string str = Strings.deReplaceStr(matchCollection[index].Value);
        if (getStart)
          str = strStart + str;
        if (getEnd)
          str += strEnd;
        arrayList.Add((object) str);
      }
      return arrayList;
    }

    private static string getResult(string sHtml, string strStart, string strEnd)
    {
      return Strings.getResult(sHtml, strStart, strEnd, false, false);
    }

    private static string getResult(string sHtml, string strStart, string strEnd, bool getStart, bool getEnd)
    {
      if (strEnd == null || strEnd == "")
      {
        sHtml += "superstring_空尾";
        strEnd = "superstring_空尾";
      }
      if (strStart == null || strStart == "")
      {
        sHtml = "superstring_空头" + sHtml;
        strStart = "superstring_空头";
      }
      string str = Strings.deReplaceStr(new Regex(Strings.RegexStr(Strings.enReplaceStr(strStart), Strings.enReplaceStr(strEnd)), RegexOptions.Multiline | RegexOptions.Singleline).Match(Strings.enReplaceStr(sHtml)).Value);
      if (getStart)
        str = strStart + str;
      if (getEnd)
        str += strEnd;
      return str;
    }

    private static string RegexStr(string strStart, string strEnd)
    {
      string str1 = strStart;
      string str2 = strEnd;
      for (int index = 0; index < Strings.aryChar.Length; ++index)
      {
        str1 = str1.Replace(Strings.aryChar[index], "\\" + Strings.aryChar[index]);
        str2 = str2.Replace(Strings.aryChar[index], "\\" + Strings.aryChar[index]);
      }
      return "(?<=(" + str1 + "))[.\\s\\S]*?(?=(" + str2 + "))";
    }
  }
}
