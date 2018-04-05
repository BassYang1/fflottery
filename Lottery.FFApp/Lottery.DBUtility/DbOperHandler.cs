// Decompiled with JetBrains decompiler
// Type: Lottery.DBUtility.DbOperHandler
// Assembly: Lottery.DBUtility, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 41391965-66A5-4DE4-8203-13B298F4A572
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DBUtility.dll

using Lottery.Utils;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Lottery.DBUtility
{
    public abstract class DbOperHandler : IDisposable
    {
        public string ConditionExpress = string.Empty;
        public string SqlCmd = string.Empty;
        protected string tableName = string.Empty;
        protected string fieldName = string.Empty;
        protected ArrayList alFieldItems = new ArrayList(10);
        protected ArrayList alSqlCmdParameters = new ArrayList(5);
        protected ArrayList alConditionParameters = new ArrayList(5);
        public DatabaseType dbType;
        protected IDbConnection conn;
        protected IDbCommand cmd;
        protected IDbDataAdapter da;

        ~DbOperHandler()
        {
            this.conn.Close();
        }

        public IDbConnection GetConnection()
        {
            return this.conn;
        }

        public void Reset()
        {
            this.alFieldItems.Clear();
            this.alSqlCmdParameters.Clear();
            this.alConditionParameters.Clear();
            this.ConditionExpress = string.Empty;
            this.SqlCmd = string.Empty;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.cmd.CommandType = CommandType.Text;
        }

        public void AddFieldItem(string _fieldName, object _fieldValue)
        {
            for (int index = 0; index < this.alFieldItems.Count; ++index)
            {
                if (((DbKeyItem)this.alFieldItems[index]).fieldName == _fieldName)
                    throw new ArgumentException(_fieldName + "不能重复赋值!");
            }
            this.alFieldItems.Add((object)new DbKeyItem(_fieldName, _fieldValue));
        }

        public void AddFieldItems(object[,] _vFields)
        {
            if (object.Equals((object)_vFields, (object)null) || _vFields.GetUpperBound(0) != 1 || _vFields.Rank != 2)
                return;
            for (int index = 0; index <= _vFields.GetUpperBound(1); ++index)
            {
                if (!object.Equals(_vFields[0, index], (object)null))
                    this.AddFieldItem(_vFields[0, index].ToString(), _vFields[1, index]);
            }
        }

        public void AddConditionParameter(string _conditionName, object _conditionValue)
        {
            for (int index = 0; index < this.alConditionParameters.Count; ++index)
            {
                if (((DbKeyItem)this.alConditionParameters[index]).fieldName == _conditionName)
                    throw new ArgumentException("条件参数名\"" + _conditionName + "\"不能重复赋值!");
            }
            this.alConditionParameters.Add((object)new DbKeyItem(_conditionName, _conditionValue));
        }

        public void AddConditionParameters(object[,] _vParameters)
        {
            if (object.Equals((object)_vParameters, (object)null) || _vParameters.GetUpperBound(0) != 1 || _vParameters.Rank != 2)
                return;
            for (int index = 0; index <= _vParameters.GetUpperBound(1); ++index)
            {
                if (!object.Equals(_vParameters[0, index], (object)null))
                    this.AddConditionParameter(_vParameters[0, index].ToString(), _vParameters[1, index]);
            }
        }

        public int Count(string tableName)
        {
            return Convert.ToInt32(this.GetField(tableName, "count(*)", false).ToString());
        }

        public int CountId(string tableName)
        {
            return Convert.ToInt32(this.GetField(tableName, "count(id)", false).ToString());
        }

        public bool Exist(string tableName)
        {
            return this.GetField(tableName, "count(*)", false).ToString() != "0";
        }

        public int MaxId(string tableName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "max(id)", false).ToString());
        }

        public int MinValue(string tableName, string fieldName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "min(" + fieldName + ")", false).ToString());
        }

        public int MaxValue(string tableName, string fieldName)
        {
            return Convert.ToInt32("0" + this.GetField(tableName, "max(" + fieldName + ")", false).ToString());
        }

        public Decimal MaxDecValue(string tableName, string fieldName)
        {
            return Convert.ToDecimal("0" + this.GetField(tableName, "max(" + fieldName + ")", false).ToString());
        }

        protected abstract void GenParameters();

        public int Insert(string _tableName)
        {
            this.tableName = _tableName;
            this.fieldName = string.Empty;
            this.SqlCmd = "insert into [" + this.tableName + "](";
            string str = " values(";
            for (int index = 0; index < this.alFieldItems.Count - 1; ++index)
            {
                DbOperHandler dbOperHandler = this;
                dbOperHandler.SqlCmd = dbOperHandler.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[index]).fieldName + "]";
                this.SqlCmd += ",";
                str = str + "@para" + index.ToString() + ",";
            }
            DbOperHandler dbOperHandler1 = this;
            dbOperHandler1.SqlCmd = dbOperHandler1.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[this.alFieldItems.Count - 1]).fieldName + "]";
            this.SqlCmd += ") ";
            this.SqlCmd += str + "@para" + (this.alFieldItems.Count - 1).ToString() + ")";
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            this.cmd.ExecuteNonQuery();
            int num = 0;
            try
            {
                this.cmd.CommandText = "select @@identity as id";
                num = Convert.ToInt32(this.cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public int Update(string _tableName)
        {
            this.tableName = _tableName;
            this.fieldName = string.Empty;
            this.SqlCmd = "UPDATE [" + this.tableName + "] SET ";
            for (int index = 0; index < this.alFieldItems.Count - 1; ++index)
            {
                DbOperHandler dbOperHandler = this;
                dbOperHandler.SqlCmd = dbOperHandler.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[index]).fieldName + "]";
                this.SqlCmd += "=";
                this.SqlCmd += "@para";
                this.SqlCmd += index.ToString();
                this.SqlCmd += ",";
            }
            DbOperHandler dbOperHandler1 = this;
            dbOperHandler1.SqlCmd = dbOperHandler1.SqlCmd + "[" + ((DbKeyItem)this.alFieldItems[this.alFieldItems.Count - 1]).fieldName + "]";
            this.SqlCmd += "=";
            this.SqlCmd += "@para";
            this.SqlCmd += (this.alFieldItems.Count - 1).ToString();
            if (this.ConditionExpress != string.Empty)
            {
                DbOperHandler dbOperHandler2 = this;
                dbOperHandler2.SqlCmd = dbOperHandler2.SqlCmd + " where " + this.ConditionExpress;
            }
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteNonQuery();
        }

        public int ExecuteSqlNonQuery()
        {
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteNonQuery();
        }

        public bool DropTable(string _tableName)
        {
            try
            {
                this.cmd.CommandText = "drop table [" + _tableName + "]";
                this.cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ExistTable(string _tableName)
        {
            try
            {
                this.cmd.CommandText = "select top 1 * from [" + _tableName + "]";
                this.cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public object GetField(string _tableName, string _fieldName, bool _isField)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            if (_isField)
                this.SqlCmd = "select [" + this.fieldName + "] from [" + this.tableName + "] with(nolock) ";
            else
                this.SqlCmd = "select " + this.fieldName + " from [" + this.tableName + "] with(nolock) ";
            if (this.ConditionExpress != string.Empty)
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteScalar() ?? (object)string.Empty;
        }

        public object GetField(string _tableName, string _fieldName)
        {
            return this.GetField(_tableName, _fieldName, true);
        }

        public object[] GetFields(string _tableName, string _fieldNames)
        {
            this.SqlCmd = "select " + _fieldNames + " from " + _tableName + " with(nolock) ";
            if (this.ConditionExpress != string.Empty)
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            DataSet dataSet = new DataSet();
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dataSet);
            DataTable table = dataSet.Tables[0];
            if (table.Rows.Count <= 0)
                return (object[])null;
            object[] objArray = new object[table.Columns.Count];
            for (int index = 0; index < table.Columns.Count; ++index)
                objArray[index] = table.Rows[0][index];
            return objArray;
        }

        public int GetCount(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            this.SqlCmd = "select count(" + this.fieldName + ") from [" + this.tableName + "] with(nolock) ";
            if (this.ConditionExpress != string.Empty)
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return (int)this.cmd.ExecuteScalar();
        }

        public DataTable GetDataTable()
        {
            return this.GetDataSet().Tables[0];
        }

        public DataSet GetDataSet()
        {
            this.alConditionParameters.Clear();
            this.ConditionExpress = string.Empty;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            DataSet dataSet = new DataSet();
            this.da.SelectCommand = this.cmd;
            this.da.Fill(dataSet);
            return dataSet;
        }

        public int Add(string _tableName, string _fieldName)
        {
            return this.Add(_tableName, _fieldName, 1);
        }

        public int Add(string _tableName, string _fieldName, int _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            int num = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName)) + _num;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, (object)num);
            this.Update(this.tableName);
            return num;
        }

        public Decimal Add(string _tableName, string _fieldName, Decimal _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            Decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName)) + _num;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, (object)num);
            this.Update(this.tableName);
            return num;
        }

        public int Deduct(string _tableName, string _fieldName)
        {
            return this.Deduct(_tableName, _fieldName, 1);
        }

        public Decimal Deduct(string _tableName, string _fieldName, Decimal _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            Decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName));
            if (num > new Decimal(0))
            {
                num -= _num;
                if (num < new Decimal(0))
                    num = new Decimal(0);
            }
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, (object)num);
            this.Update(this.tableName);
            return num;
        }

        public Decimal Deduct2(string _tableName, string _fieldName, Decimal _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            Decimal num = Convert.ToDecimal("0" + this.GetField(this.tableName, this.fieldName)) - _num;
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, (object)num);
            this.Update(this.tableName);
            return num;
        }

        public int Deduct(string _tableName, string _fieldName, int _num)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            int num = Convert.ToInt32("0" + this.GetField(this.tableName, this.fieldName));
            if (num > 0)
            {
                num -= _num;
                if (num < 0)
                    num = 0;
            }
            this.cmd.Parameters.Clear();
            this.cmd.CommandText = string.Empty;
            this.AddFieldItem(_fieldName, (object)num);
            this.Update(this.tableName);
            return num;
        }

        public int Sum(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            return Convert.ToInt32("0" + this.GetField(this.tableName, "sum(" + this.fieldName + ")", false));
        }

        public int Delete(string _tableName)
        {
            this.tableName = _tableName;
            this.SqlCmd = "delete from [" + this.tableName + "]";
            if (this.ConditionExpress != string.Empty)
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteNonQuery();
        }

        public int Audit(string _tableName, string _fieldName)
        {
            this.tableName = _tableName;
            this.fieldName = _fieldName;
            this.SqlCmd = "UPDATE [" + this.tableName + "] SET [" + this.fieldName + "]=1-" + this.fieldName;
            if (this.ConditionExpress != string.Empty)
                this.SqlCmd = this.SqlCmd + " where " + this.ConditionExpress;
            this.cmd.CommandText = this.SqlCmd;
            this.GenParameters();
            return this.cmd.ExecuteNonQuery();
        }

        public DataRow GetSP_Row(string ProcedureName)
        {
            this.cmd.CommandText = ProcedureName;
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.GenParameters();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = (SqlCommand)this.cmd;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();
            if (dataTable.Rows.Count <= 0)
                return (DataRow)null;
            return dataTable.Rows[0];
        }

        public DataRowCollection GetSP_Rows(string ProcedureName)
        {
            this.cmd.CommandText = ProcedureName;
            this.cmd.CommandType = CommandType.StoredProcedure;
            this.GenParameters();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = (SqlCommand)this.cmd;
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            sqlDataAdapter.Dispose();
            return dataTable.Rows;
        }

        public int ExecuteSql(string SqlCmd)
        {
            this.cmd.CommandText = SqlCmd;
            this.cmd.ExecuteNonQuery();
            int num = 0;
            try
            {
                this.cmd.CommandText = "select @@identity as id";
                num = Convert.ToInt32(this.cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
            }
            return num;
        }

        public void ExecuteProcActive(string ProcedureName)
        {
            SqlConnection connection = new SqlConnection(Const.ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.ExecuteNonQuery();
            connection.Close();
        }

        public object[] ExecuteProc(string ProcedureName, string userId)
        {
            object[] objArray = new object[3];
            SqlConnection connection = new SqlConnection(Const.ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = (object)userId;
            sqlCommand.Parameters.Add("@ownbet", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@ownbet"].Direction = ParameterDirection.Output;
            sqlCommand.ExecuteNonQuery();
            objArray[0] = sqlCommand.Parameters["@ownbet"].Value;
            connection.Close();
            return objArray;
        }

        public object ExecuteProcAuto(string ProcedureName, string userId)
        {
            SqlConnection connection = new SqlConnection(Const.ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = (object)userId;
            sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
            sqlCommand.ExecuteNonQuery();
            object obj = sqlCommand.Parameters["@output"].Value;
            connection.Close();
            return obj;
        }

        public object ExecuteProc_Active1(string ProcedureName, string userId, string CheckIp, string CheckMachine)
        {
            SqlConnection connection = new SqlConnection(Const.ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(ProcedureName, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@userId", SqlDbType.VarChar, 100).Value = (object)userId;
            sqlCommand.Parameters.Add("@CheckIp", SqlDbType.VarChar, 100).Value = (object)CheckIp;
            sqlCommand.Parameters.Add("@CheckMachine", SqlDbType.VarChar, 100).Value = (object)CheckMachine;
            sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 100);
            sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
            sqlCommand.ExecuteNonQuery();
            object obj = sqlCommand.Parameters["@output"].Value;
            connection.Close();
            return obj;
        }

        public int ExecuteProcUserOpers(string ssId, string userId, Decimal userMoney, Decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark)
        {
            SqlConnection connection = new SqlConnection(Const.ConnectionString);
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("Global_UserOperTran", connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@SsId", SqlDbType.VarChar, 50).Value = (object)ssId;
            sqlCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = (object)userId;
            sqlCommand.Parameters.Add("@UserMoney", SqlDbType.Decimal, 18).Value = (object)userMoney;
            sqlCommand.Parameters.Add("@StatMoney", SqlDbType.Decimal, 18).Value = (object)statMoney;
            sqlCommand.Parameters.Add("@StatType", SqlDbType.VarChar, 20).Value = (object)statType;
            sqlCommand.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = (object)logLotteryId;
            sqlCommand.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = (object)logPlayId;
            sqlCommand.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = (object)logSysId;
            sqlCommand.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = (object)logCode;
            sqlCommand.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = (object)logIsSoft;
            sqlCommand.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = (object)reMark;
            sqlCommand.Parameters.Add("@MessageTitle", SqlDbType.VarChar, 50).Value = (object)messageTitle;
            sqlCommand.Parameters.Add("@MessageContent", SqlDbType.VarChar, 200).Value = (object)messageContent;
            sqlCommand.Parameters.Add("@output", SqlDbType.VarChar, 200);
            sqlCommand.Parameters["@output"].Direction = ParameterDirection.Output;
            sqlCommand.ExecuteNonQuery();
            object obj = sqlCommand.Parameters["@output"].Value;
            connection.Close();
            return Convert.ToInt32(obj);
        }

        public object ExecuteProcUserOpers(string ssId, string userId, Decimal userMoney, Decimal statMoney, string statType, int logLotteryId, int logPlayId, int logSysId, int logCode, int logIsSoft, string messageTitle, string messageContent, string reMark, SqlCommand cmd)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Global_UserOperTran";
            cmd.Parameters.Add("@SsId", SqlDbType.VarChar, 50).Value = (object)ssId;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = (object)userId;
            cmd.Parameters.Add("@UserMoney", SqlDbType.Decimal, 18).Value = (object)userMoney;
            cmd.Parameters.Add("@StatMoney", SqlDbType.Decimal, 18).Value = (object)statMoney;
            cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 20).Value = (object)statType;
            cmd.Parameters.Add("@LogLotteryId", SqlDbType.Int, 8).Value = (object)logLotteryId;
            cmd.Parameters.Add("@LogPlayId", SqlDbType.Int, 8).Value = (object)logPlayId;
            cmd.Parameters.Add("@LogSysId", SqlDbType.Int, 8).Value = (object)logSysId;
            cmd.Parameters.Add("@LogCode", SqlDbType.Int, 8).Value = (object)logCode;
            cmd.Parameters.Add("@LogIsSoft", SqlDbType.Int, 8).Value = (object)logIsSoft;
            cmd.Parameters.Add("@LogReMark", SqlDbType.VarChar, 200).Value = (object)reMark;
            cmd.Parameters.Add("@MessageTitle", SqlDbType.VarChar, 50).Value = (object)messageTitle;
            cmd.Parameters.Add("@MessageContent", SqlDbType.VarChar, 200).Value = (object)messageContent;
            cmd.Parameters.Add("@output", SqlDbType.VarChar, 200);
            cmd.Parameters["@output"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@output"].Value;
        }

        public void Dispose()
        {
            this.conn.Close();
            this.conn.Dispose();
        }
    }
}
