using System;
using System.IO;
using System.Net;
using System.Text;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class HtmlOperate2
	{
		public static string GetHtml(string Url)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
				httpWebRequest.Method = "GET";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
				result = streamReader.ReadToEnd();
				responseStream.Close();
			}
			catch
			{
				new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
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
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);
				result = streamReader.ReadToEnd();
				responseStream.Close();
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
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				Stream responseStream = httpWebResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
				result = streamReader.ReadToEnd();
				responseStream.Close();
			}
			catch
			{
				new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
			}
			return result;
		}

		public static string HtmlToJs(string source)
		{
			return string.Format("document.writeln(\"{0}\");", string.Join("\");\r\ndocument.writeln(\"", source.Replace("\\", "\\\\").Replace("/", "\\/").Replace("'", "\\'").Replace("\"", "\\\"").Split(new char[]
			{
				'\r',
				'\n'
			}, StringSplitOptions.RemoveEmptyEntries)));
		}

		public static string HttpGet(string url, Encoding enc)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Timeout = 10000;
			httpWebRequest.Proxy = null;
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "application/x-www-from-urlencoded";
			WebResponse response = httpWebRequest.GetResponse();
			StreamReader streamReader = new StreamReader(response.GetResponseStream(), enc);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(streamReader.ReadToEnd());
			streamReader.Close();
			streamReader.Dispose();
			response.Close();
			return stringBuilder.ToString();
		}
	}
}
