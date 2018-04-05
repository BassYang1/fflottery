using log4net;
using Lottery.Collect.Sys;
using Lottery.DAL;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace Lottery.Collect
{
    /// <summary>
    /// 系统彩
    /// </summary>
    public class SystemLotteryData
    {
        public static void UpdateLottery()
        {
            //新德里1.5分彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysXdl90mData.UpdateData));

            //菲律宾1.5分
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysFlb90mData.UpdateData));

            //韩国1.5分彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysHg90mData.UpdateData));

            //东京1.5分彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysDj15Data.UpdateData));

            //新加坡2分彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysXjp2fcData.UpdateData));

            //台湾5分彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysTw5fcData.UpdateData));

            //新加坡30秒
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysXjp30mData.UpdateData));

            //纽约30秒彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysNy30mData.UpdateData));

            //台湾45秒彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysTw45mData.UpdateData));

            //首尔60秒
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysSe60mData.UpdateData));

            //纽约30秒11选5
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysYf11x5Data.UpdateData));

            //韩国1.5分11选5
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysHg11x5Data.UpdateData));

            //纽约30秒3D
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysSe60sdData.UpdateData));

            //韩国1.5分3D
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysHg90sdData.UpdateData));

            //北京PK10
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysBjpk10Data.UpdateData));

            //英国30秒赛车
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysYfpk10Data.UpdateData));

            //英国60秒赛车
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysYg60scData.UpdateData));

            //英国120秒赛车
            ThreadPool.QueueUserWorkItem(new WaitCallback(SysYg120scData.UpdateData));
        }
    }
}