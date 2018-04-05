// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxBank
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;

namespace Lottery.IPhone
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

    private void ajaxGetChargeSetList()
    {
        string id = this.q("id");
        string code = this.q("code");
        string _jsonstr = "";

        if (!string.IsNullOrEmpty(code))
        {
            new Lottery.DAL.Flex.UserBankDAL().GetIphoneChargeSetJSONByCode(code, ref _jsonstr);
        }
        else
        {
            new Lottery.DAL.Flex.UserBankDAL().GetIphoneChargeSetJSON(id, ref _jsonstr);
        }

        this._response = _jsonstr;
    }

    private void ajaxAddBank()
    {
      string PayMethod = this.f("Bank");
      string PayBank = this.f("BankName");
      string PayBankAddress = this.f("Address");
      string PayAccount = this.f("PayAccount");
      string PayName = this.f("PayName");
      this.f("PayPwd");
      this._response = new Lottery.DAL.Flex.UserBankDAL().Save(this.AdminId, PayMethod, PayBank, PayBankAddress, PayAccount, PayName, "", "");
    }
  }
}
