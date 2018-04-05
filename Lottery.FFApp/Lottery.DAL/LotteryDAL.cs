// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LotteryDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.FFCache;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Linq;

namespace Lottery.DAL
{
    public class LotteryDAL : ComData
    {
        public void GetLotteryTime(string Lid, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                string str1 = "[{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\"}]".Replace("名称", LotteryUtils.LotteryTitle(int.Parse(Lid))).Replace("彩种类别", Lid);
                DateTime dateTime1 = DateTime.Now;
                DateTime now = DateTime.Now;
                string str2 = now.ToString("yyyyMMdd");
                string str3 = now.ToString("HH:mm:ss");
                now.ToString("yyyy-MM-dd");
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select dbo.f_GetCloseTime(" + Lid + ") as closetime";
                DataTable dataTable1 = dbOperHandler.GetDataTable();
                string str4 = str1.Replace("封单时间", dataTable1.Rows[0]["closetime"].ToString());
                string newValue1;
                string newValue2;
                TimeSpan timeSpan;
                if (Lid == "3002" || Lid == "3003")
                {
                    DateTime dateTime2 = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime2.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    int Num = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7 + 1;
                    string str5 = now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                    string str6 = now.ToString("yyyy-MM-dd") + " 20:30:00";
                    if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
                        str6 = now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                    else
                        --Num;
                    newValue1 = now.Year.ToString() + Func.AddZero(Num, 3);
                    newValue2 = now.Year.ToString() + Func.AddZero(Num + 1, 3);
                    timeSpan = Convert.ToDateTime(str6) - Convert.ToDateTime(str3);
                }
                else
                {
                    if (ComData.LotteryTime == null)
                        ComData.LotteryTime = new LotteryTimeDAL().GetTable();
                    DataRow[] dataRowArray1 = ComData.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + Lid, "Time asc");
                    if (dataRowArray1.Length == 0)
                    {
                        dataRowArray1 = ComData.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + Lid, "Time asc");
                        newValue2 = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                    }
                    else
                    {
                        newValue2 = str2 + "-" + dataRowArray1[0]["Sn"].ToString();
                        dateTime1 = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString());
                        if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && Lid == "1003")
                            newValue2 = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                    }
                    if (Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()) < Convert.ToDateTime(str3))
                        dateTime1 = Convert.ToDateTime(now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRowArray1[0]["Time"].ToString());
                    timeSpan = dateTime1 - Convert.ToDateTime(str3);
                    DataRow[] dataRowArray2 = ComData.LotteryTime.Select("Time <'" + str3 + "' and LotteryId=" + Lid, "Time desc");
                    if (dataRowArray2.Length == 0)
                    {
                        dataRowArray2 = ComData.LotteryTime.Select("LotteryId=" + Lid, "Time desc");
                        newValue1 = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    }
                    else
                    {
                        newValue1 = str2 + "-" + dataRowArray2[0]["Sn"].ToString();
                        if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && Lid == "1003")
                            newValue1 = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    }
                    if (Lid == "4001")
                    {
                        DateTime dateTime2 = Convert.ToDateTime("2016-01-01 00:00:00");
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime2.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                        DataTable dataTable2 = dbOperHandler.GetDataTable();
                        if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 09:07:01") || now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:57:01") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59"))
                        {
                            newValue1 = string.Concat((object)(530900 + (Convert.ToInt32(dataTable2.Rows[0]["d"]) - 8) * 179 + Convert.ToInt32(dataRowArray2[0]["Sn"])));
                            newValue2 = string.Concat((object)(530900 + (Convert.ToInt32(dataTable2.Rows[0]["d"]) - 8) * 179 + Convert.ToInt32(dataRowArray2[0]["Sn"]) + 1));
                        }
                        else
                        {
                            newValue1 = string.Concat((object)(530900 + (Convert.ToInt32(dataTable2.Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(dataRowArray2[0]["Sn"])));
                            newValue2 = string.Concat((object)(530900 + (Convert.ToInt32(dataTable2.Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(dataRowArray2[0]["Sn"]) + 1));
                        }
                    }
                    if (Lid == "1010" || Lid == "1017" || Lid == "3004")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1010") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                    if (Lid == "1012")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                    if (Lid == "1013")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                }
                string newValue3 = string.Concat((object)(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
                string str7 = str4.Replace("下期期号", newValue2).Replace("当前期号", newValue1).Replace("倒计时", newValue3);
                _jsonstr = str7;
            }
        }

        public void GetLotteryZhList(string Lid, ref string _jsonstr)
        {
            DateTime now = DateTime.Now;
            string str1 = now.ToString("yyyyMMdd");
            string str2 = now.ToString("HH:mm:ss");
            now.ToString("yyyy-MM-dd");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                if (Lid == "3002" || Lid == "3003")
                {
                    DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    int num = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7 + 1;
                    string str3 = now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                    string str4 = now.ToString("yyyy-MM-dd") + " 20:30:00";
                    if (now > Convert.ToDateTime(now.ToString(" 20:30:00")))
                    {
                        string str5 = now.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                    }
                    else
                        --num;
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int index = 0; index <= 9; ++index)
                    {
                        string str6 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},".Replace("编号", (index + 1).ToString()).Replace("期号", now.Year.ToString() + Func.AddZero(num + (index + 1), 3)).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.AddDays((double)index).ToString("yyyy-MM-dd") + " 20:30:00");
                        stringBuilder.Append(str6);
                    }
                    _jsonstr = "[" + stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1) + "]";
                }
                else
                {
                    if (ComData.LotteryTime == null)
                        ComData.LotteryTime = new LotteryTimeDAL().GetTable();
                    DataRow[] dataRowArray = ComData.LotteryTime.Select("Time >'" + str2 + "' and LotteryId=" + Lid, "Time asc");
                    if (dataRowArray.Length == 0)
                    {
                        dataRowArray = ComData.LotteryTime.Select("Time <='" + str2 + "' and LotteryId=" + Lid, "Time asc");
                        str1 = now.AddDays(1.0).ToString("yyyyMMdd");
                    }
                    int num = ComData.LotteryTime.Select("LotteryId=" + Lid, "Time asc").Length;
                    if (num > 120)
                        num = 120;
                    StringBuilder stringBuilder = new StringBuilder();
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top " + (object)num + " * from Sys_LotteryTime where lotteryid=" + Lid + " and sn>=" + dataRowArray[0]["Sn"].ToString() + "order by sn asc";
                    DataTable dataTable1 = dbOperHandler.GetDataTable();
                    for (int index = 0; index < dataTable1.Rows.Count; ++index)
                    {
                        string str3 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
                        string newValue = str1 + "-" + dataTable1.Rows[index]["sn"].ToString();
                        if (Lid == "1010" || Lid == "1017" || (Lid == "3004" || Lid == "1012") || Lid == "1013")
                            newValue = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(Lid) + Convert.ToInt32(dataTable1.Rows[index]["sn"].ToString())));
                        string str4 = str3.Replace("编号", (index + 1).ToString()).Replace("期号", newValue).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.ToString("yyyy-MM-dd") + " " + dataTable1.Rows[index]["time"]);
                        stringBuilder.Append(str4);
                    }
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top " + (object)(num - dataTable1.Rows.Count) + " * from Sys_LotteryTime where lotteryid=" + Lid + " order by sn asc";
                    DataTable dataTable2 = dbOperHandler.GetDataTable();
                    for (int index = 0; index < dataTable2.Rows.Count; ++index)
                    {
                        string str3 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
                        string newValue = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable2.Rows[index]["sn"].ToString();
                        if (Lid == "1010" || Lid == "1017" || (Lid == "3004" || Lid == "1012") || Lid == "1013")
                            newValue = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(Lid) + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString())));
                        string str4 = str3.Replace("编号", (index + 1 + dataTable1.Rows.Count).ToString()).Replace("期号", newValue).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", now.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataTable2.Rows[index]["time"]);
                        stringBuilder.Append(str4);
                    }
                    _jsonstr = "[" + stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1) + "]";
                }
            }
        }

        public void GetLotteryNumber(string Lid, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT Title,Number FROM Sys_LotteryData \r\n                            where Type={0} and Title=(select max(Title) from [Sys_LotteryData] where Type={0})", (object)Lid);
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetNumberMmc(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT TOP 10 [Id]\r\n                                      ,[Type]\r\n                                      ,[Title]\r\n                                      ,[Number]\r\n                                      ,[Total]\r\n                                FROM [Sys_LotteryData] where Type=1006 and Title in (\r\n                                SELECT IssueNum FROM [N_UserBet] where lotteryId=1006 and UserId=" + UserId + ") order by Id desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetPlayTypeXml(ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT [Id],[TypeId],[Title] FROM [Sys_PlayBigType] where TypeId=2 and IsOpen=1 order by sort ";
                DataTable dataTable1 = dbOperHandler.GetDataTable();
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT [Id]\r\n                                  ,[Title0]\r\n                                  ,[Title]\r\n                                  ,[Title2]\r\n                                  ,[Radio]\r\n                              FROM [Sys_PlaySmallType] \r\n                              where IsOpen=1 and flag=0 order by Id ";
                DataTable dataTable2 = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToXML(dataTable1, dataTable2);
                dataTable1.Clear();
                dataTable1.Dispose();
            }
        }

        public void GetPlayListXml(int lotteryId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                string str = "SELECT * FROM [Sys_PlaySmallType] where lotteryId=" + (object)lotteryId;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = str;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.CDataToXml(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void Create(int loid)
        {
            string str1 = ConfigurationManager.AppSettings["DataUrl"].ToString();
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT top 1 [Title],[Number] FROM [Sys_LotteryData] where Type=" + (object)loid + " order by Title desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                string str2 = "{\"title\": \"期号\",\"number\": \"号码\"}";
                string str3 = dataTable.Rows.Count <= 0 ? str2.Replace("期号", "").Replace("号码", "") : str2.Replace("期号", dataTable.Rows[0]["Title"].ToString()).Replace("号码", dataTable.Rows[0]["Number"].ToString());
                dataTable.Clear();
                dataTable.Dispose();
                string str4 = str1 + "EMindexData" + (object)loid + ".json";
                DirFile.CreateFolder(DirFile.GetFolderPath(false, str4));
                StreamWriter streamWriter = new StreamWriter(str4, false, Encoding.UTF8);
                streamWriter.Write(str3);
                streamWriter.Close();
            }
        }

        public bool IsAuto(int lotteryId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Id=@Id";
                dbOperHandler.AddConditionParameter("@Id", (object)lotteryId);
                return Convert.ToInt32(dbOperHandler.GetField("Sys_Lottery", "IsAuto")) == 0;
            }
        }

        public DataTable GetAutoUrl(int lotteryId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 IsAuto,isnull((select Url from Sys_lotteryUrl where Id=a.AutoUrl),'0') as AutoUrl \r\n                            ,isnull((select Title from Sys_lotteryUrl where Id=a.AutoUrl),'0') as Title\r\n                            from Sys_lottery a where Id=" + (object)lotteryId;
                return dbOperHandler.GetDataTable();
            }
        }

        public static DataTable GetDataTable(string lotteryId, string IssueNum)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.SelectCommand.CommandText = "select [Id],[UserId],[PlayCode],[Times],[Total],[STime2],[Pos],[PointMoney],[Bonus],[SingleMoney] from N_UserBet where state=0 and lotteryId=" + lotteryId + " and IssueNum='" + IssueNum + "'";
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public static Decimal GetCurRealGet(int lotteryId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT case isnull(sum(Total*Times),0) when 0 then 0 else isnull(-sum(realGet),-0)*100/isnull(sum(Total*Times),0) end as win FROM [N_UserBet] where state>=2 and DateDiff(dd,STime,getdate())=0 and lotteryId=" + (object)lotteryId;
                return Convert.ToDecimal(dbOperHandler.GetDataTable().Rows[0]["win"]);
            }
        }

        public static DataTable GetLotteryCheck(int lotteryId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT top 1 [CheckNum],[CheckPer] FROM [Sys_LotteryCheck] where Id=" + (object)lotteryId;
                return dbOperHandler.GetDataTable();
            }
        }

        public string GetListSn(int loid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                DateTime now = DateTime.Now;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " and Time < '" + now.ToString("HH:mm:ss") + "' order by time desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                string str;
                if (dataTable.Rows.Count < 1)
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " order by time desc";
                    dataTable = dbOperHandler.GetDataTable();
                    str = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                }
                else
                    str = now.ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                if (loid == 4001)
                {
                    DateTime dateTime = Convert.ToDateTime("2016-01-01 00:00:00");
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    str = string.Concat((object)(530900 + Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) * 179 + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString())));
                }
                if (loid == 1010 || loid == 1012 || (loid == 1017 || loid == 3004) || loid == 1013)
                    str = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(loid.ToString()) + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString())));
                if (loid == 1014 || loid == 1016)
                {
                    if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:01:30") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59"))
                        str = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                    str = str.Replace("-", "");
                }
                dataTable.Clear();
                dataTable.Dispose();
                return str;
            }
        }

        public string GetListNextSn(int loid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                DateTime now = DateTime.Now;
                string str;
                if (loid == 3002 || loid == 3003)
                {
                    DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    int num = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7 + 1;
                    if (now < Convert.ToDateTime(now.ToString(" 20:30:00")))
                        --num;
                    str = now.Year.ToString() + Func.AddZero(num + 1, 3);
                }
                else
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " and Time > '" + now.ToString("HH:mm:ss") + "' order by time asc";
                    DataTable dataTable = dbOperHandler.GetDataTable();
                    if (dataTable.Rows.Count < 1)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " order by time asc";
                        dataTable = dbOperHandler.GetDataTable();
                        str = now.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                    }
                    else
                        str = now.ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                    if (loid == 4001)
                    {
                        DateTime dateTime = Convert.ToDateTime("2016-01-01 00:00:00");
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                        str = string.Concat((object)(530900 + Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) * 179 + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString())));
                    }
                    dataTable.Clear();
                    dataTable.Dispose();
                }
                return str;
            }
        }

        public string GetCurrentSn(int loid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                DateTime now = DateTime.Now;
                string str;
                if (loid == 3002 || loid == 3003)
                {
                    DateTime dateTime = Convert.ToDateTime(now.Year.ToString() + "-01-01 20:30:00");
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    int Num = Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7;
                    str = now.Year.ToString() + Func.AddZero(Num, 3);
                }
                else
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " and Time < '" + now.ToString("HH:mm:ss") + "' order by time desc";
                    DataTable dataTable = dbOperHandler.GetDataTable();
                    if (dataTable.Rows.Count < 1)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select top 1 Sn from Sys_LotteryTime where LotteryId=" + (object)loid + " order by time desc";
                        dataTable = dbOperHandler.GetDataTable();
                        str = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                    }
                    else
                    {
                        str = now.ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                        if (now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00") && now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 10:00:01") && loid == 1003)
                            str = now.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataTable.Rows[0]["Sn"].ToString();
                    }
                    if (loid == 4001)
                    {
                        DateTime dateTime = Convert.ToDateTime("2016-01-01 00:00:00");
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + now.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                        str = string.Concat((object)(530900 + (Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) - 7) * 179 + Convert.ToInt32(dataTable.Rows[0]["Sn"].ToString())));
                    }
                    dataTable.Clear();
                    dataTable.Dispose();
                }
                return str;
            }
        }

        public string GetListTime(int loid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                DateTime now = DateTime.Now;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 Time from Sys_LotteryTime where LotteryId=" + (object)loid + " and Time > '" + now.ToString("HH:mm:ss") + "' order by time asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                string str;
                if (dataTable.Rows.Count < 1)
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top 1 Time from Sys_LotteryTime where LotteryId=" + (object)loid + " order by time desc";
                    dataTable = dbOperHandler.GetDataTable();
                    str = now.AddDays(-1.0).ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["Time"].ToString();
                }
                else
                    str = now.ToString("yyyy-MM-dd") + " " + dataTable.Rows[0]["Time"].ToString();
                dataTable.Clear();
                dataTable.Dispose();
                return str;
            }
        }

        public DataTable GetLotteryAutoMode()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT top 1 [AutoLottery],[ProfitModel],[ProfitMargin] FROM [Sys_Info]";
                return dbOperHandler.GetDataTable();
            }
        }

        public string GetLotteryNumber(int type, string title)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT Number from Sys_LotteryData where Type=" + (object)type + " and title=" + title;
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                    return string.Concat(dataTable.Rows[0]["Number"]);
                return "0";
            }
        }

        /// <summary>
        /// 获取彩票系统配置
        /// </summary>
        /// <param name="code">彩票code</param>
        /// <returns>彩票详细</returns>
        public SysLotteryModel GetSysLotteryByCode(string code)
        {
            IList<SysLotteryModel> lotteries = GetSysLotteries();
            SysLotteryModel lottery = null;

            if (lotteries != null && lotteries.Count > 0)
            {
                lottery = (from lt in lotteries where lt.Code.Equals(code, StringComparison.OrdinalIgnoreCase) select lt).FirstOrDefault();
            }

            return lottery;
        }

        /// <summary>
        /// 获取彩票系统配置
        /// </summary>
        /// <returns>彩票详细</returns>
        public SysLotteryModel GetSysLotteryById(int id)
        {
            IList<SysLotteryModel> lotteries = GetSysLotteries();
            SysLotteryModel lottery = null;

            if (lotteries != null && lotteries.Count > 0)
            {
                lottery = (from lt in lotteries where lt.Id == id select lt).FirstOrDefault();
            }

            return lottery;
        }

        /// <summary>
        /// 获取彩票系统配置
        /// </summary>
        /// <returns>彩票详细</returns>
        public IList<SysLotteryModel> GetSysLotteries()
        {
            IList<SysLotteryModel> lotteries = (IList<SysLotteryModel>)RTCache.Get(Const.CACHE_KEY_SYS_LOTTERY);

            if (lotteries == null || lotteries.Count <= 0)
            {
                lotteries = new List<SysLotteryModel>();
                using (DbOperHandler db = new ComData().Doh())
                {
                    db.Reset();
                    db.SqlCmd = "select * from Sys_Lottery order by id";
                    DataTable dataTable = db.GetDataTable();

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            SysLotteryModel lot = new SysLotteryModel()
                            {
                                Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                                Title = Convert.ToString(dataTable.Rows[i]["Title"]),
                                Code = Convert.ToString(dataTable.Rows[i]["Code"]),
                                CloseTime = Convert.ToInt32(dataTable.Rows[i]["CloseTime"]),
                                IsOpen = Convert.ToBoolean(dataTable.Rows[i]["CloseTime"]),
                                ApiUrl = Convert.ToString(dataTable.Rows[i]["ApiUrl"])
                            };

                            lotteries.Add(lot);
                        }
                    }
                }

                RTCache.Insert(Const.CACHE_KEY_SYS_LOTTERY, lotteries);
            }

            return lotteries;
        }
    }
}