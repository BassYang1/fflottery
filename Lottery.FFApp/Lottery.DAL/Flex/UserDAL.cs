// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;

namespace Lottery.DAL.Flex
{
    public class UserDAL : ComData
    {
        public void ClearSession()
        {
            Cookie.Del("UserId");
            Cookie.Del("UserName");
            Cookie.Del("UserPoint");
            Cookie.Del("SessionId");
        }

        public string UserInfo(string UserId, string SessionId)
        {
            if (!(UserId != "0") || !(SessionId != "0"))
                return this.GetJsonResult(0, "登陆已超时，请您重新登陆！");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT '1' as result,Id,Money,IsDel,IsEnable,sessionId\r\n                                    ,Convert(varchar(10),cast(round(Point/10.0,2) as numeric(10,2))) as Point\r\n                                    ,'0' as email\r\n                                    ,'0' as notice\r\n                                    FROM [N_User] a where Id={0} and sessionId='{1}'", (object)UserId, (object)SessionId);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["IsDel"].Equals((object)"1") || dataTable.Rows[0]["IsEnable"].Equals((object)"1"))
                        return this.GetJsonResult(0, "您的账户存在未知问题，请于客服联系！");
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "Id=@Id";
                    dbOperHandler.AddConditionParameter("@Id", (object)UserId);
                    dbOperHandler.AddFieldItem("ontime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IsOnline", (object)1);
                    dbOperHandler.Update("N_User");
                    dbOperHandler.Dispose();
                    return this.ConverTableToJSON(dataTable);
                }
                dbOperHandler.Dispose();
                return this.GetJsonResult(0, "登陆已超时，请您重新登陆");
            }
        }

        public string GetEmailCount(string UserId)
        {
            if (!(UserId != "0"))
                return this.GetJsonResult(0, "0");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "IsDelReceive=0 and IsRead=0 and ReceiveId=" + UserId;
                int num1 = dbOperHandler.CountId("N_UserEmail");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "IsRead=0 and UserId=" + UserId;
                int num2 = dbOperHandler.CountId("N_UserMessage");
                return this.GetJsonResult(1, string.Concat((object)(num1 + num2)));
            }
        }

        public string Login(string UserName, string UserPass)
        {
            UserName = UserName.ToLower().Replace("'", "");
            UserPass = MD5.Last64(MD5.Lower32(UserPass));
            string strValue = Guid.NewGuid().ToString().Replace("-", "");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 '1' as result,a.Id,ParentId,UserGroup,Convert(varchar(10),cast(round(Point/10.0,2) as numeric(10,2))) as Point,\r\n                                            UserName,Money,'{0}' as SessionId,LastTime,OnTime,IP,a.IsEnable,IsGetCash,IsBet,IsTranAcc,EnableSeason,LoginId,\r\n                                            case when b.Id is null then '0' else '1' end as IsBank,'0' as email,'0' as notice \r\n                                            FROM N_User a left join N_UserBank b on a.Id=b.UserId\r\n                                            where username='{1}' and password='{2}' and isDel=0", (object)strValue, (object)UserName, (object)UserPass);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
                        return this.GetJsonResult(0, "您的账户存在未知问题，请于客服联系！");
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
                        return this.GetJsonResult(0, "对不起，您的网络不稳定，请重新登录！！");
                    this.ClearSession();
                    Cookie.SetObj("UserId", dataTable.Rows[0]["Id"].ToString());
                    Cookie.SetObj("UserName", UserName);
                    Cookie.SetObj("UserPoint", dataTable.Rows[0]["Point"].ToString());
                    Cookie.SetObj("SessionId", strValue);
                    string clientIp = IPHelp.ClientIP;
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "Id=@Id";
                    dbOperHandler.AddConditionParameter("@Id", (object)dataTable.Rows[0]["Id"].ToString());
                    dbOperHandler.AddFieldItem("LastTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("ontime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IP", (object)clientIp);
                    dbOperHandler.AddFieldItem("sessionId", (object)strValue);
                    dbOperHandler.AddFieldItem("IsOnline", (object)1);
                    dbOperHandler.AddFieldItem("Source", (object)0);
                    dbOperHandler.Update("N_User");
                    dbOperHandler.Dispose();
                    IPScaner ipScaner = new IPScaner();
                    ipScaner.DataPath = HttpContext.Current.Server.MapPath("Data/qqwry.dat");
                    ipScaner.IP = clientIp;
                    string _address = ipScaner.IPLocation() + ipScaner.ErrMsg;
                    string _browser = HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
                    string osNameByUserAgent = this.GetOSNameByUserAgent(HttpContext.Current.Request.UserAgent);
                    new LogUserLoginDAL().Save(dataTable.Rows[0]["Id"].ToString(), _address, _browser, osNameByUserAgent, clientIp);
                    return this.ConverTableToJSON(dataTable);
                }
                dbOperHandler.Dispose();
                return this.GetJsonResult(0, "登录失败，用户名或密码错误！");
            }
        }

        public string Register(string _ParentId, string _UserGroup, string _UserName, string _Password, string _Point)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT Id FROM [N_User] WHERE [UserName]='" + _UserName.ToLower() + "'";
                if (dbOperHandler.GetDataTable().Rows.Count > 0)
                    return this.GetJsonResult(0, "账号已存在，请更换一个账号！");
                string str = MD5.Last64(MD5.Lower32(_Password));
                object[,] _vFields1 = new object[2, 6]
        {
          {
            (object) "ParentId",
            (object) "UserGroup",
            (object) "UserName",
            (object) "Password",
            (object) "Point",
            (object) "PayPass"
          },
          {
            (object) _ParentId,
            (object) _UserGroup,
            (object) _UserName.ToLower(),
            (object) str,
            (object) _Point,
            (object) MD5.Last64(MD5.Lower32("123456"))
          }
        };
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItems(_vFields1);
                int num = dbOperHandler.Insert("N_User");
                if (num <= 0)
                    return this.GetJsonResult(0, "添加会员失败！");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_ParentId);
                object field = dbOperHandler.GetField("N_User", "UserCode");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=" + (object)num;
                dbOperHandler.AddFieldItem("UserCode", (object)(field.ToString() + Strings.PadLeft(num.ToString())));
                dbOperHandler.Update("N_User");
                object[,] _vFields2 = new object[2, 2]
        {
          {
            (object) "UserId",
            (object) "Change"
          },
          {
            (object) num,
            (object) 0
          }
        };
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItems(_vFields2);
                dbOperHandler.Insert("N_UserMoneyStatAll");
                return this.GetJsonResult(1, "添加会员成功！");
            }
        }

        public void getUserPointListJson(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)UserId);
                object field = dbOperHandler.GetField("N_User", "Point");
                dbOperHandler.SqlCmd = "SELECT point,Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2)))+'%' as title FROM [N_UserLevel] where point>=100 and point<" + field;
                dbOperHandler.SqlCmd += " ORDER BY Bonus desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getUserUpPointListJson(string UserId, string MinPoint, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)UserId);
                object field = dbOperHandler.GetField("N_User", "Point");
                dbOperHandler.SqlCmd = "SELECT point,Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2)))+'%' as title FROM [N_UserLevel] where point<" + field + " and point>=" + (object)(Convert.ToDouble(MinPoint.Replace("%", "")) * 10.0);
                dbOperHandler.SqlCmd += " ORDER BY Bonus asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getSysBankJson(ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT Id,Bank from Sys_Bank where IsGetCash=0 and IsUsed=0";
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getChargeSysBankJson(ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT Id,Bank from Sys_Bank where IsCharge=0 and IsUsed=0";
                dbOperHandler.SqlCmd += " ORDER BY id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getChargeSetJson(string Type, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT Id,MerName from Sys_ChargeSet where IsUsed=0";
                dbOperHandler.SqlCmd += " ORDER BY sort asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getChargeSysBankByIdJson(string Id, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT * from Sys_Bank where Id=" + Id;
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getChargeSetByIdJson(string Id, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id=" + Id;
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void getSysBankBinByIdJson(string BankId, string BankBin, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT top 1 * from Sys_BankBinInfo where BankId=" + BankId + " and BankBin='" + BankBin + "'";
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = whereStr;
                string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Flex_User").ToString() + " as totalcount,row_number() over (order by Id asc) as rowid,*", "Flex_User", "Id", PSize, page, "asc", whereStr);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListOnlineJSON(int page, int PSize, string whereStr, string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = whereStr + " and UserCode like '%," + UserId + ",%'";
                int num = dbOperHandler.Count("N_User");
                string sql0 = SqlHelp.GetSql0(num.ToString() + " as totalcount,ID,UserName,UserCode,Money,LastTime", "N_User", "Id", PSize, page, "asc", whereStr + " and UserCode like '%," + UserId + ",%'");
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                string str1 = "";
                if (dataTable.Rows.Count > 0)
                {
                    for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
                    {
                        string str2 = string.Empty;
                        string str3 = dataTable.Rows[index1]["UserCode"].ToString().Replace(",,", "_").Replace(",", "");
                        string[] strArray = str3.Substring(str3.IndexOf(UserId)).Split('_');
                        if (strArray.Length > 0)
                        {
                            for (int index2 = 0; index2 < strArray.Length; ++index2)
                            {
                                if (!string.IsNullOrEmpty(strArray[index2]))
                                {
                                    dbOperHandler.Reset();
                                    dbOperHandler.ConditionExpress = "Id=" + strArray[index2];
                                    str2 = str2 + dbOperHandler.GetField("N_User", "UserName") + ">";
                                }
                            }
                            string str4 = str2.Substring(0, str2.Length - 1);
                            if (index1 != 0)
                                str1 += " union all ";
                            str1 = str1 + " select  " + (object)num + " as totalcount,row_number() over (order by Id asc) as rowid,ID,UserName,UserCode,Money,'" + str4 + "' as CodeName,LastTime from N_User  where  Id=" + dataTable.Rows[index1]["Id"];
                        }
                    }
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = str1;
                    dataTable = dbOperHandler.GetDataTable();
                }
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetUserInfoJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from V_UserInfo where Id=" + UserId;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel GetUserInfo(int userId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                UserModel user = null;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from V_User where Id=" + userId;
                DataTable table = dbOperHandler.GetDataTable();

                if (table != null && table.Rows.Count > 0)
                {
                    user = new UserModel();
                    user.Id = Convert.IsDBNull(table.Rows[0]["Id"]) ? 0 : (int)table.Rows[0]["Id"];
                    user.UserName = Convert.IsDBNull(table.Rows[0]["UserName"]) ? "" : table.Rows[0]["UserName"].ToString();
                    user.UserGroup = Convert.IsDBNull(table.Rows[0]["UserGroup"]) ? 0 : (int)table.Rows[0]["UserGroup"];
                    user.UserGroupName = Convert.IsDBNull(table.Rows[0]["UserGroupName"]) ? "" : table.Rows[0]["UserGroupName"].ToString();
                    user.ParentId = Convert.IsDBNull(table.Rows[0]["ParentId"]) ? 0 : (int)table.Rows[0]["ParentId"];
                }

                table.Clear();
                table.Dispose();

                return user;
            }
        }

        /// <summary>
        /// 是否是管理员用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsAdminUser(int userId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                bool isAdminUser = false;
                UserModel user = null;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select 1 from N_User where IsNull(ParentId, 0)=0 AND UserGroup in (5,6) AND Id=" + userId;
                DataTable table = dbOperHandler.GetDataTable();

                if (table != null && table.Rows.Count > 0)
                {
                    isAdminUser = true;
                }

                table.Clear();
                table.Dispose();

                return isAdminUser;
            }
        }

        public void GetMoneyJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select Money from N_User where Id=" + UserId;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetMsgJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 Id,title,Msg from N_UserMessage with(nolock) where IsRead=0 and UserId=" + UserId + " order by Id desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "Id=@Id";
                    dbOperHandler.AddConditionParameter("@Id", (object)dataTable.Rows[0]["Id"].ToString());
                    dbOperHandler.AddFieldItem("IsRead", (object)"1");
                    dbOperHandler.Update("N_UserMessage");
                    _jsonstr = this.ConverTableToJSON(dataTable);
                }
                else
                    _jsonstr = "[{\"title\":\"0\",\"msg\":\"0\"}]";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public int UpdateInfo(string UserId, string Question, string Answer)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=" + UserId;
                dbOperHandler.AddFieldItem("Question", (object)Question);
                dbOperHandler.AddFieldItem("Answer", (object)Answer);
                return dbOperHandler.Update("N_User");
            }
        }

        public int UpdatePoint(string UserId, string Point)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=" + UserId;
                dbOperHandler.AddFieldItem("Point", (object)Point);
                return dbOperHandler.Update("N_User");
            }
        }

        public string UpdateInfo(string UserId, string QQ, string Email, string Mobile)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=" + UserId;
                dbOperHandler.AddFieldItem("QQ", (object)QQ);
                dbOperHandler.AddFieldItem("Email", (object)Email);
                dbOperHandler.AddFieldItem("Mobile", (object)Mobile);
                if (dbOperHandler.Update("N_User") > 0)
                    return this.GetJsonResult(1, "基本信息保存成功！");
                return this.GetJsonResult(0, "基本信息保存失败！");
            }
        }

        public string ChangeUserPassword(string _userid, string _oldPassword, string _newPassword)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                object field = dbOperHandler.GetField("N_User", "PassWord");
                if (field == null)
                    return this.GetJsonResult(0, "原登录密码错误！");
                if (!(field.ToString().ToLower() == MD5.Last64(MD5.Lower32(_oldPassword))))
                    return this.GetJsonResult(0, "原登录密码错误！");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("PassWord", (object)MD5.Last64(MD5.Lower32(_newPassword)));
                dbOperHandler.AddFieldItem("IP", (object)Const.GetUserIp);
                if (dbOperHandler.Update("N_User") > 0)
                    return this.GetJsonResult(1, "登录密码修改成功！");
                return this.GetJsonResult(0, "登录密码修改失败！");
            }
        }

        public string ChangePayPassword(string _userid, string _oldPassword, string _newPassword)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                object field = dbOperHandler.GetField("N_User", "PayPass");
                if (field == null)
                    return this.GetJsonResult(0, "原取款密码错误！");
                if (!(field.ToString().ToLower() == MD5.Last64(MD5.Lower32(_oldPassword))))
                    return this.GetJsonResult(0, "原取款密码错误！");
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("PayPass", (object)MD5.Last64(MD5.Lower32(_newPassword)));
                dbOperHandler.AddFieldItem("IP", (object)Const.GetUserIp);
                if (dbOperHandler.Update("N_User") > 0)
                    return this.GetJsonResult(1, "取款密码修改成功！");
                return this.GetJsonResult(0, "取款密码修改失败！");
            }
        }

        public string ClearUserPassword(string _userid, string Password)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("PassWord", (object)MD5.Last64(MD5.Lower32(Password)));
                dbOperHandler.AddFieldItem("IP", (object)Const.GetUserIp);
                if (dbOperHandler.Update("N_User") > 0)
                    return this.GetJsonResult(1, "重置密码成功！");
                return this.GetJsonResult(0, "重置密码失败！");
            }
        }

        public string UserTranAcc(string Type, string UserId, string ToUserId, string Money, string PassWord)
        {
            if (Convert.ToDecimal(Money) < new Decimal(0))
                return this.GetJsonResult(0, "转账金额不正确！");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)UserId);
                object[] fields = dbOperHandler.GetFields("N_User", "Money,PayPass");
                if (fields.Length <= 0)
                    return this.GetJsonResult(0, "账号出现问题，请您重新登陆！");
                if (Convert.ToDecimal(Money) > Convert.ToDecimal(fields[0]))
                    return this.GetJsonResult(0, "您的可用余额不足");
                if (!MD5.Last64(MD5.Lower32(PassWord)).Equals(fields[1].ToString()))
                    return this.GetJsonResult(0, "您的取款密码错误");
                if (new UserChargeDAL().SaveUpCharge(Type, UserId, ToUserId, Convert.ToDecimal(Money)) <= 0)
                    return this.GetJsonResult(0, "转账失败！");
                new LogSysDAL().Save("会员管理", "Id为" + UserId + "的会员转账给Id为" + ToUserId + "的会员！");
                return this.GetJsonResult(1, "转账成功！");
            }
        }

        public void GetUserNameJSON(string UserName, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select '1' as result,Id,UserName,Question,Answer from N_User where UserName='" + UserName + "'";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = dataTable.Rows.Count <= 0 ? this.GetJsonResult(0, "用户不存在！") : this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetLoginListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Log_UserLogin").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "Log_UserLogin", "loginTime", _pagesize, _thispage, "desc", _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON2(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        private string GetOSNameByUserAgent(string userAgent)
        {
            return !userAgent.Contains("NT 10.0") ? (!userAgent.Contains("NT 6.3") ? (!userAgent.Contains("NT 6.2") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.0") ? (!userAgent.Contains("NT 5.2") ? (!userAgent.Contains("NT 5.1") ? (!userAgent.Contains("NT 5") ? (!userAgent.Contains("NT 4") ? (!userAgent.Contains("Me") ? (!userAgent.Contains("98") ? (!userAgent.Contains("95") ? (!userAgent.Contains("Mac") ? (!userAgent.Contains("Unix") ? (!userAgent.Contains("Linux") ? (!userAgent.Contains("SunOS") ? HttpContext.Current.Request.Browser.Platform : "SunOS") : "Linux") : "UNIX") : "Mac") : "Windows 95") : "Windows 98") : "Windows Me") : "Windows NT4") : "Windows 2000") : "Windows XP") : (!userAgent.Contains("64") ? "Windows Server 2003" : "Windows XP")) : "Windows Vista/Server 2008") : "Windows 7") : "Windows 7") : "Windows 8") : "Windows 8.1") : "Windows 10";
        }
    }
}
