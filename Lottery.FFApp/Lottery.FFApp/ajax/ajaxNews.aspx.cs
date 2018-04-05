// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.ajaxNews
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.WebApp
{
  public partial class ajaxNews : UserCenterSession
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxGetNewsList":
          this.ajaxGetNewsList();
          break;
        case "ajaxGetNewsContent":
          this.ajaxGetNewsContent();
          break;
        case "ajaxGetNewsTop1":
          this.ajaxGetNewsTop1();
          break;
        case "ajaxHistoryTop5":
          this.ajaxHistoryTop5();
          break;
        case "ajaxGetSscList":
          this.ajaxGetSscList();
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

    private void ajaxGetNewsList()
    {
      this.q("issoft");
      string str1 = this.q("d1") + " 00:00:00";
      string str2 = this.q("d2") + " 23:59:59";
      int _thispage = this.Int_ThisPage();
      int _pagesize = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      string _wherestr1 = "IsUsed =1";
      string _jsonstr = "";
      new WebAppListOper().GetNewsListJSON(_thispage, _pagesize, _wherestr1, ref _jsonstr);
      this._response = _jsonstr;
    }

    private void ajaxGetNewsContent()
    {
      string _wherestr1 = "id =" + this.q("id");
      string _jsonstr = "";
      new NewsDAL().GetListJSON(_wherestr1, ref _jsonstr);
      this._response = _jsonstr.Replace("<br/>", "");
    }

    private void ajaxGetNewsTop1()
    {
      string _jsonstr = "";
      new NewsDAL().GetListJSON_Top1(ref _jsonstr);
      this._response = _jsonstr.Replace("<br/>", "");
    }

    private void ajaxHistoryTop5()
    {
      string str1 = this.q("lid");
      this.doh.Reset();
      if (str1 == "23")
        this.doh.SqlCmd = "SELECT [IssueNum] as title,(select number from Sys_LotteryData where title=a.IssueNum) as number FROM [N_UserBet] a where lotteryId=23 and UserId=" + this.AdminId + " group by a.IssueNum order by a.IssueNum desc";
      else
        this.doh.SqlCmd = "SELECT TOP 5 [Title],[Number] FROM [Sys_LotteryData] with(nolock) where Type=" + str1 + " order by replace([Title],'-','') desc";
      DataTable dataTable = this.doh.GetDataTable();
      string str2 = "\"recordcount\":1,\"table\": [信息列表]";
      string str3 = "";
      int num = 1;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string str4 = "{\"no\":" + (object) num + ",\"title\": \"" + row["Title"].ToString() + "\",";
        if (!string.IsNullOrEmpty(string.Concat(row["Number"])))
        {
          string[] strArray = row["Number"].ToString().Split(',');
          for (int index = 0; index < strArray.Length; ++index)
            str4 = str4 + "\"ball" + (object) (index + 1) + "\": \"" + strArray[index] + "\",";
          str4 = str4.Substring(0, str4.Length - 1) + "}";
        }
        str3 = str3 + str4 + ",";
        ++num;
      }
      this._response = "{\"result\" :\"1\",\"returnval\" :\"加载完成\"," + str2.Replace("信息列表", str3.Substring(0, str3.Length > 1 ? str3.Length - 1 : 0)) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }

    private void ajaxGetSscList()
    {
      string str = this.q("type");
      int num1 = this.Int_ThisPage();
      int num2 = this.Str2Int(this.q("pagesize"), 20);
      this.Str2Int(this.q("flag"), 0);
      if (string.IsNullOrEmpty(str))
        str = "1";
      this.doh.Reset();
      this.doh.ConditionExpress = "Id=@UserId";
      this.doh.AddConditionParameter("@UserId", (object) this.AdminId);
      object field = this.doh.GetField("N_User", "Point");
      if (str == "3")
      {
        this.doh.Reset();
        this.doh.ConditionExpress = "Point=@Point";
        this.doh.AddConditionParameter("@Point", field);
        field = this.doh.GetField("N_UserLevel", "DpPoint");
      }
      string whereStr = "flag=0 and LotteryId=" + str;
      this.doh.Reset();
      this.doh.ConditionExpress = whereStr;
      int totalCount = this.doh.Count("Sys_PlaySmallType");
      string sql0 = SqlHelp.GetSql0("row_number() over (order by Sort asc) as rowid,Convert(decimal(10,2),MinBonus+" + (object) Convert.ToDecimal(field) + "*PosBonus*2) as ownMaxBonus,*,(select Title from Sys_PlayBigType where Id=a.Radio) as bigtitle", "Sys_PlaySmallType a", "Sort", num2, num1, "asc", whereStr);
      this.doh.Reset();
      this.doh.SqlCmd = sql0;
      DataTable dataTable = this.doh.GetDataTable();
      this._response = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, num2, num1, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable) + "}";
      dataTable.Clear();
      dataTable.Dispose();
    }
  }
}
