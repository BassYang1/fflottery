// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxHistory
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.Collect;
using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp
{
  public partial class ajaxHistory : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxGetList_User":
          this.ajaxGetList_User();
          break;
        case "ajaxGetChargeCashList":
          this.ajaxGetChargeCashList();
          break;
        case "GetUserTotalList":
          this.GetUserTotalList();
          break;
        case "GetLotteryOpenList":
          this.GetLotteryOpenList();
          break;
        case "ajaxGetListDay":
          this.ajaxGetListDay();
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
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      string str3 = this.q("tid");
      string str4 = this.q("sel");
      string str5 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "" + "UserId =" + this.AdminId;
      if (!string.IsNullOrEmpty(str5))
        _wherestr1 = _wherestr1 + " and " + str4 + " like '%" + str5 + "%'";
      if (!string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and Code =" + str3;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetHisStoryJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetList_User()
    {
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      string str3 = this.q("tid");
      string str4 = this.q("sel");
      string str5 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "" + "usercode like '%" + Strings.PadLeft(this.AdminId) + "%'";
      if (!string.IsNullOrEmpty(str5))
        _wherestr1 = _wherestr1 + " and " + str4 + " like '%" + str5 + "%'";
      if (!string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and Code =" + str3;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetHisStoryJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetChargeCashList()
    {
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      string str3 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str4 = "UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
      if (!string.IsNullOrEmpty(str3))
        str4 = str4 + " and Uname like '%" + str3 + "%'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        str4 = str4 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _wherestr1 = str4 + " and Code in (1,2)";
      string _jsonstr = "";
      new WebAppListOper().GetHisStoryJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void GetUserTotalList()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserMoneyStatDAL().GetUserTotalList(this.AdminId, ref _jsonstr);
      this._response = this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"table\":" + _jsonstr + "}";
    }

    public void GetLotteryOpenList()
    {
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(this.ConvertXMLToDataSet(Public.GetOpenListJson(Convert.ToInt32(this.q("lid")))).Tables[0]) + "}";
    }

    private void ajaxGetListDay()
    {
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      string str3 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = Convert.ToDateTime(this.StartTime).AddDays(-30.0).ToString("yyyy-MM-dd");
      if (str2.Trim().Length == 0)
        str2 = Convert.ToDateTime(this.EndTime).AddDays(1.0).ToString("yyyy-MM-dd");
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string FldName = "STime";
      string str4 = " STime >='" + str1 + "' and STime <='" + str2 + "'";
      string whereStr = string.IsNullOrEmpty(str3) ? str4 + " and UserId = " + this.AdminId : str4 + " and dbo.f_GetUserName(UserId) = '" + str3 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserMoneyStatAllDayOfUser");
      string sql0 = SqlHelp.GetSql0("*,-total as total2", "V_UserMoneyStatAllDayOfUser", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(80, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
