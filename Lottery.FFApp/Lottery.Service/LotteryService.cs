using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using Lottery.FFModel;
using Lottery.Core;
using Lottery.FFData;
using System.Data;
using Lottery.Utils;
using Lottery.DAL;
using Lottery.FFCache;
using Lottery.Collect;

namespace Lottery.Service
{
    public class LotteryService : BaseService, ILotteryService
    {
        /// <summary>
        /// 获取彩票开奖数据，最新20条
        /// </summary>
        /// <param name="lotteryId">彩票种类id</param>
        /// <returns>彩票开奖数据，最新20条</returns>
        public string GetLotteryData(int lotteryId)
        {
            return Public.GetOpenListJson(lotteryId).Replace("[", "").Replace("]", "").ToLower();
        }

        /// <summary>
        /// 获取彩票种类
        /// </summary>
        /// <returns>彩票种类</returns>
        public string GetLottery()
        {
            string json = (string)RTCache.Get(Const.CACHE_KEY_SYS_LOTTERY_TABLE);

            if (string.IsNullOrEmpty(json))
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by sort asc";
                DataTable dataTable1 = this.doh.GetDataTable();
                json = dtHelp.DT2JSON(dataTable1);
                dataTable1.Clear();
                dataTable1.Dispose();

                RTCache.Insert(Const.CACHE_KEY_SYS_LOTTERY_PLAY_TABLE, json);
            }

            return json;
        }

        /// <summary>
        /// 获取彩票种类玩法
        /// </summary>
        /// <returns>彩票种类玩法</returns>
        public string GetLotteryPlayType()
        {
            string json = (string)RTCache.Get(Const.CACHE_KEY_SYS_LOTTERY_PLAY_TABLE);

            if (string.IsNullOrEmpty(json))
            {
                this.doh.SqlCmd = "SELECT Id,Title,Ltype FROM Sys_Lottery where IsOpen=0 ORDER BY Sort asc";
                DataTable dataTable2 = this.doh.GetDataTable();
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT Id,LotteryId,Title FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
                DataTable dataTable3 = this.doh.GetDataTable();
                json = dtHelp.DT2JSON2(dataTable2, dataTable3);
                dataTable2.Clear();
                dataTable3.Clear();
                dataTable2.Dispose();
                dataTable3.Dispose();

                RTCache.Insert(Const.CACHE_KEY_SYS_LOTTERY_PLAY_TABLE, json);
            }

            return json;
        }

        /// <summary>
        /// 获取彩票玩法大类
        /// </summary>
        /// <returns>大类</returns>
        public string GetPlayBigType()
        {
            string json = (string)RTCache.Get(Const.CACHE_KEY_SYS_BIG_PLAY_TABLE);

            if (string.IsNullOrEmpty(json))
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
                DataTable dataTable1 = this.doh.GetDataTable();
                json = dtHelp.DT2JSON(dataTable1);
                dataTable1.Clear();
                dataTable1.Dispose();

                RTCache.Insert(Const.CACHE_KEY_SYS_BIG_PLAY_TABLE, json);
            }

            return json;
        }

        /// <summary>
        /// 获取彩票玩法小类
        /// </summary>
        /// <returns>小类</returns>
        public string GetPlaySmallType()
        {
            string json = (string)RTCache.Get(Const.CACHE_KEY_SYS_SMALL_PLAY_TABLE);

            if (string.IsNullOrEmpty(json))
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where IsOpen=0 ORDER BY Sort asc";
                DataTable dataTable2 = this.doh.GetDataTable();
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=0 and flag=0 ORDER BY Sort asc";
                DataTable dataTable3 = this.doh.GetDataTable();
                json = dtHelp.DT2JSON(dataTable2, dataTable3);
                dataTable2.Clear();
                dataTable3.Clear();
                dataTable2.Dispose();
                dataTable3.Dispose();

                RTCache.Insert(Const.CACHE_KEY_SYS_SMALL_PLAY_TABLE, json);
            }

            return json;
        }
    }
}