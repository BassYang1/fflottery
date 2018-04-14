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
    /// 会员
    /// </summary>
    [RoutePrefix("v1/user")]
    public class UserController : BaseController
    {
        private IUserService UserService { get; set; }

        /// <summary>
        /// 会员账户
        /// </summary>
        /// <param name="userService"></param>
        public UserController()
        {
            this.UserService = new UserService();
        }

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="model">会员注册</param>
        /// <returns>会员登录成功凭证Token</returns>
        [Route("regiter")]
        [HttpPost]
        [CrossSite]
        [Description("会员注册")]
        public Result<string> Regiter(UserRegModel model)
        {
            if (model == null)
            {
                return GetInvalidResult<string>(null, "无效的会员注册信息");
            }

            if (string.IsNullOrEmpty(model.MerchantId))
            {
                return GetInvalidResult<string>(null, "商户Id不能为空");
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                return GetInvalidResult<string>(null, "会员名称不能为空");
            }
            if (string.IsNullOrEmpty(model.SignKey))
            {
                return GetInvalidResult<string>(null, "签名不能为空");
            }

            try
            {
                var result = this.UserService.RegiterUser(model);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns>会员登录成功凭证Token</returns>
        [Route("login")]
        [HttpPost]
        [CrossSite]
        [Description("会员登录")]
        public Result<string> Login(UserAddModel model)
        {
            if (model == null)
            {
                return GetInvalidResult<string>(null, "无效的会员登录信息");
            }

            if (string.IsNullOrEmpty(model.MerchantId))
            {
                return GetInvalidResult<string>(null, "商户Id不能为空");
            }
            if (string.IsNullOrEmpty(model.UserName))
            {
                return GetInvalidResult<string>(null, "会员名称不能为空");
            }
            if (string.IsNullOrEmpty(model.SignKey))
            {
                return GetInvalidResult<string>(null, "签名不能为空");
            }

            try
            {
                var result = this.UserService.GetUserToken(model);
                return GetSuccessResult(result); 
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }

        /// <summary>
        /// 获取用户下注，最新20条
        /// </summary>
        /// <param name="lotteryId">彩票种类Id</param>
        /// <returns>用户下注，最新20条</returns>
        [Route("bet")]
        [HttpGet]
        [CrossSite]
        [Description("获取用户下注，最新20条")]
        public Result<string> GetUserBets(int lotteryId)
        {
            try
            {
                if (CurrentUser == null)
                {
                    return GetInvalidResult<string>(null, "登录用户无效");
                }

                var result = this.UserService.GetUserBets(CurrentUser.Id, lotteryId);
                return GetSuccessResult(result);
            }
            catch (Exception ex)
            {
                return GetExceptionResult<string>(null, ex);
            }
        }
    }
}
