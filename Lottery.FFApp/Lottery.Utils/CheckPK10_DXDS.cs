// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckPK10_DXDS
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

namespace Lottery.Utils
{
  public static class CheckPK10_DXDS
  {
    public static int PK10_DS(string LotteryNumber, string CheckNumber, int Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (Pos == 1)
        LotteryNumber = strArray1[0];
      if (Pos == 2)
        LotteryNumber = strArray1[1];
      if (Pos == 3)
        LotteryNumber = strArray1[2];
      string[] strArray2 = LotteryNumber.Replace("01", "单").Replace("02", "双").Replace("03", "单").Replace("04", "双").Replace("05", "单").Replace("06", "双").Replace("07", "单").Replace("08", "双").Replace("09", "单").Replace("10", "双").Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      string[] strArray4 = strArray2[0].Split('_');
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray4.Length; ++index2)
        {
          if (strArray3[0].IndexOf(strArray4[index1]) != -1)
            ++num;
        }
      }
      return num;
    }

    public static int PK10_DX(string LotteryNumber, string CheckNumber, int Pos)
    {
      int num = 0;
      string[] strArray1 = LotteryNumber.Split(',');
      if (Pos == 1)
        LotteryNumber = strArray1[0];
      if (Pos == 2)
        LotteryNumber = strArray1[1];
      if (Pos == 3)
        LotteryNumber = strArray1[2];
      string[] strArray2 = LotteryNumber.Replace("01", "小").Replace("02", "小").Replace("03", "小").Replace("04", "小").Replace("05", "小").Replace("06", "大").Replace("07", "大").Replace("08", "大").Replace("09", "大").Replace("10", "大").Split(',');
      string[] strArray3 = CheckNumber.Split(',');
      string[] strArray4 = strArray2[0].Split('_');
      for (int index1 = 0; index1 < strArray4.Length; ++index1)
      {
        for (int index2 = 0; index2 < strArray4.Length; ++index2)
        {
          if (strArray3[0].IndexOf(strArray4[index1]) != -1)
            ++num;
        }
      }
      return num;
    }
  }
}
