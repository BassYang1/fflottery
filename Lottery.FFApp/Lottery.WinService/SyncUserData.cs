
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
    /// 同步结果
    /// </summary>
    public class SyncUserResult
    {
        public String code { get; set; }
        public String msg { get; set; }
        public String data { get; set; }
    }

    /// <summary>
    /// 同步用户数据
    /// </summary>
    public static class SyncUserData
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SyncUserData));
        private static string userPointApi;
        private static bool isRunning = false;
        private static DateTime lastTime = new DateTime(1970, 1, 1);

        public static double SyncDataInterval { get; set; }


        static SyncUserData()
        {
            if (ConfigurationManager.AppSettings["UserPointApi"] != null)
            {
                userPointApi = ConfigurationManager.AppSettings["UserPointApi"].ToString();
            }
        }

        public static void DoSync(object arg = null)
        {
            if (isRunning)
            {
                return;
            }

            try
            {
                log.Debug("正在同步用户返点记录...");
                isRunning = true;
                DataTable userTable = null;

                #region 获取记录
                log.Debug("获取记录...");
                using (SqlConnection conn = new SqlConnection(SrvConst.ConnStr))
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"SELECT Id, UserName, Point 
                                FROM N_User WHERE ISNULL(IsDel, 0)=0 AND ISNULL(Point, 0) IN (0);";
                        conn.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataSet dataSet = new DataSet();
                            adapter.Fill(dataSet);

                            if (dataSet != null && dataSet.Tables.Count > 0)
                            {
                                userTable = dataSet.Tables[0];
                            }

                            adapter.Dispose();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }
                }

                log.Debug("获取用户结束...");
                if (userTable == null || userTable.Rows.Count <= 0)
                {
                    log.Debug("本次用户结束...");
                    isRunning = false;
                    return;
                }
                #endregion
                
                #region 检查记录
                log.Debug("检查记录...");
                foreach (DataRow row in userTable.Rows)
                {
                    RestClient client = new RestClient(SrvConst.ApiHost);
                    RestRequest req = new RestRequest(string.Format("{0}?name={1}", userPointApi, row["UserName"].ToString()), Method.GET);
                    req.AddHeader("Accept", "*/*");
                    req.AddHeader("Content-Type", "application/json; charset=utf-8");

                    IRestResponse<SyncUserResult> res = client.Execute<SyncUserResult>(req);
                    if (res != null && res.Data != null && SrvConst.SuccMsg.Equals(res.Data.msg, StringComparison.OrdinalIgnoreCase))
                    {
                        row["Point"] = res.Data.data;
                    }

                    if (row == userTable.Rows[userTable.Rows.Count - 1])
                    {
                        log.Debug("用户名: " + row["UserName"].ToString());
                        log.Debug("用户返点: " + res.Content);
                    }
                }
                log.Debug("同步用户返点结束...");
                #endregion

                int count = 0;
                int syncCount = 0;
                StringBuilder sql = new StringBuilder();

                #region 更新数据库
                log.Debug("更新数据库...");
                foreach (DataRow row in userTable.Rows)
                {
                    count++;
                    sql.Append(string.Format("update N_User set Point={0} WHERE id={1};", row["Point"].ToString(), row["id"].ToString()));

                    if (count % 100 != 0 && count != userTable.Rows.Count)
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

                    log.Debug("同步用户返点数: " + count);
                    log.Debug("同步用户返点SQL: " + sql.ToString());
                    sql.Clear();
                }
                log.Debug("更新数据库结束...");
                #endregion

                isRunning = false;
                log.Debug("本次同步用户返点结束...");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
