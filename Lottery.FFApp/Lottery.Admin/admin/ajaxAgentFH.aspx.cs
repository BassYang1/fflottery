// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxAgentFH
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxAgentFH : AdminCenter
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
        case "ajaxGetDetailList":
          this.ajaxGetDetailList();
          break;
        case "ajaxDetailStates":
          this.ajaxDetailStates();
          break;
        case "ajaxGetAgentFHList":
          this.ajaxGetProListSub();
          break;
        case "ajaxGetAgentFHRecord":
          this.ajaxGetAgentFHRecord();
          break;
        case "ajaxGetAgent1List":
          this.ajaxGetAgent1List();
          break;
        case "ajaxAgent1States":
          this.ajaxAgent1States();
          break;
        case "ajaxGetAgent2List":
          this.ajaxGetAgent2List();
          break;
        case "ajaxAgent2States":
          this.ajaxAgent2States();
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

    private void ajaxGetDetailList()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Act_AgentFHDetail");
      string sql0 = SqlHelp.GetSql0("*", "Act_AgentFHDetail", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxDetailStates()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Act_AgentFHDetail", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      if (this.doh.Update("Act_AgentFHDetail") > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetProListSub()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "select * from N_User with(nolock) where IsDel=0 and AgentId<>0 order by Id asc";
      DataTable dataTable = this.doh.GetDataTable();
      string str = "";
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        str = str + "," + dataTable.Rows[index]["Id"].ToString();
      DataTable dt = new DataTable();
      if (str.Length > 1)
        dt = this.GetUserMoneyStatSub(str.Substring(1, str.Length - 1));
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dt) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private DataTable GetUserMoneyStatSub(string userId)
    {
      DataTable dataTable1 = this.CreatDataTable();
      Decimal num1 = new Decimal(0);
      Decimal num2 = new Decimal(0);
      Decimal num3 = new Decimal(0);
      Decimal num4 = new Decimal(0);
      Decimal num5 = new Decimal(0);
      Decimal num6 = new Decimal(0);
      Decimal num7 = new Decimal(0);
      Decimal num8 = new Decimal(0);
      string[] strArray = userId.Split(',');
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        Decimal num9 = new Decimal(0);
        Decimal num10 = new Decimal(0);
        Decimal num11 = new Decimal(0);
        Decimal num12 = new Decimal(0);
        Decimal num13 = new Decimal(0);
        Decimal num14 = new Decimal(0);
        Decimal num15 = new Decimal(0);
        Decimal num16 = new Decimal(0);
        string userMoneyStatSubSql = this.GetUserMoneyStatSubSql(strArray[index1]);
        this.doh.Reset();
        this.doh.SqlCmd = userMoneyStatSubSql;
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          num9 += Convert.ToDecimal(dataTable2.Rows[index2]["Bet"].ToString());
          num10 += Convert.ToDecimal(dataTable2.Rows[index2]["Point"].ToString());
          num11 += Convert.ToDecimal(dataTable2.Rows[index2]["Win"].ToString());
          num12 += Convert.ToDecimal(dataTable2.Rows[index2]["Cancellation"].ToString());
          num13 += Convert.ToDecimal(dataTable2.Rows[index2]["Give"].ToString());
          num14 += Convert.ToDecimal(dataTable2.Rows[index2]["Other"].ToString());
          num15 += Convert.ToDecimal(dataTable2.Rows[index2]["AdminAddDed"].ToString());
          num16 += Convert.ToDecimal(dataTable2.Rows[index2]["Change"].ToString());
        }
        DataRow row = dataTable1.NewRow();
        row["Id"] = (object) strArray[index1];
        row["userName"] = (object) dataTable2.Rows[0]["userName"].ToString();
        row["agentId"] = (object) dataTable2.Rows[0]["agentId"].ToString();
        row["Bet"] = (object) (num9 - num12).ToString();
        row["Total"] = (object) string.Concat((object) (num11 + num13 + num16 + num14 + num12 + num10 - num9 - num15));
        row["AgentFHMoney"] = (object) (string.IsNullOrEmpty(string.Concat(dataTable2.Rows[0]["AgentFHMoney"])) ? new Decimal(0) : Convert.ToDecimal(dataTable2.Rows[0]["AgentFHMoney"]));
        row["STime"] = (object) dataTable2.Rows[0]["STime"].ToString();
        row["Point"] = (object) num10.ToString();
        row["Win"] = (object) num11.ToString();
        row["Cancellation"] = (object) num12.ToString();
        row["Give"] = (object) num13.ToString();
        row["Other"] = (object) num14.ToString();
        row["AdminAddDed"] = (object) num15.ToString();
        row["Change"] = (object) num16.ToString();
        dataTable1.Rows.Add(row);
      }
      return dataTable1;
    }

    private string GetUserMoneyStatSubSql(string userId)
    {
      return "SELECT " + userId + " as Id,(select userName from N_User with(nolock) where Id=" + userId + ") as userName,(select agentId from N_User with(nolock) where Id=" + userId + ") as agentId,(select top 1 Inmoney from Act_AgentFHRecord where UserId=" + userId + " order by Id desc) as AgentFHMoney,(SELECT [STime] FROM [Act_AgentFHSet]) as STime,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where (STime between (SELECT max([STime]) FROM [Act_AgentFHSet]) and getdate()) and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%" + Strings.PadLeft(userId) + "%') or UserId=" + userId + ")";
    }

    private DataTable CreatDataTable()
    {
      return new DataTable()
      {
        Columns = {
          {
            "Id",
            typeof (int)
          },
          {
            "userName",
            typeof (string)
          },
          {
            "money",
            typeof (Decimal)
          },
          {
            "agentId",
            typeof (int)
          },
          {
            "AgentFHMoney",
            typeof (Decimal)
          },
          {
            "STime",
            typeof (string)
          },
          {
            "Bet",
            typeof (Decimal)
          },
          {
            "Point",
            typeof (Decimal)
          },
          {
            "Win",
            typeof (Decimal)
          },
          {
            "Cancellation",
            typeof (Decimal)
          },
          {
            "Give",
            typeof (Decimal)
          },
          {
            "Other",
            typeof (Decimal)
          },
          {
            "Change",
            typeof (Decimal)
          },
          {
            "Total",
            typeof (Decimal)
          }
        }
      };
    }

    private void ajaxGetAgentFHRecord()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("code");
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
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + " and UserName LIKE '%" + str4 + "%'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        whereStr = whereStr + " and STime >='" + str1 + "' and STime <'" + str2 + "'";
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + " and AgentId=" + str3;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_AgentFHRecord");
      string str6 = str5 + " SELECT '0' as [Id],'0' as [UserId],'全部合计' as [username],'0' as [AgentId],'-' as [GroupName],isnull(sum(contractcount),0) as contractcount,max([StartTime]) as [StartTime],max([EndTime]) as [EndTime],isnull(sum([Bet]),0) as [Bet],isnull(sum([Total]),0) as [Total],0 as [Per],isnull(sum([InMoney]),0) as [InMoney],getdate() as [STime],'-' as [Remark]\r\n                    FROM [V_AgentFHRecord] where " + whereStr + " union all " + " select * from ( " + SqlHelp.GetSql0("*", "V_AgentFHRecord", "id", num2, num1, "desc", whereStr) + " ) YouleTable order by Id desc ";
      this.doh.Reset();
      this.doh.SqlCmd = str6;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetAgent1List()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Act_Agent1");
      string sql0 = SqlHelp.GetSql0("*", "Act_Agent1", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxAgent1States()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Act_Agent1", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      if (this.doh.Update("Act_Agent1") > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }

    private void ajaxGetAgent2List()
    {
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Act_Agent2");
      string sql0 = SqlHelp.GetSql0("*", "Act_Agent2", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxAgent2States()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int int32 = Convert.ToInt32(this.doh.GetField("Act_Agent2", "IsUsed"));
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + str;
      this.doh.AddFieldItem("IsUsed", (object) (int32 == 0 ? 1 : 0));
      if (this.doh.Update("Act_Agent2") > 0)
        this._response = this.JsonResult(1, "设置成功");
      else
        this._response = this.JsonResult(0, "设置失败");
    }
  }
}
