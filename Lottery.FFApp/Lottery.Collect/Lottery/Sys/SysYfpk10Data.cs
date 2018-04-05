using log4net;
using Lottery.DAL;
using Lottery.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lottery.Collect.Sys
{
    /// <summary>
    /// 英国30秒赛车
    /// </summary>
    public class SysYfpk10Data : SysBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SysYfpk10Data));
        private static SysBase Lottery = new SysYfpk10Data();

        public SysYfpk10Data()
            : base("yfpk10", "英国30秒赛车")
        {
            base.NumberCount = 10;
            base.NumberAllCount = 10;
            base.NumberAllSize = 2;
            Log.DebugFormat("彩种: {0}, {1}", base.Name, base.Code);
        }

        /// <summary>
        /// 生成彩票开奖信息
        /// </summary>
        /// <returns></returns>
        public override void Generate()
        {
            string[] source = { "02", "01", "03", "06", "07", "09", "04", "06", "10", "05", "08", "02", "01", "03", "10", "07", "09", "04", "06", "10", "05", "08" };
            string[] numAllArr = GetRandomNums(source, 10, false);

            base.NumberAll = string.Join(",", numAllArr);
            base.Number = base.NumberAll;
            Log.DebugFormat("生成开奖号码: {0} {1}", base.Name, base.Number);
        }

        /// <summary>
        /// 更新开奖信息
        /// </summary>
        public static void UpdateData(object code = null)
        {
            try
            {
                //更新开奖期号
                Lottery.UpdateExpect();

                if (string.IsNullOrEmpty(Lottery.LastExpect) || !Lottery.LastExpect.Equals(Lottery.ExpectNo))
                {
                    Lottery.LastExpect = Lottery.ExpectNo;
                    Lottery.UpdateLottery();
                    Log.DebugFormat("开奖期数: {0} {1}", Lottery.Name, Lottery.ExpectNo);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("开奖发生异常: {0} {1}", Lottery.Name, ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }
    }
}
   