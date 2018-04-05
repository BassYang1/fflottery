using log4net;
using Lottery.Collect.Sys;
using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Lottery.Collect
{
    public class TimeData
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(TimeData));

        public static void Run()
        {
            //腾迅分分彩
            TimeData.timerQQ60.Elapsed += new ElapsedEventHandler(TimeData.timerQQ60_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThQQ60_Fun));

            //官方时时彩
            TimeData.timerOfficLot.Elapsed += new ElapsedEventHandler(TimeData.timerOfficLot_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThOfficLot_Fun));

            //福彩3D
            TimeData.timer3d.Elapsed += new ElapsedEventHandler(TimeData.timer3d_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.Th3d_Fun));

            //体彩P3
            TimeData.timerP3.Elapsed += new ElapsedEventHandler(TimeData.timerP3_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThP3_Fun));

            //系统彩
            //TimeData.timerSystemLot.Elapsed += new ElapsedEventHandler(TimeData.timerSystemLot_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSystemLot_Fun));

            //新德里1.5分彩
            TimeData.timerXdl90.Elapsed += new ElapsedEventHandler(TimeData.timerXdl90_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXdl90_Fun));

            //菲律宾1.5分
            TimeData.timerFlb90m.Elapsed += new ElapsedEventHandler(TimeData.timerFlb90m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThFlb90m_Fun));

            //韩国1.5分彩
            TimeData.timerHg90m.Elapsed += new ElapsedEventHandler(TimeData.timerHg90m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg90m_Fun));

            //东京1.5分彩
            TimeData.timerDj15.Elapsed += new ElapsedEventHandler(TimeData.timerDj15_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThDj15_Fun));

            //新加坡2分彩
            TimeData.timerXjp2fc.Elapsed += new ElapsedEventHandler(TimeData.timerXjp2fc_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXjp2fc_Fun));

            //台湾5分彩
            TimeData.timerTw5fc.Elapsed += new ElapsedEventHandler(TimeData.timerTw5fc_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThTw5fc_Fun));

            //新加坡30秒
            TimeData.timerXjp30m.Elapsed += new ElapsedEventHandler(TimeData.timerXjp30m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXjp30m_Fun));

            //纽约30秒彩
            TimeData.timerNy30m.Elapsed += new ElapsedEventHandler(TimeData.timerNy30m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThNy30m_Fun));

            //台湾45秒彩
            TimeData.timerTw45m.Elapsed += new ElapsedEventHandler(TimeData.timerTw45m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThTw45m_Fun));

            //首尔60秒
            TimeData.timerSe60m.Elapsed += new ElapsedEventHandler(TimeData.timerSe60m_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSe60m_Fun));

            //纽约30秒11选5
            TimeData.timerYf11x5.Elapsed += new ElapsedEventHandler(TimeData.timerYf11x5_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYf11x5_Fun));

            //韩国1.5分11选5
            TimeData.timerHg11x5.Elapsed += new ElapsedEventHandler(TimeData.timerHg11x5_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg11x5_Fun));

            //纽约30秒3D
            TimeData.timerSe60sd.Elapsed += new ElapsedEventHandler(TimeData.timerSe60sd_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSe60sd_Fun));

            //韩国1.5分3D
            TimeData.timerHg90sd.Elapsed += new ElapsedEventHandler(TimeData.timerHg90sd_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg90sd_Fun));

            //北京PK10 改为官方彩
            //TimeData.timerBjpk10.Elapsed += new ElapsedEventHandler(TimeData.timerBjpk10_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThBjpk10_Fun));

            //英国30秒赛车
            TimeData.timerYfpk10.Elapsed += new ElapsedEventHandler(TimeData.timerYfpk10_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYfpk10_Fun));

            //英国60秒赛车
            TimeData.timerYg60sc.Elapsed += new ElapsedEventHandler(TimeData.timerYg60sc_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg60sc_Fun));

            //英国120秒赛车
            TimeData.timerYg120sc.Elapsed += new ElapsedEventHandler(TimeData.timerYg120sc_Elapsed);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg120sc_Fun));

            //TimeData.timerHg90.Elapsed += new ElapsedEventHandler(TimeData.timerHg90_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg90_Fun));
            //TimeData.timerXjp120.Elapsed += new ElapsedEventHandler(TimeData.timerXjp120_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThXjp120_Fun));
            //TimeData.timerDj90.Elapsed += new ElapsedEventHandler(TimeData.timerDj90_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThDj90_Fun));

            //TimeData.timerOne11x5.Elapsed += new ElapsedEventHandler(TimeData.timerOne11x5_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThOne11x5_Fun));
            //TimeData.timerHg3d.Elapsed += new ElapsedEventHandler(TimeData.timerHg3d_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThHg3d_Fun));
            //TimeData.timerSe3d.Elapsed += new ElapsedEventHandler(TimeData.timerSe3d_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThSe3d_Fun));
            //TimeData.timerOnePk10.Elapsed += new ElapsedEventHandler(TimeData.timerOnePk10_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThOnePk10_Fun));
            //TimeData.timerYg60Pk10.Elapsed += new ElapsedEventHandler(TimeData.timerYg60Pk10_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg60Pk10_Fun));
            //TimeData.timerYg120Pk10.Elapsed += new ElapsedEventHandler(TimeData.timerYg120Pk10_Elapsed);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(TimeData.ThYg120Pk10_Fun));
        }
        
        public static void Stop()
        {
            //腾迅分分彩
            TimeData.timerQQ60.Enabled = false;

            //官方时时彩
            TimeData.timerOfficLot.Enabled = false;

            //福彩3D
            TimeData.timer3d.Enabled = false;

            //体彩P3
            TimeData.timerP3.Enabled = false;

            //系统彩
            //TimeData.timerSystemLot.Enabled = false;

            //新德里1.5分彩
            TimeData.timerXdl90.Enabled = false;

            //菲律宾1.5分
            TimeData.timerFlb90m.Enabled = false;

            //韩国1.5分彩
            TimeData.timerHg90m.Enabled = false;

            //东京1.5分彩
            TimeData.timerDj15.Enabled = false;

            //新加坡2分彩
            TimeData.timerXjp2fc.Enabled = false;

            //台湾5分彩
            TimeData.timerTw5fc.Enabled = false;

            //新加坡30秒
            TimeData.timerXjp30m.Enabled = false;

            //纽约30秒彩
            TimeData.timerNy30m.Enabled = false;

            //台湾45秒彩
            TimeData.timerTw45m.Enabled = false;

            //首尔60秒
            TimeData.timerSe60m.Enabled = false;

            //纽约30秒11选5
            TimeData.timerYf11x5.Enabled = false;

            //韩国1.5分11选5
            TimeData.timerHg11x5.Enabled = false;

            //纽约30秒3D
            TimeData.timerSe60sd.Enabled = false;

            //韩国1.5分3D
            TimeData.timerHg90sd.Enabled = false;

            //北京PK10
            //TimeData.timerBjpk10.Enabled = false;

            //英国30秒赛车
            TimeData.timerYfpk10.Enabled = false;

            //英国60秒赛车
            TimeData.timerYg60sc.Enabled = false;

            //英国120秒赛车
            TimeData.timerYg120sc.Enabled = false;
        }

        #region 腾迅分分彩
        /// <summary>
        /// 腾迅分分彩
        /// </summary>
        private static System.Timers.Timer timerQQ60 = new System.Timers.Timer(4000.0);

        /// <summary>
        /// 腾迅分分彩, 锁
        /// </summary>
        private static object obj_locQQ60 = new object();


        private static void ThQQ60_Fun(object stateInfo)
        {
            TimeData.timerQQ60_Elapsed(null, null);
            TimeData.timerQQ60.Start();
        }

        private static void timerQQ60_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locQQ60)
                {
                    DateTime now = DateTime.Now;
                    int hour = now.Hour;
                    int minute = now.Minute;
                    int second = now.Second;
                    int num = minute % 2;
                    //if (second > 0 && second <= 15)
                    //{

                    QqSscData.QqSsc();
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集SSC异常 {0}", ex);
            }
        }
        
        #endregion

        #region 官方时时彩
        /// <summary>
        /// 官方时时彩
        /// </summary>
        private static System.Timers.Timer timerOfficLot = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 官方时时彩, 锁
        /// </summary>
        private static object obj_locOfficLot = new object();


        /// <summary>
        /// 官方时时彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThOfficLot_Fun(object stateInfo)
        {
            TimeData.timerOfficLot_Elapsed(null, null);
            TimeData.timerOfficLot.Start();
        }

        /// <summary>
        /// 官方时时彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerOfficLot_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locOfficLot)
                {
                    OfficialLotteryData.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集重庆时时彩异常: {0}", ex);
            }
        }
        #endregion

        #region 福彩3D

        /// <summary>
        /// 福彩3D
        /// </summary>
        private static System.Timers.Timer timer3d = new System.Timers.Timer(60000.0);

        /// <summary>
        /// 福彩3D, 锁
        /// </summary>
        private static object obj_loc3d = new object();

        /// <summary>
        /// 福彩3D
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void Th3d_Fun(object stateInfo)
        {
            TimeData.timer3d_Elapsed(null, null);
            TimeData.timer3d.Start();
        }

        /// <summary>
        /// 福彩3D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timer3d_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_loc3d)
                {
                    DateTime now = DateTime.Now;
                    int minute = now.Minute;
                    int second = now.Second;
                    if (minute % 10 == 0)
                    {
                        Fc3dData.Fc3d();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("3d Exception:" + ex.Message);
            }
        }

        #endregion

        #region 体彩P3

        /// <summary>
        /// 体彩P3
        /// </summary>
        private static System.Timers.Timer timerP3 = new System.Timers.Timer(60000.0);

        /// <summary>
        /// 体彩P3, 锁
        /// </summary>
        private static object obj_locP3 = new object();

        /// <summary>
        /// 体彩P3
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThP3_Fun(object stateInfo)
        {
            TimeData.timerP3_Elapsed(null, null);
            TimeData.timerP3.Start();
        }

        /// <summary>
        /// 体彩P3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerP3_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locP3)
                {
                    DateTime now = DateTime.Now;
                    int minute = now.Minute;
                    int second = now.Second;
                    if (minute % 10 == 0)
                    {
                        Tcp3Data.Tcp3();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("P3 Exception:" + ex.Message);
            }
        }

        #endregion

        #region 系统彩
        /// <summary>
        /// 系统彩
        /// </summary>
        private static System.Timers.Timer timerSystemLot = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 系统彩, 锁
        /// </summary>
        private static object obj_locSystemLot = new object();


        /// <summary>
        /// 系统彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThSystemLot_Fun(object stateInfo)
        {
            TimeData.timerSystemLot_Elapsed(null, null);
            TimeData.timerSystemLot.Start();
        }

        /// <summary>
        /// 系统彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerSystemLot_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locSystemLot)
                {
                    SystemLotteryData.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集重庆时时彩异常: {0}", ex);
            }
        }
        #endregion

        #region 新德里1.5分彩
        //private static SystemLotteryData xdl90m = new SystemLotteryData("xdl90m");

        /// <summary>
        /// 新德里1.5分彩
        /// </summary>
        private static System.Timers.Timer timerXdl90 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 新德里1.5分彩, 锁
        /// </summary>
        private static object obj_locXdl90 = new object();

        /// <summary>
        /// 新德里1.5分彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThXdl90_Fun(object stateInfo)
        {
            TimeData.timerXdl90_Elapsed(null, null);
            TimeData.timerXdl90.Start();
        }

        /// <summary>
        /// 新德里1.5分彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerXdl90_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locXdl90)
                {
                    SysXdl90mData.UpdateData();
                    //xdl90m.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("新德里1.5分彩: {0}", ex);
            }
        }
        #endregion

        #region 菲律宾1.5分
        /// <summary>
        /// 菲律宾1.5分
        /// </summary>
        private static System.Timers.Timer timerFlb90m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 菲律宾1.5分, 锁
        /// </summary>
        private static object obj_locFlb90m = new object();

        /// <summary>
        /// 菲律宾1.5分
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThFlb90m_Fun(object stateInfo)
        {
            TimeData.timerFlb90m_Elapsed(null, null);
            TimeData.timerFlb90m.Start();
        }

        /// <summary>
        /// 菲律宾1.5分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerFlb90m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locFlb90m)
                {
                    SysFlb90mData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("菲律宾1.5分: {0}", ex);
            }
        }
        #endregion

        #region 韩国1.5分彩
        /// <summary>
        /// 韩国1.5分彩
        /// </summary>
        private static System.Timers.Timer timerHg90m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 韩国1.5分彩, 锁
        /// </summary>
        private static object obj_locHg90m = new object();

        /// <summary>
        /// 韩国1.5分彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThHg90m_Fun(object stateInfo)
        {
            TimeData.timerHg90m_Elapsed(null, null);
            TimeData.timerHg90m.Start();
        }

        /// <summary>
        /// 韩国1.5分彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerHg90m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locHg90m)
                {
                    SysHg90mData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("韩国1.5分彩: {0}", ex);
            }
        }
        #endregion

        #region 东京1.5分彩
        /// <summary>
        /// 东京1.5分彩
        /// </summary>
        private static System.Timers.Timer timerDj15 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 东京1.5分彩, 锁
        /// </summary>
        private static object obj_locDj15 = new object();

        /// <summary>
        /// 东京1.5分彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThDj15_Fun(object stateInfo)
        {
            TimeData.timerDj15_Elapsed(null, null);
            TimeData.timerDj15.Start();
        }

        /// <summary>
        /// 东京1.5分彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerDj15_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locDj15)
                {
                    SysDj15Data.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("东京1.5分彩: {0}", ex);
            }
        }
        #endregion

        #region 新加坡2分彩
        /// <summary>
        /// 新加坡2分彩
        /// </summary>
        private static System.Timers.Timer timerXjp2fc = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 新加坡2分彩, 锁
        /// </summary>
        private static object obj_locXjp2fc = new object();

        /// <summary>
        /// 新加坡2分彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThXjp2fc_Fun(object stateInfo)
        {
            TimeData.timerXjp2fc_Elapsed(null, null);
            TimeData.timerXjp2fc.Start();
        }

        /// <summary>
        /// 新加坡2分彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerXjp2fc_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locXjp2fc)
                {
                    SysXjp2fcData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("新加坡2分彩: {0}", ex);
            }
        }
        #endregion

        #region 台湾5分彩

        //private static SystemLotteryData tw5fc = new SystemLotteryData("tw5fc");

        /// <summary>
        /// 台湾5分彩
        /// </summary>
        private static System.Timers.Timer timerTw5fc = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 台湾5分彩, 锁
        /// </summary>
        private static object obj_locTw5fc = new object();

        /// <summary>
        /// 台湾5分彩
        /// </summary>
        /// <param name="stateInfo"></param>
        private static void ThTw5fc_Fun(object stateInfo)
        {
            TimeData.timerTw5fc_Elapsed(null, null);
            TimeData.timerTw5fc.Start();
        }

        /// <summary>
        /// 台湾5分彩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerTw5fc_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locTw5fc)
                {
                    SysTw5fcData.UpdateData();
                    //tw5fc.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("台湾5分彩: {0}", ex);
            }
        }
        #endregion

        #region 新加坡30秒

        //private static SystemLotteryData xjp30m = new SystemLotteryData("xjp30m");

        /// <summary>
        /// 新加坡30秒
        /// </summary>
        private static System.Timers.Timer timerXjp30m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 新加坡30秒, 锁
        /// </summary>
        private static object obj_locXjp30m = new object();


        private static void ThXjp30m_Fun(object stateInfo)
        {
            TimeData.timerXjp30m_Elapsed(null, null);
            TimeData.timerXjp30m.Start();
        }

        private static void timerXjp30m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locXjp30m)
                {
                    SysXjp30mData.UpdateData();
                    //xjp30m.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("新加坡30秒 :{0}", ex);
            }
        }
        #endregion

        #region 纽约30秒彩

        //private static SystemLotteryData ny30m = new SystemLotteryData("ny30m");

        /// <summary>
        /// 纽约30秒彩
        /// </summary>
        private static System.Timers.Timer timerNy30m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 纽约30秒彩, 锁
        /// </summary>
        private static object obj_locNy30m = new object();

        private static void ThNy30m_Fun(object stateInfo)
        {
            TimeData.timerNy30m_Elapsed(null, null);
            TimeData.timerNy30m.Start();
        }

        private static void timerNy30m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locNy30m)
                {
                    SysNy30mData.UpdateData();
                    //ny30m.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("纽约30秒彩, 异常 {0}", ex);
            }
        }

        #endregion

        #region 台湾45秒彩

        //private static SystemLotteryData tw60m = new SystemLotteryData("tw60m");

        /// <summary>
        /// 台湾45秒彩
        /// </summary>
        private static System.Timers.Timer timerTw45m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 台湾45秒彩, 锁
        /// </summary>
        private static object obj_locTw45m = new object();

        private static void ThTw45m_Fun(object stateInfo)
        {
            TimeData.timerTw45m_Elapsed(null, null);
            TimeData.timerTw45m.Start();
        }

        private static void timerTw45m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locTw45m)
                {
                    SysTw45mData.UpdateData();
                    //tw60m.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("台湾45秒彩: {0}", ex);
            }
        }
        #endregion

        #region 首尔60秒

        //private static SystemLotteryData se60m = new SystemLotteryData("se60m");

        /// <summary>
        /// 首尔60秒
        /// </summary>
        private static System.Timers.Timer timerSe60m = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 首尔60秒, 锁
        /// </summary>
        private static object obj_locSe60m = new object();

        private static void ThSe60m_Fun(object stateInfo)
        {
            TimeData.timerSe60m_Elapsed(null, null);
            TimeData.timerSe60m.Start();
        }

        private static void timerSe60m_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locSe60m)
                {
                    SysSe60mData.UpdateData();
                    //se60m.UpdateLottery();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("首尔60秒: {0}", ex);
            }
        }

        #endregion

        #region 纽约30秒11选5

        /// <summary>
        /// 纽约30秒11选5
        /// </summary>
        private static System.Timers.Timer timerYf11x5 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 纽约30秒11选5, 锁
        /// </summary>
        private static object obj_locYf11x5 = new object();

        private static void ThYf11x5_Fun(object stateInfo)
        {
            TimeData.timerYf11x5_Elapsed(null, null);
            TimeData.timerYf11x5.Start();
        }

        private static void timerYf11x5_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locYf11x5)
                {
                    SysYf11x5Data.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("纽约30秒11选5: {0}", ex);
            }
        }

        #endregion

        #region 韩国1.5分11选5

        /// <summary>
        /// 韩国1.5分11选5
        /// </summary>
        private static System.Timers.Timer timerHg11x5 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 韩国1.5分11选5, 锁
        /// </summary>
        private static object obj_locHg11x5 = new object();

        private static void ThHg11x5_Fun(object stateInfo)
        {
            TimeData.timerHg11x5_Elapsed(null, null);
            TimeData.timerHg11x5.Start();
        }

        private static void timerHg11x5_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locHg11x5)
                {
                    SysHg11x5Data.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("韩国1.5分11选5: {0}", ex);
            }
        }

        #endregion

        #region 纽约30秒3D

        /// <summary>
        /// 纽约30秒3D
        /// </summary>
        private static System.Timers.Timer timerSe60sd = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 纽约30秒3D, 锁
        /// </summary>
        private static object obj_locSe60sd = new object();

        private static void ThSe60sd_Fun(object stateInfo)
        {
            TimeData.timerSe60sd_Elapsed(null, null);
            TimeData.timerSe60sd.Start();
        }

        private static void timerSe60sd_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locSe60sd)
                {
                    SysSe60sdData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("纽约30秒3D: {0}", ex);
            }
        }

        #endregion

        #region 韩国1.5分3D

        /// <summary>
        /// 韩国1.5分3D
        /// </summary>
        private static System.Timers.Timer timerHg90sd = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 韩国1.5分3D, 锁
        /// </summary>
        private static object obj_locHg90sd = new object();

        private static void ThHg90sd_Fun(object stateInfo)
        {
            TimeData.timerHg90sd_Elapsed(null, null);
            TimeData.timerHg90sd.Start();
        }

        private static void timerHg90sd_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locHg90sd)
                {
                    SysHg90sdData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("纽约30秒3D: {0}", ex);
            }
        }

        #endregion

        #region 北京PK10

        /// <summary>
        /// 北京PK10
        /// </summary>
        private static System.Timers.Timer timerBjpk10 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 北京PK10, 锁
        /// </summary>
        private static object obj_locBjpk10 = new object();

        private static void ThBjpk10_Fun(object stateInfo)
        {
            TimeData.timerBjpk10_Elapsed(null, null);
            TimeData.timerBjpk10.Start();
        }

        private static void timerBjpk10_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locBjpk10 )
                {
                    SysBjpk10Data.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("北京PK10: {0}", ex);
            }
        }

        #endregion

        #region 英国30秒赛车

        /// <summary>
        /// 英国30秒赛车
        /// </summary>
        private static System.Timers.Timer timerYfpk10 = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 英国30秒赛车, 锁
        /// </summary>
        private static object obj_locYfpk10 = new object();

        private static void ThYfpk10_Fun(object stateInfo)
        {
            TimeData.timerYfpk10_Elapsed(null, null);
            TimeData.timerYfpk10.Start();
        }

        private static void timerYfpk10_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locYfpk10)
                {
                    SysYfpk10Data.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("英国30秒赛车: {0}", ex);
            }
        }

        #endregion

        #region 英国60秒赛车

        /// <summary>
        /// 英国60秒赛车
        /// </summary>
        private static System.Timers.Timer timerYg60sc = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 英国60秒赛车, 锁
        /// </summary>
        private static object obj_locYg60sc = new object();

        private static void ThYg60sc_Fun(object stateInfo)
        {
            TimeData.timerYg60sc_Elapsed(null, null);
            TimeData.timerYg60sc.Start();
        }

        private static void timerYg60sc_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locYg60sc)
                {
                    SysYg60scData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("英国60秒赛车: {0}", ex);
            }
        }

        #endregion

        #region 英国120秒赛车

        /// <summary>
        /// 英国120秒赛车
        /// </summary>
        private static System.Timers.Timer timerYg120sc = new System.Timers.Timer(5000.0);

        /// <summary>
        /// 英国120秒赛车, 锁
        /// </summary>
        private static object obj_locYg120sc = new object();

        private static void ThYg120sc_Fun(object stateInfo)
        {
            TimeData.timerYg120sc_Elapsed(null, null);
            TimeData.timerYg120sc.Start();
        }

        private static void timerYg120sc_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                lock (TimeData.obj_locYg120sc)
                {
                    SysYg120scData.UpdateData();
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("英国120秒赛车: {0}", ex);
            }
        }

        #endregion

        //private static void ThHg90_Fun(object stateInfo)
        //{
        //    TimeData.timerHg90_Elapsed(null, null);
        //    TimeData.timerHg90.Start();
        //}

        //private static void timerHg90_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locHg90)
        //        {
        //            int second = DateTime.Now.Second;
        //            YouleToOther.DataToOther(1010);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("采集SSC异常 {0}", ex);
        //        Console.WriteLine("SSC Exception:" + ex.Message);
        //    }
        //}

        //private static void ThXjp120_Fun(object stateInfo)
        //{
        //    TimeData.timerXjp120_Elapsed(null, null);
        //    TimeData.timerXjp120.Start();
        //}

        //private static void timerXjp120_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locXjp120)
        //        {
        //            int second = DateTime.Now.Second;
        //            YouleToOther.DataToOther(1012);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("采集SSC异常 {0}", ex);
        //        Console.WriteLine("SSC Exception:" + ex.Message);
        //    }
        //}

        //private static void ThDj90_Fun(object stateInfo)
        //{
        //    TimeData.timerDj90_Elapsed(null, null);
        //    TimeData.timerDj90.Start();
        //}

        //private static void timerDj90_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locDj90)
        //        {
        //            int second = DateTime.Now.Second;
        //            YouleToOther.DataToOther(1016);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("采集SSC异常 {0}", ex);
        //        Console.WriteLine("SSC Exception:" + ex.Message);
        //    }
        //}

        //private static void ThOne_Fun(object stateInfo)
        //{
        //    TimeData.timerOne_Elapsed(null, null);
        //    TimeData.timerOne.Start();
        //}

        //private static void timerOne_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locOne)
        //        {
        //            int second = DateTime.Now.Second;
        //            YouleToOther.DataToOther(1009);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("采集SSC异常 {0}", ex);
        //        Console.WriteLine("SSC Exception:" + ex.Message);
        //    }
        //}

        //private static void ThOne11x5_Fun(object stateInfo)
        //{
        //    TimeData.timerOne11x5_Elapsed(null, null);
        //    TimeData.timerOne11x5.Start();
        //}

        //private static void timerOne11x5_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locOne11x5)
        //        {
        //            YouleToOther11x5.DataToOther(2005);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("One11x5 Exception:" + ex.Message);
        //    }
        //}

        //private static void ThOne3d_Fun(object stateInfo)
        //{
        //    TimeData.timerOne3d_Elapsed(null, null);
        //    TimeData.timerOne3d.Start();
        //}

        //private static void timerOne3d_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locOne3d)
        //        {
        //            YouleToOther3d.DataToOther(3006);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("One3d Exception:" + ex.Message);
        //    }
        //}

        //private static void ThHg3d_Fun(object stateInfo)
        //{
        //    TimeData.timerHg3d_Elapsed(null, null);
        //    TimeData.timerHg3d.Start();
        //}

        //private static void timerHg3d_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locHg3d)
        //        {
        //            YouleToOther3d.DataToOther(3004);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Hg3d Exception:" + ex.Message);
        //    }
        //}

        //private static void ThSe3d_Fun(object stateInfo)
        //{
        //    TimeData.timerSe3d_Elapsed(null, null);
        //    TimeData.timerSe3d.Start();
        //}

        //private static void timerSe3d_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locSe3d)
        //        {
        //            YouleToOther3d.DataToOther(3005);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Se3d Exception:" + ex.Message);
        //    }
        //}

        //private static void ThOnePk10_Fun(object stateInfo)
        //{
        //    TimeData.timerOnePk10_Elapsed(null, null);
        //    TimeData.timerOnePk10.Start();
        //}

        //private static void timerOnePk10_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locOnePk10)
        //        {
        //            YouleToOther11x5.DataToOther(4002);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("OnePk10 Exception:" + ex.Message);
        //    }
        //}

        //private static void ThYg60Pk10_Fun(object stateInfo)
        //{
        //    TimeData.timerYg60Pk10_Elapsed(null, null);
        //    TimeData.timerYg60Pk10.Start();
        //}

        //private static void timerYg60Pk10_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locYg60Pk10)
        //        {
        //            YouleToOther11x5.DataToOther(4003);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Yg60Pk10 Exception:" + ex.Message);
        //    }
        //}

        //private static void ThYg120Pk10_Fun(object stateInfo)
        //{
        //    TimeData.timerYg120Pk10_Elapsed(null, null);
        //    TimeData.timerYg120Pk10.Start();
        //}

        //private static void timerYg120Pk10_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        lock (TimeData.obj_locYg120Pk10)
        //        {
        //            YouleToOther11x5.DataToOther(4004);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Yg120Pk10 Exception:" + ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 重庆时时彩
        ///// </summary>
        //private static System.Timers.Timer timerHg90 = new System.Timers.Timer(5000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locHg90 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerXjp120 = new System.Timers.Timer(5000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locXjp120 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerDj90 = new System.Timers.Timer(5000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locDj90 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerOne = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locOne = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerOne11x5 = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locOne11x5 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerOne3d = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locOne3d = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerHg3d = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locHg3d = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerSe3d = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locSe3d = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerOnePk10 = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locOnePk10 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerYg60Pk10 = new System.Timers.Timer(1000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locYg60Pk10 = new object();

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static System.Timers.Timer timerYg120Pk10 = new System.Timers.Timer(2000.0);

        ///// <summary>
        ///// 重庆时时彩, 锁
        ///// </summary>
        //private static object obj_locYg120Pk10 = new object();
    }
}
