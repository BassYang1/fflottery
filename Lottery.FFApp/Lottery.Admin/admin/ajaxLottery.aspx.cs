// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxLottery
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxLottery : AdminCenter
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
        case "ajaxGetLotteryList":
          this.ajaxGetLotteryList();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxAutoStates":
          this.ajaxAutoStates();
          break;
        case "ajaxGetPlayBigList":
          this.ajaxGetPlayBigList();
          break;
        case "ajaxPlayBigStates":
          this.ajaxPlayBigStates();
          break;
        case "ajaxGetPlaySmallList":
          this.ajaxGetPlaySmallList();
          break;
        case "ajaxPlaySmallStates":
          this.ajaxPlaySmallStates();
          break;
        case "ajaxGetTimeList":
          this.ajaxGetTimeList();
          break;
        case "ajaxGetAutoUrlList":
          this.ajaxGetAutoUrlList();
          break;
        case "ajaxGetLotteryCheckList":
          this.ajaxGetLotteryCheckList();
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

    private void ajaxGetLotteryList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_Lottery");
      string sql0 = SqlHelp.GetSql0("(select count(*) from Sys_PlayBigType where typeId=a.ltype) as childcount,case AutoUrl when 0 then '系统自动' else (select title from Sys_lotteryUrl where Id=a.AutoUrl) end AutoName,*", "Sys_Lottery a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Lottery", "IsOpen"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsOpen", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_Lottery");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + str + "的彩种启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxAutoStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Lottery", "IsAuto"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsAuto", (object) (int32 == 0 ? 1 : 0));
      if (this.doh.Update("Sys_Lottery") > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetPlayBigList()
    {
      string str = this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (!string.IsNullOrEmpty(str) && str != "0")
        whereStr = whereStr + " TypeId=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_PlayBigType");
      string sql0 = SqlHelp.GetSql0("(select count(*) from Sys_PlaySmallType where Radio=a.Id) as childcount,*", "Sys_PlayBigType a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxPlayBigStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_PlayBigType", "IsOpen"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsOpen", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_PlayBigType");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + str + "的玩法类别启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetPlaySmallList()
    {
      string str1 = this.q("type");
      string str2 = this.q("play");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str1) && str1 != "0")
        whereStr = whereStr + " and LotteryId=" + str1;
      if (!string.IsNullOrEmpty(str2) && str2 != "0" && str2 != "null")
        whereStr = whereStr + " and Radio=" + str2;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_PlaySmallType");
      string sql0 = SqlHelp.GetSql0("*", "V_PlaySmallType", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxPlaySmallStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_PlaySmallType", "IsOpen"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsOpen", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_PlaySmallType");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "编辑了Id为" + str + "的玩法启用状态");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetTimeList()
    {
      string str = this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (!string.IsNullOrEmpty(str) && str != "0")
        whereStr = whereStr + " lotteryId=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_LotteryTime");
      string sql0 = SqlHelp.GetSql0("[Id],[LotteryId],dbo.f_GetLotteryName(LotteryId) as LotteryName,[Sn],[Time],[Sort],STime", "Sys_LotteryTime", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetAutoUrlList()
    {
      string str = this.q("lid");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "IsUsed = 0";
      if (!string.IsNullOrEmpty(str))
        whereStr = whereStr + " and Lid=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_lotteryUrl");
      string sql0 = SqlHelp.GetSql0("*", "Sys_lotteryUrl a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetLotteryCheckList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_LotteryCheck");
      string sql0 = SqlHelp.GetSql0("(select Title from Sys_Lottery where Id=a.Id) as Name,*", "Sys_LotteryCheck a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
