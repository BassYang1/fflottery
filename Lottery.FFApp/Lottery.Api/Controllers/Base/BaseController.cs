using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using log4net;
using Lottery.FFModel;
using Lottery.Core;

namespace Lottery.Api.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public abstract class BaseController : ApiController
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(BaseController));

        /// <summary>
        /// 获取处理结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="isError"></param>
        /// <returns></returns>
        protected virtual Result<T> GetResult<T>(string code, T data, string message, bool isError = false)
        {
            var result = new Result<T>()
            {
                Code = code,
                Message = message,
                Data = data
            };

            if (isError)
            {
                Log.Error(message, new Exception() { Source = Environment.StackTrace });
            }

            return result;
        }

        /// <summary>
        /// 处理成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Result<T> GetSuccessResult<T>(T data, string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                Log.Info(message);
            }

            return GetResult<T>(HttpConst.STATUS_CODE_200, data, message);
        }

        /// <summary>
        /// 获取404无效处理结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Result<T> GetInvalidResult<T>(T data, string message)
        {
            return GetResult<T>(HttpConst.STATUS_CODE_404, data, message);
        }

        /// <summary>
        /// 服务异常处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Result<T> GetExceptionResult<T>(T data, Exception ex, string message = "")
        {
            if (ex != null)
            {
                Log.Error(ex);
                message = string.IsNullOrEmpty(ex.Message) ? message : ex.Message;
            }

            var result = new Result<T>()
            {
                Message = string.IsNullOrEmpty(message) ? "服务器处理异常" : message,
                Data = data
            };

            //if (ex is TvInvalidLoginException)
            if (ex is Exception)
            {
                result.Code = HttpConst.STATUS_CODE_601;
                result.Message = "请重新登录";
            }
            //else if (ex is TvNotEnoughBalanceException)
            else if (ex is Exception)
            {
                result.Code = HttpConst.STATUS_CODE_602;
                result.Message = "会员余额不足";
            }
            else
            {
                result.Code = HttpConst.STATUS_CODE_500;
            }

            return result;
        }
        
        /// <summary>
        /// 获取Web.config的配AppSetting项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string GetAppSetting(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                return ConfigurationManager.AppSettings[key];
            }

            return string.Empty;
        }
    }
}