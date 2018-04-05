// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxBetting
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using FlexDAL = Lottery.DAL.Flex;
using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Lottery.IPhone
{
    public partial class ajaxBetting : UserCenterSession
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.CheckFormUrl())
                this.Response.End();
            this.Admin_Load("master", "json");
            this._operType = this.q("oper");
            switch (this._operType)
            {
                case "ajaxCheckBetTime":  //允许投注
                    this.ajaxCheckBetTime();
                    break;
                case "ajaxBetting":
                    this.ajaxBetting2();
                    break;
                case "ajaxBettingCancel":
                    this.ajaxBettingCancel();
                    break;
                case "ajaxZHBetting":
                    this.ajaxZHBetting();
                    break;
                case "ajaxLottery":
                    this.ajaxLottery();
                    break;
                case "ajaxBigType":
                    this.ajaxBigType();
                    break;
                case "ajaxLotteryTime23":
                    this.ajaxLotteryTime23();
                    break;
                default:
                    this.DefaultResponse();
                    break;
            }
            this.Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = this.JsonResult(0, "未知操作");
        }

        private void ajaxLottery()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT * FROM [Sys_Lottery] where IsOpen=0 order by sort asc";
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxBigType()
        {
            string str = this.q("lid");
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,TypeId,Title FROM Sys_PlayBigType where TypeId=(SELECT [Ltype] FROM [Sys_Lottery] where Id =" + str + ") and IsOpen=1 ORDER BY Sort asc";
            DataTable dataTable1 = this.doh.GetDataTable();
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT * FROM Sys_PlaySmallType where IsOpen=1 ORDER BY Sort asc";
            DataTable dataTable2 = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable1, dataTable2) + "}";
            dataTable1.Clear();
            dataTable1.Dispose();
        }

        private void ajaxBettingCancel()
        {
            this._response = new Lottery.DAL.Flex.UserBetDAL().BetCancel(this.f("Id"));
        }

        private void ajaxZHBetting()
        {
            if (this.AdminId == "")
            {
                this._response = this.JsonResult(0, "投注失败,请重新登录后再进行投注!");
            }
            else
            {
                HttpContext.Current.Response.ContentType = "application/json";
                List<ajaxBetting.RequestDataJSONZH> requestDataJsonzhList = ajaxBetting.JSONToObject<List<ajaxBetting.RequestDataJSONZH>>("[" + new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd() + "]");
                ajaxBetting.RequestDataJSONZH requestDataJsonzh1 = new ajaxBetting.RequestDataJSONZH();
                int int32_1 = Convert.ToInt32(this.AdminId);
                int int32_2 = Convert.ToInt32(requestDataJsonzhList[0].lotteryId);
                int int32_3 = Convert.ToInt32(requestDataJsonzhList[0].IsStop);
                int int32_4 = Convert.ToInt32(requestDataJsonzhList[0].ZHNums);
                Decimal num = Convert.ToDecimal(requestDataJsonzhList[0].ZHSums);

                LotteryDAL lotDal = new LotteryDAL();
                FlexDAL.UserBetDAL betDal = new FlexDAL.UserBetDAL();
                SysLotteryModel lottery = lotDal.GetSysLotteryById(int32_2);

                if (betDal.CheckBetTime(int32_2) == false)
                {
                    this._response = this.JsonResult(0, string.Format("凌晨1点55分到上午10点, [{0}]暂停开奖", lottery.Title));
                    return;
                }

                try
                {
                    string[] issueTimeAndSn = betDal.GetIssueTimeAndSN(int32_2);
                    string str1 = issueTimeAndSn[0];
                    DateTime dateTime = Convert.ToDateTime(issueTimeAndSn[1]);
                    DateTime serverTime = FlexDAL.PublicDAL.GetServerTime();
                    string str2 = betDal.CheckBet(int32_1, int32_2, Convert.ToDecimal(num), dateTime);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        this._response = this.JsonResult(0, str2);
                    }
                    else
                    {
                        Decimal money = new Decimal(0);
                        UserZhBetModel zhmodel = new UserZhBetModel();
                        zhmodel.UserId = int32_1;
                        zhmodel.LotteryId = int32_2;
                        zhmodel.PlayId = 0;
                        zhmodel.StartIssueNum = str1;
                        zhmodel.TotalNums = int32_4;
                        zhmodel.IsStop = int32_3;
                        zhmodel.STime = DateTime.Now;
                        List<UserBetModel> userBetModelList = new List<UserBetModel>();
                        List<UserZhDetailModel> listZh = new List<UserZhDetailModel>();
                        for (int index1 = 0; index1 < requestDataJsonzhList.Count; ++index1)
                        {
                            ajaxBetting.RequestDataJSONZH requestDataJsonzh2 = requestDataJsonzhList[index1];
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Id=@Id";
                            DbOperHandler doh = this.doh;
                            string _conditionName = "@Id";
                            int playId1 = requestDataJsonzh2.playId;
                            string str3 = playId1.ToString();
                            doh.AddConditionParameter(_conditionName, (object)str3);
                            string str4 = string.Concat(this.doh.GetField("Sys_PlaySmallType", "Title2"));
                            if (Convert.ToDecimal(requestDataJsonzh2.price) < new Decimal(0) || Convert.ToDecimal(requestDataJsonzh2.Num) < new Decimal(1) || Convert.ToDecimal(requestDataJsonzh2.times) < new Decimal(1))
                            {
                                this._response = this.JsonResult(0, "投注错误！请重新投注！");
                                return;
                            }
                            Decimal singelBouns = new Decimal(0);
                            int userId = int32_1;
                            int lotteryId = int32_2;
                            string balls = requestDataJsonzh2.balls;
                            playId1 = requestDataJsonzh2.playId;
                            string playId2 = playId1.ToString();
                            string strPos = requestDataJsonzh2.strPos;
                            int int32_5 = Convert.ToInt32(requestDataJsonzh2.Num);
                            Decimal Point = Convert.ToDecimal(requestDataJsonzh2.Point);
                            string str5 = Calculate.BetNumerice(userId, lotteryId, balls, playId2, strPos, int32_5, Point, ref singelBouns);
                            if (!string.IsNullOrEmpty(str5))
                            {
                                this._response = str5;
                                return;
                            }
                            if (singelBouns <= new Decimal(0))
                            {
                                this._response = this.JsonResult(0, "投注失败,返点错误，请重新投注！");
                                return;
                            }
                            UserBetModel userBetModel = new UserBetModel();
                            userBetModel.UserId = int32_1;
                            userBetModel.UserMoney = new Decimal(0);
                            userBetModel.LotteryId = int32_2;
                            userBetModel.PlayId = Convert.ToInt32(requestDataJsonzh2.playId);
                            userBetModel.PlayCode = str4;
                            userBetModel.SingleMoney = Convert.ToDecimal(requestDataJsonzh2.price);
                            userBetModel.Num = Convert.ToInt32(requestDataJsonzh2.Num);
                            userBetModel.Detail = requestDataJsonzh2.balls;
                            userBetModel.Point = Convert.ToDecimal(requestDataJsonzh2.Point);
                            userBetModel.Bonus = singelBouns;
                            userBetModel.Pos = requestDataJsonzh2.strPos;
                            userBetModel.STime2 = serverTime;
                            userBetModel.IsDelay = 0;
                            userBetModel.ZHID = 0;
                            for (int index2 = 0; index2 < requestDataJsonzh2.table2.Count; ++index2)
                            {
                                ajaxBetting.RequestDataJSONZH2 requestDataJsonzH2 = requestDataJsonzh2.table2[index2];
                                if (Convert.ToInt32(requestDataJsonzH2.ZHTimes) > 0 && Convert.ToDecimal(requestDataJsonzH2.ZHIssueNum.Replace("-", "")) >= Convert.ToDecimal(str1.Replace("-", "")))
                                {
                                    UserZhDetailModel userZhDetailModel = new UserZhDetailModel();
                                    userZhDetailModel.IssueNum = requestDataJsonzH2.ZHIssueNum;
                                    userZhDetailModel.Times = Convert.ToInt32(requestDataJsonzH2.ZHTimes);
                                    userZhDetailModel.STime = Convert.ToDateTime(requestDataJsonzH2.ZHSTime);
                                    userZhDetailModel.Lists.Add(userBetModel);
                                    money += userBetModel.SingleMoney * (Decimal)userBetModel.Num * (Decimal)userZhDetailModel.Times;
                                    listZh.Add(userZhDetailModel);
                                }
                            }
                        }
                        zhmodel.TotalSums = money;
                        if (listZh.Count > 0)
                        {
                            if (betDal.InsertZhBet(zhmodel, listZh, money, "手机追号") > 0)
                                this._response = this.JsonResult(1, "追号成功！请等待开奖！");
                            else
                                this._response = this.JsonResult(0, "对不起,投注失败！");
                        }
                        else
                            this._response = this.JsonResult(0, "对不起,投注失败！");
                    }
                }
                catch (Exception ex)
                {
                    this._response = this.JsonResult(0, "对不起,投注失败！");
                }
            }
        }

        /// <summary>
        /// 检查投注时间
        /// </summary>
        private void ajaxCheckBetTime()
        {
            LotteryDAL lotDal = new LotteryDAL();
            FlexDAL.UserBetDAL betDal = new FlexDAL.UserBetDAL();
            string id = this.q("id"); //彩中编码
            int lotteryid = 0;
            Int32.TryParse(id, out lotteryid);
            SysLotteryModel lottery = lotDal.GetSysLotteryById(lotteryid);

            if (betDal.CheckBetTime(lotteryid) == false)
            {
                this._response = this.JsonResult(0, string.Format("凌晨1点55分到上午10点, [{0}]暂停开奖", lottery.Title));
            }
            else
            {
                this._response = this.JsonResult(1, "有效开奖时间");
            }
        }

        private void ajaxBetting2()
        {
            if (this.AdminId == "")
                this._response = this.JsonResult(0, "投注失败,请重新登录后再进行投注!");
            else if (this.site.BetIsOpen == 1)
            {
                this._response = this.JsonResult(0, "系统正在维护，不能投注！");
            }
            else
            {
                HttpContext.Current.Response.ContentType = "application/json";
                List<ajaxBetting.RequestDataJSON> requestDataJsonList = ajaxBetting.JSONToObject<List<ajaxBetting.RequestDataJSON>>(HttpUtility.UrlDecode(new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd()));
                ajaxBetting.RequestDataJSON requestDataJson1 = new ajaxBetting.RequestDataJSON();
                int lotteryId1 = requestDataJsonList[0].lotteryId;　//投注信息
                int int32_1 = Convert.ToInt32(this.AdminId); //会员Id

                LotteryDAL lotDal = new LotteryDAL();
                FlexDAL.UserBetDAL betDal = new FlexDAL.UserBetDAL();
                SysLotteryModel lottery = lotDal.GetSysLotteryById(lotteryId1);

                if (betDal.CheckBetTime(lotteryId1) == false)
                {
                    this._response = this.JsonResult(0, string.Format("凌晨1点55分到上午10点, [{0}]暂停开奖", lottery.Title));
                    return;
                }


                Decimal num1 = new Decimal(0);
                try
                {
                    //获取当前一期时间和期号
                    string[] issueTimeAndSn = betDal.GetIssueTimeAndSN(lotteryId1);
                    string str1 = issueTimeAndSn[0]; //期号
                    DateTime dateTime = Convert.ToDateTime(issueTimeAndSn[1]); //开奖时间
                    DateTime serverTime = FlexDAL.PublicDAL.GetServerTime(); //系统时间
                    for (int index = 0; index < requestDataJsonList.Count; ++index)
                    {
                        ajaxBetting.RequestDataJSON requestDataJson2 = requestDataJsonList[index];
                        num1 += requestDataJson2.price * (Decimal)requestDataJson2.Num * requestDataJson2.times;
                    }
                    string str2 = betDal.CheckBet(int32_1, lotteryId1, Convert.ToDecimal(num1), dateTime);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        this._response = this.JsonResult(0, str2);
                    }
                    else
                    {
                        int num2 = 0;
                        for (int index = 0; index < requestDataJsonList.Count; ++index)
                        {
                            ajaxBetting.RequestDataJSON requestDataJson2 = requestDataJsonList[index];
                            this.doh.Reset();
                            this.doh.ConditionExpress = "Id=@Id";
                            DbOperHandler doh = this.doh;
                            string _conditionName = "@Id";
                            int playId1 = requestDataJson2.playId;
                            string str3 = playId1.ToString();
                            doh.AddConditionParameter(_conditionName, (object)str3);
                            string str4 = string.Concat(this.doh.GetField("Sys_PlaySmallType", "Title2"));
                            Decimal singelBouns = new Decimal(0);
                            if (lotteryId1 != 5001)
                            {
                                if (Convert.ToDecimal(requestDataJson2.price) < new Decimal(0) || Convert.ToDecimal(requestDataJson2.Num) < new Decimal(1) || Convert.ToDecimal(requestDataJson2.times) < new Decimal(1))
                                {
                                    this._response = this.JsonResult(0, "投注错误！请重新投注！");
                                    return;
                                }
                                int userId = int32_1;
                                int lotteryId2 = lotteryId1;
                                string balls = requestDataJson2.balls;
                                playId1 = requestDataJson2.playId;
                                string playId2 = playId1.ToString();
                                string strPos = requestDataJson2.strPos;
                                int int32_2 = Convert.ToInt32(requestDataJson2.Num);
                                Decimal Point = Convert.ToDecimal(requestDataJson2.Point);
                                string str5 = Calculate.BetNumerice(userId, lotteryId2, balls, playId2, strPos, int32_2, Point, ref singelBouns);
                                if (!string.IsNullOrEmpty(str5))
                                {
                                    this._response = str5.Replace("[", "").Replace("]", "");
                                    return;
                                }
                                if (singelBouns <= new Decimal(0))
                                {
                                    this._response = this.JsonResult(0, "投注失败,返点错误，请重新投注！");
                                    return;
                                }
                            }
                            UserBetModel model = new UserBetModel();
                            model.UserId = int32_1;
                            model.UserMoney = new Decimal(0);
                            model.LotteryId = lotteryId1;
                            model.PlayId = Convert.ToInt32(requestDataJson2.playId);
                            model.PlayCode = str4;
                            model.IssueNum = str1;
                            model.SingleMoney = Convert.ToDecimal(requestDataJson2.price);
                            model.Num = Convert.ToInt32(requestDataJson2.Num);
                            model.Detail = requestDataJson2.balls;
                            model.Point = Convert.ToDecimal(requestDataJson2.Point);
                            model.Bonus = singelBouns;
                            model.Pos = requestDataJson2.strPos;
                            model.STime = dateTime;
                            model.STime2 = serverTime;
                            model.IsDelay = 0;
                            model.Times = Convert.ToDecimal(requestDataJson2.times);
                            model.ZHID = 0;
                            num2 = !model.Pos.Equals("") ? 
                                betDal.InsertBetPos(model, "手机端") : 
                                (!model.PlayCode.Equals("P_5ZH") && !model.PlayCode.Equals("P_4ZH_L") && (!model.PlayCode.Equals("P_4ZH_R") && !model.PlayCode.Equals("P_3ZH_L")) && !model.PlayCode.Equals("P_3ZH_C") && !model.PlayCode.Equals("P_3ZH_R") ? 
                                betDal.InsertBet(model, "手机端") : 
                                betDal.InsertBetZH(model, "手机端"));
                        }
                        if (num2 > 0)
                            this._response = this.JsonResult(1, "第" + str1 + "期投注成功，请期待开奖！");
                        else
                            this._response = this.JsonResult(0, "对不起,投注失败！");

                        //this._response = this.JsonResult(1, "第" + str1 + "期投注成功，请期待开奖！");
                    }
                }
                catch (Exception ex)
                {
                    this._response = this.JsonResult(0, "对不起,投注失败！");
                }
            }
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
            this.doh.SqlCmd = "SELECT TOP 1 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where UserId=" + this.AdminId + " order by Id desc";
            DataTable dataTable2 = this.doh.GetDataTable();
            string str4;
            if (dataTable2.Rows.Count > 0)
            {
                string newValue3 = dataTable2.Rows.Count > 0 ? dataTable2.Rows[0]["title"].ToString() : "您还未投注";
                string newValue4 = dataTable2.Rows.Count > 0 ? string.Concat((object)(Convert.ToDecimal(dataTable2.Rows[0]["title"].ToString()) + 1)) : DateTime.Now.ToString("yyyyMMdd") + "00001";
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
                string newValue4 = DateTime.Now.ToString("yyyyMMdd") + "00001";
                str4 = str3.Replace("下期期号", newValue4).Replace("当前期号", newValue3).Replace("开奖号码", "<p class='hm'>" + "请您先投注" + "</p>");
            }
            this._response = str4;
        }

        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            try
            {
                return scriptSerializer.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.JSONToObject(): " + ex.Message);
            }
        }

        [Serializable]
        public class RequestDataJSON
        {
            public int lotteryId { get; set; }

            public int playId { get; set; }

            public Decimal price { get; set; }

            public Decimal times { get; set; }

            public int Num { get; set; }

            public string price_win { get; set; }

            public Decimal singelBouns { get; set; }

            public Decimal Point { get; set; }

            public string balls { get; set; }

            public string strPos { get; set; }
        }

        [Serializable]
        public class RequestDataJSONZH
        {
            public int lotteryId { get; set; }

            public int playId { get; set; }

            public string IssueNum { get; set; }

            public Decimal price { get; set; }

            public Decimal times { get; set; }

            public int Num { get; set; }

            public string price_win { get; set; }

            public Decimal singelBouns { get; set; }

            public Decimal Point { get; set; }

            public string balls { get; set; }

            public string strPos { get; set; }

            public Decimal ZHNums { get; set; }

            public Decimal ZHSums { get; set; }

            public int IsStop { get; set; }

            public List<ajaxBetting.RequestDataJSONZH2> table2 { get; set; }
        }

        [Serializable]
        public class RequestDataJSONZH2
        {
            public string ZHIssueNum { get; set; }

            public int ZHTimes { get; set; }

            public string ZHSTime { get; set; }
        }
    }
}
