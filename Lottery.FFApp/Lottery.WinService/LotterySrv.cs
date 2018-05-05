﻿using log4net;
using Lottery.Collect;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Lottery.WinService
{
    public partial class LotterySrv : ServiceBase
    {
        //private System.Timers.Timer gzTimer;
        //private System.Timers.Timer fh2610Timer;
        //private System.Timers.Timer fh1125Timer;
        private System.Timers.Timer syncBetTimer;
        private static string hourStr = "3"; //执行时间, 默认凌晨3点
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(LotterySrv));
        //private DateTime? lastGzDate; //上一次发放工资的日期
        //private DateTime? lastFH2610Date; //上一次发放1到15分红的日期
        //private DateTime? lastFH1125Date; //结算当月11号到25号的分红
        //private int hour = 3;

        static LotterySrv()
        {
            if (ConfigurationManager.AppSettings["DoHour"] != null)
            {
                hourStr = ConfigurationManager.AppSettings["DoHour"].ToString();
            }
        }

        public LotterySrv()
        {
            InitializeComponent();

            //if (Int32.TryParse(hourStr, out hour) == false)
            //{
            //    hour = 3;
            //}

            ////定时发放工资(每天凌晨3点派发前一天契约工资)
            //gzTimer = new System.Timers.Timer(30 * 60 * 1000);
            //gzTimer.Elapsed += new ElapsedEventHandler(GZ_Elapsed);
            //gzTimer.AutoReset = true;
            //log.Info("开始定时发放工资(每天凌晨3点派发前一天契约工资)...");

            ////定时发放分红(结算上月26号到当月10号的分红)
            //fh2610Timer = new System.Timers.Timer(40 * 60 * 1000);
            //fh2610Timer.Elapsed += new ElapsedEventHandler(FH2610_Elapsed);
            //fh2610Timer.AutoReset = true;
            //log.Info("开始定时发放工资(结算上月26号到当月10号的分红)...");

            ////定时发放分红(结算当月11号到25号的分红)
            //fh1125Timer = new System.Timers.Timer(40 * 60 * 1000);
            //fh1125Timer.Elapsed += new ElapsedEventHandler(FH1125_Elapsed);
            //fh1125Timer.AutoReset = true;
            //log.Info("开始定时发放工资(结算当月11号到25号的分红)...");

            //同步彩票投注记录
            syncBetTimer = new System.Timers.Timer(5 * 60 * 1000);
            syncBetTimer.Elapsed += new ElapsedEventHandler(SyncBet_Elapsed);
            syncBetTimer.AutoReset = true;
        }

        protected override void OnStart(string[] args)
        {
            //log.Info("启动Lottery平台工资和分红派发平台...");
            //gzTimer.Enabled = true;
            //fh2610Timer.Enabled = true;
            //fh1125Timer.Enabled = true;

            syncBetTimer.Enabled = true;
            TimeData.Run();
        }

        protected override void OnStop()
        {
            //log.Info("停止Lottery平台工资和分红派发平台...");
            //gzTimer.Enabled = false;
            //fh2610Timer.Enabled = false;
            //fh1125Timer.Enabled = false;

            syncBetTimer.Enabled = false;
            TimeData.Stop();
        }

        protected override void OnShutdown()
        {
            log.Info("服务器计算机关闭");
            //gzTimer.Enabled = false;
            //fh2610Timer.Enabled = false;
            //fh1125Timer.Enabled = false;

            syncBetTimer.Enabled = false;
            TimeData.Stop();
        }

        ///// <summary>
        ///// 每天凌晨3点派发前一天契约工资
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //private void GZ_Elapsed(object source, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        log.Debug("发放工资...");

        //        //每天
        //        //凌晨3 && 当天没有发放过工资
        //        if (DateTime.Now.Hour == hour && (!lastGzDate.HasValue || lastGzDate.Value.Date != DateTime.Now.Date))
        //        {
        //            lastGzDate = DateTime.Now;
        //            log.Info("开始发放工资...");

        //            using (SqlConnection conn = new SqlConnection(connStr))
        //            {
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.CommandText = "GZBatchByDate";
        //                    SqlParameter parm = new SqlParameter("@gzdate", SqlDbType.DateTime);
        //                    parm.Value = DateTime.Now.AddDays(-1); //发放该日期的工资
        //                    cmd.Parameters.Add(parm);
        //                    conn.Open();
        //                    cmd.ExecuteNonQuery();
        //                    cmd.Dispose();
        //                    conn.Close();
        //                }
        //            }

        //            log.Info("结束发放工资...");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //}

        ///// <summary>
        ///// 结算上月26号到当月10号的分红
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //private void FH2610_Elapsed(object source, ElapsedEventArgs e)
        //{
        //    log.Debug("发放上月26号到当月10号分红...");

        //    try
        //    {
        //        //每天
        //        //当月11号 && 凌晨3 && 当天没有发放过分红
        //        if (DateTime.Now.Day == 11 && DateTime.Now.Hour == hour && (!lastFH2610Date.HasValue || lastFH2610Date.Value.Date != DateTime.Now.Date))
        //        {
        //            lastFH2610Date = DateTime.Now;
        //            log.Info("结算上月26号到当月10号的分红...");

        //            using (SqlConnection conn = new SqlConnection(connStr))
        //            {
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.CommandText = "FH2610BatchByDate";
        //                    SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
        //                    parm.Value = DateTime.Now.AddDays(-1); //结算上月26号到当月10号的分红
        //                    cmd.Parameters.Add(parm);
        //                    conn.Open();
        //                    cmd.ExecuteNonQuery();
        //                    cmd.Dispose();
        //                    conn.Close();
        //                }
        //            }

        //            log.Info("结束结算上月26号到当月10号的分红...");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //}

        ///// <summary>
        ///// 结算当月11号到25号的分红
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="e"></param>
        //private void FH1125_Elapsed(object source, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        log.Debug("发放当月11号到25号分红...");

        //        //每天
        //        //当月26号 && 凌晨3 && 当天没有发放过分红
        //        if (DateTime.Now.Day == 26 && DateTime.Now.Hour == hour && (!lastFH1125Date.HasValue || lastFH1125Date.Value.Date != DateTime.Now.Date))
        //        {
        //            lastFH1125Date = DateTime.Now;
        //            log.Info("开始结算当月11号到25号的分红...");

        //            using (SqlConnection conn = new SqlConnection(connStr))
        //            {
        //                using (SqlCommand cmd = conn.CreateCommand())
        //                {
        //                    cmd.CommandType = CommandType.StoredProcedure;
        //                    cmd.CommandText = "FH1125BatchByDate";
        //                    SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
        //                    parm.Value = DateTime.Now.AddDays(-1); //结算当月11号到25号的分红
        //                    cmd.Parameters.Add(parm);
        //                    conn.Open();
        //                    cmd.ExecuteNonQuery();
        //                    cmd.Dispose();
        //                    conn.Close();
        //                }
        //            }

        //            log.Info("结束结算当月11号到25号的分红...");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);
        //    }
        //}

        /// <summary>
        /// 同步彩票投注记录
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void SyncBet_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                log.Info("同步彩票投注记录...");
                SyncBetData.DoSync();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }

}
