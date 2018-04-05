// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserTotalTran
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
  public class UserTotalTran : ComData
  {
    public int MoneyOpers(string ssId, string userId, Decimal Money, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, string STime2 = "")
    {
      switch (logCode)
      {
        case 1:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Charge", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 2:
          return this.UserOpersCmd(ssId, userId, -Money, Money, "GetCash", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 3:
          return this.UserOpersCmd(ssId, userId, -Money, Money, "Bet", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 4:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Point", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 5:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Win", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 6:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Cancellation", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 7:
          return this.UserOpersCmd(ssId, userId, -Money, Money, "TranAccOut", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 8:
          return this.UserOpersCmd(ssId, userId, Money, Money, "TranAccIn", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 9:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Give", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 10:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Other", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 11:
          return this.UserOpersCmd(ssId, userId, Money, Money, "Change", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        case 12:
          return this.UserOpersCmd(ssId, userId, Money, Money, "AgentFH", logLotteryId, logPlayId, logSysId, logCode, logIsSoft, messageTitle, messageContent, reMark, STime2);
        default:
          return 0;
      }
    }

    private int UserOpersCmd(string ssId, string userId, Decimal userMoney, Decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, string STime2)
    {
      using (SqlConnection connection = new SqlConnection(ComData.connectionString))
      {
        connection.Open();
        SqlCommand sqlCommand = new SqlCommand("Global_UserOperTran", connection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        try
        {
          sqlCommand.Parameters.Add("@LogSsId", SqlDbType.VarChar, 50).Value = (object) ssId;
          sqlCommand.Parameters.Add("@LogUserId", SqlDbType.VarChar, 20).Value = (object) userId;
          sqlCommand.Parameters.Add("@LogUserMoney", SqlDbType.Decimal, 18).Value = (object) userMoney;
          sqlCommand.Parameters.Add("@LogStatMoney", SqlDbType.Decimal, 18).Value = (object) statMoney;
          sqlCommand.Parameters.Add("@LogStatType", SqlDbType.VarChar, 20).Value = (object) statType;
          sqlCommand.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = (object) logLotteryId;
          sqlCommand.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = (object) logPlayId;
          sqlCommand.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = (object) logSysId;
          sqlCommand.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = (object) logCode;
          sqlCommand.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = (object) logIsSoft;
          sqlCommand.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = (object) reMark;
          sqlCommand.Parameters.Add("@LogMessageTitle", SqlDbType.VarChar, 50).Value = (object) messageTitle;
          sqlCommand.Parameters.Add("@LogMessageContent", SqlDbType.VarChar, 200).Value = (object) messageContent;
          sqlCommand.Parameters.Add("@STime2", SqlDbType.VarChar, 50).Value = (object) STime2;
          sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 200);
          sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
          sqlCommand.ExecuteNonQuery();
          object obj = sqlCommand.Parameters["@output"].Value;
          connection.Close();
          return Convert.ToInt32(obj);
        }
        catch (Exception ex)
        {
          new LogExceptionDAL().Save("系统异常", ex.Message);
          return 0;
        }
      }
    }

    private static int UserMoneyStatTran(string UserId, string StatType, Decimal StatValue)
    {
      using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
      {
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        try
        {
          sqlCommand.CommandText = "select Id From N_UserMoneyStatAll with(nolock) where UserId=" + UserId + " and DateDiff(D,STime,getDate())=0";
          int int32 = Convert.ToInt32(sqlCommand.ExecuteScalar());
          int num;
          if (int32 == 0)
          {
            sqlCommand.CommandText = "insert into N_UserMoneyStatAll(UserId," + StatType + ",STime) values (" + UserId + "," + (object) StatValue + ",getdate())";
            num = sqlCommand.ExecuteNonQuery();
          }
          else
          {
            sqlCommand.CommandText = "update N_UserMoneyStatAll set " + StatType + "=" + StatType + "+" + (object) StatValue + " where Id=" + (object) int32;
            num = sqlCommand.ExecuteNonQuery();
          }
          return num;
        }
        catch (Exception ex)
        {
          return 0;
        }
        finally
        {
          sqlConnection.Dispose();
          sqlConnection.Close();
        }
      }
    }
  }
}
