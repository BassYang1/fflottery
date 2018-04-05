// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.usercashedit
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.DAL.Flex;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public class usercashedit : AdminCenter
  {
    public string url = "";
    protected HtmlForm form1;
    protected Label lblBank;
    protected Label lblBankCode;
    protected Label lblUserName;
    protected Label txtPayName;
    protected Label txtPayAccount;
    protected Label txtMoney;
    protected RadioButton rbo2;
    protected RadioButton rbo3;
    protected RadioButton rbo1;
    protected TextBox txtSeason;
    protected TextBox txtId;
    protected Label lblMsg;
    protected Button btnSave;
    protected HtmlInputHidden orderNo;
    protected HtmlInputHidden tradeDate;
    protected HtmlInputHidden Amt;
    protected HtmlInputHidden bankAccName;
    protected HtmlInputHidden bankName;
    protected HtmlInputHidden bankCode;
    protected HtmlInputHidden bankAccNo;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.IsPostBack)
        return;
      string str = this.txtId.Text = this.Str2Str(this.q("id"));
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from Flex_UserGetCash where Id=" + str;
      DataTable dataTable = this.doh.GetDataTable();
      this.orderNo.Value = dataTable.Rows[0]["ssid"].ToString();
      this.tradeDate.Value = DateTime.Now.ToString("yyyyMMdd");
      this.Amt.Value = Convert.ToDouble(dataTable.Rows[0]["CashMoney"].ToString()).ToString("0.00");
      this.bankAccName.Value = dataTable.Rows[0]["PayName"].ToString();
      this.bankName.Value = dataTable.Rows[0]["PayBankAddress"].ToString();
      this.bankCode.Value = dataTable.Rows[0]["bankCode"].ToString();
      this.bankAccNo.Value = dataTable.Rows[0]["PayAccount"].ToString();
      this.lblUserName.Text = dataTable.Rows[0]["UserName"].ToString();
      this.lblBank.Text = dataTable.Rows[0]["PayBank"].ToString();
      this.txtPayName.Text = dataTable.Rows[0]["PayName"].ToString();
      this.txtPayAccount.Text = dataTable.Rows[0]["PayAccount"].ToString();
      this.txtMoney.Text = Convert.ToDouble(dataTable.Rows[0]["CashMoney"].ToString()).ToString("0.00");
      if (string.IsNullOrEmpty(this.bankName.Value))
        this.bankName.Value = dataTable.Rows[0]["PayBank"].ToString();
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from N_UserBank where replace(PayAccount,' ','')='" + dataTable.Rows[0]["PayAccount"].ToString().Replace(" ", "").Trim() + "' and UserId=" + dataTable.Rows[0]["UserId"].ToString();
      if (this.doh.GetDataTable().Rows.Count < 1)
      {
        this.lblBankCode.Text = "-1";
        this.lblBank.Text = "取款的卡号与会员绑定的卡号不符，请仔细查看！";
      }
      else if (string.IsNullOrEmpty(string.Concat(dataTable.Rows[0]["PayMethod"])))
      {
        this.lblBankCode.Text = "-1";
        this.lblBank.Text = "未绑定银行信息或者提现后删除了银行信息,请从新绑定！";
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=" + dataTable.Rows[0]["PayMethod"].ToString();
        object[] fields = this.doh.GetFields("Sys_Bank", "Url,Code");
        this.url = string.Concat(fields[0]);
        this.lblBankCode.Text = string.Concat(fields[1]);
      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.bankAccName.Value) || string.IsNullOrEmpty(this.bankName.Value) || string.IsNullOrEmpty(this.bankCode.Value) || string.IsNullOrEmpty(this.bankAccNo.Value))
      {
        this.FinalMessage("银行信息不完整，不能处理！", "/admin/close.htm", 0);
      }
      else
      {
        if (this.rbo2.Checked)
        {
          if (this.lblBankCode.Text.Equals("-1"))
            this.FinalMessage(this.lblBank.Text, "/admin/close.htm", 0);
          else if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
          {
            new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 1);
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "同意会员" + this.lblUserName.Text + "的提现申请（手工）");
            this.FinalMessage("操作成功", "/admin/close.htm", 0);
          }
          else
            this.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
        }
        if (this.rbo3.Checked)
        {
          if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
          {
            new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 2);
            new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "拒绝会员" + this.lblUserName.Text + "的提现申请");
            this.FinalMessage("操作成功", "/admin/close.htm", 0);
          }
          else
            this.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
        }
        if (!this.rbo1.Checked)
          return;
        if (new UserGetCashDAL().Exists(" (state=0 or state=99) and Id=" + this.txtId.Text))
        {
          string str = this.remitMiao(ConfigurationManager.AppSettings["codeMiao"].ToString(), ConfigurationManager.AppSettings["keyMiao"].ToString());
          if (string.IsNullOrEmpty(str))
          {
            this.FinalMessage("连接支付平台错误！", "/admin/close.htm", 0);
          }
          else
          {
            JObject jobject = (JObject) JsonConvert.DeserializeObject(str);
            if (jobject["is_success"].ToString() == "true")
            {
              new UserGetCashDAL().Check(this.txtId.Text, this.txtSeason.Text, 1);
              new LogAdminOperDAL().SaveLog(this.AdminId, this.txtId.Text, "会员取款", "同意会员" + this.lblUserName.Text + "的提现申请（国盛通微信）");
              this.FinalMessage("取款成功", "/admin/close.htm", 0);
            }
            else
              this.FinalMessage(jobject["errror_msg"].ToString(), "/admin/close.htm", 0);
          }
        }
        else
          this.FinalMessage("该提现请求已处理，不能重复处理！", "/admin/close.htm", 0);
      }
    }

    private string remitMiao(string code, string key)
    {
      string str1 = "";
      RestRequest request = new RestRequest("", Method.POST);
      request.AddParameter("account_name", (object) this.bankAccName.Value);
      request.AddParameter("account_number", (object) this.bankAccNo.Value);
      request.AddParameter("amount", (object) this.Amt.Value);
      request.AddParameter("bank_name", (object) this.bankCode.Value);
      request.AddParameter("bitch_no", (object) this.orderNo.Value);
      request.AddParameter("currentDate", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
      request.AddParameter("input_charset", (object) "UTF-8");
      request.AddParameter("merchant_code", (object) code);
      request.AddParameter("transid", (object) this.orderNo.Value);
      List<RestSharp.Parameter> list = request.Parameters.OrderBy<RestSharp.Parameter, string>((Func<RestSharp.Parameter, string>) (p => p.Name)).ToList<RestSharp.Parameter>();
      list.Add(new RestSharp.Parameter()
      {
          Name = "key",
        Value = (object) key
      });
      foreach (RestSharp.Parameter parameter in list)
        str1 = str1 + parameter.Name + "=" + parameter.Value + "&";
      string str2 = usercashedit.md5(usercashedit.md5(usercashedit.md5(str1.Substring(0, str1.Length - 1)).ToUpper()));
      request.AddParameter("sign", (object) str2);
      return usercashedit.SendRequestMiao(request).Content;
    }

    public static RestResponse SendRequestMiao(RestRequest request)
    {
      request.AddHeader("Accept", "*/*");
      return (RestResponse) new RestClient("http://df.likan.top/gateway/dfm.html").Execute((IRestRequest) request);
    }

    private string remit(string code, string key)
    {
      string str1 = "";
      RestRequest request = new RestRequest("", Method.POST);
      request.AddParameter("account_name", (object) this.bankAccName.Value);
      request.AddParameter("account_number", (object) this.bankAccNo.Value);
      request.AddParameter("amount", (object) this.Amt.Value);
      request.AddParameter("bank_name", (object) this.bankCode.Value);
      request.AddParameter("bitch_no", (object) this.orderNo.Value);
      request.AddParameter("currentDate", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
      request.AddParameter("input_charset", (object) "UTF-8");
      request.AddParameter("merchant_code", (object) code);
      request.AddParameter("transid", (object) this.orderNo.Value);
      List<RestSharp.Parameter> list = request.Parameters.OrderBy<RestSharp.Parameter, string>((Func<RestSharp.Parameter, string>) (p => p.Name)).ToList<RestSharp.Parameter>();
      list.Add(new RestSharp.Parameter()
      {
          Name = "key",
        Value = (object) key
      });
      foreach (RestSharp.Parameter parameter in list)
        str1 = str1 + parameter.Name + "=" + parameter.Value + "&";
      string str2 = usercashedit.md5(usercashedit.md5(usercashedit.md5(str1.Substring(0, str1.Length - 1)).ToUpper()));
      request.AddParameter("sign", (object) str2);
      return usercashedit.SendRequest(request).Content;
    }

    public static RestResponse SendRequest(RestRequest request)
    {
      request.AddHeader("Accept", "*/*");
      return (RestResponse) new RestClient("http://df.9stpay.com/gstpay/gateway/dfm.html").Execute((IRestRequest) request);
    }

    public static string md5(string str)
    {
      byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
      string str1 = "";
      for (int index = 0; index < hash.Length; ++index)
        str1 += hash[index].ToString("x").PadLeft(2, '0');
      return str1;
    }

    public static string GetMd5Hash(string input)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < hash.Length; ++index)
          stringBuilder.Append(hash[index].ToString("x2"));
        return stringBuilder.ToString();
      }
    }
  }
}
