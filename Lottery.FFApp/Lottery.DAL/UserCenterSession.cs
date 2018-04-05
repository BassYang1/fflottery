// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserCenterSession
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.Utils;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

namespace Lottery.DAL
{
    public class UserCenterSession : BasicPage
    {
        public string id = "0";
        protected string AdminId = "0";
        protected string AdminName = string.Empty;
        protected string AdminCookiess = string.Empty;
        protected string AdminMoney = "0";
        protected string AdminFreezing = "0";
        protected string AdminScore = "0";
        protected string AdminPic = "1";
        protected string AdminPoint = "1";
        public string loStr = "";
        public string StartTime = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd") + " 00:00:00";
        public string EndTime = DateTime.Now.AddDays(1.0).ToString("yyyy-MM-dd") + " 00:00:00";
        private byte[] Keys = new byte[8]
    {
      (byte) 18,
      (byte) 52,
      (byte) 86,
      (byte) 120,
      (byte) 144,
      (byte) 171,
      (byte) 205,
      (byte) 239
    };
        protected bool AdminIsLogin;

        protected void Admin_Load(string powerNum, string pageType)
        {
            if (this.site.WebIsOpen.ToString().Equals("1"))
                this.showErrMsg(this.site.WebCloseSeason.ToString(), pageType);
            else
                this.chkPower(powerNum, pageType);
        }

        protected void chkPower(string s, string pageType)
        {
            if (pageType == "json" && !this.CheckFormUrl())
            {
                this.Response.End();
            }
            else
            {
                if (this.IsPower(s))
                    return;
                this.showErrMsg("您还没有登录，请先登陆", pageType);
            }
        }

        protected bool IsPower(string s)
        {
            bool flag = false;
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
            {
                this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
                this.AdminName = Cookie.GetValue(this.site.CookiePrev + "WebApp", "name");
                this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
                this.AdminPoint = Cookie.GetValue(this.site.CookiePrev + "WebApp", "point");
                if (this.AdminId != "0")
                    flag = true;
            }
            return flag;
        }

