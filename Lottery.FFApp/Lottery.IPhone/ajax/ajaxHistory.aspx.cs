// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxHistory
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.Utils;
using System;

namespace Lottery.IPhone
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
        case "ajaxGetChargeCashList":
          this.ajaxGetChargeCashList();
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
      string str3 = this.q("lid");
      string str4 = this.q("sid");
      string str5 = this.q("tid");
      string str6 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str7 = "";
      string _wherestr1;
      if (string.IsNullOrEmpty(str6))
        _wherestr1 = str7 + "UserId =" + this.AdminId;
      else
        _wherestr1 = str7 + "dbo.f_GetUserCode(UserId) like '%" + Strings.PadLeft(this.AdminId) + "%' and UserId<>" + this.AdminId + " and Uname like '%" + str6 + "%'";
      if (!string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        _wherestr1 = _wherestr1 + " and SingleMoney ='" + str4 + "'";
      if (!string.IsNullOrEmpty(str5))
        _wherestr1 = _wherestr1 + " and Code =" + str5;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new HistoryDAL().GetListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetChargeCashList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("type");
      string str4 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str5 = "";
      string str6;
      if (string.IsNullOrEmpty(str3))
        str6 = str5 + "UserId =" + this.AdminId;
      else
        str6 = str5 + "dbo.f_GetUserCode(UserId) like '%" + Strings.PadLeft(this.AdminId) + "%' and UserId<>" + this.AdminId + " and Uname like '%" + str4 + "%'";
      if (string.IsNullOrEmpty(str4))
        str6 = str6 + " and Uname like '%" + str4 + "%'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        str6 = str6 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _wherestr1 = str6 + " and Code in (1,2,3,10,11,15)";
      string _jsonstr = "";
      new HistoryDAL().GetListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }
  }
}
