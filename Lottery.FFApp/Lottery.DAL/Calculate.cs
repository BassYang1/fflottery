﻿// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Calculate
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;
using System.Linq;

namespace Lottery.DAL
{
    public class Calculate
    {
        /// <summary>
        /// 检查下注注数和奖金赔率
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lotteryId"></param>
        /// <param name="balls"></param>
        /// <param name="playId"></param>
        /// <param name="pos"></param>
        /// <param name="betnum"></param>
        /// <param name="Point"></param>
        /// <param name="singelBouns"></param>
        /// <returns></returns>
        public static string BetNumerice(int userId, int lotteryId, string balls, string playId, string pos, int betnum, Decimal Point, ref Decimal singelBouns)
        {
            string str1 = "";
            int num1 = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "LotteryId=" + (object)lotteryId;
                if (dbOperHandler.GetField("Sys_LotteryPlaySetting", "Setting").ToString().IndexOf("," + playId + ",") != -1)
                    return Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "UserId=" + (object)userId + " and LotteryId=" + (object)lotteryId;
                if (dbOperHandler.GetField("N_UserPlaySetting", "Setting").ToString().IndexOf("," + playId + ",") != -1)
                    return Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Id=@Id";
                dbOperHandler.AddConditionParameter("@Id", (object)userId.ToString());
                object field = dbOperHandler.GetField("N_User", "Point");
                Decimal num2 = string.IsNullOrEmpty(string.Concat(field)) ? new Decimal(0) : Convert.ToDecimal(string.Concat(field));
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from Sys_PlaySmallType where Id=" + playId;
                DataTable dataTable = dbOperHandler.GetDataTable();
                int int32 = Convert.ToInt32(dataTable.Rows[0]["LotteryId"].ToString());
                str1 = dataTable.Rows[0]["Title2"].ToString();
                Convert.ToDecimal(dataTable.Rows[0]["MaxBonus"].ToString());
                Decimal num3 = Convert.ToDecimal(dataTable.Rows[0]["MinBonus"].ToString());
                Convert.ToDecimal(dataTable.Rows[0]["MinBonus2"].ToString());
                Decimal num4 = Convert.ToDecimal(dataTable.Rows[0]["PosBonus"].ToString());
                string str2 = dataTable.Rows[0]["IsOpen"].ToString();
                num1 = Convert.ToInt32(dataTable.Rows[0]["MaxNum"].ToString());
                if (Convert.ToInt32(lotteryId.ToString().Substring(0, 1)) != int32)
                    return Calculate.JsonResult(0, "投注失败, 投注玩法错误!");
                if (Convert.ToInt32(str2) == 1)
                    return Calculate.JsonResult(0, "投注失败,该玩法已关闭!");
                if (num4 != new Decimal(0))
                {
                    if (lotteryId != 5001)
                    {
                        if (Point > num2 || Point < new Decimal(0))
                            return Calculate.JsonResult(0, "投注失败,返点错误，请重新投注！");
                        singelBouns = Convert.ToDecimal(Convert.ToDecimal(num3 + (num2 - Point * new Decimal(10)) * new Decimal(2) * num4).ToString("0.0000"));
                    }
                }
            }
            int num5;
            switch (str1)
            {
                case "H_ZMP1Z1":
                    num5 = RedCommon4(balls, 1);
                    break;
                case "H_ZMP2Z2":
                    num5 = RedCommon4(balls, 2);
                    break;
                case "H_ZMP3Z2X3":
                    num5 = RedCommon4(balls, 3);
                    break;
                case "H_ZMP3Z3":
                    num5 = RedCommon4(balls, 3);
                    break;
                case "H_ZMP4Z4":
                    num5 = RedCommon4(balls, 4);
                    break;
                case "H_BZ5":
                case "H_BZ6":
                case "H_BZ7":
                case "H_BZ8":
                case "H_BZ9":
                case "H_BZ10":
                case "H_BZ11":
                case "H_BZ12":
                case "H_BZ15":
                    num5 = RedCommon4(balls, Convert.ToInt32(str1.Replace("H_BZ", "")));
                    break;
                case "H_ZMPTX1":
                case "H_ZMPTX2":
                case "H_ZMPTX3":
                case "H_ZMPTX4":
                case "H_ZMPTX5":
                    num5 = RedCommon4(balls, Convert.ToInt32(str1.Replace("H_ZMPTX", "")));
                    break;
                case "H_SXZX":
                case "H_SXZXDS":
                    num5 = 1;
                    break;
                case "H_ZMDX":
                case "H_ZMDS":
                case "H_ZMHSDX":
                case "H_ZMHSDS":
                case "H_ZMSBH":
                case "H_ZMSBLAN":
                case "H_ZMSBLV":
                case "H_ZMWSDX":
                    num5 = RedCommon3(balls);
                    break;
                case "H_TMBT":
                case "H_TMBB":
                case "H_TMBBB":
                    num5 = RedCommon2(balls);
                    break;
                case "H_TM": //六合彩，特码
                case "H_TMDX":
                case "H_TMDS":
                case "H_TMHDX":
                case "H_TMHDS":
                case "H_TMWDX":
                case "H_TMWDS":
                case "H_TMTS":
                case "H_TMWS":
                case "H_TMSB":
                case "H_TMSX":
                case "H_ZHDX":
                case "H_ZHDS":
                    num5 = string.IsNullOrEmpty(balls) ? 0 : balls.Split('_').Length;
                    break;
                case "K_3HZ": //快三
                case "K_3STDX":
                case "K_32TDX":
                    num5 = Calculate.RedK3HZ(balls);
                    break;
                case "K_3STTX":
                case "K_32TTX":
                    num5 = 1;
                    break;
                case "K_32BT": //二不同直选
                    num5 = Calculate.RedK32BT(balls);
                    break;
                case "K_33BT": //三不同直选
                    num5 = Calculate.RedK33BT(balls);
                    break;
                case "K_33LTX": //三连号通选
                    num5 = Calculate.RedK33LTX(balls);
                    break;
                case "P_5ZX120":
                    num5 = Calculate.RedZu120(balls);
                    break;
                case "P_5ZX60":
                    num5 = Calculate.RedZu60(balls);
                    break;
                case "P_5ZX30":
                    num5 = Calculate.RedZu30(balls);
                    break;
                case "P_5ZX20":
                    num5 = Calculate.RedZu20(balls);
                    break;
                case "P_5ZX10":
                    num5 = Calculate.RedZu10(balls);
                    break;
                case "P_5ZX5":
                    num5 = Calculate.RedZu5(balls);
                    break;
                case "P_5TS1":
                case "P_5TS2":
                case "P_5TS3":
                case "P_5TS4":
                    num5 = balls.Contains<char>(',') || balls.Length > 1 && !balls.Contains("_") ? 0 : Calculate.RedTS(balls);
                    break;
                case "P_4ZX24":
                    num5 = Calculate.RedZu24(balls);
                    break;
                case "P_4ZX12":
                    num5 = Calculate.RedZu12(balls);
                    break;
                case "P_4ZX6":
                    num5 = Calculate.RedZu61(balls);
                    break;
                case "P_4ZX4":
                    num5 = Calculate.RedZu4(balls);
                    break;
                case "P_5FS":
                    num5 = balls.Split(',').Length == 5 ? Calculate.RedFS(balls) : 0;
                    break;
                case "P_4FS_L":
                case "P_4FS_R":
                    num5 = balls.Split(',').Length == 4 ? Calculate.RedFS(balls) : 0;
                    break;
                case "P_3FS_L":
                case "P_3FS_C":
                case "P_3FS_R":
                    num5 = balls.Split(',').Length == 3 ? Calculate.RedFS(balls) : 0;
                    break;
                case "P_2FS_L":
                case "P_2FS_R":
                    num5 = balls.Split(',').Length == 2 ? Calculate.RedFS(balls) : 0;
                    break;
                case "P_5DS":
                    num5 = Calculate.RedDS(balls, 5);
                    break;
                case "P_4DS_L":
                case "P_4DS_R":
                    num5 = Calculate.RedDS(balls, 4);
                    break;
                case "P_3DS_L":
                case "P_3DS_C":
                case "P_3DS_R":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "P_2DS_L":
                case "P_2DS_R":
                case "P_2ZDS_L":
                case "P_2ZDS_R":
                    num5 = Calculate.RedDS(balls, 2);
                    break;
                case "P_3HX_L":
                case "P_3HX_C":
                case "P_3HX_R":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "P_3Z3_L":
                case "P_3Z3_C":
                case "P_3Z3_R":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu3(balls);
                    break;
                case "P_3Z6_L":
                case "P_3Z6_C":
                case "P_3Z6_R":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu6(balls);
                    break;
                case "P_2Z2_L":
                case "P_2Z2_R":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu2(balls);
                    break;
                case "P_DD":
                    num5 = balls.Split(',').Length == 5 ? Calculate.RedDD(balls) : 0;
                    bool flag1 = false;
                    string str3 = balls;
                    char[] chArray1 = new char[1] { ',' };
                    foreach (string str2 in str3.Split(chArray1))
                    {
                        if (str2.Length > 8)
                            flag1 = true;
                    }
                    if (flag1)
                        return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
                    break;
                case "P_BDD_C":
                case "P_BDD_L":
                case "P_BDD_R":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedFS(balls);
                    break;
                case "P_2DXDS_L":
                case "P_2DXDS_R":
                case "P_LHH_WQ":
                case "P_LHH_WB":
                case "P_LHH_WS":
                case "P_LHH_WG":
                case "P_LHH_QB":
                case "P_LHH_QS":
                case "P_LHH_QG":
                case "P_LHH_BS":
                case "P_LHH_BG":
                case "P_LHH_SG":
                    num5 = Calculate.RedFS(balls.Replace("_", ""));
                    break;
                case "P_3HE_L":
                case "P_3HE_C":
                case "P_3HE_R":
                    num5 = Calculate.RedHE3(balls);
                    break;
                case "P_3ZHE_L":
                case "P_3ZHE_C":
                case "P_3ZHE_R":
                    num5 = Calculate.RedZHE3(balls);
                    break;
                case "P_3KD_L":
                case "P_3KD_C":
                case "P_3KD_R":
                    num5 = Calculate.Red3KD(balls);
                    break;
                case "P_3Z3DS_L":
                case "P_3Z6DS_L":
                case "P_3Z3DS_C":
                case "P_3Z6DS_C":
                case "P_3Z3DS_R":
                case "P_3Z6DS_R":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "P_3ZBD_L":
                case "P_3ZBD_C":
                case "P_3ZBD_R":
                    num5 = 54;
                    break;
                case "P_3QTWS_L":
                case "P_3QTWS_C":
                case "P_3QTWS_R":
                    num5 = Calculate.RedDD(balls.Replace("_", ""));
                    break;
                case "P_3QTTS_L":
                case "P_3QTTS_C":
                case "P_3QTTS_R":
                    num5 = Calculate.RedDD(balls.Replace("_", "")) / 2;
                    break;
                case "P_2HE_L":
                case "P_2HE_R":
                    num5 = Calculate.RedHE2(balls);
                    break;
                case "P_2ZHE_L":
                case "P_2ZHE_R":
                    num5 = Calculate.RedZHE2(balls);
                    break;
                case "P_2KD_L":
                case "P_2KD_R":
                    num5 = Calculate.Red2KD(balls);
                    break;
                case "P_2ZBD_L":
                case "P_2ZBD_R":
                    num5 = 9;
                    break;
                case "P_5ZH":
                    num5 = Calculate.Red5ZuHe(balls);
                    break;
                case "P_4ZH_L":
                case "P_4ZH_R":
                    num5 = Calculate.Red4ZuHe(balls);
                    break;
                case "P_3ZH_L":
                case "P_3ZH_C":
                case "P_3ZH_R":
                    num5 = Calculate.Red3ZuHe(balls);
                    break;
                case "P_3BDD1_R":
                case "P_3BDD1_L":
                case "P_4BDD1":
                    num5 = Calculate.RedDD(balls.Replace("_", ""));
                    break;
                case "P_3BDD2_R":
                case "P_3BDD2_L":
                case "P_4BDD2":
                case "P_5BDD2":
                    num5 = Calculate.RedZu2(balls.Replace("_", ""));
                    break;
                case "P_5BDD3":
                    num5 = Calculate.RedZu6(balls.Replace("_", ""));
                    break;
                case "P_5QJ3":
                case "P_4QJ3":
                case "P_3QJ2_L":
                case "P_3QJ2_R":
                    num5 = Calculate.RedQwQj(balls);
                    break;
                case "P_5QW3":
                case "P_4QW3":
                case "P_3QW2_L":
                case "P_3QW2_R":
                    num5 = Calculate.RedFS(balls.Replace("_", ""));
                    break;
                case "R_4FS":
                    num5 = Calculate.RedFS_R(balls, pos, 4);
                    break;
                case "R_4DS":
                    num5 = Calculate.RedDS_R(balls, pos, 4);
                    break;
                case "R_4ZX24":
                    num5 = Calculate.RedZu24(balls) * Calculate.Combine(pos.Split('1').Length - 1, 4);
                    break;
                case "R_4ZX12":
                    num5 = Calculate.RedZu12(balls) * Calculate.Combine(pos.Split('1').Length - 1, 4);
                    break;
                case "R_4ZX6":
                    num5 = Calculate.RedZu61(balls) * Calculate.Combine(pos.Split('1').Length - 1, 4);
                    break;
                case "R_4ZX4":
                    num5 = Calculate.RedZu4(balls) * Calculate.Combine(pos.Split('1').Length - 1, 4);
                    break;
                case "R_3FS":
                    num5 = Calculate.RedFS_R(balls, pos, 3);
                    break;
                case "R_3DS":
                    num5 = Calculate.RedDS_R(balls, pos, 3);
                    break;
                case "R_3HX":
                    num5 = Calculate.RedDS_R(balls, pos, 3);
                    break;
                case "R_3Z6":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu6_R(balls, pos, 3);
                    break;
                case "R_3Z3":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu3_R(balls, pos, 3);
                    break;
                case "R_3HE":
                    num5 = Calculate.RedHE3(balls) * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3ZHE":
                    num5 = Calculate.RedZHE3(balls) * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3KD":
                    num5 = Calculate.Red3KD(balls) * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3ZBD":
                    num5 = 54 * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3QTWS":
                    num5 = Calculate.RedDD(balls.Replace("_", "")) * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3QTTS":
                    num5 = Calculate.RedDD(balls.Replace("_", "")) / 2 * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_3Z3DS":
                case "R_3Z6DS":
                    num5 = Calculate.RedDS(balls, 3) * Calculate.Combine(pos.Split('1').Length - 1, 3);
                    break;
                case "R_2FS":
                    num5 = Calculate.RedFS_R(balls, pos, 2);
                    break;
                case "R_2DS":
                    num5 = Calculate.RedDS_R(balls, pos, 2);
                    break;
                case "R_2Z2":
                    num5 = balls.Contains(",") ? 0 : Calculate.RedZu2_R(balls, pos, 2);
                    break;
                case "R_2HE":
                    num5 = Calculate.RedHE2(balls) * Calculate.Combine(pos.Split('1').Length - 1, 2);
                    break;
                case "R_2ZHE":
                    num5 = Calculate.RedZHE2(balls) * Calculate.Combine(pos.Split('1').Length - 1, 2);
                    break;
                case "R_2ZDS":
                    num5 = Calculate.RedDS(balls, 2) * Calculate.Combine(pos.Split('1').Length - 1, 2);
                    break;
                case "R_2KD":
                    num5 = Calculate.Red2KD(balls) * Calculate.Combine(pos.Split('1').Length - 1, 2);
                    break;
                case "R_2ZBD":
                    num5 = 9 * Calculate.Combine(pos.Split('1').Length - 1, 2);
                    break;
                case "P11_RXDS_1":
                    num5 = Calculate.RedDS(balls, 1);
                    break;
                case "P11_RXDS_2":
                    num5 = Calculate.RedDS(balls, 2);
                    break;
                case "P11_RXDS_3":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "P11_RXDS_4":
                    num5 = Calculate.RedDS(balls, 4);
                    break;
                case "P11_RXDS_5":
                    num5 = Calculate.RedDS(balls, 5);
                    break;
                case "P11_RXDS_6":
                    num5 = Calculate.RedDS(balls, 6);
                    break;
                case "P11_RXDS_7":
                    num5 = Calculate.RedDS(balls, 7);
                    break;
                case "P11_RXDS_8":
                    num5 = Calculate.RedDS(balls, 8);
                    break;
                case "P11_RXFS_1":
                    num5 = Calculate.RedRXFS_11(balls, 1);
                    break;
                case "P11_RXFS_2":
                    num5 = Calculate.RedRXFS_11(balls, 2);
                    break;
                case "P11_RXFS_3":
                    num5 = Calculate.RedRXFS_11(balls, 3);
                    break;
                case "P11_RXFS_4":
                    num5 = Calculate.RedRXFS_11(balls, 4);
                    break;
                case "P11_RXFS_5":
                    num5 = Calculate.RedRXFS_11(balls, 5);
                    break;
                case "P11_RXFS_6":
                    num5 = Calculate.RedRXFS_11(balls, 6);
                    break;
                case "P11_RXFS_7":
                    num5 = Calculate.RedRXFS_11(balls, 7);
                    break;
                case "P11_RXFS_8":
                    num5 = Calculate.RedRXFS_11(balls, 8);
                    break;
                case "P11_3FS_L":
                    num5 = Calculate.Red3FS_11(balls);
                    break;
                case "P11_3ZFS_L":
                    num5 = Calculate.Red3ZFS_11(balls);
                    break;
                case "P11_2FS_L":
                    num5 = Calculate.Red2FS_11(balls);
                    break;
                case "P11_2ZFS_L":
                    num5 = Calculate.Red2ZFS_11(balls);
                    break;
                case "P11_3DS_L":
                case "P11_3ZDS_L":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "P11_2DS_L":
                case "P11_2ZDS_L":
                    num5 = Calculate.RedDS(balls, 2);
                    break;
                case "P11_DD":
                    num5 = balls.Split(',').Length == 3 ? Calculate.RedDD_11(balls) : 0;
                    bool flag2 = false;
                    string str4 = balls;
                    char[] chArray2 = new char[1] { ',' };
                    foreach (string str2 in str4.Split(chArray2))
                    {
                        char[] chArray3 = new char[1] { '_' };
                        if (str2.Split(chArray3).Length > 8)
                            ;
                    }
                    if (flag2)
                        return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
                    break;
                case "P11_BDD_L":
                    num5 = balls.Contains<char>(',') || balls.Length > 2 && !balls.Contains("_") ? 0 : Calculate.RedRXFS_11(balls, 1);
                    break;
                case "P11_CDS":
                    num5 = Calculate.RedRXFS_11(balls, 1);
                    break;
                case "P11_CZW":
                    num5 = Calculate.RedRXFS_11(balls, 1);
                    break;
                case "P_DD_3":
                    num5 = Calculate.RedDD(balls);
                    break;
                case "PK10_One":
                    num5 = Calculate.PK10FS_One(balls);
                    break;
                case "PK10_TwoFS":
                    num5 = Calculate.Red2FS_11(balls);
                    break;
                case "PK10_TwoDS":
                    num5 = Calculate.RedDS(balls, 2);
                    break;
                case "PK10_ThreeFS":
                    num5 = Calculate.Red3FS_11(balls);
                    break;
                case "PK10_ThreeDS":
                    num5 = Calculate.RedDS(balls, 3);
                    break;
                case "PK10_DD1_5":
                case "PK10_DD6_10":
                    num5 = Calculate.RedDD_11(balls);
                    bool flag3 = false;
                    string str5 = balls;
                    char[] chArray4 = new char[1] { ',' };
                    foreach (string str2 in str5.Split(chArray4))
                    {
                        char[] chArray3 = new char[1] { '_' };
                        if (str2.Split(chArray3).Length > 8)
                            flag3 = true;
                    }
                    if (flag3)
                        return Calculate.JsonResult(0, "投注失败,定位胆单个位置最多允许投注8码！");
                    break;
                case "PK10_DXOne":
                case "PK10_DXTwo":
                case "PK10_DXThree":
                case "PK10_DSOne":
                case "PK10_DSTwo":
                case "PK10_DSThree":
                    num5 = Calculate.PK10DXDS(balls);
                    break;
                default:
                    num5 = 0;
                    break;
            }
            if (num5 < 1 || betnum != num5)
                return Calculate.JsonResult(0, "投注失败,投注号码不正确，请重新投注！");
            if (num1 < num5)
                return Calculate.JsonResult(0, "投注失败,该玩法最大允许投注" + (object)num1 + "注！");
            return "";
        }
        
