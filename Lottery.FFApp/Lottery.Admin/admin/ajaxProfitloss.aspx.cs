// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxProfitloss
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
  public partial class ajaxProfitloss : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetProList":
          this.ajaxGetProList();
          break;
        case "ajaxGetProListTeam":
          this.ajaxGetProListTeam();
          break;
        case "ajaxGetProReturnListTeam":
          this.ajaxGetProReturnListTeam();
          break;
        case "ajaxUserDetail":
          this.ajaxUserDetail();
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

    private void ajaxGetProList()
    {
      string str1 = this.q("u");
      string str2 = this.q("Id");
      string d1 = this.q("d1");
      string d2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string whereStr = "";
      if (str1.Trim().Length > 0)
        whereStr = whereStr + " UserName LIKE '%" + str1 + "%'";
      else if (!string.IsNullOrEmpty(str2))
        whereStr = whereStr + " ParentId=" + str2;
      if (d1.Trim().Length == 0)
        d1 = this.StartTime;
      if (d2.Trim().Length == 0)
        d2 = this.EndTime;
      if (Convert.ToDateTime(d1) > Convert.ToDateTime(d2))
        d1 = d2;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("[Id]", "N_User a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      string str3 = "";
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        str3 = str3 + dataTable.Rows[index]["Id"].ToString() + ",";
      DataTable dt = new DataTable();
      if (str3.Length > 1)
      {
        string userId = str3.Substring(0, str3.Length - 1);
        dt = this.GetUserMoneyStat(d1, d2, userId);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dt) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private DataTable GetUserMoneyStat(string d1, string d2, string userId)
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
      Decimal num9 = new Decimal(0);
      Decimal num10 = new Decimal(0);
      Decimal num11 = new Decimal(0);
      Decimal num12 = new Decimal(0);
      Decimal num13 = new Decimal(0);
      string[] strArray = userId.Split(',');
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        Decimal num14 = new Decimal(0);
        Decimal num15 = new Decimal(0);
        Decimal num16 = new Decimal(0);
        Decimal num17 = new Decimal(0);
        Decimal num18 = new Decimal(0);
        Decimal num19 = new Decimal(0);
        Decimal num20 = new Decimal(0);
        Decimal num21 = new Decimal(0);
        Decimal num22 = new Decimal(0);
        Decimal num23 = new Decimal(0);
        Decimal num24 = new Decimal(0);
        Decimal num25 = new Decimal(0);
        Decimal num26 = new Decimal(0);
        string userMoneyStatSql = this.GetUserMoneyStatSql(d1, d2, strArray[index1]);
        this.doh.Reset();
        this.doh.SqlCmd = userMoneyStatSql;
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          num14 = Convert.ToDecimal(dataTable2.Rows[index2]["money"].ToString());
          num15 = Convert.ToDecimal(dataTable2.Rows[index2]["Charge"].ToString());
          num16 = Convert.ToDecimal(dataTable2.Rows[index2]["GetCash"].ToString());
          num17 = Convert.ToDecimal(dataTable2.Rows[index2]["Bet"].ToString());
          num18 = Convert.ToDecimal(dataTable2.Rows[index2]["Point"].ToString());
          num19 = Convert.ToDecimal(dataTable2.Rows[index2]["Win"].ToString());
          num20 = Convert.ToDecimal(dataTable2.Rows[index2]["Cancellation"].ToString());
          num21 = Convert.ToDecimal(dataTable2.Rows[index2]["TranAccIn"].ToString());
          num22 = Convert.ToDecimal(dataTable2.Rows[index2]["TranAccOut"].ToString());
          num23 = Convert.ToDecimal(dataTable2.Rows[index2]["Give"].ToString());
          num24 = Convert.ToDecimal(dataTable2.Rows[index2]["AgentFH"].ToString());
          num25 = Convert.ToDecimal(dataTable2.Rows[index2]["Other"].ToString());
          num26 = Convert.ToDecimal(dataTable2.Rows[index2]["Change"].ToString());
        }
        DataRow row = dataTable1.NewRow();
        row["Id"] = (object) strArray[index1];
        row["userId"] = (object)dataTable2.Rows[0]["Id"].ToString();
        row["userName"] = (object) dataTable2.Rows[0]["userName"].ToString();
        row["chindcount"] = (object) dataTable2.Rows[0]["chindcount"].ToString();
        row["money"] = (object) num14.ToString();
        row["Charge"] = (object) num15.ToString();
        row["GetCash"] = (object) num16.ToString();
        row["Bet"] = (object) (num17 - num20).ToString();
        row["Point"] = (object) num18.ToString();
        row["Win"] = (object) num19.ToString();
        row["Cancellation"] = (object) num20.ToString();
        row["TranAccIn"] = (object) num21.ToString();
        row["TranAccOut"] = (object) num22.ToString();
        row["Give"] = (object) num23.ToString();
        row["AgentFH"] = (object) num24.ToString();
        row["Other"] = (object) num25.ToString();
        row["Change"] = (object) num26.ToString();
        row["Total"] = (object) string.Concat((object) (num19 + num23 + num26 + num20 + num18 - num17));
        row["MoneyTotal"] = (object) string.Concat((object) (num15 - num16));
        dataTable1.Rows.Add(row);
      }
      return dataTable1;
    }

    private string GetUserMoneyStatSql(string d1, string d2, string userId)
    {
      return "SELECT " + userId + " as Id,(select userName from N_User with(nolock) where Id=" + userId + ") as userName,(select count(*) from N_User with(nolock) where parentId=" + userId + ") as chindcount,(select money from N_User with(nolock) where Id=" + userId + ") as money,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b where STime>='" + d1 + "' and STime <'" + d2 + "' and (UserId=" + userId + ")";
    }

    private void ajaxGetProListTeam()
    {
      string str1 = this.q("u");
      string str2 = this.q("Id");
      string d1 = this.q("d1");
      string d2 = this.q("d2");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str3 = "isDel=0";
      string whereStr;
      if (str1.Trim().Length > 0)
        whereStr = str3 + " and UserName LIKE '%" + str1 + "%'";
      else if (string.IsNullOrEmpty(str2))
      {
        whereStr = str3 + " and ParentId=0";
        this.Session["return"] = (object) 0;
      }
      else
      {
        whereStr = str3 + " and ParentId=" + str2;
        this.Session["return"] = (object) str2;
      }
      if (d1.Trim().Length == 0)
        d1 = this.StartTime;
      if (d2.Trim().Length == 0)
        d2 = this.EndTime;
      if (Convert.ToDateTime(d1) > Convert.ToDateTime(d2))
        d1 = d2;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("[Id]", "N_User a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      string str4 = "";
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        str4 = str4 + dataTable.Rows[index]["Id"].ToString() + ",";
      DataTable dt = new DataTable();
      if (str4.Length > 1)
      {
        string userId = str4.Substring(0, str4.Length - 1);
        dt = this.GetUserMoneyStatTeam(d1, d2, userId);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dt) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetProReturnListTeam()
    {
      string str1 = this.q("u");
      this.q("Id");
      string d1 = this.q("d1");
      string d2 = this.q("d2");
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
      if (str1.Trim().Length > 0)
        whereStr = whereStr + " and UserName LIKE '%" + str1 + "%'";
      if (d1.Trim().Length == 0)
        d1 = this.StartTime;
      if (d2.Trim().Length == 0)
        d2 = this.EndTime;
      if (Convert.ToDateTime(d1) > Convert.ToDateTime(d2))
        d1 = d2;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("[Id]", "N_User a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      string str2 = "";
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        str2 = str2 + dataTable.Rows[index]["Id"].ToString() + ",";
      DataTable dt = new DataTable();
      if (str2.Length > 1)
      {
        string userId = str2.Substring(0, str2.Length - 1);
        dt = this.GetUserMoneyStatTeam(d1, d2, userId);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxGetProReturnListTeam(<#page#>);") + "\"," + dtHelp.DT2JSON(dt) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private DataTable GetUserMoneyStatTeam(string d1, string d2, string userId)
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
      Decimal num9 = new Decimal(0);
      Decimal num10 = new Decimal(0);
      Decimal num11 = new Decimal(0);
      Decimal num12 = new Decimal(0);
      Decimal num13 = new Decimal(0);
      string[] strArray = userId.Split(',');
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        Decimal num14 = new Decimal(0);
        Decimal num15 = new Decimal(0);
        Decimal num16 = new Decimal(0);
        Decimal num17 = new Decimal(0);
        Decimal num18 = new Decimal(0);
        Decimal num19 = new Decimal(0);
        Decimal num20 = new Decimal(0);
        Decimal num21 = new Decimal(0);
        Decimal num22 = new Decimal(0);
        Decimal num23 = new Decimal(0);
        Decimal num24 = new Decimal(0);
        Decimal num25 = new Decimal(0);
        Decimal num26 = new Decimal(0);
        string moneyStatTeamSql = this.GetUserMoneyStatTeamSql(d1, d2, strArray[index1]);
        this.doh.Reset();
        this.doh.SqlCmd = moneyStatTeamSql;
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          num14 = Convert.ToDecimal(dataTable2.Rows[index2]["money"].ToString());
          num15 += Convert.ToDecimal(dataTable2.Rows[index2]["Charge"].ToString());
          num16 += Convert.ToDecimal(dataTable2.Rows[index2]["GetCash"].ToString());
          num17 += Convert.ToDecimal(dataTable2.Rows[index2]["Bet"].ToString());
          num18 += Convert.ToDecimal(dataTable2.Rows[index2]["Point"].ToString());
          num19 += Convert.ToDecimal(dataTable2.Rows[index2]["Win"].ToString());
          num20 += Convert.ToDecimal(dataTable2.Rows[index2]["Cancellation"].ToString());
          num21 += Convert.ToDecimal(dataTable2.Rows[index2]["TranAccIn"].ToString());
          num22 += Convert.ToDecimal(dataTable2.Rows[index2]["TranAccOut"].ToString());
          num23 += Convert.ToDecimal(dataTable2.Rows[index2]["Give"].ToString());
          num24 += Convert.ToDecimal(dataTable2.Rows[index2]["AgentFH"].ToString());
          num25 += Convert.ToDecimal(dataTable2.Rows[index2]["Other"].ToString());
          num26 += Convert.ToDecimal(dataTable2.Rows[index2]["Change"].ToString());
        }
        DataRow row = dataTable1.NewRow();
        row["Id"] = (object) strArray[index1];
        row["userId"] = (object)dataTable2.Rows[0]["Id"].ToString();
        row["userName"] = (object) dataTable2.Rows[0]["userName"].ToString();
        row["chindcount"] = (object) dataTable2.Rows[0]["chindcount"].ToString();
        row["money"] = (object) num14.ToString();
        row["Charge"] = (object) num15.ToString();
        row["GetCash"] = (object) num16.ToString();
        row["Bet"] = (object) (num17 - num20).ToString();
        row["Point"] = (object) num18.ToString();
        row["Win"] = (object) num19.ToString();
        row["Cancellation"] = (object) num20.ToString();
        row["TranAccIn"] = (object) num21.ToString();
        row["TranAccOut"] = (object) num22.ToString();
        row["Give"] = (object) num23.ToString();
        row["AgentFH"] = (object) num24.ToString();
        row["Other"] = (object) num25.ToString();
        row["Change"] = (object) num26.ToString();
        row["Total"] = (object) string.Concat((object) (num19 + num23 + num26 + num20 + num18 - num17));
        row["MoneyTotal"] = (object) string.Concat((object) (num15 - num16));
        dataTable1.Rows.Add(row);
      }
      return dataTable1;
    }

    private string GetUserMoneyStatTeamSql(string d1, string d2, string userId)
    {
      string str = " and dbo.f_GetUserCode(UserId) like '%" + Strings.PadLeft(userId) + "%'";
      return "SELECT " + userId + " as Id,(select userName from N_User with(nolock) where Id=" + userId + ") as userName,(select count(*) from N_User with(nolock) where parentId=" + userId + ") as chindcount,(select money from N_User with(nolock) where Id=" + userId + ") as money,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.Bet),0) Bet,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.TranAccIn),0) TranAccIn,isnull(sum(b.TranAccOut),0) TranAccOut,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.Other),0) Other,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b where STime>='" + d1 + "' and STime<'" + d2 + "' " + str;
    }

    private void ajaxUserDetail()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      this.q("group");
      string str3 = this.q("u");
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string str4 = string.Format("SELECT a.[Id]\r\n                                            ,[UserName]\r\n                                            ,[Money]\r\n                                            ,a.[Point] as userpoint\r\n                                            ,isnull(sum(Charge),0) as Charge\r\n                                            ,isnull(sum(getcash),0)  as getcash\r\n                                            ,isnull(sum(Bet),0)-isnull(sum(Cancellation),0)  as bet\r\n                                            ,isnull(sum(Win),0)  as Win\r\n                                            ,isnull(sum(b.Point),0)  as Point\r\n                                            ,isnull(sum(Give),0)  as Give\r\n                                            ,isnull(sum(agentFH),0) as agentFH\r\n                                            ,isnull(sum(other),0)  as other\r\n                                            ,(isnull(sum(Win),0)+isnull(sum(b.Point),0)+isnull(sum(Change),0)+isnull(sum(Give),0)+isnull(sum(Cancellation),0))-isnull(sum(Bet),0)  as total\r\n                                            ,isnull(sum(Charge),0)-isnull(sum(getcash),0)  as moneytotal\r\n                                            ,(SELECT count(*) FROM [N_UserMoneyStatAll] with(nolock) where dbo.f_GetUserCode(UserId) like '%,'+Convert(varchar(10),a.id)+',%' and Convert(varchar(10),STime,120)=Convert(varchar(10),getdate(),120) and Bet-Cancellation>0) as YxNum\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and IsOnline=1) as OnLineNum\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=129) as point129\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=128) as point128\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=127) as point127\r\n                                            ,(select count(*) from N_User where dbo.f_GetUserCode(id) like '%,'+Convert(varchar(10),a.id)+',%' and Point=126) as point126\r\n                                            FROM [V_User] a left join [N_UserMoneyStatAll] b on dbo.f_GetUserCode(b.UserId) like '%,'+Convert(varchar(10),a.id)+',%'\r\n                                            where {0}", (object) ("STime>='" + str1 + "' and STime <'" + str2 + "'"));
      string str5 = (str3.Trim().Length <= 0 ? str4 + " and UserName = ''" : str4 + " and UserName like '%" + str3 + "%'") + " group by a.[Id],[UserName],[Money],a.[Point]";
      this.doh.Reset();
      this.doh.SqlCmd = str5;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private DataTable CreatDataTable()
    {
      return new DataTable()
      {
        Columns = {
          "Id",
          "userId",
          "userName",
          "chindcount",
          "money",
          "Charge",
          "GetCash",
          "Bet",
          "Point",
          "Win",
          "Cancellation",
          "TranAccIn",
          "TranAccOut",
          "Give",
          "Other",
          "AgentFH",
          "Change",
          "Total",
          "MoneyTotal"
        }
      };
    }
  }
}
