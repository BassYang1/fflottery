// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserBetDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Lottery.DAL.Flex
{
    public class UserBetDAL : ComData
    {
        protected SiteModel site;

        public UserBetDAL()
        {
            this.site = new conSite().GetSite();
        }
        
        /// <summary>
        /// 检查投注时间
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <returns>是否是有效开奖时段</returns>
        public bool CheckBetTime(int lotteryId)
        {
            if (lotteryId == 1001)
            {
                DateTime time = DateTime.Now;
                DateTime stime = Convert.ToDateTime("01:55:00");
                DateTime etime = Convert.ToDateTime("09:50:00");

                if (time >= stime && time <= etime)
                {
                    return false;
                }
            }

            return true;
        }

        public string CheckBet(int UserId, int lotteryId, Decimal betSumTotal, DateTime STime)
        {
            if (UserId == 0)
                return "投注失败,请重新登录后再进行投注!";
            if (this.site.BetIsOpen == 1)
                return "系统正在维护，不能投注！";
            if (betSumTotal <= new Decimal(0))
                return "投注失败,您的帐号余额不足!";
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from Sys_Lottery where Id=" + (object)lotteryId;
                DataTable dataTable1 = dbOperHandler.GetDataTable();
                int int32_1 = Convert.ToInt32(dataTable1.Rows[0]["IsOpen"]);
                Convert.ToInt32(dataTable1.Rows[0]["Ltype"]);
                int int32_2 = Convert.ToInt32(dataTable1.Rows[0]["CloseTime"]);
                Convert.ToDecimal(dataTable1.Rows[0]["MinTimes"]);
                Convert.ToDecimal(dataTable1.Rows[0]["MaxTimes"]);
                if (int32_1 != 0)
                    return "暂停投注，请与客服联系。";
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select DATEDIFF(S,GETDATE(),'" + STime.ToString("yyyy-MM-dd HH:mm:ss") + "') as time,GETDATE() as now";
                DataTable dataTable2 = dbOperHandler.GetDataTable();
                if (Convert.ToDateTime(dataTable2.Rows[0]["now"]) > STime || Convert.ToInt32(dataTable2.Rows[0]["time"]) <= int32_2)
                    return "本期已封单,不能投注!";
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select Money,IsEnable,IsBet,IsDelay,Point,EnableSeason from N_User where Id=" + (object)UserId;
                DataTable dataTable3 = dbOperHandler.GetDataTable();
                int int32_3 = Convert.ToInt32(dataTable3.Rows[0]["IsBet"]);
                int int32_4 = Convert.ToInt32(dataTable3.Rows[0]["IsEnable"]);
                Convert.ToInt32(dataTable3.Rows[0]["IsDelay"]);
                Decimal num1 = string.IsNullOrEmpty(string.Concat(dataTable3.Rows[0]["Point"])) ? new Decimal(0) : Convert.ToDecimal(dataTable3.Rows[0]["Point"]);
                Decimal num2 = Convert.ToDecimal(dataTable3.Rows[0]["Money"]);
                dataTable3.Rows[0]["EnableSeason"].ToString();
                if (int32_4 != 0)
                    return "当前帐号无法投注，请联系客服处理!";
                if (int32_3 != 0)
                    return "投注失败,您的帐号禁止投注!";
                if (num1 >= this.site.MaxLevel * new Decimal(10))
                    return "当前帐号无法投注，请联系客服处理!";
                if (num2 < betSumTotal)
                    return "投注失败,您的帐号余额不足!";
                if (betSumTotal > this.site.MaxBet)
                    return "系统设置最大投注额不能超过" + (object)this.site.MaxBet + "元！";
            }
            return string.Empty;
        }

        public int InsertBet(UserBetModel model, string Source)
        {
            int logSysId = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    string bet = SsId.Bet;
                    Decimal Money = Convert.ToDecimal(model.SingleMoney * (Decimal)model.Num * model.Times);
                    if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), Money, model.LotteryId, model.PlayId, logSysId, 3, 99, string.Empty, string.Empty, "会员投注", "") <= 0)
                        return 0;
                    SqlParameter[] values = new SqlParameter[21]
                      {
                        new SqlParameter("@SsId", (object) bet),
                        new SqlParameter("@UserId", (object) model.UserId),
                        new SqlParameter("@UserMoney", (object) model.UserMoney),
                        new SqlParameter("@LotteryId", (object) model.LotteryId),
                        new SqlParameter("@PlayId", (object) model.PlayId),
                        new SqlParameter("@IssueNum", (object) model.IssueNum),
                        new SqlParameter("@SingleMoney", (object) model.SingleMoney),
                        new SqlParameter("@Num", (object) model.Num),
                        new SqlParameter("@Detail", (object) ""),
                        new SqlParameter("@Total", (object) (model.SingleMoney * (Decimal) model.Num)),
                        new SqlParameter("@Point", (object) model.Point),
                        new SqlParameter("@PointMoney", (object) (model.SingleMoney * (Decimal) model.Num * model.Point / new Decimal(100))),
                        new SqlParameter("@Bonus", (object) model.Bonus),
                        new SqlParameter("@Pos", (object) model.Pos),
                        new SqlParameter("@PlayCode", (object) model.PlayCode),
                        new SqlParameter("@STime", (object) model.STime),
                        new SqlParameter("@STime2", (object) model.STime2),
                        new SqlParameter("@IsDelay", (object) model.IsDelay),
                        new SqlParameter("@Times", (object) model.Times),
                        new SqlParameter("@ZhId", (object) "0"),
                        new SqlParameter("@Source", (object) Source)
                      };
                    sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                    sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                    sqlCommand.Parameters.AddRange(values);
                    logSysId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    sqlCommand.Parameters.Clear();
                    BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), logSysId.ToString(), model.Detail.Replace("|", "#"));
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                    logSysId = 0;
                }
            }
            return logSysId;
        }

        public int InsertBetPos(UserBetModel model, string Source)
        {
            int logSysId = 0;
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    string str1 = "";
                    string[] strArray1 = model.Pos.Split(',');
                    string str2 = model.PlayCode + "_";
                    switch (model.PlayCode)
                    {
                        case "R_4FS":
                        case "R_4DS":
                        case "R_4ZX24":
                        case "R_4ZX12":
                        case "R_4ZX6":
                        case "R_4ZX4":
                            if (model.Pos != "")
                            {
                                int count = Regex.Matches(model.Pos, "1").Count;
                                if (count == 4)
                                    str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                if (count == 5)
                                {
                                    string[] strArray2 = "W,Q,B,S,G".Split(',');
                                    for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                    {
                                        for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                        {
                                            for (int index3 = index2 + 1; index3 < strArray2.Length; ++index3)
                                            {
                                                for (int index4 = index3 + 1; index4 < strArray2.Length; ++index4)
                                                    str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + strArray2[index3] + strArray2[index4] + ",";
                                            }
                                        }
                                    }
                                    break;
                                }
                                break;
                            }
                            break;
                        case "R_3FS":
                        case "R_3DS":
                        case "R_3Z3":
                        case "R_3Z6":
                        case "R_3HX":
                        case "R_3HE":
                        case "R_3ZHE":
                        case "R_3KD":
                        case "R_3ZBD":
                        case "R_3QTWS":
                        case "R_3QTTS":
                        case "R_3Z3DS":
                        case "R_3Z6DS":
                            if (model.Pos != "")
                            {
                                int count = Regex.Matches(model.Pos, "1").Count;
                                if (count == 3)
                                    str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                if (count >= 4)
                                {
                                    string str3 = "" + (strArray1[0].Equals("1") ? "W," : "") + (strArray1[1].Equals("1") ? "Q," : "") + (strArray1[2].Equals("1") ? "B," : "") + (strArray1[3].Equals("1") ? "S," : "") + (strArray1[4].Equals("1") ? "G," : "");
                                    string[] strArray2 = str3.Substring(0, str3.Length - 1).Split(',');
                                    for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                    {
                                        for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                        {
                                            for (int index3 = index2 + 1; index3 < strArray2.Length; ++index3)
                                                str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + strArray2[index3] + ",";
                                        }
                                    }
                                    break;
                                }
                                break;
                            }
                            break;
                        case "R_2FS":
                        case "R_2DS":
                        case "R_2Z2":
                        case "R_2HE":
                        case "R_2ZHE":
                        case "R_2ZDS":
                        case "R_2KD":
                        case "R_2ZBD":
                            if (model.Pos != "")
                            {
                                int count = Regex.Matches(model.Pos, "1").Count;
                                if (count == 2)
                                    str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                if (count >= 3)
                                {
                                    string str3 = "" + (strArray1[0].Equals("1") ? "W," : "") + (strArray1[1].Equals("1") ? "Q," : "") + (strArray1[2].Equals("1") ? "B," : "") + (strArray1[3].Equals("1") ? "S," : "") + (strArray1[4].Equals("1") ? "G," : "");
                                    string[] strArray2 = str3.Substring(0, str3.Length - 1).Split(',');
                                    for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                    {
                                        for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                            str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + ",";
                                    }
                                    break;
                                }
                                break;
                            }
                            break;
                    }
                    string[] strArray3 = str1.Substring(0, str1.Length - 1).Split(',');
                    for (int index = 0; index < strArray3.Length; ++index)
                    {
                        string bet = SsId.Bet;
                        Decimal Money = Convert.ToDecimal(model.SingleMoney * (Decimal)model.Num * model.Times / (Decimal)strArray3.Length);
                        if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), Money, model.LotteryId, model.PlayId, logSysId, 3, 99, string.Empty, string.Empty, "会员投注", "") > 0)
                        {
                            SqlParameter[] values = new SqlParameter[21]
              {
                new SqlParameter("@SsId", (object) bet),
                new SqlParameter("@UserId", (object) model.UserId),
                new SqlParameter("@UserMoney", (object) model.UserMoney),
                new SqlParameter("@LotteryId", (object) model.LotteryId),
                new SqlParameter("@PlayId", (object) model.PlayId),
                new SqlParameter("@IssueNum", (object) model.IssueNum),
                new SqlParameter("@SingleMoney", (object) model.SingleMoney),
                new SqlParameter("@Num", (object) (model.Num / strArray3.Length)),
                new SqlParameter("@Detail", (object) ""),
                new SqlParameter("@Total", (object) (model.SingleMoney * (Decimal) model.Num / (Decimal) strArray3.Length)),
                new SqlParameter("@Point", (object) model.Point),
                new SqlParameter("@PointMoney", (object) (model.SingleMoney * (Decimal) model.Num * model.Point / (Decimal) strArray3.Length / new Decimal(100))),
                new SqlParameter("@Bonus", (object) model.Bonus),
                new SqlParameter("@Pos", (object) ""),
                new SqlParameter("@PlayCode", (object) strArray3[index]),
                new SqlParameter("@STime", (object) model.STime),
                new SqlParameter("@STime2", (object) model.STime2),
                new SqlParameter("@IsDelay", (object) model.IsDelay),
                new SqlParameter("@Times", (object) model.Times),
                new SqlParameter("@ZhId", (object) "0"),
                new SqlParameter("@Source", (object) Source)
              };
                            sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                            sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                            sqlCommand.Parameters.AddRange(values);
                            logSysId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                            sqlCommand.Parameters.Clear();
                            ++num;
                            BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), logSysId.ToString(), model.Detail.Replace("|", "#"));
                        }
                    }
                    logSysId = num < strArray3.Length ? 0 : 1;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                    logSysId = 0;
                }
            }
            return logSysId;
        }

        public int InsertBetZH(UserBetModel model, string Source)
        {
            int logSysId = 0;
            int num1 = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    string str1 = "";
                    string str2 = "";
                    string str3 = "";
                    if (model.PlayCode == "P_5ZH")
                    {
                        str1 = "P_5ZH_WQBSG,P_5ZH_QBSG,P_5ZH_BSG,P_5ZH_SG,P_5ZH_G";
                        str2 = "1,10,100,1000,10000";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        int num5 = 1;
                        int num6 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        for (int index = 3; index < strArray.Length; ++index)
                            num5 *= strArray[index].Length;
                        for (int index = 4; index < strArray.Length; ++index)
                            num6 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5 + "," + (object)num6;
                    }
                    if (model.PlayCode == "P_4ZH_L")
                    {
                        str1 = "P_4ZH_L_WQBS,P_4ZH_L_QBS,P_4ZH_L_BS,P_4ZH_L_S";
                        str2 = "1,10,100,1000";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        int num5 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        for (int index = 3; index < strArray.Length; ++index)
                            num5 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5;
                    }
                    if (model.PlayCode == "P_4ZH_R")
                    {
                        str1 = "P_4ZH_R_QBSG,P_4ZH_R_BSG,P_4ZH_R_SG,P_4ZH_R_G";
                        str2 = "1,10,100,1000";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        int num5 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        for (int index = 3; index < strArray.Length; ++index)
                            num5 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5;
                    }
                    if (model.PlayCode == "P_3ZH_L")
                    {
                        str1 = "P_3ZH_L_WQB,P_3ZH_L_QB,P_3ZH_L_B";
                        str2 = "1,10,100";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                    }
                    if (model.PlayCode == "P_3ZH_C")
                    {
                        str1 = "P_3ZH_C_QBS,P_3ZH_C_BS,P_3ZH_C_S";
                        str2 = "1,10,100";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                    }
                    if (model.PlayCode == "P_3ZH_R")
                    {
                        str1 = "P_3ZH_R_BSG,P_3ZH_R_SG,P_3ZH_R_G";
                        str2 = "1,10,100";
                        string[] strArray = model.Detail.Replace("_", "").Split(',');
                        int num2 = 1;
                        int num3 = 1;
                        int num4 = 1;
                        for (int index = 0; index < strArray.Length; ++index)
                            num2 *= strArray[index].Length;
                        for (int index = 1; index < strArray.Length; ++index)
                            num3 *= strArray[index].Length;
                        for (int index = 2; index < strArray.Length; ++index)
                            num4 *= strArray[index].Length;
                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                    }
                    string[] strArray1 = str1.Split(',');
                    string[] strArray2 = str2.Split(',');
                    string[] strArray3 = str3.Split(',');
                    for (int index = 0; index < strArray1.Length; ++index)
                    {
                        if (Convert.ToInt32(strArray3[index]) > 0)
                        {
                            string bet = SsId.Bet;
                            Decimal Money = Convert.ToDecimal(model.SingleMoney * (Decimal)Convert.ToInt32(strArray3[index]) * model.Times);
                            if (new UserTotalTran().MoneyOpers(bet, model.UserId.ToString(), Money, model.LotteryId, model.PlayId, logSysId, 3, 99, string.Empty, string.Empty, "会员投注", "") > 0)
                            {
                                SqlParameter[] values = new SqlParameter[21]
                {
                  new SqlParameter("@SsId", (object) bet),
                  new SqlParameter("@UserId", (object) model.UserId),
                  new SqlParameter("@UserMoney", (object) model.UserMoney),
                  new SqlParameter("@LotteryId", (object) model.LotteryId),
                  new SqlParameter("@PlayId", (object) model.PlayId),
                  new SqlParameter("@IssueNum", (object) model.IssueNum),
                  new SqlParameter("@SingleMoney", (object) model.SingleMoney),
                  new SqlParameter("@Num", (object) Convert.ToInt32(strArray3[index])),
                  new SqlParameter("@Detail", (object) ""),
                  new SqlParameter("@Total", (object) (model.SingleMoney * (Decimal) Convert.ToInt32(strArray3[index]))),
                  new SqlParameter("@Point", (object) model.Point),
                  new SqlParameter("@PointMoney", (object) (model.SingleMoney * (Decimal) Convert.ToInt32(strArray3[index]) * model.Point / new Decimal(100))),
                  new SqlParameter("@Bonus", (object) (model.Bonus / (Decimal) Convert.ToInt32(strArray2[index]))),
                  new SqlParameter("@Pos", (object) ""),
                  new SqlParameter("@PlayCode", (object) strArray1[index]),
                  new SqlParameter("@STime", (object) model.STime),
                  new SqlParameter("@STime2", (object) model.STime2),
                  new SqlParameter("@IsDelay", (object) model.IsDelay),
                  new SqlParameter("@Times", (object) model.Times),
                  new SqlParameter("@ZhId", (object) "0"),
                  new SqlParameter("@Source", (object) Source)
                };
                                sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                                sqlCommand.Parameters.AddRange(values);
                                logSysId = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                sqlCommand.Parameters.Clear();
                                ++num1;
                                BetDetailDAL.SetBetDetail(model.STime2.ToString("yyyyMMdd"), model.UserId.ToString(), logSysId.ToString(), model.Detail.Replace("|", "#"));
                            }
                        }
                    }
                    logSysId = num1 <= 0 ? 0 : 1;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                    logSysId = 0;
                }
            }
            return logSysId;
        }

        public int InsertZhBet(UserZhBetModel zhmodel, List<UserZhDetailModel> listZh, Decimal money, string Source)
        {
            int num1 = 0;
            if (listZh.Count > 0)
            {
                using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    try
                    {
                        string zbet = SsId.ZBet;
                        if (new UserTotalTran().MoneyOpers(zbet, zhmodel.UserId.ToString(), money, 0, 0, 0, 3, 99, string.Empty, string.Empty, "会员追号", "") <= 0)
                            return 0;
                        SqlParameter[] values1 = new SqlParameter[9]
            {
              new SqlParameter("@SsId", (object) zbet),
              new SqlParameter("@UserId", (object) zhmodel.UserId),
              new SqlParameter("@LotteryId", (object) zhmodel.LotteryId),
              new SqlParameter("@PlayId", (object) zhmodel.PlayId),
              new SqlParameter("@StartIssueNum", (object) zhmodel.StartIssueNum),
              new SqlParameter("@TotalNums", (object) zhmodel.TotalNums),
              new SqlParameter("@TotalSums", (object) zhmodel.TotalSums),
              new SqlParameter("@IsStop", (object) zhmodel.IsStop),
              new SqlParameter("@STime", (object) zhmodel.STime)
            };
                        sqlCommand.CommandText = "INSERT INTO N_UserZhBet (SsId,UserId ,LotteryId ,PlayId ,StartIssueNum ,TotalNums ,TotalSums ,IsStop ,STime)\r\n                                        values(@SsId,@UserId,@LotteryId,@PlayId,@StartIssueNum,@TotalNums,@TotalSums,@IsStop,@STime)";
                        sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                        sqlCommand.Parameters.AddRange(values1);
                        int int32 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.Parameters.Clear();
                        foreach (UserZhDetailModel userZhDetailModel in listZh)
                        {
                            UserBetModel list = userZhDetailModel.Lists[0];
                            DateTime stime2;
                            int userId;
                            if (list.Pos.Equals(""))
                            {
                                if (list.PlayCode.Equals("P_5ZH") || list.PlayCode.Equals("P_4ZH_L") || (list.PlayCode.Equals("P_4ZH_R") || list.PlayCode.Equals("P_3ZH_L")) || (list.PlayCode.Equals("P_3ZH_C") || list.PlayCode.Equals("P_3ZH_R")))
                                {
                                    string str1 = "";
                                    string str2 = "";
                                    string str3 = "";
                                    if (list.PlayCode == "P_5ZH")
                                    {
                                        str1 = "P_5ZH_WQBSG,P_5ZH_QBSG,P_5ZH_BSG,P_5ZH_SG,P_5ZH_G";
                                        str2 = "1,10,100,1000,10000";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        int num5 = 1;
                                        int num6 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        for (int index = 3; index < strArray.Length; ++index)
                                            num5 *= strArray[index].Length;
                                        for (int index = 4; index < strArray.Length; ++index)
                                            num6 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5 + "," + (object)num6;
                                    }
                                    if (list.PlayCode == "P_4ZH_L")
                                    {
                                        str1 = "P_4ZH_L_WQBS,P_4ZH_L_QBS,P_4ZH_L_BS,P_4ZH_L_S";
                                        str2 = "1,10,100,1000";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        int num5 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        for (int index = 3; index < strArray.Length; ++index)
                                            num5 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5;
                                    }
                                    if (list.PlayCode == "P_4ZH_R")
                                    {
                                        str1 = "P_4ZH_R_QBSG,P_4ZH_R_BSG,P_4ZH_R_SG,P_4ZH_R_G";
                                        str2 = "1,10,100,1000";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        int num5 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        for (int index = 3; index < strArray.Length; ++index)
                                            num5 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4 + "," + (object)num5;
                                    }
                                    if (list.PlayCode == "P_3ZH_L")
                                    {
                                        str1 = "P_3ZH_L_WQB,P_3ZH_L_QB,P_3ZH_L_B";
                                        str2 = "1,10,100";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                                    }
                                    if (list.PlayCode == "P_3ZH_C")
                                    {
                                        str1 = "P_3ZH_C_QBS,P_3ZH_C_BS,P_3ZH_C_S";
                                        str2 = "1,10,100";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                                    }
                                    if (list.PlayCode == "P_3ZH_R")
                                    {
                                        str1 = "P_3ZH_R_BSG,P_3ZH_R_SG,P_3ZH_R_G";
                                        str2 = "1,10,100";
                                        string[] strArray = list.Detail.Replace("_", "").Split(',');
                                        int num2 = 1;
                                        int num3 = 1;
                                        int num4 = 1;
                                        for (int index = 0; index < strArray.Length; ++index)
                                            num2 *= strArray[index].Length;
                                        for (int index = 1; index < strArray.Length; ++index)
                                            num3 *= strArray[index].Length;
                                        for (int index = 2; index < strArray.Length; ++index)
                                            num4 *= strArray[index].Length;
                                        str3 = num2.ToString() + "," + (object)num3 + "," + (object)num4;
                                    }
                                    string[] strArray1 = str1.Split(',');
                                    string[] strArray2 = str2.Split(',');
                                    string[] strArray3 = str3.Split(',');
                                    for (int index = 0; index < strArray1.Length; ++index)
                                    {
                                        if (Convert.ToInt32(strArray3[index]) > 0)
                                        {
                                            SqlParameter[] values2 = new SqlParameter[21]
                      {
                        new SqlParameter("@SsId", (object) SsId.Bet),
                        new SqlParameter("@UserId", (object) list.UserId),
                        new SqlParameter("@UserMoney", (object) list.UserMoney),
                        new SqlParameter("@LotteryId", (object) list.LotteryId),
                        new SqlParameter("@PlayId", (object) list.PlayId),
                        new SqlParameter("@IssueNum", (object) userZhDetailModel.IssueNum),
                        new SqlParameter("@SingleMoney", (object) list.SingleMoney),
                        new SqlParameter("@Num", (object) Convert.ToInt32(strArray3[index])),
                        new SqlParameter("@Detail", (object) ""),
                        new SqlParameter("@Total", (object) (list.SingleMoney * (Decimal) Convert.ToInt32(strArray3[index]))),
                        new SqlParameter("@Point", (object) list.Point),
                        new SqlParameter("@PointMoney", (object) (list.SingleMoney * (Decimal) Convert.ToInt32(strArray3[index]) * list.Point / new Decimal(100))),
                        new SqlParameter("@Bonus", (object) (list.Bonus / (Decimal) Convert.ToInt32(strArray2[index]))),
                        new SqlParameter("@Pos", (object) list.Pos),
                        new SqlParameter("@PlayCode", (object) list.PlayCode),
                        new SqlParameter("@STime", (object) userZhDetailModel.STime),
                        new SqlParameter("@STime2", (object) list.STime2),
                        new SqlParameter("@IsDelay", (object) list.IsDelay),
                        new SqlParameter("@Times", (object) userZhDetailModel.Times),
                        new SqlParameter("@ZhId", (object) int32),
                        new SqlParameter("@Source", (object) Source)
                      };
                                            sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                                                    ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                                                    values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                                                    ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                            sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                                            sqlCommand.Parameters.AddRange(values2);
                                            num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                            sqlCommand.Parameters.Clear();
                                            stime2 = list.STime2;
                                            string STime = stime2.ToString("yyyyMMdd");
                                            userId = list.UserId;
                                            string UserId = userId.ToString();
                                            string BetId = num1.ToString();
                                            string Detail = list.Detail.Replace("|", "#");
                                            BetDetailDAL.SetBetDetail(STime, UserId, BetId, Detail);
                                        }
                                    }
                                }
                                else
                                {
                                    SqlParameter[] values2 = new SqlParameter[21]
                  {
                    new SqlParameter("@SsId", (object) SsId.Bet),
                    new SqlParameter("@UserId", (object) list.UserId),
                    new SqlParameter("@UserMoney", (object) list.UserMoney),
                    new SqlParameter("@LotteryId", (object) list.LotteryId),
                    new SqlParameter("@PlayId", (object) list.PlayId),
                    new SqlParameter("@IssueNum", (object) userZhDetailModel.IssueNum),
                    new SqlParameter("@SingleMoney", (object) list.SingleMoney),
                    new SqlParameter("@Num", (object) list.Num),
                    new SqlParameter("@Detail", (object) ""),
                    new SqlParameter("@Total", (object) (list.SingleMoney * (Decimal) list.Num)),
                    new SqlParameter("@Point", (object) list.Point),
                    new SqlParameter("@PointMoney", (object) (list.SingleMoney * (Decimal) list.Num * list.Point / new Decimal(100))),
                    new SqlParameter("@Bonus", (object) list.Bonus),
                    new SqlParameter("@Pos", (object) list.Pos),
                    new SqlParameter("@PlayCode", (object) list.PlayCode),
                    new SqlParameter("@STime", (object) userZhDetailModel.STime),
                    new SqlParameter("@STime2", (object) list.STime2),
                    new SqlParameter("@IsDelay", (object) list.IsDelay),
                    new SqlParameter("@Times", (object) userZhDetailModel.Times),
                    new SqlParameter("@ZhId", (object) int32),
                    new SqlParameter("@Source", (object) Source)
                  };
                                    sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                    sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                                    sqlCommand.Parameters.AddRange(values2);
                                    num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                    sqlCommand.Parameters.Clear();
                                    stime2 = list.STime2;
                                    string STime = stime2.ToString("yyyyMMdd");
                                    userId = list.UserId;
                                    string UserId = userId.ToString();
                                    string BetId = num1.ToString();
                                    string Detail = list.Detail.Replace("|", "#");
                                    BetDetailDAL.SetBetDetail(STime, UserId, BetId, Detail);
                                }
                            }
                            else
                            {
                                string str1 = "";
                                string[] strArray1 = list.Pos.Split(',');
                                if (list.PlayCode == "R_4FS" || list.PlayCode == "R_4DS")
                                {
                                    string str2 = list.PlayCode + "_";
                                    if (list.Pos != "")
                                    {
                                        int count = Regex.Matches(list.Pos, "1").Count;
                                        if (count == 4)
                                            str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                        if (count == 5)
                                        {
                                            string[] strArray2 = "W,Q,B,S,G".Split(',');
                                            for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                            {
                                                for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                                {
                                                    for (int index3 = index2 + 1; index3 < strArray2.Length; ++index3)
                                                    {
                                                        for (int index4 = index3 + 1; index4 < strArray2.Length; ++index4)
                                                            str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + strArray2[index3] + strArray2[index4] + ",";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (list.PlayCode == "R_3FS" || list.PlayCode == "R_3DS" || (list.PlayCode == "R_3Z3" || list.PlayCode == "R_3Z6") || list.PlayCode == "R_3HX")
                                {
                                    string str2 = list.PlayCode + "_";
                                    if (list.Pos != "")
                                    {
                                        int count = Regex.Matches(list.Pos, "1").Count;
                                        if (count == 3)
                                            str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                        if (count >= 4)
                                        {
                                            string str3 = "" + (strArray1[0].Equals("1") ? "W," : "") + (strArray1[1].Equals("1") ? "Q," : "") + (strArray1[2].Equals("1") ? "B," : "") + (strArray1[3].Equals("1") ? "S," : "") + (strArray1[4].Equals("1") ? "G," : "");
                                            string[] strArray2 = str3.Substring(0, str3.Length - 1).Split(',');
                                            for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                            {
                                                for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                                {
                                                    for (int index3 = index2 + 1; index3 < strArray2.Length; ++index3)
                                                        str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + strArray2[index3] + ",";
                                                }
                                            }
                                        }
                                    }
                                }
                                if (list.PlayCode == "R_2FS" || list.PlayCode == "R_2DS" || list.PlayCode == "R_2Z2")
                                {
                                    string str2 = list.PlayCode + "_";
                                    if (list.Pos != "")
                                    {
                                        int count = Regex.Matches(list.Pos, "1").Count;
                                        if (count == 2)
                                            str1 = str1 + str2 + (strArray1[0].Equals("1") ? "W" : "") + (strArray1[1].Equals("1") ? "Q" : "") + (strArray1[2].Equals("1") ? "B" : "") + (strArray1[3].Equals("1") ? "S" : "") + (strArray1[4].Equals("1") ? "G" : "") + ",";
                                        if (count >= 3)
                                        {
                                            string str3 = "" + (strArray1[0].Equals("1") ? "W," : "") + (strArray1[1].Equals("1") ? "Q," : "") + (strArray1[2].Equals("1") ? "B," : "") + (strArray1[3].Equals("1") ? "S," : "") + (strArray1[4].Equals("1") ? "G," : "");
                                            string[] strArray2 = str3.Substring(0, str3.Length - 1).Split(',');
                                            for (int index1 = 0; index1 < strArray2.Length; ++index1)
                                            {
                                                for (int index2 = index1 + 1; index2 < strArray2.Length; ++index2)
                                                    str1 = str1 + str2 + strArray2[index1] + strArray2[index2] + ",";
                                            }
                                        }
                                    }
                                }
                                string[] strArray3 = str1.Substring(0, str1.Length - 1).Split(',');
                                for (int index = 0; index < strArray3.Length; ++index)
                                {
                                    SqlParameter[] values2 = new SqlParameter[21]
                  {
                    new SqlParameter("@SsId", (object) SsId.Bet),
                    new SqlParameter("@UserId", (object) list.UserId),
                    new SqlParameter("@UserMoney", (object) list.UserMoney),
                    new SqlParameter("@LotteryId", (object) list.LotteryId),
                    new SqlParameter("@PlayId", (object) list.PlayId),
                    new SqlParameter("@IssueNum", (object) userZhDetailModel.IssueNum),
                    new SqlParameter("@SingleMoney", (object) list.SingleMoney),
                    new SqlParameter("@Num", (object) (list.Num / strArray3.Length)),
                    new SqlParameter("@Detail", (object) ""),
                    new SqlParameter("@Total", (object) (list.SingleMoney * (Decimal) list.Num / (Decimal) strArray3.Length)),
                    new SqlParameter("@Point", (object) list.Point),
                    new SqlParameter("@PointMoney", (object) (list.SingleMoney * (Decimal) list.Num * list.Point / (Decimal) strArray3.Length / new Decimal(100))),
                    new SqlParameter("@Bonus", (object) list.Bonus),
                    new SqlParameter("@Pos", (object) ""),
                    new SqlParameter("@PlayCode", (object) strArray3[index]),
                    new SqlParameter("@STime", (object) userZhDetailModel.STime),
                    new SqlParameter("@STime2", (object) list.STime2),
                    new SqlParameter("@IsDelay", (object) list.IsDelay),
                    new SqlParameter("@Times", (object) userZhDetailModel.Times),
                    new SqlParameter("@ZhId", (object) int32),
                    new SqlParameter("@Source", (object) Source)
                  };
                                    sqlCommand.CommandText = "insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total\r\n                                        ,Point,PointMoney,Bonus,Pos,PlayCode,STime,STime2,IsDelay,Times,ZhId,Source)\r\n                                        values(@SsId,@UserId,@UserMoney,@LotteryId,@PlayId,@IssueNum,@SingleMoney,@Num,@Detail,@Total\r\n                                        ,@Point,@PointMoney,@Bonus,@Pos,@PlayCode,@STime,@STime2,@IsDelay,@Times,@ZhId,@Source)";
                                    sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                                    sqlCommand.Parameters.AddRange(values2);
                                    num1 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                                    sqlCommand.Parameters.Clear();
                                    stime2 = list.STime2;
                                    string STime = stime2.ToString("yyyyMMdd");
                                    userId = list.UserId;
                                    string UserId = userId.ToString();
                                    string BetId = num1.ToString();
                                    string Detail = list.Detail.Replace("|", "#");
                                    BetDetailDAL.SetBetDetail(STime, UserId, BetId, Detail);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        new LogExceptionDAL().Save("系统异常", ex.Message);
                        num1 = 0;
                    }
                }
            }
            return num1;
        }

        public void GetPlayListJSON(int lotteryId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                string str = "SELECT [Id],[Title] FROM [Sys_PlaySmallType] where flag=0 and lotteryId=" + (object)lotteryId;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = str;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = whereStr;
                string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Flex_UserBet").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,Id,SsId,LotteryName+'-'+PlayName as LName,UserId,UserName,PlayId,PlayName,PlayCode,LotteryId,LotteryName,IssueNum,SingleMoney,moshi,Times,Num,cast(Times*Total as decimal(15,4)) as Total,Point,PointMoney,Bonus,Bonus2,WinNum,WinBonus,RealGet,Pos,STime,STime2,state,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime,number", "Flex_UserBet", "Id", PSize, page, "desc", whereStr);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListJSONById(string BetId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select Id,SsId,UserId,UserMoney,LotteryId,PlayId,PlayCode,IssueNum,Number,SingleMoney,Times,Num,Detail,DX,DS,Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime2,STime2 as STime,IsOpen,State,IsDelay,IsWin,STime9,IsCheat,ZhId,Source,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(number,'未开奖不计算') as kjnumber,ZhId\r\n                                from N_UserBet where Id=" + BetId;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListZhJSON(int page, int PSize, string whereStr, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = whereStr;
                string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Flex_UserBetZh").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,*", "Flex_UserBetZh", "Id", PSize, page, "desc", whereStr);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public string BetCancel(string betId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet where state=0 and Id=" + betId;
                    DataTable dataTable = new DataTable("N_UserBet");
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count <= 0)
                        return this.JsonResult(0, "已经开奖或已撤单,不能撤单!");
                    string ssId = dataTable.Rows[0]["SsId"].ToString();
                    int int32_1 = Convert.ToInt32(dataTable.Rows[0]["LotteryId"]);
                    string IssueNum = dataTable.Rows[0]["IssueNum"].ToString();
                    if (Convert.ToInt32(dataTable.Rows[0]["State"]) != 0)
                        return this.JsonResult(0, "已经开奖或已撤单,不能撤单!");
                    if ((int32_1 == 3002 || int32_1 == 3003) && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") && DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00"))
                        return this.JsonResult(0, "现在是封单时间,不能撤单!");
                    DateTime dateTime = Convert.ToDateTime(new UserBetDAL().GetIssueTime(int32_1, IssueNum));
                    sqlCommand.CommandText = "select CloseTime From Sys_Lottery with(nolock) where Id=" + (object)int32_1;
                    int int32_2 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    sqlCommand.CommandText = "select DATEDIFF(S,GETDATE(),'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as aaa";
                    if (Convert.ToInt32(sqlCommand.ExecuteScalar()) <= int32_2)
                        return this.JsonResult(0, "现在是封单时间,不能撤单!");
                    Decimal Money = Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[0]["Total"].ToString()) * Convert.ToDecimal(dataTable.Rows[0]["Times"].ToString()));
                    if (new UserTotalTran().MoneyOpers(ssId, dataTable.Rows[0]["UserId"].ToString(), Money, int32_1, Convert.ToInt32(dataTable.Rows[0]["PlayId"].ToString()), Convert.ToInt32(betId), 6, 99, string.Empty, string.Empty, "会员撤单", dataTable.Rows[0]["Stime"].ToString()) <= 0)
                        return this.JsonResult(0, "撤单失败!");
                    sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + betId;
                    sqlCommand.ExecuteNonQuery();
                    return this.JsonResult(1, "撤单成功!");
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                }
            }
            return this.JsonResult(0, "撤单失败!");
        }

        public string BetAllCancel(string ZhId, string PlayId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet with(nolock) where state=0 and PlayId=" + PlayId + " and ZhId=" + ZhId;
                    DataTable dataTable = new DataTable("N_UserBet");
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count <= 0)
                        return this.JsonResult(0, "没有可撤单的订单,不能撤单!");
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        bool flag = true;
                        string str = row["Id"].ToString();
                        string ssId = row["SsId"].ToString();
                        int int32_1 = Convert.ToInt32(row["LotteryId"]);
                        string IssueNum = row["IssueNum"].ToString();
                        if (Convert.ToInt32(row["State"]) != 0)
                            flag = false;
                        if ((int32_1 == 3002 || int32_1 == 3003) && DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") && DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00"))
                            flag = false;
                        DateTime dateTime = Convert.ToDateTime(new UserBetDAL().GetIssueTime(int32_1, IssueNum));
                        sqlCommand.CommandText = "select CloseTime From Sys_Lottery with(nolock) where Id=" + (object)int32_1;
                        int int32_2 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.CommandText = "select DATEDIFF(S,GETDATE(),'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as aaa";
                        if (Convert.ToInt32(sqlCommand.ExecuteScalar()) <= int32_2)
                            flag = false;
                        if (flag)
                        {
                            Decimal Money = Convert.ToDecimal(Convert.ToDecimal(row["Total"].ToString()) * Convert.ToDecimal(row["Times"].ToString()));
                            if (new UserTotalTran().MoneyOpers(ssId, row["UserId"].ToString(), Money, int32_1, Convert.ToInt32(row["PlayId"].ToString()), Convert.ToInt32(str), 6, 99, string.Empty, string.Empty, "会员撤单", row["STime"].ToString()) > 0)
                            {
                                sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + str;
                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    return this.JsonResult(1, "撤单成功!");
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                    return this.JsonResult(0, "撤单失败!");
                }
            }
        }

        public int InsertBetAgain(int Id, int userId, string IssueNum)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    sqlCommand.CommandText = "Insert into N_UserBet(SsId,UserId,UserMoney,LotteryId,PlayId,IssueNum,SingleMoney,Num,Detail,Total,Point,PointMoney,Bonus,Pos,PlayCode,STime,IsDelay,Times) \r\n                                        select '" + SsId.Bet + "',UserId,UserMoney,LotteryId,PlayId,'" + IssueNum + "',SingleMoney,Num,Detail,Total,Point,PointMoney,Bonus,Pos,PlayCode,getdate(),IsDelay,Times from N_UserBet where Id=" + (object)Id;
                    sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                    int int32 = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    object[] objArray = new object[5];
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.ConditionExpress = "Id=@Id";
                        dbOperHandler.AddConditionParameter("@Id", (object)Id);
                        objArray = dbOperHandler.GetFields("N_UserBet", "Total,LotteryId,IssueNum,Times,PlayId,ssId");
                    }
                    Decimal Money = Convert.ToDecimal(Convert.ToDecimal(objArray[0]) * Convert.ToDecimal(objArray[3]));
                    return new UserTotalTran().MoneyOpers(objArray[5].ToString(), userId.ToString(), Money, Convert.ToInt32(objArray[1].ToString()), Convert.ToInt32(objArray[4].ToString()), int32, 3, 99, string.Empty, string.Empty, "再次投注", "") > 0 ? 1 : 0;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取下一期开奖信息
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <returns></returns>
        public string[] GetIssueTimeAndSN(int lotteryId)
        {
            string[] strArray = new string[2];
            DateTime now = DateTime.Now;
            switch (lotteryId)
            {
                case 3002:
                case 3003:
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,Convert(varchar(4),getdate(),120)+'-01-01 20:30:00',Convert(varchar(20),getdate(),120)) as d";
                        int num = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7 + 1;
                        strArray[1] = now.ToString("yyyy-MM-dd") + " 20:30:00";
                        if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
                            strArray[1] = now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                        else
                            --num;
                        strArray[0] = now.Year.ToString() + this.AddZero(num + 1, 3);
                        break;
                    }
                default:
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where Time > Convert(varchar(10),getdate(),108) and LotteryId=" + (object)lotteryId + " order by Time asc";
                        DataTable dataTable1 = dbOperHandler.GetDataTable();
                        if (dataTable1.Rows.Count > 0)
                        {
                            DataRow row = dataTable1.Rows[0];
                            strArray[1] = now.ToString("yyyy-MM-dd") + " " + row["Time"].ToString();
                            int int32 = Convert.ToInt32(row["Sn"].ToString());
                            strArray[0] = now.ToString("yyyyMMdd") + "-" + row["Sn"].ToString();
                            if (lotteryId == 1003 && int32 >= 85)
                                strArray[0] = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + this.AddZero(int32, 2);
                            if (lotteryId == 1010 || lotteryId == 1017 || (lotteryId == 3004 || lotteryId == 1012) || lotteryId == 1013)
                                strArray[0] = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(lotteryId.ToString()) + Convert.ToInt32(row["Sn"].ToString())));
                            if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59") && (lotteryId == 1014 || lotteryId == 1016))
                            {
                                strArray[0] = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + row["Sn"].ToString();
                                strArray[1] = now.ToString("yyyy-MM-dd") + " " + row["Time"].ToString();
                            }
                            if (lotteryId == 1014 || lotteryId == 1015 || lotteryId == 1016)
                                strArray[0] = strArray[0].Replace("-", "");
                            if (lotteryId == 4001)
                            {
                                strArray[0] = !(DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) || !(DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01")) ? string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(row["Sn"].ToString()))) : string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + 179 + Convert.ToInt32(row["Sn"].ToString())));
                                break;
                            }
                            break;
                        }
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where LotteryId=" + (object)lotteryId + " order by Time asc";
                        DataTable dataTable2 = dbOperHandler.GetDataTable();
                        strArray[0] = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[0]["Sn"].ToString();
                        strArray[1] = now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataTable2.Rows[0]["Time"].ToString();
                        if (lotteryId == 1010 || lotteryId == 1017 || lotteryId == 3004)
                            strArray[0] = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1010") + 880 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString())));
                        if (lotteryId == 1012)
                            strArray[0] = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1012") + 660 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString())));
                        if (lotteryId == 1013)
                            strArray[0] = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1013") + 203 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString())));
                        if (lotteryId == 1014 || lotteryId == 1015 || lotteryId == 1016)
                            strArray[0] = strArray[0].Replace("-", "");
                        if (lotteryId == 4001)
                        {
                            strArray[0] = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + 179 + Convert.ToInt32(dataTable2.Rows[0]["Sn"].ToString())));
                            break;
                        }
                        break;
                    }
            }
            return strArray;
        }

        public string GetIssueTime(int lotteryId, string IssueNum)
        {
            string str1 = "";
            string str2 = IssueNum;
            DateTime dateTime = new DateTimePubDAL().GetDateTime();
            string str3 = dateTime.ToString("yyyyMMdd");
            dateTime.ToString("HH:mm:ss");
            IssueNum = IssueNum.Substring(IssueNum.IndexOf('-') + 1);
            switch (lotteryId)
            {
                case 1010:
                case 1012:
                case 1013:
                case 1017:
                case 3004:
                case 4001:
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        int tsIssueNumToPet = new LotteryTimeDAL().GetTsIssueNumToPet(lotteryId, Convert.ToInt32(IssueNum));
                        string str4 = tsIssueNumToPet.ToString().Length < 3 ? "0" + (object)tsIssueNumToPet : tsIssueNumToPet.ToString();
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='" + str4 + "' and LotteryId=" + (object)lotteryId + " order by Time asc";
                        DataTable dataTable = dbOperHandler.GetDataTable();
                        if (dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            str1 = dateTime.ToString("yyyy-MM-dd") + " " + row["Time"].ToString();
                            break;
                        }
                        break;
                    }
                case 1014:
                case 1015:
                case 1016:
                    string str5 = str2.Substring(0, 8);
                    IssueNum = IssueNum.Substring(8);
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='" + IssueNum + "' and LotteryId=" + (object)lotteryId + " order by Time asc";
                        DataTable dataTable = dbOperHandler.GetDataTable();
                        if (dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            str1 = Convert.ToInt32(str5) <= Convert.ToInt32(str3) ? dateTime.ToString("yyyy-MM-dd") + " " + row["Time"].ToString() : dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + row["Time"].ToString();
                            break;
                        }
                        break;
                    }
                case 3002:
                case 3003:
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,Convert(varchar(4),getdate(),120)+'-01-01 20:30:00',Convert(varchar(20),getdate(),120)) as d";
                        int num1 = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7 + 1;
                        str1 = dateTime.ToString("yyyy-MM-dd") + " 20:30:00";
                        if (dateTime > Convert.ToDateTime(dateTime.ToString(" 20:30:00")))
                        {
                            str1 = dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                            break;
                        }
                        int num2 = num1 - 1;
                        break;
                    }
                default:
                    string str6 = str2.Substring(0, 8);
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Id, Sn,Time,LotteryId from Sys_LotteryTime where sn ='" + IssueNum + "' and LotteryId=" + (object)lotteryId + " order by Time asc";
                        DataTable dataTable = dbOperHandler.GetDataTable();
                        if (dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];
                            str1 = Convert.ToInt32(str6) <= Convert.ToInt32(str3) ? dateTime.ToString("yyyy-MM-dd") + " " + row["Time"].ToString() : dateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + row["Time"].ToString();
                            break;
                        }
                        break;
                    }
            }
            return str1;
        }

        public void GetBetInfoJSON(string Id, string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 UserId,STime2 from N_UserBet where Id=" + Id;
                DataTable dataTable1 = dbOperHandler.GetDataTable();
                string betDetail = BetDetailDAL.GetBetDetail(Convert.ToDateTime(dataTable1.Rows[0]["STime2"]).ToString("yyyyMMdd"), dataTable1.Rows[0]["UserId"].ToString(), Id);
                if (!string.IsNullOrEmpty(betDetail))
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select a.*," + UserId + " as CurUserId,'@@youle' as strDetail,cast(round(a.total*times,4) as numeric(15,4)) as Total2,Convert(varchar(15),cast(Bonus as numeric(15,4)))+'/'+Convert(varchar(10),cast(round([Point],2) as numeric(10,2)))+'%' as Point2,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*a.Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(b.number,'未开奖不计算') as kjnumber from N_UserBet a left join Sys_LotteryData b on a.LotteryId=b.Type and a.IssueNum=b.Title where a.Id=" + Id;
                    DataTable dataTable2 = dbOperHandler.GetDataTable();
                    _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable2) + "}";
                    _jsonstr = _jsonstr.Replace("@@youle", betDetail);
                    dataTable2.Clear();
                    dataTable2.Dispose();
                }
                else
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select a.*," + UserId + " as CurUserId,Detail as strDetail,cast(round(a.total*times,4) as numeric(15,4)) as Total2,Convert(varchar(15),cast(Bonus as numeric(15,4)))+'/'+Convert(varchar(10),cast(round([Point],2) as numeric(10,2)))+'%' as Point2,case [SingleMoney] when '2.00' then '元' when '0.20' then '角' when '0.02' then '分' when '0.002' then '厘' end moshi,dbo.f_GetUserName(UserId) as UserName,dbo.f_GetPlayName(PlayId) as PlayName,dbo.f_GetLotteryName(LotteryId) as LotteryName,cast(Times*a.Total as decimal(15,4)) as BetMoney,case state when 0 then '未开奖' when 1 then '已撤单' when 2 then '未中奖' when 3 then '已中奖' end as stateName,isnull(b.number,'未开奖不计算') as kjnumber from N_UserBet a left join Sys_LotteryData b on a.LotteryId=b.Type and a.IssueNum=b.Title where a.Id=" + Id;
                    DataTable dataTable2 = dbOperHandler.GetDataTable();
                    _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable2) + "}";
                    dataTable2.Clear();
                    dataTable2.Dispose();
                }
            }
        }

        protected new string JsonResult(int success, string str)
        {
            return "[{\"result\" :\"" + success.ToString() + "\",\"returnval\" :\"" + str + "\"}]";
        }
    }
}
