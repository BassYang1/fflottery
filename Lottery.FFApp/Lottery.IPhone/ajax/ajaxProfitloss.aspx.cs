// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajaxProfitloss
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.IPhone
{
  public partial class ajaxProfitloss : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetPointList":
          this.ajaxGetPointList();
          break;
        case "ajaxGetProList":
          this.ajaxGetProList();
          break;
        case "ajaxGetProListSub":
          this.ajaxGetProListSub();
          break;
        case "ajaxGetProListId":
          this.ajaxGetProListId();
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

    private void ajaxGetProListId()
    {
      string d2 = this.q("d2");
      this.q("keys");
      string d1 = this.q("d1");
      string str1 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str2 = "IsDel=0";
      string whereStr = string.IsNullOrEmpty(str1) ? str2 + " and ParentId =-1" : str2 + " and ParentId =" + str1;
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
      DataTable dataTable1 = this.doh.GetDataTable();
      string str3 = "";
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        str3 = str3 + "," + dataTable1.Rows[index]["Id"].ToString();
      DataTable dataTable2 = new DataTable();
      if (str3.Length > 1)
      {
        string userId1 = str3.Substring(1, str3.Length - 1);
        DataTable userMoneyStat = this.GetUserMoneyStat(d1, d2, userId1);
        this.doh.Reset();
        this.doh.SqlCmd = "select Id from N_User with(nolock) where " + whereStr;
        DataTable dataTable3 = this.doh.GetDataTable();
        string str4 = "";
        for (int index = 0; index < dataTable3.Rows.Count; ++index)
          str4 = str4 + "," + dataTable3.Rows[index]["Id"].ToString();
        DataTable dtsum = new DataTable();
        if (str4.Length > 1)
        {
          string userId2 = str4.Substring(1, str4.Length - 1);
          dtsum = this.GetUserMoneyStat(d1, d2, userId2);
        }
        this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON123(dtsum, userMoneyStat, 0, "recordcount", "table", true) + "}";
        dataTable1.Clear();
        dataTable1.Dispose();
      }
      else
        this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\"}";
    }

    private void ajaxGetProList()
    {
      string str1 = this.q("type");
      if (string.IsNullOrEmpty(str1) || str1 == "0")
      {
        string str2 = this.q("stime");
        string str3 = this.q("keys");
        string d1 = this.q("d1");
        string d2 = this.q("d2");
        string str4 = this.q("id");
        int num1 = this.Int_ThisPage();
        int num2 = this.Str2Int(this.q("pagesize"), 20);
        this.Str2Int(this.q("flag"), 0);
        string str5 = "IsDel=0";
        string whereStr;
        if (!string.IsNullOrEmpty(str4))
          whereStr = str5 + " and ParentId =" + str4;
        else if (str3.Trim().Length > 0)
          whereStr = str5 + " and UserCode like '%" + Strings.PadLeft(this.AdminId) + "%' and UserName LIKE '%" + str3 + "%'";
        else
          whereStr = str5 + " and ParentId =" + this.AdminId;
        if (!string.IsNullOrEmpty(str2))
        {
          switch (str2)
          {
            case "1":
              d1 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
              d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
              break;
            case "2":
              d1 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
              d2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
              break;
            case "3":
              d1 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
              d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
              break;
            case "4":
              d1 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
              DateTime dateTime1 = DateTime.Now;
              dateTime1 = dateTime1.AddDays(0.0);
              d2 = dateTime1.ToString("yyyy-MM-dd") + " 23:59:59";
              break;
            case "5":
              d1 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
              d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
              break;
            case "6":
              d1 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
              DateTime dateTime2 = DateTime.Now;
              dateTime2 = dateTime2.AddDays(0.0);
              d2 = dateTime2.ToString("yyyy-MM-dd") + " 23:59:59";
              break;
          }
        }
        else
        {
          if (d1.Trim().Length == 0)
            d1 = this.StartTime;
          if (d2.Trim().Length == 0)
            d2 = this.EndTime;
          if (Convert.ToDateTime(d1) > Convert.ToDateTime(d2))
            d1 = d2;
        }
        this.doh.Reset();
        this.doh.ConditionExpress = whereStr;
        int totalCount = this.doh.Count("N_User");
        string sql0 = SqlHelp.GetSql0("[Id]", "N_User a", "Id", num2, num1, "asc", whereStr);
        this.doh.Reset();
        this.doh.SqlCmd = sql0;
        DataTable dataTable1 = this.doh.GetDataTable();
        string str6 = "";
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
          str6 = str6 + "," + dataTable1.Rows[index]["Id"].ToString();
        DataTable dt = this.GetUserMoneyStat(d1, d2, this.AdminId);
        if (str6.Length > 1)
        {
          string userId = str6.Substring(1, str6.Length - 1);
          dt = num1 != 1 ? this.GetUserMoneyStat(d1, d2, userId) : this.GetUserMoneyStat(d1, d2, this.AdminId + "," + userId);
        }
        this.doh.Reset();
        this.doh.SqlCmd = "select Id from N_User with(nolock) where " + whereStr;
        DataTable dataTable2 = this.doh.GetDataTable();
        string str7 = "";
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
          str7 = str7 + "," + dataTable2.Rows[index]["Id"].ToString();
        DataTable userMoneyStat = this.GetUserMoneyStat(d1, d2, this.AdminId);
        if (str7.Length > 1)
        {
          string str8 = str7.Substring(1, str7.Length - 1);
          userMoneyStat = this.GetUserMoneyStat(d1, d2, this.AdminId + "," + str8);
        }
        this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON123(userMoneyStat, dt, 0, "recordcount", "table", true) + "}";
        dataTable1.Clear();
        dataTable1.Dispose();
      }
      else
        this.ajaxGetProListSub();
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
      Decimal num14 = new Decimal(0);
      Decimal num15 = new Decimal(0);
      Decimal num16 = new Decimal(0);
      Decimal num17 = new Decimal(0);
      Decimal num18 = new Decimal(0);
      string[] strArray = userId.Split(',');
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        Decimal num19 = new Decimal(0);
        Decimal num20 = new Decimal(0);
        Decimal num21 = new Decimal(0);
        Decimal num22 = new Decimal(0);
        Decimal num23 = new Decimal(0);
        Decimal num24 = new Decimal(0);
        Decimal num25 = new Decimal(0);
        Decimal num26 = new Decimal(0);
        Decimal num27 = new Decimal(0);
        Decimal num28 = new Decimal(0);
        Decimal num29 = new Decimal(0);
        Decimal num30 = new Decimal(0);
        Decimal num31 = new Decimal(0);
        Decimal num32 = new Decimal(0);
        Decimal num33 = new Decimal(0);
        Decimal num34 = new Decimal(0);
        Decimal num35 = new Decimal(0);
        Decimal num36 = new Decimal(0);
        string userMoneyStatSql = this.GetUserMoneyStatSql(d1, d2, strArray[index1]);
        this.doh.Reset();
        this.doh.SqlCmd = userMoneyStatSql;
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          num19 = Convert.ToDecimal(dataTable2.Rows[index2]["money"].ToString());
          num20 = Convert.ToDecimal(dataTable2.Rows[index2]["childnum"].ToString());
          num21 = Convert.ToDecimal(dataTable2.Rows[index2]["Charge"].ToString());
          num22 = Convert.ToDecimal(dataTable2.Rows[index2]["GetCash"].ToString());
          num23 = Convert.ToDecimal(dataTable2.Rows[index2]["GetCashErr"].ToString());
          num24 = Convert.ToDecimal(dataTable2.Rows[index2]["Bet"].ToString());
          num25 = Convert.ToDecimal(dataTable2.Rows[index2]["Betno"].ToString());
          num26 = Convert.ToDecimal(dataTable2.Rows[index2]["BetChase"].ToString());
          num27 = Convert.ToDecimal(dataTable2.Rows[index2]["WinChase"].ToString());
          num28 = Convert.ToDecimal(dataTable2.Rows[index2]["Point"].ToString());
          num29 = Convert.ToDecimal(dataTable2.Rows[index2]["Win"].ToString());
          num30 = Convert.ToDecimal(dataTable2.Rows[index2]["Cancellation"].ToString());
          num31 = Convert.ToDecimal(dataTable2.Rows[index2]["ChargeDeduct"].ToString());
          num32 = Convert.ToDecimal(dataTable2.Rows[index2]["ChargeUp"].ToString());
          num33 = Convert.ToDecimal(dataTable2.Rows[index2]["Give"].ToString());
          num34 = Convert.ToDecimal(dataTable2.Rows[index2]["AgentFH"].ToString());
          num35 = Convert.ToDecimal(dataTable2.Rows[index2]["AdminAddDed"].ToString());
          num36 = Convert.ToDecimal(dataTable2.Rows[index2]["Change"].ToString());
        }
        DataRow row = dataTable1.NewRow();
        row["Id"] = (object) strArray[index1];
        row["userName"] = (object) dataTable2.Rows[0]["userName"].ToString();
        row["money"] = (object) num19.ToString();
        row["childnum"] = (object) num20.ToString();
        row["Charge"] = (object) num21.ToString();
        DataRow dataRow1 = row;
        string index3 = "GetCash";
        Decimal num37 = num22 - num23;
        string str1 = num37.ToString();
        dataRow1[index3] = (object) str1;
        row["GetCashErr"] = (object) num23.ToString();
        DataRow dataRow2 = row;
        string index4 = "Bet";
        num37 = num24 + num26 - num27 - num30 - num25;
        string str2 = num37.ToString();
        dataRow2[index4] = (object) str2;
        row["BetChase"] = (object) num26.ToString();
        row["WinChase"] = (object) num27.ToString();
        row["Point"] = (object) num28.ToString();
        row["Win"] = (object) num29.ToString();
        row["Cancellation"] = (object) num30.ToString();
        row["ChargeDeduct"] = (object) num31.ToString();
        row["ChargeUp"] = (object) num32.ToString();
        row["Give"] = (object) num33.ToString();
        row["AgentFH"] = (object) num34.ToString();
        row["AdminAddDed"] = (object) num35.ToString();
        row["Change"] = (object) num36.ToString();
        row["Total"] = (object) string.Concat((object) (num29 + num27 + num33 + num36 + num34 + num30 + num28 + num25 - num24 - num26));
        dataTable1.Rows.Add(row);
      }
      return dataTable1;
    }

    private string GetUserMoneyStatSql(string d1, string d2, string userId)
    {
      return "SELECT " + userId + " as Id,(select userName from N_User with(nolock) where Id=" + userId + ") as userName,(select money from N_User with(nolock) where Id=" + userId + ") as money,(SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and (Stime2 >'" + d1 + "' and STime2<'" + d2 + "') and UserId =" + userId + ") as Betno,(select count(*) from N_User with(nolock) where ParentId=" + userId + ") as childnum,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.GetCashErr),0) GetCashErr,isnull(sum(b.Bet),0) Bet,isnull(sum(b.BetChase),0) BetChase,isnull(sum(b.WinChase),0) WinChase,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.AdminAddDed),0) AdminAddDed,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where STime>='" + d1 + "' and STime<='" + d2 + "' and (UserId=" + userId + ")";
    }

    private void ajaxGetProListSub()
    {
      string str1 = this.q("stime");
      string str2 = this.q("keys");
      string d1 = this.q("d1");
      string d2 = this.q("d2");
      string str3 = this.q("id");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string str4 = "IsDel=0";
      string whereStr;
      if (!string.IsNullOrEmpty(str3))
        whereStr = str4 + " and ParentId =" + str3;
      else if (str2.Trim().Length > 0)
        whereStr = str4 + " and UserCode like '%" + Strings.PadLeft(this.AdminId) + "%' and UserName LIKE '%" + str2 + "%'";
      else
        whereStr = str4 + " and ParentId =" + this.AdminId;
      if (!string.IsNullOrEmpty(str1))
      {
        switch (str1)
        {
          case "1":
            d1 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
            d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "2":
            d1 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
            d2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "3":
            d1 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
            d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "4":
            d1 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
            d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "5":
            d1 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
            d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "6":
            d1 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
            d2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
        }
      }
      else
      {
        if (d1.Trim().Length == 0)
          d1 = this.StartTime;
        if (d2.Trim().Length == 0)
          d2 = this.EndTime;
        if (Convert.ToDateTime(d1) > Convert.ToDateTime(d2))
          d1 = d2;
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("N_User");
      string sql0 = SqlHelp.GetSql0("[Id]", "N_User a", "Id", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable1 = this.doh.GetDataTable();
      string str5 = "";
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
        str5 = str5 + "," + dataTable1.Rows[index]["Id"].ToString();
      DataTable dt = this.GetUserMoneyStatSub(d1, d2, this.AdminId);
      if (str5.Length > 1)
      {
        string userId = str5.Substring(1, str5.Length - 1);
        dt = num1 != 1 ? this.GetUserMoneyStatSub(d1, d2, userId) : this.GetUserMoneyStatSub(d1, d2, this.AdminId + "," + userId);
      }
      this.doh.Reset();
      this.doh.SqlCmd = "select Id from N_User with(nolock) where " + whereStr;
      DataTable dataTable2 = this.doh.GetDataTable();
      string str6 = "";
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
        str6 = str6 + "," + dataTable2.Rows[index]["Id"].ToString();
      DataTable userMoneyStatSub = this.GetUserMoneyStatSub(d1, d2, this.AdminId);
      if (str6.Length > 1)
      {
        string str7 = str6.Substring(1, str6.Length - 1);
        userMoneyStatSub = this.GetUserMoneyStatSub(d1, d2, this.AdminId + "," + str7);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON123(userMoneyStatSub, dt, 0, "recordcount", "table", true) + "}";
      dataTable1.Clear();
      dataTable1.Dispose();
    }

    private DataTable GetUserMoneyStatSub(string d1, string d2, string userId)
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
      Decimal num14 = new Decimal(0);
      Decimal num15 = new Decimal(0);
      Decimal num16 = new Decimal(0);
      Decimal num17 = new Decimal(0);
      Decimal num18 = new Decimal(0);
      string[] strArray = userId.Split(',');
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        Decimal num19 = new Decimal(0);
        Decimal num20 = new Decimal(0);
        Decimal num21 = new Decimal(0);
        Decimal num22 = new Decimal(0);
        Decimal num23 = new Decimal(0);
        Decimal num24 = new Decimal(0);
        Decimal num25 = new Decimal(0);
        Decimal num26 = new Decimal(0);
        Decimal num27 = new Decimal(0);
        Decimal num28 = new Decimal(0);
        Decimal num29 = new Decimal(0);
        Decimal num30 = new Decimal(0);
        Decimal num31 = new Decimal(0);
        Decimal num32 = new Decimal(0);
        Decimal num33 = new Decimal(0);
        Decimal num34 = new Decimal(0);
        Decimal num35 = new Decimal(0);
        Decimal num36 = new Decimal(0);
        string userMoneyStatSubSql = this.GetUserMoneyStatSubSql(d1, d2, strArray[index1]);
        this.doh.Reset();
        this.doh.SqlCmd = userMoneyStatSubSql;
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          num19 = Convert.ToDecimal(dataTable2.Rows[index2]["money"].ToString());
          num20 = Convert.ToDecimal(dataTable2.Rows[index2]["childnum"].ToString());
          num21 += Convert.ToDecimal(dataTable2.Rows[index2]["Charge"].ToString());
          num22 += Convert.ToDecimal(dataTable2.Rows[index2]["GetCash"].ToString());
          num23 += Convert.ToDecimal(dataTable2.Rows[index2]["GetCashErr"].ToString());
          num24 += Convert.ToDecimal(dataTable2.Rows[index2]["Bet"].ToString());
          num25 += Convert.ToDecimal(dataTable2.Rows[index2]["Betno"].ToString());
          num26 += Convert.ToDecimal(dataTable2.Rows[index2]["BetChase"].ToString());
          num27 += Convert.ToDecimal(dataTable2.Rows[index2]["WinChase"].ToString());
          num28 += Convert.ToDecimal(dataTable2.Rows[index2]["Point"].ToString());
          num29 += Convert.ToDecimal(dataTable2.Rows[index2]["Win"].ToString());
          num30 += Convert.ToDecimal(dataTable2.Rows[index2]["Cancellation"].ToString());
          num31 += Convert.ToDecimal(dataTable2.Rows[index2]["ChargeDeduct"].ToString());
          num32 += Convert.ToDecimal(dataTable2.Rows[index2]["ChargeUp"].ToString());
          num33 += Convert.ToDecimal(dataTable2.Rows[index2]["Give"].ToString());
          num34 += Convert.ToDecimal(dataTable2.Rows[index2]["AgentFH"].ToString());
          num35 += Convert.ToDecimal(dataTable2.Rows[index2]["AdminAddDed"].ToString());
          num36 += Convert.ToDecimal(dataTable2.Rows[index2]["Change"].ToString());
        }
        DataRow row = dataTable1.NewRow();
        row["Id"] = (object) strArray[index1];
        row["userName"] = (object) dataTable2.Rows[0]["userName"].ToString();
        row["money"] = (object) num19.ToString();
        row["childnum"] = (object) num20.ToString();
        row["Charge"] = (object) num21.ToString();
        DataRow dataRow1 = row;
        string index3 = "GetCash";
        Decimal num37 = num22 - num23;
        string str1 = num37.ToString();
        dataRow1[index3] = (object) str1;
        row["GetCashErr"] = (object) num23.ToString();
        DataRow dataRow2 = row;
        string index4 = "Bet";
        num37 = num24 + num26 - num27 - num30 - num25;
        string str2 = num37.ToString();
        dataRow2[index4] = (object) str2;
        row["BetChase"] = (object) num26.ToString();
        row["WinChase"] = (object) num27.ToString();
        row["Point"] = (object) num28.ToString();
        row["Win"] = (object) num29.ToString();
        row["Cancellation"] = (object) num30.ToString();
        row["ChargeDeduct"] = (object) num31.ToString();
        row["ChargeUp"] = (object) num32.ToString();
        row["Give"] = (object) num33.ToString();
        row["AgentFH"] = (object) num34.ToString();
        row["AdminAddDed"] = (object) num35.ToString();
        row["Change"] = (object) num36.ToString();
        row["Total"] = (object) string.Concat((object) (num29 + num27 + num33 + num36 + num34 + num30 + num28 + num25 - num24 - num26));
        dataTable1.Rows.Add(row);
      }
      return dataTable1;
    }

    private string GetUserMoneyStatSubSql(string d1, string d2, string userId)
    {
      return "SELECT " + userId + " as Id,(select userName from N_User with(nolock) where Id=" + userId + ") as userName,(select money from N_User with(nolock) where Id=" + userId + ") as money,(SELECT isnull(sum(Times*total),0) FROM [N_UserBet] with(nolock) where state=0 and (Stime2 >'" + d1 + "' and STime2<'" + d2 + "') and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%" + Strings.PadLeft(userId) + "%') or UserId=" + userId + ")) as Betno,(select count(*) from N_User with(nolock) where ParentId=" + userId + ") as childnum,isnull(sum(b.Charge),0) Charge,isnull(sum(b.GetCash),0) GetCash,isnull(sum(b.GetCashErr),0) GetCashErr,isnull(sum(b.Bet),0) Bet,isnull(sum(b.BetChase),0) BetChase,isnull(sum(b.WinChase),0) WinChase,isnull(sum(b.Point),0) Point,isnull(sum(b.Win),0) Win,isnull(sum(b.Cancellation),0) Cancellation,isnull(sum(b.ChargeDeduct),0) ChargeDeduct,isnull(sum(b.ChargeUp),0) ChargeUp,isnull(sum(b.Give),0) Give,isnull(sum(b.AgentFH),0) AgentFH,isnull(sum(b.AdminAddDed),0) AdminAddDed,isnull(sum(b.Change),0) Change FROM N_UserMoneyStatAll b with(nolock)  where STime>='" + d1 + "' and STime<='" + d2 + "' and (UserId in (SELECT [Id] FROM [N_User] where UserCode like '%" + Strings.PadLeft(userId) + "%') or UserId=" + userId + ")";
    }

    private void ajaxGetPointList()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      string _wherestr1 = "UserId =" + this.AdminId;
      if (str1.Trim().Length == 0)
        str1 = DateTime.Now.ToString("yyyy-MM") + "-01";
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1.Substring(0, 10) + "' and STime <='" + str2.Substring(0, 10) + "'";
      string _jsonstr = "";
      new UserMoneyStatDAL().GetListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
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
            "childnum",
            typeof (Decimal)
          },
          {
            "Charge",
            typeof (Decimal)
          },
          {
            "GetCash",
            typeof (Decimal)
          },
          {
            "GetCashErr",
            typeof (Decimal)
          },
          {
            "Bet",
            typeof (Decimal)
          },
          {
            "BetChase",
            typeof (Decimal)
          },
          {
            "WinChase",
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
            "ChargeDeduct",
            typeof (Decimal)
          },
          {
            "ChargeUp",
            typeof (Decimal)
          },
          {
            "Give",
            typeof (Decimal)
          },
          {
            "AgentFH",
            typeof (Decimal)
          },
          {
            "AdminAddDed",
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
  }
}
