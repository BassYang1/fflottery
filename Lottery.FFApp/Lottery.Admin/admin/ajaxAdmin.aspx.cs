// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxAdmin
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxAdmin : AdminCenter
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
        case "ajaxGetList2":
          this.ajaxGetList2();
          break;
        case "ajaxStates":
          this.ajaxStates();
          break;
        case "ajaxDel":
          this.ajaxDel();
          break;
        case "ajaxUnLock":
          this.ajaxUnLock();
          break;
        case "ajaxGetRoleList":
          this.ajaxGetRoleList();
          break;
        case "ajaxRoleStates":
          this.ajaxRoleStates();
          break;
        case "ajaxRoleDel":
          this.ajaxRoleDel();
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
      this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "GroupId=1";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_Admin");
      string sql0 = SqlHelp.GetSql0("*,(select Name from Sys_Role where Id=a.RoleId) as RoleName", "Sys_Admin a", "id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetList2()
    {
      this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "GroupId=2";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_Admin");
      string sql0 = SqlHelp.GetSql0("*,(select Name from Sys_Role where Id=a.RoleId) as RoleName", "Sys_Admin a", "id", num2, num1, "asc", whereStr);
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
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Admin", "flag"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("flag", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_Admin");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + str + "的管理员");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxDel()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      int num = this.doh.Delete("Sys_Admin");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "删除Id为" + str + "的管理员！");
      if (num > 0)
        this._response = this.JsonResult(1, "操作成功");
      else
        this._response = this.JsonResult(0, "操作失败");
    }

    private void ajaxUnLock()
    {
      string s = this.f("p");
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id";
      this.doh.AddConditionParameter("@Id", (object) this.AdminId);
      object field = this.doh.GetField("Sys_Admin", "Password");
      if (field != null)
      {
        if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(s)))
          this._response = this.JsonResult(1, "解锁成功");
        else
          this._response = this.JsonResult(0, "解锁失败");
      }
      else
        this._response = this.JsonResult(0, "解锁失败");
    }

    private void ajaxGetRoleList()
    {
      this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_Role");
      string sql0 = SqlHelp.GetSql0("*", "Sys_Role", "id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxRoleStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Sys_Role", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      int num = this.doh.Update("Sys_Role");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "编辑了Id为" + str + "的管理员角色");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxRoleDel()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      int num = this.doh.Delete("Sys_Role");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "管理员管理", "删除Id为" + str + "的管理员角色！");
      if (num > 0)
        this._response = this.JsonResult(1, "操作成功");
      else
        this._response = this.JsonResult(0, "操作失败");
    }
  }
}
