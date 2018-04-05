// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Validator
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class Validator
  {
    public static bool IsCommonDomain(string _value)
    {
      return Validator.QuickValidate("^(www.)?(\\w+\\.){1,3}(org|org.cn|gov.cn|com|cn|net|cc)$", _value.ToLower());
    }

    public static bool IsStringDate(string _value)
    {
      try
      {
        DateTime.Parse(_value);
      }
      catch (FormatException ex)
      {
        return false;
      }
      return true;
    }

    public static bool IsNumeric(string _value)
    {
      return Validator.QuickValidate("^[-]?[1-9]*[0-9]*$", _value);
    }

    public static bool IsLetterOrNumber(string _value)
    {
      return Validator.QuickValidate("^[a-zA-Z0-9_]*$", _value);
    }

    public static bool IsNumber(string _value)
    {
      return Validator.QuickValidate("^(0|([1-9]+[0-9]*))(.[0-9]+)?$", _value);
    }

    public static bool QuickValidate(string _express, string _value)
    {
      Regex regex = new Regex(_express);
      if (_value == null || _value.Length == 0)
        return false;
      return regex.IsMatch(_value);
    }

    public static bool IsEmail(string _value)
    {
      return new Regex("^\\w+([-+.]\\w+)*@(\\w+([-.]\\w+)*\\.)+([a-zA-Z]+)+$", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsZIPCode(string _value)
    {
      return Validator.QuickValidate("^([0-9]{6})$", _value);
    }

    public static bool IsIDCard(string _value)
    {
      if (_value.Length != 15 && _value.Length != 18)
        return false;
      if (_value.Length == 15)
      {
        Regex regex = new Regex("^(\\d{6})(\\d{2})(\\d{2})(\\d{2})(\\d{3})$");
        if (!regex.Match(_value).Success)
          return false;
        string[] strArray = regex.Split(_value);
        try
        {
          DateTime dateTime = new DateTime(int.Parse("19" + strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
          return true;
        }
        catch
        {
          return false;
        }
      }
      else
      {
        Regex regex = new Regex("^(\\d{6})(\\d{4})(\\d{2})(\\d{2})(\\d{3})([0-9Xx])$");
        if (!regex.Match(_value).Success)
          return false;
        string[] strArray = regex.Split(_value);
        try
        {
          DateTime dateTime = new DateTime(int.Parse(strArray[2]), int.Parse(strArray[3]), int.Parse(strArray[4]));
          return true;
        }
        catch
        {
          return false;
        }
      }
    }

    public static bool IsInt(string _value)
    {
      return new Regex("^(-){0,1}\\d+$").Match(_value).Success && long.Parse(_value) <= (long) int.MaxValue && long.Parse(_value) >= (long) int.MinValue;
    }

    public static bool IsLengthStr(string _value, int _begin, int _end)
    {
      int length = _value.Length;
      return length >= _begin || length <= _end;
    }

    public static bool IsChinese(string _value)
    {
      return new Regex("^[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+$", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsMobileNum(string _value)
    {
      return new Regex("^(13|15)\\d{9}$", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsPhoneNum(string _value)
    {
      return new Regex("^(86)?(-)?(0\\d{2,3})?(-)?(\\d{7,8})(-)?(\\d{3,5})?$", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsUrl(string _value)
    {
      return new Regex("(http://)?([\\w-]+\\.)*[\\w-]+(/[\\w- ./?%&=]*)?", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsIP(string _value)
    {
      return new Regex("^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$", RegexOptions.IgnoreCase).Match(_value).Success;
    }

    public static bool IsWordAndNum(string _value)
    {
      return new Regex("[a-zA-Z0-9]?").Match(_value).Success;
    }

    public static DateTime StrToDate(string _value, DateTime _defaultValue)
    {
      if (Validator.IsStringDate(_value))
        return Convert.ToDateTime(_value);
      return _defaultValue;
    }

    public static bool CompareDate(string today, string writeDate, int n)
    {
      DateTime dateTime1 = Convert.ToDateTime(today);
      DateTime dateTime2 = Convert.ToDateTime(writeDate);
      dateTime2 = dateTime2.AddDays((double) n);
      return !(dateTime1 >= dateTime2);
    }

    public static bool ValidDate(string myDate)
    {
      if (!Validator.IsStringDate(myDate))
        return true;
      return Validator.CompareDate(myDate, DateTime.Now.ToShortDateString(), 0);
    }

    public static int StrToInt(string _value, int _defaultValue)
    {
      if (Validator.IsNumeric(_value))
        return int.Parse(_value);
      return _defaultValue;
    }

    public static bool IsFreeSite(string _defaultpage, string _webname)
    {
      string http = HttpHelp.Get_Http(_defaultpage, 10000, Encoding.UTF8);
      string str = http.ToLower().Replace("\"", "").Replace("'", "");
      if (!http.Contains(_webname))
        return false;
      if (!str.Contains("href=http://www.Lottery.net"))
        return str.Contains("href=http://Lottery.net");
      return true;
    }
  }
}
