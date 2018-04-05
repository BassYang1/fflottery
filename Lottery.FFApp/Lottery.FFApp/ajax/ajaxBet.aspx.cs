// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxBet
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Text;

namespace Lottery.WebApp
{
  public partial class ajaxBet : UserCenterSession
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
        case "ajaxGetListIndex":
          this.ajaxGetListIndex();
          break;
        case "ajaxGetList":
          this.ajaxGetList();
          break;
        case "ajaxGetZHListIndex":
          this.ajaxGetZHListIndex();
          break;
        case "ajaxGetZHList":
          this.ajaxGetZHList();
          break;
        case "ajaxGetZH":
          this.ajaxGetZH();
          break;
        case "ajaxGetZHInfo":
          this.ajaxGetZHInfo();
          break;
        case "ajaxOper":
          this.ajaxOper();
          break;
        case "ajaxZHIssueNum":
          this.ajaxZHIssueNum();
          break;
        case "ajaxGetBetInfo":
          this.ajaxGetBetInfo();
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

    private void ajaxGetListIndex()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int _thispage = 0;
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "" + " UserId =" + this.AdminId;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime2 >='" + str1 + "' and STime2 <='" + str2 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetListJSON(_thispage, _pagesize, _wherestr1, this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetList()
    {
      this.q("stime");
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      string str3 = this.q("lid");
      string str4 = this.q("pid");
      string str5 = this.q("type");
      string str6 = this.q("state");
      string str7 = this.q("moshi");
      string str8 = this.q("sel");
      string str9 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "";
      if (str5 == "")
        _wherestr1 = _wherestr1 + " UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
      if (str5 == "1")
        _wherestr1 = _wherestr1 + " UserId =" + this.AdminId;
      if (str5 == "2")
        _wherestr1 = _wherestr1 + " ParentId =" + this.AdminId;
      if (str5 == "3")
        _wherestr1 = _wherestr1 + " UserCode like '%" + Strings.PadLeft(this.AdminId) + "%' and UserId<>" + this.AdminId;
      if (!string.IsNullOrEmpty(str9))
      {
        if (str8 == "ssid")
          _wherestr1 = _wherestr1 + " and ssid LIKE '" + str9 + "%'";
        if (str8 == "UserName")
          _wherestr1 = _wherestr1 + " and UserName LIKE '" + str9 + "%'";
        if (str8 == "")
          _wherestr1 = _wherestr1 + " and (UserName LIKE '" + str9 + "%' or ssid LIKE '" + str9 + "%')";
      }
      if (!string.IsNullOrEmpty(str3))
        _wherestr1 = _wherestr1 + " and LotteryId =" + str3;
      if (!string.IsNullOrEmpty(str4))
        _wherestr1 = _wherestr1 + " and PlayId ='" + str4 + "'";
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime2 >='" + str1 + "' and STime2 <='" + str2 + "'";
      if (!string.IsNullOrEmpty(str6))
        _wherestr1 = _wherestr1 + " and State =" + str6;
      if (!string.IsNullOrEmpty(str7))
        _wherestr1 = _wherestr1 + " and SingleMoney ='" + str7 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetListJSON(_thispage, _pagesize, _wherestr1, this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetBetInfo()
    {
      string Id = this.q("Id");
      string _jsonstr = "";
      new Lottery.DAL.Flex.UserBetDAL().GetBetInfoJSON(Id, this.AdminId, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetZHListIndex()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      int _thispage = 0;
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string _wherestr1 = "" + " UserId =" + this.AdminId;
      if (str1.Trim().Length > 0 && str2.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str1 + "' and STime <='" + str2 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetListJSON_ZH(_thispage, _pagesize, _wherestr1, "", ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetZHList()
    {
      string str1 = this.q("stime");
      string str2 = this.q("d1") + " 00:00:00";
      string str3 = this.q("d2") + " 23:59:59";
      string str4 = this.q("lid");
      string str5 = this.q("pid");
      string str6 = this.q("type");
      string str7 = this.q("u");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (!string.IsNullOrEmpty(str1))
      {
        switch (str1)
        {
          case "1":
            str2 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 00:00:00";
            str3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "2":
            str2 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 00:00:00";
            str3 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "3":
            str2 = DateTime.Now.AddDays(-7.0).ToString("yyyy-MM-dd") + " 00:00:00";
            str3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "4":
            str2 = DateTime.Now.ToString("yyyy-MM") + "-01 00:00:00";
            DateTime dateTime1 = DateTime.Now;
            dateTime1 = dateTime1.AddDays(0.0);
            str3 = dateTime1.ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "5":
            str2 = DateTime.Now.AddMonths(-3).ToString("yyyy-MM") + "-01 00:00:00";
            str3 = DateTime.Now.AddDays(0.0).ToString("yyyy-MM-dd") + " 23:59:59";
            break;
          case "6":
            str2 = DateTime.Now.ToString("yyyy") + "-01-01 00:00:00";
            DateTime dateTime2 = DateTime.Now;
            dateTime2 = dateTime2.AddDays(0.0);
            str3 = dateTime2.ToString("yyyy-MM-dd") + " 23:59:59";
            break;
        }
      }
      else
      {
        if (str2.Trim().Length == 0)
          str2 = this.StartTime;
        if (str3.Trim().Length == 0)
          str3 = this.EndTime;
        if (Convert.ToDateTime(str2) > Convert.ToDateTime(str3))
          str2 = str3;
      }
      string _wherestr1 = "";
      if (str6 == "")
        _wherestr1 = _wherestr1 + "UserCode like '%" + Strings.PadLeft(this.AdminId) + "%'";
      if (str6 == "1")
        _wherestr1 = _wherestr1 + "UserId =" + this.AdminId;
      if (str6 == "2")
        _wherestr1 = _wherestr1 + "ParentId =" + this.AdminId;
      if (str6 == "3")
        _wherestr1 = _wherestr1 + "UserCode like '%" + Strings.PadLeft(this.AdminId) + "%' and UserId<>" + this.AdminId;
      if (!string.IsNullOrEmpty(str7))
        _wherestr1 = _wherestr1 + " and UserName LIKE '%" + str7 + "%'";
      if (!string.IsNullOrEmpty(str4))
        _wherestr1 = _wherestr1 + " and LotteryId =" + str4;
      if (!string.IsNullOrEmpty(str5))
        _wherestr1 = _wherestr1 + " and PlayId ='" + str5 + "'";
      if (str2.Trim().Length > 0 && str3.Trim().Length > 0)
        _wherestr1 = _wherestr1 + " and STime >='" + str2 + "' and STime <='" + str3 + "'";
      string _jsonstr = "";
      new WebAppListOper().GetListJSON_ZH(_thispage, _pagesize, _wherestr1, "", ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetZH()
    {
      string str = this.Str2Str(this.q("id"));
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      string _wherestr1 = "Id=" + str;
      string _jsonstr = "";
      new UserBetZhDAL().GetListJSON_ZH(_thispage, _pagesize, _wherestr1, "", ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetZHInfo()
    {
      string str1 = this.q("id");
      string str2 = this.q("state");
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      string _wherestr1 = "zhId =" + str1;
      if (!string.IsNullOrEmpty(str2))
        _wherestr1 = _wherestr1 + " and State =" + str2;
      string _jsonstr = "";
      new WebAppListOper().GetListJSON_ZHDetail(_thispage, _pagesize, _wherestr1, "", ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxOper()
    {
      string str = this.f("ids");
      char[] chArray = new char[1]{ ',' };
      foreach (string betId in str.Split(chArray))
        new Lottery.DAL.Flex.UserBetDAL().BetCancel(betId);
      this._response = this.JsonResult(1, "撤单成功!");
    }

    private void ajaxZHIssueNum()
    {
      string lotteryId = this.q("lid");
      string str1 = this.q("flag");
      DateTime dateTime1 = this.GetDateTime();
      string str2 = dateTime1.ToString("yyyyMMdd");
      string str3 = dateTime1.ToString("HH:mm:ss");
      dateTime1.ToString("yyyy-MM-dd");
      if (lotteryId == "3002" || lotteryId == "3003")
      {
        int num1 = dateTime1.Year;
        DateTime dateTime2 = Convert.ToDateTime(num1.ToString() + "-01-01 20:30:00");
        this.doh.Reset();
        this.doh.SqlCmd = "select datediff(d,'" + dateTime2.ToString("yyyy-MM-dd HH:mm:ss") + "','" + dateTime1.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
        int num2 = Convert.ToInt32(this.doh.GetDataTable().Rows[0]["d"]) - 6 + 1;
        string str4 = dateTime1.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
        string str5 = dateTime1.ToString("yyyy-MM-dd") + " 20:30:00";
        if (dateTime1 > Convert.ToDateTime(dateTime1.ToString(" 20:30:00")))
          str5 = dateTime1.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
        else
          --num2;
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index <= 9; ++index)
        {
          string str6 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
          string oldValue1 = "编号";
          num1 = index + 1;
          string newValue1 = num1.ToString();
          string str7 = str6.Replace(oldValue1, newValue1);
          string oldValue2 = "期号";
          num1 = dateTime1.Year;
          string newValue2 = num1.ToString() + Func.AddZero(num2 + index, 3);
          string str8 = str7.Replace(oldValue2, newValue2).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", dateTime1.AddDays((double) index).ToString("yyyy-MM-dd") + " 20:30:00");
          stringBuilder.Append(str8);
        }
        this._response = "{\"result\" :\"1\",\"lotteryid\" :\"" + lotteryId + "\",\"totalcount\" :\"10\",\"table\": [" + stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1) + "]}";
      }
      else
      {
        if (UserCenterSession.LotteryTime == null)
          UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
        DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + lotteryId, "Time asc");
        DateTime dateTime2;
        if (dataRowArray.Length == 0)
        {
          dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + lotteryId, "Time asc");
          dateTime2 = dateTime1.AddDays(1.0);
          str2 = dateTime2.ToString("yyyyMMdd");
        }
        if (dateTime1 > Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " 00:00:00") && dateTime1 < Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " 02:00:01") && lotteryId == "1003")
        {
          dateTime2 = dateTime1.AddDays(-1.0);
          str2 = dateTime2.ToString("yyyyMMdd");
        }
        int num1 = UserCenterSession.LotteryTime.Select("LotteryId=" + lotteryId, "Time asc").Length;
        if (num1 > 120)
          num1 = 120;
        StringBuilder stringBuilder = new StringBuilder();
        this.doh.Reset();
        if (str1.Equals("0"))
          this.doh.SqlCmd = "select top " + (object) num1 + " * from Sys_LotteryTime where lotteryid=" + lotteryId + " and sn>=" + dataRowArray[0]["Sn"].ToString() + "order by Convert(int,sn) asc";
        if (str1.Equals("1"))
          this.doh.SqlCmd = "select top " + (object) num1 + " * from Sys_LotteryTime where lotteryid=" + lotteryId + " and sn>" + dataRowArray[0]["Sn"].ToString() + "order by Convert(int,sn) asc";
        DataTable dataTable1 = this.doh.GetDataTable();
        int num2;
        for (int index = 0; index < dataTable1.Rows.Count; ++index)
        {
          string newValue1 = str2 + "-" + dataTable1.Rows[index]["sn"].ToString();
          if (lotteryId == "1010" || lotteryId == "1017" || (lotteryId == "1012" || lotteryId == "1013") || lotteryId == "3004")
            newValue1 = string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + Convert.ToInt32(dataTable1.Rows[index]["sn"].ToString())));
          if (lotteryId == "4001")
          {
            DateTime now1 = DateTime.Now;
            dateTime2 = DateTime.Now;
            DateTime dateTime3 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 00:00:00");
            int num3;
            if (now1 > dateTime3)
            {
              DateTime now2 = DateTime.Now;
              dateTime2 = DateTime.Now;
              DateTime dateTime4 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 09:07:01");
              if (now2 < dateTime4)
              {
                num3 = 0;
                goto label_30;
              }
            }
            DateTime now3 = DateTime.Now;
            dateTime2 = DateTime.Now;
            DateTime dateTime5 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 23:57:01");
            if (now3 > dateTime5)
            {
              DateTime now2 = DateTime.Now;
              dateTime2 = DateTime.Now;
              DateTime dateTime4 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 23:59:59");
              num3 = !(now2 < dateTime4) ? 1 : 0;
            }
            else
              num3 = 1;
label_30:
            newValue1 = num3 != 0 ? string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + Convert.ToInt32(dataTable1.Rows[index]["sn"].ToString()))) : string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + 179 + Convert.ToInt32(dataTable1.Rows[index]["sn"].ToString())));
          }
          if (lotteryId == "1014" || lotteryId == "1015" || lotteryId == "1016")
            newValue1 = newValue1.Replace("-", "");
          string str4 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
          string oldValue = "编号";
          num2 = index + 1;
          string newValue2 = num2.ToString();
          string str5 = str4.Replace(oldValue, newValue2).Replace("期号", newValue1).Replace("倍数", "0").Replace("金额", "0.00").Replace("时间", dateTime1.ToString("yyyy-MM-dd") + " " + dataTable1.Rows[index]["time"]);
          stringBuilder.Append(str5);
        }
        this.doh.Reset();
        this.doh.SqlCmd = "select top " + (object) (num1 - dataTable1.Rows.Count) + " * from Sys_LotteryTime where lotteryid=" + lotteryId + " order by Convert(int,sn) asc";
        DataTable dataTable2 = this.doh.GetDataTable();
        for (int index = 0; index < dataTable2.Rows.Count; ++index)
        {
          dateTime2 = dateTime1.AddDays(1.0);
          string newValue1 = dateTime2.ToString("yyyyMMdd") + "-" + dataTable2.Rows[index]["sn"].ToString();
          if (lotteryId == "1010" || lotteryId == "1017" || lotteryId == "3004")
            newValue1 = string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + 880 + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString())));
          if (lotteryId == "1012")
            newValue1 = string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + 660 + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString())));
          if (lotteryId == "1013")
            newValue1 = string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + 203 + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString())));
          if (lotteryId == "4001")
          {
            DateTime now1 = DateTime.Now;
            dateTime2 = DateTime.Now;
            DateTime dateTime3 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 00:00:00");
            int num3;
            if (now1 > dateTime3)
            {
              DateTime now2 = DateTime.Now;
              dateTime2 = DateTime.Now;
              DateTime dateTime4 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 09:07:01");
              if (now2 < dateTime4)
              {
                num3 = 0;
                goto label_49;
              }
            }
            DateTime now3 = DateTime.Now;
            dateTime2 = DateTime.Now;
            DateTime dateTime5 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 23:57:01");
            if (now3 > dateTime5)
            {
              DateTime now2 = DateTime.Now;
              dateTime2 = DateTime.Now;
              DateTime dateTime4 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " 23:59:59");
              num3 = !(now2 < dateTime4) ? 1 : 0;
            }
            else
              num3 = 1;
