// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.CheckPlay
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;

namespace Lottery.Utils
{
  public class CheckPlay
  {
    public static int Check(string LotteryNumber, string CheckNumber, string Pos, string sType)
    {
      if (Pos != "")
      {
        string str = "";
        string[] strArray = Pos.Split(',');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (Convert.ToInt32(strArray[index]) == 1)
            str = str + "," + index.ToString();
        }
        Pos = str.Substring(1);
      }
      CheckNumber = CheckNumber.Replace("０", "0");
      CheckNumber = CheckNumber.Replace("１", "1");
      CheckNumber = CheckNumber.Replace("２", "2");
      CheckNumber = CheckNumber.Replace("３", "3");
      CheckNumber = CheckNumber.Replace("４", "4");
      CheckNumber = CheckNumber.Replace("５", "5");
      CheckNumber = CheckNumber.Replace("６", "6");
      CheckNumber = CheckNumber.Replace("７", "7");
      CheckNumber = CheckNumber.Replace("８", "8");
      CheckNumber = CheckNumber.Replace("９", "9");
      if (LotteryNumber != "" && CheckNumber != "" && sType != "")
      {
        LotteryNumber.Split(',');
        switch (sType)
        {
          case "P_5FS":
            return CheckSSC_5Start.P_5FS(LotteryNumber, CheckNumber);
          case "P_5DS":
            return CheckSSC_5Start.P_5DS(LotteryNumber, CheckNumber);
          case "P_5ZX120":
            return CheckSSC_5Start.P_5ZX120(LotteryNumber, CheckNumber);
          case "P_5ZX60":
            return CheckSSC_5Start.P_5ZX60(LotteryNumber, CheckNumber);
          case "P_5ZX30":
            return CheckSSC_5Start.P_5ZX30(LotteryNumber, CheckNumber);
          case "P_5ZX20":
            return CheckSSC_5Start.P_5ZX20(LotteryNumber, CheckNumber);
          case "P_5ZX10":
            return CheckSSC_5Start.P_5ZX10(LotteryNumber, CheckNumber);
          case "P_5ZX5":
            return CheckSSC_5Start.P_5ZX5(LotteryNumber, CheckNumber);
          case "P_5TS1":
            return CheckSSC_5Start.P_5TS(LotteryNumber, CheckNumber, 1);
          case "P_5TS2":
            return CheckSSC_5Start.P_5TS(LotteryNumber, CheckNumber, 2);
          case "P_5TS3":
            return CheckSSC_5Start.P_5TS(LotteryNumber, CheckNumber, 3);
          case "P_5TS4":
            return CheckSSC_5Start.P_5TS(LotteryNumber, CheckNumber, 4);
          case "P_5ZH_WQBSG":
            return CheckSSC_5Start.P_5ZH_5(LotteryNumber, CheckNumber);
          case "P_5ZH_QBSG":
            return CheckSSC_5Start.P_5ZH_4(LotteryNumber, CheckNumber);
          case "P_5ZH_BSG":
            return CheckSSC_5Start.P_5ZH_3(LotteryNumber, CheckNumber);
          case "P_5ZH_SG":
            return CheckSSC_5Start.P_5ZH_2(LotteryNumber, CheckNumber);
          case "P_5ZH_G":
            return CheckSSC_5Start.P_5ZH_1(LotteryNumber, CheckNumber);
          case "P_4FS_L":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "L");
          case "P_4FS_R":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "R");
          case "R_4FS_WQBS":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "WQBS");
          case "R_4FS_WQBG":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "WQBG");
          case "R_4FS_WQSG":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "WQSG");
          case "R_4FS_WBSG":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "WBSG");
          case "R_4FS_QBSG":
            return CheckSSC_4Start.P_4FS(LotteryNumber, CheckNumber, "QBSG");
          case "P_4DS_L":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "L");
          case "P_4DS_R":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "R");
          case "R_4DS_WQBS":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "WQBS");
          case "R_4DS_WQBG":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "WQBG");
          case "R_4DS_WQSG":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "WQSG");
          case "R_4DS_WBSG":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "WBSG");
          case "R_4DS_QBSG":
            return CheckSSC_4Start.P_4DS(LotteryNumber, CheckNumber, "QBSG");
          case "P_4ZX24":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "R");
          case "R_4ZX24_WQBS":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "WQBS");
          case "R_4ZX24_WQBG":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "WQBG");
          case "R_4ZX24_WQSG":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "WQSG");
          case "R_4ZX24_WBSG":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "WBSG");
          case "R_4ZX24_QBSG":
            return CheckSSC_4Start.P_4ZX24(LotteryNumber, CheckNumber, "QBSG");
          case "P_4ZX12":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "R");
          case "R_4ZX12_WQBS":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "WQBS");
          case "R_4ZX12_WQBG":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "WQBG");
          case "R_4ZX12_WQSG":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "WQSG");
          case "R_4ZX12_WBSG":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "WBSG");
          case "R_4ZX12_QBSG":
            return CheckSSC_4Start.P_4ZX12(LotteryNumber, CheckNumber, "QBSG");
          case "P_4ZX6":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "R");
          case "R_4ZX6_WQBS":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "WQBS");
          case "R_4ZX6_WQBG":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "WQBG");
          case "R_4ZX6_WQSG":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "WQSG");
          case "R_4ZX6_WBSG":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "WBSG");
          case "R_4ZX6_QBSG":
            return CheckSSC_4Start.P_4ZX6(LotteryNumber, CheckNumber, "QBSG");
          case "P_4ZX4":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "R");
          case "R_4ZX4_WQBS":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "WQBS");
          case "R_4ZX4_WQBG":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "WQBG");
          case "R_4ZX4_WQSG":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "WQSG");
          case "R_4ZX4_WBSG":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "WBSG");
          case "R_4ZX4_QBSG":
            return CheckSSC_4Start.P_4ZX4(LotteryNumber, CheckNumber, "QBSG");
          case "P_4ZH_L_WQBS":
            return CheckSSC_4Start.P_4ZH_4(LotteryNumber, CheckNumber, "L");
          case "P_4ZH_L_QBS":
            return CheckSSC_4Start.P_4ZH_3(LotteryNumber, CheckNumber, "L");
          case "P_4ZH_L_BS":
            return CheckSSC_4Start.P_4ZH_2(LotteryNumber, CheckNumber, "L");
          case "P_4ZH_L_S":
            return CheckSSC_4Start.P_4ZH_1(LotteryNumber, CheckNumber, "L");
          case "P_4ZH_R_QBSG":
            return CheckSSC_4Start.P_4ZH_4(LotteryNumber, CheckNumber, "R");
          case "P_4ZH_R_BSG":
            return CheckSSC_4Start.P_4ZH_3(LotteryNumber, CheckNumber, "R");
          case "P_4ZH_R_SG":
            return CheckSSC_4Start.P_4ZH_2(LotteryNumber, CheckNumber, "R");
          case "P_4ZH_R_G":
            return CheckSSC_4Start.P_4ZH_1(LotteryNumber, CheckNumber, "R");
          case "P_3FS_L":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "L");
          case "P_3FS_C":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "C");
          case "P_3FS_R":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "R");
          case "R_3FS_WQB":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WQB");
          case "R_3FS_WQS":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WQS");
          case "R_3FS_WQG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WQG");
          case "R_3FS_WBS":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WBS");
          case "R_3FS_WBG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WBG");
          case "R_3FS_WSG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "WSG");
          case "R_3FS_QBS":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "QBS");
          case "R_3FS_QBG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "QBG");
          case "R_3FS_QSG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "QSG");
          case "R_3FS_BSG":
            return CheckSSC_3Start.P_3ZX(LotteryNumber, CheckNumber, "BSG");
          case "P_3DS_L":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "L");
          case "P_3DS_C":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "C");
          case "P_3DS_R":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "R");
          case "R_3DS_WQB":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WQB");
          case "R_3DS_WQS":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WQS");
          case "R_3DS_WQG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WQG");
          case "R_3DS_WBS":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WBS");
          case "R_3DS_WBG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WBG");
          case "R_3DS_WSG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "WSG");
          case "R_3DS_QBS":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "QBS");
          case "R_3DS_QBG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "QBG");
          case "R_3DS_QSG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "QSG");
          case "R_3DS_BSG":
            return CheckSSC_3Start.P_3DS(LotteryNumber, CheckNumber, "BSG");
          case "P_3Z3_L":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "L");
          case "P_3Z3_C":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "C");
          case "P_3Z3_R":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "R");
          case "R_3Z3_WQB":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WQB");
          case "R_3Z3_WQS":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WQS");
          case "R_3Z3_WQG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WQG");
          case "R_3Z3_WBS":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WBS");
          case "R_3Z3_WBG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WBG");
          case "R_3Z3_WSG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "WSG");
          case "R_3Z3_QBS":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "QBS");
          case "R_3Z3_QBG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "QBG");
          case "R_3Z3_QSG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "QSG");
          case "R_3Z3_BSG":
            return CheckSSC_3Start.P_3Z3(LotteryNumber, CheckNumber, "BSG");
          case "P_3Z6_L":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "L");
          case "P_3Z6_C":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "C");
          case "P_3Z6_R":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "R");
          case "R_3Z6_WQB":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WQB");
          case "R_3Z6_WQS":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WQS");
          case "R_3Z6_WQG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WQG");
          case "R_3Z6_WBS":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WBS");
          case "R_3Z6_WBG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WBG");
          case "R_3Z6_WSG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "WSG");
          case "R_3Z6_QBS":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "QBS");
          case "R_3Z6_QBG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "QBG");
          case "R_3Z6_QSG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "QSG");
          case "R_3Z6_BSG":
            return CheckSSC_3Start.P_3Z6(LotteryNumber, CheckNumber, "BSG");
          case "P_3Z3_2_L":
            return CheckSSC_3Start.P_3Z3_2(LotteryNumber, CheckNumber, "L");
          case "P_3Z3_2_C":
            return CheckSSC_3Start.P_3Z3_2(LotteryNumber, CheckNumber, "C");
          case "P_3Z3_2_R":
            return CheckSSC_3Start.P_3Z3_2(LotteryNumber, CheckNumber, "R");
          case "P_3Z6_2_L":
            return CheckSSC_3Start.P_3Z6_2(LotteryNumber, CheckNumber, "L");
          case "P_3Z6_2_C":
            return CheckSSC_3Start.P_3Z6_2(LotteryNumber, CheckNumber, "C");
          case "P_3Z6_2_R":
            return CheckSSC_3Start.P_3Z6_2(LotteryNumber, CheckNumber, "R");
          case "P_3HX_L":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "L");
          case "P_3HX_C":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "C");
          case "P_3HX_R":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "R");
          case "R_3HX_WQB":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WQB");
          case "R_3HX_WQS":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WQS");
          case "R_3HX_WQG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WQG");
          case "R_3HX_WBS":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WBS");
          case "R_3HX_WBG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WBG");
          case "R_3HX_WSG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "WSG");
          case "R_3HX_QBS":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "QBS");
          case "R_3HX_QBG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "QBG");
          case "R_3HX_QSG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "QSG");
          case "R_3HX_BSG":
            return CheckSSC_3Start.P_3HX(LotteryNumber, CheckNumber, "BSG");
          case "R_3FS":
            return CheckSSC_3Start.R_3FS(LotteryNumber, CheckNumber, Pos);
          case "R_3DS":
            return CheckSSC_3Start.R_3DS(LotteryNumber, CheckNumber, Pos);
          case "R_3Z3":
            return CheckSSC_3Start.R_3Z3(LotteryNumber, CheckNumber, Pos);
          case "R_3Z6":
            return CheckSSC_3Start.R_3Z6(LotteryNumber, CheckNumber, Pos);
          case "R_3Z3_2":
            return CheckSSC_3Start.R_3Z3_2(LotteryNumber, CheckNumber, Pos);
          case "R_3Z6_2":
            return CheckSSC_3Start.R_3Z6_2(LotteryNumber, CheckNumber, Pos);
          case "P_3HE_L":
          case "P_3ZHE_L":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "L");
          case "P_3HE_C":
          case "P_3ZHE_C":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "C");
          case "P_3HE_R":
          case "P_3ZHE_R":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "R");
          case "R_3HE_WQB":
          case "R_3ZHE_WQB":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WQB");
          case "R_3HE_WQS":
          case "R_3ZHE_WQS":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WQS");
          case "R_3HE_WQG":
          case "R_3ZHE_WQG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WQG");
          case "R_3HE_WBS":
          case "R_3ZHE_WBS":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WBS");
          case "R_3HE_WBG":
          case "R_3ZHE_WBG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WBG");
          case "R_3HE_WSG":
          case "R_3ZHE_WSG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "WSG");
          case "R_3HE_QBS":
          case "R_3ZHE_QBS":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "QBS");
          case "R_3HE_QBG":
          case "R_3ZHE_QBG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "QBG");
          case "R_3HE_QSG":
          case "R_3ZHE_QSG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "QSG");
          case "R_3HE_BSG":
          case "R_3ZHE_BSG":
            return CheckSSC_3Start.P_3HE(LotteryNumber, CheckNumber, "BSG");
          case "P_3KD_L":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "L");
          case "P_3KD_C":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "C");
          case "P_3KD_R":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "R");
          case "R_3KD_WQB":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WQB");
          case "R_3KD_WQS":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WQS");
          case "R_3KD_WQG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WQG");
          case "R_3KD_WBS":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WBS");
          case "R_3KD_WBG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WBG");
          case "R_3KD_WSG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "WSG");
          case "R_3KD_QBS":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "QBS");
          case "R_3KD_QBG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "QBG");
          case "R_3KD_QSG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "QSG");
          case "R_3KD_BSG":
            return CheckSSC_3Start.P_3KD(LotteryNumber, CheckNumber, "BSG");
          case "P_3Z3DS_L":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "L");
          case "P_3Z3DS_C":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "C");
          case "P_3Z3DS_R":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "R");
          case "R_3Z3DS_WQB":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WQB");
          case "R_3Z3DS_WQS":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WQS");
          case "R_3Z3DS_WQG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WQG");
          case "R_3Z3DS_WBS":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WBS");
          case "R_3Z3DS_WBG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WBG");
          case "R_3Z3DS_WSG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "WSG");
          case "R_3Z3DS_QBS":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "QBS");
          case "R_3Z3DS_QBG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "QBG");
          case "R_3Z3DS_QSG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "QSG");
          case "R_3Z3DS_BSG":
            return CheckSSC_3Start.P_3Z3DS(LotteryNumber, CheckNumber, "BSG");
          case "P_3Z6DS_L":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "L");
          case "P_3Z6DS_C":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "C");
          case "P_3Z6DS_R":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "R");
          case "R_3Z6DS_WQB":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WQB");
          case "R_3Z6DS_WQS":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WQS");
          case "R_3Z6DS_WQG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WQG");
          case "R_3Z6DS_WBS":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WBS");
          case "R_3Z6DS_WBG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WBG");
          case "R_3Z6DS_WSG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "WSG");
          case "R_3Z6DS_QBS":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "QBS");
          case "R_3Z6DS_QBG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "QBG");
          case "R_3Z6DS_QSG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "QSG");
          case "R_3Z6DS_BSG":
            return CheckSSC_3Start.P_3Z6DS(LotteryNumber, CheckNumber, "BSG");
          case "P_3ZBDZ3_L":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "L");
          case "P_3ZBDZ3_C":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "C");
          case "P_3ZBDZ3_R":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "R");
          case "R_3ZBDZ3_WQB":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WQB");
          case "R_3ZBDZ3_WQS":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WQS");
          case "R_3ZBDZ3_WQG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WQG");
          case "R_3ZBDZ3_WBS":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WBS");
          case "R_3ZBDZ3_WBG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WBG");
          case "R_3ZBDZ3_WSG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "WSG");
          case "R_3ZBDZ3_QBS":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "QBS");
          case "R_3ZBDZ3_QBG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "QBG");
          case "R_3ZBDZ3_QSG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "QSG");
          case "R_3ZBDZ3_BSG":
            return CheckSSC_3Start.P_3ZBDZ3(LotteryNumber, CheckNumber, "BSG");
          case "P_3ZBDZ6_L":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "L");
          case "P_3ZBDZ6_C":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "C");
          case "P_3ZBDZ6_R":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "R");
          case "R_3ZBDZ6_WQB":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WQB");
          case "R_3ZBDZ6_WQS":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WQS");
          case "R_3ZBDZ6_WQG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WQG");
          case "R_3ZBDZ6_WBS":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WBS");
          case "R_3ZBDZ6_WBG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WBG");
          case "R_3ZBDZ6_WSG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "WSG");
          case "R_3ZBDZ6_QBS":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "QBS");
          case "R_3ZBDZ6_QBG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "QBG");
          case "R_3ZBDZ6_QSG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "QSG");
          case "R_3ZBDZ6_BSG":
            return CheckSSC_3Start.P_3ZBDZ6(LotteryNumber, CheckNumber, "BSG");
          case "P_3QTWS_L":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "L");
          case "P_3QTWS_C":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "C");
          case "P_3QTWS_R":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "R");
          case "R_3QTWS_WQB":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WQB");
          case "R_3QTWS_WQS":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WQS");
          case "R_3QTWS_WQG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WQG");
          case "R_3QTWS_WBS":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WBS");
          case "R_3QTWS_WBG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WBG");
          case "R_3QTWS_WSG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "WSG");
          case "R_3QTWS_QBS":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "QBS");
          case "R_3QTWS_QBG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "QBG");
          case "R_3QTWS_QSG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "QSG");
          case "R_3QTWS_BSG":
            return CheckSSC_3Start.P_3QTWS(LotteryNumber, CheckNumber, "BSG");
          case "P_3QTTS_L":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "L");
          case "P_3QTTS_C":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "C");
          case "P_3QTTS_R":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "R");
          case "R_3QTTS_WQB":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WQB");
          case "R_3QTTS_WQS":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WQS");
          case "R_3QTTS_WQG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WQG");
          case "R_3QTTS_WBS":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WBS");
          case "R_3QTTS_WBG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WBG");
          case "R_3QTTS_WSG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "WSG");
          case "R_3QTTS_QBS":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "QBS");
          case "R_3QTTS_QBG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "QBG");
          case "R_3QTTS_QSG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "QSG");
          case "R_3QTTS_BSG":
            return CheckSSC_3Start.P_3QTTS(LotteryNumber, CheckNumber, "BSG");
          case "P_3ZH_L_WQB":
            return CheckSSC_3Start.P_3ZH_3(LotteryNumber, CheckNumber, "L");
          case "P_3ZH_C_QBS":
            return CheckSSC_3Start.P_3ZH_2(LotteryNumber, CheckNumber, "C");
          case "P_3ZH_R_BSG":
            return CheckSSC_3Start.P_3ZH_1(LotteryNumber, CheckNumber, "R");
          case "P_3ZH_L_QB":
            return CheckSSC_3Start.P_3ZH_3(LotteryNumber, CheckNumber, "L");
          case "P_3ZH_C_BS":
            return CheckSSC_3Start.P_3ZH_2(LotteryNumber, CheckNumber, "C");
          case "P_3ZH_R_SG":
            return CheckSSC_3Start.P_3ZH_1(LotteryNumber, CheckNumber, "R");
          case "P_3ZH_L_B":
            return CheckSSC_3Start.P_3ZH_3(LotteryNumber, CheckNumber, "L");
          case "P_3ZH_C_S":
            return CheckSSC_3Start.P_3ZH_2(LotteryNumber, CheckNumber, "C");
          case "P_3ZH_R_G":
            return CheckSSC_3Start.P_3ZH_1(LotteryNumber, CheckNumber, "R");
          case "P_2FS_L":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "L");
          case "P_2FS_R":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "R");
          case "R_2FS_WQ":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "WQ");
          case "R_2FS_WB":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "WB");
          case "R_2FS_WS":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "WS");
          case "R_2FS_WG":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "WG");
          case "R_2FS_QB":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "QB");
          case "R_2FS_QS":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "QS");
          case "R_2FS_QG":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "QG");
          case "R_2FS_BS":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "BS");
          case "R_2FS_BG":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "BG");
          case "R_2FS_SG":
            return CheckSSC_2Start.P_2ZX(LotteryNumber, CheckNumber, "SG");
          case "P_2DS_L":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "L");
          case "P_2DS_R":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "R");
          case "R_2DS_WQ":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "WQ");
          case "R_2DS_WB":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "WB");
          case "R_2DS_WS":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "WS");
          case "R_2DS_WG":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "WG");
          case "R_2DS_QB":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "QB");
          case "R_2DS_QS":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "QS");
          case "R_2DS_QG":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "QG");
          case "R_2DS_BS":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "BS");
          case "R_2DS_BG":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "BG");
          case "R_2DS_SG":
            return CheckSSC_2Start.P_2DS(LotteryNumber, CheckNumber, "SG");
          case "P_2Z2_L":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "L");
          case "P_2Z2_R":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "R");
          case "R_2Z2_WQ":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "WQ");
          case "R_2Z2_WB":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "WB");
          case "R_2Z2_WS":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "WS");
          case "R_2Z2_WG":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "WG");
          case "R_2Z2_QB":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "QB");
          case "R_2Z2_QS":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "QS");
          case "R_2Z2_QG":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "QG");
          case "R_2Z2_BS":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "BS");
          case "R_2Z2_BG":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "BG");
          case "R_2Z2_SG":
            return CheckSSC_2Start.P_2Z2(LotteryNumber, CheckNumber, "SG");
          case "P_2HE_L":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "L");
          case "P_2HE_R":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "R");
          case "R_2HE_WQ":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "WQ");
          case "R_2HE_WB":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "WB");
          case "R_2HE_WS":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "WS");
          case "R_2HE_WG":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "WG");
          case "R_2HE_QB":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "QB");
          case "R_2HE_QS":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "QS");
          case "R_2HE_QG":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "QG");
          case "R_2HE_BS":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "BS");
          case "R_2HE_BG":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "BG");
          case "R_2HE_SG":
            return CheckSSC_2Start.P_2HE(LotteryNumber, CheckNumber, "SG");
          case "P_2ZHE_L":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "L");
          case "P_2ZHE_R":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "R");
          case "R_2ZHE_WQ":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "WQ");
          case "R_2ZHE_WB":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "WB");
          case "R_2ZHE_WS":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "WS");
          case "R_2ZHE_WG":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "WG");
          case "R_2ZHE_QB":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "QB");
          case "R_2ZHE_QS":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "QS");
          case "R_2ZHE_QG":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "QG");
          case "R_2ZHE_BS":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "BS");
          case "R_2ZHE_BG":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "BG");
          case "R_2ZHE_SG":
            return CheckSSC_2Start.P_2ZHE(LotteryNumber, CheckNumber, "SG");
          case "P_2KD_L":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "L");
          case "P_2KD_R":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "R");
          case "R_2KD_WQ":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "WQ");
          case "R_2KD_WB":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "WB");
          case "R_2KD_WS":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "WS");
          case "R_2KD_WG":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "WG");
          case "R_2KD_QB":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "QB");
          case "R_2KD_QS":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "QS");
          case "R_2KD_QG":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "QG");
          case "R_2KD_BS":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "BS");
          case "R_2KD_BG":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "BG");
          case "R_2KD_SG":
            return CheckSSC_2Start.P_2KD(LotteryNumber, CheckNumber, "SG");
          case "P_2ZDS_L":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "L");
          case "P_2ZDS_R":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "R");
          case "R_2ZDS_WQ":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "WQ");
          case "R_2ZDS_WB":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "WB");
          case "R_2ZDS_WS":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "WS");
          case "R_2ZDS_WG":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "WG");
          case "R_2ZDS_QB":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "QB");
          case "R_2ZDS_QS":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "QS");
          case "R_2ZDS_QG":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "QG");
          case "R_2ZDS_BS":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "BS");
          case "R_2ZDS_BG":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "BG");
          case "R_2ZDS_SG":
            return CheckSSC_2Start.P_2ZDS(LotteryNumber, CheckNumber, "SG");
          case "P_2ZBD_L":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "L");
          case "P_2ZBD_R":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "R");
          case "R_2ZBD_WQ":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "WQ");
          case "R_2ZBD_WB":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "WB");
          case "R_2ZBD_WS":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "WS");
          case "R_2ZBD_WG":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "WG");
          case "R_2ZBD_QB":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "QB");
          case "R_2ZBD_QS":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "QS");
          case "R_2ZBD_QG":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "QG");
          case "R_2ZBD_BS":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "BS");
          case "R_2ZBD_BG":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "BG");
          case "R_2ZBD_SG":
            return CheckSSC_2Start.P_2ZBD(LotteryNumber, CheckNumber, "SG");
          case "P_DD":
          case "P_DD_3":
            return CheckSSC_DDBDD.P_DD(LotteryNumber, CheckNumber);
          case "P_3BDD1_L":
            return CheckSSC_DDBDD.P_3BDD1(LotteryNumber, CheckNumber, "L");
          case "P_3BDD1_R":
            return CheckSSC_DDBDD.P_3BDD1(LotteryNumber, CheckNumber, "R");
          case "P_3BDD2_L":
            return CheckSSC_DDBDD.P_3BDD2(LotteryNumber, CheckNumber, "L");
          case "P_3BDD2_R":
            return CheckSSC_DDBDD.P_3BDD2(LotteryNumber, CheckNumber, "R");
          case "P_4BDD1":
            return CheckSSC_DDBDD.P_4BDD1(LotteryNumber, CheckNumber);
          case "P_4BDD2":
            return CheckSSC_DDBDD.P_4BDD2(LotteryNumber, CheckNumber);
          case "P_5BDD2":
            return CheckSSC_DDBDD.P_5BDD2(LotteryNumber, CheckNumber);
          case "P_5BDD3":
            return CheckSSC_DDBDD.P_5BDD3(LotteryNumber, CheckNumber);
          case "P_BDD_L":
            return CheckSSC_DDBDD.P_BDD(LotteryNumber, CheckNumber, "L");
          case "P_2DXDS_L":
            return CheckSSC_DXDS.P_2DXDS(LotteryNumber, CheckNumber, "L");
          case "P_2DXDS_R":
            return CheckSSC_DXDS.P_2DXDS(LotteryNumber, CheckNumber, "R");
          case "P_5QJ3_1":
            return CheckSSC_QW.P_5QJ3_1(LotteryNumber, CheckNumber);
          case "P_5QJ3_2":
            return CheckSSC_QW.P_5QJ3_2(LotteryNumber, CheckNumber);
          case "P_4QJ3_1":
            return CheckSSC_QW.P_4QJ3_1(LotteryNumber, CheckNumber);
          case "P_4QJ3_2":
            return CheckSSC_QW.P_4QJ3_2(LotteryNumber, CheckNumber);
          case "P_3QJ2_L_1":
            return CheckSSC_QW.P_3QJ2_1(LotteryNumber, CheckNumber, "L");
          case "P_3QJ2_L_2":
            return CheckSSC_QW.P_3QJ2_2(LotteryNumber, CheckNumber, "L");
          case "P_3QJ2_R_1":
            return CheckSSC_QW.P_3QJ2_1(LotteryNumber, CheckNumber, "R");
          case "P_3QJ2_R_2":
            return CheckSSC_QW.P_3QJ2_2(LotteryNumber, CheckNumber, "R");
          case "P_5QW3_1":
            return CheckSSC_QW.P_5QW3_1(LotteryNumber, CheckNumber);
          case "P_5QW3_2":
            return CheckSSC_QW.P_5QW3_2(LotteryNumber, CheckNumber);
          case "P_4QW3_1":
            return CheckSSC_QW.P_4QW3_1(LotteryNumber, CheckNumber);
          case "P_4QW3_2":
            return CheckSSC_QW.P_4QW3_2(LotteryNumber, CheckNumber);
          case "P_3QW2_L_1":
            return CheckSSC_QW.P_3QW2_1(LotteryNumber, CheckNumber, "L");
          case "P_3QW2_L_2":
            return CheckSSC_QW.P_3QW2_2(LotteryNumber, CheckNumber, "L");
          case "P_3QW2_R_1":
            return CheckSSC_QW.P_3QW2_1(LotteryNumber, CheckNumber, "R");
          case "P_3QW2_R_2":
            return CheckSSC_QW.P_3QW2_2(LotteryNumber, CheckNumber, "R");
          case "P_LHH_WQ":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "WQ");
          case "P_LHH_WB":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "WB");
          case "P_LHH_WS":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "WS");
          case "P_LHH_WG":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "WG");
          case "P_LHH_QB":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "QB");
          case "P_LHH_QS":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "QS");
          case "P_LHH_QG":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "QG");
          case "P_LHH_BS":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "BS");
          case "P_LHH_BG":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "BG");
          case "P_LHH_SG":
            return CheckSSC_LHH.P_LHH(LotteryNumber, CheckNumber, "SG");
          case "P11_3FS_L":
            return Check11X5_3Start.P_3ZXFS(LotteryNumber, CheckNumber);
          case "P11_3DS_L":
            return Check11X5_3Start.P_3ZXDS(LotteryNumber, CheckNumber);
          case "P11_3ZFS_L":
            return Check11X5_3Start.P_3ZXFS_Z(LotteryNumber, CheckNumber);
          case "P11_3ZDS_L":
            return Check11X5_3Start.P_3ZXDS_Z(LotteryNumber, CheckNumber);
          case "P11_2FS_L":
            return Check11X5_2Start.P_2ZXFS(LotteryNumber, CheckNumber);
          case "P11_2DS_L":
            return Check11X5_2Start.P_2ZXDS(LotteryNumber, CheckNumber);
          case "P11_2ZFS_L":
            return Check11X5_2Start.P_2ZXFS_Z(LotteryNumber, CheckNumber);
          case "P11_2ZDS_L":
            return Check11X5_2Start.P_2ZXDS_Z(LotteryNumber, CheckNumber);
          case "P11_DD":
            return Check11X5_DDBDD.P_DD(LotteryNumber, CheckNumber);
          case "P11_BDD_L":
            return Check11X5_DDBDD.P_BDD(LotteryNumber, CheckNumber);
          case "P11_RXDS_1":
            return Check11X5_RXDS.P11_RXDS_1(LotteryNumber, CheckNumber);
          case "P11_RXDS_2":
            return Check11X5_RXDS.P11_RXDS_2(LotteryNumber, CheckNumber);
          case "P11_RXDS_3":
            return Check11X5_RXDS.P11_RXDS_3(LotteryNumber, CheckNumber);
          case "P11_RXDS_4":
            return Check11X5_RXDS.P11_RXDS_4(LotteryNumber, CheckNumber);
          case "P11_RXDS_5":
            return Check11X5_RXDS.P11_RXDS_5(LotteryNumber, CheckNumber);
          case "P11_RXDS_6":
            return Check11X5_RXDS.P11_RXDS_6(LotteryNumber, CheckNumber);
          case "P11_RXDS_7":
            return Check11X5_RXDS.P11_RXDS_7(LotteryNumber, CheckNumber);
          case "P11_RXDS_8":
            return Check11X5_RXDS.P11_RXDS_8(LotteryNumber, CheckNumber);
          case "P11_RXFS_1":
            return Check11X5_RXFS.P11_RXFS_1(LotteryNumber, CheckNumber);
          case "P11_RXFS_2":
            return Check11X5_RXFS.P11_RXFS_2(LotteryNumber, CheckNumber);
          case "P11_RXFS_3":
            return Check11X5_RXFS.P11_RXFS_3(LotteryNumber, CheckNumber);
          case "P11_RXFS_4":
            return Check11X5_RXFS.P11_RXFS_4(LotteryNumber, CheckNumber);
          case "P11_RXFS_5":
            return Check11X5_RXFS.P11_RXFS_5(LotteryNumber, CheckNumber);
          case "P11_RXFS_6":
            return Check11X5_RXFS.P11_RXFS_6(LotteryNumber, CheckNumber);
          case "P11_RXFS_7":
            return Check11X5_RXFS.P11_RXFS_7(LotteryNumber, CheckNumber);
          case "P11_RXFS_8":
            return Check11X5_RXFS.P11_RXFS_8(LotteryNumber, CheckNumber);
          case "P11_CDS":
            return Check11X5_CDS.P_CDS(LotteryNumber, CheckNumber);
          case "P11_CZW":
            return Check11X5_CZW.P_CZW(LotteryNumber, CheckNumber);
          case "PK10_One":
            return CheckPK10_1Start.PK10_1FS(LotteryNumber, CheckNumber);
          case "PK10_TwoFS":
            return CheckPK10_2Start.PK10_2FS(LotteryNumber, CheckNumber);
          case "PK10_TwoDS":
            return CheckPK10_2Start.PK10_2DS(LotteryNumber, CheckNumber);
          case "PK10_ThreeFS":
            return CheckPK10_3Start.PK10_3FS(LotteryNumber, CheckNumber);
          case "PK10_ThreeDS":
            return CheckPK10_3Start.PK10_3DS(LotteryNumber, CheckNumber);
          case "PK10_DD1_5":
            return CheckPK10_DDBDD.P_DD1_5(LotteryNumber, CheckNumber);
          case "PK10_DD6_10":
            return CheckPK10_DDBDD.P_DD6_10(LotteryNumber, CheckNumber);
          case "PK10_DXOne":
            return CheckPK10_DXDS.PK10_DX(LotteryNumber, CheckNumber, 1);
          case "PK10_DXTwo":
            return CheckPK10_DXDS.PK10_DX(LotteryNumber, CheckNumber, 2);
          case "PK10_DXThree":
            return CheckPK10_DXDS.PK10_DX(LotteryNumber, CheckNumber, 3);
          case "PK10_DSOne":
            return CheckPK10_DXDS.PK10_DS(LotteryNumber, CheckNumber, 1);
          case "PK10_DSTwo":
            return CheckPK10_DXDS.PK10_DS(LotteryNumber, CheckNumber, 2);
          case "PK10_DSThree":
            return CheckPK10_DXDS.PK10_DS(LotteryNumber, CheckNumber, 3);
          case "wj1":
            return CheckSSC_DN.P_Wj(LotteryNumber, 1);
          case "wj2":
            return CheckSSC_DN.P_Wj(LotteryNumber, 2);
          case "wj3":
            return CheckSSC_DN.P_Wj(LotteryNumber, 3);
          case "wj4":
            return CheckSSC_DN.P_Wj(LotteryNumber, 4);
          case "wj5":
            return CheckSSC_DN.P_Wj(LotteryNumber, 5);
        }
      }
      return 0;
    }
  }
}
