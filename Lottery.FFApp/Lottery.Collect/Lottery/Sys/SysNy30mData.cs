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
    /// 纽约30秒彩
    /// </summary>
    public class SysNy30mData : SysBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SysNy30mData));
        private static SysBase Lottery = new SysNy30mData();

        public SysNy30mData()
            : base("ny30m", "纽约30秒彩")
        {
            base.NumberCount = 5;
            base.NumberAllCount = 20;
            base.NumberAllSize = 2;
            Log.DebugFormat("彩种: {0}, {1}", base.Name, base.Code);
        }

        /// <summary>
        /// 生成彩票开奖信息
        /// </summary>
        /// <returns></returns>
        public override void Generate()
        {
            int[] numArr = new int[base.NumberCount];
            string[] numAllArr = new string[base.NumberAllCount];

            //生成开奖初使信息
            for (int i = 0; i < base.NumberAllCount; i++)
            {
                numAllArr[i] = GetRandomNums(base.NumberAllSize);

                //生成开奖号码
                if ((i + 1) % 4 == 0)
                {
                    numArr[i - 3 * ((i + 1) / 4)] = (Convert.ToInt32(numAllArr[i]) + Convert.ToInt32(numAllArr[i - 1]) + Convert.ToInt32(numAllArr[i - 2]) + Convert.ToInt32(numAllArr[i - 3])) % 10;
                }
            }

            base.NumberAll = string.Join(",", numAllArr);
            base.Number = string.Join(",", numArr);

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
   