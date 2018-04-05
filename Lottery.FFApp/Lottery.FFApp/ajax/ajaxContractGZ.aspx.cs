// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxContractGZ
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using log4net;
using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Lottery.WebApp
{
    public partial class ajaxContractGZ : UserCenterSession
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(typeof(ajaxContractGZ));

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
                case "ajaxSaveContract":
                    this.ajaxSaveContract();
                    break;
                case "UpdateContractState":
                    this.UpdateContractState();
                    break;
                case "GetContractInfo":
                    this.GetContractInfo();
                    break;
                case "GetContractInfo2":
                    this.GetContractInfo2();
                    break;
                case "CanContract":
                    this.CheckUserAccess();
                    break;
                case "IsContract":
                    this.IsContract();
                    break;
                case "IsContractState":
                    this.IsContractState();
                    break;
                case "UpdateContractStateUserId":
                    this.UpdateContractStateUserId();
                    break;
                case "ajaxGetList":
                    this.ajaxGetList();
                    break;
                case "ajaxGetDetail":
                    this.ajaxGetDetail();
                    break;
                case "ajaxContractgzOperInfo":
                    this.ajaxContractgzOperInfo();
                    break;
                case "ajaxGetAgentGZRecord":
                    this.ajaxGetAgentGZRecord();
                    break;
                case "ajaxGetContractGZLog":
                    this.ajaxGetContractGZLog();
                    break;
                case "ajaxGZReissue":
                    this.ajaxGZReissue();
                    break;
                case "ajaxGetContractGZRecord":
                    this.ajaxGetContractGZRecord();
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

        private void IsContractState()
        {
            string _jsonstr = "";
            new ContractGzDAL().IsContract(this.AdminId, " and (IsUsed=0 or IsUsed=3)", ref _jsonstr);
            this._response = _jsonstr;
        }

        private void IsContract()
        {
            string _jsonstr = "";
            new ContractGzDAL().IsContract(this.AdminId, ref _jsonstr);
            this._response = _jsonstr;
        }

        /// <summary>
        /// 获取工资契约信息
        /// </summary>
        private void GetContractInfo2()
        {
            string UserId = this.q("id");
            if (string.IsNullOrEmpty(UserId))
            {
                UserId = this.AdminId;
            }

            string _jsonstr = "";
            new ContractGzDAL().GetContractInfo2(UserId, ref _jsonstr);
            this._response = _jsonstr;
        }

        private void GetContractInfo()
        {
            string UserId = this.q("id");
            if (string.IsNullOrEmpty(UserId))
                UserId = this.AdminId;
            string _jsonstr = "";
            new ContractGzDAL().GetContractInfo(UserId, ref _jsonstr);
            this._response = _jsonstr;
        }

        private void UpdateContractState()
        {
            this._response = new ContractGzDAL().UpdateContractState(this.AdminId, this.f("state"));
        }

        private void UpdateContractStateUserId()
        {
            this._response = new ContractGzDAL().UpdateContractState(this.f("userid"), this.f("state"));
        }

        private void CheckUserAccess()
        {
            int adminId = Int32.Parse(this.AdminId);

            if ((new Lottery.DAL.Flex.UserDAL()).IsAdminUser(adminId) || (new UserContractDAL()).HasUsedContract(adminId, 2))
            {
                this._response = this.JsonResult(1, "可以分配契约！");
            }
            else
            {
                this._response = this.JsonResult(0, "会员暂无权限分配契约！");
            }
        }

        private void ajaxSaveContract()
        {
            HttpContext.Current.Response.ContentType = "application/json";
            List<ajaxContractGZ.RequestDataJSON> requestDataJsonList = ajaxContractGZ.JSONToObject<List<ajaxContractGZ.RequestDataJSON>>(HttpUtility.UrlDecode(new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd()));
            ajaxContractGZ.RequestDataJSON requestDataJson1 = new ajaxContractGZ.RequestDataJSON();
            try
            {
                int adminId = Int32.Parse(this.AdminId);

                if ((new Lottery.DAL.Flex.UserDAL()).IsAdminUser(adminId) || (new UserContractDAL()).HasUsedContract(adminId, 2))
                {
                    UserContract list = new UserContract();
                    list.Type = 2;
                    list.ParentId = Convert.ToInt32(this.AdminId);
                    list.UserId = Convert.ToInt32(requestDataJsonList[0].userId);
                    List<UserContractDetail> userContractDetailList = new List<UserContractDetail>();
                    for (int index = 0; index < requestDataJsonList.Count; ++index)
                    {
                        ajaxContractGZ.RequestDataJSON requestDataJson2 = requestDataJsonList[index];

                        UserContractDetail detail = new UserContractDetail()
                        {
                            MinMoney = Convert.ToDecimal(requestDataJson2.money),
                            Money = Convert.ToDecimal(requestDataJson2.per)
                        };

                        if(detail.MinMoney <= 0 || detail.Money <= 0)
                        {
                            throw new Exception("输入的数值无效");
                        }

                        userContractDetailList.Add(detail);
                    }
                    list.UserContractDetails = userContractDetailList;
                    this._response = new ContractGzDAL().SaveContract(list) <= 0 ? this.JsonResult(0, "分配契约失败！") : this.JsonResult(1, "分配契约成功！");
                }
                else
                {
                    this._response = this.JsonResult(0, "会员暂无权限分配契约！");
                }
            }
            catch (Exception ex)
            {
                this._response = this.JsonResult(0, "分配契约失败！");
            }
        }

        private void ajaxGetList()
        {
            this.q("p");
            string str = this.q("u");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string whereStr = "type=2 and ParentId=" + this.AdminId;
            if (!string.IsNullOrEmpty(str))
                whereStr = whereStr + " and UserName = '" + str + "'";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("V_UserContract");
            string sql0 = SqlHelp.GetSql0("row_number() over (order by Id desc) as rowid,'2' as Type,*", "V_UserContract", "Id", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetDetail()
        {
            string str = this.q("id");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            string whereStr = "UcId=" + str;
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("N_UserContractDetail");
            string sql0 = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxContractgzOperInfo()
        {
            string str = this.q("id");
            DateTime now = DateTime.Now;
            string newValue1 = "0";
            string newValue2 = "0";
            string newValue3 = "0";
            string newValue4 = now.ToString("yyyy-MM-dd") + " 00:00:00";
            string newValue5 = now.ToString("yyyy-MM-dd") + " 23:59:59";
            this.doh.Reset();
            this.doh.SqlCmd = string.Format("SELECT \r\n                    (isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) as Bet,\r\n                    isnull(sum(Bet),0)-(isnull(sum(Win),0)+isnull(sum(Give),0)+isnull(sum(Change),0)+isnull(sum(Cancellation),0)+isnull(sum(Point),0)) as Loss\r\n                    FROM [N_UserMoneyStatAll] with(nolock)\r\n                    where (STime>='{0}' and STime<='{1}') and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code({2})+'%'", (object)newValue4, (object)newValue5, (object)str);
            DataTable dataTable1 = this.doh.GetDataTable();
            if (dataTable1.Rows.Count > 0)
            {
                newValue1 = dataTable1.Rows[0]["Bet"].ToString();
                newValue2 = dataTable1.Rows[0]["Loss"].ToString();
            }
            this.doh.Reset();
            this.doh.SqlCmd = string.Format("SELECT [Type],[ParentId],[UserId],[IsUsed],[STime],b.* FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and userId={0} and {1}>=MinMoney*10000 order by MinMoney desc", (object)str, (object)newValue1);
            DataTable dataTable2 = this.doh.GetDataTable();
            if (dataTable2.Rows.Count > 0)
                newValue3 = dataTable2.Rows[0]["Money"].ToString();
            string newValue6 = Convert.ToDecimal(Convert.ToDecimal(newValue1) * Convert.ToDecimal(newValue3) / new Decimal(100)).ToString("0.0000");
            this._response = "{\"starttime\": \"开始时间\",\"endtime\": \"截止时间\",\"bet\": \"销量\",\"loss\": \"亏损\",\"per\": \"比例\",\"money\": \"金额\"}".Replace("开始时间", newValue4).Replace("截止时间", newValue5).Replace("销量", newValue1).Replace("亏损", newValue2).Replace("比例", newValue3).Replace("金额", newValue6);
        }

        private void ajaxGetAgentGZRecord()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string whereStr = "UserId=" + this.AdminId;
            if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
                whereStr = whereStr + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("Act_ActiveRecord");
            string sql0 = SqlHelp.GetSql0("*,dbo.f_GetUserName(UserId) as UserName,(select count(*) from N_UserContract where ParentId=a.userId and Type=1) as contractcount", "Act_ActiveRecord a", "id", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetContractGZRecord()
        {
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string whereStr = "ActiveType='ActGongziContract' and dbo.f_GetUserCode(UserId) like '%," + this.AdminId + ",%'";
            if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
                whereStr = whereStr + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("Act_ActiveRecord");
            string sql0 = SqlHelp.GetSql0("a.SsId, a.Bet, a.InMoney, a.STime, a.CheckIp AS Remark, dbo.f_GetUserName(UserId) as UserName", "Act_ActiveRecord a", "id", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        private void ajaxGetContractGZLog()
        {
            string state = this.q("state"); //发放状态
            string str1 = this.q("d1");
            string str2 = this.q("d2");
            int num1 = this.Int_ThisPage();
            int num2 = this.Str2Int(this.q("pagesize"), 20);
            this.Str2Int(this.q("flag"), 0);
            if (str1.Trim().Length == 0)
                str1 = this.StartTime;
            if (str2.Trim().Length == 0)
                str2 = this.EndTime;
            if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
                str1 = str2;
            string whereStr = "dbo.f_GetUserCode(UserId) like '%," + this.AdminId + ",%' AND Type=2";

            if (!string.IsNullOrEmpty(state) && state != "0")
            {
                whereStr += " AND " + (state == "2" ? "Allowed=1" : "Allowed=0");
            }

            if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
                whereStr = whereStr + " and OperTime >='" + str1 + "' and OperTime <='" + str2 + "'";
            this.doh.Reset();
            this.doh.ConditionExpress = whereStr;
            int totalCount = this.doh.Count("Log_ContractOper");
            string sql0 = SqlHelp.GetSql0("Id, Bet, Money, Convert(NVARCHAR(10), OperTime, 120) AS OperTime, Remark, dbo.f_GetUserName(UserId) as UserName, Allowed", "Log_ContractOper", "id", num2, num1, "desc", whereStr);
            this.doh.Reset();
            this.doh.SqlCmd = sql0;
            DataTable dataTable = this.doh.GetDataTable();
            this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
            dataTable.Clear();
            dataTable.Dispose();
        }

        /// <summary>
        /// 手动派发工资
        /// </summary>
        private void ajaxGZReissue()
        {
            string logId = this.q("id");
            if (string.IsNullOrEmpty(logId))
            {
                this._response = "{\"result\" :\"0\",\"returnval\" :\"无效数据\"}";
            }

            using (SqlConnection conn = new SqlConnection(Const.ConnectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GZReissue";

                    SqlParameter parm1 = new SqlParameter("@logId", SqlDbType.Int);
                    parm1.Value = logId;
                    parm1.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(parm1);
                    
                    SqlParameter parm2 = new SqlParameter("@result", SqlDbType.NVarChar, 100);
                    parm2.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm2);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();

                    string result = parm2.Value.ToString();
                    Log.Debug(result);

                    if (string.IsNullOrEmpty(result))
                    {
                        this._response = "{\"result\" :\"1\",\"returnval\" :\"工资发放成功\"}";
                    }
                    else
                    {
                        this._response = "{\"result\" :\"0\",\"returnval\" :\"" + result + "\"}";
                    }
                }
            }
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
            public string userId { get; set; }

            public Decimal money { get; set; }

            public Decimal per { get; set; }
        }
    }
}
