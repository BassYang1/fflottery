// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Int
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public static class Int
  {
    public static int PageCount(int TotalCount, int PageSize)
    {
      if (TotalCount == 0)
        return 1;
      if (TotalCount % PageSize != 0)
        return TotalCount / PageSize + 1;
      return TotalCount / PageSize;
    }

    public static int Max(int int1, int int2)
    {
      if (int1 <= int2)
        return int2;
      return int1;
    }

    public static int Min(int int1, int int2)
    {
      if (int1 >= int2)
        return int2;
      return int1;
    }

    public static int ExactlyDivisible(double x, double y, bool ending)
    {
      double num = x / y;
      if (!ending)
        return Convert.ToInt32(num);
      return Convert.ToInt32(num - x % y / y);
    }
  }
}
