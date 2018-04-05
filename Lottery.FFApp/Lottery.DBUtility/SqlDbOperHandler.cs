// Decompiled with JetBrains decompiler
// Type: Lottery.DBUtility.SqlDbOperHandler
// Assembly: Lottery.DBUtility, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 41391965-66A5-4DE4-8203-13B298F4A572
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DBUtility.dll

using System.Data;
using System.Data.SqlClient;

namespace Lottery.DBUtility
{
  public class SqlDbOperHandler : DbOperHandler
  {
    public SqlDbOperHandler(SqlConnection _conn)
    {
      this.conn = (IDbConnection) _conn;
      this.dbType = DatabaseType.SqlServer;
      this.conn.Open();
      this.cmd = this.conn.CreateCommand();
      this.da = (IDbDataAdapter) new SqlDataAdapter();
    }

    protected override void GenParameters()
    {
      SqlCommand cmd = (SqlCommand) this.cmd;
      if (this.alFieldItems.Count > 0)
      {
        for (int index = 0; index < this.alFieldItems.Count; ++index)
          cmd.Parameters.AddWithValue("@para" + index.ToString(), (object) ((DbKeyItem) this.alFieldItems[index]).fieldValue.ToString());
      }
      if (this.alSqlCmdParameters.Count > 0)
      {
        for (int index = 0; index < this.alSqlCmdParameters.Count; ++index)
          cmd.Parameters.AddWithValue(((DbKeyItem) this.alSqlCmdParameters[index]).fieldName.ToString(), (object) ((DbKeyItem) this.alSqlCmdParameters[index]).fieldValue.ToString());
      }
      if (this.alConditionParameters.Count <= 0)
        return;
      for (int index = 0; index < this.alConditionParameters.Count; ++index)
        cmd.Parameters.AddWithValue(((DbKeyItem) this.alConditionParameters[index]).fieldName.ToString(), (object) ((DbKeyItem) this.alConditionParameters[index]).fieldValue.ToString());
    }
  }
}
