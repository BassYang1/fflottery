using System;
using System.Data;
using Lottery.DBUtility;
using Lottery.Utils;
using Lottery.FFCache;
using Lottery.Entity;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Newtonsoft.Json.Converters;

namespace Lottery.DAL
{
    public class LotteryDataDAL : ComData
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(LotteryDataDAL));

        public void GetListJSON(int lotteryId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = base.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListJSON(int lotteryId, ref string _jsonstr, ref string _xml)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 20 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = base.ConverTableToJSON(dataTable);
                _xml = base.ConverTableToLotteryXML(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetDnListJSON(int lotteryId, ref string _jsonstr)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Title");
            dataTable.Columns.Add("Number");
            dataTable.Columns.Add("Number1");
            dataTable.Columns.Add("Number2");
            dataTable.Columns.Add("Number3");
            dataTable.Columns.Add("Number4");
            dataTable.Columns.Add("Number5");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 18 * from Sys_LotteryData where Type=" + lotteryId + " order by Title desc";
                DataTable dataTable2 = dbOperHandler.GetDataTable();
                for (int i = 0; i < dataTable2.Rows.Count; i++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Title"] = dataTable2.Rows[i]["Title"].ToString();
                    dataRow["Number"] = dataTable2.Rows[i]["Number"].ToString() + "(" + CheckSSC_DN.CheckNNum(dataTable2.Rows[i]["Number"].ToString()) + ")";
                    dataRow["Number1"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 1) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 1)) + ")";
                    dataRow["Number2"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 2) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 2)) + ")";
                    dataRow["Number3"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 3) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 3)) + ")";
                    dataRow["Number4"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 4) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 4)) + ")";
                    dataRow["Number5"] = CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 5) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable2.Rows[i]["Number"].ToString(), 5)) + ")";
                    dataTable.Rows.Add(dataRow);
                }
                _jsonstr = base.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public bool Exists(int _type, string _title)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Title=@Title and Type=@Type";
                dbOperHandler.AddConditionParameter("@Title", _title);
                dbOperHandler.AddConditionParameter("@Type", _type);
                if (dbOperHandler.Exist("Sys_LotteryData"))
                {
                    num = 1;
                }
            }
            return num == 1;
        }

        public bool Exists(int _type, string _title, string _number)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Type=@Type and NumberAll=@NumberAll";
                dbOperHandler.AddConditionParameter("@Type", _type);
                dbOperHandler.AddConditionParameter("@NumberAll", _number);
                if (dbOperHandler.Exist("Sys_LotteryData"))
                {
                    num = 1;
                }
            }
            return num == 1;
        }

        public bool Add(int type, string title, string Number, string opentime, string NumberAll)
        {
            int num = LotterySum.SumNumber(Number);
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItem("Type", type);
                dbOperHandler.AddFieldItem("Title", title);
                dbOperHandler.AddFieldItem("Number", Number);
                dbOperHandler.AddFieldItem("NumberAll", NumberAll);
                dbOperHandler.AddFieldItem("Total", num);
                dbOperHandler.AddFieldItem("Opentime", Convert.ToDateTime(opentime));
                dbOperHandler.AddFieldItem("IsFill", "1");
                if (dbOperHandler.Insert("Sys_LotteryData") > 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <param name="lotteryId"></param>
        /// <param name="_jsonstr"></param>
        /// <param name="_xml"></param>
        public void ConvertLotteryDataToStr(int lotteryId, ref string _jsonstr, ref string _xml)
        {
            IList<LotteryDataModel> history = GetLotteryHistory(lotteryId) ?? new List<LotteryDataModel>();

            //转json
            IsoDateTimeConverter dtFormat = new IsoDateTimeConverter();
            dtFormat.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            _jsonstr = JsonConvert.SerializeObject(history, Formatting.Indented, dtFormat);

            StringBuilder xmlBuilder = new StringBuilder();
            xmlBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xmlBuilder.Append("<xml rows=\"5\" code=\"ssc\" remain=\"10hrs\">");

            foreach (LotteryDataModel lot in history)
            {
                xmlBuilder.Append("<row expect=\"" + lot.Title + "\" opencode=\"" + lot.NumberAll + "\" opentime=\"" + lot.OpenTime.ToString("yyyy/MM/dd HH:mm:ss") + "\" />");
            }

            xmlBuilder.Append("</xml>");
            _xml = xmlBuilder.ToString();

        }

        /// <summary>
        /// 更新彩票开奖信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool Update(int type, string title)
        {
            string cacheKey = string.Format(Const.CACHE_KEY_LOTTERY_HISTORY, type);
            IList<LotteryDataModel> history = GetLotteryHistory(type);

            //获取缓存数据
            LotteryDataModel lottery = (from it in history where it.Type == type && it.Title == title select it).FirstOrDefault();
            if (lottery == null)
            {
                return false;
            }

            string number = lottery.Number;
            string numberAll = lottery.NumberAll;
            int num = lottery.Total;
            string openTime = lottery.OpenTime.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                using (SqlConnection conn = new SqlConnection(ComData.connectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        conn.Open();
                        com.Connection = conn;
                        com.CommandText = string.Format(@"INSERT INTO Sys_LotteryData(Type, Title, Number, NumberAll, Total, OpenTime, IsFill)
                                                    SELECT {0}, '{1}', '{2}', '{3}', {4}, '{5}', 1 
                                                    WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryData WHERE Type={0} and Title='{1}'); 
                                                    select @@identity as id; ", type, title, number, numberAll, num, openTime);

                        object id = com.ExecuteScalar(); //插入数据

                        //插入成功
                        if (!Convert.IsDBNull(id))
                        {
                            lottery.Id = Convert.ToInt32(id);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("开奖入库异常 {0}", ex);
            }

            return false;
        }

        /// <summary>
        /// 更新彩票开奖信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="number"></param>
        /// <param name="opentime"></param>
        /// <param name="numberAll"></param>
        /// <returns></returns>
        public bool Update(int type, string title, string number, string opentime, string numberAll)
        {
            string cacheKey = string.Format(Const.CACHE_KEY_LOTTERY_HISTORY, type);
            IList<LotteryDataModel> history = GetLotteryHistory(type);

            //数据已存在
            if ((from it in history where it.Title == title select it).Count() > 0)
            {
                return false;
            }

            try
            {
                int num = LotterySum.SumNumber(number);

                using (SqlConnection conn = new SqlConnection(ComData.connectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {

                        conn.Open();
                        com.Connection = conn;
                        com.CommandText = string.Format(@"INSERT INTO Sys_LotteryData(Type, Title, Number, NumberAll, Total, OpenTime, IsFill)
                                                    SELECT {0}, '{1}', '{2}', '{3}', {4}, '{5}', 1 
                                                    WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryData WHERE Type={0} and Title='{1}'); 
                                                    select @@identity as id; ", type, title, number, numberAll, num, opentime);

                        object id = com.ExecuteScalar(); //插入数据

                        //插入成功
                        if (!Convert.IsDBNull(id))
                        {
                            //更新缓存
                            if (history.Count > 0)
                            {
                                history.RemoveAt(history.Count - 1); //移除最后一条
                            }

                            history.Insert(0, new LotteryDataModel()
                            {
                                Id = Convert.ToInt32(id),
                                Type = type,
                                Title = title,
                                Number = number,
                                NumberAll = numberAll,
                                Total = num,
                                Dx = 0,
                                Ds = 0,
                                Flag = 0,
                                Flag2 = 0,
                                IsFill = true,
                                OpenTime = Convert.ToDateTime(opentime)
                            });

                            RTCache.Insert(cacheKey, history);

                            return true;
                        }
                        else
                        {
                            Log.ErrorFormat("彩票已开奖, 彩种: {0}, 期号: {1}", type, title);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("开奖入库异常 {0}", ex);
            }

            return false;
        }

        /// <summary>
        /// 获取最新一期开奖信息
        /// </summary>
        /// <param name="ltId"></param>
        /// <returns></returns>
        public LotteryDataModel GetLatestLottery(int ltId)
        {
            IList<LotteryDataModel> lotteries = GetLotteryHistory(ltId);

            if (lotteries != null && lotteries.Count > 0)
            {
                return lotteries.First(); 
            }

            return null;
        }

        public IList<LotteryDataModel> GetLotteryHistory(int ltId)
        {
            string cacheKey = string.Format(Const.CACHE_KEY_LOTTERY_HISTORY, ltId);
            IList<LotteryDataModel> history = (IList<LotteryDataModel>)RTCache.Get(cacheKey);

            if (history == null || history.Count < 0)
            {
                history = new List<LotteryDataModel>();

                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select top 20 * from Sys_LotteryData where Type=" + ltId + " order by Title desc";
                    DataTable dataTable = dbOperHandler.GetDataTable();

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            LotteryDataModel lot = new LotteryDataModel()
                            {
                                Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                                Type = Convert.ToInt32(dataTable.Rows[i]["Type"]),
                                Title = Convert.ToString(dataTable.Rows[i]["Title"]),
                                Number = Convert.ToString(dataTable.Rows[i]["Number"]),
                                NumberAll = Convert.ToString(dataTable.Rows[i]["NumberAll"]),
                                Total = Convert.ToInt32(dataTable.Rows[i]["Total"]),
                                Dx = Convert.ToInt32(dataTable.Rows[i]["Dx"]),
                                Ds = Convert.ToInt32(dataTable.Rows[i]["Ds"]),
                                OpenTime = Convert.ToDateTime(dataTable.Rows[i]["OpenTime"]),
                                STime = Convert.ToDateTime(dataTable.Rows[i]["STime"]),
                                Flag = Convert.ToInt32(dataTable.Rows[i]["Flag"]),
                                Flag2 = Convert.ToInt32(dataTable.Rows[i]["Flag2"]),
                                IsFill = Convert.ToBoolean(dataTable.Rows[i]["IsFill"])
                            };

                            history.Add(lot);
                        }
                    }

                    //排序
                    if (history.Count > 0)
                    {
                        history = (from it in history orderby it.Title descending select it).ToList();
                    }

                    dataTable.Clear();
                    dataTable.Dispose();
                }

                RTCache.Insert(cacheKey, history);
            }

            return history;
        }

        public bool AddYoule(int type, string title, string Number, string opentime, string NumberAll = "")
        {
            int num = LotterySum.SumNumber(Number);
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                NumberAll = (string.IsNullOrEmpty(NumberAll) ? Number : NumberAll);
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItem("Type", type);
                dbOperHandler.AddFieldItem("Title", title);
                dbOperHandler.AddFieldItem("Number", Number);
                dbOperHandler.AddFieldItem("NumberAll", NumberAll);
                dbOperHandler.AddFieldItem("Total", num);
                dbOperHandler.AddFieldItem("Opentime", opentime);
                dbOperHandler.AddFieldItem("IsFill", "1");
                if (dbOperHandler.Insert("Sys_LotteryData") > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateBetNumber(int type, string title)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Id=" + title;
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[0]["Type"].ToString()), dataTable.Rows[0]["Title"].ToString(), dataTable.Rows[0]["Number"].ToString());
                    return true;
                }
            }
            return false;
        }

        public bool UpdateAllBetNumber(int type)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 Type,Title,Number from Sys_LotteryData where Type=" + type + " order by Title desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        LotteryCheck.AdminRunOper(Convert.ToInt32(dataTable.Rows[i]["Type"].ToString()), dataTable.Rows[i]["Title"].ToString(), dataTable.Rows[i]["Number"].ToString());
                    }
                }
            }
            return true;
        }

        public DataTable GetListDataTable(int lotteryId, int top)
        {
            DataTable result;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Concat(new object[]
				{
					"select * from (select top ",
					top,
					" * from Sys_LotteryData where Type=",
					lotteryId,
					" order by Title Desc) A order by Title asc"
				});
                DataTable dataTable = dbOperHandler.GetDataTable();
                result = dataTable;
            }
            return result;
        }

        public string GetHisNumber(string ltype)
        {
            string text = "";
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 1)
                {
                    Random random = new Random();
                    string text2 = dataTable.Rows[random.Next(0, dataTable.Rows.Count - 1)]["Number"].ToString();
                    int num = random.Next(0, 4);
                    string[] array = text2.Split(new char[]
					{
						','
					});
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i == num)
                        {
                            text = text + random.Next(0, 9) + ",";
                        }
                        else
                        {
                            text = text + array[i] + ",";
                        }
                    }
                    text = text.Substring(0, text.Length - 1);
                }
                else
                {
                    text = NumberCode.CreateCode(5);
                }
            }
            return text;
        }

        public DataTable GetHisNumber2(string ltype)
        {
            DataTable result;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT TOP 1000 [Number]\r\n                                FROM [Sys_LotteryData] where '" + ltype + "'=substring(Convert(varchar(10),type),1,1) order by STime asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                result = dataTable;
            }
            return result;
        }
    }
}
