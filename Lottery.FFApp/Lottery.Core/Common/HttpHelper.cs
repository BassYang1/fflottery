using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using log4net;
using Microsoft.JScript;

namespace Lottery.Core
{
    public class HttpHelper
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(HttpHelper));

        /// <summary>
        /// 检查连接是否有效
        /// </summary>
        /// <param name="host">域</param>
        /// <param name="url">连接</param>
        /// <returns></returns>
        public static string CheckUrl(string host, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            var temp = host.Split(new[] { "//" }, StringSplitOptions.None);

            if (temp.Length >= 2)
            {
                host = string.Format("{0}//{1}", temp[0], temp[1]);
            }

            if (host.EndsWith("/"))
            {
                host = host.Substring(0, host.Length - 1);
            }

            temp = url.Split('?');

            var queryString = string.Empty;
            if (temp.Length >= 2)
            {
                queryString = temp[1];
            }

            temp = temp[0].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            url = string.Empty;

            foreach (var path in temp)
            {
                url = string.Format("{0}{1}/", url, path);
            }

            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            url = string.Format("{0}/{1}", host, url);

            if (!string.IsNullOrEmpty(queryString))
            {
                url = string.Format("{0}?{1}", url, queryString);
            }

            return url;
        }

        /// <summary>
        /// 获取页面Html字符串
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Get(string url, int timeout = 30000)
        {
            try
            {
                Log.Debug("========================request========================");
                Log.Debug(url);

                var request = WebRequest.Create(url) as HttpWebRequest;

                if (request == null)
                {
                    throw new NullReferenceException(string.Format("请求服务器异常: {0}", url));
                }

                request.Method = "GET";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; Maxthon 2.0)";
                request.Timeout = timeout;

                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    throw new NullReferenceException("读取响应数据异常");
                }

                var streamReader = new StreamReader(stream, Encoding.UTF8);
                var result = streamReader.ReadToEnd();
                streamReader.Close();
                stream.Close();

                Log.Debug("========================response========================");
                Log.Debug(result);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(url);
                Log.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// 获取页面Html字符串
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="headers">头部信息</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string Get(string url, NameValueCollection headers, int timeout = 10000)
        {
            try
            {
                Log.Debug("========================request========================");
                Log.Debug(url);

                var request = WebRequest.Create(url) as HttpWebRequest;

                if (request == null)
                {
                    throw new NullReferenceException(string.Format("请求服务器异常: {0}", url));
                }

                request.Method = "GET";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; Maxthon 2.0)";
                request.Timeout = timeout;
                request.Headers.Add(headers);

                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    throw new NullReferenceException("读取响应数据异常");
                }

                var streamReader = new StreamReader(stream, Encoding.UTF8);
                var result = streamReader.ReadToEnd();
                streamReader.Close();
                stream.Close();

                Log.Debug("========================response========================");
                Log.Debug(result);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(url);
                Log.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// 获取页面Html字符串
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string Post(string url, string content, string contentType = "application/x-www-form-urlencoded")
        {
            try
            {
                Log.Debug("========================request========================");
                Log.Debug(url);

                var request = WebRequest.Create(url) as HttpWebRequest;

                if (request == null)
                {
                    throw new NullReferenceException(string.Format("请求服务器异常: {0}", url));
                }

                byte[] contentBytes = Encoding.UTF8.GetBytes(content);

                request.ContentType = contentType;
                request.ContentLength = contentBytes.Length;
                request.Method = "POST";
                request.Timeout = 20000;

                var requestStream = request.GetRequestStream();
                requestStream.Write(contentBytes, 0, contentBytes.Length);
                requestStream.Close();

                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();

                if (stream == null)
                {
                    throw new NullReferenceException("读取响应数据异常");
                }

                var streamReader = new StreamReader(stream, Encoding.UTF8);
                var result = streamReader.ReadToEnd();
                streamReader.Close();
                stream.Close();

                Log.Debug(content);
                Log.Debug("========================response========================");
                Log.Debug(result);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(url);
                Log.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// 获取Json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T GetJson<T>(string url)
        {
            string jsonStr = Get(url);

            if (string.IsNullOrEmpty(jsonStr))
            {
                Log.Error("获取服务器数据失败");
                jsonStr = string.Empty;
            }

            Log.Debug("========================request========================");
            Log.Debug(url);
            Log.Debug("========================response========================");
            Log.Debug(jsonStr);

            return Commonn.JsonDeserialize<T>(jsonStr);
        }

        /// <summary>
        /// 读取上传文件内容
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="index">文件索引</param>
        /// <returns>上文件内容</returns>
        public static byte[] GetUploadedFile(out string fileName, int index = 0)
        {
            fileName = string.Empty;

            if (HttpContext.Current.Request.Files.Count > index)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[index];
                fileName = file.FileName;
                Stream fileStream = file.InputStream;
                var buffer = new byte[file.ContentLength];
                fileStream.Read(buffer, 0, file.ContentLength);

                return buffer;
            }

            return null;
        }

        /// <summary>
        /// 获取附件文件响应
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <returns></returns>
        public static HttpResponseMessage DownloadFile(byte[] content)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            if (content != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(content);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }

            return response;
        }

        /// <summary>
        /// 对Url进行UTF8编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EncodeUrl(string url)
        {
            return HttpUtility.UrlEncode(url, Encoding.UTF8);
        }

        /// <summary>
        /// 创建新的cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expireDate">过期日期</param>
        public static void NewCookie(string name, string value, DateTime expireDate)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            if (HttpContext.Current.Request.Cookies[name] != null)
            {
                //cookie = HttpContext.Current.Request.Cookies[name];
                //HttpContext.Current.Request.Cookies.Remove(name);
                HttpContext.Current.Response.Cookies.Remove(name);
            }

            var cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = expireDate;
            //cookie.HttpOnly = true;
            //cookie.Secure = true;
            cookie.Path = "/";

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 清除cookie的cookie
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveCookie(string name)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request.Cookies[name] != null)
            {
                var cookie = new HttpCookie(name);
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        /// <summary>
        /// 获取cookie的cookie
        /// </summary>
        /// <param name="name"></param>
        public static object GetCookie(string name)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request.Cookies[name] != null)
            {
                return HttpContext.Current.Request.Cookies[name].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取Url字符串
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string GetUrlString(SortedDictionary<string, object> items)
        {
            string urlString = string.Empty;

            items.ToList().ForEach(item => urlString = string.Format("{0}{1}={2}&", urlString, item.Key, item.Value));

            return urlString.Trim('&');
        }

        /// <summary>
        /// 从 HttpHeader 获取对应key 的 值
        /// </summary>
        /// <param name="request">Request 对象</param>
        /// <param name="key">Http Header 的 Key</param>
        /// <returns>值</returns>
        public static string GetHeaderByKey(HttpRequestMessage request, string key)
        {
            string value;

            try
            {
                var values = request.Headers.GetValues(key);
                value = values.Any() ? values.FirstOrDefault() : string.Empty;
            }
            catch
            {
                value = string.Empty;
            }

            return value;
        }

        /// <summary>
        /// 从 HttpHeader 获取对应key 的 值
        /// </summary>
        /// <param name="request">Request 对象</param>
        /// <param name="key">Http Header 的 Key</param>
        /// <returns>值</returns>
        public static string GetReferrer(HttpRequestMessage request)
        {
            try
            {
                var referrer = request.Headers.Referrer;

                if (referrer != null)
                {
                    return request.Headers.Referrer.ToString();
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}