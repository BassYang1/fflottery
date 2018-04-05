// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxBank
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;

namespace Lottery.WebApp
{
  public partial class ajaxBank : UserCenterSession
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
        case "ajaxGetChargeSet":
          this.ajaxGetChargeSet();
          break;
        case "ajaxGetChargeSetList":
          this.ajaxGetChargeSetList();
          break;
        case "ajaxAddBank":
          this.ajaxAddBank();
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
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserBankDAL().GetIphoneBankInfoJSON(this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetChargeSet()
    {
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserBankDAL().GetChargeSetJSON(ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetChargeSetList()
    {
      string Id = this.q("id");
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserBankDAL().GetIphoneChargeSetJSON(Id, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxAddBank()
    {
      this._response = new Lottery.DAL.Flex.UserBankDAL().Save(this.AdminId, this.f("Bank"), this.f("BankName"), this.f("Address"), this.f("PayAccount"), this.f("PayName"), this.f("PayPwd"));
    }
  }
}
