// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxLogs
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lottery.Admin
{
  public partial class ajaxLogs : AdminCenter
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
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "clear":
          this.ajaxClear();
          break;
        case "ajaxGetExceptionList":
          this.ajaxGetExceptionList();
          break;
        case "ajaxExceptionClear":
          this.ajaxExceptionClear();
          break;
        case "ajaxGetUserLoginList":
          this.ajaxGetUserLoginList();
          break;
        case "ajaxUserLoginClear":
          this.ajaxUserLoginClear();
          break;
        case "ajaxGetAdminList":
          this.ajaxGetAdminList();
          break;
        case "ajaxGetErrorList":
          this.ajaxGetErrorList();
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

    private void ajaxGetList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel2");
      string str4 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "1=1";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and Title LIKE '%" + str3 + "%'";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and Content LIKE '%" + str4 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Log_Sys");
      string sql0 = SqlHelp.GetSql0("*", "Log_Sys", "id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxClear()
    {
      new LogSysDAL().DeleteLogs();
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了系统日志！");
      this._response = this.JsonResult(1, "成功清空");
    }

    private void ajaxGetExceptionList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel2");
      string str4 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "1=1";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and Title LIKE '%" + str3 + "%'";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and Content LIKE '%" + str4 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Log_Exception");
      string sql0 = SqlHelp.GetSql0("*", "Log_Exception", "id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxExceptionClear()
    {
      new LogExceptionDAL().DeleteLogs();
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了异常日志！");
      this._response = this.JsonResult(1, "成功清空");
    }

    private void ajaxGetUserLoginList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel2");
      string str4 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str5 = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = str5 + " LoginTime >='" + str1 + "' and LoginTime <='" + str2 + "'";
      if (!string.IsNullOrEmpty(str4))
      {
        if (string.IsNullOrEmpty(str3))
          whereStr = whereStr + " and (UserName = '" + str4 + "' or IP = '" + str4 + "' or Browser = '" + str4 + "')";
        if ("1".Equals(str3))
          whereStr = whereStr + " and UserName = '" + str4 + "'";
        if ("2".Equals(str3))
          whereStr = whereStr + " and IP = '" + str4 + "'";
        if ("3".Equals(str3))
          whereStr = whereStr + " and Browser = '" + str4 + "'";
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_LogUserLogin");
      string sql0 = SqlHelp.GetSql0("*", "V_LogUserLogin", "LoginTime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxUserLoginClear()
    {
      new LogSysDAL().DeleteUserLogs();
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员清空了登录日志！");
      this._response = this.JsonResult(1, "成功清空");
    }

    private void ajaxGetAdminList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sel2");
      string str4 = this.q("admin");
      string str5 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str6 = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = str6 + " OperTime >='" + str1 + "' and OperTime <='" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and OperTitle LIKE '" + str3 + "%'";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and AdminName='" + str4 + "'";
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and UserName= '" + str5 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_LogAdminOper");
      string sql0 = SqlHelp.GetSql0("*", "V_LogAdminOper", "OperTime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetErrorList()
    {
      string str1 = this.q("d1");
      if (str1.Trim().Length == 0)
        str1 = Convert.ToDateTime(this.StartTime).ToString("yyyy-MM-dd");
      if (str1.Trim().Length > 0)
        str1 = Convert.ToDateTime(str1).ToString("yyyy-MM-dd");
      string str2 = string.Format("select STime,Userid,dbo.f_GetUserName(Userid) as UserName,charge,remark\r\n                        FROM N_UserMoneyStatAll b where Convert(varchar(10),STime,120)='{0}'\r\n                        and charge>0\r\n                        and (userid not in (\r\n                          SELECT Userid FROM [Pay_SFT_temp] where Convert(varchar(10),STime,120)='{0}'\r\n                        ) and userid not in(\r\n                          SELECT [payUser] FROM [payRecord] where Convert(varchar(10),[payTime],120)='{0}'\r\n                        ) and userid not in(\r\n                          SELECT [UserId] FROM [N_UserCharge] where BankId=888 and  Convert(varchar(10),STime,120)='{0}'\r\n                        )\r\n                        )\r\n                        order by Userid", (object) str1);
      this.doh.Reset();
      this.doh.SqlCmd = str2;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetError2List()
    {
      string str = this.q("d1");
      int num1 = this.Int_ThisPage();
      int num2 = 100;
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str.Trim().Length == 0)
      {
        string startTime = this.StartTime;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_CheckCharge");
      string sql0 = SqlHelp.GetSql0("*", "V_CheckCharge", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      DataTable dt = this.CreatDataTable();
      for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
      {
        bool flag = false;
        DataRow row = dt.NewRow();
        row["userName"] = (object) dataTable.Rows[index1]["userName"].ToString();
        row["stime"] = (object) dataTable.Rows[index1]["stime"].ToString();
        row["charge"] = (object) dataTable.Rows[index1]["charge"].ToString();
        string[] strArray = dataTable.Rows[index1]["remark"].ToString().Split(',');
        for (int index2 = 0; index2 < strArray.Length; ++index2)
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.LoadXml(ajaxLogs.query("", "POST"));
          XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("/pay/response/is_success");
          row["remark" + (object) (index2 + 1)] = (object) xmlNodeList[0].InnerText;
          if (xmlNodeList[0].InnerText != "TRUE")
            flag = true;
        }
        if (flag)
          dt.Rows.Add(row);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dt) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private static string query(string merchant_order, string method)
    {
      string str = "";
      RestRequest request = new RestRequest("/query/", method == "POST" ? Method.POST : Method.GET);
      request.AddParameter("order_no", (object) "2015122723564252780");
      request.AddParameter("trade_no", (object) "30636184796920821");
      request.AddParameter("input_charset", (object) "UTF-8");
      request.AddParameter("merchant_code", (object) "19931090");
      List<Parameter> list = request.Parameters.OrderBy<Parameter, string>((Func<Parameter, string>) (p => p.Name)).ToList<Parameter>();
      list.Add(new Parameter()
      {
        Name = "key",
        Value = (object) "bad3bdfc0e984b999bfe0a6ecd16ce7d"
      });
      foreach (Parameter parameter in list)
        str = str + parameter.Name + "=" + parameter.Value + "&";
      request.AddParameter("sign", (object) ajaxLogs.GetMd5Hash(str.Substring(0, str.Length - 1)));
      return ajaxLogs.SendRequest(request).Content;
    }

    public static RestResponse SendRequest(RestRequest request)
    {
      request.AddHeader("Accept", "*/*");
      return (RestResponse) new RestClient("http://pay.41.cn/").Execute((IRestRequest) request);
    }

    public static string GetMd5Hash(string input)
    {
      using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
      {
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2"));
        return stringBuilder.ToString();
      }
    }

    private DataTable CreatDataTable()
    {
      return new DataTable()
      {
        Columns = {
          {
            "userName",
            typeof (string)
          },
          {
            "stime",
            typeof (string)
          },
          {
            "charge",
            typeof (Decimal)
          },
          {
            "remark1",
            typeof (string)
          },
          {
            "remark2",
            typeof (string)
          },
          {
            "remark3",
            typeof (string)
          },
          {
            "remark4",
            typeof (string)
          },
          {
            "remark5",
            typeof (string)
          },
          {
            "remark6",
            typeof (string)
          },
          {
            "remark7",
            typeof (string)
          },
          {
            "remark8",
            typeof (string)
          }
        }
      };
    }
  }
}
