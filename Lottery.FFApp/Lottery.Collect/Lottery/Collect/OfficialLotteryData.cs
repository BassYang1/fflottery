using System;
using System.Collections;
using System.Text;
using LitJson;
using Lottery.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;
using System.Configuration;
using Lottery.Collect.Boyi;
using Lottery.Entity;
using System.Threading;

namespace Lottery.Collect
{
    /// <summary>
    /// 官方时时彩
    /// <remarks>
    /// 重庆时时彩 cqssc
    /// 新疆时时彩 xjssc
    /// 天津时时彩 tjssc
    /// </remarks>
    /// </summary>
    public class OfficialLotteryData
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(OfficialLotteryData));

        private static LotteryDataDAL _dal = new LotteryDataDAL();
        private static LotteryDAL _lotteryDal = new LotteryDAL();

        public static void UpdateLottery()
        { 
            //重庆时时彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "cqssc");

            //新疆时时彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "xjssc");

            //天津时时彩
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "tjssc");

            //广东11选5
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "gd11x5");

            //上海11选5
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "sh11x5");

            //江西11选5
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "jx11x5");

            //福彩3D
            //ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "3d");

            //体彩 排列3
            //ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "p3");

            //上海时时乐
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "ssl");

            //北京PK10 bjpk10
            ThreadPool.QueueUserWorkItem(new WaitCallback(UpdateData), "bjpk10");
        }

        /// <summary>
        /// 更新彩票数据
        /// </summary>
        /// <param name="code">彩票编码</param>
        public static void UpdateData(object code = null)
        {
            try
            {
                //http://api.b1cp.com/t?p=json&t=qqffc&token=FF446B723EB25993
                //http://www.b1cp.com/api?p=json&t=txffc&limit=1&token=00fb782bad8e5241
                //Log.Debug("开始QqSsc...");

                //if (code == "ssl")
                //{
                //    string test = "";
                //}

                SysLotteryModel sysLottery = _lotteryDal.GetSysLotteryByCode(code.ToString());

                Log.DebugFormat("获取开奖信息: {0} {1}", sysLottery.Title, sysLottery.Code);
                if (sysLottery == null || string.IsNullOrEmpty(sysLottery.ApiUrl))
                {
                    throw new Exception("无效的API配置");
                }

                int lotId = sysLottery.Id; //彩票Id
                IList<ByLottery> data = ByHelper.GetLotteryData(sysLottery.ApiUrl);

                foreach (ByLottery lot in data)
                {
                    string openTime = lot.opentime;
                    string openCode = lot.opencode;
                    string expect = lot.expect; //期号

                    expect = GetExpect(code.ToString(), expect);

                    if (_dal.Update(lotId, expect, openCode, openTime, openCode))
                    {
                        Public.SaveLotteryData2File(lotId);
                        LotteryCheck.RunOfIssueNum(lotId, expect);
                        Log.DebugFormat("发布开奖信息: {0} {1}", sysLottery.Title, openCode);
                    }

                    Log.DebugFormat("开奖信息详细: {0} {1} {2}", sysLottery.Title, expect, openCode);
                }
            }
            catch (Exception ex)
            {                
                Log.ErrorFormat("官方彩票异常: {0} {1}", code != null ? code.ToString() : "奖种无效", ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改期号
        /// </summary>
        /// <param name="code"></param>
        /// <param name="expect"></param>
        /// <returns></returns>
        private static string GetExpect(string code, string expect)
        {
            switch (code)
            {
                case "tjssc":
                case "cqssc":
                    return expect.Substring(0, 8) + "-" + expect.Substring(8);
                case "xjssc":
                case "ssl":
                    return expect.Substring(0, 8) + "-" + expect.Substring(9);
                case "gd11x5":
                case "sh11x5":
                case "jx11x5":
                    return expect.Substring(0, 8) + "-" + expect.Substring(9);
                case "p3":
                case "3d":
                    return expect;
                case "bjpk10": //北京PK10
                    return expect;
                default:
                    return expect;

            }
        }
    }
}
