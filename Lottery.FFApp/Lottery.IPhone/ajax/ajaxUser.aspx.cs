// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxUser
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Configuration;
using System.Data;

namespace Lottery.IPhone
{
  public partial class ajaxUser : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "changepass":
          this.ajaxChangeUserPwd();
          break;
        case "moneypass":
          this.ajaxChangeMoneyPwd();
          break;
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxGetTotalList":
          this.ajaxGetTotalList();
          break;
        case "ajaxGetTeamTotalList":
          this.ajaxGetTeamTotalList();
          break;
        case "ajaxGetTeamType":
          this.ajaxGetTeamType();
          break;
        case "ajaxRegiter":
          this.ajaxRegiter();
          break;
        case "ajaxGetRegStrList":
          this.ajaxGetRegStrList();
          break;
        case "ajaxRegStrAll":
          this.ajaxRegStrAll();
          break;
        case "ajaxVerify":
          this.ajaxVerify();
          break;
        case "ajaxVerifyExist":
          this.ajaxVerifyExist();
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

    private void ajaxRegiter()
    {
      this._response = new Lottery.DAL.Flex.UserDAL().Register(this.AdminId, this.f("type"), this.f("name"), this.f("pwd"), this.f("point"));
    }

    private void ajaxGetList()
    {
      string str1 = this.q("keys");
      string str2 = this.q("moneymin");
      string str3 = this.q("moneymax");
      string str4 = this.q("pointmin");
      string str5 = this.q("pointmax");
      string orderby = this.q("orderby");
      string order = this.q("order");
      string str6 = this.q("group");
      string str7 = this.q("online");
      this.Str2Int(this.q("gId"), 0);
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string _wherestr1 = str1.Trim().Length <= 0 ? "parentId=" + this.AdminId : "dbo.f_GetUserCode(Id) like '%" + Strings.PadLeft(this.AdminId) + "%'" + " and UserName LIKE '%" + str1 + "%'";
      if (!string.IsNullOrEmpty(str2) && !string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and Money >=" + str2 + " and Money<=" + str3;
      if (!string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
        _wherestr1 = _wherestr1 + " and Convert(decimal(18, 2),Replace(Point,'%','')) >=" + str4 + " and Convert(decimal(18, 2),Replace(Point,'%',''))<=" + str5;
      if (!string.IsNullOrEmpty(str6))
        _wherestr1 = _wherestr1 + " and usergroup =" + str6;
      if (!string.IsNullOrEmpty(str7))
        _wherestr1 = _wherestr1 + " and IsOnline =" + str7;
      if (string.IsNullOrEmpty(orderby))
        orderby = "desc";
      if (string.IsNullOrEmpty(order))
        order = "Id";
      string _jsonstr = "";
      new Lottery.DAL.UserDAL().GetListJSON(_thispage, _pagesize, _wherestr1, orderby, order, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetTotalList()
    {
      int PageIndex = this.Int_ThisPage();
      int PageSize = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "userId =" + this.AdminId;
      string sql0 = SqlHelp.GetSql0("[sort],\r\n                                                    [Name],\r\n                                                    isnull(sum(Charge),0) as Charge,\r\n                                                    isnull(sum(getcash),0) as getcash, \r\n                                                    isnull(sum(bet),0) as bet ,\r\n                                                    isnull(sum(win),0) as win,\r\n                                                    isnull(sum(Point),0) as Point,\r\n                                                    isnull(sum(Give),0) as Give,\r\n                                                    isnull(sum(Change),0) as Change,\r\n                                                    isnull(sum(AgentFH),0) as AgentFH, \r\n                                                    isnull(sum(total),0) as total,\r\n                                                    isnull(sum(betno),0) as betno", "V_UserMoneyStatAllUserTotal", "sort", PageSize, PageIndex, "asc", whereStr, "Name,sort");
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetTeamTotalList()
    {
        int PageIndex = this.Int_ThisPage();
        int PageSize = this.Str2Int(this.q("pagesize"), 20);
        string str1 = this.q("flag");
        string str2 = "dbo.f_GetUserCode(userId) like '%" + Strings.PadLeft(this.AdminId) + "%'";
        DateTime dateTime1 = DateTime.Now;
        dateTime1 = dateTime1.AddDays(0.0);
        string str3 = dateTime1.ToString("yyyy-MM-dd") + " 00:00:00";
        DateTime dateTime2 = DateTime.Now;
        dateTime2 = dateTime2.AddDays(0.0);
        string str4 = dateTime2.ToString("yyyy-MM-dd") + " 23:59:59";
        if (string.IsNullOrEmpty(str1))
            str1 = "1";
        string whereStr = str2 + " and sort=" + str1;
        string fields = @"[sort],
                    isnull(sum(Charge),0) as Charge,
                    isnull(sum(getcash),0) as getcash,
                    isnull(sum(bet),0) as bet ,
                    isnull(sum(win),0) as win,
                    isnull(sum(Point),0) as Point,
                    isnull(sum(Give),0) as Give,
                    isnull(sum(other),0) as other,
                    isnull(sum(-total),0) as total,
                    isnull(sum(moneytotal),0) as moneytotal";

        string sql0 = SqlHelp.GetSql0(fields, "V_UserMoneyStatAllUserTotal", "sort", PageSize, PageIndex, "asc", whereStr, "sort");
        this.doh.Reset();
        this.doh.SqlCmd = sql0;
        DataTable dataTable = this.doh.GetDataTable();
        this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
    }

    private void ajaxGetTeamType()
    {
      string str = string.Format("SELECT \r\n                            Convert(varchar(10),STime,120) as STime,\r\n                            isnull(sum(Charge),0) as Charge,\r\n                            isnull(sum(getcash),0)-isnull(sum(GetCashErr),0) as getcash, \r\n                            isnull(sum(Bet),0)+isnull(sum(BetChase),0)-isnull(sum(WinChase),0)-isnull(sum(Cancellation),0) as bet ,\r\n                            isnull(sum(Win),0) as win,\r\n                            isnull(sum(Point),0) as Point,\r\n                            isnull(sum(Give),0) as Give,\r\n                            isnull(sum(Change),0) as Change,\r\n                            isnull(sum(AgentFH),0) as AgentFH, \r\n                            (isnull(sum(WinChase),0)+isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(AgentFH),0)+isnull(sum(Cancellation),0))-(isnull(sum(Bet),0)+isnull(sum(BetChase),0)) as total,\r\n                            (SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and Convert(varchar(10),STime,120)=Convert(varchar(10),a.STime,120)) as betno\r\n                            FROM [N_UserMoneyStatAll] a with(nolock)\r\n                            where dbo.f_GetUserCode(userId) like '%{0}%' and Id<>{1} and convert(varchar(7),STime,120)=convert(varchar(7),getdate(),120) \r\n                            group by Convert(varchar(10),STime,120)", (object) Strings.PadLeft(this.AdminId), (object) this.AdminId);
      this.doh.Reset();
      this.doh.SqlCmd = str;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetRegStrList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "UserId=" + this.AdminId;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserRegLink");
      string sql0 = SqlHelp.GetSql0("row_number() over (order by Point desc) as rowid,*", "N_UserRegLink", "Point", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, num2 * (num1 - 1)) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxRegStrAll()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "select Point from N_User with(nolock) where Id=" + this.AdminId;
      DataTable dataTable1 = this.doh.GetDataTable();
      for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
      {
        this.doh.Reset();
        this.doh.SqlCmd = "SELECT Point FROM [N_UserLevel] where point>=100 and Point<" + (object) Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          string encryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
          string Url = ConfigurationManager.AppSettings["RootUrl"].ToString() + "/register.aspx?u=" + this.EncryptDES(this.AdminId + "@" + dataTable2.Rows[index2]["Point"].ToString(), encryptKey).Replace("+", "@");
          new UserRegLinkDAL().SaveUserRegLink(this.AdminId, Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Url);
        }
      }
      new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员生成注册链接！");
      this._response = this.JsonResult(1, "注册链接全部生成成功！");
    }

    private void ajaxChangeUserPwd()
    {
      if (new Lottery.DAL.UserDAL().ChangeUserPassword(this.AdminId, this.f("oldpass"), this.f("newpass")))
      {
        new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改登录密码！");
        this._response = this.JsonResult(1, "密码修改成功");
      }
      else
        this._response = this.JsonResult(0, "旧密码错误");
    }

    private void ajaxChangeMoneyPwd()
    {
      if (new Lottery.DAL.UserDAL().ChangePayPassword(this.AdminId, this.f("oldpass"), this.f("newpass")))
      {
        new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员修改银行密码！");
        this._response = this.JsonResult(1, "密码修改成功");
      }
      else
        this._response = this.JsonResult(0, "旧密码错误");
    }

    private void ajaxVerifyExist()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id and Question<>'' and Answer<>''";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      if (this.doh.Exist("N_User"))
        this._response = this.JsonResult(1, "验证信息绑定成功");
      else
        this._response = this.JsonResult(0, "验证信息绑定失败");
    }

    private void ajaxVerify()
    {
      string str1 = this.f("question");
      string str2 = this.f("answer");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      this.doh.AddFieldItem("Question", (object) str1);
      this.doh.AddFieldItem("Answer", (object) str2);
      this.doh.Update("N_User");
      new LogSysDAL().Save("会员管理", "Id为" + this.AdminId + "的会员绑定验证信息！");
      this._response = this.JsonResult(1, "验证信息绑定成功");
    }
  }
}
