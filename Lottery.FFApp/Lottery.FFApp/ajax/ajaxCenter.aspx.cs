// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxCenter
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp
{
  public partial class ajaxCenter : UserCenterSession
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
        case "ajaxGetUserInfo":
          this.ajaxGetUserInfo();
          break;
        case "ajaxUserLoginList":
          this.ajaxUserLoginList();
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

    private void ajaxGetUserInfo()
    {
      if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
      {
        this.AdminId = Cookie.GetValue(this.site.CookiePrev + "WebApp", "id");
        if (this.AdminId.Length != 0)
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select * from Web_UserInfo where Id=" + this.AdminId;
          DataTable dataTable = this.doh.GetDataTable();
          this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
          dataTable.Clear();
          dataTable.Dispose();
        }
        else
          this._response = "{\"result\" :\"0\",\"returnval\" :\"登录超时，请重新登录！\"}";
      }
      else
        this._response = "{\"result\" :\"0\",\"returnval\" :\"登录超时，请重新登录！\"}";
    }

    private void ajaxUserLoginList()
    {
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string _wherestr1 = "UserId=" + this.AdminId;
      string _jsonstr = "";
      new WebAppListOper().GetUserLoginListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }
  }
}
