// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxEmail
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;

namespace Lottery.IPhone
{
  public partial class ajaxEmail : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetSendList":
          this.ajaxGetSendList();
          break;
        case "ajaxGetReceiveList":
          this.ajaxGetReceiveList();
          break;
        case "ajaxGetNewsContent":
          this.ajaxGetNewsContent();
          break;
        case "ajaxGetListCount":
          this.ajaxGetListCount();
          break;
        case "ajaxAllState":
          this.ajaxAllState();
          break;
        case "ajaxAllDel":
          this.ajaxAllDel();
          break;
        case "ajaxDel":
          this.ajaxDel();
          break;
        case "ajaxGetUserList":
          this.ajaxGetUserList();
          break;
        case "Send":
          this.Send();
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

    private void ajaxGetSendList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      if (str1.Trim().Length == 0)
        str1 = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd HH:mm:ss");
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "IsDel=0 and SendId =" + this.AdminId;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new UserEmailDAL().GetListJSON(_thispage, _pagesize, _wherestr1, this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetReceiveList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("type");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      if (str1.Trim().Length == 0)
        str1 = DateTime.Now.AddDays(-10.0).ToString("yyyy-MM-dd HH:mm:ss");
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "IsDel=0 and ReceiveId =" + this.AdminId;
      if (str3 == "0")
        _wherestr1 += " and IsRead =0";
      if (str3 == "1")
        _wherestr1 += " and IsRead =0";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new UserEmailDAL().GetListJSON(_thispage, _pagesize, _wherestr1, this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetNewsContent()
    {
      string str = "id =" + this.q("id");
      this._response = "".Replace("<br/>", "");
    }

    private void ajaxGetListCount()
    {
      string _wherestr1 = "ReceiveId =" + this.AdminId + " and IsRead =0";
      string _jsonstr = "";
      new UserEmailDAL().GetListCount(_wherestr1, ref _jsonstr);
      this._response = _jsonstr.Replace("<br/>", "");
    }

    private void ajaxAllDel()
    {
      string str = this.f("ids");
      char[] chArray = new char[1]{ ',' };
      foreach (string _id in str.Split(chArray))
        new UserEmailDAL().Deletes(_id);
    }

    private void ajaxAllState()
    {
      string str = this.f("ids");
      char[] chArray = new char[1]{ ',' };
      foreach (string _id in str.Split(chArray))
        new UserEmailDAL().UpdateState(_id);
    }

    private void ajaxDel()
    {
      new UserEmailDAL().Deletes(this.f("id"));
    }

    private void ajaxGetUserList()
    {
      string _wherestr1 = "parentId=" + this.AdminId;
      string _jsonstr = "";
      new UserDAL().GetListJSON(1, 99999, _wherestr1, "desc", "Id", ref _jsonstr);
      this._response = _jsonstr;
    }

    private void Send()
    {
      string Title = this.f("title");
      string Contents = this.f("content");
      string str1 = this.f("type");
      string str2 = this.f("name");
      if (str1.Equals("1"))
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "Id=@Id";
        this.doh.AddConditionParameter("@Id", (object) this.AdminId);
        string ReceiveId = string.Concat(this.doh.GetField("N_User", "ParentId"));
        if (ReceiveId.Equals("0"))
          this._response = this.JsonResult(1, "您没有上级不能发送!");
        else if (new UserEmailDAL().Save(this.AdminId, ReceiveId, Title, Contents) > 0)
        {
          new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员发送邮件！");
          this._response = this.JsonResult(1, "邮件发送成功!");
        }
        else
          this._response = this.JsonResult(1, "邮件发送失败!");
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "UserName=@UserName";
        this.doh.AddConditionParameter("@UserName", (object) str2);
        string ReceiveId = string.Concat(this.doh.GetField("N_User", "Id"));
        if (string.IsNullOrEmpty(ReceiveId))
        {
          this._response = this.JsonResult(1, "您输入的账号不正确!");
        }
        else
        {
          new UserEmailDAL().Save(this.AdminId, ReceiveId, Title, Contents);
          new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员发送邮件！");
          this._response = this.JsonResult(1, "邮件发送成功!");
        }
      }
    }
  }
}
