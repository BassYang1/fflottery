// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserGetCashDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lottery.DAL.Flex
{
    public class UserGetCashDAL : ComData
    {
        protected SiteModel site;

        public UserGetCashDAL()
        {
            this.site = new conSite().GetSite();
        }

        /// <summary>
        /// 检查支付状态
        /// </summary>
        /// <param name="num">最新支付数量</param>
        /// <param name="lastCkDate">上一次检查时间</param>
        /// <returns></returns>
        public String CheckCashState(ref int num, string lastCkDate = null)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                lastCkDate = string.IsNullOrEmpty(lastCkDate) ? "1900-01-01" : lastCkDate;

                String date = string.Empty;
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT COUNT(1) AS Num, MAX(STime) AS STime FROM N_UserGetCash WHERE ISNULL(State, 0) = 0 AND STime > '" + lastCkDate + "' ORDER BY STime DESC";
                DataTable dataTable = dbOperHandler.GetDataTable();

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    num = Int32.Parse(dataTable.Rows[0]["Num"].ToString());

                    if (!Convert.IsDBNull(dataTable.Rows[0]["STime"]))
                    {
                        date = dataTable.Rows[0]["STime"].ToString();
                    }
                }

                dataTable.Clear();
                dataTable.Dispose();

                return date;
            }
        }

        public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                dbOperHandler.Count("Flex_UserGetCash");
                string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,0.0000 as sxf,*", "Flex_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetIphoneListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                int totalCount = dbOperHandler.Count("Flex_UserGetCash");
                string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,0.0000 as sxf,*", "Flex_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public DataTable GetListDataTable(string Id)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from Flex_UserGetCash where state=0 and Id=" + Id;
                return dbOperHandler.GetDataTable();
            }
        }

        /// <summary>
        /// 获取待处理的订单
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public IList<CashGetModel> GetListDataTable()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                IList<CashGetModel> records = new List<CashGetModel>();
                StringBuilder sql = new StringBuilder("INSERT INTO N_UserGetCashHistory(SsId, UserId, UserName) VALUES");
                StringBuilder sqlValue = new StringBuilder();

                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select A.SsId, A.UserId, B.UserName, A.PayBank, A.PayAccount, A.PayName, A.Money, A.Msg from N_UserGetCash A INNER JOIN N_USER B ON A.UserId=B.Id where state=0 and not exists(SELECT 1 FROM N_UserGetCashHistory WHERE SsId = A.SsId)";
                DataTable table = dbOperHandler.GetDataTable();

                if (table != null && table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["Money"] != null)
                        {
                            sqlValue.Append(String.Format("('{0}', {1}, '{2}')", table.Rows[i]["SsId"].ToString(), table.Rows[i]["UserId"].ToString(), table.Rows[i]["UserName"].ToString()));

                            if (i < table.Rows.Count - 1)
                            {
                                sqlValue.Append(",");
                            }

                            records.Add(new CashGetModel()
                            {
                                SsId = table.Rows[i]["SsId"].ToString(),
                                UserName = table.Rows[i]["UserName"].ToString(),
                                PayBank = table.Rows[i]["PayBank"].ToString(),
                                PayAccount = table.Rows[i]["PayAccount"].ToString(),
                                PayName = table.Rows[i]["PayName"].ToString(),
                                Money = Convert.ToDecimal(table.Rows[i]["Money"]),
                                Msg = table.Rows[i]["Msg"].ToString()
                            });
                        }
                    }
                }

                if (records.Count > 0)
                {
                    dbOperHandler.Reset();
                    sql.Append(sqlValue.ToString());
                    dbOperHandler.ExecuteSql(sql.ToString());
                }

                return records;
            }
        }

        public string UserGetCash(string UserId, string UserBankId, string BankId, string Money, string PassWord)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 1 * from Sys_Bank where id=" + BankId;
                DataTable dataTable1 = dbOperHandler.GetDataTable();

                //每日最大提款金额
                decimal minCharge = Convert.ToDecimal(dataTable1.Rows[0]["MinCharge"]);
                decimal maxCharge = Convert.ToDecimal(dataTable1.Rows[0]["MaxCharge"]);
                maxCharge = maxCharge > 20000M ? 20000M : maxCharge;

                if (Convert.ToDecimal(Money) > maxCharge || Convert.ToDecimal(Money) < minCharge)
                {
                    return string.Format("提款金额最大{0}元，最小{0}元", maxCharge, minCharge);
                }

                //每日最大提现次数
                int maxCashCount = Convert.ToInt32(dataTable1.Rows[0]["MaxGetCash"]);
                maxCashCount = maxCashCount > 5 ? 5 : maxCashCount;

                //充值消费额度
                double betPerCheck = Convert.ToDouble(dataTable1.Rows[0]["BetPerCheck"]);
                betPerCheck = betPerCheck < 50.0 ? 50.0 : betPerCheck;

                //绑卡时间提现期限, 至少绑定24小时，才能提现
                int bindTime = Convert.ToInt32(dataTable1.Rows[0]["BindTime"]);
                bindTime = bindTime < 24 ? 24 : bindTime;

                //是否允许银行卡提现
                if (Convert.ToInt32(dataTable1.Rows[0]["IsUsed"]) == 1)
                    return "取款失败,当前银行禁止取款!";

                //会员信息
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)UserId);
                object[] fields = dbOperHandler.GetFields("N_User", "Money,PayPass,IsGetCash,EnableSeason,UserGroup");
                if (fields.Length <= 0)
                    return "账号出现问题，请您重新登陆！";

                //会员是否允许提现
                int int32 = Convert.ToInt32(fields[2]); //IsGetCash
                if (int32 != 0)
                    return "取款失败,您的帐号禁止取款!";

                //提款金额是否大于会员余额
                if (Convert.ToDecimal(Money) > Convert.ToDecimal(fields[0]))
                    return "您的可用余额不足";

                //提款密码是否正确
                if (!MD5.Last64(MD5.Lower32(PassWord)).Equals(fields[1].ToString()))
                    return "您的提现密码错误";

                //会员银行卡信息
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "SELECT [PayBank],[PayAccount],[PayName], AddTime FROM [N_UserBank] where UserId=" + UserId + " and Id=" + UserBankId;
                DataTable dataTable5 = dbOperHandler.GetDataTable();
                if (dataTable5.Rows.Count <= 0)
                {
                    return "您的银行卡无效";
                }

                //检查绑定时间
                DateTime addTime = Convert.ToDateTime(dataTable5.Rows[0]["AddTime"]);
                if ((DateTime.Now - addTime).TotalHours <= bindTime)
                {
                    return string.Format("您的银行卡绑定还未满{0}小时", bindTime);
                }

                //今天的充值记录
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = string.Format(@"select STime from Act_ActiveRecord where UserId={0} 
                                and ActiveType='Charge' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object)UserId);
                DataTable dataTable2 = dbOperHandler.GetDataTable();
                if (dataTable2.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(fields[0]) - Convert.ToDecimal(Money) < new Decimal(50))
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.SqlCmd = string.Format("SELECT cast(round(isnull(Sum(Total*Times),0),4) as numeric(20,4)) as bet FROM [N_UserBet]\r\n                                                                where UserId={0} and (state=2 or state=3) and STime>'{1}' ", (object)UserId, (object)dataTable2.Rows[0]["STime"].ToString());
                        DataTable dataTable3 = dbOperHandler.GetDataTable();
                        if (dataTable3.Rows.Count > 0 && Convert.ToDecimal(dataTable3.Rows[0]["bet"]) < new Decimal(800))
                            return "首充佣金50元不能体现，您的消费未满800元！";
                    }
                }
                else if (Convert.ToInt32(fields[4].ToString()) < 2)
                {
                    dbOperHandler.Reset();
                    dbOperHandler.SqlCmd = "SELECT (isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) as bet,isnull(sum(charge),0) as charge FROM [N_UserMoneyStatAll] with(nolock) where userId=" + UserId;
                    DataTable dataTable3 = dbOperHandler.GetDataTable();
                    double num1 = Convert.ToDouble(dataTable3.Rows[0]["bet"].ToString()); //消费金额, 下注金额
                    double num2 = Convert.ToDouble(dataTable3.Rows[0]["charge"].ToString()); //充值金额总数
                    if (num2 > 0.0 && num1 * 100.0 / num2 < betPerCheck)
                        return "对不起，您未消费到充值的" + betPerCheck + "%，不能提现！";
                }
                if (Convert.ToDecimal(Money) < Convert.ToDecimal(dataTable1.Rows[0]["MinCharge"]))
                    return "提现金额不能小于单笔最小金额";
                if (Convert.ToDecimal(Money) > Convert.ToDecimal(dataTable1.Rows[0]["MaxCharge"]))
                    return "提现金额不能大于单笔最大金额";
                DateTime dateTime1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dataTable1.Rows[0]["StartTime"]);
                DateTime dateTime2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + dataTable1.Rows[0]["EndTime"]);
                DateTime now = DateTime.Now;
                if (dateTime1.Hour >= dateTime2.Hour)
                {
                    if (now < dateTime1 && now > dateTime2)
                        return "提现时间为" + dataTable1.Rows[0]["StartTime"] + "至" + dataTable1.Rows[0]["EndTime"];
                }
                else if (now < dateTime1 || now > dateTime2)
                    return "提现时间为" + dataTable1.Rows[0]["StartTime"] + "至" + dataTable1.Rows[0]["EndTime"];

                int cashCount = 0; //已提现次数
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select count(*) as txcs,isnull(sum(Money),0) as txje from N_UserGetCash where userId=" + UserId + " and datediff(d,STime,getdate())=0 and State<>2";
                DataTable dataTable4 = dbOperHandler.GetDataTable();
                if (dataTable4.Rows.Count > 0)
                {
                    cashCount = Convert.ToInt32(dataTable4.Rows[0]["txcs"]);
                    dataTable4.Rows[0]["txje"].ToString();
                }

                if (cashCount > maxCashCount)
                    return string.Format("今日提现已得到最大提现次数{0}次", maxCashCount);

                if (dataTable5.Rows.Count <= 0 || this.Save(UserId, UserBankId, dataTable5.Rows[0]["PayBank"].ToString(), dataTable5.Rows[0]["PayAccount"].ToString(), dataTable5.Rows[0]["PayName"].ToString(), Convert.ToDecimal(Money)) <= 0)
                    return "申请提现失败！";
                new LogSysDAL().Save("会员管理", "Id为" + UserId + "的会员申请提现！");
                return "申请提现成功！";
            }
        }

        public int Save(string _userId, string Bank, string PayBank, string PayAccount, string PayName, Decimal Money)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                try
                {
                    string getCash = SsId.GetCash;
                    Decimal Money1 = Convert.ToDecimal(Money);
                    if (new UserTotalTran().MoneyOpers(getCash, _userId, Money1, 0, 0, 0, 2, 99, "提现申请", "提现申请成功，请耐心等待……", "会员提现", "") > 0)
                    {
                        SqlParameter[] values = new SqlParameter[8]
            {
              new SqlParameter("@SsId", (object) getCash),
              new SqlParameter("@UserId", (object) _userId),
              new SqlParameter("@BankId", (object) Bank),
              new SqlParameter("@PayBank", (object) PayBank),
              new SqlParameter("@PayAccount", (object) PayAccount),
              new SqlParameter("@PayName", (object) PayName),
              new SqlParameter("@Money", (object) Money),
              new SqlParameter("@STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };
                        sqlCommand.CommandText = "insert into N_UserGetCash(SsId,UserId,BankId,PayBank,PayAccount,PayName,Money,STime) values(@SsId,@UserId,@BankId,@PayBank,@PayAccount,@PayName,@Money,@STime)";
                        sqlCommand.CommandText += " SELECT SCOPE_IDENTITY()";
                        sqlCommand.Parameters.AddRange(values);
                        num = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.Parameters.Clear();
                        num = 1;
                    }
                    else
                        num = 0;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                }
            }
            return num;
        }

        public bool Exists(string _wherestr)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr;
                if (dbOperHandler.Exist("N_UserGetCash"))
                    num = 1;
            }
            return num == 1;
        }

        public void GetAdminListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                int totalCount = dbOperHandler.Count("V_UserGetCash");
                string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,*", "V_UserGetCash", "Id", _pagesize, _thispage, "desc", _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public int Check(string _cashId, string _Msg, int State)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                try
                {
                    object[] objArray = new object[2];
                    using (DbOperHandler dbOperHandler = new ComData().Doh())
                    {
                        dbOperHandler.Reset();
                        dbOperHandler.ConditionExpress = "id=" + _cashId;
                        objArray = dbOperHandler.GetFields("N_UserGetCash", "UserId,Money,ssId,STime");
                    }
                    if (State == 1)
                    {
                        SqlParameter[] values = new SqlParameter[3]
            {
              new SqlParameter("@Id", (object) _cashId),
              new SqlParameter("@Msg", (object) _Msg),
              new SqlParameter("@State", (object) 1)
            };
                        cmd.CommandText = "update N_UserGetCash set Msg=@Msg,STime2=getdate(),State=1 where Id=" + _cashId;
                        cmd.Parameters.AddRange(values);
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Parameters.Clear();
                        new UserMessageDAL().Save(cmd, int.Parse(objArray[0].ToString()), "提现成功", "您的" + objArray[1].ToString() + "元提现已处理，请注意查收，如有疑问请联系在线客服！");
                    }
                    if (State == 2)
                    {
                        if (new UserTotalTran().MoneyOpers(objArray[2].ToString(), objArray[0].ToString(), -Convert.ToDecimal(objArray[1].ToString()), 0, 0, 0, 2, 99, "提现失败", "您的" + objArray[1].ToString() + "元提现被拒绝，拒绝理由：（" + _Msg + "），如有疑问请联系在线客服！", "提现失败", objArray[3].ToString()) <= 0)
                            return 0;
                        SqlParameter[] values = new SqlParameter[3]
            {
              new SqlParameter("@Id", (object) _cashId),
              new SqlParameter("@Msg", (object) _Msg),
              new SqlParameter("@State", (object) 2)
            };
                        cmd.CommandText = "update N_UserGetCash set Msg=@Msg,STime2=getdate(),State=2 where Id=" + _cashId;
                        cmd.Parameters.AddRange(values);
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.Parameters.Clear();
                    }
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                }
            }
            return num;
        }

        public void DeleteLogs()
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "1=1";
                dbOperHandler.Delete("N_UserGetCash");
            }
        }
    }
}
