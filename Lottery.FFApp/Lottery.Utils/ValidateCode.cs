// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.ValidateCode
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class ValidateCode
  {
    public static bool CheckValidateCode(string _code, ref string _realcode)
    {
      _realcode = Session.Get("ValidateCode");
      return _code != null && _code.Length != 0 && (_realcode != null && _realcode.Length != 0) && _realcode.ToLower() == _code.ToLower();
    }

    public static string GetValidateCode(int _length, bool _init)
    {
      if (_init)
        ValidateCode.CreateValidateCode(_length, true);
      return Session.Get("ValidateCode");
    }

    public static void CreateValidateCode(int _length, bool _cover)
    {
      if (_cover)
      {
        ValidateCode.SaveCookie(_length);
      }
      else
      {
        if (Session.Get("ValidateCode") != null)
          return;
        ValidateCode.SaveCookie(_length);
      }
    }

    public static void SaveCookie(int _length)
    {
      char[] charArray = "0123456789".ToCharArray();
      Random random = new Random();
      string empty = string.Empty;
      for (int index = 0; index < _length; ++index)
        empty += charArray[random.Next(0, charArray.Length)].ToString();
      if (Session.Get("ValidateCode") != null)
        Session.Del("ValidateCode");
      Session.Add("ValidateCode", empty);
    }
  }
}
