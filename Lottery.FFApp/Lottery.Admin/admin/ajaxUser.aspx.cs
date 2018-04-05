// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxUser
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxUser : AdminCenter
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
        case "ajaxGetReturnList":
          this.ajaxGetReturnList();
          break;
        case "ajaxGetFKList":
          this.ajaxGetFKList();
          break;
        case "ajaxGetFKListOnLine":
          this.ajaxGetFKListOnLine();
          break;
        case "ajaxGetFKProListSub":
          this.ajaxGetFKProListSub();
          break;
        case "ajaxGetQuotasList":
          this.ajaxGetQuotasList();
          break;
        case "ajaxDel":
          this.ajaxDel();
          break;
        case "ajaxAllDel":
          this.ajaxAllDel();
          break;
        case "ajaxDel2":
          this.ajaxDel2();
          break;
        case "ajaxAllDel2":
          this.ajaxAllDel2();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxUpdatePwd":
          this.ajaxUpdatePwd();
          break;
        case "ajaxCheckUserName":
          this.ajaxCheckUserName();
          break;
        case "clearLoginRecord":
          this.clearLoginRecord();
          break;
        case "ajaxGetDelList":
          this.ajaxGetDelList();
          break;
        case "ajaxDelStates":
          this.ajaxDelStates();
          break;
        case "ajaxOnline":
          this.ajaxOnline();
          break;
        case "ajaxAllOnline":
          this.ajaxAllOnline();
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

    private void ajaxCheckUserName()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "username=@username";
      this.doh.AddConditionParameter("@username", (object) this.q("txtUserName"));
      if (this.doh.Exist("N_User"))
        this._response = this.JsonResult(0, "此账号已存在，不能添加");
      else
        this._response = this.JsonResult(1, "帐号不存在，可以添加");
    }

    private void ajaxGetList()
    {
      string str1 = this.q("ip");
      string str2 = this.q("group");
      string str3 = this.q("online");
      string str4 = this.q("isenable");
      string str5 = this.q("nologin");
      string str6 = this.q("money1");
      string str7 = this.q("money2");
      string str8 = this.q("sel2");
      string str9 = this.q("uname");
      string str10 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=0";
      string FldName = "Id";
      if (!string.IsNullOrEmpty(str2))
      {
        whereStr = whereStr + " and UserGroup =" + str2;
        FldName = "Id";
      }
      if (!string.IsNullOrEmpty(str3))
      {
        whereStr = whereStr + " and IsOnline =" + str3;
        FldName = "Id";
      }
      if (!string.IsNullOrEmpty(str4))
      {
        whereStr = whereStr + " and isenable =" + str4;
        FldName = "Id";
      }
      if (!string.IsNullOrEmpty(str6) && !string.IsNullOrEmpty(str7))
      {
        whereStr = whereStr + " and Money >= " + str6 + " and Money<=" + str7;
        FldName = "Money";
      }
      if (!string.IsNullOrEmpty(str5))
      {
        whereStr = whereStr + " and datediff(day, OnTime, getdate()) >= " + str5;
        FldName = "datediff(day, OnTime, getdate())";
      }
      if (!string.IsNullOrEmpty(str1))
      {
        whereStr += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
        FldName = "Ip";
      }
      if (!string.IsNullOrEmpty(str10))
        whereStr = whereStr + " and ParentId=" + str10;
      else if (!string.IsNullOrEmpty(str9.Trim()))
      {
        if ("1".Equals(str8))
        {
          whereStr = whereStr + " and UserName = '" + str9.Trim() + "'";
          FldName = "UserName";
        }
        if ("2".Equals(str8))
        {
          whereStr = whereStr + " and Id = '" + str9.Trim() + "'";
          FldName = "Id";
        }
        if ("3".Equals(str8))
        {
          whereStr = whereStr + " and UPoint = '" + (object) (Convert.ToDecimal(str9.Trim()) * new Decimal(10)) + "'";
          FldName = "Point";
        }
        if ("4".Equals(str8))
        {
          whereStr = whereStr + " and Id in (select UserId from N_UserBank where PayAccount like '%" + str9.Trim() + "%')";
          FldName = "Id";
        }
        if ("5".Equals(str8))
        {
          whereStr = whereStr + " and Id in (select UserId from N_UserBank where PayName like '%" + str9.Trim() + "%')";
          FldName = "Id";
        }
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", FldName, num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetReturnList()
    {
      string str1 = this.q("money1");
      string str2 = this.q("money2");
      string str3 = this.q("score1");
      string str4 = this.q("score2");
      string str5 = this.q("ucode");
      string str6 = this.q("uname");
      this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=0";
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) string.Concat(this.Session["return"]));
      object field = this.doh.GetField("N_User", "ParentId");
      object obj = string.IsNullOrEmpty(string.Concat(field)) ? (object) 0 : field;
      if (!string.IsNullOrEmpty(string.Concat(obj)))
      {
        whereStr = whereStr + " and ParentId=" + obj;
        this.Session["return"] = obj;
      }
      if (!string.IsNullOrEmpty(str6))
        whereStr = whereStr + " and (UserName LIKE '%" + str6 + "%')";
      if (!string.IsNullOrEmpty(str5))
        whereStr = whereStr + " and len(UserCode) = " + str5;
      if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
        whereStr = whereStr + " and Money >= " + str1 + " and Money<=" + str2;
      if (!string.IsNullOrEmpty(str3) && !string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and Score >= " + str3 + " and Score<=" + str4;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxGetReturnList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetFKList()
    {
      string str1 = this.q("ip");
      string str2 = this.q("group");
      this.q("online");
      string str3 = this.q("sel1");
      string str4 = this.q("money1");
      string str5 = this.q("money2");
      string str6 = this.q("sel2");
      string str7 = this.q("uname");
      string str8 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=0 ";
      string FldName = "Id";
      if (!string.IsNullOrEmpty(str2))
      {
        whereStr = whereStr + " and UserGroup =" + str2;
        FldName = "Id";
      }
      if (!string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
      {
        if ("1".Equals(str3))
        {
          whereStr = whereStr + " and Money >= " + str4 + " and Money<=" + str5;
          FldName = "Money";
        }
        if ("2".Equals(str3))
        {
          whereStr = whereStr + " and Score >= " + str4 + " and Score<=" + str5;
          FldName = "Score";
        }
        if ("3".Equals(str3))
        {
          whereStr = whereStr + " and datediff(day, OnTime, getdate()) >= " + str4 + " and datediff(day, OnTime, getdate())<=" + str5;
          FldName = "datediff(day, OnTime, getdate())";
        }
      }
      if (!string.IsNullOrEmpty(str1))
      {
        whereStr += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
        FldName = "Ip";
      }
      if (!string.IsNullOrEmpty(str8))
        whereStr = whereStr + " and ParentId=" + str8;
      else if (!string.IsNullOrEmpty(str7.Trim()))
      {
        if ("1".Equals(str6))
        {
          whereStr = whereStr + " and ParentId = (select Id from N_User where UserName = '" + str7.Trim() + "')";
          FldName = "UserName";
        }
      }
      else
        whereStr += " and UserName=''";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", FldName, num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetFKListOnLine()
    {
      string str1 = this.q("ip");
      string str2 = this.q("group");
      this.q("online");
      string str3 = this.q("sel1");
      string str4 = this.q("money1");
      string str5 = this.q("money2");
      string str6 = this.q("sel2");
      string str7 = this.q("uname");
      string str8 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=0 ";
      string FldName = "Id";
      if (!string.IsNullOrEmpty(str2))
      {
        whereStr = whereStr + " and UserGroup =" + str2;
        FldName = "Id";
      }
      if (!string.IsNullOrEmpty(str4) && !string.IsNullOrEmpty(str5))
      {
        if ("1".Equals(str3))
        {
          whereStr = whereStr + " and Money >= " + str4 + " and Money<=" + str5;
          FldName = "Money";
        }
        if ("2".Equals(str3))
        {
          whereStr = whereStr + " and Score >= " + str4 + " and Score<=" + str5;
          FldName = "Score";
        }
        if ("3".Equals(str3))
        {
          whereStr = whereStr + " and datediff(day, OnTime, getdate()) >= " + str4 + " and datediff(day, OnTime, getdate())<=" + str5;
          FldName = "datediff(day, OnTime, getdate())";
        }
      }
      if (!string.IsNullOrEmpty(str1))
      {
        whereStr += " and Ip in(select Ip from N_User with(nolock) group by Ip having count(Ip)>1)";
        FldName = "Ip";
      }
      if (!string.IsNullOrEmpty(str8))
        whereStr = whereStr + " and ParentId=" + str8 + " and IsOnline =1";
      else if (!string.IsNullOrEmpty(str7.Trim()))
      {
        if ("1".Equals(str6))
        {
          whereStr = whereStr + " and ParentId = (select Id from N_User where UserName = '" + str7.Trim() + "')" + " and IsOnline =1";
          FldName = "UserName";
        }
      }
      else
        whereStr += " and UserName=''";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", FldName, num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void ajaxGetFKProListSub()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string inputString = this.q("id");
      string str3 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str4 = " STime >='" + str1 + "' and STime <'" + str2 + "'";
      bool flag = true;
      if (string.IsNullOrEmpty(inputString))
      {
        if (!string.IsNullOrEmpty(str3.Trim()))
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select Id from N_User where UserName='" + str3 + "'";
          DataTable dataTable = this.doh.GetDataTable();
          if (dataTable.Rows.Count > 0)
            inputString = dataTable.Rows[0]["Id"].ToString();
          else
            flag = false;
        }
        else
        {
          inputString = "-1";
          flag = false;
        }
      }
      if (flag)
      {
        int num3 = 0;
        string str5 = string.Format("select {1} as totalcount, {0} as UserID,\r\n                                            (select Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2))) from N_User with(nolock) where Id={0} ) as userpoint,\r\n                                            dbo.f_GetUserName({0}) as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where Id = {0}) as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {2} and UserId={0}", (object) inputString, (object) num3, (object) str4) + " union all ";
        this.doh.Reset();
        this.doh.ConditionExpress = " ParentId = " + inputString;
        int totalCount = this.doh.Count("N_User");
        this.doh.Reset();
        this.doh.SqlCmd = SqlHelp.GetSql0("Id,UserName,Money,Point", "N_User", "ID", num2, num1, "asc", " ParentId = " + inputString);
        DataTable dataTable1 = this.doh.GetDataTable();
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
        {
          string str6 = str4 + " and UserCode like '%" + Strings.PadLeft(dataTable1.Rows[index]["Id"].ToString()) + "%'";
          str5 = str5 + string.Format("select {0} as totalcount, {1} as UserID,\r\n                                            Convert(varchar(10),cast(round({2}/10.0,2) as numeric(5,2))) as userpoint,\r\n                                            '{3}' as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{1},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            from Flex_UserMoneyStatAll b with(nolock)\r\n                                            where {5}", (object) totalCount, (object) dataTable1.Rows[index]["Id"].ToString(), (object) dataTable1.Rows[index]["Point"].ToString(), (object) dataTable1.Rows[index]["UserName"].ToString(), (object) dataTable1.Rows[index]["Money"].ToString(), (object) str6) + " union all ";
        }
        string str7 = str5 + string.Format("select {2} as totalcount, '-1' as UserID,'合计' as userpoint,'' as userName,\r\n                                            (select isnull(sum(money),0) from N_User with(nolock) where UserCode like '%,{0},%') as money,\r\n                                            isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0)-isnull(sum(b.Cancellation),0)  Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change,\r\n                                            (isnull(sum(Win),0)+isnull(sum(Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0) as total,\r\n                                            (isnull(sum(Charge),0)-isnull(sum(getcash),0)) as moneytotal\r\n                                            FROM Flex_UserMoneyStatAll b with(nolock) where {1}", (object) inputString, (object) (str4 + " and UserCode like '%" + Strings.PadLeft(inputString) + "%'"), (object) totalCount);
        this.doh.Reset();
        this.doh.SqlCmd = str7;
        DataTable dataTable2 = this.doh.GetDataTable();
        this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable2) + "}";
        dataTable2.Clear();
        dataTable2.Dispose();
      }
      else
        this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"recordcount\":0,\"table\": []}";
    }

    private void ajaxGetQuotasList()
    {
      string str = this.q("uname");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (!string.IsNullOrEmpty(str))
        whereStr = whereStr + " UserName LIKE '%" + str + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserQuotasList");
      string sql0 = SqlHelp.GetSql0("*", "V_UserQuotasList", "Id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxDel()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      this.doh.AddFieldItem("isDel", (object) 1);
      if (this.doh.Update("N_User") > 0)
      {
        new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "删除了Id为" + userid + "的会员");
        this._response = this.JsonResult(1, "删除成功");
      }
      else
        this._response = this.JsonResult(0, "删除失败");
    }

    private void ajaxAllDel()
    {
      string[] strArray = this.f("ids").Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=@id";
        this.doh.AddConditionParameter("@id", (object) strArray[index]);
        this.doh.AddFieldItem("isDel", (object) 1);
        this.doh.AddFieldItem("IsOnline", (object) 0);
        this.doh.AddFieldItem("SessionId", (object) Guid.NewGuid().ToString().Replace("-", ""));
        this.doh.Update("N_User");
        new LogAdminOperDAL().SaveLog(this.AdminId, strArray[index], "会员管理", "删除了Id为" + strArray[index] + "的会员");
      }
      this._response = this.JsonResult(1, "删除成功");
    }

    private void ajaxDel2()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      if (this.doh.Delete("N_User") > 0)
      {
        new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "彻底删除了Id为" + userid + "的会员");
        this._response = this.JsonResult(1, "删除成功");
      }
      else
        this._response = this.JsonResult(0, "删除失败");
    }

    private void ajaxAllDel2()
    {
      string[] strArray = this.f("ids").Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=@id";
        this.doh.AddConditionParameter("@id", (object) strArray[index]);
        this.doh.Delete("N_User");
        new LogAdminOperDAL().SaveLog(this.AdminId, strArray[index], "会员管理", "彻底删除Id为" + strArray[index] + "的会员");
      }
      this._response = this.JsonResult(1, "删除成功");
    }

    private void ajaxStates()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      int int32 = Convert.ToInt32(this.doh.GetField("N_User", "IsEnable"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("IsEnable", (object) (int32 == 0 ? 1 : 0));
      this.doh.AddFieldItem("IsOnline", (object) 0);
      this.doh.AddFieldItem("SessionId", (object) Guid.NewGuid().ToString().Replace("-", ""));
      int num = this.doh.Update("N_User");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "锁定了Id为" + userid + "的会员");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxOnline()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("IsOnline", (object) 0);
      this.doh.AddFieldItem("SessionId", (object) Guid.NewGuid().ToString().Replace("-", ""));
      int num = this.doh.Update("N_User");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "强制Id为" + userid + "的会员下线");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxAllOnline()
    {
      string[] strArray = this.f("ids").Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "id=" + strArray[index];
        this.doh.AddFieldItem("IsOnline", (object) 0);
        this.doh.AddFieldItem("SessionId", (object) Guid.NewGuid().ToString().Replace("-", ""));
        this.doh.Update("N_User");
        new LogAdminOperDAL().SaveLog(this.AdminId, strArray[index], "会员管理", "强制Id为" + strArray[index] + "的会员下线");
      }
      this._response = this.JsonResult(1, "下线成功");
    }

    private void ajaxUpdatePwd()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) userid);
      this.doh.AddConditionParameter("@Password", (object) MD5.Last64(MD5.Lower32("123456")));
      int num = this.doh.Update("N_User");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "重置Id为" + userid + "的会员的密码为123456");
      if (num > 0)
        this._response = this.JsonResult(1, "重置成功");
      else
        this._response = this.JsonResult(0, "重置失败");
    }

    private void clearLoginRecord()
    {
      new LogSysDAL().DeleteUserLogs();
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "会员管理", "清空会员登陆日志");
      this._response = this.JsonResult(1, "成功清空");
    }

    private void ajaxGetDelList()
    {
      string str1 = this.q("ucode");
      string str2 = this.q("uname");
      string str3 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "isDel=1";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and ParentId=" + str3;
      if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + " and (UserName LIKE '%" + str2 + "%')";
      if (!string.IsNullOrEmpty(str1))
        whereStr = whereStr + " and len(UserCode) = " + str1;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("*", "V_User", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxDelStates()
    {
      string userid = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + userid;
      this.doh.AddFieldItem("IsDel", (object) 0);
      int num = this.doh.Update("N_User");
      new LogAdminOperDAL().SaveLog(this.AdminId, userid, "会员管理", "恢复了Id为" + userid + "的会员");
      if (num > 0)
        this._response = this.JsonResult(1, "恢复成功");
      else
        this._response = this.JsonResult(0, "恢复失败");
    }
  }
}
