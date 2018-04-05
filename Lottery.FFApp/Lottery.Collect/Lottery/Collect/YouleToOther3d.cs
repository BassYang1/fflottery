using System;
using System.Configuration;
using System.Xml;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class YouleToOther3d
	{
		public static void DataToOther(int lotteryId)
		{
			try
			{
				string html = HtmlOperate.GetHtml(string.Format(Config.DefaultUrlYoule, lotteryId));
				if (string.IsNullOrEmpty(html))
				{
                    new LogExceptionDAL().Save("采集异常[YouleToOther3d]", "采集主站找不到开奖数据的关键字符1");
				}
				else
				{
					XmlNodeList xmlNode = Public.GetXmlNode(html, "row");
					if (xmlNode == null)
					{
                        new LogExceptionDAL().Save("采集异常[YouleToOther3d]", "采集主站找不到开奖数据的关键字符2");
					}
					else if (xmlNode.Count == 0)
					{
                        new LogExceptionDAL().Save("采集异常[YouleToOther3d]", "采集主站找不到开奖数据的关键字符3");
					}
					else
					{
						foreach (XmlNode xmlNode2 in xmlNode)
						{
							string innerText = xmlNode2.Attributes["opentime"].InnerText;
							string innerText2 = xmlNode2.Attributes["expect"].InnerText;
							string innerText3 = xmlNode2.Attributes["opencode"].InnerText;
							if (string.IsNullOrEmpty(innerText) || string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText3))
							{
                                new LogExceptionDAL().Save("采集异常[YouleToOther3d]", "采集主站找不到开奖数据的关键字符4");
								break;
							}
							bool flag = true;
							string text = ConfigurationManager.AppSettings["RootUrl"].ToString();
							string html2 = HtmlOperate.GetHtml(string.Concat(new object[]
							{
								text,
								"/Data/lottery",
								lotteryId,
								".xml"
							}));
							if (!string.IsNullOrEmpty(html2))
							{
								XmlNodeList xmlNode3 = Public.GetXmlNode(html2, "row");
								foreach (XmlNode xmlNode4 in xmlNode3)
								{
									string innerText4 = xmlNode4.Attributes["expect"].InnerText;
									if (innerText4.Equals(innerText2))
									{
										flag = false;
									}
								}
							}
							if (flag)
							{
								string text2 = innerText2;
								if (!new LotteryDataDAL().Exists(lotteryId, text2))
								{
									string[] array = innerText3.Split(new char[]
									{
										','
									});
									int num = (Convert.ToInt32(array[0]) + Convert.ToInt32(array[1]) + Convert.ToInt32(array[2]) + Convert.ToInt32(array[3]) + Convert.ToInt32(array[4]) + Convert.ToInt32(array[5]) + Convert.ToInt32(array[6])) % 10;
									int num2 = (Convert.ToInt32(array[7]) + Convert.ToInt32(array[8]) + Convert.ToInt32(array[9]) + Convert.ToInt32(array[10]) + Convert.ToInt32(array[11]) + Convert.ToInt32(array[12]) + Convert.ToInt32(array[13])) % 10;
									int num3 = (Convert.ToInt32(array[14]) + Convert.ToInt32(array[15]) + Convert.ToInt32(array[16]) + Convert.ToInt32(array[17]) + Convert.ToInt32(array[18]) + Convert.ToInt32(array[19])) % 10;
									string number = string.Concat(new object[]
									{
										num,
										",",
										num2,
										",",
										num3
									});
									new LotteryDataDAL().Add(lotteryId, text2, number, innerText, string.Join(",", array));
									Public.SetOpenListJson(lotteryId);
									LotteryCheck.RunOfIssueNum(lotteryId, text2);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("采集异常", "采集主站获取开奖数据出错，错误代码：" + ex.Message);
			}
		}
	}
}
