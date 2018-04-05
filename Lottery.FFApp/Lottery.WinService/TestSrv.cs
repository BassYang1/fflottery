using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Timers;

namespace Lottery.WinService
{
    public class TestSrv
    {
        private System.Timers.Timer gzTimer;
        private System.Timers.Timer fh2610Timer;
        private System.Timers.Timer fh1125Timer;
        private static string connStr = string.Empty;
        private static string hourStr = "3"; //执行时间, 默认凌晨3点
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(TestSrv));
        private DateTime? lastGzDate; //上一次发放工资的日期
        private DateTime? lastFH2610Date; //上一次发放1到15分红的日期
        private DateTime? lastFH1125Date; //结算当月11号到25号的分红
        private int hour = 3;

        static TestSrv()
        {
            if (ConfigurationManager.ConnectionStrings["ConnStr"] != null)
            {
                connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }

            if (ConfigurationManager.AppSettings["DoHour"] != null)
            {
                hourStr = ConfigurationManager.AppSettings["DoHour"].ToString();
            }
        }

        public TestSrv()
        {
            if (Int32.TryParse(hourStr, out hour) == false)
            {
                hour = 3;
            }

            //定时发放工资(每天凌晨3点派发前一天契约工资)
            gzTimer = new System.Timers.Timer(3 * 1000);
            gzTimer.Elapsed += new ElapsedEventHandler(GZ_Elapsed);
            gzTimer.AutoReset = true;
            log.Info("开始定时发放工资(每天凌晨3点派发前一天契约工资)...");

            //定时发放分红(结算上月26号到当月10号的分红)
            fh2610Timer = new System.Timers.Timer(3 * 1000);
            fh2610Timer.Elapsed += new ElapsedEventHandler(FH2610_Elapsed);
            fh2610Timer.AutoReset = true;
            log.Info("开始定时发放工资(结算上月26号到当月10号的分红)...");

            //定时发放分红(结算当月11号到25号的分红)
            fh1125Timer = new System.Timers.Timer(40 * 60 * 1000);
            fh1125Timer.Elapsed += new ElapsedEventHandler(FH1125_Elapsed);
            fh1125Timer.AutoReset = true;
            log.Info("开始定时发放工资(结算当月11号到25号的分红)...");
        }

        public void TestStart()
        {
            gzTimer.Enabled = true;
            fh2610Timer.Enabled = true;
            fh1125Timer.Enabled = true;
        }

        /// <summary>
        /// 每天凌晨3点派发前一天契约工资
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GZ_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //凌晨3 && 当天没有发放过工资
                if (DateTime.Now.Hour == hour && (!lastGzDate.HasValue || lastGzDate.Value.Date != DateTime.Now.Date))
                {
                    lastGzDate = DateTime.Now;
                    log.Info("开始发放工资...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "GZBatchByDate";
                            SqlParameter parm = new SqlParameter("@gzdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //发放该日期的工资
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束发放工资...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
                
        /// <summary>
        /// 结算上月26号到当月10号的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH2610_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //当月11号 && 凌晨3 && 当天没有发放过分红
                if (DateTime.Now.Day == 11 && DateTime.Now.Hour == hour && (!lastFH2610Date.HasValue || lastFH2610Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH2610Date = DateTime.Now;
                    log.Info("结算上月26号到当月10号的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH2610BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //结算上月26号到当月10号的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束结算上月26号到当月10号的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 结算当月11号到25号的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH1125_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //当月26号 && 凌晨3 && 当天没有发放过分红
                if (DateTime.Now.Day == 26 && DateTime.Now.Hour == hour && (!lastFH1125Date.HasValue || lastFH1125Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH1125Date = DateTime.Now;
                    log.Info("开始结算当月11号到25号的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH1125BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //结算当月11号到25号的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束结算当月11号到25号的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
