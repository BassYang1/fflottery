// Decompiled with JetBrains decompiler
// Type: Lottery.IPhone.ajax
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.Collect;
using Lottery.DAL;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Configuration;
using System.Data;
using System.Web;

namespace Lottery.IPhone
{
  public partial class ajax : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxCheckLogin":
          this.ajaxCheckLogin();
          break;
        case "ajaxAllLottery":
          this.ajaxAllLottery();
          break;
        case "ajaxIndexLottery":
          this.ajaxIndexLottery();
          break;
        case "ajaxLotteryTime":
          this.ajaxLotteryTime();
          break;
        case "ajaxUserInfo":
          this.ajaxUserInfo();
          break;
        case "checkusername":
          this.ajaxCheckUserName();
          break;
        case "ajaxPopInfo":
          this.ajaxPopInfo();
          break;
        case "ajaxRegister":
          this.ajaxRegister();
          break;
        case "login":
          this.ajaxLogin();
          break;
        case "getpwd":
          this.ajaxGetPwd();
          break;
        case "logout":
          this.ajaxLogout();
          break;
        case "ajaxLotteryTime23":
          this.ajaxLotteryTime23();
          break;
        case "GetListLotteryData":
          this.GetListLotteryData();
          break;
        case "GetKaiJiangList":
          this.GetKaiJiangList();
          break;
        case "GetKaiJiangInfo":
          this.GetKaiJiangInfo();
          break;
        case "GetKaiJiangInfoTop10":
          this.GetKaiJiangInfoTop10();
          break;
        case "GetLotteryNumber":
          this.GetLotteryNumber();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this.Admin_Load("", "json");
      this._response = this.JsonResult(1, "成功登录");
    }

    public void GetLotteryNumber()
    {
        this._response = "{\"result\":\"1\",\"table\": [" + Public.GetOpenListJson(Convert.ToInt32(this.q("lid"))).Replace("[", "").Replace("]", "").ToLower() + "]}";
    }

    private void ajaxCheckLogin()
    {
      if (Cookie.GetValue(this.site.CookiePrev + "WebApp", "id") != null)
      {
        this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
        this.AdminCookiess = Cookie.GetValue(this.site.CookiePrev + "WebApp", "cookiess");
        if (this.AdminId != "0")
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "IsDel=0 and IsEnable=0 and id=@id and sessionId=@cookiess";
          this.doh.AddConditionParameter("@id", (object) this.AdminId);
          this.doh.AddConditionParameter("@cookiess", (object) this.AdminCookiess);
          if (this.doh.Count("N_User") > 0)
            this._response = this.JsonResult(1, "账号在线");
          else
            this._response = this.JsonResult(0, "账号不在线");
        }
        else
          this._response = this.JsonResult(0, "账号不在线");
      }
      else
        this._response = this.JsonResult(0, "账号不在线");
    }

    private void ajaxAllLottery()
    {
      string str1 = "";
      this.doh.Reset();
      this.doh.SqlCmd = "select * from [Sys_Lottery] where IsOpen=0 and IphoneIsOpen=0 order by Sort asc";
      DataTable dataTable = this.doh.GetDataTable();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string newValue1 = dataTable.Rows[index]["Id"].ToString();
        string str2 = "{\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"ordertime\": \"倒计时\",\"remark\": \"说明\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("类别Id", dataTable.Rows[index]["LType"].ToString()).Replace("彩种Id", newValue1).Replace("说明", dataTable.Rows[index]["IphoneRemark"].ToString());
        DateTime now = DateTime.Now;
        DateTime dateTime1 = this.GetDateTime();
        dateTime1.ToString("yyyyMMdd");
        string str3 = dateTime1.ToString("HH:mm:ss");
        dateTime1.ToString("yyyy-MM-dd");
        DateTime dateTime2;
        TimeSpan timeSpan;
        if (newValue1 == "3002" || newValue1 == "3003")
        {
          string str4 = dateTime1.ToString("yyyy-MM-dd") + " 20:30:00";
          if (dateTime1 > Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " 20:30:00"))
          {
            dateTime2 = dateTime1.AddDays(1.0);
            str4 = dateTime2.ToString("yyyy-MM-dd") + " 20:30:00";
          }
          timeSpan = Convert.ToDateTime(str4) - dateTime1;
        }
        else
        {
          if (UserCenterSession.LotteryTime == null)
            UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
          DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + newValue1, "Time asc");
          if (dataRowArray.Length == 0)
            dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + newValue1, "Time asc");
          DateTime dateTime3 = Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
          if (Convert.ToDateTime(dataRowArray[0]["Time"].ToString()) < Convert.ToDateTime(str3))
          {
            dateTime2 = dateTime1.AddDays(1.0);
            dateTime3 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
          }
          timeSpan = dateTime3 - Convert.ToDateTime(str3);
        }
        string newValue2 = string.Concat((object) ((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000));
        string str5 = str2.Replace("倒计时", newValue2);
        str1 = str1 + str5 + ",";
      }
      this._response = "{\"result\":\"1\",\"table\": [" + str1.Substring(0, str1.Length - 1) + "]}";
    }

    private void ajaxIndexLottery()
    {
      string str1 = "";
      this.doh.Reset();
      this.doh.SqlCmd = "select row_number() over (order by Sort asc) as rowid,* from [Sys_Lottery] where IsOpen=0 and Id in (1001,1004,1009,1016,2001,3002) order by Sort asc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count > 0)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          string newValue1 = dataTable.Rows[index]["Id"].ToString();
          string str2 = "{\"rowid\": \"排序Id\",\"tid\": \"类别Id\",\"id\": \"彩种Id\",\"name\": \"名称\",\"ordertime\": \"倒计时\",\"remark\": \"说明\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("排序Id", dataTable.Rows[index]["rowid"].ToString()).Replace("类别Id", dataTable.Rows[index]["LType"].ToString()).Replace("彩种Id", newValue1).Replace("说明", dataTable.Rows[index]["IphoneRemark"].ToString());
          DateTime now = DateTime.Now;
          DateTime dateTime1 = this.GetDateTime();
          dateTime1.ToString("yyyyMMdd");
          string str3 = dateTime1.ToString("HH:mm:ss");
          dateTime1.ToString("yyyy-MM-dd");
          DateTime dateTime2;
          TimeSpan timeSpan;
          if (newValue1 == "3002" || newValue1 == "3003")
          {
            string str4 = dateTime1.ToString("yyyy-MM-dd") + " 20:30:00";
            if (dateTime1 > Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " 20:30:00"))
            {
              dateTime2 = dateTime1.AddDays(1.0);
              str4 = dateTime2.ToString("yyyy-MM-dd") + " 20:30:00";
            }
            timeSpan = Convert.ToDateTime(str4) - dateTime1;
          }
          else
          {
            if (UserCenterSession.LotteryTime == null)
              UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
            DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str3 + "' and LotteryId=" + newValue1, "Time asc");
            if (dataRowArray.Length == 0)
              dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str3 + "' and LotteryId=" + newValue1, "Time asc");
            DateTime dateTime3 = Convert.ToDateTime(dateTime1.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
            if (Convert.ToDateTime(dataRowArray[0]["Time"].ToString()) < Convert.ToDateTime(str3))
            {
              dateTime2 = dateTime1.AddDays(1.0);
              dateTime3 = Convert.ToDateTime(dateTime2.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
            }
            timeSpan = dateTime3 - Convert.ToDateTime(str3);
          }
          string newValue2 = string.Concat((object) ((timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds) * 1000));
          string str5 = str2.Replace("倒计时", newValue2);
          str1 = str1 + str5 + ",";
        }
        this._response = "{\"result\":\"1\",\"table\": [" + str1.Substring(0, str1.Length - 1) + "]}";
      }
      else
        this._response = "{\"result\":\"0\",\"table\": []}";
    }

    private void ajaxLotteryTime23()
    {
      string newValue1 = this.q("lid");
      string str1 = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"cursn\": \"当前期号\",\"curnumber\": \"开奖号码\"}".Replace("名称", LotteryUtils.LotteryTitle(Convert.ToInt32(newValue1))).Replace("彩种类别", newValue1);
      DateTime now = DateTime.Now;
      DateTime dateTime = this.GetDateTime();
      dateTime.ToString("yyyyMMdd");
      dateTime.ToString("HH:mm:ss");
      dateTime.ToString("yyyy-MM-dd");
      this.doh.Reset();
      this.doh.SqlCmd = "select dbo.f_GetCloseTime(" + newValue1 + ") as closetime";
      DataTable dataTable1 = this.doh.GetDataTable();
      string str2 = str1.Replace("封单时间", dataTable1.Rows[0]["closetime"].ToString());
      TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") - DateTime.Now;
      string newValue2 = string.Concat((object) (timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
      string str3 = str2.Replace("倒计时", newValue2);
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT TOP 1 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + newValue1 + " order by Id desc";
      DataTable dataTable2 = this.doh.GetDataTable();
      string str4;
      if (dataTable2.Rows.Count > 0)
      {
        string newValue3 = dataTable2.Rows[0]["title"].ToString();
        string newValue4 = string.Concat((object) (Convert.ToDecimal(dataTable2.Rows[0]["title"].ToString()) + 1));
        string str5 = str3.Replace("下期期号", newValue4).Replace("当前期号", newValue3);
        string[] strArray = dataTable2.Rows[0]["Number"].ToString().Split(',');
        string str6 = "<p class='hm'>";
        for (int index = 0; index < strArray.Length; ++index)
          str6 = str6 + "<span>" + strArray[index] + "</span>";
        string newValue5 = str6 + "</p>";
        str4 = str5.Replace("开奖号码", newValue5);
      }
      else
      {
        string newValue3 = "您还未投注";
        string newValue4 = DateTime.Now.ToString("yyyy") + "000000001";
        str4 = str3.Replace("下期期号", newValue4).Replace("当前期号", newValue3).Replace("开奖号码", "请您先投注");
      }
      this._response = str4;
    }

    private void ajaxLotteryTime()
    {
        LotteryDAL dal = new LotteryDAL();

        string ltId = this.q("lid");//彩种Id
        int id;

        if (Int32.TryParse(ltId, out id) == false)
        {
            this._response = "{}";
            return;
        }

        SysLotteryModel lottery = dal.GetSysLotteryById(id);

        if (lottery == null)
        {
            this._response = "{}";
            return;
        }

        string str2 = "0";
        //名称
        //彩种类别
        //倒计时
        //封单时间
        //下期期号
        //已开期数
        //当前期号
        string ltInfo = "{\"name\": \"名称\",\"lotteryid\": \"彩种类别\",\"ordertime\": \"倒计时\",\"closetime\": \"封单时间\",\"nestsn\": \"下期期号\",\"opennum\": \"已开期数\",\"cursn\": \"当前期号\"}";
        ltInfo = ltInfo.Replace("名称", lottery.Title);
        ltInfo = ltInfo.Replace("彩种类别", ltId);
        ltInfo = ltInfo.Replace("封单时间", lottery.CloseTime.ToString());

        DateTime dateTime1 = DateTime.Now;
        //DateTime curDateTime = this.GetDateTime(); //当前日期时间
        DateTime curDateTime = this.GetDateTime(); //当前日期时间
        string curDate = curDateTime.ToString("yyyyMMdd"); //当前日期
        string curTime = curDateTime.ToString("HH:mm:ss"); //当前时间

        int num;
        string newValue1;
        string newValue2;
        TimeSpan timeSpan;
        if (ltId == "3002" || ltId == "3003")
        {
            num = curDateTime.Year;
            DateTime dateTime3 = Convert.ToDateTime(num.ToString() + "-01-01 20:30:00");
            this.doh.Reset();
            this.doh.SqlCmd = "select datediff(d,'" + dateTime3.ToString("yyyy-MM-dd HH:mm:ss") + "','" + curDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "') as d";
            int Num = Convert.ToInt32(this.doh.GetDataTable().Rows[0]["d"]) - 7 + 1;
            string str7 = curDateTime.AddDays(-1.0).ToString("yyyy-MM-dd") + " 20:30:00";
            string str8 = curDateTime.ToString("yyyy-MM-dd") + " 20:30:00";
            if (curDateTime > Convert.ToDateTime(curDateTime.ToString(" 20:30:00")))
                str8 = curDateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " 20:30:00";
            else
                --Num;
            num = curDateTime.Year;
            newValue1 = num.ToString() + Func.AddZero(Num, 3);
            num = curDateTime.Year;
            newValue2 = num.ToString() + Func.AddZero(Num + 1, 3);
            timeSpan = Convert.ToDateTime(str8) - Convert.ToDateTime(curTime);
        }
        else
        {
            if (UserCenterSession.LotteryTime == null)
                UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
            DataRow[] dataRowArray1 = UserCenterSession.LotteryTime.Select("Time >'" + curTime + "' and LotteryId=" + ltId, "Time asc");
            if (dataRowArray1.Length == 0)
            {
                dataRowArray1 = UserCenterSession.LotteryTime.Select("Time <='" + curTime + "' and LotteryId=" + ltId, "Time asc");
                newValue2 = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
            }
            else
            {
                newValue2 = curDate + "-" + dataRowArray1[0]["Sn"].ToString();
                dateTime1 = Convert.ToDateTime(dataRowArray1[0]["Time"].ToString());
                if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 00:00:00") && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 10:00:01") && ltId == "1003")
                    newValue2 = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
                if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:00:00") && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:59:59") && (ltId == "1014" || ltId == "1016"))
                    newValue2 = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray1[0]["Sn"].ToString();
            }
            if (Convert.ToDateTime(dataRowArray1[0]["Time"].ToString()) < Convert.ToDateTime(curTime))
                dateTime1 = Convert.ToDateTime(curDateTime.AddDays(1.0).ToString("yyyy-MM-dd") + " " + dataRowArray1[0]["Time"].ToString());
            timeSpan = dateTime1 - Convert.ToDateTime(curTime);
            DataRow[] dataRowArray2 = UserCenterSession.LotteryTime.Select("Time <'" + curTime + "' and LotteryId=" + ltId, "Time desc");
            if (dataRowArray2.Length == 0)
            {
                dataRowArray2 = UserCenterSession.LotteryTime.Select("LotteryId=" + ltId, "Time desc");
                newValue1 = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                str2 = dataRowArray2[0]["Sn"].ToString();
            }
            else
            {
                newValue1 = curDate + "-" + dataRowArray2[0]["Sn"].ToString();
                str2 = dataRowArray2[0]["Sn"].ToString();
                if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 00:00:00") && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 10:00:01") && ltId == "1003")
                {
                    newValue1 = curDateTime.AddDays(-1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
                    str2 = dataRowArray2[0]["Sn"].ToString();
                }
                if (curDateTime > Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:00:00") && curDateTime < Convert.ToDateTime(curDateTime.ToString("yyyy-MM-dd") + " 23:59:59") && (ltId == "1014" || ltId == "1016"))
                    newValue1 = curDateTime.AddDays(1.0).ToString("yyyyMMdd") + "-" + dataRowArray2[0]["Sn"].ToString();
            }
            if (ltId == "1010" || ltId == "1017" || ltId == "3004")
            {
                newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum(ltId) + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                str2 = dataRowArray2[0]["Sn"].ToString();
                newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
            }
            if (ltId == "1012")
            {
                newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1012") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                str2 = dataRowArray2[0]["Sn"].ToString();
                newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
            }
            if (ltId == "1013")
            {
                newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("1013") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                str2 = dataRowArray2[0]["Sn"].ToString();
                newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
            }
            if (ltId == "1014" || ltId == "1015" || ltId == "1016")
            {
                newValue1 = newValue1.Replace("-", "");
                newValue2 = newValue2.Replace("-", "");
            }
            if (ltId == "4001")
            {
                newValue1 = string.Concat((object)(new LotteryTimeDAL().GetTsIssueNum("4001") + Convert.ToInt32(dataRowArray2[0]["Sn"].ToString())));
                str2 = dataRowArray2[0]["Sn"].ToString();
                newValue2 = string.Concat((object)(Convert.ToInt32(newValue1) + 1));
            }
        }
        string newValue3 = string.Concat((object)(timeSpan.Days * 24 * 60 * 60 + timeSpan.Hours * 60 * 60 + timeSpan.Minutes * 60 + timeSpan.Seconds));
        string str9 = ltInfo.Replace("下期期号", newValue2).Replace("当前期号", newValue1).Replace("倒计时", newValue3);
        string oldValue = "已开期数";
        num = Convert.ToInt32(str2);
        string newValue4 = num.ToString();
        this._response = str9.Replace(oldValue, newValue4);
    }

    private void ajaxUserInfo()
    {
      string _jsonstr = "";
      this.UserInfo(ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxCheckUserName()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "username=@username";
      this.doh.AddConditionParameter("@username", (object) this.q("txtUserName"));
      if (this.doh.Exist("N_User"))
        this._response = this.JsonResult(0, "此账号已存在，不能添加");
      else
        this._response = this.JsonResult(1, "帐号不存在，可以添加");
    }

    private void ajaxPopInfo()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "id=" + this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id"));
      this.doh.AddFieldItem("OnTime", (object) DateTime.Now.ToString());
      this.doh.AddFieldItem("IsOnLine", (object) 1);
      this.doh.Update("N_User");
      this.doh.Reset();
      this.doh.SqlCmd = "select top 5 Id,Title,Msg from N_UserMessage with(nolock) where IsRead=0 and UserId=" + this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")) + " order by Id asc";
      DataTable dataTable = this.doh.GetDataTable();
      if (dataTable.Rows.Count > 0)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "Id=@Id";
          this.doh.AddConditionParameter("@Id", (object) dataTable.Rows[index]["Id"].ToString());
          this.doh.AddFieldItem("IsRead", (object) "1");
          this.doh.Update("N_UserMessage");
        }
        this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      }
      else
        this._response = "{\"result\" :\"0\",\"returnval\" :\"加载完成\",\"title\":\"0\",\"content\":\"0\"}";
      dataTable.Dispose();
    }

    private void ajaxRegister()
    {
      string _UserName = this.f("name");
      string _Password = this.f("pass");
      string _code = this.f("code");
      string str1 = this.f("u");
      string _realcode = "";
      try
      {
        if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
          this._response = this.JsonResult(0, "验证码错误");
        else if (_UserName.Length > 0 && _Password.Length > 0)
        {
          string decryptKey = ConfigurationManager.AppSettings["DesKey"].ToString();
          string str2 = this.DecryptDES(str1.Replace("@", "+"), decryptKey);
          string str3 = str2.Substring(0, str2.IndexOf('@'));
          this.doh.Reset();
          this.doh.ConditionExpress = "id=@id and Isdel=0";
          this.doh.AddConditionParameter("@id", (object) str3);
          if (this.doh.Count("N_User") < 1)
          {
            this._response = this.JsonResult(0, "对不起，该注册链接已失效！");
          }
          else
          {
            string str4 = str2.Substring(str2.IndexOf('@') + 1);
            if (Convert.ToDecimal(str4) >= new Decimal(125))
            {
              this.doh.Reset();
              this.doh.ConditionExpress = "UserId=@UserId and Point=@Point";
              this.doh.AddConditionParameter("@UserId", (object) str3);
              this.doh.AddConditionParameter("@Point", (object) Convert.ToDecimal(Convert.ToDecimal(str4) / new Decimal(10)).ToString("0.00"));
              object[] fields = this.doh.GetFields("V_UserQuota", "ChildNums,useNums,useNums2");
              if (fields == null)
              {
                this._response = this.JsonResult(0, "对不起，此链接的配额不足，请联系上级！");
                return;
              }
              if (Convert.ToDecimal(fields[0]) - (Convert.ToDecimal(fields[1]) + Convert.ToDecimal(fields[2])) < new Decimal(1))
              {
                this._response = this.JsonResult(0, "对不起，此链接的配额不足，请联系上级！");
                return;
              }
            }
            int result;
            if (int.TryParse(str3, out result))
            {
              this.GetRandomNumberString(64, false);
              int num1 = new UserDAL().Register(str3, _UserName, _Password, Convert.ToDecimal(str4));
              if (num1 > 0)
              {
                this.doh.Reset();
                this.doh.ConditionExpress = "id=@id";
                this.doh.AddConditionParameter("@id", (object) str3);
                string str5 = this.doh.GetField("N_User", "UserCode").ToString() + Strings.PadLeft(num1.ToString());
                this.doh.Reset();
                this.doh.ConditionExpress = "id=" + (object) num1;
                this.doh.AddFieldItem("UserCode", (object) str5);
                this.doh.AddFieldItem("UserGroup", (object) "0");
                if (this.doh.Update("N_User") > 0)
                {
                  this.doh.Reset();
                  this.doh.SqlCmd = "select Id,Point from N_User with(nolock) where Id=" + (object) num1 + " and IsEnable=0 and IsDel=0";
                  DataTable dataTable1 = this.doh.GetDataTable();
                  for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
                  {
                    this.doh.Reset();
                    this.doh.SqlCmd = "SELECT [Point] FROM [N_UserLevel] where Point>=125.00 and Point<=" + (object) Convert.ToDecimal(dataTable1.Rows[index1]["Point"]) + " order by [Point] desc";
                    DataTable dataTable2 = this.doh.GetDataTable();
                    for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
                    {
                      int num2 = 0;
                      string str6 = "0";
                      this.doh.Reset();
                      this.doh.SqlCmd = "select isnull(ChildNums-useNums-useNums2,0) as num from V_UserQuota with(nolock) where UserId=" + this.AdminId + " and point=" + (object) (Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10));
                      DataTable dataTable3 = this.doh.GetDataTable();
                      if (dataTable3.Rows.Count > 0)
                        num2 = Convert.ToInt32(dataTable3.Rows[0]["num"]);
                      if (Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) == Convert.ToDecimal(dataTable1.Rows[index1]["Point"]))
                      {
                        new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), 0);
                      }
                      else
                      {
                        if (num2 <= 5)
                          str6 = string.Concat((object) num2);
                        new UserQuotaDAL().SaveUserQuota(dataTable1.Rows[index1]["Id"].ToString(), Convert.ToDecimal(dataTable2.Rows[index2]["Point"]) / new Decimal(10), Convert.ToInt32(str6));
                      }
                    }
                  }
                }
                new LogSysDAL().Save("会员管理", "Id为" + (object) num1 + "的会员注册成功！");
                this._response = this.JsonResult(1, "会员注册成功");
              }
              else
                this._response = this.JsonResult(0, "注册失败，请重新注册");
            }
            else
              this._response = this.JsonResult(0, "链接地址错误！请重新打开");
          }
        }
      }
      catch (Exception ex)
      {
        this._response = this.JsonResult(0, "注册异常：" + (object) ex);
      }
    }

    private void ajaxLogin()
    {
      string str1 = this.f("name");
      string str2 = this.f("pass");
      string _code = this.f("code");
      string _realcode = "";
      if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
      {
        this._response = this.GetJsonResult(0, "验证码不正确！");
      }
      else
      {
        int iExpires = 604800;
        string _userid = new UserDAL().ChkLogin(str1.Trim(), str2.Trim(), iExpires);
        if (_userid.Length < 10)
        {
          new LogUserLoginDAL().Save(_userid, "mobile", "", "", IPHelp.ClientIP);
          _userid = this.GetJsonResult(1, "success");
        }
        this._response = _userid;
      }
    }

    private void ajaxGetPwd()
    {
      string str1 = this.f("name");
      string str2 = this.f("question");
      string str3 = this.f("answer");
      string _code = this.f("code");
      string _realcode = "";
      if (!ValidateCode.CheckValidateCode(_code, ref _realcode))
      {
        this._response = "验证码应该是" + _realcode;
      }
      else
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "userName=@userName";
        this.doh.AddConditionParameter("@userName", (object) str1);
        if (!this.doh.Exist("N_User"))
        {
          this._response = "对不起，账号不存在!";
        }
        else
        {
          this.doh.Reset();
          this.doh.ConditionExpress = "userName =@userName and Question=@Question and Answer=@Answer";
          this.doh.AddConditionParameter("@userName", (object) str1);
          this.doh.AddConditionParameter("@Question", (object) str2);
          this.doh.AddConditionParameter("@Answer", (object) str3);
          if (!this.doh.Exist("N_User"))
          {
            this._response = "对不起，验证问题错误!";
          }
          else
          {
            this.doh.Reset();
            this.doh.ConditionExpress = "userName=@userName";
            this.doh.AddConditionParameter("@userName", (object) str1);
            this.doh.AddFieldItem("Password", (object) MD5.Last64(MD5.Lower32("123456")));
            this.doh.Update("N_User");
            this._response = "密码也为您重置为：123456，请您登陆系统及时更改密码！";
            new LogSysDAL().Save("会员管理", str1 + "找回密码！");
          }
        }
      }
    }

    private void ajaxLogout()
    {
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@Id and IsEnable=0";
      this.doh.AddConditionParameter("@Id", (object) this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")));
      this.doh.AddFieldItem("LastTime", (object) DateTime.Now.ToString());
      this.doh.AddFieldItem("IsOnline", (object) 0);
      this.doh.Update("N_User");
      this.doh.Dispose();
      new UserDAL().ChkLogout();
      this._response = this.JsonResult(1, "成功退出");
    }

    private void ajaxHistoryTop5()
    {
      string str1 = this.q("lid");
      this.doh.Reset();
      if (str1 == "23")
        this.doh.SqlCmd = "SELECT TOP 10 [IssueNum] as title,(select number from Sys_LotteryData where title=a.IssueNum) as number FROM [N_UserBet] a where lotteryId=23 and UserId=" + this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "WebApp", "id")) + " group by a.IssueNum order by a.IssueNum desc";
      else
        this.doh.SqlCmd = "SELECT TOP 10 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + str1 + " order by replace([Title],'-','') desc";
      DataTable dataTable = this.doh.GetDataTable();
      string str2 = "\"recordcount\":1,\"table\": [信息列表]";
      string str3 = "";
      int num = 1;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string str4 = "{\"no\":" + (object) num + ",\"type\":\"类别\",\"title\": \"" + row["Title"].ToString() + "\",";
        if (!string.IsNullOrEmpty(string.Concat(row["Number"])))
        {
          string[] strArray = row["Number"].ToString().Split(',');
          string str5 = strArray.Length != 3 ? str4.Replace("类别", "5") : str4.Replace("类别", "3");
          for (int index = 0; index < strArray.Length; ++index)
            str5 = str5 + "\"ball" + (object) (index + 1) + "\": \"" + strArray[index] + "\",";
          str4 = str5.Substring(0, str5.Length - 1) + "}";
        }
        str3 = str3 + str4 + ",";
        ++num;
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + str2.Replace("信息列表", str3.Substring(0, str3.Length > 1 ? str3.Length - 1 : 0)) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxLotteryTime2()
    {
      this.q("lid");
      string str1 = "\"table\": [{\"loid1\": 1,\"ordertime1\": \"倒计时1\",\"nestsn1\": \"下期期号1\",\"loid2\": 9,\"ordertime2\": \"倒计时2\",\"nestsn2\": \"下期期号2\"}]";
      string[] lot1 = this.GetLot("1");
      string str2 = str1.Replace("下期期号1", lot1[0]).Replace("倒计时1", lot1[1]);
      string[] lot2 = this.GetLot("9");
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + str2.Replace("下期期号2", lot2[0]).Replace("倒计时2", lot2[1]) + "}";
    }

    private string[] GetLot(string Lid)
    {
      string[] strArray = new string[2];
      DateTime dateTime1 = DateTime.Now;
      DateTime dateTime2 = this.GetDateTime();
      string str1 = dateTime2.ToString("yyyyMMdd");
      string str2 = dateTime2.ToString("HH:mm:ss");
      dateTime2.ToString("yyyy-MM-dd");
      if (UserCenterSession.LotteryTime == null)
        UserCenterSession.LotteryTime = new LotteryTimeDAL().GetTable();
      DataRow[] dataRowArray = UserCenterSession.LotteryTime.Select("Time >'" + str2 + "' and LotteryId=" + Lid, "Time asc");
      DateTime dateTime3;
      string str3;
      if (dataRowArray.Length == 0)
      {
        dataRowArray = UserCenterSession.LotteryTime.Select("Time <='" + str2 + "' and LotteryId=" + Lid, "Time asc");
        dateTime3 = dateTime2.AddDays(1.0);
        str3 = dateTime3.ToString("yyyyMMdd") + "-" + dataRowArray[0]["Sn"].ToString();
      }
      else
      {
        str3 = str1 + "-" + dataRowArray[0]["Sn"].ToString();
        dateTime1 = Convert.ToDateTime(dataRowArray[0]["Time"].ToString());
      }
      if (Convert.ToDateTime(dataRowArray[0]["Time"].ToString()) < Convert.ToDateTime(str2))
      {
        dateTime3 = dateTime2.AddDays(1.0);
        dateTime1 = Convert.ToDateTime(dateTime3.ToString("yyyy-MM-dd") + " " + dataRowArray[0]["Time"].ToString());
      }
      string str4 = Convert.ToDateTime((dateTime1 - Convert.ToDateTime(str2)).ToString()).ToString("HH:mm:ss");
      string s = str4.Substring(0, 2);
      string str5 = str4.Substring(3, 2);
      string str6 = str4.Substring(6, 2);
      string str7;
      if (int.Parse(s) == 0)
        str7 = "<span class='k'>" + str5.Substring(0, 1) + "</span><span class='k'>" + str5.Substring(1, 1) + "</span><span class='i'>:</span><span class='k'>" + str6.Substring(0, 1) + "</span><span class='k'>" + str6.Substring(1, 1) + "</span>";
      else
        str7 = "<span class='k'>" + s.Substring(0, 1) + "</span><span class='k'>" + s.Substring(1, 1) + "</span><span class='i'>:</span><span class='k'>" + str5.Substring(0, 1) + "</span><span class='k'>" + str5.Substring(1, 1) + "</span>";
      strArray[0] = str3;
      strArray[1] = str7;
      return strArray;
    }

    public void GetListBetTop20()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 20 dbo.f_GetUserName([UserId]) as userName,dbo.f_GetLotteryName(LotteryId) as LotteryName,dbo.f_GetPlayName(PlayId) as PlayName,WinBonus,STime2,IssueNum\r\n                                FROM [N_UserBet] where State=3 or State=4 order by Id desc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void GetListLotteryData()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 5000 * from Sys_LotteryData order by Id desc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = dtHelp.DT2JSONAIR(dataTable, 10);
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void GetKaiJiangList()
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 30 * from V_KaiJiangNotice order by LotteryId asc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void GetKaiJiangInfo()
    {
      string str = this.q("lid");
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 120 *,dbo.f_GetLotteryName(type) as LotteryName from Sys_LotteryData where Type=" + str + " order by title desc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    public void GetKaiJiangInfoTop10()
    {
      string str = this.q("lid");
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT top 10 *,dbo.f_GetLotteryName(type) as LotteryName from Sys_LotteryData where Type=" + str + " order by title desc";
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private string GetOSNameByUserAgent(string userAgent)
    {
      return !userAgent.Contains("NT 10.0") ? (!userAgent.Contains("NT 6.3") ? (!userAgent.Contains("NT 6.2") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.1") ? (!userAgent.Contains("NT 6.0") ? (!userAgent.Contains("NT 5.2") ? (!userAgent.Contains("NT 5.1") ? (!userAgent.Contains("NT 5") ? (!userAgent.Contains("NT 4") ? (!userAgent.Contains("Me") ? (!userAgent.Contains("98") ? (!userAgent.Contains("95") ? (!userAgent.Contains("Mac") ? (!userAgent.Contains("Unix") ? (!userAgent.Contains("Linux") ? (!userAgent.Contains("SunOS") ? HttpContext.Current.Request.Browser.Platform : "SunOS") : "Linux") : "UNIX") : "Mac") : "Windows 95") : "Windows 98") : "Windows Me") : "Windows NT4") : "Windows 2000") : "Windows XP") : (!userAgent.Contains("64") ? "Windows Server 2003" : "Windows XP")) : "Windows Vista/Server 2008") : "Windows 7") : "Windows 7") : "Windows 8") : "Windows 8.1") : "Windows 10";
    }
  }
}
