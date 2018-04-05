// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxLotterydata
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.Collect;
using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Xml;

namespace Lottery.Admin
{
  public partial class ajaxLotterydata : AdminCenter
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
        case "ajaxGetNum":
          this.ajaxGetNum();
          break;
        case "ajaxPaiJiang":
          this.ajaxPaiJiang();
          break;
        case "ajaxPaiJiangTitle":
          this.ajaxPaiJiangTitle();
          break;
        case "ajaxDel":
          this.ajaxDel();
          break;
        case "ajaxGetDNList":
          this.ajaxGetDNList();
          break;
        case "ajaxGetListNo":
          this.ajaxGetListNo();
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
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      string str3 = this.q("sort");
      string str4 = this.q("u");
      this.Str2Int(this.q("gId"), 0);
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      int num3 = this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = " STime >='" + str1 + "' and STime <'" + str2 + "' and [Type]=" + (object) num3;
      this.q("id");
      if (!string.IsNullOrEmpty(str4))
        whereStr = whereStr + "and Title like '" + str4 + "%'";
      if (!string.IsNullOrEmpty(str3))
      {
        if (str3.Equals("0"))
          whereStr += "and Flag=0";
        if (str3.Equals("1"))
          whereStr += "and Flag>0";
      }
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_LotteryData");
      string sql0 = SqlHelp.GetSql0("Id,TypeName,Title,Number,NumberAll,Total,DX,DS,OpenTime,STime,Flag,Flag2,IsFill", "V_LotteryData", "STime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetListNo()
    {
      string str1 = this.q("d1");
      string str2 = this.q("d2");
      this.q("sort");
      string str3 = this.q("u");
      this.Str2Int(this.q("gId"), 0);
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (str1.Trim().Length == 0)
        str1 = this.StartTime;
      if (str2.Trim().Length == 0)
        str2 = this.EndTime;
      if (Convert.ToDateTime(str1) > Convert.ToDateTime(str2))
        str1 = str2;
      string whereStr = " STime >='" + str1 + "' and STime <'" + str2 + "' and Flag>0";
      this.q("id");
      if (!string.IsNullOrEmpty(str3))
        whereStr = whereStr + "and Title like '" + str3 + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_LotteryData");
      string sql0 = SqlHelp.GetSql0("Id,TypeName,Title,Number,NumberAll,Total,DX,DS,OpenTime,STime,Flag,Flag2,IsFill", "V_LotteryData", "STime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetNum()
    {
      switch (this.Str2Int(this.q("flag"), 0))
      {
        case 1001:
          this.GetLotteryNumber("cqssc");
          break;
        case 1003:
          this.GetLotteryNumber("xjssc");
          break;
        case 1004:
          this.GetLotteryNumberYoule(1004);
          break;
        case 1007:
          this.GetLotteryNumber("tjssc");
          break;
        case 1008:
          this.GetLotteryNumber("ynssc");
          break;
        case 1009:
          this.GetLotteryNumberYoule(1009);
          break;
        case 1010:
          this.GetLotteryNumberYoule(1010);
          break;
        case 1011:
          this.GetLotteryNumberYoule(1011);
          break;
        case 1012:
          this.GetLotteryNumberYoule(1012);
          break;
        case 1013:
          this.GetLotteryNumber("twbingo");
          break;
        case 1014:
          this.GetLotteryNumber("jpkeno");
          break;
        case 1015:
          this.GetLotteryNumber("phkeno");
          break;
        case 1016:
          this.GetLotteryNumberYoule(1016);
          break;
        case 1017:
          this.GetLotteryNumber("krkeno");
          break;
        case 2001:
          this.GetLotteryNumber("sd11x5");
          break;
        case 2002:
          this.GetLotteryNumber("gd11x5");
          break;
        case 2003:
          this.GetLotteryNumber("sh11x5");
          break;
        case 2004:
          this.GetLotteryNumber("jx11x5");
          break;
        case 3001:
          this.GetLotteryNumber("shssl");
          break;
        case 3002:
          Fc3dData.Fc3d();
          break;
        case 3003:
          Tcp3Data.Tcp3();
          break;
        case 3004:
          this.GetLotteryNumber("krkeno");
          break;
        case 4001:
          this.GetLotteryNumber("bjpk10");
          break;
      }
      this._response = this.JsonResult(1, "获取号码成功");
    }

    private void ajaxPaiJiang()
    {
      int type = this.Str2Int(this.q("flag"), 0);
      new LotteryDataDAL().UpdateAllBetNumber(type);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "手工派奖Id为" + (object) type + "的彩种");
      this._response = this.JsonResult(1, "操作成功，请在日志中查看派奖情况");
    }

    private void ajaxPaiJiangTitle()
    {
      int type = this.Str2Int(this.q("flag"), 0);
      string str1 = this.f("ids");
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string title in str2.Split(chArray))
        new LotteryDataDAL().UpdateBetNumber(type, title);
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "手工派奖Id为" + str1 + "的彩种");
      this._response = this.JsonResult(1, "操作成功，请在日志中查看派奖情况");
    }