        //普通记录
        public static int RedCommon2(string balls)
        {
            var num = 0;

            if (balls != "")
            {
                num = 1;
                string[] strArray2;
                string[] strArray1 = balls.Split(',');

                for (int i = 0; i < strArray1.Length; i++)
                {
                    strArray2 = strArray1[i].Split('_');
                    num *= strArray2.Length;
                }
            }

            return num;
        }

        //普通记录
        public static int RedCommon3(string balls)
        {
            var num = 0;

            if (balls != "")
            {
                string[] strArray2;
                string[] strArray1 = balls.Split(',');

                for (int i = 0; i < strArray1.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArray1[i]))
                    {
                        strArray2 = strArray1[i].Split('_');
                        num += strArray2.Length;
                    }
                }
            }

            return num;
        }

        //组合数
        public static int RedCommon4(string balls, int len)
        {
            int num = 1;
            int repeat = 1;

            if (balls != "")
            {
                string[] strArray2 = balls.Split('_');

                if (strArray2.Length < len)
                {
                    return 0;
                }

                for (int i = 0; i < len; i++)
                {
                    num *= (strArray2.Length - i);
                    repeat *= (len - i);
                }

                return num / repeat;
            }

            return 0;
        }

        //11选5, 任选拖胆
        public static int Red11X5TD(string balls, string type) 
        {
            if (balls != "") {
                var strArray1 = balls.Split(',');
                if (strArray1.Length != 2) {
                    return 0;
                }

                var strArray2 = strArray1[1].Split('_');
                var len = strArray2.Length;

                if (len >= 1 && type == "2") {//拖码1个球
                    return len;
                }
                else if (len >= 2 && type == "3") {//拖码2个球
                    return (len * (len - 1)) / (2 * 1);
                }
                else if (len >= 3 && type == "4") { //拖码3个球
                    return (len * (len - 2) * (len - 1)) / (3 * 2 * 1);
                }
                else if (len >= 4 && type == "5") { //拖码4个球
                    return (len * (len - 3) * (len - 2) * (len - 1)) / (4 * 3 * 2 * 1);
                }
                else if (len >= 5 && type == "6") { //拖码5个球
                    return (len * (len - 4) * (len - 3) * (len - 2) * (len - 1)) / (5 * 4 * 3 * 2 * 1);
                }
                else if (len >= 6 && type == "7") { //拖码6个球
                    return (len * (len - 5) * (len - 4) * (len - 3) * (len - 2) * (len - 1)) / (6 * 5 * 4 * 3 * 2 * 1);
                }
                else if (len >= 7 && type == "8") { //拖码7个球
                    return (len * (len - 6) * (len - 5) * (len - 4) * (len - 3) * (len - 2) * (len - 1)) / (7 * 6 * 5 * 4 * 3 * 2 * 1);
                }
            }

            return 0;
        }

        //快三和值
        public static int RedK3HZ(string balls)
        {
            if (balls != "")
            {
                var strArray2 = balls.Split('_');
                return strArray2.Length;
            }
            return 0;
        }
        
        //快三,二不同直选
         public static int  RedK32BT(string balls) {
            if (balls != "") {
                var strArray2 = balls.Split('_');
                return (strArray2.Length * (strArray2.Length - 1)) / 2;
            }

            return 0;
        }

         //快三,三不同直选
         public static int RedK33BT(string balls)
         {
             if (balls != "")
             {
                 var strArray2 = balls.Split('_');
                 return (strArray2.Length * (strArray2.Length - 2) * (strArray2.Length - 1)) / 6;
             }

             return 0;
         }

         //快三,三连号通选
         public static int RedK33LTX(string balls)
         {
             if (balls != "")
             {
                 var strArray2 = balls.Split('_');
                 return (strArray2.Length * (strArray2.Length - 2) * (strArray2.Length - 1)) / 6;
             }

             return 0;
         }

        public static int RedFS(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            int num = 1;
            for (int index = 0; index < strArray.Length; ++index)
            {
                int length = strArray[index].Length;
                num *= length;
            }
            return num;
        }

        public static int RedDS(string balls, int num)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].Split(' ').Length > num)
                    return 0;
            }
            return strArray.Length;
        }

        public static int Pareto(int n, int r)
        {
            int num = 1;
            for (int index = n; index != n - r; --index)
                num *= index;
            return num;
        }

        public static int Combine(int n, int r)
        {
            return Calculate.Pareto(n, r) / Calculate.Pareto(r, r);
        }

        public static int RedZu6(string balls)
        {
            if (balls != "")
                return Calculate.Pareto(balls.Length, 3) / Calculate.Pareto(3, 3);
            return 0;
        }

        public static int RedZu3(string balls)
        {
            if (balls != "")
                return Calculate.Pareto(balls.Length, 2);
            return 0;
        }

        public static int RedZu2(string balls)
        {
            if (!(balls != ""))
                return 0;
            int length = balls.Length;
            return length * (length - 1) / 2;
        }

        public static int RedDD(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            int num = 0;
            for (int index = 0; index < strArray.Length; ++index)
            {
                int length = strArray[index].Length;
                num += length;
            }
            return num;
        }

        public static int RedFS_R(string balls, string p, int PlayWzNum)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            int num = 1;
            for (int index = 0; index < strArray.Length; ++index)
            {
                int length = strArray[index].Length;
                num *= length;
            }
            int n = p.Split('1').Length - 1;
            return num * Calculate.Combine(n, PlayWzNum);
        }

        public static int RedDS_R(string balls, string p, int PlayWzNum)
        {
            if (!(balls != ""))
                return 0;
            return balls.Split(',').Length * Calculate.Combine(p.Split('1').Length - 1, PlayWzNum);
        }

        public static int RedZu6_R(string balls, string p, int PlayWzNum)
        {
            if (!(balls != ""))
                return 0;
            return Calculate.Pareto(balls.Length, 3) / Calculate.Pareto(3, 3) * Calculate.Combine(p.Split('1').Length - 1, PlayWzNum);
        }

        public static int RedZu3_R(string balls, string p, int PlayWzNum)
        {
            if (!(balls != ""))
                return 0;
            return Calculate.Pareto(balls.Length, 2) * Calculate.Combine(p.Split('1').Length - 1, PlayWzNum);
        }

        public static int RedZu2_R(string balls, string p, int PlayWzNum)
        {
            if (!(balls != ""))
                return 0;
            int length = balls.Length;
            int n = p.Split('1').Length - 1;
            return length * (length - 1) / 2 * Calculate.Combine(n, PlayWzNum);
        }

        public static int RedHE3(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 0)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 1)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 15;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 21;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 28;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 36;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 45;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 55;
                    if (Convert.ToInt32(strArray[index]) == 10)
                        num += 63;
                    if (Convert.ToInt32(strArray[index]) == 11)
                        num += 69;
                    if (Convert.ToInt32(strArray[index]) == 12)
                        num += 73;
                    if (Convert.ToInt32(strArray[index]) == 13)
                        num += 75;
                    if (Convert.ToInt32(strArray[index]) == 14)
                        num += 75;
                    if (Convert.ToInt32(strArray[index]) == 15)
                        num += 73;
                    if (Convert.ToInt32(strArray[index]) == 16)
                        num += 69;
                    if (Convert.ToInt32(strArray[index]) == 17)
                        num += 63;
                    if (Convert.ToInt32(strArray[index]) == 18)
                        num += 55;
                    if (Convert.ToInt32(strArray[index]) == 19)
                        num += 45;
                    if (Convert.ToInt32(strArray[index]) == 20)
                        num += 36;
                    if (Convert.ToInt32(strArray[index]) == 21)
                        num += 28;
                    if (Convert.ToInt32(strArray[index]) == 22)
                        num += 21;
                    if (Convert.ToInt32(strArray[index]) == 23)
                        num += 15;
                    if (Convert.ToInt32(strArray[index]) == 24)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 25)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 26)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 27)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZHE3(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 1)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 5;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 8;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 11;
                    if (Convert.ToInt32(strArray[index]) == 10)
                        num += 13;
                    if (Convert.ToInt32(strArray[index]) == 11)
                        num += 14;
                    if (Convert.ToInt32(strArray[index]) == 12)
                        num += 14;
                    if (Convert.ToInt32(strArray[index]) == 13)
                        num += 15;
                    if (Convert.ToInt32(strArray[index]) == 14)
                        num += 15;
                    if (Convert.ToInt32(strArray[index]) == 15)
                        num += 14;
                    if (Convert.ToInt32(strArray[index]) == 16)
                        num += 14;
                    if (Convert.ToInt32(strArray[index]) == 17)
                        num += 13;
                    if (Convert.ToInt32(strArray[index]) == 18)
                        num += 11;
                    if (Convert.ToInt32(strArray[index]) == 19)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 20)
                        num += 8;
                    if (Convert.ToInt32(strArray[index]) == 21)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 22)
                        num += 5;
                    if (Convert.ToInt32(strArray[index]) == 23)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 24)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 25)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 26)
                        ++num;
                }
            }
            return num;
        }

        public static int Red3KD(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 0)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 1)
                        num += 54;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        num += 96;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 126;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 144;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 150;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 144;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 126;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 96;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 54;
                }
            }
            return num;
        }

        public static int RedHE2(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 0)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 1)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 5;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 7;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 8;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 9;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 10)
                        num += 9;
                    if (Convert.ToInt32(strArray[index]) == 11)
                        num += 8;
                    if (Convert.ToInt32(strArray[index]) == 12)
                        num += 7;
                    if (Convert.ToInt32(strArray[index]) == 13)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 14)
                        num += 5;
                    if (Convert.ToInt32(strArray[index]) == 15)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 16)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 17)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 18)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZHE2(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 1)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 5;
                    if (Convert.ToInt32(strArray[index]) == 10)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 11)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 12)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 13)
                        num += 3;
                    if (Convert.ToInt32(strArray[index]) == 14)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 15)
                        num += 2;
                    if (Convert.ToInt32(strArray[index]) == 16)
                        ++num;
                    if (Convert.ToInt32(strArray[index]) == 17)
                        ++num;
                }
            }
            return num;
        }

        public static int Red2KD(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                string[] strArray = balls.Split('_');
                for (int index = 0; index != strArray.Length; ++index)
                {
                    if (Convert.ToInt32(strArray[index]) == 0)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 1)
                        num += 18;
                    if (Convert.ToInt32(strArray[index]) == 2)
                        num += 16;
                    if (Convert.ToInt32(strArray[index]) == 3)
                        num += 14;
                    if (Convert.ToInt32(strArray[index]) == 4)
                        num += 12;
                    if (Convert.ToInt32(strArray[index]) == 5)
                        num += 10;
                    if (Convert.ToInt32(strArray[index]) == 6)
                        num += 8;
                    if (Convert.ToInt32(strArray[index]) == 7)
                        num += 6;
                    if (Convert.ToInt32(strArray[index]) == 8)
                        num += 4;
                    if (Convert.ToInt32(strArray[index]) == 9)
                        num += 2;
                }
            }
            return num;
        }

        public static int RedQwQj(string balls)
        {
            if (!(balls != ""))
                return 0;
            balls = balls.Replace("_", "");
            string[] strArray = balls.Split(',');
            int num1 = 1;
            if (strArray.Length == 5)
            {
                int num2 = 1;
                for (int index = 2; index < strArray.Length; ++index)
                {
                    int length = strArray[index].Length;
                    num2 *= length;
                }
                num1 = num2 * strArray[0].Length / 2 * strArray[1].Length / 2;
            }
            if (strArray.Length == 4)
            {
                int num2 = 1;
                for (int index = 1; index < strArray.Length; ++index)
                {
                    int length = strArray[index].Length;
                    num2 *= length;
                }
                num1 = num2 * strArray[0].Length / 2;
            }
            if (strArray.Length == 3)
            {
                int num2 = 1;
                for (int index = 1; index < strArray.Length; ++index)
                {
                    int length = strArray[index].Length;
                    num2 *= length;
                }
                num1 = num2 * strArray[0].Length / 2;
            }
            return num1;
        }

        public static int Red5ZuHe(string balls)
        {
            if (!(balls != ""))
                return 0;
            balls = balls.Replace("_", "");
            string[] strArray = balls.Split(',');
            if (strArray.Length < 5)
                return 0;
            int num1 = 1;
            int num2 = 1;
            int num3 = 1;
            int num4 = 1;
            int num5 = 1;
            for (int index = 0; index < strArray.Length; ++index)
                num1 *= strArray[index].Length;
            for (int index = 1; index < strArray.Length; ++index)
                num2 *= strArray[index].Length;
            for (int index = 2; index < strArray.Length; ++index)
                num3 *= strArray[index].Length;
            for (int index = 3; index < strArray.Length; ++index)
                num4 *= strArray[index].Length;
            for (int index = 4; index < strArray.Length; ++index)
                num5 *= strArray[index].Length;
            return num1 + num2 + num3 + num4 + num5;
        }

        public static int Red4ZuHe(string balls)
        {
            if (!(balls != ""))
                return 0;
            balls = balls.Replace("_", "");
            string[] strArray = balls.Split(',');
            if (strArray.Length < 4)
                return 0;
            int num1 = 1;
            int num2 = 1;
            int num3 = 1;
            int num4 = 1;
            for (int index = 0; index < strArray.Length; ++index)
                num1 *= strArray[index].Length;
            for (int index = 1; index < strArray.Length; ++index)
                num2 *= strArray[index].Length;
            for (int index = 2; index < strArray.Length; ++index)
                num3 *= strArray[index].Length;
            for (int index = 3; index < strArray.Length; ++index)
                num4 *= strArray[index].Length;
            return num1 + num2 + num3 + num4;
        }

        public static int Red3ZuHe(string balls)
        {
            if (!(balls != ""))
                return 0;
            balls = balls.Replace("_", "");
            string[] strArray = balls.Split(',');
            if (strArray.Length < 3)
                return 0;
            int num1 = 1;
            int num2 = 1;
            int num3 = 1;
            for (int index = 0; index < strArray.Length; ++index)
                num1 *= strArray[index].Length;
            for (int index = 1; index < strArray.Length; ++index)
                num2 *= strArray[index].Length;
            for (int index = 2; index < strArray.Length; ++index)
                num3 *= strArray[index].Length;
            return num1 + num2 + num3;
        }

        public static int RedRXFS_11(string balls, int num)
        {
            if (!(balls != ""))
                return 0;
            return Calculate.Combine(balls.Split('_').Length, num);
        }

        public static int Red3FS_11(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                if (!balls.Contains(","))
                    return 0;
                string[] strArray1 = balls.Split(',');
                if (strArray1.Length == 3)
                {
                    string[] strArray2 = strArray1[0].Split('_');
                    string[] strArray3 = strArray1[1].Split('_');
                    string[] strArray4 = strArray1[2].Split('_');
                    for (int index1 = 0; index1 < strArray2.Length; ++index1)
                    {
                        if (strArray2[index1].Length > 2)
                            return 0;
                        for (int index2 = 0; index2 < strArray3.Length; ++index2)
                        {
                            if (strArray3[index2].Length > 2)
                                return 0;
                            if (strArray2[index1] != strArray3[index2])
                            {
                                for (int index3 = 0; index3 < strArray4.Length; ++index3)
                                {
                                    if (strArray4[index3].Length > 2)
                                        return 0;
                                    if (strArray4[index3] != strArray2[index1] && strArray4[index3] != strArray3[index2])
                                        ++num;
                                }
                            }
                        }
                    }
                }
            }
            return num;
        }

        public static int Red3ZFS_11(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split('_');
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].Length > 2)
                    return 0;
            }
            return Calculate.Combine(strArray.Length, 3);
        }

        public static int Red2FS_11(string balls)
        {
            int num = 0;
            if (balls != "")
            {
                if (!balls.Contains(","))
                    return 0;
                string[] strArray1 = balls.Split(',');
                if (strArray1.Length == 2)
                {
                    string[] strArray2 = strArray1[0].Split('_');
                    string[] strArray3 = strArray1[1].Split('_');
                    for (int index1 = 0; index1 < strArray2.Length; ++index1)
                    {
                        if (strArray2[index1].Length > 2)
                            return 0;
                        for (int index2 = 0; index2 < strArray3.Length; ++index2)
                        {
                            if (strArray3[index2].Length > 2)
                                return 0;
                            if (strArray2[index1] != strArray3[index2])
                                ++num;
                        }
                    }
                }
            }
            return num;
        }

        public static int Red2ZFS_11(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split('_');
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].Length > 2)
                    return 0;
            }
            return strArray.Length * (strArray.Length - 1) / 2;
        }

        public static int RedDD_11(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            int num = 0;
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index] != "")
                {
                    int length = strArray[index].Split('_').Length;
                    num += length;
                }
            }
            return num;
        }

        public static int RedZH(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split(',');
            int num = 5;
            for (int index = 0; index < strArray.Length; ++index)
            {
                int length = strArray[index].Length;
                num *= length;
            }
            return num;
        }

        public static int RedZu120(string balls)
        {
            if (balls != "")
                return Calculate.Pareto(balls.Replace("_", "").Length, 5) / Calculate.Pareto(5, 5);
            return 0;
        }

        public static int RedZu60(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index1 = 0; index1 < strArray3.Length; ++index1)
            {
                for (int index2 = index1; index2 < strArray3.Length; ++index2)
                {
                    for (int index3 = index2; index3 < strArray3.Length; ++index3)
                    {
                        if (strArray3[index1] != strArray3[index2] && strArray3[index2] != strArray3[index3] && strArray3[index3] != strArray3[index1])
                            str1 = str1 + strArray3[index1] + strArray3[index2] + strArray3[index3] + ",";
                    }
                }
            }
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZu30(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index1 = 0; index1 < strArray2.Length; ++index1)
            {
                for (int index2 = index1; index2 < strArray2.Length; ++index2)
                {
                    if (strArray2[index1] != strArray2[index2])
                        str1 = str1 + strArray2[index1] + strArray2[index2] + ",";
                }
            }
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray3.Length; ++index)
                {
                    if (str3.IndexOf(strArray3[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZu20(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index1 = 0; index1 < strArray3.Length; ++index1)
            {
                for (int index2 = index1; index2 < strArray3.Length; ++index2)
                {
                    if (strArray3[index1] != strArray3[index2])
                        str1 = str1 + strArray3[index1] + strArray3[index2] + ",";
                }
            }
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZu10(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index = 0; index < strArray3.Length; ++index)
                str1 = str1 + strArray3[index] + ",";
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZu5(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index = 0; index < strArray3.Length; ++index)
                str1 = str1 + strArray3[index] + ",";
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedTS(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray = balls.Split('_');
            int num = 0;
            for (int index = 0; index < strArray.Length; ++index)
            {
                int length = strArray[index].Length;
                num += length;
            }
            return num;
        }

        public static int RedZu24(string balls)
        {
            if (balls != "")
                return Calculate.Pareto(balls.Replace("_", "").Length, 4) / Calculate.Pareto(4, 4);
            return 0;
        }

        public static int RedZu12(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index1 = 0; index1 < strArray3.Length; ++index1)
            {
                for (int index2 = index1; index2 < strArray3.Length; ++index2)
                {
                    if (strArray3[index1] != strArray3[index2])
                        str1 = str1 + strArray3[index1] + strArray3[index2] + ",";
                }
            }
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int RedZu61(string balls)
        {
            if (balls != "")
                return Calculate.Pareto(balls.Replace("_", "").Length, 2) / Calculate.Pareto(2, 2);
            return 0;
        }

        public static int RedZu4(string balls)
        {
            if (!(balls != ""))
                return 0;
            string[] strArray1 = balls.Split(',');
            if (strArray1.Length <= 1)
                return 0;
            string[] strArray2 = strArray1[0].Split('_');
            string[] strArray3 = strArray1[1].Split('_');
            int num = 0;
            string str1 = "";
            for (int index = 0; index < strArray3.Length; ++index)
                str1 = str1 + strArray3[index] + ",";
            string str2 = str1.Substring(0, str1.Length - 1);
            char[] chArray = new char[1] { ',' };
            foreach (string str3 in str2.Split(chArray))
            {
                for (int index = 0; index < strArray2.Length; ++index)
                {
                    if (str3.IndexOf(strArray2[index]) == -1)
                        ++num;
                }
            }
            return num;
        }

        public static int PK10FS_One(string balls)
        {
            if (!(balls != ""))
                return 0;
            return Calculate.Combine(balls.Split('_').Length, 1);
        }

        public static int PK10DXDS(string balls)
        {
            if (!(balls != ""))
                return 0;
            return 1 * balls.Split('_').Length;
        }

        protected static string JsonResult(int success, string str)
        {
            return "[{\"result\" :\"" + success.ToString() + "\",\"returnval\" :\"" + str + "\"}]";
        }
    }
}