        public void UserInfo(ref string _jsonstr)
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
            {
                this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
                this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
                if (this.AdminId != "0" && this.AdminCookiess != "")
                {
                    this.doh.Reset();
                    this.doh.ConditionExpress = "id=" + this.AdminId;
                    this.doh.AddFieldItem("OnTime", (object)DateTime.Now.ToString());
                    this.doh.Update("N_User");
                    this.doh.Reset();
                    this.doh.SqlCmd = "select top 1 UserName,Point,Money,Pic,IsDel,IsEnable,sessionId from N_User where Id=" + this.AdminId;
                    DataTable dataTable = this.doh.GetDataTable();
                    if (dataTable.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dataTable.Rows[0]["IsDel"]) != 0)
                            _jsonstr = "{\"result\" :\"0\",\"Message\" :\"您的账号异常，请联系客服\"}";
                        else if (Convert.ToInt32(dataTable.Rows[0]["IsEnable"]) != 0)
                            _jsonstr = "{\"result\" :\"0\",\"Message\" :\"您的账号异常，请联系客服\"}";
                        else if (!this.AdminCookiess.Equals(dataTable.Rows[0]["sessionId"].ToString()))
                        {
                            _jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
                        }
                        else
                        {
                            int num = 0;
                            string str = Convert.ToDouble(dataTable.Rows[0]["Money"]).ToString("0.00").PadLeft(10, '0').Replace(".", "");
                            _jsonstr = "{\"result\" :\"1\",\"AdminId\" :\"" + this.AdminId + "\",\"AdminName\" :\"" + dataTable.Rows[0]["UserName"] + "\",\"AdminMoney\" :\"" + dataTable.Rows[0]["Money"] + "\",\"Money\" :\"" + str + "\",\"emailcount\" :\"" + (object)num + "\"}";
                        }
                    }
                    else
                        _jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
                }
                else
                    _jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
            }
            else
                _jsonstr = "{\"result\" :\"0\",\"Message\" :\"登陆已超时，请您重新登陆\"}";
        }

        public void chkLogin()
        {
            if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") == null)
                return;
            this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
            this.AdminName = Cookie.GetValue(this.site.CookiePrev + "WebApp", "name");
            this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
            if (this.AdminId.Length == 0 || this.AdminName.Length == 0)
                return;
            this.doh.Reset();
            this.doh.ConditionExpress = "id=@id and sessionId=@cookiess";
            this.doh.AddConditionParameter("@id", (object)this.AdminId);
            this.doh.AddConditionParameter("@cookiess", (object)this.AdminCookiess);
            object[] fields = this.doh.GetFields("N_User", "PassWord,sessionId,Id,Money,Score,Pic,Point");
            if (fields == null)
                return;
            this.AdminIsLogin = true;
            this.AdminMoney = fields[3].ToString();
            this.AdminScore = fields[4].ToString();
            this.AdminPic = fields[5].ToString();
            this.AdminPoint = fields[6].ToString();
        }

        protected void showErrMsg(string msg, string pageType)
        {
            if (pageType != "json")
            {
                this.FinalMessage(msg, "/login", 0);
            }
            else
            {
                HttpContext.Current.Response.Clear();
                if (!this.AdminIsLogin)
                    HttpContext.Current.Response.Write(this.JsonResult(-1, msg));
                else
                    HttpContext.Current.Response.Write(this.JsonResult(0, msg));
                HttpContext.Current.Response.End();
            }
        }

        protected DateTime GetDateTime()
        {
            return new DateTimePubDAL().GetDateTime();
        }

        public static DataTable LotteryTime { get; set; }

        protected string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] bytes1 = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] keys = this.Keys;
                byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, keys), CryptoStreamMode.Write);
                cryptoStream.Write(bytes2, 0, bytes2.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        protected string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
                byte[] keys = this.Keys;
                byte[] buffer = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
                cryptoStream.Write(buffer, 0, buffer.Length);
                cryptoStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        protected void getUserGroupDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
            {
                return;
            }

            this.doh.Reset();
            this.doh.SqlCmd = string.Format("select UserGroup,Point from N_User where Id={0}", this.AdminId);
            DataTable dataTable1 = this.doh.GetDataTable();
            if (dataTable1.Rows.Count <= 0)
            {
                return;
            }

            this.doh.Reset();
            if (Convert.ToDouble(dataTable1.Rows[0]["Point"]) > 130.0)
            {
                if (dataTable1.Rows[0]["UserGroup"].ToString() == "1")
                {
                    this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<={0} ORDER BY Id desc", dataTable1.Rows[0]["UserGroup"]);
                }
                else
                {
                    this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<{0} ORDER BY Id desc", dataTable1.Rows[0]["UserGroup"]);
                }
            }
            else
            {
                if (dataTable1.Rows[0]["UserGroup"].ToString() == "1")
                {
                    this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<={0} ORDER BY Id desc", dataTable1.Rows[0]["UserGroup"]);
                }
                else
                {
                    this.doh.SqlCmd = string.Format("SELECT [Id],[Name] FROM N_UserGroup where Id<{0} ORDER BY Id desc", dataTable1.Rows[0]["UserGroup"]);
                }
            }

            DataTable dataTable2 = this.doh.GetDataTable();

            if (dataTable2.Rows.Count == 0)
            {
                ddlClassId.Items.Add(new ListItem(" 会员", "0"));
                dataTable2.Clear();
                dataTable2.Dispose();
            }
            else
            {
                for (int index = 0; index < dataTable2.Rows.Count; ++index)
                {
                    ddlClassId.Items.Add(new ListItem(" " + dataTable2.Rows[index]["Name"].ToString(), dataTable2.Rows[index]["Id"].ToString()));
                }

                dataTable2.Clear();
                dataTable2.Dispose();
            }
        }

        protected void getEditDropDownList(ref DropDownList ddlClassId, Decimal ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point>=100 and point<" + this.AdminPoint;
            this.doh.SqlCmd += " ORDER BY Bonus desc";
            DataTable dataTable1 = this.doh.GetDataTable();
            if (dataTable1.Rows.Count == 0)
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point=" + this.AdminPoint;
                this.doh.SqlCmd += " ORDER BY Bonus desc";
                DataTable dataTable2 = this.doh.GetDataTable();
                for (int index = 0; index < dataTable2.Rows.Count; ++index)
                    ddlClassId.Items.Add(new ListItem(dataTable2.Rows[index]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable2.Rows[index]["Point"]) / new Decimal(10)).ToString("0.00") + "%", dataTable2.Rows[index]["Point"].ToString()));
            }
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
            {
                string str = "";
                ddlClassId.Items.Add(new ListItem(dataTable1.Rows[index]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable1.Rows[index]["Point"]) / new Decimal(10)).ToString("0.00") + "% " + str, dataTable1.Rows[index]["Point"].ToString()));
            }
            dataTable1.Clear();
            dataTable1.Dispose();
        }

        protected void getEditDropDownList(ref DropDownList ddlClassId, Decimal point, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point>=" + (object)point + " and point<" + this.AdminPoint;
            this.doh.SqlCmd += " ORDER BY Bonus";
            DataTable dataTable1 = this.doh.GetDataTable();
            if (dataTable1.Rows.Count == 0)
            {
                this.doh.Reset();
                this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel] where point=" + this.AdminPoint;
                this.doh.SqlCmd += " ORDER BY Bonus";
                DataTable dataTable2 = this.doh.GetDataTable();
                for (int index = 0; index < dataTable2.Rows.Count; ++index)
                    ddlClassId.Items.Add(new ListItem(dataTable2.Rows[index]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable2.Rows[index]["Point"]) / new Decimal(10)).ToString("0.00") + "%", dataTable2.Rows[index]["Point"].ToString()));
            }
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
                ddlClassId.Items.Add(new ListItem(dataTable1.Rows[index]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable1.Rows[index]["Point"]) / new Decimal(10)).ToString("0.00") + "%", dataTable1.Rows[index]["Point"].ToString()));
            dataTable1.Clear();
            dataTable1.Dispose();
        }

        protected void getLotteryDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Id],[Title] FROM Sys_Lottery where IsOpen=0 ORDER BY Id asc";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
            {
                dataTable.Clear();
                dataTable.Dispose();
            }
            else
            {
                ddlClassId.Items.Add(new ListItem(" 所有彩票", ""));
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                    ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[index]["Title"].ToString(), dataTable.Rows[index]["Id"].ToString()));
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        protected void getSingleDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            ddlClassId.Items.Add(new ListItem(" 所有模式", ""));
            ddlClassId.Items.Add(new ListItem(" 元", "元"));
            ddlClassId.Items.Add(new ListItem(" 角", "角"));
            ddlClassId.Items.Add(new ListItem(" 分", "分"));
        }

        protected void getTypeDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            ddlClassId.Items.Add(new ListItem(" 所有类型", ""));
            ddlClassId.Items.Add(new ListItem(" 账号充值", "1"));
            ddlClassId.Items.Add(new ListItem(" 账号提款", "2"));
            ddlClassId.Items.Add(new ListItem(" 提现失败", "3"));
            ddlClassId.Items.Add(new ListItem(" 投注扣款", "4"));
            ddlClassId.Items.Add(new ListItem(" 追号扣款", "5"));
            ddlClassId.Items.Add(new ListItem(" 追号返款", "6"));
            ddlClassId.Items.Add(new ListItem(" 游戏返点", "7"));
            ddlClassId.Items.Add(new ListItem(" 奖金派送", "8"));
            ddlClassId.Items.Add(new ListItem(" 撤单返款", "9"));
            ddlClassId.Items.Add(new ListItem(" 充值扣费", "10"));
            ddlClassId.Items.Add(new ListItem(" 上级充值", "11"));
            ddlClassId.Items.Add(new ListItem(" 活动礼金", "12"));
            ddlClassId.Items.Add(new ListItem(" 代理分红", "13"));
            ddlClassId.Items.Add(new ListItem(" 管理员减扣", "14"));
            ddlClassId.Items.Add(new ListItem(" 积分兑换", "15"));
        }

        protected void getBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Id],[Bank] FROM [Sys_Bank] where IsGetCash=0 ORDER BY Id asc";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
                return;
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Bank"].ToString(), dataTable.Rows[index]["Id"].ToString()));
            dataTable.Clear();
            dataTable.Dispose();
        }

        protected void getTxBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,PayBank+'@'+'************'+substring(Payaccount,len(Payaccount)-3,4) as Name FROM [N_UserBank] where UserId=" + this.AdminId + " ORDER BY Id asc";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
            {
                dataTable.Clear();
                dataTable.Dispose();
            }
            else
            {
                for (int index = 0; index < dataTable.Rows.Count; ++index)
                    ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Name"].ToString(), dataTable.Rows[index]["Id"].ToString()));
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        protected void getZXChargeSetDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Id,MerName FROM [Sys_ChargeSet] where IsUsed=0 ORDER BY Sort asc";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
                return;
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["MerName"].ToString(), dataTable.Rows[index]["Id"].ToString()));
            dataTable.Clear();
            dataTable.Dispose();
        }

        protected void getZXBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT Code,Bank FROM [Sys_Bank] where IsCharge=1 ORDER BY Id asc";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
                return;
            for (int index = 0; index < dataTable.Rows.Count; ++index)
                ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Bank"].ToString(), dataTable.Rows[index]["Code"].ToString()));
            dataTable.Clear();
            dataTable.Dispose();
        }

        protected void getPointDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (this.Page.IsPostBack)
                return;
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Point],[Bonus] FROM [N_UserLevel] where point=(select point from N_User with(nolock) where Id=1893)";
            DataTable dataTable = this.doh.GetDataTable();
            if (dataTable.Rows.Count == 0)
            {
                dataTable.Clear();
                dataTable.Dispose();
            }
            else
            {
                DataRow row = dataTable.Rows[0];
                ddlClassId.Items.Add(new ListItem(row["Bonus"].ToString() + "/0.00%", row["Bonus"].ToString() + "/0.00%"));
                ddlClassId.Items.Add(new ListItem("1850/" + Convert.ToDouble(Convert.ToDouble(row["Point"]) / 10.0).ToString("0.00") + "%", "1850/" + Convert.ToDouble(Convert.ToDouble(row["Point"]) / 10.0).ToString("0.00") + "%"));
                dataTable.Clear();
                dataTable.Dispose();
            }
        }

        public DataSet ConvertXMLToDataSet(string xmlData)
        {
            XmlTextReader xmlTextReader = (XmlTextReader)null;
            try
            {
                DataSet dataSet = new DataSet();
                xmlTextReader = new XmlTextReader((TextReader)new StringReader(xmlData));
                int num = (int)dataSet.ReadXml((XmlReader)xmlTextReader);
                return dataSet;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return (DataSet)null;
            }
            finally
            {
                if (xmlTextReader != null)
                    xmlTextReader.Close();
            }
        }

        public string GetJsonResult(int result, string Message)
        {
            return "{\"result\":\"" + (object)result + "\",\"message\":\"" + Message + "\"}";
        }

        public string GetJsonResult2(int result, string Message)
        {
            return "[{\"result\":\"" + (object)result + "\",\"message\":\"" + Message + "\"}]";
        }
    }
}