label_49:
            newValue1 = num3 != 0 ? string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString()))) : string.Concat((object) (new LotteryTimeDAL().GetTsIssueNum(lotteryId) + 179 + Convert.ToInt32(dataTable2.Rows[index]["sn"].ToString())));
          }
          if (lotteryId == "1014" || lotteryId == "1015" || lotteryId == "1016")
            newValue1 = newValue1.Replace("-", "");
          string str4 = "{\"no\": \"编号\",\"sn\": \"期号\",\"count\": \"倍数\",\"price\": \"金额\",\"stime\": \"时间\"},";
          string oldValue1 = "编号";
          num2 = index + 1 + dataTable1.Rows.Count;
          string newValue2 = num2.ToString();
          string str5 = str4.Replace(oldValue1, newValue2).Replace("期号", newValue1).Replace("倍数", "0").Replace("金额", "0.00");
          string oldValue2 = "时间";
          dateTime2 = dateTime1.AddDays(1.0);
          string newValue3 = dateTime2.ToString("yyyy-MM-dd") + " " + dataTable2.Rows[index]["time"];
          string str6 = str5.Replace(oldValue2, newValue3);
          stringBuilder.Append(str6);
        }
        this._response = "{\"result\" :\"1\",\"lotteryid\" :\"" + lotteryId + "\",\"totalcount\" :\"" + (object) num1 + "\",\"table\": [" + stringBuilder.ToString().Substring(0, stringBuilder.ToString().Length - 1) + "]}";
      }
    }
  }
}
