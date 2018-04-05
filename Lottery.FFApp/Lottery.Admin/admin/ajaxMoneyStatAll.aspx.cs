// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxMoneyStatAll
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Text;

namespace Lottery.Admin
{
    public partial class ajaxMoneyStatAll : AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "json");
            this._operType = this.q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    this.ajaxGetList();
                    break;
                case "ajaxGetListTop10":
                    this.ajaxGetListTop10();
                    break;
                case "ajaxGetListDay":
                    this.ajaxGetListDay();
                    break;
                case "ajaxGetListRank":
                    this.ajaxGetListRank();
                    break;
                case "ajaxGetListLottery":
                    this.ajaxGetListLottery();
                    break;
                case "ajaxGetListIuss":
                    this.ajaxGetListIuss();
                    break;
                case "ajaxGetListPlay":
                    this.ajaxGetListPlay();
                    break;
                case "ajaxGetListTeamSale":
                    this.ajaxGetListTeamSale();
                    break;
                case "ajaxGetAllInfo":
                    this.ajaxGetAllInfo();
                    break;
                case "ajaxGetListMonth":
                    this.ajaxGetListMonth();
                    break;
                case "ajaxGetListCheck":
                    this.ajaxGetListCheck();
                    break;
                case "ajaxChargeState": //支付订单状态
                    this.ajaxChargeState();
                    break;
                case "ajaxCashState": //提现订单状态
                    this.ajaxCashState();
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

        /// <summary>
        /// 获取订单状态
        /// </summary>
        private void ajaxChargeState()
        {
            Lottery.DAL.Flex.UserChargeDAL dal = new Lottery.DAL.Flex.UserChargeDAL();
            string ckDate = this.q("date");
            string lastDate = string.Empty;
            int num = 0;

            try
            {
                if (string.IsNullOrEmpty(ckDate))
                {
                    lastDate = dal.CheckChargeState(ref num);
                }
                else
                {
                    lastDate = dal.CheckChargeState(ref num, ckDate);
                }

            }
            catch
            {
                lastDate = dal.CheckChargeState(ref num);
            }

            this._response = this.JsonResult(num, lastDate);
        }

        /// <summary>
        /// 获取提现状态
        /// </summary>
        private void ajaxCashState()
        {
            Lottery.DAL.Flex.UserGetCashDAL dal = new Lottery.DAL.Flex.UserGetCashDAL();
            string ckDate = this.q("date");
            string lastDate = string.Empty;
            int num = 0;

            try
            {
                if (string.IsNullOrEmpty(ckDate))
                {
                    lastDate = dal.CheckCashState(ref num);
                }
                else
                {
                    lastDate = dal.CheckCashState(ref num, ckDate);
                }

            }
            catch
            {
                lastDate = dal.CheckCashState(ref num);
            }

            this._response = this.JsonResult(num, lastDate);
        }

        private void ajaxGetList()
        {
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            string whereStr = "";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("V_UserMoneyStatAllTotal");
            string sql0 = SqlHelp.GetSql0("*", "V_UserMoneyStatAllTotal", "sort", num2, num1, "asc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListTop10()
        {
            int PageIndex = this.Int_ThisPage();
            int PageSize = 10;
            int num = 0;
            string whereStr = "";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            num = this.doh.Count("V_UserMoneyStatAllTop10");
            string sql0 = SqlHelp.GetSql0("*", "V_UserMoneyStatAllTop10", "total", PageSize, PageIndex, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListDay()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            string str3 = this.q("code");
            string str4 = this.q("u");
            string FldName = this.q("order");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.StartTime);
                dateTime = dateTime.AddDays(-30.0);
                str1 = dateTime.ToString("yyyy-MM-dd");
            }
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            if (string.IsNullOrEmpty(FldName))
                FldName = "STime";
            string str5 = "";
            string whereStr1 = " STime >='" + str1 + "' and STime <'" + str2 + "'";
            if (string.IsNullOrEmpty(str3))
                str3 = "0";
            int totalCount;
            DataTable dataTable1;
            if (!string.IsNullOrEmpty(str4))
            {
                string whereStr2;
                if (str3 == "0")
                {
                    whereStr2 = whereStr1 + " and dbo.f_GetUserName(UserId) = '" + str4 + "'";
                }
                else
                {
                    this.doh.Reset();
                    this.doh.SqlCmd = "select top 1 Id from N_User where UserName='" + str4 + "'";
                    DataTable dataTable2 = this.doh.GetDataTable();
                    if (dataTable2.Rows.Count > 0)
                        whereStr2 = whereStr1 + " and dbo.f_GetUserCode(UserId) like '%" + dataTable2.Rows[0]["Id"] + "%'";
                    else
                        whereStr2 = whereStr1 + "1<>1";
                }
                this.doh.Reset();
                this.doh.ConditionExpress = whereStr2;
                totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
                string str6 = str5 + "select * from (" + "SELECT ' 全部合计' as [STime]\r\n                            ,isnull(sum(Charge),0) Charge\r\n                            ,isnull(sum(getcash),0) getcash\r\n                            ,isnull(sum(bet),0) bet\r\n                            ,isnull(sum(win),0) win\r\n                            ,isnull(sum(Point),0) Point\r\n                            ,isnull(sum(TranAccIn),0) TranAccIn\r\n                            ,isnull(sum(TranAccOut),0) TranAccOut\r\n                            ,isnull(sum(Give),0) Give\r\n                            ,isnull(sum(other),0) other\r\n                            ,isnull(sum(agentFH),0) agentFH\r\n                            ,isnull(sum(total),0) total\r\n                            ,isnull(sum(moneytotal),0) moneytotal\r\n                            FROM [V_UserMoneyStatAllDayOfUser] where " + whereStr2 + " union all " + SqlHelp.GetSql0("STime,sum(Charge) as Charge,sum(getcash) as getcash,sum(bet) as bet,sum(win) as win,sum(Point) as Point,sum(TranAccIn) as TranAccIn,sum(TranAccOut) as TranAccOut,sum(Give) as Give,sum(other) as other,sum(agentFH) as agentFH,sum(total) as total,sum(moneytotal) as moneytotal", "V_UserMoneyStatAllDayOfUser", FldName, num2, num1, "desc", whereStr2, "STime") + " ) A order by STime desc";
                this.doh.Reset();
                this.doh.SqlCmd = str6;
                dataTable1 = this.doh.GetDataTable();
            }
            else
            {
                this.doh.Reset();
                this.doh.ConditionExpress = whereStr1;
                totalCount = this.doh.Count("V_UserMoneyStatAllDay");
                string str6 = str5 + "select * from (" + "SELECT ' 全部合计' as [STime]\r\n                            ,isnull(sum(Charge),0) Charge\r\n                            ,isnull(sum(getcash),0) getcash\r\n                            ,isnull(sum(bet),0) bet\r\n                            ,isnull(sum(win),0) win\r\n                            ,isnull(sum(Point),0) Point\r\n                            ,isnull(sum(TranAccIn),0) TranAccIn\r\n                            ,isnull(sum(TranAccOut),0) TranAccOut\r\n                            ,isnull(sum(Give),0) Give\r\n                            ,isnull(sum(other),0) other\r\n                            ,isnull(sum(agentFH),0) agentFH\r\n                            ,isnull(sum(total),0) total\r\n                            ,isnull(sum(moneytotal),0) moneytotal\r\n                            FROM [V_UserMoneyStatAllDay] where " + whereStr1 + " union all " + SqlHelp.GetSql0("*", "V_UserMoneyStatAllDay", FldName, num2, num1, "desc", whereStr1) + " ) A order by STime desc";
                this.doh.Reset();
                this.doh.SqlCmd = str6;
                dataTable1 = this.doh.GetDataTable();
            }
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable1) + "}";
            dataTable1.Clear();
            dataTable1.Dispose();
        }

        private void ajaxGetListRank()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            string str3 = this.q("u");
            string str4 = this.q("order");
            string str5 = this.q("orderby");
            this.Int_ThisPage();
            this.Str2Int(this.q("pagesize"), 20);
            if (str1.Trim().Length == 0)
                str1 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            if (string.IsNullOrEmpty(str4))
                str4 = "total";
            if (string.IsNullOrEmpty(str5))
                str5 = "asc";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select * from (");
            stringBuilder.Append(" SELECT userId,dbo.f_GetUserName(userId) as userName,");
            stringBuilder.Append(" (select money from N_User with(nolock) where Id=a.userId) as userMoney,");
            stringBuilder.Append(" (SELECT count(Id) FROM [N_UserBet] where userId=a.userId and (State=3 or State=4) and STime >='{2}' and STime <'{3}') as winNum,");
            stringBuilder.Append(" isnull(sum(Charge),0) as Charge,");
            stringBuilder.Append(" isnull(sum(getcash),0) as getcash,");
            stringBuilder.Append(" (isnull(sum(Bet),0)) as bet,");
            stringBuilder.Append(" isnull(sum(Win),0)as win,");
            stringBuilder.Append(" isnull(sum(Cancellation),0) as Cancellation,");
            stringBuilder.Append(" isnull(sum(Point),0) as Point,");
            stringBuilder.Append(" isnull(sum(Give),0) as tranaccin,");
            stringBuilder.Append(" isnull(sum(Give),0) as tranaccout,");
            stringBuilder.Append(" isnull(sum(Give),0) as Give,");
            stringBuilder.Append(" isnull(sum(other),0) as other,");
            stringBuilder.Append(" isnull(sum(agentFH),0) as agentFH,");
            stringBuilder.Append(" (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total");
            stringBuilder.Append(" ,(isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal");
            stringBuilder.Append(" From N_UserMoneyStatAll a with(nolock)");
            stringBuilder.Append(" Where STime >='{2}' and STime <'{3}'");
            if (!string.IsNullOrEmpty(str3))
                stringBuilder.Append(" and dbo.f_GetUserName(UserId) = '" + str3 + "'");
            stringBuilder.Append(" group by userId ) A");
            stringBuilder.Append(" order by {0} {1}");
            this.doh.Reset();
            this.doh.SqlCmd = string.Format(stringBuilder.ToString(), (object)str4, (object)str5, (object)str1, (object)str2);
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListIuss()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            string str3 = this.q("lid");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string str4 = "STime >='" + str1 + "' and STime <'" + str2 + "' and state<>0 and state<>1";
            int totalCount;
            DataTable dataTable;
            if (!string.IsNullOrEmpty(str3))
            {
                string whereStr = str4 + " and LotteryId = " + str3;
                this.doh.Reset();
                this.doh.SqlCmd = "select IssueNum FROM [N_UserBet] where " + whereStr + " group by IssueNum";
                totalCount = this.doh.GetDataTable().Rows.Count;
                string sqlRow = SqlHelp.GetSqlRow("\r\n                    LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,sum(Total*Times) as bet,\r\n                    sum(WinBonus) as win,sum(-RealGet) as total\r\n                    ,isnull(sum(num),0)  as num\r\n                    ,isnull(sum(winnum),0)  as winnum\r\n                    ,isnull(cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)),0) as per\r\n                    ,isnull(sum(PointMoney),0)  as point ", "N_UserBet a", "IssueNum", num2, num1, "desc", whereStr, "LotteryId,IssueNum");
                this.doh.Reset();
                this.doh.SqlCmd = sqlRow;
                dataTable = this.doh.GetDataTable();
            }
            else
            {
                this.doh.Reset();
                this.doh.ConditionExpress = "";
                totalCount = this.doh.Count("V_UserMoneyStatAllIuss");
                string sql0 = SqlHelp.GetSql0("*", "V_UserMoneyStatAllIuss", "LotteryId", num2, num1, "asc", "");
                this.doh.Reset();
                this.doh.SqlCmd = sql0;
                dataTable = this.doh.GetDataTable();
            }
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListPlay()
        {
            string str1 = this.q("lid");
            string str2 = this.q("d1");
            string str3 = this.q("d2");
            string str4 = this.q("u");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str2.Trim().Length == 0)
                str2 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
            if (str3.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str3 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str2) > Convert.ToDateTime(str3))
                str2 = str3;
            string str5 = "";
            string whereStr = "STime2 >='" + str2 + "' and STime2 <'" + str3 + "' and state>1";
            if (!string.IsNullOrEmpty(str1))
                whereStr = whereStr + " and LotteryId = " + str1;
            if (!string.IsNullOrEmpty(str4))
                whereStr = whereStr + " and Title = '" + str4 + "'";
            this.doh.Reset();
            this.doh.SqlCmd = "select PlayId FROM [N_UserBet] where " + whereStr + " group by PlayId";
            int count = this.doh.GetDataTable().Rows.Count;
            string str6 = !string.IsNullOrEmpty(str1) ? LotteryUtils.LotteryTitle(Convert.ToInt32(str1)) : "全部游戏";
            string str7 = str5 + " SELECT '99999' as rowNember,'-1' as [LotteryId],'全部合计' as [LotteryName],'999' as PlayId,'' as Title,\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total FROM [N_UserBet] where " + whereStr + " union all " + SqlHelp.GetSqlRow("'" + str1 + "' as LotteryId,'" + str6 + "' as LotteryName,PlayId,(select titleName from Sys_PlaySmallType where Id=PlayId) as Title,cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total", "N_UserBet", "PlayId", num2, num1, "asc", whereStr, "PlayId");
            this.doh.Reset();
            this.doh.SqlCmd = str7;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, count, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListLottery()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string str3 = "";
            string whereStr = "STime >='" + str1 + "' and STime <'" + str2 + "' and state>=2";
            this.doh.Reset();
            this.doh.SqlCmd = "select LotteryId FROM [N_UserBet] where " + whereStr + " group by LotteryId";
            int count = this.doh.GetDataTable().Rows.Count;
            string str4 = str3 + " SELECT '99999' as rowNember,'9999' as [LotteryId],'全部合计' as [Title],\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total FROM [N_UserBet] where " + whereStr + " union all " + SqlHelp.GetSqlRow("LotteryId,(select title from Sys_Lottery where Id=LotteryId) as Title,\r\n            cast(round(isnull(sum(Total*Times),0),4) as numeric(18,4)) as Bet ,isnull(sum(WinBonus),0) as Win,\r\n            isnull(sum(num),0) as Num,isnull(sum(winnum),0) as WinNum,\r\n            cast(round(CONVERT(float,isnull(sum(WinBonus),0))*100/CONVERT(float,isnull(sum(Total*Times),1)) ,4) as numeric(9,4)) as Per,\r\n            isnull(sum(PointMoney),0) as Point,isnull(sum(-RealGet),0) as total", "N_UserBet", "LotteryId", num2, num1, "asc", whereStr, "LotteryId");
            this.doh.Reset();
            this.doh.SqlCmd = str4;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, count, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListTeamSale()
        {
            string startTime = this.q("d1");
            string endTime = this.q("d2");
            string group = this.q("group");
            string userName = this.q("u");

            if (startTime.Trim().Length == 0)
            {
                startTime = this.StartTime;
            }
            if (endTime.Trim().Length == 0)
            {
                endTime = this.EndTime;
            }
            if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
            {
                startTime = endTime;
            }

            string sql = @"SELECT a.[Id], a.[UserName],a.point as userpoint,
                                        isnull(sum(Charge),0) as Charge,
                                        isnull(sum(getcash),0) as getcash,
                                        isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as bet,
                                        isnull(sum(Win),0) as Win,
                                        isnull(sum(b.Point),0) as Point,
                                        isnull(sum(Give),0) as Give,
                                        isnull(sum(other),0) as other,
                                        isnull(sum(agentFH),0) as agentFH,
                                        isnull(sum(TranAccIn),0) as TranAccIn,
                                        isnull(sum(TranAccOut),0) as TranAccOut,
                                        (isnull(sum(Win),0)+isnull(sum(b.Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,
                                        (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal
                                        FROM [V_User] a left join [N_UserMoneyStatAll] b on dbo.f_GetUserCode(b.UserId) like '%,'+Convert(varchar(10),a.Id)+',%' ";
            sql += " where b.STime>='" + startTime + "' and b.STime <'" + endTime + "'";
            sql += (userName.Trim().Length <= 0 ? " and UserName = ''" : " and UserName = '" + userName + "'");
            sql += " group by a.Id,a.UserName,a.point";

            this.doh.Reset();
            this.doh.SqlCmd = sql;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetAllInfo()
        {
            this.Int_ThisPage();
            this.Str2Int(this.q("pagesize"), 20);
            string str = "SELECT \r\n                            Convert(Varchar(10),getdate(),120) as Date,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(dd,regTime,getdate())=0) as regToday,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(dd,regTime,getdate())=1) as regYesterday,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0 and DateDiff(MM,regTime,getdate())=0) as regMonth,\r\n                            (SELECT count(Id) FROM [N_User] where IsDel=0) as sum,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1) as sumOnline,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1 and Source=1) as sumIosOnline,\r\n                            (SELECT count(Id) FROM [Flex_User] where IsDel=0 and IsOnline=1 and Source=0) as sumPcOnline,\r\n                            Isnull(sum(money),0) as Money\r\n                            FROM [N_User] where IsDel=0";
            this.doh.Reset();
            this.doh.SqlCmd = str;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetListMonth()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            string str3 = this.q("code");
            string str4 = this.q("u");
            string FldName = this.q("order");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.StartTime);
                dateTime = dateTime.AddDays(-30.0);
                str1 = dateTime.ToString("yyyy-MM-dd");
            }
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            if (string.IsNullOrEmpty(FldName))
                FldName = "STime";
            string whereStr1 = " STime >='" + str1 + "' and STime <'" + str2 + "'";
            if (string.IsNullOrEmpty(str3))
                str3 = "0";
            int totalCount;
            DataTable dataTable1;
            if (!string.IsNullOrEmpty(str4))
            {
                string whereStr2;
                if (str3 == "0")
                {
                    whereStr2 = whereStr1 + " and dbo.f_GetUserName(UserId) = '" + str4 + "'";
                }
                else
                {
                    this.doh.Reset();
                    this.doh.SqlCmd = "select top 1 Id from N_User where UserName='" + str4 + "'";
                    DataTable dataTable2 = this.doh.GetDataTable();
                    if (dataTable2.Rows.Count > 0)
                        whereStr2 = whereStr1 + " and dbo.f_GetUserCode(UserId) like '%" + dataTable2.Rows[0]["Id"] + "%'";
                    else
                        whereStr2 = whereStr1 + "1<>1";
                }
                this.doh.Reset();
                this.doh.ConditionExpress = whereStr2;
                totalCount = this.doh.Count("V_UserMoneyStatAllMonthOfUser");
                string sql0 = SqlHelp.GetSql0("*", "V_UserMoneyStatAllMonthOfUser", FldName, num2, num1, "desc", whereStr2);
                this.doh.Reset();
                this.doh.SqlCmd = sql0;
                dataTable1 = this.doh.GetDataTable();
            }
            else
            {
                this.doh.Reset();
                this.doh.ConditionExpress = whereStr1;
                totalCount = this.doh.Count("V_UserMoneyStatAllMonth");
                string sql0 = SqlHelp.GetSql0("*", "V_UserMoneyStatAllMonth", FldName, num2, num1, "desc", whereStr1);
                this.doh.Reset();
                this.doh.SqlCmd = sql0;
                dataTable1 = this.doh.GetDataTable();
            }
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable1) + "}";
            dataTable1.Clear();
            dataTable1.Dispose();
        }

        private void ajaxGetListCheck()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            string str3 = this.q("group");
            string str4 = this.q("point");
            string str5 = this.q("sel1");
            string str6 = this.q("bet");
            this.q("u");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.StartTime);
                dateTime = dateTime.AddDays(-30.0);
                str1 = dateTime.ToString("yyyy-MM-dd");
            }
            if (str2.Trim().Length == 0)
            {
                DateTime dateTime = Convert.ToDateTime(this.EndTime);
                dateTime = dateTime.AddDays(1.0);
                str2 = dateTime.ToString("yyyy-MM-dd");
            }
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string str7 = " STime >='" + str1 + "' and STime <'" + str2 + "'";
            string whereStr = "1=1";
            if (!string.IsNullOrEmpty(str3))
                whereStr = whereStr + "and UserGroup=" + str3;
            if (!string.IsNullOrEmpty(str4))
                whereStr = whereStr + "and point=" + (object)(Convert.ToDecimal(str4) * new Decimal(10));
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("Flex_User");
            string str8 = "select * from (" + SqlHelp.GetSql0("'" + str1 + "' as starttime,'" + str2 + "' as endtime,[Id],[UserName],UserGroup,[UserGroupName],[Point],[RegTime],[LastTime],(SELECT isnull(sum([Bet]),0)-isnull(sum(Cancellation),0) FROM [N_UserMoneyStatAll] where " + str7 + " and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code(a.Id)+'%') as Bet", "Flex_User a", "Id", num2, num1, "asc", whereStr) + " ) A";
            if (!string.IsNullOrEmpty(str6))
            {
                if (str5.Equals("0"))
                    str8 = str8 + " where bet<" + (object)Convert.ToDecimal(str6);
                if (str5.Equals("1"))
                    str8 = str8 + " where bet>" + (object)Convert.ToDecimal(str6);
            }
            this.doh.Reset();
            this.doh.SqlCmd = str8;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }
    }
}
