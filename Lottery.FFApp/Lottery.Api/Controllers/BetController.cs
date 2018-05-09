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
    /// 投注
    /// </summary>
    [RoutePrefix("v1/bet")]
    public class BetController : BaseController
    {
        private IBetService BetService { get; set; }

        /// <summary>
        /// 会员账户
        /// </summary>
        /// <param name="userService"></param>
        public BetController()
        {
            this.BetService = new BetService();
        }

        /// <summary>
        /// 分页查询投注记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>投注数据</returns>
        [Route("search")]
        [HttpPost]
        [CrossSite]
        [Description("分页查询投注记录")]
        public Result<PageData<UserBetRecordModel>> Search(SearchBetModel query, int pageSize, int pageIndex)
        {
            try
            {
                var result = this.BetService.SearchBets(query, pageSize, pageIndex);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult(new PageData<UserBetRecordModel>(pageSize, pageIndex), ex);
            }
        }
    }
}
