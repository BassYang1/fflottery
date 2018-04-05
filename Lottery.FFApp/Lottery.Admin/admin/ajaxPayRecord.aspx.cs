// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxPayRecord
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
  public partial class ajaxPayRecord : AdminCenter
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
        case "ajaxGetYBList":
          this.ajaxGetYBList();
          break;
        case "ajaxGetZFList":
          this.ajaxGetZFList();
          break;
        case "ajaxGet18List":
          this.ajaxGet18List();
          break;
        case "ajaxGetIpsList":
          this.ajaxGetIpsList();
          break;
        case "ajaxGetSftList":
          this.ajaxGetSftList();
          break;
        case "ajaxGetSftCheckList":
          this.ajaxGetSftCheckList();
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

    private void ajaxGetYBList()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " Convert(varchar(10),order_time,120) >='" + str1 + "' and Convert(varchar(10),order_time,120) <='" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_YiBao_temp");
      string sql0 = SqlHelp.GetSql0("ID,UserId,dbo.f_GetUserName(UserId) as UserName,order_no,order_amount,substring([order_time],1,4)+'-'+substring([order_time],5,2)+'-'+substring([order_time],7,2)+' '+substring([order_time],9,2)+':'+substring([order_time],11,2)+':'+substring([order_time],13,2) as order_time,trade_no,trade_status", "Pay_YiBao_temp", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetZFList()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " Convert(varchar(10),order_time,120) >='" + str1 + "' and Convert(varchar(10),order_time,120) <='" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_Trade_temp");
      string sql0 = SqlHelp.GetSql0("*,dbo.f_GetUserName(UserId) as UserName", "Pay_Trade_temp", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGet18List()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " Convert(varchar(10),o_time,120) >='" + str1 + "' and Convert(varchar(10),o_time,120) <='" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_My18_temp");
      string sql0 = SqlHelp.GetSql0("*,dbo.f_GetUserName(U_id) as UserName", "Pay_My18_temp", "o_time", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetIpsList()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("code");
      string str4 = this.q("payrequestid");
      string str5 = this.q("ipsrequestid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " PaySTime >='" + str1 + "' and PaySTime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + "  and PayCode='" + str3 + "'";
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + "  and payrequestid like '%" + str4 + "%'";
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + "  and ipsrequestid like '%" + str5 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_temp");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,(select MerName from Sys_ChargeSet where Id=a.PayCode) as PayName,*", "Pay_temp a", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetSftList()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " STime >='" + str1 + "' and STime <'" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_SFT_temp");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Pay_SFT_temp", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetSftCheckList()
    {
      this.q("keys");
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = 500;
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " STime >='" + str1 + "' and STime <'" + str2 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Pay_SFT_temp");
      string sql0 = SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Pay_SFT_temp", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      DataTable dt = this.CreatDataTable();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dt.NewRow();
        row["rowid"] = (object) string.Concat((object) (index + 1));
        row["UserId"] = (object) dataTable.Rows[index]["UserId"].ToString();
        row["UserName"] = (object) dataTable.Rows[index]["UserName"].ToString();
        row["billno"] = (object) dataTable.Rows[index]["billno"].ToString();
        row["amount"] = (object) dataTable.Rows[index]["amount"].ToString();
        row["ordertime"] = (object) dataTable.Rows[index]["ordertime"].ToString();
        row["ipsbillno"] = (object) dataTable.Rows[index]["ipsbillno"].ToString();
        row["stime"] = (object) dataTable.Rows[index]["stime"].ToString();
        row["status"] = (object) dataTable.Rows[index]["status"].ToString();
        string str3 = dataTable.Rows[index]["UserId"].ToString();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(ajaxPayRecord.query(dataTable.Rows[index]["billno"].ToString(), "POST"));
        XmlElement documentElement = xmlDocument.DocumentElement;
        XmlNodeList xmlNodeList1 = documentElement.SelectNodes("/pay/response/is_success");
        XmlNodeList xmlNodeList2 = documentElement.SelectNodes("/pay/response/order_amount");
        XmlNodeList xmlNodeList3 = documentElement.SelectNodes("/pay/response/trade_no");
        XmlNodeList xmlNodeList4 = documentElement.SelectNodes("/pay/response/order_no");
        row["remark"] = !(xmlNodeList1[0].InnerText != "TRUE") ? (dataTable.Rows[index]["billno"].ToString().Length != 19 ? (!(Convert.ToDecimal(xmlNodeList2[0].InnerText) != Convert.ToDecimal(dataTable.Rows[index]["amount"].ToString())) ? (xmlNodeList3[0].InnerText.Equals(dataTable.Rows[index]["ipsbillno"].ToString()) ? (xmlNodeList4[0].InnerText.Substring(15, str3.Length).Equals(str3) ? (object) "对账通过" : (object) "对账错误") : (object) "对账错误") : (object) "对账错误") : (!(Convert.ToDecimal(xmlNodeList2[0].InnerText) != Convert.ToDecimal(dataTable.Rows[index]["amount"].ToString())) ? (xmlNodeList3[0].InnerText.Equals(dataTable.Rows[index]["ipsbillno"].ToString()) ? (object) "对账通过" : (object) "对账错误") : (object) "对账错误")) : (object) "对账错误";
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
      request.AddParameter("order_no", (object) merchant_order);
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
      request.AddParameter("sign", (object) ajaxPayRecord.GetMd5Hash(str.Substring(0, str.Length - 1)));
      return ajaxPayRecord.SendRequest(request).Content;
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
            "rowid",
            typeof (string)
          },
          {
            "UserId",
            typeof (string)
          },
          {
            "UserName",
            typeof (string)
          },
          {
            "billno",
            typeof (string)
          },
          {
            "amount",
            typeof (Decimal)
          },
          {
            "ordertime",
            typeof (string)
          },
          {
            "ipsbillno",
            typeof (string)
          },
          {
            "stime",
            typeof (string)
          },
          {
            "status",
            typeof (string)
          },
          {
            "remark",
            typeof (string)
          }
        }
      };
    }
  }
}
