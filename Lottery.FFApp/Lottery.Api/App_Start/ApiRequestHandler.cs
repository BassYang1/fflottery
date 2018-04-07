using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Configuration;
using Lottery.FFModel;
using System.ComponentModel;
using Lottery.Service;
using Lottery.Service.Interfaces;
using Lottery.Core;

namespace Lottery.Api
{
    /// <summary>
    /// API 请求处理器
    /// </summary>
    public class ApiRequestHandler : DelegatingHandler
    {
        private static readonly object Locker = new object();

        /// <summary>
        /// 会员处理
        /// </summary>
        private IUserService _userService = new UserService();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ApiRequestHandler()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accountService"></param>
        public ApiRequestHandler()
        {
        }

        /// <summary>
        /// 检查用户登录状态，并以异步的方式请请求传送给下一个Http Request Handler
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
                return Task.Run<HttpResponseMessage>(() =>
                {
                    var resp = new HttpResponseMessage();
                    resp.Content = new StringContent("");
                    resp.Content.Headers.Add("Access-Control-Allow-Origin", GetHeaderByKey(request, "Origin"));
                    resp.Content.Headers.Add("Access-Control-Allow-Methods", "GET, PUT, POST, DELETE");
                    resp.Content.Headers.Add("Access-Control-Allow-Headers", "token,Content-Type,X-Requested-With");
                    return resp;
                });
            }
            
            try
            {
                var token = GetHeaderByKey(request, "Token");

                // check token except  register & login & resetpassword
                if (request.RequestUri != null && IsApiTokenChecked(request.RequestUri.ToString()))
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new InvalidCastException("会员登录Token无效");
                    }

                    //获取用户详细
                    var user = _userService.GetUserDetailByToken(token);

                    if (user == null)
                    {
                        throw new InvalidCastException("会员登录Token已过期");
                    }
                }
            }
            catch (Exception ex)
            {
                return Task.Run<HttpResponseMessage>(() =>
                {
                    return GenerateResponse(request, HttpStatusCode.Unauthorized, "会员登录Token无效", ex.Message);
                });
            }

            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 从 HttpHeader 获取对应key 的 值
        /// </summary>
        /// <param name="request">Request 对象</param>
        /// <param name="key">Http Header 的 Key</param>
        /// <returns>值</returns>
        private static string GetHeaderByKey(HttpRequestMessage request, string key)
        {
            string value = null;

            try
            {
                value = request.Headers.GetValues(key).FirstOrDefault();
            }
            catch
            {
            }

            return value;
        }

        /// <summary>
        /// 生成对应 Request 的 Response
        /// </summary>
        /// <param name="request">Request 对象</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="detail">具体消息</param>
        /// <returns>Response 对象 </returns>
        private HttpResponseMessage GenerateResponse(HttpRequestMessage request, HttpStatusCode statusCode,
            string message, string detail)
        {
            var response = request.CreateResponse<Result<string>>(statusCode, new Result<string>
            {
                Code = (statusCode == HttpStatusCode.Unauthorized ? "500" : statusCode.ToString()),
                Data = detail,
                Message = message
            });

            response.Content.Headers.Add("Access-Control-Allow-Origin", GetHeaderByKey(request, "Origin"));

            return response;
        }

        /// <summary>
        /// 是否是需要Token验证的API
        /// </summary>
        /// <param name="apiUri"></param>
        /// <returns></returns>
        private bool IsApiTokenChecked(string apiUri)
        {
            string[] ignoredApis =
            {
                "v1/user/login",
                "v1/user/regiter"
            };

            for (int i = 0; i < ignoredApis.Length; i++)
            {
                if (apiUri.Contains(ignoredApis[i]))
                {
                    return false;
                }
            }

            return true;
        }

    }
}