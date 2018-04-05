// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxSysInfo
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;

namespace Lottery.Admin
{
  public partial class ajaxSysInfo : AdminCenter
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
        case "ajaxUpdate":
          this.ajaxUpdate();
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
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from Sys_Info";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxUpdate()
    {
      string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=1";
      for (int index = 0; index < allKeys.Length; ++index)
      {
        string str = this.f(allKeys[index]);
        if (!string.IsNullOrEmpty(str))
          this.doh.AddFieldItem(allKeys[index], (object) str);
      }
      int num = this.doh.Update("Sys_Info");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "修改了常规设置");
      if (num > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }
  }
}
