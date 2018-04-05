using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Lottery.Core;

namespace Lottery.Api
{
    /// <summary>
    /// api跨域
    /// </summary>
    public class CrossSiteAttribute : ActionFilterAttribute
    {
        private const string Origin = "Origin";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string OriginHeaderdefault = "*";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //var origin = HttpHelper.GetHeaderByKey(actionExecutedContext.Request, Origin);
            //origin = string.IsNullOrEmpty(origin) ? HttpHelper.GetReferrer(actionExecutedContext.Request) : origin;
            //origin = HttpHelper.ValidReuest(origin);
            ////var debug = TypeConvert.ToBoolean(HttpHelper.GetHeaderByKey(actionExecutedContext.Request, "Debug"));

            //if (!string.IsNullOrEmpty(origin))
            //{
            //    actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, origin);
            //}
            ////else if (debug)
            ////{
            ////    actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, OriginHeaderdefault);
            ////}
        }
    }
}