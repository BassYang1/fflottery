using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Lottery.DAL.Flex;
using System.Web;
using Lottery.Entity;

namespace Lottery.IPhone.SBF
{
    public static class SbfHelper
    {
        /// <summary>
        /// 随笔付商户Id
        /// </summary>
        public static String SBF_USER = ConfigurationManager.AppSettings["SbfUser"];

        /// <summary>
        /// 随笔付商户密钥
        /// </summary>
        public static String SBF_USER_KEY = ConfigurationManager.AppSettings["SbfUserKey"];

        /// <summary>
        /// 随笔付API
        /// </summary>
        public static String SBF_API= ConfigurationManager.AppSettings["SbfApi"];

        /// <summary>
        /// 格式化字符串空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Trim(string str)
        {
            return str == null ? "" : str.Trim();
        }

        /// <summary>
        /// 获取随笔付支付方式
        /// </summary>
        /// <param name="sysCode">平台支付方式编码</param>
        /// <returns></returns>
        public static ChannelMapModel GetChannelMap(string sysCode)
        {
            SbfDAL dal = new SbfDAL();
            IList<ChannelMapModel> channels = dal.GetSbfChannels();

            foreach (ChannelMapModel map in channels)
            {
                if (map != null && map.SysCode.Equals(sysCode, StringComparison.OrdinalIgnoreCase))
                {
                    return map;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取页面地址
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string GetUrl(string page)
        {
            string scheme = HttpContext.Current.Request.Url.Scheme;
            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;
            string url = string.Empty;
            string[] segments = HttpContext.Current.Request.Url.Segments;
            for(int i =0;i < segments.Length - 1; i ++ ){
                url = url + segments[i];
            }

            return string.Format("{0}://{1}:{2}{3}{4}", scheme, host, port, url, page);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 搜集请求参数
        /// </summary>
        /// <returns></returns>
        public static string GetRequestData()
        {
            StringBuilder parStr = new StringBuilder();

            //QueryString
            var pars = HttpContext.Current.Request.QueryString;

            foreach (var key in pars.AllKeys)
            {
                parStr.AppendFormat("&{0}={1}", key, pars[key]);
            }

            //Form
            pars = HttpContext.Current.Request.Form;

            foreach (var key in pars.AllKeys)
            {
                parStr.AppendFormat("&{0}={1}", key, pars[key]);
            }

            if (parStr.Length <= 0)
            {
                return string.Empty;
            }

            return parStr.ToString().Substring(1);
        }
    }
}
