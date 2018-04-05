using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Lottery.DAL;
using Lottery.Utils;
using log4net;

namespace Lottery.Collect
{
    public class Public
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(Public));

		public static string GetJson(string loid)
		{
			Random random = new Random();
			string arg = ConfigurationManager.AppSettings["CollectUrl"].ToString();
			string text = HtmlOperate.GetHtml(arg + "/Data/hisStory.xml?" + random.Next(1, 1000));
			if (!string.IsNullOrEmpty(text))
			{
				XmlNodeList xmlNode = Public.GetXmlNode(text, "row");
				foreach (XmlNode xmlNode2 in xmlNode)
				{
					string innerText = xmlNode2.Attributes["code"].InnerText;
					string text2 = xmlNode2.Attributes["expect"].InnerText;
					string text3 = xmlNode2.Attributes["opencode"].InnerText.Replace("+", ",");
					switch (loid)
					{
					case "1001":
						if ("cqssc".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "1003":
						if ("xjssc".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(9);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "1007":
						if ("tjssc".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "1008":
						if ("ynssc".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "2001":
						if ("sd11x5".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "2002":
						if ("gd11x5".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "2003":
						if ("sh11x5".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "2004":
						if ("jx11x5".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					case "1010":
						if ("krkeno".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							string[] array = text3.Split(new char[]
							{
								','
							});
							int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
							int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
							int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
							int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
							int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
							string text4 = string.Concat(new object[]
							{
								num2,
								",",
								num3,
								",",
								num4,
								",",
								num5,
								",",
								num6
							});
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text4,
								"\"}]"
							});
						}
						break;
					case "1012":
						if ("sgkeno".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							string[] array = text3.Split(new char[]
							{
								','
							});
							int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
							int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
							int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
							int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
							int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
							string text4 = string.Concat(new object[]
							{
								num2,
								",",
								num3,
								",",
								num4,
								",",
								num5,
								",",
								num6
							});
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text4,
								"\"}]"
							});
						}
						break;
					case "1013":
						if ("twbingo".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							string[] array = text3.Split(new char[]
							{
								','
							});
							int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3])) % 10;
							int num3 = (Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6]) + Convert.ToInt32(array[7])) % 10;
							int num4 = (Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11])) % 10;
							int num5 = (Convert.ToInt32(array[12]) + Convert.ToInt32(array[13]) + Convert.ToInt32(array[14]) + Convert.ToInt32(array[15])) % 10;
							int num6 = (Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
							string text4 = string.Concat(new object[]
							{
								num2,
								",",
								num3,
								",",
								num4,
								",",
								num5,
								",",
								num6
							});
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text4,
								"\"}]"
							});
						}
						break;
					case "3004":
						if ("krkeno".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							string[] array = text3.Split(new char[]
							{
								','
							});
							int num2 = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6])) % 10;
							int num3 = (Convert.ToInt32(array[7]) + Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11]) + Convert.ToInt32(array[12]) + Convert.ToInt32(array[13])) % 10;
							int num4 = (Convert.ToInt32(array[14]) + Convert.ToInt32(array[15]) + Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
							string text4 = string.Concat(new object[]
							{
								num2,
								",",
								num3,
								",",
								num4
							});
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text4,
								"\"}]"
							});
						}
						break;
					case "3001":
						if ("shssl".Equals(xmlNode2.Attributes["code"].InnerText))
						{
							text2 = text2.Substring(0, 8) + "-" + text2.Substring(8);
							text = string.Concat(new string[]
							{
								"[{\"title\": \"",
								text2,
								"\",\"number\": \"",
								text3,
								"\"}]"
							});
						}
						break;
					}
				}
			}
			return text;
		}

		public static string GetBetRankJson()
		{
			Random random = new Random();
			string arg = ConfigurationManager.AppSettings["CollectUrl"].ToString();
			return HtmlOperate.GetHtml(arg + "/Data/BetRank.xml?" + random.Next(1, 1000));
		}

		public static void SetOpenListJson(int lotteryId)
		{
            
            try
            {
                //LogHelp.Debug("SetOpenListJson", "1[" + lotteryId + "]");
                string value = "";
                string value2 = "";
                new LotteryDataDAL().GetListJSON(lotteryId, ref value2, ref value);
                string path = ConfigurationManager.AppSettings["DataUrl"].ToString();
                string openListFile = string.Format("{0}OpenList{1}.xml", path, lotteryId);

                //LogHelp.Debug("SetOpenListJson", "text2[" + text2 + "]");
                DirFile.CreateFolder(DirFile.GetFolderPath(false, openListFile));
                StreamWriter streamWriter = new StreamWriter(openListFile, false, Encoding.UTF8);
                streamWriter.Write(value2);
                streamWriter.Close();
                string lotteryFile = string.Format("{0}lottery{1}.xml", path, lotteryId);

                //LogHelp.Debug("SetOpenListJson", "text3[" + text3 + "]");
                DirFile.CreateFolder(DirFile.GetFolderPath(false, lotteryFile));
                StreamWriter streamWriter2 = new StreamWriter(lotteryFile, false, Encoding.UTF8);
                streamWriter2.Write(value);
                streamWriter2.Close();
                //LogHelp.Debug("SetOpenListJson", "完成[" + lotteryId + "]");
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集异常 {0}", ex);
            }
		}

        /// <summary>
        /// 存储彩票数据到磁盘
        /// </summary>
        /// <param name="lotteryId">彩票类型Id</param>
        public static void SaveLotteryData2File(int lotteryId)
        {
            try
            {
                //获取json和xml字符串
                string xmlData = "";
                string jsonData = "";
                new LotteryDataDAL().ConvertLotteryDataToStr(lotteryId, ref jsonData, ref xmlData);

                //保存json字符串到文件
                string path = ConfigurationManager.AppSettings["DataUrl"].ToString();
                string openListFile = string.Format("{0}OpenList{1}.xml", path, lotteryId);
                DirFile.CreateFolder(DirFile.GetFolderPath(false, openListFile));
                StreamWriter streamWriter = new StreamWriter(openListFile, false, Encoding.UTF8);
                streamWriter.Write(jsonData);
                streamWriter.Close();

                //保存xml字符串到文件
                string lotteryFile = string.Format("{0}lottery{1}.xml", path, lotteryId);
                DirFile.CreateFolder(DirFile.GetFolderPath(false, lotteryFile));
                StreamWriter streamWriter2 = new StreamWriter(lotteryFile, false, Encoding.UTF8);
                streamWriter2.Write(xmlData);
                streamWriter2.Close();
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集异常 {0}", ex);
            }
        }

        public static string GetOpenListJson(int lotteryId)
        {
            string host = ConfigurationManager.AppSettings["CollectUrl"].ToString();
            string url = string.Format("{0}/Data/OpenList{1}.xml", host, lotteryId);

            string result = string.Empty;

            try
            {
                result = HtmlOperate.GetHtml(url);
            }
            catch { }

            return result;
        }

		public static string GetUserJson(int UserId)
		{
			string text = ConfigurationManager.AppSettings["CollectUrl"].ToString();
			text = string.Concat(new object[]
			{
				text,
				"/Data/User/User",
				UserId,
				".xml"
			});
			string result;
			if (Public.RemoteFileExists(text))
			{
				string html = HtmlOperate.GetHtml(text);
				result = html;
			}
			else
			{
				result = "";
			}
			return result;
		}

		public static XmlNodeList GetXmlNode(string shtml, string rootElm)
		{
			XmlNodeList result = null;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(shtml);
				result = xmlDocument.ChildNodes.Item(1).SelectNodes(rootElm);
			}
			catch
			{
			}
			return result;
		}

		public static bool RemoteFileExists(string fileUrl)
		{
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse httpWebResponse = null;
			bool result;
			try
			{
				httpWebRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
				httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				if (httpWebResponse.ContentLength != 0L)
				{
					result = true;
					return result;
				}
			}
			catch (Exception)
			{
				result = false;
				return result;
			}
			finally
			{
				if (httpWebRequest != null)
				{
					httpWebRequest.Abort();
				}
				if (httpWebResponse != null)
				{
					httpWebResponse.Close();
				}
			}
			result = false;
			return result;
		}
	}
}
