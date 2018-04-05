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
using System.Diagnostics;
using Lottery.Utils;
using System.Linq;
using Lottery.FFCache;

namespace Lottery.Collect
{
    public class JsonDataModel
    {
        public string Number { get; set; }
        public string DateLine { get; set; }
        public string DataQs { get; set; }
    }

    public class QqSscData
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(QqSscData));

        private static LotteryDataDAL _dal = new LotteryDataDAL();
        private static LotteryDAL _lotteryDal = new LotteryDAL();
        private static DateTime? OpenTime = null; //开奖时间

        public static void QqSsc()
        {
            try
            {
                //http://api.b1cp.com/t?p=json&t=qqffc&token=FF446B723EB25993
                //http://www.b1cp.com/api?p=json&t=txffc&limit=1&token=00fb782bad8e5241
                Log.Debug("开始腾迅分分彩...");
                
                SysLotteryModel sysLottery = _lotteryDal.GetSysLotteryByCode("qqffc");

                if (sysLottery == null || string.IsNullOrEmpty(sysLottery.ApiUrl))
                {
                    throw new Exception("无效的API配置");
                }

                //Stopwatch sw = new Stopwatch();
                //sw.Start(); //  开始监视代码运行时间

                //如果上期开奖时间大于1分钟
                if (OpenTime.HasValue && OpenTime.Value.AddSeconds(60) > DateTime.Now)
                {
                    return;
                }

                //string url = "http://api.b1cp.com/t?p=json&t=txffc&token=776E371D7EF404E4";
                //IList<ByLottery> data = ByHelper.GetLotteryData(url);
                IList<ByLottery> data = ByHelper.GetLotteryData(sysLottery.ApiUrl);

                //sw.Stop(); //  停止监视
                //Log.DebugFormat("获取腾迅分分彩花费时间: {0}毫秒", sw.Elapsed.Milliseconds);

                foreach (ByLottery lot in data)
                {
                    string openTime = lot.opentime;
                    string openCode = lot.opencode;
                    string expect = lot.expect; //期号

                    expect = expect.Substring(0, 8) + "-" + expect.Substring(8);

                    //存储开奖号码
                    if (_dal.Update(1005, expect, openCode, openTime, openCode))
                    {
                        OpenTime = Convert.ToDateTime(openTime); //开奖时间
                        Public.SaveLotteryData2File(1005); //存储开奖号码到JSON/XML文件
                        LotteryCheck.RunOfIssueNum(1005, expect); //处理开奖信息
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("腾讯分分彩异常: {0}", ex);
                //new LogExceptionDAL().Save("采集异常", "腾讯分分彩获取开奖数据出错，错误代码：" + ex.Message);
            }
        }

        /// <summary>
        /// 已开奖
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="number"></param>
        /// <param name="opentime"></param>
        /// <param name="numberAll"></param>
        /// <returns></returns>
        public static bool ExistLottery(int type, string title, string number, string opentime, string numberAll)
        {
            string cacheKey = string.Format(Const.CACHE_KEY_LOTTERY_HISTORY, type);
            IList<LotteryDataModel> history = _dal.GetLotteryHistory(type);

            //数据已存在
            if ((from it in history where it.Title == title select it).Count() > 0)
            {
                Log.ErrorFormat("彩票已开奖, 彩种: {0}, 期号: {1}", type, title);
                return true;
            }

            //更新缓存
            if (history.Count > 0)
            {
                history.RemoveAt(history.Count - 1); //移除最后一条
            }

            history.Insert(0, new LotteryDataModel()
            {
                Type = type,
                Title = title,
                Number = number,
                NumberAll = numberAll,
                Total = LotterySum.SumNumber(number),
                Dx = 0,
                Ds = 0,
                Flag = 0,
                Flag2 = 0,
                IsFill = true,
                OpenTime = Convert.ToDateTime(opentime)
            });

            RTCache.Insert(cacheKey, history);
            return false;
        }

    }
}
