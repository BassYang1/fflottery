// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.fastJSON.JsonParser
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lottery.Utils.fastJSON
{
  internal class JsonParser
  {
    private const int TOKEN_NONE = 0;
    private const int TOKEN_CURLY_OPEN = 1;
    private const int TOKEN_CURLY_CLOSE = 2;
    private const int TOKEN_SQUARED_OPEN = 3;
    private const int TOKEN_SQUARED_CLOSE = 4;
    private const int TOKEN_COLON = 5;
    private const int TOKEN_COMMA = 6;
    private const int TOKEN_STRING = 7;
    private const int TOKEN_NUMBER = 8;
    private const int TOKEN_TRUE = 9;
    private const int TOKEN_FALSE = 10;
    private const int TOKEN_NULL = 11;

    internal static object JsonDecode(string json)
    {
      bool success = true;
      return JsonParser.JsonDecode(json, ref success);
    }

    private static object JsonDecode(string json, ref bool success)
    {
      success = true;
      if (json == null)
        return (object) null;
      char[] charArray = json.ToCharArray();
      int index = 0;
      return JsonParser.ParseValue(charArray, ref index, ref success);
    }

    protected static Dictionary<string, object> ParseObject(char[] json, ref int index, ref bool success)
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      JsonParser.NextToken(json, ref index);
      bool flag = false;
      while (!flag)
      {
        switch (JsonParser.LookAhead(json, index))
        {
          case 0:
            success = false;
            return (Dictionary<string, object>) null;
          case 2:
            JsonParser.NextToken(json, ref index);
            return dictionary;
          case 6:
            JsonParser.NextToken(json, ref index);
            continue;
          default:
            string index1 = JsonParser.ParseString(json, ref index, ref success);
            if (!success)
            {
              success = false;
              return (Dictionary<string, object>) null;
            }
            if (JsonParser.NextToken(json, ref index) != 5)
            {
              success = false;
              return (Dictionary<string, object>) null;
            }
            object obj = JsonParser.ParseValue(json, ref index, ref success);
            if (!success)
            {
              success = false;
              return (Dictionary<string, object>) null;
            }
            dictionary[index1] = obj;
            continue;
        }
      }
      return dictionary;
    }

    protected static ArrayList ParseArray(char[] json, ref int index, ref bool success)
    {
      ArrayList arrayList = new ArrayList();
      JsonParser.NextToken(json, ref index);
      bool flag = false;
      while (!flag)
      {
        switch (JsonParser.LookAhead(json, index))
        {
          case 0:
            success = false;
            return (ArrayList) null;
          case 4:
            JsonParser.NextToken(json, ref index);
            goto label_9;
          case 6:
            JsonParser.NextToken(json, ref index);
            continue;
          default:
            object obj = JsonParser.ParseValue(json, ref index, ref success);
            if (!success)
              return (ArrayList) null;
            arrayList.Add(obj);
            continue;
        }
      }
label_9:
      return arrayList;
    }

    protected static object ParseValue(char[] json, ref int index, ref bool success)
    {
      switch (JsonParser.LookAhead(json, index))
      {
        case 1:
          return (object) JsonParser.ParseObject(json, ref index, ref success);
        case 3:
          return (object) JsonParser.ParseArray(json, ref index, ref success);
        case 7:
          return (object) JsonParser.ParseString(json, ref index, ref success);
        case 8:
          return (object) JsonParser.ParseNumber(json, ref index, ref success);
        case 9:
          JsonParser.NextToken(json, ref index);
          return (object) true;
        case 10:
          JsonParser.NextToken(json, ref index);
          return (object) false;
        case 11:
          JsonParser.NextToken(json, ref index);
          return (object) null;
        default:
          success = false;
          return (object) null;
      }
    }

    protected static string ParseString(char[] json, ref int index, ref bool success)
    {
      StringBuilder stringBuilder = new StringBuilder();
      JsonParser.EatWhitespace(json, ref index);
      char ch1 = json[index++];
      bool flag = false;
      while (!flag && index != json.Length)
      {
        char ch2 = json[index++];
        switch (ch2)
        {
          case '"':
            flag = true;
            goto label_19;
          case '\\':
            if (index != json.Length)
            {
              switch (json[index++])
              {
                case '"':
                  stringBuilder.Append('"');
                  continue;
                case '/':
                  stringBuilder.Append('/');
                  continue;
                case '\\':
                  stringBuilder.Append('\\');
                  continue;
                case 'b':
                  stringBuilder.Append('\b');
                  continue;
                case 'f':
                  stringBuilder.Append('\f');
                  continue;
                case 'n':
                  stringBuilder.Append('\n');
                  continue;
                case 'r':
                  stringBuilder.Append('\r');
                  continue;
                case 't':
                  stringBuilder.Append('\t');
                  continue;
                case 'u':
                  if (json.Length - index >= 4)
                  {
                    uint result;
                    if (!(success = uint.TryParse(new string(json, index, 4), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result)))
                      return "";
                    stringBuilder.Append(char.ConvertFromUtf32((int) result));
                    index += 4;
                    continue;
                  }
                  goto label_19;
                default:
                  continue;
              }
            }
            else
              goto label_19;
          default:
            stringBuilder.Append(ch2);
            continue;
        }
      }
label_19:
      if (flag)
        return stringBuilder.ToString();
      success = false;
      return (string) null;
    }

    protected static string ParseNumber(char[] json, ref int index, ref bool success)
    {
      JsonParser.EatWhitespace(json, ref index);
      int lastIndexOfNumber = JsonParser.GetLastIndexOfNumber(json, index);
      int length = lastIndexOfNumber - index + 1;
      string str = new string(json, index, length);
      success = true;
      index = lastIndexOfNumber + 1;
      return str;
    }

    protected static int GetLastIndexOfNumber(char[] json, int index)
    {
      int index1 = index;
      while (index1 < json.Length && "0123456789+-.eE".IndexOf(json[index1]) != -1)
        ++index1;
      return index1 - 1;
    }

    protected static void EatWhitespace(char[] json, ref int index)
    {
      while (index < json.Length && " \t\n\r".IndexOf(json[index]) != -1)
        ++index;
    }

    protected static int LookAhead(char[] json, int index)
    {
      int index1 = index;
      return JsonParser.NextToken(json, ref index1);
    }

    protected static int NextToken(char[] json, ref int index)
    {
      JsonParser.EatWhitespace(json, ref index);
      if (index == json.Length)
        return 0;
      char ch = json[index];
      ++index;
      switch (ch)
      {
        case '"':
          return 7;
        case ',':
          return 6;
        case '-':
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
          return 8;
        case ':':
          return 5;
        case '[':
          return 3;
        case ']':
          return 4;
        case '{':
          return 1;
        case '}':
          return 2;
        default:
          --index;
          int num = json.Length - index;
          if (num >= 5 && (int) json[index] == 102 && ((int) json[index + 1] == 97 && (int) json[index + 2] == 108) && ((int) json[index + 3] == 115 && (int) json[index + 4] == 101))
          {
            index += 5;
            return 10;
          }
          if (num >= 4 && (int) json[index] == 116 && ((int) json[index + 1] == 114 && (int) json[index + 2] == 117) && (int) json[index + 3] == 101)
          {
            index += 4;
            return 9;
          }
          if (num < 4 || (int) json[index] != 110 || ((int) json[index + 1] != 117 || (int) json[index + 2] != 108) || (int) json[index + 3] != 108)
            return 0;
          index += 4;
          return 11;
      }
    }

    protected static bool SerializeValue(object value, StringBuilder builder)
    {
      bool flag = true;
      if (value is string)
        flag = JsonParser.SerializeString((string) value, builder);
      else if (value is Hashtable)
        flag = JsonParser.SerializeObject((Hashtable) value, builder);
      else if (value is ArrayList)
        flag = JsonParser.SerializeArray((ArrayList) value, builder);
      else if (JsonParser.IsNumeric(value))
        flag = JsonParser.SerializeNumber(Convert.ToDouble(value), builder);
      else if (value is bool && (bool) value)
        builder.Append("true");
      else if (value is bool && !(bool) value)
        builder.Append("false");
      else if (value == null)
        builder.Append("null");
      else
        flag = false;
      return flag;
    }

    protected static bool SerializeObject(Hashtable anObject, StringBuilder builder)
    {
      builder.Append("{");
      IDictionaryEnumerator enumerator = anObject.GetEnumerator();
      bool flag = true;
      while (enumerator.MoveNext())
      {
        string aString = enumerator.Key.ToString();
        object obj = enumerator.Value;
        if (!flag)
          builder.Append(", ");
        JsonParser.SerializeString(aString, builder);
        builder.Append(":");
        if (!JsonParser.SerializeValue(obj, builder))
          return false;
        flag = false;
      }
      builder.Append("}");
      return true;
    }

    protected static bool SerializeArray(ArrayList anArray, StringBuilder builder)
    {
      builder.Append("[");
      bool flag = true;
      for (int index = 0; index < anArray.Count; ++index)
      {
        object an = anArray[index];
        if (!flag)
          builder.Append(", ");
        if (!JsonParser.SerializeValue(an, builder))
          return false;
        flag = false;
      }
      builder.Append("]");
      return true;
    }

    protected static bool SerializeString(string aString, StringBuilder builder)
    {
      builder.Append("\"");
      foreach (char ch in aString.ToCharArray())
      {
        switch (ch)
        {
          case '\b':
            builder.Append("\\b");
            break;
          case '\t':
            builder.Append("\\t");
            break;
          case '\n':
            builder.Append("\\n");
            break;
          case '\f':
            builder.Append("\\f");
            break;
          case '\r':
            builder.Append("\\r");
            break;
          case '"':
            builder.Append("\\\"");
            break;
          case '\\':
            builder.Append("\\\\");
            break;
          default:
            int int32 = Convert.ToInt32(ch);
            if (int32 >= 32 && int32 <= 126)
            {
              builder.Append(ch);
              break;
            }
            builder.Append("\\u" + Convert.ToString(int32, 16).PadLeft(4, '0'));
            break;
        }
      }
      builder.Append("\"");
      return true;
    }

    protected static bool SerializeNumber(double number, StringBuilder builder)
    {
      builder.Append(Convert.ToString(number, (IFormatProvider) CultureInfo.InvariantCulture));
      return true;
    }

    protected static bool IsNumeric(object o)
    {
      if (o != null)
      {
        double result;
        return double.TryParse(o.ToString(), out result);
      }
      return false;
    }
  }
}
