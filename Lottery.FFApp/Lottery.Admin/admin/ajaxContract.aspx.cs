// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxContract
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.DAL.Flex;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxContract : AdminCenter
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
        case "ajaxGetDetail":
          this.ajaxGetDetail();
          break;
        case "ajaxGetDetailByUserId":
          this.ajaxGetDetailByUserId();
          break;
        case "ajaxDel":
          this.ajaxDel();
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
      string str1 = this.q("type");
      string str2 = this.q("p");
      string str3 = this.q("u");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str1))
        whereStr = whereStr + " and type = " + str1;
      if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + " and dbo.f_GetUserName(ParentId) = '" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) = '" + str3 + "'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserContract");
      string sql0 = SqlHelp.GetSql0("*,dbo.f_GetUserName(ParentId) as ParentName,dbo.f_GetUserName(UserId) as UserName", "N_UserContract", "id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetDetail()
    {
      string str = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "UcId=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserContractDetail");
      string sql0 = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetDetailByUserId()
    {
      string str = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      DataTable dataTable1 = new DataTable();
      string whereStr;
      if (string.IsNullOrEmpty(str))
      {
        whereStr = "UcId=0";
      }
      else
      {
        this.doh.Reset();
        this.doh.SqlCmd = "select top 1 Id from [N_UserContract] where userid=" + str + " and type=1";
        DataTable dataTable2 = this.doh.GetDataTable();
        whereStr = dataTable2.Rows.Count <= 0 ? "UcId=0" : "UcId=" + dataTable2.Rows[0]["Id"];
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserContractDetail");
      string sql0 = SqlHelp.GetSql0("*", "N_UserContractDetail", "id", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable3 = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable3) + "}";
      dataTable3.Clear();
      dataTable3.Dispose();
    }

    private void ajaxDel()
    {
      string ucid = this.f("id");
      new ContractDAL().Delete(ucid);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "管理员删除了" + ucid + "契约！");
      this._response = this.JsonResult(1, "成功清空");
    }
  }
}
