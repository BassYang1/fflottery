// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxBet
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxBet : AdminCenter
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
        case "ajaxGetListOfMissing":
          this.ajaxGetListOfMissing();
          break;
        case "ajaxGetZHList":
          this.ajaxGetZHList();
          break;
        case "ajaxGetZH":
          this.ajaxGetZH();
          break;
        case "ajaxGetZHInfo":
          this.ajaxGetZHInfo();
          break;
        case "ajaxCancelTitle":
          this.ajaxCancelTitle();
          break;
        case "ajaxCancelTitleOfNo":
          this.ajaxCancelTitleOfNo();
          break;
        case "ajaxBetCanel":
          this.ajaxBetCanel();
          break;
        case "ajaxBetCheat":
          this.ajaxBetCheat();
          break;
        case "ajaxOper":
          this.ajaxOper();
          break;
        case "ajaxPaiJiangBetId":
          this.ajaxPaiJiangBetId();
          break;
        case "ajaxBetOpers":
          this.ajaxBetOpers();
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

    private void ajaxPaiJiangBetId()
    {
      this.Str2Int(this.q("flag"), 0);
      string[] strArray = this.f("ids").Split(',');
      string str = "";
      for (int index = 0; index < strArray.Length; ++index)
        str += new LotteryCheck().RunOfBetId(strArray[index]);
      if (string.IsNullOrEmpty(str))
        this._response = this.JsonResult(1, "派奖成功");
      else
        this._response = this.JsonResult(0, str);
    }

    private void ajaxCancelTitle()
    {
      int LotteryId = this.Str2Int(this.q("flag"), 0);
      string str1 = this.f("ids");
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string title in str2.Split(chArray))
        new LotteryCheck().Cancel(LotteryId, title, 2);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + str1 + "进行撤单！");
      this._response = this.JsonResult(1, "撤单成功");
    }

    private void ajaxCancelTitleOfNo()
    {
      int LotteryId = this.Str2Int(this.q("flag"), 0);
      string str1 = this.f("ids");
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string title in str2.Split(chArray))
        new LotteryCheck().Cancel(LotteryId, title, 0);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + str1 + "进行撤单！");
      this._response = this.JsonResult(1, "撤单成功");
    }

    private void ajaxBetCanel()
    {
      this.Str2Int(this.q("flag"), 0);
      string str1 = this.f("ids");
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string betId in str2.Split(chArray))
        new LotteryCheck().CancelOfBetId(betId);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + str1 + "进行撤单！");
      this._response = this.JsonResult(1, "撤单成功");
    }

    private void ajaxBetCheat()
    {
      this.Str2Int(this.q("flag"), 0);
      string str = this.f("ids");
      char[] chArray = new char[1]{ ',' };
      foreach (object obj in str.Split(chArray))
        new UserBetDAL().BetCheat(obj.ToString());
      this._response = this.JsonResult(1, "加入改单列表成功，请到待修改订单中修改");
    }

    private void ajaxGetList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("state");
      string str4 = this.q("lid");
      string str5 = this.q("pid");
      string str6 = this.q("sel");
      string str7 = this.q("u");
      string str8 = this.q("IsCheat");
      string str9 = this.q("yc");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str10 = "";
      string whereStr = " STime2 >='" + str1 + "' and STime2 <'" + str2 + "'";
      string str11 = this.q("id");
      if (!string.IsNullOrEmpty(str11))
      {
        whereStr = whereStr + " and ssid ='" + str11 + "'";
      }
      else
      {
        if (!string.IsNullOrEmpty(str7))
        {
          string str12 = str7.Trim();
          whereStr = !str6.Equals("username") ? (!str6.Equals("IssueNum") ? whereStr + " and ssid = '" + str12 + "'" : whereStr + " and IssueNum = '" + str12 + "'") : whereStr + " and UserName = '" + str12 + "'";
        }
        if (!string.IsNullOrEmpty(str4))
          whereStr = whereStr + " and LotteryId =" + str4;
        if (!string.IsNullOrEmpty(str5))
          whereStr = whereStr + " and PlayId =" + str5;
        if (!string.IsNullOrEmpty(str3))
          whereStr = whereStr + " and state =" + str3;
        if (!string.IsNullOrEmpty(str8))
          whereStr = whereStr + " and IsCheat=" + str8 + " and State=0";
        if (!string.IsNullOrEmpty(str9))
          whereStr += " and WarnState='异常'";
      }
      string FldName = this.q("order");
      if (!string.IsNullOrEmpty(FldName))
      {
        if (FldName.Equals("bet"))
          FldName = "Times*Total";
      }
      else
        FldName = "Id";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserBet");
      string str13 = str10 + SqlHelp.GetSql0("*", "V_UserBet", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = str13;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetListOfMissing()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("sel");
      string str6 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = "State=0 and " + new UserBetDAL().GetWQWhere();
      if (!string.IsNullOrEmpty(str6))
        whereStr = !str5.Equals("username") ? whereStr + " and ssid like '%" + str6 + "%'" : whereStr + " and dbo.f_GetUserName(UserId) like '%" + str6 + "%'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and PlayId ='" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime2 >='" + str1 + "' and STime2 <'" + str2 + "'";
      string FldName = this.q("order");
      if (!string.IsNullOrEmpty(FldName))
      {
        if (FldName.Equals("bet"))
          FldName = "Times*Total";
      }
      else
        FldName = "stime2";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserBet");
      string sql0 = SqlHelp.GetSql0("Id,ssid,UserId,dbo.f_GetUserName(UserId) as UserName,UserMoney,PlayId,dbo.f_GetPlayName(PlayId) as PlayName,PlayCode,LotteryId,dbo.f_GetLotteryName(LotteryId) as LotteryName,IssueNum,SingleMoney,Times,Num,DX,DS,cast(round(Times*Total,4) as numeric(15,4)) as Total,Point,PointMoney,Bonus,WinNum,WinBonus,RealGet,Pos,STime,STime2,IsOpen,State,IsDelay,IsWin,STime9", "N_UserBet", FldName, num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetZHList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("sel");
      string str6 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = "1=1";
      string str7 = this.q("id");
      if (!string.IsNullOrEmpty(str7))
      {
        whereStr = whereStr + " and ssid ='" + str7 + "'";
      }
      else
      {
        if (!string.IsNullOrEmpty(str6))
          whereStr = !str5.Equals("username") ? whereStr + " and ssid like '%" + str6 + "%'" : whereStr + " and dbo.f_GetUserName(UserId) like '%" + str6 + "%'";
        if (!string.IsNullOrEmpty(str3))
          whereStr = whereStr + " and LotteryId =" + str3;
        if (!string.IsNullOrEmpty(str4))
          whereStr = whereStr + " and PlayId ='" + str4 + "'";
        if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
          whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserBetZh");
      string sql0 = SqlHelp.GetSql0("*", "V_UserBetZh", "STime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetZH()
    {
      string str = this.Str2Str(this.q("id"));
      int PageIndex = this.Int_ThisPage();
      int PageSize = this.Str2Int(this.q("pagesize"), 20);
      int num = 0;
      string whereStr = "Id=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      num = this.doh.Count("V_UserBetZh");
      string sql0 = SqlHelp.GetSql0("*", "V_UserBetZh", "STime", PageSize, PageIndex, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetZHInfo()
    {
      string str = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "zhid =" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserBetZhDetail");
      string sql0 = SqlHelp.GetSql0("*", "V_UserBetZhDetail", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxOper()
    {
      string betId = this.f("ids");
      if (new UserBetZhDAL().BetCancel(betId) == 1)
      {
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "管理员对" + betId + "追号进行终止追号！");
        this._response = this.JsonResult(1, "操作成功！");
      }
      else
        this._response = this.JsonResult(0, "操作失败！");
    }

    private void ajaxBetOpers()
    {
      string str1 = this.f("flag");
      string str2 = this.f("loid");
      string str3 = this.f("Issue");
      if (str1.Trim().Equals("1"))
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "IssueNum='" + str3.Trim() + "'";
        if (this.doh.Count("N_UserBet") > 0)
        {
          new LotteryCheck().Cancel(Convert.ToInt32(str2.Trim()), str3.Trim(), 0);
          new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + str3.Trim() + "期进行撤单");
          this._response = this.JsonResult(1, "操作成功！");
        }
        else
          this._response = this.JsonResult(0, "该期号不存在投注记录！");
      }
      if (str1.Trim().Equals("2"))
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "IssueNum='" + str3.Trim() + "'";
        if (this.doh.Count("N_UserBet") > 0)
        {
          new LotteryCheck().Cancel(Convert.ToInt32(str2.Trim()), str3.Trim(), 2);
          new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + str3.Trim() + "期进行撤单");
          this._response = this.JsonResult(1, "操作成功！");
        }
        else
          this._response = this.JsonResult(0, "该期号不存在投注记录！");
      }
      if (!str1.Trim().Equals("3"))
        return;
      this.doh.Reset();
      this.doh.ConditionExpress = "IssueNum='" + str3.Trim() + "'";
      if (this.doh.Count("N_UserBet") > 0)
      {
        new LotteryCheck().CancelToNoOfTitle(str2.Trim(), str3.Trim());
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "订单管理", "对" + str3.Trim() + "期进行撤回到未开奖");
        this._response = this.JsonResult(1, "操作成功！");
      }
      else
        this._response = this.JsonResult(0, "该期号不存在投注记录！");
    }
  }
}
