using Lottery.FFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Service
{
    public interface ILotteryService
    {
        /// <summary>
        /// 获取彩票开奖数据，最新20条
        /// </summary>
        /// <param name="lotteryId">彩票种类id</param>
        /// <returns>彩票开奖数据，最新20条</returns>
        string GetLotteryData(int lotteryId);

        /// <summary>
        /// 获取彩票种类
        /// </summary>
        /// <returns>彩票种类</returns>
        string GetLottery();

        /// <summary>
        /// 获取彩票种类玩法
        /// </summary>
        /// <returns>彩票种类玩法</returns>
        string GetLotteryPlayType();

        /// <summary>
        /// 获取彩票玩法大类
        /// </summary>
        /// <returns>大类</returns>
        string GetPlayBigType();

        /// <summary>
        /// 获取彩票玩法小类
        /// </summary>
        /// <returns>小类</returns>
        string GetPlaySmallType();
    }
}
