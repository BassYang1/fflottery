// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
    public class UserDAL : ComData
    {
        protected SiteModel site;

        public UserDAL()
        {
            this.site = new conSite().GetSite();
        }

        public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string orderby, string order, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                int totalCount = dbOperHandler.Count("V_User");
                string sql0 = SqlHelp.GetSql0("*", "V_User", order, _pagesize, _thispage, orderby, _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetListJSON2(int _thispage, int _pagesize, string _wherestr1, string orderby, string order, string pid, string uid, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                int totalCount = dbOperHandler.Count("V_User");
                string sql0 = SqlHelp.GetSql0(pid + " as pid,*", "V_User", order, _pagesize, _thispage, orderby, _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"nav\" :\"" + this.GetUserNav(pid, uid) + "\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public int Register(string _ParentId, string _UserName, string _Password, Decimal _Point)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                string str = MD5.Last64(_Password);
                object[,] _vFields1 = new object[2, 5]
        {
          {
            (object) "ParentId",
            (object) "UserName",
            (object) "Password",
            (object) "Point",
            (object) "PayPass"
          },
          {
            (object) _ParentId,
            (object) _UserName,
            (object) str,
            (object) _Point,
            (object) MD5.Last64(MD5.Lower32("123456"))
          }
        };
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItems(_vFields1);
                int num = dbOperHandler.Insert("N_User");
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
                return num;
            }
        }

        public string ChkLogin(string _adminname, string _adminpass, int iExpires)
        {
            _adminname = _adminname.Replace("'", "");
            string str1 = MD5.Last64(_adminpass);
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                //str1 = "4f5d4bb4a98a1b7b589833d832ff21664e22c970afb770375bf750cd3b88658c"; //admin
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("select top 1 Id,Point,IsEnable from N_User with(nolock) where username='{0}' and password='{1}' and isDel=0", (object)_adminname, (object)str1);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
                        return this.JsonResult(0, "您的账户存在未知问题，请于客服联系！");
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
                        return this.JsonResult(0, "对不起，您的网络不稳定，请重新登录！");
                    string str2 = Guid.NewGuid().ToString().Replace("-", "");
                    Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
          {
            {
              "id",
              dataTable.Rows[0]["Id"].ToString()
            },
            {
              "name",
              _adminname
            },
            {
              "cookiess",
              str2
            },
            {
              "point",
              dataTable.Rows[0]["Point"].ToString()
            }
          }, this.site.CookieDomain, this.site.CookiePath);
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "Id=@Id and IsEnable=0";
                    dbOperHandler.AddConditionParameter("@Id", (object)dataTable.Rows[0]["Id"].ToString());
                    dbOperHandler.AddFieldItem("LastTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IP", (object)IPHelp.ClientIP);
                    dbOperHandler.AddFieldItem("sessionId", (object)str2);
                    dbOperHandler.AddFieldItem("IsOnline", (object)1);
                    dbOperHandler.AddFieldItem("Source", (object)1);
                    dbOperHandler.Update("N_User");
                    dbOperHandler.Dispose();
                    return dataTable.Rows[0]["Id"].ToString();
                }
                dbOperHandler.Dispose();
                return this.JsonResult(0, "会员账号或密码错误！");
            }
        }

        public string ChkLoginWebApp(string _adminname, string _adminpass, int iExpires)
        {
            _adminname = _adminname.Replace("'", "");
            string str1 = MD5.Last64(_adminpass);
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                //str1 = "4f5d4bb4a98a1b7b589833d832ff21664e22c970afb770375bf750cd3b88658c"; //admin
                dbOperHandler.SqlCmd = string.Format("select top 1 Id,Point,IsEnable from N_User with(nolock) where username='{0}' and password='{1}' and isDel=0", (object)_adminname, (object)str1);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 1)
                        return this.JsonResult(0, "您的账户存在未知问题，请于客服联系！");
                    if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"].ToString()) == 2)
                        return this.JsonResult(0, "对不起，您的网络不稳定，请重新登录！");
                    string str2 = Guid.NewGuid().ToString().Replace("-", "");
                    Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
          {
            {
              "id",
              dataTable.Rows[0]["Id"].ToString()
            },
            {
              "name",
              _adminname
            },
            {
              "cookiess",
              str2
            },
            {
              "point",
              dataTable.Rows[0]["Point"].ToString()
            }
          }, this.site.CookieDomain, this.site.CookiePath);
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "Id=@Id and IsEnable=0";
                    dbOperHandler.AddConditionParameter("@Id", (object)dataTable.Rows[0]["Id"].ToString());
                    dbOperHandler.AddFieldItem("LastTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IP", (object)IPHelp.ClientIP);
                    dbOperHandler.AddFieldItem("sessionId", (object)str2);
                    dbOperHandler.AddFieldItem("IsOnline", (object)1);
                    dbOperHandler.AddFieldItem("Source", (object)0);
                    dbOperHandler.Update("N_User");
                    dbOperHandler.Dispose();
                    return dataTable.Rows[0]["Id"].ToString();
                }
                dbOperHandler.Dispose();
                return this.JsonResult(0, "会员账号或密码错误！");
            }
        }

        public string ChkAutoLoginWebApp(string _Id, string _sessionId, int iExpires)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("select top 1 UserName,Point,sessionId from N_User with(nolock) where Id={0}", (object)_Id);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["sessionId"])))
                    {
                        Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
            {
              {
                "id",
                _Id
              },
              {
                "name",
                dataTable.Rows[0]["UserName"].ToString()
              },
              {
                "cookiess",
                dataTable.Rows[0]["sessionId"].ToString()
              },
              {
                "point",
                dataTable.Rows[0]["Point"].ToString()
              }
            }, "www.youle1288.com;youle1288.com;www.youle2888.com;youle2888.com,feifan1188.com,www.feifan1188.com", this.site.CookiePath);
                    }
                    else
                    {
                        string str = Guid.NewGuid().ToString().Replace("-", "");
                        dbOperHandler.Reset();
                        dbOperHandler.ConditionExpress = "Id=@Id";
                        dbOperHandler.AddConditionParameter("@Id", (object)_Id);
                        dbOperHandler.AddFieldItem("sessionId", (object)str);
                        dbOperHandler.Update("N_User");
                        dbOperHandler.Dispose();
                        Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
            {
              {
                "id",
                _Id
              },
              {
                "name",
                dataTable.Rows[0]["UserName"].ToString()
              },
              {
                "cookiess",
                str
              },
              {
                "point",
                dataTable.Rows[0]["Point"].ToString()
              }
            }, "www.youle1288.com;youle1288.com;www.youle2888.com;youle2888.com,feifan1188.com,www.feifan1188.com", this.site.CookiePath);
                    }
                }
                return _Id;
            }
        }

        public string ChkAutoLoginWebApp(string _Id, string _sessionId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("select top 1 UserName,Point,sessionId from N_User with(nolock) where Id={0}", (object)_Id);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["sessionId"])))
                    {
                        Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
            {
              {
                "id",
                _Id
              },
              {
                "name",
                dataTable.Rows[0]["UserName"].ToString()
              },
              {
                "cookiess",
                dataTable.Rows[0]["sessionId"].ToString()
              },
              {
                "point",
                dataTable.Rows[0]["Point"].ToString()
              }
            }, this.site.CookieDomain, this.site.CookiePath);
                    }
                    else
                    {
                        string str = Guid.NewGuid().ToString().Replace("-", "");
                        dbOperHandler.Reset();
                        dbOperHandler.ConditionExpress = "Id=@Id";
                        dbOperHandler.AddConditionParameter("@Id", (object)_Id);
                        dbOperHandler.AddFieldItem("sessionId", (object)str);
                        dbOperHandler.Update("N_User");
                        dbOperHandler.Dispose();
                        Cookie.SetObj(this.site.CookiePrev + "WebApp", 1, new NameValueCollection()
            {
              {
                "id",
                _Id
              },
              {
                "name",
                dataTable.Rows[0]["UserName"].ToString()
              },
              {
                "cookiess",
                str
              },
              {
                "point",
                dataTable.Rows[0]["Point"].ToString()
              }
            }, this.site.CookieDomain, this.site.CookiePath);
                    }
                }
                return _Id;
            }
        }

        public void ChkLogout()
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp") == null)
                return;
            Cookie.Del(this.site.CookiePrev + "WebApp", this.site.CookieDomain, this.site.CookiePath);
        }

        public bool Exists(string _wherestr)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr;
                if (dbOperHandler.Exist("N_User"))
                    num = 1;
            }
            return num == 1;
        }

        public string GetUserName(string _id)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT [UserName] FROM [N_User] WHERE [Id]=" + _id;
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count > 0)
                    return dataTable.Rows[0]["UserName"].ToString();
                return string.Empty;
            }
        }

        public bool ChangeUserPassword(string _userid, string _oldPassword, string _newPassword)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                object field = dbOperHandler.GetField("N_User", "PassWord");
                if (field == null || !(field.ToString().ToLower() == MD5.Last64(_oldPassword)))
                    return false;
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("PassWord", (object)MD5.Last64(_newPassword));
                dbOperHandler.AddFieldItem("IP", (object)Const.GetUserIp);
                dbOperHandler.Update("N_User");
                return true;
            }
        }

        public bool ChangePayPassword(string _userid, string _oldPassword, string _newPassword)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                object field = dbOperHandler.GetField("N_User", "PayPass");
                if (field == null || !(field.ToString().ToLower() == MD5.Last64(_oldPassword)))
                    return false;
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("PayPass", (object)MD5.Last64(_newPassword));
                dbOperHandler.AddFieldItem("IP", (object)Const.GetUserIp);
                dbOperHandler.Update("N_User");
                return true;
            }
        }

        public bool SaveUserName(string _userid, string _name)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("TrueName", (object)_name);
                return dbOperHandler.Update("N_User") > 0;
            }
        }

        public bool SaveEmail(string _userid, string _name)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("Email", (object)_name);
                return dbOperHandler.Update("N_User") > 0;
            }
        }

        public bool SaveMobile(string _userid, string _name)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)_userid);
                dbOperHandler.AddFieldItem("Mobile", (object)_name);
                return dbOperHandler.Update("N_User") > 0;
            }
        }

        public string UpdateParentId(string Id, string UserName, string Point, string UserGroup, string Code)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = "SELECT Id,UserCode,UserGroup,ParentId,point FROM [N_User] where UserName='" + UserName + "'";
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter.Fill(dataTable1);
                    if (dataTable1.Rows.Count <= 0)
                        return "切入账户不存在！";
                    string str = dataTable1.Rows[0]["UserCode"].ToString() + Strings.PadLeft(Id);
                    if (Convert.ToDecimal(dataTable1.Rows[0]["Point"]) / new Decimal(10) < Convert.ToDecimal(Point))
                        return "返点高于切入账号的返点！不能切线";
                    if (Convert.ToDecimal(dataTable1.Rows[0]["UserGroup"]) < Convert.ToDecimal(UserGroup))
                        return "级别高于切入账号的级别！不能切线";
                    sqlDataAdapter.SelectCommand.CommandText = "SELECT Id FROM [N_User] where UserCode like (select UserCode from N_User where Id=" + Id + ")+'%' and Id<>" + Id;
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    if (dataTable2.Rows.Count > 0)
                    {
                        for (int index = 0; index < dataTable2.Rows.Count; ++index)
                        {
                            sqlCommand.CommandText = "update N_User set UserCode=Replace(UserCode,'" + Code + "','" + str + "') where Id=" + dataTable2.Rows[index]["Id"].ToString();
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                    sqlCommand.CommandText = "update N_User set ParentId=" + dataTable1.Rows[0]["Id"] + ",UserCode='" + dataTable1.Rows[0]["UserCode"] + Strings.PadLeft(Id) + "' where Id=" + Id;
                    if (sqlCommand.ExecuteNonQuery() > 0)
                        return "切线成功！";
                }
                catch (Exception ex)
                {
                    return "切线失败！";
                }
            }
            return "";
        }

        public void ChargePoints2(string _id, Decimal _money, Decimal _points)
        {
            using (new ComData().Doh())
                ;
        }

        public int DeleteUser(int userId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Id=" + (object)userId;
                return dbOperHandler.Delete("N_User");
            }
        }

        public void ClearAllUser()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "1=1";
                dbOperHandler.AddFieldItem("IsOnline", (object)0);
                dbOperHandler.AddFieldItem("SessionId", (object)Guid.NewGuid().ToString().Replace("-", ""));
                dbOperHandler.Update("N_User");
            }
        }

        public void UpdateInfo(SqlCommand cmd, int _userId, string _statType, Decimal _statValue)
        {
            try
            {
                SqlParameter[] values = new SqlParameter[3]
        {
          new SqlParameter("@UserId", (object) _userId),
          new SqlParameter("@statType", (object) _statType),
          new SqlParameter("@statValue", (object) _statValue)
        };
                cmd.CommandText = "update N_User set " + _statType + "=" + _statType + "+@statValue,updateTime=getdate() where Id=@UserId";
                cmd.Parameters.AddRange(values);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteRegLink(int userId)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "Id=" + (object)userId;
                return dbOperHandler.Delete("N_UserRegLink");
            }
        }

        public string GetUserNav(string pid, string uid)
        {
            string str = "<span><a href='javascript:void(0);' onclick='ajaxSearchPid(" + uid + ")'>本级</a></span><span>></span>";
            if (pid != "0")
            {
                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "SELECT UserCode FROM [N_User] WHERE [Id]=" + pid;
                    DataTable dataTable = dbOperHandler.GetDataTable();
                    if (dataTable.Rows.Count > 0)
                    {
                        string[] strArray = dataTable.Rows[0]["UserCode"].ToString().Replace(",,", ",").Substring(1, dataTable.Rows[0]["UserCode"].ToString().Replace(",,", ",").Length - 2).Split(',');
                        for (int index = 0; index < strArray.Length; ++index)
                        {
                            if (Convert.ToInt32(strArray[index]) > Convert.ToInt32(uid))
                                str = str + "<span><a href='javascript:void(0);' onclick='ajaxSearchPid(" + strArray[index] + ")'>" + this.GetUserName(strArray[index]) + "</a></span><span>></span>";
                        }
                    }
                }
            }
            return str;
        }
 
        public void UpdateUserCode(string _userid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from N_User where UserCode<>'' and len(Usercode)=" + _userid;
                DataTable dataTable1 = dbOperHandler.GetDataTable();
                if (dataTable1.Rows.Count <= 0)
                    return;
                for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "select * from N_User where UserCode='' and ParentId=" + dataTable1.Rows[index1]["Id"];
                    DataTable dataTable2 = dbOperHandler.GetDataTable();
                    for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.ConditionExpress = "id=@id";
                        dbOperHandler.AddConditionParameter("@id", dataTable2.Rows[index2]["Id"]);
                        dbOperHandler.AddFieldItem("UserCode", (object)(dataTable1.Rows[index1]["UserCode"].ToString() + "," + dataTable2.Rows[index2]["Id"] + ","));
                        dbOperHandler.Update("N_User");
                    }
                }
            }
        }

        public void Test(string _userid)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format("select UserId,isnull(sum(inmoney),0) money from Act_ActiveRecord where CONVERT(varchar(10), STime, 120) = '{0}' group by UserId", (object)_userid);
                DataTable dataTable = dbOperHandler.GetDataTable();
                if (dataTable.Rows.Count <= 0)
                    return;
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                    dbOperHandler.Reset();
            }
        }
    }
}
