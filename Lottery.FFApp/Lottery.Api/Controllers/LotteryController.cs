using Lottery.FFModel;
using Lottery.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Configuration;
using Lottery.Core;

namespace Lottery.Api.Controllers
{
    /// <summary>
    /// 彩票
    /// </summary>
    [RoutePrefix("v1/lottery")]
    public class LotteryController : BaseController
    {
        private ILotteryService LotteryService { get; set; }

        /// <summary>
        /// 彩票
        /// </summary>
        /// <param name="userService"></param>
        public LotteryController()
        {
            this.LotteryService = new LotteryService();
        }

        /// <summary>
        /// 获取彩票开奖数据，最新20条
        /// </summary>
        /// <param name="lotteryId">彩票种类id</param>
        /// <returns>彩票开奖数据，最新20条</returns>
        [Route("data")]
        [HttpGet]
        [CrossSite]
        [Description("彩票开奖数据，最新20条")]
        public Result<string> GetLotteryData(int lotteryId)
        {
            try
            {
                var result = this.LotteryService.GetLotteryData(lotteryId);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 获取彩票种类
        /// </summary>
        /// <returns>彩票种类</returns>
        [Route("type")]
        [HttpGet]
        [CrossSite]
        [Description("彩票种类")]
        public Result<string> GetLottery()
        {
            try
            {
                var result = this.LotteryService.GetLottery();
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 获取彩票种类玩法
        /// </summary>
        /// <returns>彩票种类玩法</returns>
        [Route("type/play")]
        [HttpGet]
        [CrossSite]
        [Description("彩票种类玩法")]
        public Result<string> GetLotteryPlayType()
        {
            try
            {
                var result = this.LotteryService.GetLotteryPlayType();
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 获取彩票玩法大类
        /// </summary>
        /// <returns>大类</returns>
        [Route("big/type")]
        [HttpGet]
        [CrossSite]
        [Description("获取彩票玩法大类")]
        public Result<string> GetPlayBigType()
        {
            try
            {
                var result = this.LotteryService.GetPlayBigType();
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 获取彩票玩法小类
        /// </summary>
        /// <returns>小类</returns>
        [Route("small/type")]
        [HttpGet]
        [CrossSite]
        [Description("获取彩票玩法小类")]
        public Result<string> GetPlaySmallType()
        {
            try
            {
                var result = this.LotteryService.GetPlaySmallType();
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }
    }
}
