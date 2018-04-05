// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.UserMoneyLogDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DAL
{
  public class UserMoneyLogDAL : ComData
  {
    public bool Exists(string _wherestr)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr;
        if (dbOperHandler.Exist("N_UserMoneyLog"))
          num = 1;
      }
      return num == 1;
    }

    public void Delete()
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "1=1";
        dbOperHandler.Delete("N_UserMoneyLog");
      }
    }

    public Decimal GetMaxMoney(int userId)
    {
      Decimal num = new Decimal(0);
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 ISNULL(MoneyAfter,0) as MoneyAfter from N_UserMoneyLog with(nolock) where userId=" + (object) userId + " order by Id desc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (dataTable.Rows.Count > 0)
        {
          num = Convert.ToDecimal(dataTable.Rows[0]["MoneyAfter"].ToString());
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "id=@id";
          dbOperHandler.AddConditionParameter("@id", (object) userId);
          num = Convert.ToDecimal(dbOperHandler.GetField("N_User", "Money"));
        }
      }
      return num;
    }

    public void Save(SqlCommand cmd, int _userId, int _LotteryId, int _PlayId, int _SysId, Decimal _MoneyChange, int _Code, int _IsSoft, string _Remark)
    {
      try
      {
        Decimal maxMoney = this.GetMaxMoney(_userId);
        SqlParameter[] values = new SqlParameter[11]
        {
          new SqlParameter("@UserId", (object) _userId),
          new SqlParameter("@LotteryId", (object) _LotteryId),
          new SqlParameter("@PlayId", (object) _PlayId),
          new SqlParameter("@SysId", (object) _SysId),
          new SqlParameter("@MoneyChange", (object) _MoneyChange),
          new SqlParameter("@MoneyAgo", (object) maxMoney),
          new SqlParameter("@MoneyAfter", (object) (maxMoney + _MoneyChange)),
          new SqlParameter("@IsOk", (object) 1),
          new SqlParameter("@Code", (object) _Code),
          new SqlParameter("@IsSoft", (object) _IsSoft),
          new SqlParameter("@Remark", (object) _Remark)
        };
        cmd.CommandText = string.Format("insert into N_UserMoneyLog(UserId,LotteryId,PlayId,SysId,MoneyChange,MoneyAgo,MoneyAfter,STime,IsOk,Code,IsSoft,Remark) \r\n                    values(@UserId,@LotteryId,@PlayId,@SysId,@MoneyChange,@MoneyAgo,@MoneyAfter,getdate(),@IsOk,@Code,@IsSoft,@Remark)");
        cmd.Parameters.AddRange(values);
        cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void AgencyPoint(SqlCommand cmd, int UserId, int LotteryId, int PlayId, int BetId, Decimal BetMoney)
    {
      cmd.CommandText = "select ParentId from N_User with(nolock) where Id=" + (object) UserId;
      int int32_1 = Convert.ToInt32(cmd.ExecuteScalar());
      if (int32_1 == 0)
        return;
      DataTable dataTable = new DataTable();
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
      sqlDataAdapter.SelectCommand = cmd;
      sqlDataAdapter.SelectCommand.CommandText = "select ParentId,Point,Money from N_User with(nolock) where Id=" + int32_1.ToString();
      sqlDataAdapter.Fill(dataTable);
      if (dataTable.Rows.Count <= 0)
        return;
      DataRow row = dataTable.Rows[0];
      int int32_2 = Convert.ToInt32(row["Point"]);
      cmd.CommandText = "select top 1 MoneyAfter From N_UserMoneyLog with(nolock) Where UserId=" + int32_1.ToString() + " order by Id desc";
      if (Convert.ToDecimal(cmd.ExecuteScalar()) == new Decimal(0))
        Convert.ToDecimal(row["Money"]);
      cmd.CommandText = "select Point from N_User with(nolock) where Id=" + (object) UserId;
      int int32_3 = Convert.ToInt32(cmd.ExecuteScalar());
      if (int32_2 < int32_3)
        return;
      Decimal num = BetMoney * Convert.ToDecimal(int32_2 - int32_3) / new Decimal(1000);
      new UserMoneyLogDAL().Save(cmd, int32_1, LotteryId, PlayId, BetId, num, 7, 99, "下级返点");
      new UserMoneyStatDAL().Save(cmd, int32_1, "Point", num);
      this.AgencyPoint(cmd, int32_1, LotteryId, PlayId, BetId, BetMoney);
      dataTable.Dispose();
    }
  }
}
