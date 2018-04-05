// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxActive
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxActive : AdminCenter
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
        case "ajaxAutoActive":
          this.ajaxAutoActive();
          break;
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxGetActDetail":
          this.ajaxGetActDetail();
          break;
        case "ajaxActStates":
          this.ajaxActStates();
          break;
        case "ajaxGetActiveRecord":
          this.ajaxGetActiveRecord();
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

    private void ajaxGetActDetail()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      string str = this.q("table");
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count(str);
      this.doh.Reset();
      this.doh.SqlCmd = SqlHelp.GetSql0("*", str, "Id", num2, num1, "asc", whereStr);
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxActStates()
    {
      string str = this.f("id");
      string _tableName = this.q("table");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField(_tableName, "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      if (this.doh.Update(_tableName) > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxAutoActive()
    {
      switch (this.q("flag"))
      {
        case "ActGongZi":
          new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActGongZi", "日结工资", "Task_AutoActGongZi");
          break;
        case "ActYongJin2":
          new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActYongJin", "总代佣金", "Task_AutoActYongJin_Group2");
          break;
        case "ActYongJin3":
          new ActiveAutoDAL().AutoActiveOper(this.AdminId, "ActYongJin", "直属佣金", "Task_AutoActYongJin_Group3");
          break;
      }
      this._response = this.JsonResult(1, "活动已补发，请查看活动记录！");
    }

    private void ajaxGetList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "flag=1";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Act_ActiveSet");
      string sql0 = SqlHelp.GetSql0("*", "Act_ActiveSet", "id", num2, num1, "asc", whereStr);
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
      int int32 = Convert.ToInt32(this.doh.GetField("Act_ActiveSet", "IsUse"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUse", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Act_ActiveSet");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "活动管理", "编辑了Id为" + str + "的活动");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetActiveRecord()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("type");
      string str4 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str5 = "";
      string whereStr = "1=1";
      string str6 = this.q("id");
      if (!string.IsNullOrEmpty(str6))
      {
        whereStr = whereStr + " and ssid ='" + str6 + "'";
      }
      else
      {
        if (!string.IsNullOrEmpty(str4))
          whereStr = whereStr + " and dbo.f_GetUserName(UserId) LIKE '%" + str4 + "%'";
        if (!string.IsNullOrEmpty(str3))
          whereStr = whereStr + " and ActiveType ='" + str3 + "'";
        if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
          whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Act_ActiveRecord");
      string str7 = str5 + " select '全部合计' as UserName,'0' as Id,'-' as SsId,'-' as UserId,'-' as ActiveType,'-' as ActiveName\r\n                        ,0 as Bet,isnull(sum(InMoney),0) as InMoney,getdate() as STime,'-' as CheckIp,'-' as CheckMachine,'0' as FromUserId,'-' as Remark \r\n                        from Act_ActiveRecord where " + whereStr + " union all " + " select * from ( " + SqlHelp.GetSql0("dbo.f_GetUserName(UserId) as UserName,*", "Act_ActiveRecord", "Id", num2, num1, "desc", whereStr) + " ) YouleTable order by Id desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str7;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
