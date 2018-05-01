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

        /// <summary>
        /// 会员提现
        /// </summary>
        /// <param name="model">提现信息</param>
        /// <returns>提现结果</returns>
        [Route("withdraw")]
        [HttpPost]
        [CrossSite]
        [Description("会员提现")]
        public Result<UserWithdrawResultModel> Withdraw(UserWithdrawModel model)
        {
            try
            {
                var result = this.AccountService.Withdraw(model);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<UserWithdrawResultModel>(null, ex);
            }
        }

        /// <summary>
        /// 分页查询充值记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>充值记录</returns>
        [Route("search/charge")]
        [HttpPost]
        [CrossSite]
        [Description("分页查询投注记录")]
        public Result<PageData<UserChargeRecordModel>> Search(SearchChargeModel query, int pageSize, int pageIndex)
        {
            try
            {
                if (CurrentUser == null)
                {
                    return GetInvalidResult(new PageData<UserChargeRecordModel>(pageSize, pageIndex), "登录用户无效");
                }

                var result = this.AccountService.SearchCharge(query, pageSize, pageIndex);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult(new PageData<UserChargeRecordModel>(pageSize, pageIndex), ex);
            }
        }

        /// <summary>
        /// 分页查询提现记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <returns>提现记录</returns>
        [Route("search/withdraw")]
        [HttpPost]
        [CrossSite]
        [Description("分页查询投注记录")]
        public Result<PageData<UserWithdrawRecordModel>> Search(SearchWithdrawModel query, int pageSize, int pageIndex)
        {
            try
            {
                if (CurrentUser == null)
                {
                    return GetInvalidResult(new PageData<UserWithdrawRecordModel>(pageSize, pageIndex), "登录用户无效");
                }

                var result = this.AccountService.SearchWithdraw(query, pageSize, pageIndex);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult(new PageData<UserWithdrawRecordModel>(pageSize, pageIndex), ex);
            }
        }
    }
}
