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
    /// 会员账户
    /// </summary>
    [RoutePrefix("v1/account")]
    public class AccountController : BaseController
    {
        private IAccountService AccountService { get; set; }

        /// <summary>
        /// 会员账户
        /// </summary>
        /// <param name="userService"></param>
        public AccountController()
        {
            this.AccountService = new AccountService();
        }

        /// <summary>
        /// 会员充值
        /// </summary>
        /// <param name="model">充值信息</param>
        /// <returns>充值结果</returns>
        [Route("charge")]
        [HttpPost]
        [CrossSite]
        [Description("会员充值")]
        public Result<UserChargeResultModel> Charge(UserChargeModel model)
        {
            try
            {
                var result = this.AccountService.Charge(model);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<UserChargeResultModel>(null, ex);
            }
        }
    }
}
