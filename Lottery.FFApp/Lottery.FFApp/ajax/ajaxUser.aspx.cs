// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxUser
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.Collect;
using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace Lottery.WebApp
{
    public partial class ajaxUser : UserCenterSession
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("master", "json");
            this._operType = this.q("oper");
            switch (this._operType)
            {
                case "changepass":
                    this.ajaxChangeUserPwd();
                    break;
                case "moneypass":
                    this.ajaxChangeMoneyPwd();
                    break;
                case "ajaxGetList":
                    this.ajaxGetList();
                    break;
                case "ajaxGetTotalList":
                    this.ajaxGetTotalList();
                    break;
                case "ajaxGetTeamTotalList":
                    this.ajaxGetTeamTotalList();
                    break;
                case "ajaxGetTeamType":
                    this.ajaxGetTeamType();
                    break;
                case "ajaxRegiter":
                    this.ajaxRegiter();
                    break;
                case "ajaxGetRegStrList":
                    this.ajaxGetRegStrList();
                    break;
                case "ajaxRegStr":
                    this.ajaxRegStr();
                    break;
                case "ajaxRegStrAll":
                    this.ajaxRegStrAll();
                    break;
                case "ajaxVerify":
                    this.ajaxVerify();
                    break;
                case "ajaxVerifyExist":
                    this.ajaxVerifyExist();
                    break;
                case "saveTrueName":
                    this.saveTrueName();
                    break;
                case "saveEmail":
                    this.saveEmail();
                    break;
                case "saveMobile":
                    this.saveMobile();
                    break;
                case "ajaxGetFKListOnLine":
                    this.ajaxGetFKListOnLine();
                    break;
                case "ajaxGetFKProListSub":
                    this.ajaxGetFKProListSub();
                    break;
                case "ajaxGetUserGroupList":
                    this.ajaxGetUserGroupList();
                    break;
                case "ajaxGetUserPointList":
                    this.ajaxGetUserPointList();
                    break;
                case "GetUserJson":
                    this.GetUserJson();
                    break;
                case "ajaxGetContractUserList": //契约下级
                    this.ajaxGetContractUserList();
                    break;
                default:
                    this.DefaultResponse();
                    break;
            }
            this.Response.Write(this._response);
        }

        /// <summary>
        /// 获取契约下级
        /// </summary>
        private void ajaxGetContractUserList()
        {
            //用户名
            string username = this.q("username").Trim();
            this.Str2Int(this.q("gId"), 0);
            int _thispage = this.Int_ThisPage();
            int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string userid = this.q("Id"); //用户Id
            StringBuilder _wherestr1 = new StringBuilder();

            if (username.Length <= 0)
            {
                _wherestr1.Append(string.IsNullOrEmpty(userid) ? "parentId=" + this.AdminId : "parentId=" + userid);
            }
            else
            {
                _wherestr1.Append("dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%'");
                _wherestr1.Append(" and Id<>" + this.AdminId);
                _wherestr1.Append(" and UserName LIKE '" + username + "%'");
            }

            //用户分组
            //_wherestr1.Append(" and UserGroup in (2, 3)");

            string _jsonstr = "";
            new WebAppListOper().GetUserListJSON(this.AdminId, _thispage, _pagesize, _wherestr1.ToString(), "asc", "Id", ref _jsonstr);
            this._response = _jsonstr;
        }
        private void DefaultResponse()
        {
            this._response = this.JsonResult(0, "未知操作");
        }

        public void GetUserJson()
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") == null)
                return;
            string str = Public.GetUserJson(Convert.ToInt32(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"))).Replace("[", "").Replace("]", "");
            this._response = string.IsNullOrEmpty(str) ? "{\"result\":\"0\",\"table\": []}" : "{\"result\":\"1\",\"table\": [" + str + "]}";
        }

        private void ajaxRegiter()
        {
            string _UserGroup = this.f("type");
            string _UserName = this.f("name");
            string _Password = this.f("pwd");
            string _Point = this.f("point");
            if (Convert.ToInt32(_UserGroup) == 2 && Convert.ToDouble(_Point) != 130.0)
                this._response = this.GetJsonResult2(0, "直属只能开13.0的账号！");
            else if (Convert.ToInt32(_UserGroup) == 3 && Convert.ToDouble(_Point) != 130.0)
                this._response = this.GetJsonResult2(0, "特权直属只能开13.0的账号！");
            else if (Convert.ToInt32(_UserGroup) == 4 && Convert.ToDouble(_Point) != 130.0)
            {
                this._response = this.GetJsonResult2(0, "招商只能开13.0的账号！");
            }
            else
            {
                this.doh.Reset();
                this.doh.SqlCmd = "select UserGroup,Point as Upoint from N_User with(nolock) where Id=" + this.AdminId;
                DataTable dataTable1 = this.doh.GetDataTable();
                if (Convert.ToDouble(dataTable1.Rows[0]["Upoint"]) > 130.0)
                {
                    if (Convert.ToDouble(_Point) >= Convert.ToDouble(dataTable1.Rows[0]["Upoint"]))
                    {
                        this._response = this.GetJsonResult2(0, "您的账号不能开平级账号！");
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDouble(_Point) > Convert.ToDouble(dataTable1.Rows[0]["Upoint"]))
                    {
                        this._response = this.GetJsonResult2(0, "返点不能大于您的返点！");
                        return;
                    }
                    if (Convert.ToInt32(_UserGroup) < 2)
                    {
                        string str = string.Format("SELECT *,(select count(*) from N_User where ParentID={0} AND UserGroup<2 and Point=a.Point*10) as regNums \r\n                            From [N_UserPointQuota] a with(nolock)  \r\n                            Where [Point]={1}", (object)this.AdminId, (object)dataTable1.Rows[0]["Upoint"].ToString());
                        this.doh.Reset();
                        this.doh.SqlCmd = str;
                        DataTable dataTable2 = this.doh.GetDataTable();
                        if (dataTable2.Rows.Count > 0 && Convert.ToDouble(dataTable2.Rows[0]["ChildNums"]) <= Convert.ToDouble(dataTable2.Rows[0]["RegNums"]))
                        {
                            this._response = this.GetJsonResult2(0, "您选择的返点平级配额不足！");
                            return;
                        }
                    }
                }
                string str1 = string.Format("SELECT *,(select count(*) from N_User where ParentID={0} AND UserGroup=a.[toGroup]) as regNums \r\n                            From [N_UserGroupQuota] a with(nolock)  \r\n                            Where [Group]={1} and [ToGroup]={2}", (object)this.AdminId, (object)dataTable1.Rows[0]["UserGroup"].ToString(), (object)_UserGroup);
                this.doh.Reset();
                this.doh.SqlCmd = str1;
                DataTable dataTable3 = this.doh.GetDataTable();
                if (dataTable3.Rows.Count > 0 && Convert.ToDouble(dataTable3.Rows[0]["ChildNums"]) <= Convert.ToDouble(dataTable3.Rows[0]["RegNums"]))
                    this._response = this.GetJsonResult2(0, "您选择的开户类型配额不足！");
                else
                    this._response = new Lottery.DAL.Flex.UserDAL().Register(this.AdminId, _UserGroup, _UserName, _Password, _Point);
            }
        }

        private void ajaxGetList()
        {
            string str1 = this.q("username");
            string str2 = this.q("money1");
            string str3 = this.q("money2");
            string str4 = this.q("online").Replace(",", "");
            this.Str2Int(this.q("gId"), 0);
            int _thispage = this.Int_ThisPage();
            int _pagesize = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string str5 = this.q("Id");
            string str6 = this.q("group");
            string str7 = "dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
            string _wherestr1 = str1.Trim().Length <= 0 ? (string.IsNullOrEmpty(str5) ? "parentId=" + this.AdminId : "parentId=" + str5) : str7 + " and UserName LIKE '" + str1 + "%'";
            if (!string.IsNullOrEmpty(str6))
                _wherestr1 = _wherestr1 + " and UserGroup <" + str6;
            if (!string.IsNullOrEmpty(str2))
                _wherestr1 = _wherestr1 + " and Money <=" + str2;
            if (!string.IsNullOrEmpty(str3))
                _wherestr1 = _wherestr1 + " and Money >=" + str3;
            if (!string.IsNullOrEmpty(str4))
                _wherestr1 = _wherestr1 + " and IsOnline =" + str4;
            string _jsonstr = "";
            new WebAppListOper().GetUserListJSON(this.AdminId, _thispage, _pagesize, _wherestr1, "asc", "Id", ref _jsonstr);
            this._response = _jsonstr;
        }

        private void ajaxGetTotalList()
        {
            int PageIndex = this.Int_ThisPage();
            int PageSize = this.Str2Int(this.q("pagesize"), 20);
            string whereStr = "userId =" + this.AdminId;
            string sql0 = SqlHelp.GetSql0("[sort],\r\n                                                    [Name],\r\n                                                    isnull(sum(Charge),0) as Charge,\r\n                                                    isnull(sum(getcash),0) as getcash, \r\n                                                    isnull(sum(bet),0) as bet ,\r\n                                                    isnull(sum(win),0) as win,\r\n                                                    isnull(sum(Point),0) as Point,\r\n                                                    isnull(sum(Give),0) as Give,\r\n                                                    isnull(sum(Change),0) as Change,\r\n                                                    isnull(sum(AgentFH),0) as AgentFH, \r\n                                                    isnull(sum(total),0) as total,\r\n                                                    isnull(sum(betno),0) as betno", "V_UserMoneyStatAllUserTotal", "sort", PageSize, PageIndex, "asc", whereStr, "Name,sort");
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// 如果开始时间和结束时间为空，默认查前3天的记录
        /// </remarks>
        private void ajaxGetTeamTotalList()
        {
            this.Int_ThisPage();
            this.Str2Int(this.q("pagesize"), 20);
            this.q("flag");
            string startTime = this.q("d1");
            string endTime = this.q("d2");
            string whereStr = "dbo.f_GetUserCode(userId) like '%" + Strings.PadLeft(this.AdminId) + "%'";


            if (startTime.Trim().Length < 10)
            {
                DateTime dateTime = DateTime.Now;
                dateTime = dateTime.AddDays(-3.0);
                startTime = dateTime.ToString("yyyy-MM-dd") + " 00:00:00";
            }
            else
            {
                startTime = startTime.Substring(0, 10) + " 00:00:00";
            }

            if (endTime.Trim().Length < 10)
            {
                endTime = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            }
            else
            {
                endTime = endTime.Substring(0, 10) + " 23:59:59";
            }

            if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
            {
                startTime = Convert.ToDateTime(endTime).AddDays(-1).ToString("yy-MM-dd HH:mm:dd");
            }

            whereStr = whereStr + " and STime >='" + startTime + "' and STime <='" + endTime + "'";

            string queryStr = @"SELECT TOP 1 isnull(sum(Charge),0) as Charge,
                            isnull(sum(getcash),0) as getcash, 
                            isnull(sum(bet),0)-isnull(sum(Cancellation),0) as bet ,
                            isnull(sum(win),0) as win,
                            isnull(sum(Point),0) as Point,
                            isnull(sum(Give),0) as Give,
                            isnull(sum(other),0) as other,
                            isnull(sum(-total),0) as total 
                            From N_UserMoneyStatAll with(nolock) 
                            Where " + whereStr;
            this.doh.Reset();
            this.doh.SqlCmd = queryStr;
            DataTable dataTable = this.doh.GetDataTable();

            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";

            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetTeamType()
        {
            string str = string.Format("SELECT \r\n                            Convert(varchar(10),STime,120) as STime,\r\n                            isnull(sum(Charge),0) as Charge,\r\n                            isnull(sum(getcash),0) as getcash, \r\n                            isnull(sum(Bet),0)-isnull(sum(Cancellation),0) as bet ,\r\n                            isnull(sum(Win),0) as win,\r\n                            isnull(sum(Point),0) as Point,\r\n                            isnull(sum(Give),0) as Give,\r\n                            isnull(sum(Change),0) as Change,\r\n                            isnull(sum(AgentFH),0) as AgentFH, \r\n                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-(isnull(sum(Bet),0)) as total,\r\n                            (SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and Convert(varchar(10),STime,120)=Convert(varchar(10),a.STime,120)) as betno\r\n                            FROM [N_UserMoneyStatAll] a with(nolock)\r\n                            where dbo.f_GetUserCode(userId) like '%{0}%' and Id<>{1} and convert(varchar(7),STime,120)=convert(varchar(7),getdate(),120) \r\n                            group by Convert(varchar(10),STime,120)", (object)Strings.PadLeft(this.AdminId), (object)this.AdminId);
            this.doh.Reset();
            this.doh.SqlCmd = str;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetUserGroupList()
        {
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.doh.Reset();
            this.doh.SqlCmd = "select UserGroup from N_User with(nolock) where Id=" + this.AdminId;
            string whereStr = "[Group]=" + this.doh.GetDataTable().Rows[0]["UserGroup"];
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("N_UserGroupQuota");
            string sql0 = SqlHelp.GetSql0(string.Format("row_number() over (order by ToGroup desc) as rowid\r\n                ,case [Group] when 0 then '会员' when 1 then '代理' when 2 then '直属' when 3 then '特权直属' when 4 then '招商' when 5 then '主管' when 6 then '管理' end as GroupName\r\n                ,case [ToGroup] when 0 then '会员' when 1 then '代理' when 2 then '直属' when 3 then '特权直属' when 4 then '招商' when 5 then '主管' when 6 then '管理' end as ToGroupName\r\n                ,(select count(*) from N_User where ParentID={0} AND UserGroup=a.[ToGroup]) as regNums,*", (object)this.AdminId), "[N_UserGroupQuota] a", "ToGroup", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, num2 * (num1 - 1)) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetUserPointList()
        {
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.doh.Reset();
            this.doh.SqlCmd = "select Point*0.1 as Upoint from N_User with(nolock) where Id=" + this.AdminId;
            string whereStr = "[Point]=" + this.doh.GetDataTable().Rows[0]["Upoint"];
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("N_UserPointQuota");
            string sql0 = SqlHelp.GetSql0(string.Format("row_number() over (order by Point desc) as rowid\r\n                ,(select count(*) from N_User where ParentID={0} AND UserGroup<2 and Point=a.Point*10) as regNums,*", (object)this.AdminId), "[N_UserPointQuota] a", "Point", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, num2 * (num1 - 1)) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetRegStrList()
        {
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            string whereStr = "UserId=" + this.AdminId;
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("N_UserRegLink");
            string sql0 = SqlHelp.GetSql0("row_number() over (order by Point desc) as rowid,*", "N_UserRegLink", "Point", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, num2 * (num1 - 1)) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxRegStr()
        {
            string str = this.f("point");
            string YxTime = this.f("yxtime");
            string Times = this.f("times");
            string encryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
            string Url = ConfigurationManager.AppSettings["RootUrl"].ToString() + "/register.aspx?u=" + this.EncryptDES(this.AdminId + "@" + str, encryptKey).Replace("+", "@");
            new UserRegLinkDAL().SaveUserRegLink(this.AdminId, Convert.ToDecimal(str), YxTime, Times, Url);
            this._response = this.JsonResult(1, "注册链接全部生成成功！");
        }

        private void ajaxRegStrAll()
        {
            this.doh.Reset();
            this.doh.SqlCmd = "select Point from N_User with(nolock) where Id=" + this.AdminId;
            DataTable dataTable1 = this.doh.GetDataTable();
            for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT Point FROM [N_UserLevel] where point>=100 and Point<" + (object)Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
                DataTable dataTable2 = this.doh.GetDataTable();
                for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
                {
                    string encryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
                    string Url = ConfigurationManager.AppSettings["RootUrl"].ToString() + "/register.aspx?u=" + this.EncryptDES(this.AdminId + "@" + dataTable2.Rows[index2]["Point"].ToString(), encryptKey).Replace("+", "@");
                    new UserRegLinkDAL().SaveUserRegLink(this.AdminId, Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Url);
                }
            }
            new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员生成注册链接！");
            this._response = this.JsonResult(1, "注册链接全部生成成功！");
        }

        private void ajaxChangeUserPwd()
        {
            if (new Lottery.DAL.UserDAL().ChangeUserPassword(this.AdminId, this.f("oldpass"), this.f("newpass")))
            {
                new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改登录密码！");
                this._response = this.JsonResult(1, "密码修改成功");
            }
            else
                this._response = this.JsonResult(0, "旧密码错误");
        }

        private void ajaxChangeMoneyPwd()
        {
            if (new Lottery.DAL.UserDAL().ChangePayPassword(this.AdminId, this.f("oldpass"), this.f("newpass")))
            {
                new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改银行密码！");
                this._response = this.JsonResult(1, "密码修改成功");
            }
            else
                this._response = this.JsonResult(0, "旧密码错误");
        }

        private void ajaxVerifyExist()
        {
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=@Id and Question<>'' and Answer<>''";
            this.doh.AddConditionParameter("@Id", (object)this.AdminId);
            if (this.doh.Exist("N_User"))
                this._response = this.JsonResult(1, "验证信息绑定成功");
            else
                this._response = this.JsonResult(0, "验证信息绑定失败");
        }

        private void ajaxVerify()
        {
            string str1 = this.f("question");
            string str2 = this.f("answer");
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=@Id";
            this.doh.AddConditionParameter("@Id", (object)this.AdminId);
            this.doh.AddFieldItem("Question", (object)str1);
            this.doh.AddFieldItem("Answer", (object)str2);
            this.doh.Update("N_User");
            new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定验证信息！");
            this._response = this.JsonResult(1, "验证信息绑定成功");
        }

        private void saveTrueName()
        {
            if (new Lottery.DAL.UserDAL().SaveUserName(this.AdminId, this.f("name")))
            {
                new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定真实姓名！");
                this._response = this.JsonResult(1, "真实姓名绑定成功");
            }
            else
                this._response = this.JsonResult(0, "真实姓名绑定失败");
        }

        private void saveEmail()
        {
            if (new Lottery.DAL.UserDAL().SaveEmail(this.AdminId, this.f("name")))
            {
                new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定邮箱！");
                this._response = this.JsonResult(1, "邮箱绑定成功");
            }
            else
                this._response = this.JsonResult(0, "邮箱绑定失败");
        }

        private void saveMobile()
        {
            if (new Lottery.DAL.UserDAL().SaveMobile(this.AdminId, this.f("name")))
            {
                new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定手机！");
                this._response = this.JsonResult(1, "手机绑定成功");
            }
            else
                this._response = this.JsonResult(0, "手机绑定失败");
        }

        private void ajaxGetFKListOnLine()
        {
            string str1 = this.q("username");
            string str2 = this.q("money1");
            string str3 = this.q("money2");
            string str4 = this.q("online").Replace(",", "");
            this.Str2Int(this.q("gId"), 0);
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            this.q("Id");
            string whereStr = "IsOnline=1 and dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId;
            if (str1.Trim().Length > 0)
                whereStr = whereStr + " and UserName LIKE '" + str1 + "%'";
            if (!string.IsNullOrEmpty(str2))
                whereStr = whereStr + " and Money <=" + str2;
            if (!string.IsNullOrEmpty(str3))
                whereStr = whereStr + " and Money >=" + str3;
            if (!string.IsNullOrEmpty(str4))
                whereStr = whereStr + " and IsOnline =" + str4;
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("Flex_User");
            string str5 = "";
            this.doh.Reset();
            this.doh.SqlCmd = SqlHelp.GetSql0("Id,UserCode", "Flex_User", "ID", num2, num1, "asc", whereStr);
            DataTable dataTable1 = this.doh.GetDataTable();
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
            {
                str5 += string.Format("select *,'" + this.ajaxGetUserNames(dataTable1.Rows[index]["UserCode"].ToString(), this.AdminId) + "' as usercodes from Flex_User where Id=" + dataTable1.Rows[index]["Id"].ToString());
                if (index != dataTable1.Rows.Count - 1)
                    str5 += " union all ";
            }
            if (!string.IsNullOrEmpty(str5))
            {
                this.doh.Reset();
                this.doh.SqlCmd = str5;
                DataTable dataTable2 = this.doh.GetDataTable();
                this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable2) + "}";
                dataTable2.Clear();
                dataTable2.Dispose();
            }
            else
                this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"recordcount\":0,\"table\": []}";
        }

        public void ajaxGetFKProListSub()
        {
            string str1 = this.q("d1") + " 00:00:00";
            string str2 = this.q("d2") + " 23:59:59";
            string userId = this.q("id"); //父级代理用户Id
            string userName = this.q("u"); //用户名
            string str4 = this.q("tid");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string str5 = " STime >='" + str1 + "' and STime <='" + str2 + "'";

            bool flag = true;
            if (string.IsNullOrEmpty(userId))
            {
                if (!string.IsNullOrEmpty(userName.Trim()))
                {
                    this.doh.Reset();
                    this.doh.SqlCmd = "select Id,usercode from N_User where UserName='" + userName + "'";
                    DataTable dataTable = this.doh.GetDataTable();
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows[0]["usercode"].ToString().Contains(this.AdminId))
                        {
                            userId = dataTable.Rows[0]["Id"].ToString();
                        }
                        else
                        {
                            userId = "-1";
                            flag = false;
                        }
                    }
                    else
                        flag = false;
                }
                else
                {
                    userId = this.AdminId;
                    flag = true;
                }
            }

            if (flag)
            {
                int num3 = 0;
                //查询用户的信息
                string str6 = string.Format(@"select {1} as totalcount, {0} as UserID,
                                (select Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2))) from N_User with(nolock) where Id={0} ) as userpoint,
                                dbo.f_GetUserName({0}) as userName,
                                (select isnull(sum(money),0) from N_User with(nolock) where Id = {0}) as money,
                                isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,
                                isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0) Bet,
                                isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,
                                isnull(sum(b.Cancellation),0) Cancellation,
                                isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,
                                isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,
                                (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,
                                (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal,
                                0 AS SubCount
                                from Flex_UserMoneyStatAll b with(nolock)
                                where {2} and UserId={0}", (object)userId, (object)num3, (object)str5) + " union all ";

                //会员总数
                this.doh.Reset();
                this.doh.ConditionExpress = " ParentId = " + userId;
                int totalCount = this.doh.Count("N_User");

                //遍历会员
                this.doh.Reset();
                this.doh.SqlCmd = SqlHelp.GetSql0("Id, UserName, Money, Point, (SELECT COUNT(1) FROM N_User WHERE ParentId=Id) AS SubCount", "N_User", "ID", num2, num1, "asc", " ParentId = " + userId);
                DataTable dataTable1 = this.doh.GetDataTable();

                for (int index = 0; index < dataTable1.Rows.Count; ++index)
                {
                    string str7 = str5 + " and UserCode like '%" + Strings.PadLeft(dataTable1.Rows[index]["Id"].ToString()) + "%'";
                    string subCount = dataTable1.Rows[index]["SubCount"].ToString();

                    if (!string.IsNullOrEmpty(str4))
                    {
                        this.doh.Reset();
                        this.doh.ConditionExpress = @"STime >='" + str1 + "' and STime <='" + str2 + "' and UserId=" + dataTable1.Rows[index]["Id"] + " and (Charge<>0 or GetCash<>0 or Bet<>0 or win<>0 or point<>0 or give<>0)";
                        if (this.doh.Count("Flex_UserMoneyStatAll") > 0)
                            str6 = str6 + string.Format(@"select {0} as totalcount, {1} as UserID,
                                Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,
                                '{3}' as userName,
                                isnull(sum({4}),0)  as money,
                                isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,
                                isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,
                                isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,
                                isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,
                                isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,
                                isnull(sum(b.Change),0) Change,
                                (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,
                                (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal,
                                (SELECT COUNT(1) FROM N_User WHERE ParentId = {1}) AS SubCount
                                from Flex_UserMoneyStatAll b with(nolock)
                                where {5}",
                                          (object)totalCount,
                                          (object)dataTable1.Rows[index]["Id"].ToString(),
                                          (object)dataTable1.Rows[index]["Point"].ToString(),
                                          (object)dataTable1.Rows[index]["UserName"].ToString(),
                                          (object)dataTable1.Rows[index]["Money"].ToString(), 
                                          (object)str7) + " union all ";
                    }
                    else
                        str6 = str6 + string.Format(@"select {0} as totalcount, {1} as UserID,
                                Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,
                                '{3}' as userName,
                                (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{1},%') as money,
                                isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,
                                isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,
                                isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,
                                isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,
                                isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,
                                isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,
                                (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,
                                (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal,
                                (SELECT COUNT(1) FROM N_User WHERE ParentId = {1}) AS SubCount
                                from Flex_UserMoneyStatAll b with(nolock)
                                where {5}",
                                          (object)totalCount,
                                          (object)dataTable1.Rows[index]["Id"].ToString(),
                                          (object)dataTable1.Rows[index]["Point"].ToString(),
                                          (object)dataTable1.Rows[index]["UserName"].ToString(),
                                          (object)dataTable1.Rows[index]["Money"].ToString(),
                                          (object)str7,
                                          subCount) + " union all ";
                }

                string str8 = str6 + string.Format(@"select {2} as totalcount, '-1' as UserID,'合计' as userpoint,'' as userName,
                                (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{0},%') as money,
                                isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,
                                isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,
                                isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,
                                isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,
                                isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,
                                isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,
                                (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,
                                (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal,
                                0 AS SubCount
                                FROM Flex_UserMoneyStatAll b with(nolock) where {1}",
                                                                                    (object)userId,
                                                                                    (object)(str5 + " and UserCode like '%" + Strings.PadLeft(userId) + "%'"),
                                                                                    (object)totalCount);
                this.doh.Reset();
                this.doh.SqlCmd = str8;
                DataTable dataTable2 = this.doh.GetDataTable();
                this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable2) + "}";
                dataTable2.Clear();
                dataTable2.Dispose();
            }
            else
                this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"recordcount\":0,\"table\": []}";
        }

        public string ajaxGetUserNames(string ucode, string Id)
        {
            string str1 = "";
            ucode = ucode.Substring(ucode.IndexOf(Id) - 1);
            string[] strArray = ucode.Replace(",,", "_").Replace(",", "").Split('_');
            string str2;
            if (strArray.Length > 0)
            {
                for (int index = 0; index < strArray.Length; ++index)
                {
                    if (!string.IsNullOrEmpty(strArray[index]))
                    {
                        this.doh.Reset();
                        this.doh.ConditionExpress = "Id=" + strArray[index];
                        str1 = str1 + this.doh.GetField("N_User", "UserName") + "->";
                    }
                }
                str2 = str1.Substring(0, str1.Length - 2);
            }
            else
                str2 = "---";
            return str2;
        }
    }
}
