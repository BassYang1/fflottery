// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserBetDAL
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
  public class UserBetDAL : ComData
  {
    protected SiteModel site;

    public UserBetDAL()
    {
      this.site = new conSite().GetSite();
    }

    public string GetWhere()
    {
      return "(" + " (IssueNum <='" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(1009) + "' and LotteryId=1009)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(2006) + "' and LotteryId=2006)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)" + " or (IssueNum <='" + new LotteryDAL().GetListNextSn(4001) + "' and LotteryId=4001)" + " )" + " and ((zhid<>0 and state<>1) or (zhid=0))";
    }

    public string GetCurWhere()
    {
      return "(" + " (IssueNum ='" + new LotteryDAL().GetListNextSn(1001) + "' and LotteryId=1001)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1002) + "' and LotteryId=1002)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1003) + "' and LotteryId=1003)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1004) + "' and LotteryId=1004)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1005) + "' and LotteryId=1005)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1007) + "' and LotteryId=1007)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1008) + "' and LotteryId=1008)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(1009) + "' and LotteryId=1009)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2001) + "' and LotteryId=2001)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2002) + "' and LotteryId=2002)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2003) + "' and LotteryId=2003)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2004) + "' and LotteryId=2004)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2005) + "' and LotteryId=2005)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(2006) + "' and LotteryId=2006)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3001) + "' and LotteryId=3001)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3002) + "' and LotteryId=3002)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(3003) + "' and LotteryId=3003)" + " or (IssueNum ='" + new LotteryDAL().GetListNextSn(4001) + "' and LotteryId=4001)" + " )" + " and ((zhid<>0 and state<>1) or (zhid=0))";
    }

    public string GetWQWhere()
    {
      return "(" + " (IssueNum <'" + new LotteryDAL().GetCurrentSn(1001) + "' and LotteryId=1001)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1002) + "' and LotteryId=1002)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1003) + "' and LotteryId=1003)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1004) + "' and LotteryId=1004)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1005) + "' and LotteryId=1005)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1007) + "' and LotteryId=1007)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1008) + "' and LotteryId=1008)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(1009) + "' and LotteryId=1009)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2001) + "' and LotteryId=2001)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2002) + "' and LotteryId=2002)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2003) + "' and LotteryId=2003)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2004) + "' and LotteryId=2004)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2005) + "' and LotteryId=2005)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(2006) + "' and LotteryId=2006)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3001) + "' and LotteryId=3001)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3002) + "' and LotteryId=3002)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(3003) + "' and LotteryId=3003)" + " or (IssueNum <'" + new LotteryDAL().GetCurrentSn(4001) + "' and LotteryId=4001)" + " )";
    }

    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, string _wherestr2, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Flex_UserBet");
        string sql0 = SqlHelp.GetSql0(_wherestr2 + "as isme,row_number() over (order by Id desc) as rowid,Id,SsId,UserId,UserName,UserMoney,PlayId,PlayName,PlayCode,LotteryId,LotteryName,IssueNum,SingleMoney,moshi,Times,Num,DX,DS,cast(Times*Total as decimal(15,4)) as Total,Point,PointMoney,Bonus,Bonus2,WinNum,WinBonus,RealGet,Pos,STime,STime2,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime,IsOpen,State,IsWin,number,poslen", "Flex_UserBet", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void BetCancelOfIssue(string IssueNum)
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
          sqlDataAdapter.SelectCommand.CommandText = "select Id,ssid,UserId,IssueNum,LotteryId,PlayId,Total,Times,STime from N_UserBet where IssueNum='" + IssueNum + "'";
          DataTable dataTable = new DataTable();
          sqlDataAdapter.Fill(dataTable);
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            string ssId = dataTable.Rows[index]["ssid"].ToString();
            string userId = dataTable.Rows[index]["UserId"].ToString();
            int int32_1 = Convert.ToInt32(dataTable.Rows[index]["LotteryId"].ToString());
            int int32_2 = Convert.ToInt32(dataTable.Rows[index]["PlayId"].ToString());
            int int32_3 = Convert.ToInt32(dataTable.Rows[index]["Id"].ToString());
            Decimal Money = Convert.ToDecimal(dataTable.Rows[index]["Total"].ToString()) * Convert.ToDecimal(dataTable.Rows[index]["Times"].ToString());
            if (new UserTotalTran().MoneyOpers(ssId, userId, Money, int32_1, int32_2, int32_3, 6, 99, string.Empty, string.Empty, "会员撤单", dataTable.Rows[index]["STime"].ToString()) > 0)
            {
              sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + (object) int32_3;
              sqlCommand.ExecuteNonQuery();
            }
          }
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
        }
      }
    }

    public int BetCancel(string betId)
    {
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        try
        {
          object[] objArray = new object[6];
          using (DbOperHandler dbOperHandler = new ComData().Doh())
          {
            dbOperHandler.Reset();
            dbOperHandler.ConditionExpress = "Id=@Id";
            dbOperHandler.AddConditionParameter("@Id", (object) betId);
            objArray = dbOperHandler.GetFields("N_UserBet", "UserId,IssueNum,LotteryId,PlayId,Total,Times,ssid,STime");
          }
          Decimal Money = Convert.ToDecimal(Convert.ToDecimal(objArray[4]) * Convert.ToDecimal(objArray[5]));
          if (new UserTotalTran().MoneyOpers(objArray[6].ToString(), objArray[0].ToString(), Money, Convert.ToInt32(objArray[2].ToString()), Convert.ToInt32(objArray[3].ToString()), Convert.ToInt32(betId), 6, 99, string.Empty, string.Empty, "会员撤单", objArray[7].ToString()) <= 0)
            return 0;
          sqlCommand.CommandText = "update N_UserBet set State=1 where Id=" + betId;
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

    public void BetCheat(string betId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) betId);
        dbOperHandler.AddFieldItem("IsCheat", (object) "1");
        dbOperHandler.Update("N_UserBet");
      }
    }
  }
}
