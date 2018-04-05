// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserBankDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL.Flex
{
    public class UserBankDAL : ComData
    {
        public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr1;
                int totalCount = dbOperHandler.Count("N_UserBank");
                string sql0 = SqlHelp.GetSql0("row_number() over (order by Id asc) as rowid,'************'+substring(Payaccount,len(Payaccount)-3,4) as tPayaccount,substring(PayName,1,1)+'**' as tPayName,*", "N_UserBank", "Id", _pagesize, _thispage, "asc", _wherestr1);
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = sql0;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetBankInfoJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select * from V_UserBankInfo where UserId=" + UserId;
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = this.ConverTableToJSON(dataTable);
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetIphoneBankInfoJSON(string UserId, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "UserId=" + UserId;
                dbOperHandler.Count("N_UserBank");
                dbOperHandler.Reset();
                dbOperHandler.SqlCmd = "select top 3 * from V_UserBankInfo where UserId=" + UserId;
                dbOperHandler.SqlCmd += "order by Id desc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetChargeSetJSON(ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where IsUsed=0 and Id<>1020";
                dbOperHandler.SqlCmd += " ORDER BY Sort asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public void GetIphoneChargeSetJSONByCode(string code, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where UCode in ('" + code + "')";
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }


        public int GetIphoneChargeSetByCode(string code)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                int chargeSetId = 0;
                dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where UCode in ('" + code + "')";
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    Int32.TryParse(dataTable.Rows[0]["id"].ToString(), out chargeSetId);
                }

                dataTable.Clear();
                dataTable.Dispose();

                return chargeSetId;
            }
        }

        public void GetIphoneChargeSetJSON(string Id, ref string _jsonstr)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.SqlCmd = "SELECT * from Sys_ChargeSet where Id in (" + Id + ")";
                dbOperHandler.SqlCmd += " ORDER BY Id asc";
                DataTable dataTable = dbOperHandler.GetDataTable();
                _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName, string Question, string Answer)
        {
            if (this.Exists(" PayAccount='" + PayAccount + "'"))
                return this.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
            if (this.Exists(" UserId=" + userId))
            {
                if (!this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
                    return this.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "UserId=" + userId;
                    dbOperHandler.AddFieldItem("PayMethod", (object)PayMethod);
                    dbOperHandler.AddFieldItem("PayBank", (object)PayBank);
                    dbOperHandler.AddFieldItem("PayBankAddress", (object)PayBankAddress);
                    dbOperHandler.AddFieldItem("PayAccount", (object)PayAccount);
                    dbOperHandler.AddFieldItem("PayName", (object)PayName);
                    dbOperHandler.AddFieldItem("AddTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IsLock", (object)1);
                    if (dbOperHandler.Update("N_UserBank") <= 0)
                        return this.GetJsonResult(0, "银行资料绑定失败！");
                    new UserDAL().UpdateInfo(userId, Question, Answer);
                    return this.GetJsonResult(1, "银行资料绑定成功！");
                }
            }
            else
            {
                using (DbOperHandler dbOperHandler = new ComData().Doh())
                {
                    dbOperHandler.Reset();
                    dbOperHandler.AddFieldItem("UserId", (object)userId);
                    dbOperHandler.AddFieldItem("PayMethod", (object)PayMethod);
                    dbOperHandler.AddFieldItem("PayBank", (object)PayBank);
                    dbOperHandler.AddFieldItem("PayBankAddress", (object)PayBankAddress);
                    dbOperHandler.AddFieldItem("PayAccount", (object)PayAccount);
                    dbOperHandler.AddFieldItem("PayName", (object)PayName);
                    dbOperHandler.AddFieldItem("AddTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    dbOperHandler.AddFieldItem("IsLock", (object)1);
                    if (dbOperHandler.Insert("N_UserBank") <= 0)
                        return this.GetJsonResult(0, "银行资料绑定失败！");
                    new UserDAL().UpdateInfo(userId, Question, Answer);
                    return this.GetJsonResult(1, "银行资料绑定成功！");
                }
            }
        }

        public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName)
        {
            if (this.Exists(" PayAccount='" + PayAccount + "'"))
                return this.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
            if (this.Exists(" UserId=" + userId) && !this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
                return this.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItem("UserId", (object)userId);
                dbOperHandler.AddFieldItem("PayMethod", (object)PayMethod);
                dbOperHandler.AddFieldItem("PayBank", (object)PayBank);
                dbOperHandler.AddFieldItem("PayBankAddress", (object)PayBankAddress);
                dbOperHandler.AddFieldItem("PayAccount", (object)PayAccount);
                dbOperHandler.AddFieldItem("PayName", (object)PayName);
                dbOperHandler.AddFieldItem("AddTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dbOperHandler.AddFieldItem("IsLock", (object)1);
                if (dbOperHandler.Insert("N_UserBank") > 0)
                    return this.GetJsonResult(1, "银行资料绑定成功！");
                return this.GetJsonResult(0, "银行资料绑定失败！");
            }
        }

        public string Save(string userId, string PayMethod, string PayBank, string PayBankAddress, string PayAccount, string PayName, string strPwd)
        {
            if (this.Exists(" PayAccount='" + PayAccount + "'"))
                return this.GetJsonResult(0, "绑定失败,一张银行卡只能绑一个帐户！");
            if (this.Exists(" UserId=" + userId) && !this.Exists(" PayName='" + PayName + "' and UserId=" + userId))
                return this.GetJsonResult(0, "绑定失败,同一账户下只能绑定相同的开户名卡号！");
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=@id";
                dbOperHandler.AddConditionParameter("@id", (object)userId);
                object field = dbOperHandler.GetField("N_User", "PayPass");
                if (!MD5.Last64(strPwd).Equals(field.ToString()))
                    return this.GetJsonResult(0, "绑定失败,您的提现密码错误！");
                dbOperHandler.Reset();
                dbOperHandler.AddFieldItem("UserId", (object)userId);
                dbOperHandler.AddFieldItem("PayMethod", (object)PayMethod);
                dbOperHandler.AddFieldItem("PayBank", (object)PayBank);
                dbOperHandler.AddFieldItem("PayBankAddress", (object)PayBankAddress);
                dbOperHandler.AddFieldItem("PayAccount", (object)PayAccount);
                dbOperHandler.AddFieldItem("PayName", (object)PayName);
                dbOperHandler.AddFieldItem("AddTime", (object)DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dbOperHandler.AddFieldItem("IsLock", (object)1);
                if (dbOperHandler.Insert("N_UserBank") > 0)
                    return this.GetJsonResult(1, "银行资料绑定成功！");
                return this.GetJsonResult(0, "银行资料绑定失败！");
            }
        }

        public bool Exists(string _wherestr)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr;
                if (dbOperHandler.Exist("N_UserBank"))
                    num = 1;
            }
            return num == 1;
        }

        public void Delete(string Id)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = "id=" + Id;
                dbOperHandler.Delete("N_UserBank");
            }
        }
    }
}
