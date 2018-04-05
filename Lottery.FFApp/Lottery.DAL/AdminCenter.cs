// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.AdminCenter
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Lottery.DAL
{
  public class AdminCenter : AdminBasicPage
  {
    public string StrId = "0";
    protected string AdminId = "0";
    protected string AdminName = string.Empty;
    protected string AdminPass = string.Empty;
    protected string AdminSign = string.Empty;
    protected string AdminSetting = string.Empty;
    protected string AdminCookiess = string.Empty;
    protected int AdminGroupId = 1;
    public string StartTime = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
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
    protected bool AdminIsFounder;
    protected bool AdminIsSuper;

    protected void Admin_Load(string powerNum, string pageType)
    {
      if (pageType == "json" && !this.CheckFormUrl())
        this.Response.End();
      if (Cookie.GetValue(this.site.CookiePrev + "admin") != null)
      {
        this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
        this.AdminName = Cookie.GetValue(this.site.CookiePrev + "admin", "name");
        this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "admin", "cookiess");
        if (this.AdminId.Length != 0 && this.AdminName.Length != 0)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "id=@id";
          this.doh.AddConditionParameter("@id", (object) this.AdminId);
          if (this.doh.Count("Sys_Admin") >= 1)
            return;
          this.showErrMsg("请您登陆系统", pageType);
        }
        else
          this.showErrMsg("请您登陆系统", pageType);
      }
      else
        this.showErrMsg("请您登陆系统", pageType);
    }

    protected void showErrMsg(string msg, string pageType)
    {
      if (pageType != "json")
      {
        this.FinalMessage(msg, "login.aspx", 0);
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

    protected string leftMenuJson()
    {
      this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 1 * FROM [Sys_Admin] a left join [Sys_Role] b on a.RoleId=b.Id where a.Id=" + this.AdminId;
      DataTable dataTable1 = this.doh.GetDataTable();
      if (dataTable1.Rows.Count > 0)
      {
        this.AdminIsSuper = "1".Equals(dataTable1.Rows[0]["IsSuper"].ToString().Trim());
        this.AdminSetting = dataTable1.Rows[0]["Setting"].ToString();
      }
      else
      {
        this.AdminIsSuper = false;
        this.AdminSetting = "";
      }
      if (this.AdminSetting.Length > 2)
        this.AdminSetting = this.AdminSetting.Substring(1, this.AdminSetting.Length - 2);
      string str = "";
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0";
      if (!this.AdminIsSuper)
      {
        if (this.AdminSetting.Length > 2)
        {
          DbOperHandler doh = this.doh;
          doh.SqlCmd = doh.SqlCmd + " and Id in (" + this.AdminSetting + ")";
          str = " and Id in (" + this.AdminSetting + ")";
        }
        else
        {
          this.doh.SqlCmd += " and Id in (0)";
          str = " and Id in (0)";
        }
      }
      this.doh.SqlCmd += " ORDER BY Sort asc";
      DataTable dataTable2 = this.doh.GetDataTable();
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT * FROM Sys_Menu WHERE IsUsed=0 and pId=0";
      if (!this.AdminIsSuper && dataTable2.Rows.Count > 0)
      {
        DbOperHandler doh = this.doh;
        doh.SqlCmd = doh.SqlCmd + " and Id in (SELECT Pid FROM Sys_Menu WHERE IsUsed=0 " + str + " group by Pid)";
      }
      this.doh.SqlCmd += " ORDER BY Sort asc";
      return dtHelp.DT2JSONAdminLeft(this.doh.GetDataTable(), dataTable2);
    }

    protected void getWYBankDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT Id,Bank FROM [Sys_Bank] where IsUsed=0 ORDER BY Id asc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
        return;
      ddlClassId.Items.Add(new ListItem("全部", ""));
      ddlClassId.Items.Add(new ListItem("后台充值", "888"));
      ddlClassId.Items.Add(new ListItem("第三方充值", "999"));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Bank"].ToString(), dataTable.Rows[index]["Id"].ToString()));
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void getActiveEditDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Code],[Name] FROM [Act_ActiveSet] where Isuse=1";
      this.doh.SqlCmd += " ORDER BY Id";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Name"].ToString(), dataTable.Rows[index]["Code"].ToString()));
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void getEditDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],[Point],[Title],[Bonus],[Score],[Times],[Sort] FROM [N_UserLevel]";
      this.doh.SqlCmd += " ORDER BY Bonus desc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Bonus"].ToString() + "_" + Convert.ToDecimal(Convert.ToDecimal(dataTable.Rows[index]["Point"]) / new Decimal(10)).ToString("0.00") + "%", dataTable.Rows[index]["Point"].ToString()));
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void getRoleDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],[Name] FROM [Sys_Role] where IsUsed=0";
      this.doh.SqlCmd += " ORDER BY Sort";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ddlClassId.Items.Add(new ListItem(dataTable.Rows[index]["Name"].ToString(), dataTable.Rows[index]["Id"].ToString()));
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void getAgentDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      ddlClassId.Items.Add(new ListItem("不分红", "0"));
      ddlClassId.Items.Add(new ListItem("一级分红", "1"));
      ddlClassId.Items.Add(new ListItem("二级分红", "2"));
    }

    protected void getGroupDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],[Name] FROM N_UserGroup ORDER BY Id desc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[index]["Name"].ToString(), dataTable.Rows[index]["Id"].ToString()));
      dataTable.Clear();
      dataTable.Dispose();
    }

    protected void getLotteryDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT [Id],[Title] FROM Sys_Lottery ORDER BY Id asc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count == 0)
      {
        dataTable.Clear();
        dataTable.Dispose();
      }
      else
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          ddlClassId.Items.Add(new ListItem(" " + dataTable.Rows[index]["Title"].ToString(), dataTable.Rows[index]["Id"].ToString()));
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    protected void getTypeDropDownList(ref DropDownList ddlClassId, int ClassDepth)
    {
      if (this.Page.IsPostBack)
        return;
      ddlClassId.Items.Add(new ListItem("所有类型", ""));
      ddlClassId.Items.Add(new ListItem("账号充值", "1"));
      ddlClassId.Items.Add(new ListItem("账号提款", "2"));
      ddlClassId.Items.Add(new ListItem("提现失败", "3"));
      ddlClassId.Items.Add(new ListItem("投注扣款", "4"));
      ddlClassId.Items.Add(new ListItem("追号扣款", "5"));
      ddlClassId.Items.Add(new ListItem("追号返款", "6"));
      ddlClassId.Items.Add(new ListItem("游戏返点", "7"));
      ddlClassId.Items.Add(new ListItem("奖金派送", "8"));
      ddlClassId.Items.Add(new ListItem("撤单返款", "9"));
      ddlClassId.Items.Add(new ListItem("充值扣费", "10"));
      ddlClassId.Items.Add(new ListItem("上级充值", "11"));
      ddlClassId.Items.Add(new ListItem("活动礼金", "12"));
      ddlClassId.Items.Add(new ListItem("代理分红", "13"));
      ddlClassId.Items.Add(new ListItem("管理员减扣", "14"));
      ddlClassId.Items.Add(new ListItem("积分兑换", "15"));
    }

    protected string GetContentFile(string TxtFile)
    {
      return DirFile.ReadFile(TxtFile);
    }

    protected string EncryptDES(string encryptString, string encryptKey)
    {
      try
      {
        byte[] bytes1 = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
        byte[] keys = this.Keys;
        byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
        DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, keys), CryptoStreamMode.Write);
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
        CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
        cryptoStream.Write(buffer, 0, buffer.Length);
        cryptoStream.FlushFinalBlock();
        return Encoding.UTF8.GetString(memoryStream.ToArray());
      }
      catch
      {
        return decryptString;
      }
    }
  }
}
