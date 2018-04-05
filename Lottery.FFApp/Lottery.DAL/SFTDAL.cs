// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.SFTDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
    public class SFTDAL : ComData
    {
        public int SaveUrl(string userId, string url)
        {
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                new DateTimePubDAL().GetDateTime();
                try
                {
                    dbOperHandler.Reset();
                    dbOperHandler.AddFieldItem("UserId", (object)userId);
                    dbOperHandler.AddFieldItem("Url", (object)url);
                    return dbOperHandler.Insert("PayUrl_temp");
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public int Save(string code, string userId, string billno, string amount, string ordertime, string ipsbillno, string stime, string states)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    string str = ordertime.Substring(0, 4) + "-" + ordertime.Substring(4, 2) + "-" + ordertime.Substring(6, 2) + " " + ordertime.Substring(8, 2) + ":" + ordertime.Substring(10, 2) + ":" + ordertime.Substring(12, 2);
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsNo='{0}'", (object)ipsbillno);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count >= 1)
                        return 0;
                    SqlParameter[] values = new SqlParameter[8]
          {
            new SqlParameter("@Code", (object) code),
            new SqlParameter("@UserId", (object) userId),
            new SqlParameter("@OrderNO", (object) billno),
            new SqlParameter("@Amount", (object) amount),
            new SqlParameter("@OrderTime", (object) str),
            new SqlParameter("@IpsNo", (object) ipsbillno),
            new SqlParameter("@STime", (object) stime),
            new SqlParameter("@Status", (object) states)
          };
                    sqlCommand.CommandText = "INSERT INTO Pay_temp(Code,UserId,OrderNO,Amount,OrderTime,IpsNo,STime,States)\r\n                                            values(@Code,@UserId,@OrderNO,@Amount,@OrderTime,@IpsNo,@STime,@States)";
                    sqlCommand.Parameters.AddRange(values);
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("系统异常", ex.Message);
                }
            }
            return num;
        }

        public int SaveAdminPayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", (object)IpsRequestId);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                        return 0;
                    if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayAmount), 0, 0, 0, 1, 0, "在线充值", "您成功充值" + PayAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
                    {
                        SqlParameter[] values = new SqlParameter[8]
            {
              new SqlParameter("@UserId", (object) UserId),
              new SqlParameter("@PayCode", (object) PayCode),
              new SqlParameter("@PayRequestId", (object) PayRequestId),
              new SqlParameter("@PayAmount", (object) PayAmount),
              new SqlParameter("@PaySTime", (object) PaySTime),
              new SqlParameter("@IpsRequestId", (object) IpsRequestId),
              new SqlParameter("@IpsCompleteSTime", (object) IpsCompleteSTime),
              new SqlParameter("@PayState", (object) PayState)
            };
                        sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
                        sqlCommand.Parameters.AddRange(values);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                        sqlCommand.CommandText = "update N_UserCharge set InMoney=" + PayAmount + ",State=1 where SsId='" + PayRequestId + "'";
                        sqlCommand.ExecuteNonQuery();
                        num = 1;
                    }
                    else
                        num = 0;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("充值异常", ex.Message);
                }
            }
            return num;
        }

        public int SavePayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PayDzAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", (object)IpsRequestId);
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter.Fill(dataTable1);
                    if (dataTable1.Rows.Count > 0)
                        return 0;
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 InMoney from N_UserCharge where SsId='{0}'", (object)PayRequestId);
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    if (dataTable2.Rows.Count <= 0 || Convert.ToDecimal(dataTable2.Rows[0]["InMoney"].ToString()) != Convert.ToDecimal(PayAmount))
                        return 0;
                    if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayAmount), 0, 0, 0, 1, 1, "在线充值", "您成功充值" + PayAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
                    {
                        SqlParameter[] values = new SqlParameter[8]
            {
              new SqlParameter("@UserId", (object) UserId),
              new SqlParameter("@PayCode", (object) PayCode),
              new SqlParameter("@PayRequestId", (object) PayRequestId),
              new SqlParameter("@PayAmount", (object) PayAmount),
              new SqlParameter("@PaySTime", (object) PaySTime),
              new SqlParameter("@IpsRequestId", (object) IpsRequestId),
              new SqlParameter("@IpsCompleteSTime", (object) IpsCompleteSTime),
              new SqlParameter("@PayState", (object) PayState)
            };
                        sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
                        sqlCommand.Parameters.AddRange(values);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                        sqlCommand.CommandText = "update N_UserCharge set InMoney=" + PayAmount + ",DzMoney=" + PayDzAmount + ",State=1 where SsId='" + PayRequestId + "'";
                        sqlCommand.ExecuteNonQuery();
                        num = 1;
                    }
                    else
                        num = 0;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("充值异常", ex.Message);
                }
            }
            return num;
        }

        public int SavePayInfo(string PayRequestId)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "update N_UserCharge set State=2 where SsId='" + PayRequestId + "'";
                    sqlCommand.ExecuteNonQuery();
                    num = 1;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("充值异常", ex.Message);
                }
            }
            return num;
        }

        public int SaveGfbPayInfo(string UserId, string PayCode, string PayRequestId, string PayAmount, string PayDzAmount, string PaySTime, string IpsRequestId, string IpsCompleteSTime, string PayState)
        {
            int num = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ComData.connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                try
                {
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select Id from Pay_temp where IpsRequestId='{0}'", (object)IpsRequestId);
                    DataTable dataTable1 = new DataTable();
                    sqlDataAdapter.Fill(dataTable1);
                    if (dataTable1.Rows.Count > 0)
                        return 0;
                    sqlDataAdapter.SelectCommand.CommandText = string.Format("select top 1 InMoney from N_UserCharge where SsId='{0}'", (object)PayRequestId);
                    DataTable dataTable2 = new DataTable();
                    sqlDataAdapter.Fill(dataTable2);
                    if (dataTable2.Rows.Count <= 0 || Convert.ToDecimal(dataTable2.Rows[0]["InMoney"].ToString()) != Convert.ToDecimal(PayAmount))
                        return 0;
                    if (new UserTotalTran().MoneyOpers(PayRequestId, UserId, Convert.ToDecimal(PayDzAmount), 0, 0, 0, 1, 1, "在线充值", "您成功充值" + PayDzAmount + "元！请注意查看您的账变信息，如有疑问请联系在线客服！", "在线充值", "") > 0)
                    {
                        PaySTime = PaySTime.Substring(0, 4) + "-" + PaySTime.Substring(4, 2) + "-" + PaySTime.Substring(6, 2) + " " + PaySTime.Substring(8, 2) + ":" + PaySTime.Substring(10, 2) + ":" + PaySTime.Substring(12, 2);
                        IpsCompleteSTime = IpsCompleteSTime.Substring(0, 4) + "-" + IpsCompleteSTime.Substring(4, 2) + "-" + IpsCompleteSTime.Substring(6, 2) + " " + IpsCompleteSTime.Substring(8, 2) + ":" + IpsCompleteSTime.Substring(10, 2) + ":" + IpsCompleteSTime.Substring(12, 2);
                        SqlParameter[] values = new SqlParameter[8]
            {
              new SqlParameter("@UserId", (object) UserId),
              new SqlParameter("@PayCode", (object) PayCode),
              new SqlParameter("@PayRequestId", (object) PayRequestId),
              new SqlParameter("@PayAmount", (object) PayAmount),
              new SqlParameter("@PaySTime", (object) PaySTime),
              new SqlParameter("@IpsRequestId", (object) IpsRequestId),
              new SqlParameter("@IpsCompleteSTime", (object) IpsCompleteSTime),
              new SqlParameter("@PayState", (object) PayState)
            };
                        sqlCommand.CommandText = "INSERT INTO Pay_temp(UserId,PayCode,PayRequestId,PayAmount,PaySTime,IpsRequestId,IpsCompleteSTime,PayState)\r\n\t\t\t                                VALUES(@UserId,@PayCode,@PayRequestId,@PayAmount,@PaySTime,@IpsRequestId,@IpsCompleteSTime,@PayState)";
                        sqlCommand.Parameters.AddRange(values);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Parameters.Clear();
                        sqlCommand.CommandText = "update N_UserCharge set InMoney=" + PayAmount + ",DzMoney=" + PayDzAmount + ",State=1 where SsId='" + PayRequestId + "'";
                        sqlCommand.ExecuteNonQuery();
                        num = 1;
                    }
                    else
                        num = 0;
                }
                catch (Exception ex)
                {
                    new LogExceptionDAL().Save("充值异常", ex.Message);
                }
            }
            return num;
        }

        public bool Exists(string _wherestr)
        {
            int num = 0;
            using (DbOperHandler dbOperHandler = new ComData().Doh())
            {
                dbOperHandler.Reset();
                dbOperHandler.ConditionExpress = _wherestr;
                if (dbOperHandler.Exist("Pay_temp"))
                    num = 1;
            }
            return num == 1;
        }
    }
}
