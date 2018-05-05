using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.WinService
{
    /// <summary>
    /// 投注数据
    /// </summary>
    public class BetPostData
    {
        public String tradeNo { get; set; }
        public String reAmount { get; set; }
        public String betAmount { get; set; }
        public String userName { get; set; }
        public long betTime { get; set; }
    }

    /// <summary>
    /// 同步结果
    /// </summary>
    public class SyncResult
    {
        public String code { get; set; }
        public String msg { get; set; }
        public String data { get; set; }
    }

    /// <summary>
    /// 同步投注数据
    /// </summary>
    public static class SyncBetData
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SyncBetData));
        private static string apiHost;
        private static string syncDataApi;
        private static string checkStateApi;
        private static DateTime Dt1970 = new DateTime(1970, 1, 1);
        private static string SuccMsg = "succ";
        private static string SuccState = "200";
        private static object lockSync = new object();
        private static bool isRunning = false;
        private static DateTime lastTime = new DateTime(1970, 1, 1);


        static SyncBetData()
        {
            if (ConfigurationManager.AppSettings["ApiHost"] != null)
            {
                apiHost = ConfigurationManager.AppSettings["ApiHost"].ToString();
            }

            if (ConfigurationManager.AppSettings["SyncDataApi"] != null)
            {
                syncDataApi = ConfigurationManager.AppSettings["SyncDataApi"].ToString();
            }

            if (ConfigurationManager.AppSettings["CheckStateApi"] != null)
            {
                checkStateApi = ConfigurationManager.AppSettings["CheckStateApi"].ToString();
            }
        }

        public static void DoSync()
        {
            if (isRunning)
            {
                return;
            }

            try
            {
                lock (lockSync)
                {
                    log.Debug("正在同步投注记录...");
                    isRunning = true;
                    DataTable betTable = null;

                    using (SqlConnection conn = new SqlConnection(SrvConst.ConnStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            //0-未开奖，1-已撤销, 2-未中奖, 3-已中奖
                            cmd.CommandText = @"SELECT TOP 1 B.Id, B.Ssid, U.UserName ,B.Total, B.RealGet, B.STime, ISNULL(B.IsSync, 0) AS IsSync 
                                FROM N_UserBet B INNER JOIN N_User U ON B.UserId=U.Id WHERE ISNULL(B.IsSync, 0) != 2 AND ISNULL(B.State, 0) IN (2, 3);";
                            conn.Open();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataSet dataSet = new DataSet();
                                adapter.Fill(dataSet);

                                if (dataSet != null && dataSet.Tables.Count > 0)
                                {
                                    betTable = dataSet.Tables[0];
                                }

                                adapter.Dispose();
                                cmd.Dispose();
                                conn.Close();
                            }
                        }
                    }

                    if (betTable == null || betTable.Rows.Count <= 0)
                    {
                        return;
                    }

                    foreach (DataRow row in betTable.Rows)
                    {
                        BetPostData data = new BetPostData()
                        {
                            tradeNo = row["Ssid"].ToString(),
                            reAmount = row["RealGet"] == null ? "0" : row["RealGet"].ToString(),
                            betAmount = row["Total"] == null ? "0" : row["Total"].ToString(),
                            userName = row["UserName"].ToString(),
                            betTime = (Convert.ToDateTime(row["STime"]).Ticks - Dt1970.Ticks) / 10000000
                        };

                        RestClient client = new RestClient(apiHost);
                        RestRequest req = new RestRequest(syncDataApi, Method.POST);
                        req.AddParameter("application/json; charset=utf-8", req.JsonSerializer.Serialize(data), ParameterType.RequestBody);
                        req.AddHeader("Accept", "*/*");
                        req.AddHeader("Content-Type", "application/json; charset=utf-8");

                        IRestResponse<SyncResult> res = client.Execute<SyncResult>(req);

                        if (res != null && res.Data != null && res.Data.code == SuccState
                            && SuccMsg.Equals(res.Data.msg, StringComparison.OrdinalIgnoreCase))
                        {
                            //1 成功 2 该号已存在 3 用户名不存在
                            //0 or NULL = 未同步; 1 = 处理中; 2 = 完成同步; 3 = 数据异常
                            row["IsSync"] = res.Data.data == "3" ? 3 : 2;
                        }

                        if (row == betTable.Rows[betTable.Rows.Count - 1])
                        {
                            log.Debug("同步请求: " + req.ToString());
                            log.Debug("同步数据: " + req.JsonSerializer.Serialize(data));
                            log.Debug("同步结果: " + res.Content);
                        }
                    }

                    foreach (DataRow row in betTable.Rows)
                    {

                        if (row["IsSync"].ToString() != "1")
                        {
                            continue;
                        }

                        RestClient client = new RestClient(apiHost);
                        RestRequest req = new RestRequest(string.Format("{0}?tradeNo={1}", checkStateApi, row["Ssid"].ToString()), Method.GET);
                        req.AddHeader("Accept", "*/*");
                        req.AddHeader("Content-Type", "application/json; charset=utf-8");

                        IRestResponse<SyncResult> res = client.Execute<SyncResult>(req);
                        if (res != null && res.Content != null && res.Content.Length > 0 && "true".Equals(res.Content, StringComparison.OrdinalIgnoreCase))
                        {
                            //0 or NULL = 未同步; 1 = 处理中; 2 = 完成同步； 3 ＝ 数据异常
                            row["IsSync"] = 2;
                        }

                        if (row == betTable.Rows[betTable.Rows.Count - 1])
                        {
                            log.Debug("检查请求: " + req.ToString());
                            log.Debug("检查数据: " + row["Ssid"].ToString());
                            log.Debug("检查结果: " + res.Content);
                        }
                    }

                    int count = 0;
                    int syncCount = 0;
                    StringBuilder sql = new StringBuilder();

                    foreach (DataRow row in betTable.Rows)
                    {
                        if (row["IsSync"].ToString() != "2" && row["IsSync"].ToString() != "3")
                        {
                            continue;
                        }

                        count++;
                        sql.Append(string.Format("update N_UserBet set IsSync={0} WHERE id={1};", row["IsSync"].ToString(), row["id"].ToString()));

                        if (count % 100 != 0 && count != betTable.Rows.Count)
                        {
                            continue;
                        }

                        using (SqlConnection conn = new SqlConnection(SrvConst.ConnStr))
                        {
                            using (SqlCommand cmd = conn.CreateCommand())
                            {
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = sql.ToString();
                                conn.Open();
                                syncCount += cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                conn.Close();
                            }
                        }

                        log.Debug("同步彩票投注记录数: " + count);
                        log.Debug("同步彩票投注记录SQL: " + sql.ToString());
                        sql.Clear();
                    }


                    isRunning = false;
                    log.Debug("完成同步投注记录...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
