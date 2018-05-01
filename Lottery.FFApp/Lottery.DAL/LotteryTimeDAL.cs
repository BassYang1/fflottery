// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.LotteryTimeDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;

namespace Lottery.DAL
{
    public class LotteryTimeDAL : ComData
    {
        public DataTable GetTable()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = @"select [Id],[LotteryId],[Sn],[Time],[Sort] from Sys_LotteryTime order by Id desc";
                DataTable table = dbOperHandler.GetDataTable();

                //dbOperHandler.Reset();
                //dbOperHandler.SqlCmd = @"select [Id],[LotteryId],[Sn],[Time],[Sort] from Sys_LotteryDateTime order by Id desc";
                //table.Merge(dbOperHandler.GetDataTable());

                return table;
            }
        }

        public DataTable GetDateTimeTable()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select [Id],[LotteryId],[Sn],[Time],[Sort] from Sys_LotteryDateTime order by Id desc";
                return dbOperHandler.GetDataTable();
            }
        }

        public int GetTsIssueNum(string lotteryId)
        {
            DateTime now = DateTime.Now;
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                switch (lotteryId)
                {
                    case "1010":
                    case "3004":
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
                        num = 1658122 + Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) * 880;
                        break;
                    case "1017":
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
                        num = 1658117 + Convert.ToInt32(dbOperHandler.GetDataTable().Rows[0]["d"]) * 880;
                        break;
                    case "1012":
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-23',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable1 = dbOperHandler.GetDataTable();
                        num = !(now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00")) || !(now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:01:45")) ? 2588087 + Convert.ToInt32(dataTable1.Rows[0]["d"]) * 660 : 2588087 + (Convert.ToInt32(dataTable1.Rows[0]["d"]) - 1) * 660;
                        break;
                    case "1013":
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable2 = dbOperHandler.GetDataTable();
                        num = !(now > Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00")) || !(now < Convert.ToDateTime(now.ToString("yyyy-MM-dd") + " 06:50:00")) ? 106000000 + Convert.ToInt32(dataTable2.Rows[0]["d"]) * 203 : 106000000 + (Convert.ToInt32(dataTable2.Rows[0]["d"]) - 1) * 203;
                        break;
                    case "4001":
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2017-02-10',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable3 = dbOperHandler.GetDataTable();
                        num = !(DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00")) || 
                            !(DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01")) ? 
                            599795 + Convert.ToInt32(dataTable3.Rows[0]["d"]) * 179 :
                            599795 + (Convert.ToInt32(dataTable3.Rows[0]["d"]) - 1) * 179;
                        break;
                }
            }
            return num;
        }

        public int GetTsIssueNumToPet(int lotteryId, int curIssueNum)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                switch (lotteryId)
                {
                    case 1010:
                    case 3004:
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable1 = dbOperHandler.GetDataTable();
                        num = curIssueNum - 1658122 - Convert.ToInt32(dataTable1.Rows[0]["d"]) * 880;
                        break;
                    case 1012:
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-23',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable2 = dbOperHandler.GetDataTable();
                        num = curIssueNum - 2588087 - Convert.ToInt32(dataTable2.Rows[0]["d"]) * 660;
                        break;
                    case 1013:
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable3 = dbOperHandler.GetDataTable();
                        num = curIssueNum - 106000000 - Convert.ToInt32(dataTable3.Rows[0]["d"]) * 203;
                        break;
                    case 1017:
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2016-10-08',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable4 = dbOperHandler.GetDataTable();
                        num = curIssueNum - 1658117 - Convert.ToInt32(dataTable4.Rows[0]["d"]) * 880;
                        break;
                    case 4001:
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = "select datediff(d,'2017-01-01',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable5 = dbOperHandler.GetDataTable();
                        num = curIssueNum - 106000000 - Convert.ToInt32(dataTable5.Rows[0]["d"]) * 203;
                        dbOperHandler.Reset();

                        dbOperHandler.SqlCmd = "select datediff(d,'2017-02-10',Convert(varchar(10),getdate(),120)) as d";
                        DataTable dataTable6 = dbOperHandler.GetDataTable();
                        num = DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00") && 
                            DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 09:07:01") || 
                            DateTime.Now > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:57:01") && 
                            DateTime.Now < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") ? 
                            curIssueNum - 601048 - (Convert.ToInt32(dataTable6.Rows[0]["d"]) - 1) * 179 : 
                            curIssueNum - 601048 - Convert.ToInt32(dataTable6.Rows[0]["d"]) * 179;
                        break;
                }
            }
            return num;
        }
    }
}