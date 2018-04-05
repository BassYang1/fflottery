// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LotteryCheck
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Lottery.DAL
{
    public class LotteryCheck : ComData
    {
        /// <summary>
        /// 开奖
        /// </summary>
        /// <param name="Type">彩种Id</param>
        /// <param name="Title">期号</param>
        public static void RunOfIssueNum(int LotteryId, string IssueNum)
        {
            LotteryCheck.DoWord doWord = new LotteryCheck.DoWord(LotteryCheck.RunOper);
            doWord.BeginInvoke(LotteryId, IssueNum, new AsyncCallback(LotteryCheck.CallBack), (object)doWord);
        }

        public static void CallBack(IAsyncResult r)
        {
            LotteryCheck.DoWord asyncState = (LotteryCheck.DoWord)r.AsyncState;
        }

        /// <summary>
        /// 开奖
        /// </summary>
        /// <param name="Type">彩种Id</param>
        /// <param name="Title">期号</param>
        /// <returns></returns>
        public static string RunOper(int Type, string Title)
        {
            string str1 = "";
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    //开奖信息
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 Type,Title,Number from Sys_LotteryData where Type={0} and Title='{1}'", (object)Type, (object)Title);
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter.Fill(dataTable1);

                    if (dataTable1.Rows.Count > 0)
                    {
                        //投注信息
                        string LotteryNumber = dataTable1.Rows[0]["Number"].ToString();
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                        sqlDataAdapter.SelectCommand.CommandText = string.Format(@"select b.username,b.point as uPoint,a.*
                                                                                    From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id 
                                                                                    where a.State=0 and LotteryId={0} and IssueNum='{1}'",
                                                                                                                                         (object)dataTable1.Rows[0]["Type"].ToString(),
                                                                                                                                         (object)dataTable1.Rows[0]["Title"].ToString());
                        DataTable dataTable2 = new DataTable("N_UserBet");
                        sqlDataAdapter.Fill(dataTable2);


                        if (dataTable2.Rows.Count > 0)
                        {
                            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                            {
                                if (Convert.ToInt32(row["State"].ToString()) == 0)
                                    CheckOperation.Checking(row, LotteryNumber, sqlCommand);
                            }

                            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                            {
                                string UserName = row["UserName"].ToString();
                                int int32_1 = Convert.ToInt32(row["uPoint"]);
                                int int32_2 = Convert.ToInt32(row["Id"]);
                                string ssId = row["SsId"].ToString();
                                int int32_3 = Convert.ToInt32(row["UserId"]);
                                int int32_4 = Convert.ToInt32(row["LotteryId"]);
                                int int32_5 = Convert.ToInt32(row["PlayId"]);
                                Decimal num1 = Convert.ToDecimal(row["Total"]);
                                Decimal num2 = Convert.ToDecimal(row["Times"]);
                                CheckOperation.AgencyPoint(ssId, int32_3, UserName, int32_1, int32_4, int32_5, int32_2, Convert.ToDecimal(num1 * num2), sqlCommand);
                            }

                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                            sqlDataAdapter.SelectCommand.CommandText = string.Format("select UserId,sum(times*total) as bet,sum(WinBonus) as win,sum(RealGet) as RealGet  From N_UserBet with(nolock) \r\n                                                            where lotteryId={0} and IssueNum='{1}' group by UserId", (object)Type, (object)Title);
                            DataTable dataTable3 = new DataTable();
                            sqlDataAdapter.Fill(dataTable3);
                            foreach (DataRow row in (InternalDataCollectionBase)dataTable3.Rows)
                            {
                                string UserId = row["UserId"].ToString();
                                string str2 = LotteryUtils.LotteryTitle(Type);
                                string str3 = Title;
                                string str4 = Convert.ToDecimal(row["bet"]).ToString("0.0000");
                                string str5 = Convert.ToDecimal(row["win"]).ToString("0.0000");
                                string str6 = Convert.ToDecimal(row["RealGet"]).ToString("0.0000");
                                string content = "投注彩种 " + str2 + "<br/>" + "投注期号 " + str3 + "<br/>" + "投注金额 " + str4 + "元<br/>" + "中奖金额 " + str5 + "元<br/>" + "本次盈亏 " + str6 + "元";
                                LotteryCheck.SetUserJson(UserId, Type.ToString() + str3, content);
                            }
                            dataTable2.Dispose();
                            dataTable1.Dispose();
                        }
                        else
                            str1 = "该期没有开奖号码，请手动添加！";
                    }
                    else
                        str1 = "该期没有开奖号码，请手动添加！";
                }
                catch (Exception ex)
                {
                    str1 = "派奖出现错误，请重试！";
                    new LogExceptionDAL().Save("派奖异常", ex.Message);
                }
                finally
                {
                    sqlConnection.Dispose();
                    sqlConnection.Close();
                }
            }
            return str1;
        }

        public static void RunYouleOfIssueNum(int LotteryId, string IssueNum, string Number)
        {
            LotteryCheck.DoWordYoule doWordYoule = new LotteryCheck.DoWordYoule(LotteryCheck.YouleRunOper);
            doWordYoule.BeginInvoke(LotteryId, IssueNum, Number, new AsyncCallback(LotteryCheck.CallBackYoule), (object)doWordYoule);
        }

        public static void CallBackYoule(IAsyncResult r)
        {
            LotteryCheck.DoWordYoule asyncState = (LotteryCheck.DoWordYoule)r.AsyncState;
        }

        public static string YouleRunOper(int LotteryId, string IssueNum, string Number)
        {
            string str1 = "";
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    string LotteryNumber = Number;
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = "select b.username,b.point as uPoint,* From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id where State=0 and LotteryId=" + (object)LotteryId + " and IssueNum='" + IssueNum + "'";
                    DataTable dataTable1 = new DataTable("N_UserBet");
                    sqlDataAdapter.Fill(dataTable1);
                    if (dataTable1.Rows.Count > 0)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                        {
                            if (Convert.ToInt32(row["State"].ToString()) == 0)
                                CheckOperation.Checking(row, LotteryNumber, sqlCommand);
                        }
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                        {
                            string UserName = row["UserName"].ToString();
                            int int32_1 = Convert.ToInt32(row["uPoint"]);
                            int int32_2 = Convert.ToInt32(row["Id"]);
                            string ssId = row["SsId"].ToString();
                            int int32_3 = Convert.ToInt32(row["UserId"]);
                            int int32_4 = Convert.ToInt32(row["PlayId"]);
                            Decimal num1 = Convert.ToDecimal(row["Total"]);
                            Decimal num2 = Convert.ToDecimal(row["Times"]);
                            CheckOperation.AgencyPoint(ssId, int32_3, UserName, int32_1, LotteryId, int32_4, int32_2, Convert.ToDecimal(num1 * num2), sqlCommand);
                        }
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                        sqlDataAdapter.SelectCommand.CommandText = string.Format("select UserId,sum(times*total) as bet,sum(WinBonus) as win,sum(RealGet) as RealGet  From N_UserBet with(nolock) \r\n                                                            where lotteryId={0} and IssueNum='{1}' group by UserId", (object)LotteryId, (object)IssueNum);
                        DataTable dataTable2 = new DataTable();
                        sqlDataAdapter.Fill(dataTable2);
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                        {
                            string UserId = row["UserId"].ToString();
                            string str2 = LotteryUtils.LotteryTitle(LotteryId);
                            string str3 = IssueNum;
                            string str4 = Convert.ToDecimal(row["bet"]).ToString("0.0000");
                            string str5 = Convert.ToDecimal(row["win"]).ToString("0.0000");
                            string str6 = Convert.ToDecimal(row["RealGet"]).ToString("0.0000");
                            string content = "投注彩种 " + str2 + "<br/>" + "投注期号 " + str3 + "<br/>" + "投注金额 " + str4 + "元<br/>" + "中奖金额 " + str5 + "元<br/>" + "本次盈亏 " + str6 + "元";
                            LotteryCheck.SetUserJson(UserId, LotteryId.ToString() + str3, content);
                        }
                        dataTable2.Dispose();
                        dataTable1.Dispose();
                    }
                    else
                        str1 = "该期没有开奖号码，请手动添加！";
                }
                catch (Exception ex)
                {
                    str1 = "派奖出现错误，请重试！";
                    new LogExceptionDAL().Save("派奖异常", ex.Message);
                }
                finally
                {
                    sqlConnection.Dispose();
                    sqlConnection.Close();
                }
            }
            return str1;
        }

        public static string AdminRunOper(int LotteryId, string IssueNum, string Number)
        {
            string str = "";
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = "select b.username,b.point as uPoint,* From N_UserBet a with(nolock) left join N_User b on a.UserId=b.Id where State=0 and LotteryId=" + (object)LotteryId + " and IssueNum='" + IssueNum + "'";
                    DataTable dataTable = new DataTable("N_UserBet");
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        {
                            string LotteryNumber = Number;
                            if (Convert.ToInt32(row["State"].ToString()) == 0)
                                CheckOperation.Checking(row, LotteryNumber, sqlCommand);
                        }
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        {
                            string UserName = row["UserName"].ToString();
                            int int32_1 = Convert.ToInt32(row["uPoint"]);
                            int int32_2 = Convert.ToInt32(row["Id"]);
                            string ssId = row["SsId"].ToString();
                            int int32_3 = Convert.ToInt32(row["UserId"]);
                            int int32_4 = Convert.ToInt32(row["PlayId"]);
                            Decimal num1 = Convert.ToDecimal(row["Total"]);
                            Decimal num2 = Convert.ToDecimal(row["Times"]);
                            CheckOperation.AgencyPoint(ssId, int32_3, UserName, int32_1, LotteryId, int32_4, int32_2, Convert.ToDecimal(num1 * num2), sqlCommand);
                        }
                        dataTable.Dispose();
                    }
                    else
                        str = "该期没有开奖号码，请手动添加！";
                }
                catch (Exception ex)
                {
                    str = "派奖出现错误，请重试！";
                    new LogExceptionDAL().Save("派奖异常", ex.Message);
                }
                finally
                {
                    sqlConnection.Dispose();
                    sqlConnection.Close();
                }
            }
            return str;
        }

        public string RunOfBetId(string BetId)
        {
            return "";
        }

        public void Cancel(int LotteryId, string title, int state)
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
                    string str = LotteryUtils.LotteryTitle(LotteryId);
                    if (state == 0)
                    {
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                        sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet with(nolock)  where state=0 and LotteryId='" + LotteryId.ToString() + "' and IssueNum='" + title + "' order by id asc";
                    }
                    else
                    {
                        sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                        sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet with(nolock)  where (state<>0 and state<>1) and LotteryId='" + LotteryId.ToString() + "' and IssueNum='" + title + "' order by id asc";
                    }
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        int int32 = Convert.ToInt32(row["id"].ToString());
                        Convert.ToInt32(row["UserId"].ToString());
                        if (!CheckOperation.AdminCancel(int32, sqlCommand))
                            new LogExceptionDAL().Save("派奖异常", str + " 投注ID:" + (object)int32 + "撤单");
                    }
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("程序异常", ex.Message);
                }
                finally
                {
                    if (sqlConnection != null)
                        sqlConnection.Close();
                }
            }
        }

        public void CancelOfBetId(string betId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    CheckOperation.AdminCancel(Convert.ToInt32(betId), sqlCommand);
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("程序异常", ex.Message);
                }
                finally
                {
                    if (sqlConnection != null)
                        sqlConnection.Close();
                }
            }
        }

        public void CancelToNoOfTitle(string lotteryId, string title)
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
                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.SelectCommand.CommandText = "select * From N_UserBet with(nolock)  where lotteryId=" + lotteryId + " and (state<>0 and state<>1) and IssueNum='" + title + "' order by id asc";
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                    {
                        int int32 = Convert.ToInt32(row["id"].ToString());
                        Convert.ToInt32(row["UserId"].ToString());
                        CheckOperation.AdminCancelToNO(int32, sqlCommand);
                    }
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("程序异常", ex.Message);
                }
                finally
                {
                    if (sqlConnection != null)
                        sqlConnection.Close();
                }
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

        public static void GroupDataRows(IEnumerable<DataRow> source, List<DataTable> destination, string[] groupByFields, int fieldIndex, DataTable schema)
        {
            if (fieldIndex >= groupByFields.Length || fieldIndex < 0)
            {
                DataTable dataTable = schema.Clone();
                foreach (DataRow dataRow in source)
                {
                    DataRow row = dataTable.NewRow();
                    row.ItemArray = dataRow.ItemArray;
                    dataTable.Rows.Add(row);
                }
                destination.Add(dataTable);
            }
            else
            {
                foreach (IEnumerable<DataRow> source1 in source.GroupBy<DataRow, object>((Func<DataRow, object>)(o => o[groupByFields[fieldIndex]])))
                    LotteryCheck.GroupDataRows(source1, destination, groupByFields, fieldIndex + 1, schema);
                ++fieldIndex;
            }
        }

        public static void SetUserJson(string UserId, string title, string content)
        {
            string str1 = "[{\"userid\":\"" + UserId + "\",\"title\":\"" + title + "\",\"content\":\"" + content + "\"}]";
            string str2 = ConfigurationManager.AppSettings["DataUrl"].ToString() + "User\\User" + UserId + ".xml";
            DirFile.CreateFolder(DirFile.GetFolderPath(false, str2));
            StreamWriter streamWriter = new StreamWriter(str2, false, Encoding.UTF8);
            streamWriter.Write(str1);
            streamWriter.Close();
        }

        public delegate string DoWord(int LotteryId, string IssueNum);

        public delegate string DoWordYoule(int LotteryId, string IssueNum, string Number);
    }
}
