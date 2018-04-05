// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserBetZhDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
  public class UserBetZhDAL : ComData
  {
    protected SiteModel site;

    public UserBetZhDAL()
    {
      this.site = new conSite().GetSite();
    }

    public void GetListJSON_ZH(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Flex_UserBetZh");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by STime desc) as rowid,*,substring(Convert(varchar(20),STime,120),6,11) as ShortTime", "Flex_UserBetZh", "STime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON_ZHDetail(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("V_UserBetZhDetail");
        string sql0 = SqlHelp.GetSql0("row_number() over (order by IssueNum asc) as rowid,*,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime", "V_UserBetZhDetail", "IssueNum", _pagesize, _thispage, "asc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxZhList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string GetWhere()
    {
      return "(" + " (IssueNum >'" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)" + " or (IssueNum >'" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)" + " )" + " and zhid<>0";
    }

    public int BetCancel(string betId)
    {
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
        sqlDataAdapter.SelectCommand = sqlCommand;
        try
        {
          string str = this.GetWhere() + " and State=0 and id in(" + betId + ")";
          sqlDataAdapter.SelectCommand.CommandText = "select top 1 UserId,SsId,STime from N_UserBet where " + str + " order by Id desc";
          DataTable dataTable = new DataTable();
          sqlDataAdapter.Fill(dataTable);
          string userId = dataTable.Rows[0]["UserId"].ToString();
          string ssId = dataTable.Rows[0]["SsId"].ToString();
          if (string.IsNullOrEmpty(userId))
            return 0;
          sqlCommand.CommandText = "select isnull(sum(Total*Times),0) from N_UserBet where " + str;
          Decimal Money = Convert.ToDecimal(string.Concat(sqlCommand.ExecuteScalar()));
          if (new UserTotalTran().MoneyOpers(ssId, userId, Money, 0, 0, 0, 6, 99, "", "", "终止追号", dataTable.Rows[0]["STime"].ToString()) <= 0)
            return 0;
          sqlCommand.CommandText = "update N_UserBet set State=1 where " + str;
          sqlCommand.ExecuteNonQuery();
          return 1;
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
          return 0;
        }
      }
    }
  }
}