    private void ajaxDel()
    {
      string str = this.f("id");
      this.doh.Reset();
      this.doh.ConditionExpress = "id=@id";
      this.doh.AddConditionParameter("@id", (object) str);
      int num = this.doh.Delete("Sys_LotteryData");
      new LogAdminOperDAL().SaveLog(this.AdminId, "0", "游戏管理", "删除Id为" + str + "的彩种开奖数据");
      if (num > 0)
        this._response = this.JsonResult(1, "删除成功");
      else
        this._response = this.JsonResult(0, "删除失败");
    }

    private void ajaxGetDNList()
    {
      string str = this.q("u");
      this.Str2Int(this.q("gId"), 0);
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      string whereStr = "[Type]=" + (object) this.Str2Int(this.q("flag"), 0);
      if (!string.IsNullOrEmpty(str))
        whereStr = whereStr + "and Title like '" + str + "%'";
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("V_LotteryData");
      string sql0 = SqlHelp.GetSql0("Id,TypeName,Title,Number,OpenTime", "V_LotteryData", "OpenTime", num2, num1, "desc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      DataTable dt = new DataTable();
      dt.Columns.Add("TypeName");
      dt.Columns.Add("Title");
      dt.Columns.Add("Number");
      dt.Columns.Add("Number1");
      dt.Columns.Add("Number2");
      dt.Columns.Add("Number3");
      dt.Columns.Add("Number4");
      dt.Columns.Add("Number5");
      dt.Columns.Add("Win1");
      dt.Columns.Add("Win2");
      dt.Columns.Add("Win3");
      dt.Columns.Add("Win4");
      dt.Columns.Add("Win5");
      dt.Columns.Add("Total");
      dt.Columns.Add("OpenTime");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dt.NewRow();
        row["TypeName"] = (object) dataTable.Rows[index]["TypeName"].ToString();
        row["Title"] = (object) dataTable.Rows[index]["Title"].ToString();
        row["Number"] = (object) (dataTable.Rows[index]["Number"].ToString() + "(" + CheckSSC_DN.CheckNNum(dataTable.Rows[index]["Number"].ToString()) + ")");
        row["Number1"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 1) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 1)) + ")");
        row["Number2"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 2) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 2)) + ")");
        row["Number3"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 3) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 3)) + ")");
        row["Number4"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 4) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 4)) + ")");
        row["Number5"] = (object) (CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 5) + "(" + CheckSSC_DN.CheckNNum(CheckSSC_DN.AddDnNum(dataTable.Rows[index]["Number"].ToString(), 5)) + ")");
        row["Win1"] = (object) (CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 1).ToString() + " 倍");
        row["Win2"] = (object) (CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 2).ToString() + " 倍");
        row["Win3"] = (object) (CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 3).ToString() + " 倍");
        row["Win4"] = (object) (CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 4).ToString() + " 倍");
        row["Win5"] = (object) (CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 5).ToString() + " 倍");
        row["Total"] = (object) ((CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 1) + CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 2) + CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 3) + CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 4) + CheckSSC_DN.P_Wj(dataTable.Rows[index]["Number"].ToString(), 5) - 5) * 100);
        row["OpenTime"] = (object) dataTable.Rows[index]["OpenTime"].ToString();
        dt.Rows.Add(row);
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(3, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dt) + "}";
      dt.Clear();
      dt.Dispose();
    }

    public void GetLotteryNumberYoule(int loid)
    {
      this.doh.Reset();
      this.doh.SqlCmd = "SELECT IssueNum FROM [N_UserBet] where lotteryId=" + (object) loid + " and state=0 and DATEDIFF(minute,STime,getdate())>=5 group by IssueNum ";
      DataTable dataTable = this.doh.GetDataTable();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string Number = "";
        string[] code20 = NumberCode.CreateCode20();
        if (code20.Length >= 20)
          Number = ((Convert.ToInt32(code20[0]) + Convert.ToInt32(code20[1]) + Convert.ToInt32(code20[2]) + Convert.ToInt32(code20[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(code20[4]) + Convert.ToInt32(code20[5]) + Convert.ToInt32(code20[6]) + Convert.ToInt32(code20[7])) % 10) + "," + (object) ((Convert.ToInt32(code20[8]) + Convert.ToInt32(code20[9]) + Convert.ToInt32(code20[10]) + Convert.ToInt32(code20[11])) % 10) + "," + (object) ((Convert.ToInt32(code20[12]) + Convert.ToInt32(code20[13]) + Convert.ToInt32(code20[14]) + Convert.ToInt32(code20[15])) % 10) + "," + (object) ((Convert.ToInt32(code20[16]) + Convert.ToInt32(code20[17]) + Convert.ToInt32(code20[18]) + Convert.ToInt32(code20[19])) % 10);
        if (!new LotteryDataDAL().Exists(loid, dataTable.Rows[index]["IssueNum"].ToString()))
          new LotteryDataDAL().Add(loid, dataTable.Rows[index]["IssueNum"].ToString(), Number, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Join(",", code20));
      }
    }

    public void GetLotteryNumber(string code)
    {
      try
      {
        string html = Lottery.DAL.HtmlOperate.GetHtml("http://192.168.0.31:50000/Data/" + DateTime.Now.ToString("yyyy-MM-dd") + "/" + code + ".xml");
        if (string.IsNullOrEmpty(html))
        {
          new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
        }
        else
        {
          XmlNodeList xmlNode1 = this.GetXmlNode(html, "row");
          if (xmlNode1 == null)
            new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
          else if (xmlNode1.Count == 0)
          {
            new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
          }
          else
          {
            foreach (XmlNode xmlNode2 in xmlNode1)
            {
              string innerText1 = xmlNode2.Attributes["expect"].InnerText;
              string Number1 = xmlNode2.Attributes["opencode"].InnerText.Replace("+", ",");
              string innerText2 = xmlNode2.Attributes["opentime"].InnerText;
              if (string.IsNullOrEmpty(innerText2) || string.IsNullOrEmpty(innerText1) || string.IsNullOrEmpty(Number1))
              {
                new LogExceptionDAL().Save("采集异常", "采集找不到开奖数据的关键字符");
                break;
              }
              if (!Number1.Contains("255"))
              {
                switch (code)
                {
                  case "cqssc":
                    string str1 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1001, str1))
                    {
                      new LotteryDataDAL().Add(1001, str1, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "xjssc":
                    string str2 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(9);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1003, str2))
                    {
                      new LotteryDataDAL().Add(1003, str2, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "tjssc":
                    string str3 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1007, str3))
                    {
                      new LotteryDataDAL().Add(1007, str3, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "ynssc":
                    string str4 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 9 && !new LotteryDataDAL().Exists(1008, str4))
                    {
                      new LotteryDataDAL().Add(1008, str4, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "sgkeno":
                    if (!new LotteryDataDAL().Exists(1012, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1012, innerText1, Number2, innerText2, string.Join(",", strArray));
                      break;
                    }
                    break;
                  case "twbingo":
                    if (!new LotteryDataDAL().Exists(1013, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1013, innerText1, Number2, innerText2, string.Join(",", strArray));
                      break;
                    }
                    break;
                  case "sd11x5":
                    string str5 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2001, str5))
                    {
                      new LotteryDataDAL().Add(2001, str5, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "gd11x5":
                    string str6 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2002, str6))
                    {
                      new LotteryDataDAL().Add(2002, str6, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "sh11x5":
                    string str7 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2003, str7))
                    {
                      new LotteryDataDAL().Add(2003, str7, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "jx11x5":
                    string str8 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 14 && !new LotteryDataDAL().Exists(2004, str8))
                    {
                      new LotteryDataDAL().Add(2004, str8, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "krkeno":
                    if (!new LotteryDataDAL().Exists(1017, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1017, innerText1, Number2, innerText2, "");
                    }
                    if (!new LotteryDataDAL().Exists(3004, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3]) + Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[7]) + Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11]) + Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13])) % 10) + "," + (object) ((Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15]) + Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(3004, innerText1, Number2, innerText2, "");
                      break;
                    }
                    break;
                  case "jpkeno":
                    if (!new LotteryDataDAL().Exists(1014, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1014, innerText1, Number2, innerText2, string.Join(",", strArray));
                      break;
                    }
                    break;
                  case "phkeno":
                    if (!new LotteryDataDAL().Exists(1015, innerText1))
                    {
                      string[] strArray = Number1.Split(',');
                      string Number2 = ((Convert.ToInt32(strArray[0]) + Convert.ToInt32(strArray[1]) + Convert.ToInt32(strArray[2]) + Convert.ToInt32(strArray[3])) % 10).ToString() + "," + (object) ((Convert.ToInt32(strArray[4]) + Convert.ToInt32(strArray[5]) + Convert.ToInt32(strArray[6]) + Convert.ToInt32(strArray[7])) % 10) + "," + (object) ((Convert.ToInt32(strArray[8]) + Convert.ToInt32(strArray[9]) + Convert.ToInt32(strArray[10]) + Convert.ToInt32(strArray[11])) % 10) + "," + (object) ((Convert.ToInt32(strArray[12]) + Convert.ToInt32(strArray[13]) + Convert.ToInt32(strArray[14]) + Convert.ToInt32(strArray[15])) % 10) + "," + (object) ((Convert.ToInt32(strArray[16]) + Convert.ToInt32(strArray[17]) + Convert.ToInt32(strArray[18]) + Convert.ToInt32(strArray[19])) % 10);
                      new LotteryDataDAL().Add(1015, innerText1, Number2, innerText2, string.Join(",", strArray));
                      break;
                    }
                    break;
                  case "bjpk10":
                    if (!new LotteryDataDAL().Exists(4001, innerText1))
                    {
                      if (Number1.Split(',').Length == 10)
                        new LotteryDataDAL().Add(4001, innerText1, Number1, innerText2, "");
                      break;
                    }
                    break;
                  case "shssl":
                    string str9 = innerText1.Substring(0, 8) + "-" + innerText1.Substring(8);
                    if (Number1.Length == 5 && !new LotteryDataDAL().Exists(3001, str9))
                    {
                      new LotteryDataDAL().Add(3001, str9, Number1, innerText2, "");
                      break;
                    }
                    break;
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        new LogExceptionDAL().Save("采集异常", "采集获取开奖数据出错，错误代码：" + ex.Message);
      }
    }

    public XmlNodeList GetXmlNode(string shtml, string rootElm)
    {
      XmlNodeList xmlNodeList = (XmlNodeList) null;
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(shtml);
        xmlNodeList = xmlDocument.ChildNodes.Item(1).SelectNodes(rootElm);
      }
      catch
      {
      }
      return xmlNodeList;
    }
  }
}
