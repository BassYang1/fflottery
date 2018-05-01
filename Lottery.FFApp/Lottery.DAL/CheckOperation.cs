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
        /// 计算奖金额
        /// </summary>
        /// <param name="row">投注信息</param>
        /// <param name="lotNumber">开奖号码</param>
        /// <param name="lotNumTotal">开奖号码和值</param>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public static bool Checking(DataRow row, string lotNumber, int lotNumTotal, SqlCommand sqlCommand)
        {
            try
            {
                if (Convert.ToInt32(row["State"]) != 0)
                    return true;
                int betId = Convert.ToInt32(row["Id"]);
                string ssId = row["SsId"].ToString();
                int userId = Convert.ToInt32(row["UserId"]);
                int lotteryId = Convert.ToInt32(row["LotteryId"]);
                int playId = Convert.ToInt32(row["PlayId"]);
                int betNum = Convert.ToInt32(row["Num"]);
                string issNum = row["IssueNum"].ToString();
                string betDetail2 = BetDetailDAL.GetBetDetail2(Convert.ToDateTime(row["STime2"]).ToString("yyyyMMdd"), userId.ToString(), betId.ToString());
                Decimal betMoney = Convert.ToDecimal(row["Total"]); //下注金额
                Decimal point = Convert.ToDecimal(row["point"]);
                Decimal pointMoney = Convert.ToDecimal(row["PointMoney"]);

                //赔率
                Decimal bonus = Convert.ToDecimal(row["Bonus"]);

                //下注倍数
                Decimal times = Convert.ToDecimal(row["Times"]);

                //单注金额
                Decimal singleMoney = Convert.ToDecimal(row["SingleMoney"]);

                string Pos = row["Pos"].ToString();
                string sType = row["PlayCode"].ToString();

                int int32_6 = Convert.ToInt32(row["ZhId"]);
                string STime2 = row["STime"].ToString();
                string[] lotNums = lotNumber.Split(',');

                #region 区间玩法
                if (sType.Equals("P_5QJ3")) //五码区间三星
                {
                    string[] userBet = betDetail2.Split(',');
                    if (userBet[0].IndexOf(CheckOperation.ReplaceStr(lotNums[0])) == -1 || userBet[1].IndexOf(CheckOperation.ReplaceStr(lotNums[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_5QJ3'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_5QJ3", "P_5QJ3_2");
                    }
                    else
                        sType = sType.Replace("P_5QJ3", "P_5QJ3_1");
                }

                if (sType.Equals("P_4QJ3")) //四码区间三星
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(lotNums[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_4QJ3'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_4QJ3", "P_4QJ3_2");
                    }
                    else
                        sType = sType.Replace("P_4QJ3", "P_4QJ3_1");
                }

                if (sType.Equals("P_3QJ2_L")) //前三区间二星
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(lotNums[0])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_3QJ2_L'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QJ2_L", "P_3QJ2_L_2");
                    }
                    else
                        sType = sType.Replace("P_3QJ2_L", "P_3QJ2_L_1");
                }

                if (sType.Equals("P_3QJ2_R")) //后三区间二星
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceStr(lotNums[2])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_3QJ2_R'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QJ2_R", "P_3QJ2_R_2");
                    }
                    else
                        sType = sType.Replace("P_3QJ2_R", "P_3QJ2_R_1");
                }
                #endregion

                #region 趣味玩法
                if (sType.Equals("P_5QW3"))//五码趣味三星 //在个位、十位、百位上至少各选1个号码，并从千位与万位的“大小号”中各任选一种进行投注。
                {
                    string[] userBet = betDetail2.Split(',');
                    if (userBet[0].IndexOf(CheckOperation.ReplaceDX(lotNums[0])) == -1 || userBet[1].IndexOf(CheckOperation.ReplaceDX(lotNums[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_5QW3'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_5QW3", "P_5QW3_2");
                    }
                    else
                        sType = sType.Replace("P_5QW3", "P_5QW3_1");
                }

                if (sType.Equals("P_4QW3")) //四码趣味三星 //选择一个千位的大小号属性，并从百位、十位、个位中至少各选1个号码
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(lotNums[1])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_4QW3'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_4QW3", "P_4QW3_2");
                    }
                    else
                        sType = sType.Replace("P_4QW3", "P_4QW3_1");
                }

                if (sType.Equals("P_3QW2_L"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(lotNums[0])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_3QW2_L'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QW2_L", "P_3QW2_L_2");
                    }
                    else
                        sType = sType.Replace("P_3QW2_L", "P_3QW2_L_1");
                }

                if (sType.Equals("P_3QW2_R"))
                {
                    if (betDetail2.Split(',')[0].IndexOf(CheckOperation.ReplaceDX(lotNums[2])) == -1)
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "select MinBonus2+20*PosBonus2*(0.1*(SELECT top 1 [Point] FROM [N_User] where Id=" + (object)userId + ")-" + (object)point + ") from Sys_PlaySmallType where title2='P_3QW2_R'";
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                        sType = sType.Replace("P_3QW2_R", "P_3QW2_R_2");
                    }
                    else
                        sType = sType.Replace("P_3QW2_R", "P_3QW2_R_1");
                }
                #endregion

                #region 包胆玩法
                if (sType.Equals("P_3ZBD_L")) //前三组选包胆 //从0-9选择一个组成一注
                {
                    if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[2] || lotNums[0] == lotNums[2])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("P_3ZBD_C")) //中三组选包胆
                {
                    if (lotNums[1] == lotNums[2] || lotNums[2] == lotNums[3] || lotNums[1] == lotNums[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[1] != lotNums[2] && lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("P_3ZBD_R"))
                {
                    if (lotNums[2] == lotNums[3] || lotNums[3] == lotNums[4] || lotNums[2] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[2] != lotNums[3] && lotNums[3] != lotNums[4] && lotNums[2] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQB")) //万千百组选包胆
                {
                    if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[2] || lotNums[0] == lotNums[2])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQS"))
                {
                    if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[3] || lotNums[0] == lotNums[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[3] && lotNums[0] != lotNums[3])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WQG"))
                {
                    if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[4] || lotNums[0] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[4] && lotNums[0] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WBS"))
                {
                    if (lotNums[0] == lotNums[2] || lotNums[2] == lotNums[3] || lotNums[0] == lotNums[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[2] && lotNums[2] != lotNums[3] && lotNums[0] != lotNums[3])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WBG"))
                {
                    if (lotNums[0] == lotNums[2] || lotNums[2] == lotNums[4] || lotNums[0] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[2] && lotNums[2] != lotNums[4] && lotNums[0] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_WSG"))
                {
                    if (lotNums[0] == lotNums[3] || lotNums[3] == lotNums[4] || lotNums[0] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[0] != lotNums[3] && lotNums[3] != lotNums[4] && lotNums[0] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QBS"))
                {
                    if (lotNums[1] == lotNums[2] || lotNums[2] == lotNums[3] || lotNums[1] == lotNums[3])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[1] != lotNums[2] && lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QBG"))
                {
                    if (lotNums[1] == lotNums[2] || lotNums[2] == lotNums[4] || lotNums[1] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[1] != lotNums[2] && lotNums[2] != lotNums[4] && lotNums[1] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_QSG"))
                {
                    if (lotNums[1] == lotNums[3] || lotNums[3] == lotNums[4] || lotNums[1] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[1] != lotNums[3] && lotNums[3] != lotNums[4] && lotNums[1] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }

                if (sType.Equals("R_3ZBD_BSG"))
                {
                    if (lotNums[2] == lotNums[3] || lotNums[3] == lotNums[4] || lotNums[2] == lotNums[4])
                        sType = sType.Replace("3ZBD", "3ZBDZ3");
                    if (lotNums[2] != lotNums[3] && lotNums[3] != lotNums[4] && lotNums[2] != lotNums[4])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3ZBD", "3ZBDZ6");
                    }
                }
                #endregion

                #region 和值玩法
                //前三组选和值
                if (sType.Equals("P_3ZHE_L") && lotNums[0] != lotNums[1] && (lotNums[0] != lotNums[2] && lotNums[1] != lotNums[2]))
                    bonus /= new Decimal(2);

                //中三组选和值
                if (sType.Equals("P_3ZHE_C") && lotNums[1] != lotNums[2] && (lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3]))
                    bonus /= new Decimal(2);

                //后三组选和值
                if (sType.Equals("P_3ZHE_R") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2]))
                    bonus /= new Decimal(2);
                #endregion

                #region 混选玩法
                //三星组选混选 //手动输入号码，至少输入1个三位数号码组成一注
                if (sType.Equals("P_3HX_L"))
                {
                    if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[2] || lotNums[0] == lotNums[2])
                        sType = sType.Replace("3HX", "3Z3_2");
                    if (lotNums[0] != lotNums[1] && lotNums[0] != lotNums[2] && lotNums[1] != lotNums[2])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3HX", "3Z6_2");
                    }
                }

                //中三混合组选 //手动输入号码，至少输入1个三位数号码组成一注
                if (sType.Equals("P_3HX_C"))
                {
                    if (lotNums[1] == lotNums[2] || lotNums[2] == lotNums[3] || lotNums[1] == lotNums[3])
                        sType = sType.Replace("3HX", "3Z3_2");
                    if (lotNums[1] != lotNums[2] && lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3])
                    {
                        bonus /= new Decimal(2);
                        sType = sType.Replace("3HX", "3Z6_2");
                    }
                }

                if (sType.Equals("P_3HX_R"))
                {
                    if (lotNums.Length == 3)
                    {
                        if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[2] || lotNums[0] == lotNums[2])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2])
                        {
                            bonus /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                    else
                    {
                        if (lotNums[2] == lotNums[3] || lotNums[3] == lotNums[4] || lotNums[2] == lotNums[4])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (lotNums[2] != lotNums[3] && lotNums[3] != lotNums[4] && lotNums[2] != lotNums[4])
                        {
                            bonus /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                }

                //任三组选混选 //从万位、千位、百位、十位，个位中至少选择三个位置，手动至少输入1个三位数号码组成一注（不包含豹子号）
                if (sType.Contains("R_3HX"))
                {
                    if (lotNums.Length == 3)
                    {
                        if (lotNums[0] == lotNums[1] || lotNums[1] == lotNums[2] || lotNums[0] == lotNums[2])
                            sType = sType.Replace("3HX", "3Z3_2");
                        if (lotNums[0] != lotNums[1] && lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2])
                        {
                            bonus /= new Decimal(2);
                            sType = sType.Replace("3HX", "3Z6_2");
                        }
                    }
                    else
                    {
                        if (sType.Equals("R_3HX_WQB") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_WQS") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[3] && lotNums[0] != lotNums[3]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_WQG") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[4] && lotNums[0] != lotNums[4]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_WBS") && lotNums[0] != lotNums[2] && (lotNums[2] != lotNums[3] && lotNums[0] != lotNums[3]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_WBG") && lotNums[0] != lotNums[2] && (lotNums[2] != lotNums[4] && lotNums[0] != lotNums[4]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_WSG") && lotNums[0] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[0] != lotNums[4]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_QBS") && lotNums[1] != lotNums[2] && (lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_QBG") && lotNums[1] != lotNums[2] && (lotNums[2] != lotNums[4] && lotNums[1] != lotNums[4]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_QSG") && lotNums[1] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[1] != lotNums[4]))
                            bonus /= new Decimal(2);
                        if (sType.Equals("R_3HX_BSG") && lotNums[2] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[2] != lotNums[4]))
                            bonus /= new Decimal(2);
                    }
                }

                #endregion

                #region 组选和值玩法

                if (sType.Contains("R_3ZHE"))
                {
                    if (sType.Equals("R_3ZHE_WQB") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[2] && lotNums[0] != lotNums[2]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WQS") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[3] && lotNums[0] != lotNums[3]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WQG") && lotNums[0] != lotNums[1] && (lotNums[1] != lotNums[4] && lotNums[0] != lotNums[4]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WBS") && lotNums[0] != lotNums[2] && (lotNums[2] != lotNums[3] && lotNums[0] != lotNums[3]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WBG") && lotNums[0] != lotNums[2] && (lotNums[2] != lotNums[4] && lotNums[0] != lotNums[4]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_WSG") && lotNums[0] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[0] != lotNums[4]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QBS") && lotNums[1] != lotNums[2] && (lotNums[2] != lotNums[3] && lotNums[1] != lotNums[3]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QBG") && lotNums[1] != lotNums[2] && (lotNums[2] != lotNums[4] && lotNums[1] != lotNums[4]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_QSG") && lotNums[1] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[1] != lotNums[4]))
                        bonus /= new Decimal(2);
                    if (sType.Equals("R_3ZHE_BSG") && lotNums[2] != lotNums[3] && (lotNums[3] != lotNums[4] && lotNums[2] != lotNums[4]))
                        bonus /= new Decimal(2);
                }

                #endregion

                #region 龙虎和
                int num7 = 0;
                int num8 = 0;

                //万千
                if (sType.Equals("P_LHH_WQ"))
                {
                    num7 = Convert.ToInt32(lotNums[0]);
                    num8 = Convert.ToInt32(lotNums[1]);
                }

                //万百
                if (sType.Equals("P_LHH_WB"))
                {
                    num7 = Convert.ToInt32(lotNums[0]);
                    num8 = Convert.ToInt32(lotNums[2]);
                }

                if (sType.Equals("P_LHH_WS"))
                {
                    num7 = Convert.ToInt32(lotNums[0]);
                    num8 = Convert.ToInt32(lotNums[3]);
                }

                if (sType.Equals("P_LHH_WG"))
                {
                    num7 = Convert.ToInt32(lotNums[0]);
                    num8 = Convert.ToInt32(lotNums[4]);
                }

                if (sType.Equals("P_LHH_QB"))
                {
                    num7 = Convert.ToInt32(lotNums[1]);
                    num8 = Convert.ToInt32(lotNums[2]);
                }

                if (sType.Equals("P_LHH_QS"))
                {
                    num7 = Convert.ToInt32(lotNums[1]);
                    num8 = Convert.ToInt32(lotNums[3]);
                }

                if (sType.Equals("P_LHH_QG"))
                {
                    num7 = Convert.ToInt32(lotNums[1]);
                    num8 = Convert.ToInt32(lotNums[4]);
                }

                if (sType.Equals("P_LHH_BS"))
                {
                    num7 = Convert.ToInt32(lotNums[2]);
                    num8 = Convert.ToInt32(lotNums[3]);
                }

                if (sType.Equals("P_LHH_BG"))
                {
                    num7 = Convert.ToInt32(lotNums[2]);
                    num8 = Convert.ToInt32(lotNums[4]);
                }

                if (sType.Equals("P_LHH_SG"))
                {
                    num7 = Convert.ToInt32(lotNums[3]);
                    num8 = Convert.ToInt32(lotNums[4]);
                }

                if (num7 != num8)
                    bonus = Convert.ToDecimal(bonus / Convert.ToDecimal(4.5));
                #endregion

                #region 快三
                if (lotteryId == 5005) //快三
                {
                    if (sType.Equals("K_3HZ")) //和值
                    {
                        sType = string.Format("{0}{1}", sType, lotNumTotal);

                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = string.Format("select MinBonus from Sys_PlaySmallType where title2='{0}'", sType);
                        bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                    }
                    //前端已计算，并存入DB
                    //else
                    //{ //三同号单选, 三同号通选, 二同号单选, 二同号通选
                    //    sqlCommand.CommandType = CommandType.Text;
                    //    sqlCommand.CommandText = string.Format("select MinBonus from Sys_PlaySmallType where title2='{0}'", sType);
                    //    bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                    //}
                }
                #endregion

                #region 11选5，任选拖胆
                //前端已计算，并存入DB
                //if (sType.StartsWith("P11_RXTD")) //任选拖胆
                //{
                //    sqlCommand.CommandType = CommandType.Text;
                //    sqlCommand.CommandText = string.Format("select MinBonus from Sys_PlaySmallType where title2='{0}'", sType);
                //    bonus = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                //}
                #endregion

                //中奖注数
                int winNum = CheckPlay.Check(lotNumber, betDetail2, Pos, sType);

                Decimal Money1 = pointMoney * times;
                int num10;

                //奖金金额
                Decimal winMoney;

                if (winNum > 0)
                {
                    num10 = 3;

                    //3152.3800 * 1 *  num9 * 2 / 2.0
                    if (lotteryId == 6001)
                    {
                        switch (sType)
                        {
                            case "H_ZMDX":
                            case "H_ZMDS":
                            case "H_ZMHSDX":
                            case "H_ZMHSDS":
                            case "H_ZMWSDX":
                                int draw = CheckHK3_Start.checkDrawZM(lotNumber);
                                winMoney = 0.0M;

                                if(draw > 0)
                                {
                                    winMoney = draw * times * singleMoney;
                                }

                                winMoney += bonus * times * (Decimal)winNum * singleMoney / 2.0M;
                                break;
                            case "H_TMDX":
                            case "H_TMDS":
                            case "H_TMHDX":
                            case "H_TMHDS":
                            case "H_TMWDX":
                            case "H_TMWDS":
                            case "H_TMBT":
                            case "H_TMBB":
                                if (CheckHK3_Start.isDrawTM(lotNumber))
                                {
                                    num10 = 4;
                                    winMoney = betMoney;
                                }
                                else
                                {
                                    winMoney = bonus * times * (Decimal)winNum * singleMoney / 2.0M;
                                }

                                break;
                            default:
                                winMoney = bonus * times * (Decimal)winNum * singleMoney / 2.0M;
                                break;
                        }
                    }
                    else
                    {
                        winMoney = bonus * times * (Decimal)winNum * singleMoney / 2.0M;
                    }

                    //奖金(赔率) * 倍数 * 中奖注数，
                    //winMoney = bonus * times * winNum * singleMoney;

                    Decimal num11 = new Decimal(200000);
                    if (winMoney > num11)
                        winMoney = num11;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select top 1 MinNum from Sys_PlaySmallType where Id=" + (object)playId;
                    Decimal num12 = Convert.ToDecimal(sqlCommand.ExecuteScalar().ToString());
                    if (num12 == new Decimal(0))
                    {
                        if (winMoney > betMoney * times * new Decimal(100))
                        {
                            Decimal num13 = new Decimal(18000);
                            if (winMoney > num13)
                                winMoney = num13;
                        }
                    }
                    else if ((Decimal)betNum < num12)
                    {
                        Decimal num13 = new Decimal(18000);
                        if (winMoney > num13)
                            winMoney = num13;
                    }
                }
                else
                {
                    num10 = 2;
                    winMoney = new Decimal(0);
                }

                Decimal num14 = winMoney + Money1 - betMoney * times;

                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "update N_UserBet set State=" + num10.ToString() + ",WinNum=" + winNum.ToString() + ",WinBonus=" + winMoney.ToString() + ",RealGet=" + num14.ToString() + " where Id=" + betId.ToString();
                sqlCommand.ExecuteNonQuery();
                if (winMoney > new Decimal(0))
                    new UserTotalTran().MoneyOpers(ssId, userId.ToString(), winMoney, lotteryId, playId, betId, 5, 99, "", "", "奖金派发", STime2);
                if (Money1 > new Decimal(0))
                    new UserTotalTran().MoneyOpers(ssId, userId.ToString(), Money1, lotteryId, playId, betId, 4, 99, "", "", "返点派发", STime2);
                if (int32_6 != 0)
                {
                    string str2 = string.Format(" where LotteryId={0} and state=0 and zhid={1} and IssueNum>'{2}'", (object)lotteryId, (object)int32_6, (object)issNum);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "select count(Id) from N_UserBet" + str2;
                    if (Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0 && winNum > 0)
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
                            new UserTotalTran().MoneyOpers(ssId, userId.ToString(), Money3, lotteryId, playId, betId, 6, 99, "", "", "终止追号", STime2);
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