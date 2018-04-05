using System;
using System.IO;
using System.Net;
using System.Text;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class HtmlOperate
	{
		public static string GetHtml(string Url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = "GET";
				httpWebRequest.UserAgent = "MSIE";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
				result = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
                new LogExceptionDAL().Save("采集异常", Url + "\r\n" + ex.Message);
			}
			return result;
		}

		public static string GetHtmlGB2132(string Url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = "GET";
				httpWebRequest.UserAgent = "MSIE";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);
				result = streamReader.ReadToEnd();
			}
			catch
			{
				new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
			}
			return result;
		}

		public static string GetHtmlGB2132_2(string Url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = "GET";
				httpWebRequest.UserAgent = "MSIE";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GBK"));
				result = streamReader.ReadToEnd();
			}
			catch
			{
				new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
			}
			return result;
		}
	}
}
