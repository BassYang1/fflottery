// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxSave
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.Web;

namespace Lottery.Admin
{
  public partial class ajaxSave : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("master", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "Info":
          this.Info();
          break;
        case "Save":
          this.Save();
          break;
        case "Update":
          this.Update();
          break;
        case "OptionsInfo":
          this.OptionsInfo();
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

    private void Info()
    {
      string str1 = this.q("id");
      string str2 = this.q("t");
      this.doh.Reset();
      this.doh.SqlCmd = "select top 1 * from " + str2 + " where Id=" + str1;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void Save()
    {
      string _tableName = this.q("t");
      string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
      this.doh.Reset();
      int num = 0;
      for (int index = 0; index < allKeys.Length; ++index)
      {
        string s = this.f(allKeys[index]);
        if (!string.IsNullOrEmpty(s))
        {
          if (allKeys[index].ToLower().Equals("password"))
            this.doh.AddFieldItem("Password", (object) MD5.Last64(MD5.Lower32(s)));
          else
            this.doh.AddFieldItem(allKeys[index], (object) s);
          ++num;
        }
      }
      if (num > 0)
      {
        num = this.doh.Insert(_tableName);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "添加操作", "添加了表" + _tableName + "的数据，Id为" + (object) num);
      }
      if (num > 0)
        this._response = this.JsonResult(1, "保存成功");
      else
        this._response = this.JsonResult(0, "保存失败");
    }

    private void Update()
    {
      string str = this.q("id");
      string _tableName = this.q("t");
      string[] allKeys = HttpContext.Current.Request.Form.AllKeys;
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      int num = 0;
      for (int index = 0; index < allKeys.Length; ++index)
      {
        string s = this.f(allKeys[index]);
        if (!string.IsNullOrEmpty(s))
        {
          if (allKeys[index].ToLower().Equals("password"))
            this.doh.AddFieldItem("Password", (object) MD5.Last64(MD5.Lower32(s)));
          else
            this.doh.AddFieldItem(allKeys[index], (object) s);
          ++num;
        }
      }
      if (num > 0)
      {
        num = this.doh.Update(_tableName);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "编辑操作", "修改了表" + _tableName + "的数据，Id为" + str);
      }
      if (num > 0)
        this._response = this.JsonResult(1, "修改成功");
      else
        this._response = this.JsonResult(0, "修改失败");
    }

    private void OptionsInfo()
    {
      string str1 = this.q("t");
      string str2 = this.q("w");
      string str3 = this.q("n");
      if (string.IsNullOrEmpty(str3))
        str3 = "name";
      this.doh.Reset();
      this.doh.SqlCmd = "select id," + str3 + " as name from " + str1;
      if (!string.IsNullOrEmpty(str2))
      {
        DbOperHandler doh = this.doh;
        doh.SqlCmd = doh.SqlCmd + " where " + str2;
      }
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
