// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxUserlevel
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxUserlevel : AdminCenter
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
        case "ajaxGetQuotaList":
          this.ajaxGetQuotaList();
          break;
        case "ajaxCreate":
          this.ajaxCreate();
          break;
        case "ajaxCreateAll":
          this.ajaxCreateAll();
          break;
        case "ajaxGetUserGroupQuotaList":
          this.ajaxGetUserGroupQuotaList();
          break;
        case "ajaxGetUserPointQuotaList":
          this.ajaxGetUserPointQuotaList();
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
      this.q("keys");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.doh.Reset();
      this.doh.ConditionExpress = "";
      int totalCount = this.doh.Count("N_UserLevel");
      string sql0 = SqlHelp.GetSql0("Id,CONVERT(DECIMAL(10,2),CONVERT(DECIMAL(10,2),[Point])/10) as Point,Title,Bonus,Score,Times,Sort", "N_UserLevel", "Id", num2, num1, "desc", "");
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetUserGroupQuotaList()
    {
      string str = this.q("group");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str))
        whereStr = whereStr + " and [group] = " + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserGroupQuota");
      this.doh.Reset();
      this.doh.SqlCmd = SqlHelp.GetSql0("[Id],[Group],(select name from N_UserGroup where Id=a.Group) as GroupName,[ToGroup],(select name from N_UserGroup where Id=a.ToGroup) as ToGroupName,[ChildNums],[Sort]", "N_UserGroupQuota a", "Id", num2, num1, "asc", whereStr);
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetUserPointQuotaList()
    {
      string str = this.q("point");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str))
        whereStr = whereStr + " and [point] = " + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_UserPointQuota");
      this.doh.Reset();
      this.doh.SqlCmd = SqlHelp.GetSql0("[Id],[Point],[ChildNums],[Sort]", "N_UserPointQuota", "Id", num2, num1, "asc", whereStr);
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetQuotaList()
    {
      string str1 = this.q("uname");
      string str2 = this.q("ulevel");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "1=1";
      if (!string.IsNullOrEmpty(str1))
        whereStr = whereStr + " and dbo.f_GetUserName(UserId) LIKE '%" + str1 + "%'";
      if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + " and UserLevel = " + str2;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_UserQuota");
      this.doh.Reset();
      this.doh.SqlCmd = SqlHelp.GetSql0("*", "V_UserQuota", "Id", num2, num1, "desc", whereStr);
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxCreate()
    {
      string str1 = this.q("userId");
      string str2 = this.q("num");
      if (!string.IsNullOrEmpty(str1))
      {
        if (Convert.ToInt32(str2) >= 0)
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + str1 + " and IsEnable=0 and IsDel=0";
          DataTable dataTable1 = this.doh.GetDataTable();
          for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
          {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + (object) Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
            DataTable dataTable2 = this.doh.GetDataTable();
            for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
            {
              if (!new UserQuotaDAL().Exists("UserId=" + dataTable1.Rows[index1]["Id"].ToString() + " and UserLevel=" + (object) (Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10))))
                new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Convert.ToInt32(str2));
            }
            new LogAdminOperDAL().SaveLog(this.AdminId, dataTable1.Rows[index1]["Id"].ToString(), "会员管理", "自动生成了Id为" + dataTable1.Rows[index1]["Id"] + "的会员的配额");
          }
        }
        this._response = this.JsonResult(1, "配额生成成功！");
      }
      else
        this._response = this.JsonResult(0, "请输入会员编号再生成配额！");
    }

    private void ajaxCreateAll()
    {
      string str = this.q("num2");
      if (!string.IsNullOrEmpty(str))
      {
        if (Convert.ToInt32(str) >= 0)
        {
          this.doh.Reset();
          this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id not in (select UserId from N_UserQuota with(nolock) group by UserId) and IsEnable=0 and IsDel=0";
          DataTable dataTable1 = this.doh.GetDataTable();
          for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
          {
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + (object) Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
            DataTable dataTable2 = this.doh.GetDataTable();
            for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
            {
              if (!new UserQuotaDAL().Exists("UserId=" + dataTable1.Rows[index1]["Id"].ToString() + " and UserLevel=" + (object) (Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10))))
                new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Convert.ToInt32(str));
            }
            new LogAdminOperDAL().SaveLog(this.AdminId, dataTable1.Rows[index1]["Id"].ToString(), "会员管理", "自动生成了Id为" + dataTable1.Rows[index1]["Id"] + "的会员的配额");
          }
        }
        this._response = this.JsonResult(1, "配额生成成功！");
      }
      else
        this._response = this.JsonResult(0, "请输入配额数量！");
    }
  }
}
