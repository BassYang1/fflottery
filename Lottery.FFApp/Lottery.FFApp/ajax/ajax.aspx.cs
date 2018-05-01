// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajax
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.Collect;
using Lottery.DAL;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;

namespace Lottery.WebApp
{
    public partial class ajax : UserCenterSession
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = this.q("oper");
            switch (this._operType)
            {
                case "ajaxLotteryTimeIndex":
                    this.ajaxLotteryTimeIndex();
                    break;
                case "ajaxCheckLottery":
                    this.ajaxCheckLottery();
                    break;
                case "ajaxCheckLogin":
                    this.ajaxCheckLogin();
                    break;
                case "ajaxAllLottery":
                    this.ajaxAllLottery();
                    break;
                case "ajaxIndexLottery":
                    this.ajaxIndexLottery();
                    break;
                case "ajaxLotteryTime":
                    this.ajaxLotteryTime();
                    break;
                case "ajaxUserInfo":
                    this.ajaxUserInfo();
                    break;
                case "checkusername":
                    this.ajaxCheckUserName();
                    break;
                case "ajaxPopInfo":
                    this.ajaxPopInfo();
                    break;
                case "ajaxRegister":
                    this.ajaxRegister();
                    break;
                case "login":
                    this.ajaxLogin();
                    break;
                case "getpwd":
                    this.ajaxGetPwd();
                    break;
                case "logout":
                    this.ajaxLogout();
                    break;
                case "ajaxLotteryTime23":
                    this.ajaxLotteryTime23();
                    break;
                case "GetListLotteryData":
                    this.GetListLotteryData();
                    break;
                case "GetKaiJiangList":
                    this.GetKaiJiangList();
                    break;
                case "GetKaiJiangInfo":
                    this.GetKaiJiangInfo();
                    break;
                case "GetLotteryNumber":
                    this.GetLotteryNumber();
                    break;
                case "GetIndexWinInfo":
                    this.GetIndexWinInfo();
                    break;
                default:
                    this.DefaultResponse();
                    break;
            }
            this.Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this.Admin_Load("", "json");
            this._response = this.JsonResult(1, "成功登录");
        }

        public void GetLotteryNumber()
        {
            this._response = "{\"result\":\"1\",\"table\": [" + Public.GetOpenListJson(Convert.ToInt32(this.q("lid"))).Replace("[", "").Replace("]", "").ToLower() + "]}";
        }

        private void ajaxLotteryTimeIndex()
        {
            string newValue1 = this.q("lid");
            string str1 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"opennum\": \"已开期数\",\"cursn\": \"当前期号\",\"number\": \"开奖号码\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("彩种类别", newValue1);
            DateTime dateTime1 = DateTime.Now;
            DateTime dateTime2 = this.GetDateTime();
            string str2 = dateTime2.ToString("yyyyMMdd");
            string str3 = dateTime2.ToString("HH:mm:ss");
            dateTime2.ToString("yyyy-MM-dd");
            this.doh.Reset();
            this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + newValue1 + ") as closetime";
            DataTable dataTable1 = this.doh.GetDataTable();
            string str4 = str1.Replace("封单时间", dataTable1.Rows[0]["closetime"].ToString());
            if (UserCenterSession.LotteryTime == null)
                UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
            DataRow[] dataRowArray1 = UserCenterSession.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + newValue1, "Time asc");
            string newValue2;
            if (dataRowArray1.Length == 0)
            {
                dataRowArray1 = UserCenterSession.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + newValue1, "Time asc");
                newValue2 = dateTime2.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
            }
            else
            {
                newValue2 = str2 + "-" + dataRowArray1[0]["Sn"].ToString();
                dateTime1 = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString());
            }
            if (Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()) < Convert.ToDateTime(str3))
                dateTime1 = Convert.ToDateTime(dateTime2.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRowArray1[0]["Time"].ToString());
            TimeSpan timeSpan = dateTime1 - Convert.ToDateTime(str3);
            DataRow[] dataRowArray2 = UserCenterSession.LotteryTime.Select("Time <'" + str3 + "' and LotteryId=" + newValue1, "Time desc");
            string newValue3;
            string str5;
            if (dataRowArray2.Length == 0)
            {
                DataRow[] dataRowArray3 = UserCenterSession.LotteryTime.Select("LotteryId=" + newValue1, "Time desc");
                newValue3 = dateTime2.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray3[0]["Sn"].ToString();
                str5 = dataRowArray3[0]["Sn"].ToString();
            }
            else
            {
                newValue3 = str2 + "-" + dataRowArray2[0]["Sn"].ToString();
                str5 = dataRowArray2[0]["Sn"].ToString();
            }
            this.doh.Reset();
            this.doh.SqlCmd = string.Format("select top 1 Number from Sys_LotteryData where Type={0} and Title='{1}'", (object)newValue1, (object)newValue3);
            DataTable dataTable2 = this.doh.GetDataTable();
            string newValue4 = string.Concat((object)(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
            string str6 = str4.Replace("下期期号", newValue2).Replace("当前期号", newValue3).Replace("倒计时", newValue4).Replace("已开期数", Convert.ToInt32(str5).ToString());
            this._response = dataTable2.Rows.Count <= 0 ? str6.Replace("开奖号码", "正,在,开,奖,中") : str6.Replace("开奖号码", string.Concat(dataTable2.Rows[0]["Number"]));
        }

        private void ajaxCheckLottery()
        {
            string str = this.q("Code");
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
            {
                this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
                this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
                if (this.AdminId != "0")
                {
                    this.doh.Reset();
                    this.doh.ConditionExpress = "IsDel=0 and IsEnable=0 and id=@id and sessionId=@cookiess";
                    this.doh.AddConditionParameter("@id", (object)this.AdminId);
                    this.doh.AddConditionParameter("@cookiess", (object)this.AdminCookiess);
                    if (this.doh.Count("N_User") > 0)
                    {
                        this.doh.Reset();
                        this.doh.ConditionExpress = "Code=@Code and IsOpen=0";
                        this.doh.AddConditionParameter("@Code", (object)str);
                        if (this.doh.Count("Sys_Lottery") > 0)
                            this._response = this.JsonResult(1, "账号在线");
                        else
                            this._response = this.JsonResult(-1, "账号不在线");
                    }
                    else
                        this._response = this.JsonResult(0, "账号不在线");
                }
                else
                    this._response = this.JsonResult(0, "账号不在线");
            }
            else
                this._response = this.JsonResult(0, "账号不在线");
        }

        private void ajaxCheckLogin()
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
            {
                this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
                this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
                if (this.AdminId != "0")
                {
                    this.doh.Reset();
                    this.doh.ConditionExpress = "IsDel=0 and IsEnable=0 and id=@id and sessionId=@cookiess";
                    this.doh.AddConditionParameter("@id", (object)this.AdminId);
                    this.doh.AddConditionParameter("@cookiess", (object)this.AdminCookiess);
                    if (this.doh.Count("N_User") > 0)
                        this._response = this.JsonResult(1, "账号在线");
                    else
                        this._response = this.JsonResult(0, "账号不在线");
                }
                else
                    this._response = this.JsonResult(0, "账号不在线");
            }
            else
                this._response = this.JsonResult(0, "账号不在线");
        }

        private void ajaxAllLottery()
        {
            string str1 = "";
            this.doh.Reset();
            this.doh.SqlCmd = "select * from [Sys_Lottery] where id in (1001,1004,1005,1010,1016,2001,3002,4001) order by Id asc";
            DataTable dataTable1 = this.doh.GetDataTable();
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
            {
                string str2 = dataTable1.Rows[index]["Id"].ToString();
                string str3 = "{\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"code\": \"代码\",\"ordertime\": \"倒计时\",\"remark\": \"说明\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(str2))).Replace("类别Id", dataTable1.Rows[index]["LType"].ToString()).Replace("代码", dataTable1.Rows[index]["Code"].ToString()).Replace("彩种Id", str2).Replace("说明", dataTable1.Rows[index]["IphoneRemark"].ToString());
                DateTime dateTime1 = DateTime.Now;
                DateTime dateTime2 = this.GetDateTime();
                string str4 = dateTime2.ToString("yyyyMMdd");
                string str5 = dateTime2.ToString("HH:mm:ss");
                dateTime2.ToString("yyyy-MM-dd");
                this.doh.Reset();
                this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + str2 + ") as closetime";
                DataTable dataTable2 = this.doh.GetDataTable();
                string str6 = str3.Replace("封单时间", dataTable2.Rows[0]["closetime"].ToString());
                DateTime dateTime3;
                string newValue1;
                string newValue2;
                TimeSpan timeSpan;
                if (str2 == "3002" || str2 == "3003")
                {
                    DateTime dateTime4 = Convert.ToDateTime(dateTime2.Year.ToString() + "-01-01 20:30:00");
                    this.doh.Reset();
                    this.doh.SqlCmd = "select datediff(d,'" + dateTime4.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTime2.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                    int Num = Convert.ToInt32(this.doh.GetDataTable().Rows[0]["d"]) - 7 + 1;
                    dateTime3 = dateTime2.AddDays(-1.0);
                    string str7 = dateTime3.ToString("yyyy-MM-dd") + " 20:30:00";
                    string str8 = dateTime2.ToString("yyyy-MM-dd") + " 20:30:00";
                    if (dateTime2 > Convert.ToDateTime(dateTime2.ToString(" 20:30:00")))
                    {
                        dateTime3 = dateTime2.AddDays(1.0);
                        str8 = dateTime3.ToString("yyyy-MM-dd") + " 20:30:00";
                    }
                    else
                        --Num;
                    newValue1 = dateTime2.Year.ToString() + Func.AddZero(Num, 3);
                    newValue2 = dateTime2.Year.ToString() + Func.AddZero(Num + 1, 3);
                    timeSpan = Convert.ToDateTime(str8) - Convert.ToDateTime(str5);
                }
                else
                {
                    if (UserCenterSession.LotteryTime == null)
                        UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
                    DataRow[] dataRowArray1 = UserCenterSession.LotteryTime.Select("Time >'" + str5 + "' and LotteryId=" + str2, "Time asc");
                    if (dataRowArray1.Length == 0)
                    {
                        dataRowArray1 = UserCenterSession.LotteryTime.Select("Time <='" + str5 + "' and LotteryId=" + str2, "Time asc");
                        dateTime3 = dateTime2.AddDays(1.0);
                        newValue2 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                    }
                    else
                    {
                        newValue2 = str4 + "-" + dataRowArray1[0]["Sn"].ToString();
                        dateTime1 = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString());
                        if (dateTime2 > Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime2 < Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 10:00:01") && str2 == "1003")
                        {
                            dateTime3 = dateTime2.AddDays(-1.0);
                            newValue2 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                        }
                    }
                    if (Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()) < Convert.ToDateTime(str5))
                    {
                        dateTime3 = dateTime2.AddDays(1.0);
                        dateTime1 = Convert.ToDateTime(dateTime3.ToString("yyyy-MM-dd") + " " + dataRowArray1[0]["Time"].ToString());
                    }
                    timeSpan = dateTime1 - Convert.ToDateTime(str5);
                    DataRow[] dataRowArray2 = UserCenterSession.LotteryTime.Select("Time <'" + str5 + "' and LotteryId=" + str2, "Time desc");
                    if (dataRowArray2.Length == 0)
                    {
                        dataRowArray2 = UserCenterSession.LotteryTime.Select("LotteryId=" + str2, "Time desc");
                        dateTime3 = dateTime2.AddDays(-1.0);
                        newValue1 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    }
                    else
                    {
                        newValue1 = str4 + "-" + dataRowArray2[0]["Sn"].ToString();
                        if (dateTime2 > Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime2 < Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 10:00:01") && str2 == "1003")
                        {
                            dateTime3 = dateTime2.AddDays(-1.0);
                            newValue1 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                        }
                    }
                    if (str2 == "1010" || str2 == "1017" || str2 == "3004")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(str2) + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                    if (str2 == "1012")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                    if (str2 == "1013")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                    if (str2 == "1014" || str2 == "1015" || str2 == "1016")
                    {
                        newValue1 = newValue1.Replace("-", "");
                        newValue2 = newValue2.Replace("-", "");
                    }
                    if (str2 == "4001")
                    {
                        newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                        newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
                    }
                }
                string newValue3 = string.Concat((object)((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000));
                string str9 = str6.Replace("倒计时", newValue3).Replace("下期期号", newValue2).Replace("当前期号", newValue1);
                str1 = str1 + str9 + ",";
            }
            this._response = "{\"result\":\"1\",\"table\": [" + str1.Substring(0, str1.Length - 1) + "]}";
        }

        private void ajaxIndexLottery()
        {
            string str1 = "";
            this.doh.Reset();
            this.doh.SqlCmd = "select row_number() over (order by Sort asc) as rowid,* from [Sys_Lottery] where IsOpen=0 and Id in (1001,2001,3001,3002) order by Sort asc";
            DataTable dataTable = this.doh.GetDataTable();
            for (int index = 0; index < dataTable.Rows.Count; ++index)
            {
                string newValue1 = dataTable.Rows[index]["Id"].ToString();
                string str2 = "{\"rowid\": \"排序Id\",\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"ordertime\": \"倒计时\",\"remark\": \"说明\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("排序Id", dataTable.Rows[index]["rowid"].ToString()).Replace("类别Id", dataTable.Rows[index]["LType"].ToString()).Replace("彩种Id", newValue1).Replace("说明", dataTable.Rows[index]["IphoneRemark"].ToString());
                DateTime now = DateTime.Now;
                DateTime dateTime1 = this.GetDateTime();
                dateTime1.ToString("yyyyMMdd");
                string str3 = dateTime1.ToString("HH:mm:ss");
                dateTime1.ToString("yyyy-MM-dd");
                DateTime dateTime2;
                TimeSpan timeSpan;
                if (newValue1 == "3002" || newValue1 == "3003")
                {
                    string str4 = dateTime1.ToString("yyyy-MM-dd") + " 20:30:00";
                    if (dateTime1 > Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " 20:30:00"))
                    {
                        dateTime2 = dateTime1.AddDays(1.0);
                        str4 = dateTime2.ToString("yyyy-MM-dd") + " 20:30:00";
                    }
                    timeSpan = Convert.ToDateTime(str4) - dateTime1;
                }
                else
                {
                    if (UserCenterSession.LotteryTime == null)
                        UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
                    DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + newValue1, "Time asc");
                    if (dataRowArray.Length == 0)
                        dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + newValue1, "Time asc");
                    DateTime dateTime3 = Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
                    if (Convert.ToDateTime(dataRowArray[0]["Time"].ToString()) < Convert.ToDateTime(str3))
                    {
                        dateTime2 = dateTime1.AddDays(1.0);
                        dateTime3 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
                    }
                    timeSpan = dateTime3 - Convert.ToDateTime(str3);
                }
                string newValue2 = string.Concat((object)((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000));
                string str5 = str2.Replace("倒计时", newValue2);
                str1 = str1 + str5 + ",";
            }
            this._response = "{\"result\":\"1\",\"table\": [" + str1.Substring(0, str1.Length - 1) + "]}";
        }

        private void ajaxLotteryTime23()
        {
            string newValue1 = this.q("lid");
            string str1 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\",\"curnumber\": \"开奖号码\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("彩种类别", newValue1);
            DateTime now = DateTime.Now;
            DateTime dateTime = this.GetDateTime();
            dateTime.ToString("yyyyMMdd");
            dateTime.ToString("HH:mm:ss");
            dateTime.ToString("yyyy-MM-dd");
            this.doh.Reset();
            this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + newValue1 + ") as closetime";
            DataTable dataTable1 = this.doh.GetDataTable();
            string str2 = str1.Replace("封单时间", dataTable1.Rows[0]["closetime"].ToString());
            TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") - DateTime.Now;
            string newValue2 = string.Concat((object)(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
            string str3 = str2.Replace("倒计时", newValue2);
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT TOP 1 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + newValue1 + " order by Id desc";
            DataTable dataTable2 = this.doh.GetDataTable();
            string str4;
            if (dataTable2.Rows.Count > 0)
            {
                string newValue3 = dataTable2.Rows[0]["title"].ToString();
                string newValue4 = string.Concat((object)(Convert.ToDecimal(dataTable2.Rows[0]["title"].ToString()) + 1));
                string str5 = str3.Replace("下期期号", newValue4).Replace("当前期号", newValue3);
                string[] strArray = dataTable2.Rows[0]["Number"].ToString().Split(',');
                string str6 = "<p class='hm'>";
                for (int index = 0; index < strArray.Length; ++index)
                    str6 = str6 + "<span>" + strArray[index] + "</span>";
                string newValue5 = str6 + "</p>";
                str4 = str5.Replace("开奖号码", newValue5);
            }
            else
            {
                string newValue3 = "您还未投注";
                string newValue4 = DateTime.Now.ToString("yyyy") + "000000001";
                str4 = str3.Replace("下期期号", newValue4).Replace("当前期号", newValue3).Replace("开奖号码", "请您先投注");
            }
            this._response = str4;
        }

        private void ajaxLotteryTime()
        {
            LotteryDAL dal = new LotteryDAL();

            string ltId = this.q("lid");//彩种Id
            int id;

            if (Int32.TryParse(ltId, out id) == false)
            {
                this._response = "{}";
                return;
            }

            SysLotteryModel lottery = dal.GetSysLotteryById(id);

            if (lottery == null)
            {
                this._response = "{}";
                return;
            }

            //名称
            //彩种类别
            //倒计时
            //封单时间
            //下期期号
            //已开期数
            //当前期号
            string ltInfo = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"opennum\": \"已开期数\",\"cursn\": \"当前期号\"}";
            ltInfo = ltInfo.Replace("名称", lottery.Title);
            ltInfo = ltInfo.Replace("彩种类别", ltId);
            ltInfo = ltInfo.Replace("封单时间", lottery.CloseTime.ToString()); 
            
            DateTime nextTime = DateTime.Now; //下一期开奖时间
            //DateTime curDateTime = this.GetDateTime(); //当前日期时间
            DateTime curDateTime = DateTime.Now; //当前日期时间
            string curDate = curDateTime.ToString("yyyyMMdd"); //当前日期
            string curTime = curDateTime.ToString("HH:mm:ss"); //当前时间

            int num;
            string curExpect = string.Empty; //当前期号
            string nextExpect = string.Empty; //下一期期号
            string expectNum = "0"; //已开期数
            TimeSpan timeSpan;

            //福彩3d, 体彩3d
            if (ltId == "3002" || ltId == "3003")
            {
                num = curDateTime.Year;
                DateTime dateTime3 = Convert.ToDateTime(num.ToString() + "-01-01 20:30:00");
                this.doh.Reset();
                this.doh.SqlCmd = "select datediff(d,'" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "','" + curDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
                int Num = Convert.ToInt32(this.doh.GetDataTable().Rows[0]["d"]) - 7 + 1;
                string str7 = curDateTime.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                string str8 = curDateTime.ToString("yyyy-MM-dd") + " 20:30:00";

                if (curDateTime > Convert.ToDateTime(curDateTime.ToString(" 20:30:00")))
                {
                    str8 = curDateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
                }
                else
                {
                    --Num;
                }

                num = curDateTime.Year;
                curExpect = num.ToString() + Func.AddZero(Num, 3);
                num = curDateTime.Year;
                nextExpect = num.ToString() + Func.AddZero(Num + 1, 3);
                timeSpan = Convert.ToDateTime(str8) - Convert.ToDateTime(curTime);
            }
            //香港六合彩
            else if (ltId == "6001")
            {
                if (UserCenterSession.LotteryDateTime == null)
                {
                    UserCenterSession.LotteryDateTime = new LotteryTimeDAL().GetDateTimeTable(); //开奖时间
                }

                //大于当前时间，下一期开奖时间
                DataRow[] dataRowArray1 = UserCenterSession.LotteryDateTime.Select("Time >'" + curDateTime + "' and LotteryId=" + ltId, "Time asc");

                if (dataRowArray1.Length > 0)
                {
                    nextExpect = curDateTime.Year + "-" + dataRowArray1[0]["Sn"].ToString(); //下一期开奖期号
                    nextTime = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()); //下一期开奖时间   
                }

                DataRow[] dataRowArray2 = UserCenterSession.LotteryDateTime.Select("Time <'" + curDateTime + "' and LotteryId=" + ltId, "Time desc");

                if (dataRowArray2.Length > 0)
                {
                    curExpect = curDateTime.Year + "-" + dataRowArray2[0]["Sn"].ToString(); //当前期开奖期号
                    expectNum = dataRowArray2[0]["Sn"].ToString(); //当前期开奖时间
                }

                //计算倒计时 & 当前期号
                timeSpan = nextTime - Convert.ToDateTime(curTime);
            }
            else
            {
                if (UserCenterSession.LotteryTime == null)
                {
                    UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable(); //开奖时间
                }

                //大于当前时间，下一期开奖时间
                DataRow[] dataRowArray1 = UserCenterSession.LotteryTime.Select("Time >'" + curTime + "' and LotteryId=" + ltId, "Time asc");

                if (dataRowArray1.Length == 0)
                {
                    dataRowArray1 = UserCenterSession.LotteryTime.Select("Time <='" + curTime + "' and LotteryId=" + ltId, "Time asc");
                    nextExpect = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                }
                else
                {
                    nextExpect = curDate + "-" + dataRowArray1[0]["Sn"].ToString(); //下一期开奖期号
                    nextTime = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()); //下一期开奖时间

                    if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 00:00:00")
                        && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 10:00:01")
                        && ltId == "1003")
                    {
                        //新疆时时彩, 北京时间0点到10点，记为前一天期号
                        nextExpect = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                    }

                    if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:00:00") &&
                        curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:59:59") &&
                        (ltId == "1014" || ltId == "1016"))
                    {  
                        //东京1.5分彩, 北京时间23点，记为下一天期号
                        nextExpect = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                    }
                }

                //开奖时间小于当前时间，则从下一天(+1d)开始开奖
                if (Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()) < Convert.ToDateTime(curTime))
                {
                    nextTime = Convert.ToDateTime(curDateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRowArray1[0]["Time"].ToString());
                }

                //计算倒计时 & 当前期号
                timeSpan = nextTime - Convert.ToDateTime(curTime);
                DataRow[] dataRowArray2 = UserCenterSession.LotteryTime.Select("Time <'" + curTime + "' and LotteryId=" + ltId, "Time desc");

                if (dataRowArray2.Length == 0)
                {
                    dataRowArray2 = UserCenterSession.LotteryTime.Select("LotteryId=" + ltId, "Time desc");
                    curExpect = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    expectNum = dataRowArray2[0]["Sn"].ToString();
                }
                else
                {
                    curExpect = curDate + "-" + dataRowArray2[0]["Sn"].ToString();
                    expectNum = dataRowArray2[0]["Sn"].ToString();

                    if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 00:00:00") 
                        && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 10:00:01") 
                        && ltId == "1003")
                    {
                        //新疆时时彩, 北京时间0点到10点，记为前一天期号
                        curExpect = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                        expectNum = dataRowArray2[0]["Sn"].ToString();
                    }
                    if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:00:00")
                        && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:59:59")
                        && (ltId == "1014" || ltId == "1016"))
                    {
                        //东京1.5分彩, 北京时间23点，记为下一天期号
                        curExpect = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    }
                }

                //韩国1.5分彩, 韩国1.5分3D
                if (ltId == "1010" || ltId == "1017" || ltId == "3004")
                {
                    curExpect = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(ltId) + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                    expectNum = dataRowArray2[0]["Sn"].ToString();
                    nextExpect = string.Concat((object)(Convert.ToInt32(curExpect) + 1));
                }

                //新加坡2分彩
                if (ltId == "1012")
                {
                    curExpect = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                    expectNum = dataRowArray2[0]["Sn"].ToString();
                    nextExpect = string.Concat((object)(Convert.ToInt32(curExpect) + 1));
                }

                //台湾5分彩
                if (ltId == "1013")
                {
                    curExpect = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                    expectNum = dataRowArray2[0]["Sn"].ToString();
                    nextExpect = string.Concat((object)(Convert.ToInt32(curExpect) + 1));
                }

                //东京1.5分彩, 菲律宾1.5分
                if (ltId == "1014" || ltId == "1015" || ltId == "1016")
                {
                    curExpect = curExpect.Replace("-", "");
                    nextExpect = nextExpect.Replace("-", "");
                }

                //北京PK10
                if (ltId == "4001")
                {
                    curExpect = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                    expectNum = dataRowArray2[0]["Sn"].ToString();
                    nextExpect = string.Concat((object)(Convert.ToInt32(curExpect) + 1));
                }
            }

            ltInfo = ltInfo.Replace("已开期数", Int32.Parse(expectNum).ToString());
            ltInfo = ltInfo.Replace("下期期号", nextExpect);
            ltInfo = ltInfo.Replace("当前期号", curExpect);

            string countdown = string.Concat((object)(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
            ltInfo = ltInfo.Replace("倒计时", countdown);

            this._response = ltInfo;
        }

        private void ajaxUserInfo()
        {
            string _jsonstr = "";
            this.UserInfo(ref _jsonstr);
            this._response = _jsonstr;
        }

        private void ajaxCheckUserName()
        {
            this.doh.Reset();
            this.doh.ConditionExpress = "username=@username";
            this.doh.AddConditionParameter("@username", (object)this.q("txtUserName"));
            if (this.doh.Exist("N_User"))
                this._response = this.JsonResult(0, "此账号已存在，不能添加");
            else
                this._response = this.JsonResult(1, "帐号不存在，可以添加");
        }

        private void ajaxPopInfo()
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
            {
                this.doh.Reset();
                this.doh.SqlCmd = "select top 1 Id,Title,Msg from N_UserMessage with(nolock) where IsRead=0 and UserId=" + Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") + " order by Id desc";
                DataTable dataTable = this.doh.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"title\":\"" + dataTable.Rows[0]["Title"].ToString() + "\",\"content\":\"" + dataTable.Rows[0]["Msg"].ToString() + "\"}";
                    this.doh.Reset();
                    this.doh.ConditionExpress = "Id=@Id";
                    this.doh.AddConditionParameter("@Id", (object)dataTable.Rows[0]["Id"].ToString());
                    this.doh.AddFieldItem("IsRead", (object)"1");
                    this.doh.Update("N_UserMessage");
                }
                else
                    this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\",\"title\":\"0\",\"content\":\"0\"}";
                dataTable.Dispose();
            }
            else
                this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\",\"title\":\"0\",\"content\":\"0\"}";
        }

        private void ajaxRegister()
        {
            string _UserName = this.f("name");
            string _Password = this.f("pass");
            string _code = this.f("code");
            string str1 = this.f("u");
            string _realcode = "";
            try
            {
                if (string.IsNullOrEmpty(_UserName) || string.IsNullOrEmpty(_Password))
                    this._response = this.JsonResult(0, "用户名，密码不能为空！");
                else if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
                {
                    this._response = this.JsonResult(0, "验证码错误");
                }
                else
                {
                    this.doh.Reset();
                    this.doh.ConditionExpress = "UserName=@UserName";
                    this.doh.AddConditionParameter("@UserName", (object)_UserName.Trim());
                    if (this.doh.Count("N_User") > 0)
                        this._response = this.JsonResult(0, "对不起，该用户名已被注册！");
                    else if (_UserName.Length > 0 && _Password.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(str1))
                        {
                            string decryptKey = "qazwsxed";
                            if (str1.Length != 12)
                            {
                                this._response = this.JsonResult(0, "对不起，该注册链接不正确！");
                            }
                            else
                            {
                                string str2 = this.DecryptDES(str1.Replace("@", "+"), decryptKey);
                                string str3 = str2.Substring(0, str2.IndexOf('@'));
                                this.doh.Reset();
                                this.doh.ConditionExpress = "id=@id and Isdel=0";
                                this.doh.AddConditionParameter("@id", (object)str3);
                                if (this.doh.Count("N_User") < 1)
                                {
                                    this._response = this.JsonResult(0, "对不起，该注册链接已失效！");
                                }
                                else
                                {
                                    string str4 = str2.Substring(str2.IndexOf('@') + 1);
                                    int result;
                                    if (int.TryParse(str3, out result))
                                    {
                                        this.GetRandomNumberString(64, false);
                                        int num = new UserDAL().Register(str3, _UserName, _Password, Convert.ToDecimal(str4) * new Decimal(10));
                                        if (num > 0)
                                        {
                                            this.doh.Reset();
                                            this.doh.ConditionExpress = "id=@id";
                                            this.doh.AddConditionParameter("@id", (object)str3);
                                            string str5 = this.doh.GetField("N_User", "UserCode").ToString() + Strings.PadLeft(num.ToString());
                                            this.doh.Reset();
                                            this.doh.ConditionExpress = "id=" + (object)num;
                                            this.doh.AddFieldItem("UserCode", (object)str5);
                                            this.doh.AddFieldItem("UserGroup", (object)"0");
                                            this.doh.Update("N_User");
                                            new LogSysDAL().Save("会员管理", "Id为" + (object)num + "的会员注册成功！");
                                            this._response = this.JsonResult(1, "会员注册成功");
                                        }
                                        else
                                            this._response = this.JsonResult(0, "注册失败，请重新注册");
                                    }
                                    else
                                        this._response = this.JsonResult(0, "链接地址错误！请重新打开");
                                }
                            }
                        }
                        else
                            this._response = this.JsonResult(0, "对不起，该注册链接不正确！");
                    }
                }
            }
            catch (Exception ex)
            {
                this._response = this.JsonResult(0, "注册异常：" + (object)ex);
            }
        }

        private void ajaxLogin()
        {
            string str1 = this.f("name");
            string str2 = this.f("pass");
            string _code = this.f("code");
            string _realcode = "";
            if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                this._response = this.GetJsonResult(0, "验证码不正确！");
            }
            else
            {
                int iExpires = 604800;
                string _userid = new UserDAL().ChkLoginWebApp(str1.Trim(), str2.Trim(), iExpires);
                if (_userid.Length < 10)
                {
                    IPScaner ipScaner = new IPScaner();
                    ipScaner.DataPath = HttpContext.Current.Server.MapPath("/statics/data/QQWry.Dat");
                    string clientIp = IPHelp.ClientIP;
                    ipScaner.IP = clientIp;
                    string _address = ipScaner.IPLocation() + ipScaner.ErrMsg;
                    string _browser = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
                    string osNameByUserAgent = this.GetOSNameByUserAgent(HttpContext.Current.Request.UserAgent);
                    new LogUserLoginDAL().Save(_userid, _address, _browser, osNameByUserAgent, IPHelp.ClientIP);
                    _userid = this.GetJsonResult(1, "success");
                }
                this._response = _userid;
            }
        }

        private void ajaxGetPwd()
        {
            string str1 = this.f("name");
            string str2 = this.f("question");
            string str3 = this.f("answer");
            string _code = this.f("code");
            string _realcode = "";
            if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
            {
                this._response = "验证码应该是" + _realcode;
            }
            else
            {
                this.doh.Reset();
                this.doh.ConditionExpress = "userName=@userName";
                this.doh.AddConditionParameter("@userName", (object)str1);
                if (!this.doh.Exist("N_User"))
                {
                    this._response = "对不起，账号不存在!";
                }
                else
                {
                    this.doh.Reset();
                    this.doh.ConditionExpress = "userName =@userName and Question=@Question and Answer=@Answer";
                    this.doh.AddConditionParameter("@userName", (object)str1);
                    this.doh.AddConditionParameter("@Question", (object)str2);
                    this.doh.AddConditionParameter("@Answer", (object)str3);
                    if (!this.doh.Exist("N_User"))
                    {
                        this._response = "对不起，验证问题错误!";
                    }
                    else
                    {
                        this.doh.Reset();
                        this.doh.ConditionExpress = "userName=@userName";
                        this.doh.AddConditionParameter("@userName", (object)str1);
                        this.doh.AddFieldItem("Password", (object)MD5.Last64(MD5.Lower32("123456")));
                        this.doh.Update("N_User");
                        this._response = "密码也为您重置为：123456，请您登陆系统及时更改密码！";
                        new LogSysDAL().Save("会员管理", str1 + "找回密码！");
                    }
                }
            }
        }

        private void ajaxLogout()
        {
            new UserDAL().ChkLogout();
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=@Id and IsEnable=0";
            this.doh.AddConditionParameter("@Id", (object)this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")));
            this.doh.AddFieldItem("LastTime", (object)DateTime.Now.ToString());
            this.doh.AddFieldItem("IsOnline", (object)0);
            this.doh.Update("N_User");
            this.doh.Dispose();
            this._response = this.JsonResult(1, "成功退出");
        }

        private void ajaxHistoryTop5()
        {
            string str1 = this.q("lid");
            this.doh.Reset();
            if (str1 == "23")
                this.doh.SqlCmd = "SELECT TOP 10 [IssueNum] as title,(select number from Sys_LotteryData where title=a.IssueNum) as number FROM [N_UserBet] a where lotteryId=23 and UserId=" + this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")) + " group by a.IssueNum order by a.IssueNum desc";
            else
                this.doh.SqlCmd = "SELECT TOP 10 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + str1 + " order by replace([Title],'-','') desc";
            DataTable dataTable = this.doh.GetDataTable();
            string str2 = "\"recordcount\":1,\"table\": [信息列表]";
            string str3 = "";
            int num = 1;
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
            {
                string str4 = "{\"no\":" + (object)num + ",\"type\":\"类别\",\"title\": \"" + row["Title"].ToString() + "\",";
                if (!string.IsNullOrEmpty(string.Concat(row["Number"])))
                {
                    string[] strArray = row["Number"].ToString().Split(',');
                    string str5 = strArray.Length != 3 ? str4.Replace("类别", "5") : str4.Replace("类别", "3");
                    for (int index = 0; index < strArray.Length; ++index)
                        str5 = str5 + "\"ball" + (object)(index + 1) + "\": \"" + strArray[index] + "\",";
                    str4 = str5.Substring(0, str5.Length - 1) + "}";
                }
                str3 = str3 + str4 + ",";
                ++num;
            }
            this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + str2.Replace("信息列表", str3.Substring(0, str3.Length > 1 ? str3.Length - 1 : 0)) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxLotteryTime2()
        {
            this.q("lid");
            string str1 = "\"table\": [{\"loid1\": 1,\"ordertime1\": \"倒计时1\",\"nestsn1\": \"下期期号1\",\"loid2\": 9,\"ordertime2\": \"倒计时2\",\"nestsn2\": \"下期期号2\"}]";
            string[] lot1 = this.GetLot("1");
            string str2 = str1.Replace("下期期号1", lot1[0]).Replace("倒计时1", lot1[1]);
            string[] lot2 = this.GetLot("9");
            this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + str2.Replace("下期期号2", lot2[0]).Replace("倒计时2", lot2[1]) + "}";
        }

        private string[] GetLot(string Lid)
        {
            string[] strArray = new string[2];
            DateTime dateTime1 = DateTime.Now;
            DateTime dateTime2 = this.GetDateTime();
            string str1 = dateTime2.ToString("yyyyMMdd");
            string str2 = dateTime2.ToString("HH:mm:ss");
            dateTime2.ToString("yyyy-MM-dd");
            if (UserCenterSession.LotteryTime == null)
                UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
            DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str2 + "' and LotteryId=" + Lid, "Time asc");
            DateTime dateTime3;
            string str3;
            if (dataRowArray.Length == 0)
            {
                dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str2 + "' and LotteryId=" + Lid, "Time asc");
                dateTime3 = dateTime2.AddDays(1.0);
                str3 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray[0]["Sn"].ToString();
            }
            else
            {
                str3 = str1 + "-" + dataRowArray[0]["Sn"].ToString();
                dateTime1 = Convert.ToDateTime(dataRowArray[0]["Time"].ToString());
            }
            if (Convert.ToDateTime(dataRowArray[0]["Time"].ToString()) < Convert.ToDateTime(str2))
            {
                dateTime3 = dateTime2.AddDays(1.0);
                dateTime1 = Convert.ToDateTime(dateTime3.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
            }
            string str4 = Convert.ToDateTime((dateTime1 - Convert.ToDateTime(str2)).ToString()).ToString("HH:mm:ss");
            string s = str4.Substring(0, 2);
            string str5 = str4.Substring(3, 2);
            string str6 = str4.Substring(6, 2);
            string str7;
            if (int.Parse(s) == 0)
                str7 = "<span class='k'>" + str5.Substring(0, 1) + "</span><span class='k'>" + str5.Substring(1, 1) + "</span><span class='i'>:</span><span class='k'>" + str6.Substring(0, 1) + "</span><span class='k'>" + str6.Substring(1, 1) + "</span>";
            else
                str7 = "<span class='k'>" + s.Substring(0, 1) + "</span><span class='k'>" + s.Substring(1, 1) + "</span><span class='i'>:</span><span class='k'>" + str5.Substring(0, 1) + "</span><span class='k'>" + str5.Substring(1, 1) + "</span>";
            strArray[0] = str3;
            strArray[1] = str7;
            return strArray;
        }

        public void GetListBetTop20()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 20 dbo.f_GetUserName([UserId]) as userName,dbo.f_GetLotteryName(LotteryId) as LotteryName,dbo.f_GetPlayName(PlayId) as PlayName,WinBonus,STime2,IssueNum\r\n                                FROM [N_UserBet] where State=3 or State=4 order by Id desc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        public void GetListLotteryData()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 5000 * from Sys_LotteryData order by Id desc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = dtHelp.DT2JSONAIR(dataTable, 10);
            dataTable.Clear();
            dataTable.Dispose();
        }

        public void GetKaiJiangList()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 20 * from V_KaiJiangNotice order by LotteryId asc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        public void GetKaiJiangInfo()
        {
            string str = this.q("lid");
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 120 *,dbo.f_GetLotteryName(type) as LotteryName from Sys_LotteryData where Type=" + str + " order by title desc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        public void GetIndexWinInfo()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 8 '恭喜 '+substring(dbo.f_GetUserName(UserId),1,3)+'*** 在'+dbo.f_GetLotteryName(LotteryId)+'赢得 '+Convert(varchar(20),WinBonus)+'元' as info FROM [Flex_UserBet]\r\n                        where DateDiff(hh,STime,getdate())<1 and WinBonus>0\r\n                        order by WinBonus desc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private string GetOSNameByUserAgent(string userAgent)
        {
            return !userAgent.Contains("NT 10.0") ? (!userAgent.Contains("NT 6.3") ? (!userAgent.Contains("NT 6.2") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.0") ? (!userAgent.Contains("NT 5.2") ? (!userAgent.Contains("NT 5.1") ? (!userAgent.Contains("NT 5") ? (!userAgent.Contains("NT 4") ? (!userAgent.Contains("Me") ? (!userAgent.Contains("98") ? (!userAgent.Contains("95") ? (!userAgent.Contains("Mac") ? (!userAgent.Contains("Unix") ? (!userAgent.Contains("Linux") ? (!userAgent.Contains("SunOS") ? HttpContext.Current.Request.Browser.Platform : "SunOS") : "Linux") : "UNIX") : "Mac") : "Windows 95") : "Windows 98") : "Windows Me") : "Windows NT4") : "Windows 2000") : "Windows XP") : (!userAgent.Contains("64") ? "Windows Server 2003" : "Windows XP")) : "Windows Vista/Server 2008") : "Windows 7") : "Windows 7") : "Windows 8") : "Windows 8.1") : "Windows 10";
        }
    }
}
