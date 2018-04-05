using System;
using System.Xml;
using Lottery.DAL;

namespace Lottery.Collect
{
	public class Fc3dData
	{
		public static void Fc3d()
		{
			try
			{
				string html = HtmlOperate.GetHtml("http://f.apiplus.net/fc3d.xml");
				if (string.IsNullOrEmpty(html))
				{
					new LogExceptionDAL().Save("采集异常", "3D找不到开奖数据的关键字符");
				}
				else
				{
					XmlNodeList xmlNode = Public.GetXmlNode(html, "row");
					if (xmlNode == null)
					{
						new LogExceptionDAL().Save("采集异常", "3D找不到开奖数据的关键字符");
					}
					else if (xmlNode.Count == 0)
					{
						new LogExceptionDAL().Save("采集异常", "3D找不到开奖数据的关键字符");
					}
					else
					{
						foreach (XmlNode xmlNode2 in xmlNode)
						{
							string innerText = xmlNode2.Attributes["opentime"].InnerText;
							string text = xmlNode2.Attributes["expect"].InnerText;
							string innerText2 = xmlNode2.Attributes["opencode"].InnerText;
							if (string.IsNullOrEmpty(innerText) || string.IsNullOrEmpty(text) || string.IsNullOrEmpty(innerText2))
							{
								new LogExceptionDAL().Save("采集异常", "3D找不到开奖数据的关键字符");
								break;
							}
							if (text.Length == 5)
							{
								text = "20" + text;
							}
							string text2 = text;
							if (innerText2.Length == 5)
							{
								if (!new LotteryDataDAL().Exists(3002, text2))
								{
									if (!innerText2.Contains("255"))
									{
                                        new LotteryDataDAL().Add(3002, text2, innerText2, DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00", innerText2);
										Public.SetOpenListJson(3002);
										LotteryCheck.RunOfIssueNum(3002, text2);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				new LogExceptionDAL().Save("采集异常", "3D获取开奖数据出错，错误代码：" + ex.Message);
			}
		}
	}
}
