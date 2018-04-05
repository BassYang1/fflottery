// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.CheckOperation
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DAL.Flex;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
    public class CheckOperation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row">投注信息</param>
        /// <param name="LotteryNumber">期号</param>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public static bool Checking(DataRow row, string LotteryNumber, SqlCommand sqlCommand)
        {
            try
            {
                if (Convert.ToInt32(row["State"]) != 0)
                    return true;
                int int32_1 = Convert.ToInt32(row["Id"]);
                string ssId = row["SsId"].ToString();
                int int32_2 = Convert.ToInt32(row["UserId"]);
                int int32_3 = Convert.ToInt32(row["LotteryId"]);
                int int32_4 = Convert.ToInt32(row["PlayId"]);
                int int32_5 = Convert.ToInt32(row["Num"]);
                string str1 = row["IssueNum"].ToString();
                string betDetail2 = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), int32_2.ToString(), int32_1.ToString());
                Decimal num1 = Convert.ToDecimal(row["Total"]);
                Decimal num2 = Convert.ToDecimal(row["point"]);
                Decimal num3 = Convert.ToDecimal(row["PointMoney"]);
                Decimal num4 = Convert.ToDecimal(row["Bonus"]);
                Decimal num5 = Convert.ToDecimal(row["Times"]);
                Decimal num6 = Convert.ToDecimal(row["SingleMoney"]);
                string Pos = row["Pos"].ToString();
                string sType = row["PlayCode"].ToString();
                Convert.ToInt32(row["IsCheat"]);
                Convert.ToInt32(row["IsDelay"]);
                int int32_6 = Convert.ToInt32(row["ZhId"]);
                string STime2 = row["STime"].ToString();
                string[] strArray1 = LotteryNumber.Split(',');

                if (sType.Equals("P_5QJ3"))
                {
                    string[] strArray2 = betDetail2.Split(',');
                    if (strArray2[0].IndexOf(CheckOperation.ReplaceStr(strArray1[0])) == -1 || strArray2[1].IndexOf(CheckOperation.ReplaceStr(strArray1[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_5QJ3'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_5QJ3", "P_5QJ3_2");
                    }
                    else
                        sType = sType.Replace("P_5QJ3", "P_5QJ3_1");
                }

                if (sType.Equals("P_4QJ3"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(strArray1[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_4QJ3'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_4QJ3", "P_4QJ3_2");
                    }
                    else
                        sType = sType.Replace("P_4QJ3", "P_4QJ3_1");
                }

                if (sType.Equals("P_3QJ2_L"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(strArray1[0])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_3QJ2_L'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QJ2_L", "P_3QJ2_L_2");
                    }
                    else
                        sType = sType.Replace("P_3QJ2_L", "P_3QJ2_L_1");
                }

                if (sType.Equals("P_3QJ2_R"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(strArray1[2])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_3QJ2_R'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QJ2_R", "P_3QJ2_R_2");
                    }
                    else
                        sType = sType.Replace("P_3QJ2_R", "P_3QJ2_R_1");
                }

                if (sType.Equals("P_5QW3"))
                {
                    string[] strArray2 = betDetail2.Split(',');
                    if (strArray2[0].IndexOf(CheckOperation.ReplaceDX(strArray1[0])) == -1 || strArray2[1].IndexOf(CheckOperation.ReplaceDX(strArray1[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_5QW3'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_5QW3", "P_5QW3_2");
                    }
                    else
                        sType = sType.Replace("P_5QW3", "P_5QW3_1");
                }

                if (sType.Equals("P_4QW3"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(strArray1[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_4QW3'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_4QW3", "P_4QW3_2");
                    }
                    else
                        sType = sType.Replace("P_4QW3", "P_4QW3_1");
                }

                if (sType.Equals("P_3QW2_L"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(strArray1[0])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_3QW2_L'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QW2_L", "P_3QW2_L_2");
                    }
                    else
                        sType = sType.Replace("P_3QW2_L", "P_3QW2_L_1");
                }

                if (sType.Equals("P_3QW2_R"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(strArray1[2])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)int32_2 + ")-" + (object)num2 + ") from Sys_PlaySmallType where title2='P_3QW2_R'";
                        num4 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QW2_R", "P_3QW2_R_2");
                    }
                    else
                        sType = sType.Replace("P_3QW2_R", "P_3QW2_R_1");
                }

                if (sType.Equals("P_3ZBD_L"))
                {
                    if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("P_3ZBD_C"))
                {
                    if (strArray1[1] == strArray1[2] || strArray1[2] == strArray1[3] || strArray1[1] == strArray1[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[1] != strArray1[2] && strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("P_3ZBD_R"))
                {
                    if (strArray1[2] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[2] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[2] != strArray1[3] && strArray1[3] != strArray1[4] && strArray1[2] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQB"))
                {
                    if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQS"))
                {
                    if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[3] || strArray1[0] == strArray1[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[3] && strArray1[0] != strArray1[3])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQG"))
                {
                    if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[4] || strArray1[0] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[4] && strArray1[0] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WBS"))
                {
                    if (strArray1[0] == strArray1[2] || strArray1[2] == strArray1[3] || strArray1[0] == strArray1[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[2] && strArray1[2] != strArray1[3] && strArray1[0] != strArray1[3])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WBG"))
                {
                    if (strArray1[0] == strArray1[2] || strArray1[2] == strArray1[4] || strArray1[0] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[2] && strArray1[2] != strArray1[4] && strArray1[0] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WSG"))
                {
                    if (strArray1[0] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[0] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[0] != strArray1[3] && strArray1[3] != strArray1[4] && strArray1[0] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QBS"))
                {
                    if (strArray1[1] == strArray1[2] || strArray1[2] == strArray1[3] || strArray1[1] == strArray1[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[1] != strArray1[2] && strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QBG"))
                {
                    if (strArray1[1] == strArray1[2] || strArray1[2] == strArray1[4] || strArray1[1] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[1] != strArray1[2] && strArray1[2] != strArray1[4] && strArray1[1] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QSG"))
                {
                    if (strArray1[1] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[1] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[1] != strArray1[3] && strArray1[3] != strArray1[4] && strArray1[1] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_BSG"))
                {
                    if (strArray1[2] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[2] == strArray1[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (strArray1[2] != strArray1[3] && strArray1[3] != strArray1[4] && strArray1[2] != strArray1[4])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("P_3ZHE_L") && strArray1[0] != strArray1[1] && (strArray1[0] != strArray1[2] && strArray1[1] != strArray1[2]))
                    num4 /= new Decimal(2);

                if (sType.Equals("P_3ZHE_C") && strArray1[1] != strArray1[2] && (strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3]))
                    num4 /= new Decimal(2);

                if (sType.Equals("P_3ZHE_R") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2]))
                    num4 /= new Decimal(2);

                if (sType.Equals("P_3HX_L"))
                {
                    if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
                        sType = sType.Replace("3HX", "3Z3_2");
                    if (strArray1[0] != strArray1[1] && strArray1[0] != strArray1[2] && strArray1[1] != strArray1[2])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3HX", "3Z6_2");
                    }
                }

                if (sType.Equals("P_3HX_C"))
                {
                    if (strArray1[1] == strArray1[2] || strArray1[2] == strArray1[3] || strArray1[1] == strArray1[3])
                        sType = sType.Replace("3HX", "3Z3_2");
                    if (strArray1[1] != strArray1[2] && strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3])
                    {
                        num4 /= new Decimal(2);
                        sType = sType.Replace("3HX", "3Z6_2");
                    }
                }

                if (sType.Equals("P_3HX_R"))
                {
                    if (strArray1.Length == 3)
                    {
                        if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2])
                        {
                            num4 /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                    else
                    {
                        if (strArray1[2] == strArray1[3] || strArray1[3] == strArray1[4] || strArray1[2] == strArray1[4])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (strArray1[2] != strArray1[3] && strArray1[3] != strArray1[4] && strArray1[2] != strArray1[4])
                        {
                            num4 /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                }

                if (sType.Contains("R_3HX"))
                {
                    if (strArray1.Length == 3)
                    {
                        if (strArray1[0] == strArray1[1] || strArray1[1] == strArray1[2] || strArray1[0] == strArray1[2])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (strArray1[0] != strArray1[1] && strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2])
                        {
                            num4 /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                    else
                    {
                        if (sType.Equals("R_3HX_WQB") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_WQS") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[3] && strArray1[0] != strArray1[3]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_WQG") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[4] && strArray1[0] != strArray1[4]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_WBS") && strArray1[0] != strArray1[2] && (strArray1[2] != strArray1[3] && strArray1[0] != strArray1[3]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_WBG") && strArray1[0] != strArray1[2] && (strArray1[2] != strArray1[4] && strArray1[0] != strArray1[4]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_WSG") && strArray1[0] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[0] != strArray1[4]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_QBS") && strArray1[1] != strArray1[2] && (strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_QBG") && strArray1[1] != strArray1[2] && (strArray1[2] != strArray1[4] && strArray1[1] != strArray1[4]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_QSG") && strArray1[1] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[1] != strArray1[4]))
                            num4 /= new Decimal(2);
                        if (sType.Equals("R_3HX_BSG") && strArray1[2] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[2] != strArray1[4]))
                            num4 /= new Decimal(2);
                    }
                }

                if (sType.Contains("R_3ZHE"))
                {
                    if (sType.Equals("R_3ZHE_WQB") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[2] && strArray1[0] != strArray1[2]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WQS") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[3] && strArray1[0] != strArray1[3]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WQG") && strArray1[0] != strArray1[1] && (strArray1[1] != strArray1[4] && strArray1[0] != strArray1[4]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WBS") && strArray1[0] != strArray1[2] && (strArray1[2] != strArray1[3] && strArray1[0] != strArray1[3]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WBG") && strArray1[0] != strArray1[2] && (strArray1[2] != strArray1[4] && strArray1[0] != strArray1[4]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WSG") && strArray1[0] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[0] != strArray1[4]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QBS") && strArray1[1] != strArray1[2] && (strArray1[2] != strArray1[3] && strArray1[1] != strArray1[3]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QBG") && strArray1[1] != strArray1[2] && (strArray1[2] != strArray1[4] && strArray1[1] != strArray1[4]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QSG") && strArray1[1] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[1] != strArray1[4]))
                        num4 /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_BSG") && strArray1[2] != strArray1[3] && (strArray1[3] != strArray1[4] && strArray1[2] != strArray1[4]))
                        num4 /= new Decimal(2);
                }

                int num7 = 0;
                int num8 = 0;

                if (sType.Equals("P_LHH_WQ"))
                {
                    num7 = Convert.ToInt32(strArray1[0]);
                    num8 = Convert.ToInt32(strArray1[1]);
                }

                if (sType.Equals("P_LHH_WB"))
                {
                    num7 = Convert.ToInt32(strArray1[0]);
                    num8 = Convert.ToInt32(strArray1[2]);
                }

                if (sType.Equals("P_LHH_WS"))
                {
                    num7 = Convert.ToInt32(strArray1[0]);
                    num8 = Convert.ToInt32(strArray1[3]);
                }

                if (sType.Equals("P_LHH_WG"))
                {
                    num7 = Convert.ToInt32(strArray1[0]);
                    num8 = Convert.ToInt32(strArray1[4]);
                }

                if (sType.Equals("P_LHH_QB"))
                {
                    num7 = Convert.ToInt32(strArray1[1]);
                    num8 = Convert.ToInt32(strArray1[2]);
                }

                if (sType.Equals("P_LHH_QS"))
                {
                    num7 = Convert.ToInt32(strArray1[1]);
                    num8 = Convert.ToInt32(strArray1[3]);
                }

                if (sType.Equals("P_LHH_QG"))
                {
                    num7 = Convert.ToInt32(strArray1[1]);
                    num8 = Convert.ToInt32(strArray1[4]);
                }

                if (sType.Equals("P_LHH_BS"))
                {
                    num7 = Convert.ToInt32(strArray1[2]);
                    num8 = Convert.ToInt32(strArray1[3]);
                }

                if (sType.Equals("P_LHH_BG"))
                {
                    num7 = Convert.ToInt32(strArray1[2]);
                    num8 = Convert.ToInt32(strArray1[4]);
                }

                if (sType.Equals("P_LHH_SG"))
                {
                    num7 = Convert.ToInt32(strArray1[3]);
                    num8 = Convert.ToInt32(strArray1[4]);
                }

                if (num7 != num8)
                    num4 = Convert.ToDecimal(num4 / Convert.ToDecimal(4.5));

                int num9 = CheckPlay.Check(LotteryNumber, betDetail2, Pos, sType);

                Decimal Money1 = num3 * num5;
                int num10;
                Decimal Money2;
                if (num9 > 0)
                {
                    num10 = 3;
                    Money2 = num4 * num5 * (Decimal)num9 * num6 / new Decimal(2);
                    Decimal num11 = new Decimal(200000);
                    if (Money2 > num11)
                        Money2 = num11;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select top 1 MinNum from Sys_PlaySmallType where Id=" + (object)int32_4;
                    Decimal num12 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                    if (num12 == new Decimal(0))
                    {
                        if (Money2 > num1 * num5 * new Decimal(100))
                        {
                            Decimal num13 = new Decimal(18000);
                            if (Money2 > num13)
                                Money2 = num13;
                        }
                    }
                    else if ((Decimal)int32_5 < num12)
                    {
                        Decimal num13 = new Decimal(18000);
                        if (Money2 > num13)
                            Money2 = num13;
                    }
                }
                else
                {
                    num10 = 2;
                    Money2 = new Decimal(0);
                }
                Decimal num14 = Money2 + Money1 - num1 * num5;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "update N_UserBet set State=" + num10.ToString() + ",WinNum=" + num9.ToString() + ",WinBonus=" + Money2.ToString() + ",RealGet=" + num14.ToString() + " where Id=" + int32_1.ToString();
                sqlCommand.ExecuteNonQuery();
                if (Money2 > new Decimal(0))
                    new UserTotalTran().MoneyOpers(ssId, int32_2.ToString(), Money2, int32_3, int32_4, int32_1, 5, 99, "", "", "奖金派发", STime2);
                if (Money1 > new Decimal(0))
                    new UserTotalTran().MoneyOpers(ssId, int32_2.ToString(), Money1, int32_3, int32_4, int32_1, 4, 99, "", "", "返点派发", STime2);
                if (int32_6 != 0)
                {
                    string str2 = string.Format(" where LotteryId={0} and state=0 and zhid={1} and IssueNum>'{2}'", (object)int32_3, (object)int32_6, (object)str1);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select count(Id) from N_UserBet" + str2;
                    if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0 && num9 > 0)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = string.Format("select count(Id) from N_UserZhBet with(nolock) where isstop=1 and Id={0}", (object)int32_6);
                        if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0)
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.CommandText = "select isnull(sum(Total*Times),0) from N_UserBet " + str2;
                            Decimal Money3 = Convert.ToDecimal(string.Concat(sqlCommand.ExecuteScalar()));
                            sqlCommand.CommandType = CommandType.Text;
                            sqlCommand.CommandText = "update N_UserBet set State=1 " + str2;
                            sqlCommand.ExecuteNonQuery();
                            new UserTotalTran().MoneyOpers(ssId, int32_2.ToString(), Money3, int32_3, int32_4, int32_1, 6, 99, "", "", "终止追号", STime2);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
                return false;
            }
        }

        public static bool AdminCancel(int BetId, SqlCommand sqlCommand)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                sqlDataAdapter.SelectCommand.CommandText = "select top 1 * From N_UserBet with(nolock)  where Id=" + BetId.ToString();
                DataTable dataTable1 = new DataTable();
                sqlDataAdapter.Fill(dataTable1);
                if (dataTable1.Rows.Count > 0)
                {
                    DataRow row = dataTable1.Rows[0];
                    string ssId = row["ssId"].ToString();
                    int int32_1 = Convert.ToInt32(row["UserId"]);
                    int int32_2 = Convert.ToInt32(row["LotteryId"]);
                    int int32_3 = Convert.ToInt32(row["PlayId"]);
                    row["IssueNum"].ToString();
                    if (string.IsNullOrEmpty(BetDetailDAL.GetBetDetail2(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), int32_1.ToString(), BetId.ToString())))
                        ;
                    Decimal num1 = Convert.ToDecimal(row["Total"]);
                    Convert.ToDecimal(row["point"]);
                    Decimal num2 = Convert.ToDecimal(row["PointMoney"]);
                    Convert.ToDecimal(row["Bonus"]);
                    Decimal num3 = Convert.ToDecimal(row["Times"]);
                    Convert.ToDecimal(row["SingleMoney"]);
                    row["Pos"].ToString();
                    row["PlayCode"].ToString();
                    string STime2 = row["STime"].ToString();
                    Convert.ToInt32(row["IsCheat"]);
                    Decimal Money = num1 * num3 - num2 * num3;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "update N_UserBet set State=1,WinNum=0,RealGet=0 where Id=" + BetId.ToString();
                    sqlCommand.ExecuteNonQuery();
                    if (Money > new Decimal(0))
                        new UserTotalTran().MoneyOpers(ssId, int32_1.ToString(), Money, int32_2, int32_3, BetId, 6, 99, "", "", "后台撤单", STime2);
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserName,Point from N_User with(nolock)  where Id=" + int32_1.ToString();
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    string UserName = dataTable2.Rows[0]["UserName"].ToString();
                    int int32_4 = Convert.ToInt32(dataTable2.Rows[0]["Point"]);
                    CheckOperation.AgencyPoint(ssId, int32_1, UserName, int32_4, int32_2, int32_3, BetId, -Convert.ToDecimal(num1 * num3), sqlCommand);
                }
                dataTable1.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
                return false;
            }
        }

        public static bool AdminCancelToNO(int BetId, SqlCommand sqlCommand)
        {
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                sqlDataAdapter.SelectCommand.CommandText = "select top 1 * From N_UserBet with(nolock)  where Id=" + BetId.ToString();
                DataTable dataTable1 = new DataTable();
                sqlDataAdapter.Fill(dataTable1);
                if (dataTable1.Rows.Count > 0)
                {
                    DataRow row = dataTable1.Rows[0];
                    string ssId = row["ssId"].ToString();
                    int int32_1 = Convert.ToInt32(row["UserId"]);
                    int int32_2 = Convert.ToInt32(row["LotteryId"]);
                    int int32_3 = Convert.ToInt32(row["PlayId"]);
                    row["IssueNum"].ToString();
                    Decimal num1 = Convert.ToDecimal(row["Total"]);
                    Decimal num2 = Convert.ToDecimal(row["Times"]);
                    Decimal Money = Convert.ToDecimal(row["WinBonus"]);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "update N_UserBet set State=0,WinNum=0,WinBonus=0,RealGet=0 where Id=" + BetId.ToString();
                    sqlCommand.ExecuteNonQuery();
                    if (Money > new Decimal(0))
                        new UserTotalTran().MoneyOpers(ssId, int32_1.ToString(), Money, int32_2, int32_3, BetId, 6, 99, "", "", "撤到未开奖", row["STime"].ToString());
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserName,Point from N_User with(nolock)  where Id=" + int32_1.ToString();
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    string UserName = dataTable2.Rows[0]["UserName"].ToString();
                    int int32_4 = Convert.ToInt32(dataTable2.Rows[0]["Point"]);
                    CheckOperation.AgencyPoint(ssId, int32_1, UserName, int32_4, int32_2, int32_3, BetId, -Convert.ToDecimal(num1 * num2), sqlCommand);
                }
                dataTable1.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                new LogExceptionDAL().Save("程序异常", "派奖过程中出现异常：" + ex.Message);
                return false;
            }
        }

        public static void AgencyPoint(string ssId, int UserId, string UserName, int UserPoint, int LotteryId, int PlayId, int BetId, Decimal BetMoney, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select ParentId from N_User with(nolock) where Id=" + UserId.ToString();
            int int32_1 = Convert.ToInt32(cmd.ExecuteScalar());
            if (int32_1 == 0)
                return;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Point from N_User with(nolock) where Id=" + int32_1.ToString();
            object obj = cmd.ExecuteScalar();
            if (string.IsNullOrEmpty(string.Concat(obj)))
                return;
            int int32_2 = Convert.ToInt32(obj);
            if (int32_2 >= 133 || int32_2 < UserPoint)
                return;
            Decimal Money = BetMoney * Convert.ToDecimal(int32_2 - UserPoint) / new Decimal(1000);
            if (Convert.ToDecimal(Money.ToString("0.0000")) > new Decimal(0))
                new UserTotalTran().MoneyOpers(ssId, int32_1.ToString(), Money, LotteryId, PlayId, BetId, 4, 99, "", "", UserName + " 游戏返点", "");
            CheckOperation.AgencyPoint(ssId, int32_1, UserName, int32_2, LotteryId, PlayId, BetId, BetMoney, cmd);
        }

        public static int UserMoneyStatTran(int UserId, string StatType, Decimal StatValue, SqlCommand cmd)
        {
            try
            {
                cmd.CommandText = "select Id From N_UserMoneyStatAll with(nolock) where UserId=" + (object)UserId + " and DateDiff(D,STime,getDate())=0";
                int int32 = Convert.ToInt32(cmd.ExecuteScalar());
                int num;
                if (int32 == 0)
                {
                    cmd.CommandText = "insert into N_UserMoneyStatAll(UserId," + StatType + ",STime) values (" + (object)UserId + "," + (object)StatValue + ",getdate())";
                    num = cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = "update N_UserMoneyStatAll set " + StatType + "=" + StatType + "+" + (object)StatValue + " where Id=" + (object)int32;
                    num = cmd.ExecuteNonQuery();
                }
                return num;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string ReplaceStr(string str)
        {
            return str.Replace("0", "一区").Replace("1", "一区").Replace("2", "二区").Replace("3", "二区").Replace("4", "三区").Replace("5", "三区").Replace("6", "四区").Replace("7", "四区").Replace("8", "五区").Replace("9", "五区");
        }

        public static string ReplaceDX(string str)
        {
            return str.Replace("0", "小").Replace("1", "小").Replace("2", "小").Replace("3", "小").Replace("4", "小").Replace("5", "大").Replace("6", "大").Replace("7", "大").Replace("8", "大").Replace("9", "大");
        }
    }
}