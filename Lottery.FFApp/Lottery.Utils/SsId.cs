// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.SsId
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public class SsId
  {
    public static string Bet
    {
      get
      {
        return "B_" + (object) SsId.GuidToLongID();
      }
    }

    public static string ZBet
    {
      get
      {
        return "Z_" + (object) SsId.GuidToLongID();
      }
    }

    public static string Charge
    {
      get
      {
        return "C_" + (object) SsId.GuidToLongID();
      }
    }

    public static string ChargeLog
    {
      get
      {
        return "L_" + (object) SsId.GuidToLongID();
      }
    }

    public static string GetCash
    {
      get
      {
        return "G_" + (object) SsId.GuidToLongID();
      }
    }

    public static string MoneyLog
    {
      get
      {
        return "M_" + (object) SsId.GuidToLongID();
      }
    }

    public static string Act
    {
      get
      {
        return "A_" + (object) SsId.GuidToLongID();
      }
    }

    public static string Admin
    {
      get
      {
        return "H_" + (object) SsId.GuidToLongID();
      }
    }

    public static string GuidTo16String()
    {
      long num1 = 1;
      foreach (byte num2 in Guid.NewGuid().ToByteArray())
        num1 *= (long) ((int) num2 + 1);
      return string.Format("{0:x}", (object) (num1 - DateTime.Now.Ticks));
    }

    public static long GuidToLongID()
    {
      return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
    }
  }
}
