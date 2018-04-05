// Decompiled with JetBrains decompiler
// Type: Lottery.Admin._other_ajax
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public class _other_ajax : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxAdminToLoginWeb":
          this.ajaxAdminToLoginWeb();
          break;
        case "ajaxGetCurTime":
          this.ajaxGetCurTime();
          break;
        case "ajaxPopInfo":
          this.ajaxPopInfo();
          break;
        case "leftmenu":
          this.GetLeftMenu();
          break;
        case "login":
          this.ajaxLogin();
          break;
        case "logout":
          this.ajaxLogout();
          break;
        case "chkadminpower":
          this.ChkAdminPower();
          break;
        case "ajaxChinese2Pinyin":
          this.ajaxChinese2Pinyin();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this.Admin_Load("", "json");
      this._response = this.JsonResult(1, "成功登录");
    }

    private void ajaxAdminToLoginWeb()
    {
      string str = this.f("id");
      this.f("name");
      string _sessionId = this.f("cookieId");
      this.f("point");
      int iExpires = 604800;
      new UserDAL().ChkAutoLoginWebApp(str.Trim(), _sessionId, iExpires);
      this._response = this.JsonResult(1, "");
    }

    private void ajaxGetCurTime()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "select getdate() as Time";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxPopInfo()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "State=0 and state<>99";
      int num1 = this.doh.Count("N_UserGetCash");
      this.doh.Reset();
      this.doh.ConditionExpress = "datediff(minute,ontime ,getdate())<5 and Source=0";
      int num2 = this.doh.Count("N_User");
      this.doh.Reset();
      this.doh.ConditionExpress = "datediff(minute,ontime ,getdate())<5 and Source=1";
      int num3 = this.doh.Count("N_User");
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"title\":\"提现请求\",\"usercount\":\"" + (object) num2 + "\",\"usercount2\":\"" + (object) num3 + "\",\"cashcount\":\"" + (object) num1 + "\"}";
    }

    private void GetLeftMenu()
    {
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + this.leftMenuJson() + "}";
    }

    private void ajaxLogin()
    {
      string _adminname = this.f("name");
      string _adminpass = this.f("pass");
      int num = this.Str2Int(this.f("type"), 0);
      int iExpires = 0;
      if (num > 0)
        iExpires = 86400 * num;
      this._response = new AdminDAL().ChkAdminLogin(_adminname, _adminpass, iExpires);
    }

    private void ajaxLogout()
    {
      new AdminDAL().ChkAdminLogout();
      this._response = this.JsonResult(1, "成功退出");
    }

    private void ChkAdminPower()
    {
      this.Admin_Load(this.q("power"), "json");
      this._response = this.JsonResult(1, "身份合法");
    }

    private void ajaxChinese2Pinyin()
    {
      this.Admin_Load("", "json");
      if (this.Str2Int(this.f("t"), 0) == 1)
        this._response = this.JsonResult(1, ChineseSpell.MakeSpellCode(this.f("chinese"), "", SpellOptions.TranslateUnknowWordToInterrogation));
      else
        this._response = this.JsonResult(1, ChineseSpell.MakeSpellCode(this.f("chinese"), "", SpellOptions.FirstLetterOnly));
    }
  }
}
