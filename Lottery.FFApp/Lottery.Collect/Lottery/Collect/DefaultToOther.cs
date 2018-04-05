using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using Lottery.DAL;
using Lottery.Utils;
using log4net;

namespace Lottery.Collect
{
	public class DefaultToOther
    {
        /// <summary>
        /// Log instance.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger(typeof(DefaultToOther));

        public static void CqSsc()
        {
            try
            {
                //Log.Debug("开始CqSsc...");
                string html = HtmlOperate.GetHtml(Config.DefaultUrl);
                //Log.Debug(html);
                if (!string.IsNullOrEmpty(html))
                {
                    XmlNodeList xmlNode = Public.GetXmlNode(html, "row");
                    if (xmlNode == null)
                    {
                        //Log.Debug("采集找不到开奖数据的关键字符2");
                        (new LogExceptionDAL()).Save("采集异常[DefaultToOther]", "采集找不到开奖数据的关键字符2");
                    }
                    else if (xmlNode.Count != 0)
                    {
                        foreach (XmlNode xmlNodes in xmlNode)
                        {
                            string innerText = xmlNodes.Attributes["code"].InnerText;
                            string str = xmlNodes.Attributes["expect"].InnerText;
                            string str1 = xmlNodes.Attributes["opencode"].InnerText.Replace("+", ",");
                            string innerText1 = xmlNodes.Attributes["opentime"].InnerText;
                            if (string.IsNullOrEmpty(innerText1) || string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str1))
                            {
                                //Log.Debug("采集找不到开奖数据的关键字符4");
                                (new LogExceptionDAL()).Save("采集异常[DefaultToOther]", "采集找不到开奖数据的关键字符4");
                                return;
                            }
                            else
                            {
                                bool flag = true;
                                string html1 = HtmlOperate.GetHtml(string.Concat(ConfigurationManager.AppSettings["RootUrl"].ToString(), "/Data/hisStory.xml"));
                                if (!string.IsNullOrEmpty(html1))
                                {
                                    foreach (XmlNode xmlNode1 in Public.GetXmlNode(html1, "row"))
                                    {
                                        string innerText2 = xmlNode1.Attributes["code"].InnerText;
                                        string str2 = xmlNode1.Attributes["expect"].InnerText;
                                        if (!innerText2.Equals(innerText) || !str2.Equals(str))
                                        {
                                            continue;
                                        }
                                        flag = false;
                                    }
                                }
                                if (!flag || str1.Contains("255"))
                                {
                                    continue;
                                }
                                string str3 = innerText;
                                switch (str3)
                                {
                                    case "cqssc":
                                        {
                                            string str4 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 9 || (new LotteryDataDAL()).Exists(1001, str4))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(1001, str4, str1, innerText1, "");
                                            Public.SetOpenListJson(1001);
                                            LotteryCheck.RunOfIssueNum(1001, str4);
                                            break;
                                        }
                                    case "xjssc":
                                        {
                                            string str5 = string.Concat(str.Substring(0, 8), "-", str.Substring(9));
                                            if (str1.Length != 9 || (new LotteryDataDAL()).Exists(1003, str5))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(1003, str5, str1, innerText1, "");
                                            Public.SetOpenListJson(1003);
                                            LotteryCheck.RunOfIssueNum(1003, str5);
                                            break;
                                        }
                                    case "tjssc":
                                        {
                                            string str6 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 9 || (new LotteryDataDAL()).Exists(1007, str6))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(1007, str6, str1, innerText1, "");
                                            Public.SetOpenListJson(1007);
                                            LotteryCheck.RunOfIssueNum(1007, str6);
                                            break;
                                        }
                                    case "twbingo":
                                        {
                                            if ((new LotteryDataDAL()).Exists(1013, str))
                                            {
                                                continue;
                                            }
                                            string[] strArrays = str1.Split(new char[] { ',' });
                                            int num = (Convert.ToInt32(strArrays[0]) + Convert.ToInt32(strArrays[1]) + Convert.ToInt32(strArrays[2]) + Convert.ToInt32(strArrays[3])) % 10;
                                            int num1 = (Convert.ToInt32(strArrays[4]) + Convert.ToInt32(strArrays[5]) + Convert.ToInt32(strArrays[6]) + Convert.ToInt32(strArrays[7])) % 10;
                                            int num2 = (Convert.ToInt32(strArrays[8]) + Convert.ToInt32(strArrays[9]) + Convert.ToInt32(strArrays[10]) + Convert.ToInt32(strArrays[11])) % 10;
                                            int num3 = (Convert.ToInt32(strArrays[12]) + Convert.ToInt32(strArrays[13]) + Convert.ToInt32(strArrays[14]) + Convert.ToInt32(strArrays[15])) % 10;
                                            int num4 = (Convert.ToInt32(strArrays[16]) + Convert.ToInt32(strArrays[17]) + Convert.ToInt32(strArrays[18]) + Convert.ToInt32(strArrays[19])) % 10;
                                            string str7 = string.Concat(new object[] { num, ",", num1, ",", num2, ",", num3, ",", num4 });
                                            (new LotteryDataDAL()).Add(1013, str, str7, innerText1, string.Join(",", strArrays));
                                            Public.SetOpenListJson(1013);
                                            LotteryCheck.RunOfIssueNum(1013, str);
                                            break;
                                        }
                                    case "sd11x5":
                                        {
                                            string str8 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 14 || (new LotteryDataDAL()).Exists(2001, str8))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(2001, str8, str1, innerText1, "");
                                            Public.SetOpenListJson(2001);
                                            LotteryCheck.RunOfIssueNum(2001, str8);
                                            break;
                                        }
                                    case "gd11x5":
                                        {
                                            string str9 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 14 || (new LotteryDataDAL()).Exists(2002, str9))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(2002, str9, str1, innerText1, "");
                                            Public.SetOpenListJson(2002);
                                            LotteryCheck.RunOfIssueNum(2002, str9);
                                            break;
                                        }
                                    case "sh11x5":
                                        {
                                            string str10 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 14 || (new LotteryDataDAL()).Exists(2003, str10))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(2003, str10, str1, innerText1, "");
                                            Public.SetOpenListJson(2003);
                                            LotteryCheck.RunOfIssueNum(2003, str10);
                                            break;
                                        }
                                    case "jx11x5":
                                        {
                                            string str11 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 14 || (new LotteryDataDAL()).Exists(2004, str11))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(2004, str11, str1, innerText1, "");
                                            Public.SetOpenListJson(2004);
                                            LotteryCheck.RunOfIssueNum(2004, str11);
                                            break;
                                        }
                                    case "krkeno":
                                        {
                                            if ((new LotteryDataDAL()).Exists(1017, str))
                                            {
                                                continue;
                                            }
                                            string[] strArrays1 = str1.Split(new char[] { ',' });
                                            int num5 = (Convert.ToInt32(strArrays1[0]) + Convert.ToInt32(strArrays1[1]) + Convert.ToInt32(strArrays1[2]) + Convert.ToInt32(strArrays1[3])) % 10;
                                            int num6 = (Convert.ToInt32(strArrays1[4]) + Convert.ToInt32(strArrays1[5]) + Convert.ToInt32(strArrays1[6]) + Convert.ToInt32(strArrays1[7])) % 10;
                                            int num7 = (Convert.ToInt32(strArrays1[8]) + Convert.ToInt32(strArrays1[9]) + Convert.ToInt32(strArrays1[10]) + Convert.ToInt32(strArrays1[11])) % 10;
                                            int num8 = (Convert.ToInt32(strArrays1[12]) + Convert.ToInt32(strArrays1[13]) + Convert.ToInt32(strArrays1[14]) + Convert.ToInt32(strArrays1[15])) % 10;
                                            int num9 = (Convert.ToInt32(strArrays1[16]) + Convert.ToInt32(strArrays1[17]) + Convert.ToInt32(strArrays1[18]) + Convert.ToInt32(strArrays1[19])) % 10;
                                            string str12 = string.Concat(new object[] { num5, ",", num6, ",", num7, ",", num8, ",", num9 });
                                            (new LotteryDataDAL()).Add(1017, str, str12, innerText1, string.Join(",", strArrays1));
                                            Public.SetOpenListJson(1017);
                                            LotteryCheck.RunOfIssueNum(1017, str);
                                            break;
                                        }
                                    case "xdlkl8":
                                        {
                                            string str13 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if ((new LotteryDataDAL()).Exists(1011, str13))
                                            {
                                                continue;
                                            }
                                            string[] strArrays2 = str1.Split(new char[] { ',' });
                                            int num10 = (Convert.ToInt32(strArrays2[0]) + Convert.ToInt32(strArrays2[1]) + Convert.ToInt32(strArrays2[2]) + Convert.ToInt32(strArrays2[3])) % 10;
                                            int num11 = (Convert.ToInt32(strArrays2[4]) + Convert.ToInt32(strArrays2[5]) + Convert.ToInt32(strArrays2[6]) + Convert.ToInt32(strArrays2[7])) % 10;
                                            int num12 = (Convert.ToInt32(strArrays2[8]) + Convert.ToInt32(strArrays2[9]) + Convert.ToInt32(strArrays2[10]) + Convert.ToInt32(strArrays2[11])) % 10;
                                            int num13 = (Convert.ToInt32(strArrays2[12]) + Convert.ToInt32(strArrays2[13]) + Convert.ToInt32(strArrays2[14]) + Convert.ToInt32(strArrays2[15])) % 10;
                                            int num14 = (Convert.ToInt32(strArrays2[16]) + Convert.ToInt32(strArrays2[17]) + Convert.ToInt32(strArrays2[18]) + Convert.ToInt32(strArrays2[19])) % 10;
                                            string str14 = string.Concat(new object[] { num10, ",", num11, ",", num12, ",", num13, ",", num14 });
                                            (new LotteryDataDAL()).Add(1011, str13, str14, innerText1, string.Join(",", strArrays2));
                                            Public.SetOpenListJson(1011);
                                            LotteryCheck.RunOfIssueNum(1011, str13);
                                            break;
                                        }
                                    case "phkeno":
                                        {
                                            if ((new LotteryDataDAL()).Exists(1015, str))
                                            {
                                                continue;
                                            }
                                            string[] strArrays3 = str1.Split(new char[] { ',' });
                                            int num15 = (Convert.ToInt32(strArrays3[0]) + Convert.ToInt32(strArrays3[1]) + Convert.ToInt32(strArrays3[2]) + Convert.ToInt32(strArrays3[3])) % 10;
                                            int num16 = (Convert.ToInt32(strArrays3[4]) + Convert.ToInt32(strArrays3[5]) + Convert.ToInt32(strArrays3[6]) + Convert.ToInt32(strArrays3[7])) % 10;
                                            int num17 = (Convert.ToInt32(strArrays3[8]) + Convert.ToInt32(strArrays3[9]) + Convert.ToInt32(strArrays3[10]) + Convert.ToInt32(strArrays3[11])) % 10;
                                            int num18 = (Convert.ToInt32(strArrays3[12]) + Convert.ToInt32(strArrays3[13]) + Convert.ToInt32(strArrays3[14]) + Convert.ToInt32(strArrays3[15])) % 10;
                                            int num19 = (Convert.ToInt32(strArrays3[16]) + Convert.ToInt32(strArrays3[17]) + Convert.ToInt32(strArrays3[18]) + Convert.ToInt32(strArrays3[19])) % 10;
                                            string str15 = string.Concat(new object[] { num15, ",", num16, ",", num17, ",", num18, ",", num19 });
                                            (new LotteryDataDAL()).Add(1015, str, str15, innerText1, string.Join(",", strArrays3));
                                            Public.SetOpenListJson(1015);
                                            LotteryCheck.RunOfIssueNum(1015, str);
                                            break;
                                        }
                                    case "bjpk10":
                                        {
                                            if ((new LotteryDataDAL()).Exists(4001, str))
                                            {
                                                continue;
                                            }
                                            if ((int)str1.Split(new char[] { ',' }).Length != 10)
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(4001, str, str1, innerText1, "");
                                            Public.SetOpenListJson(4001);
                                            LotteryCheck.RunOfIssueNum(4001, str);
                                            break;
                                        }
                                    default:
                                        {
                                            if (str3 != "shssl")
                                            {
                                                continue;
                                            }
                                            string str16 = string.Concat(str.Substring(0, 8), "-", str.Substring(8));
                                            if (str1.Length != 5 || (new LotteryDataDAL()).Exists(3001, str16))
                                            {
                                                continue;
                                            }
                                        (new LotteryDataDAL()).Add(3001, str16, str1, innerText1, "");
                                            Public.SetOpenListJson(3001);
                                            LotteryCheck.RunOfIssueNum(3001, str16);
                                            break;
                                        }
                                }
                            }
                        }
                        string str17 = string.Concat(ConfigurationManager.AppSettings["DataUrl"].ToString(), "hisStory.xml");
                        DirFile.CreateFolder(DirFile.GetFolderPath(false, str17));
                        StreamWriter streamWriter = new StreamWriter(str17, false, Encoding.UTF8);
                        streamWriter.Write(html);
                        streamWriter.Close();
                    }
                    else
                    {
                        //Log.Debug("采集找不到开奖数据的关键字符3");
                        (new LogExceptionDAL()).Save("采集异常", "采集找不到开奖数据的关键字符3");
                    }
                }
                else
                {
                    //Log.Debug("采集找不到开奖数据的关键字符1");
                    (new LogExceptionDAL()).Save("采集异常", "采集找不到开奖数据的关键字符1");
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("采集SSC异常 {0}", ex);
                (new LogExceptionDAL()).Save("采集异常", string.Concat("采集获取开奖数据出错，错误代码111：", ex.Message));
            }
        }
    }
}
