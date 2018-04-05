// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.ExcelManage
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Lottery.Utils
{
  public class ExcelManage
  {
    public static ArrayList GetExcelTables(string ExcelFileName)
    {
      DataTable dataTable = new DataTable();
      ArrayList arrayList = new ArrayList();
      if (File.Exists(ExcelFileName))
      {
        using (OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
        {
          DataTable oleDbSchemaTable;
          try
          {
            oleDbConnection.Open();
            oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[4]
            {
              null,
              null,
              null,
              (object) "TABLE"
            });
          }
          catch (Exception ex)
          {
            throw ex;
          }
          int count = oleDbSchemaTable.Rows.Count;
          for (int index = 0; index < count; ++index)
          {
            string str = oleDbSchemaTable.Rows[index][2].ToString().Trim().TrimEnd('$');
            if (arrayList.IndexOf((object) str) < 0)
              arrayList.Add((object) str);
          }
        }
      }
      return arrayList;
    }

    public static ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
    {
      DataTable dataTable = new DataTable();
      ArrayList arrayList = new ArrayList();
      if (File.Exists(ExcelFileName))
      {
        using (OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
        {
          oleDbConnection.Open();
          DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[4]
          {
            null,
            null,
            (object) TableName,
            null
          });
          int count = oleDbSchemaTable.Rows.Count;
          for (int index = 0; index < count; ++index)
          {
            string str = oleDbSchemaTable.Rows[index]["Column_Name"].ToString().Trim();
            arrayList.Add((object) str);
          }
        }
      }
      return arrayList;
    }

    public static bool OutputToExcel(DataTable Table, string ExcelFilePath)
    {
      if (File.Exists(ExcelFilePath))
        throw new Exception("该文件已经存在！");
      if (Table.TableName.Trim().Length == 0 || Table.TableName.ToLower() == "table")
        Table.TableName = "Sheet1";
      int count = Table.Columns.Count;
      int index1 = 0;
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[count];
      string str1 = "Create Table " + Table.TableName + "(";
      OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;");
      OleDbCommand oleDbCommand = new OleDbCommand();
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) "System.Decimal");
      arrayList.Add((object) "System.Double");
      arrayList.Add((object) "System.Int16");
      arrayList.Add((object) "System.Int32");
      arrayList.Add((object) "System.Int64");
      arrayList.Add((object) "System.Single");
      foreach (DataColumn column in (InternalDataCollectionBase) Table.Columns)
      {
        if (arrayList.IndexOf((object) column.DataType.ToString()) >= 0)
        {
          oleDbParameterArray[index1] = new OleDbParameter("@" + column.ColumnName, OleDbType.Double);
          oleDbCommand.Parameters.Add(oleDbParameterArray[index1]);
          str1 = index1 + 1 != count ? str1 + column.ColumnName + " double," : str1 + column.ColumnName + " double)";
        }
        else
        {
          oleDbParameterArray[index1] = new OleDbParameter("@" + column.ColumnName, OleDbType.VarChar);
          oleDbCommand.Parameters.Add(oleDbParameterArray[index1]);
          str1 = index1 + 1 != count ? str1 + column.ColumnName + " varchar," : str1 + column.ColumnName + " varchar)";
        }
        ++index1;
      }
      try
      {
        oleDbCommand.Connection = oleDbConnection;
        oleDbCommand.CommandText = str1;
        if (oleDbConnection.State == ConnectionState.Closed)
          oleDbConnection.Open();
        oleDbCommand.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      string str2 = "Insert into " + Table.TableName + " (";
      string str3 = " Values (";
      for (int index2 = 0; index2 < count; ++index2)
      {
        if (index2 + 1 == count)
        {
          str2 = str2 + Table.Columns[index2].ColumnName + ")";
          str3 = str3 + "@" + Table.Columns[index2].ColumnName + ")";
        }
        else
        {
          str2 = str2 + Table.Columns[index2].ColumnName + ",";
          str3 = str3 + "@" + Table.Columns[index2].ColumnName + ",";
        }
      }
      string str4 = str2 + str3;
      for (int index2 = 0; index2 < Table.Rows.Count; ++index2)
      {
        for (int index3 = 0; index3 < count; ++index3)
        {
          if (oleDbParameterArray[index3].DbType == DbType.Double && Table.Rows[index2][index3].ToString().Trim() == "")
            oleDbParameterArray[index3].Value = (object) 0;
          else
            oleDbParameterArray[index3].Value = (object) Table.Rows[index2][index3].ToString().Trim();
        }
        try
        {
          oleDbCommand.CommandText = str4;
          oleDbCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
      }
      try
      {
        if (oleDbConnection.State == ConnectionState.Open)
          oleDbConnection.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public static bool OutputToExcel(DataTable Table, ArrayList Columns, string ExcelFilePath)
    {
      if (File.Exists(ExcelFilePath))
        throw new Exception("该文件已经存在！");
      if (Columns.Count > Table.Columns.Count)
      {
        for (int index = Table.Columns.Count + 1; index <= Columns.Count; ++index)
          Columns.RemoveAt(index);
      }
      DataColumn dataColumn1 = new DataColumn();
      for (int index = 0; index < Columns.Count; ++index)
      {
        try
        {
          DataColumn column = (DataColumn) Columns[index];
        }
        catch (Exception ex)
        {
          Columns.RemoveAt(index);
        }
      }
      if (Table.TableName.Trim().Length == 0 || Table.TableName.ToLower() == "table")
        Table.TableName = "Sheet1";
      int count = Columns.Count;
      OleDbParameter[] oleDbParameterArray = new OleDbParameter[count];
      string str1 = "Create Table " + Table.TableName + "(";
      OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;");
      OleDbCommand oleDbCommand = new OleDbCommand();
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) "System.Decimal");
      arrayList.Add((object) "System.Double");
      arrayList.Add((object) "System.Int16");
      arrayList.Add((object) "System.Int32");
      arrayList.Add((object) "System.Int64");
      arrayList.Add((object) "System.Single");
      DataColumn dataColumn2 = new DataColumn();
      for (int index = 0; index < count; ++index)
      {
        DataColumn column = (DataColumn) Columns[index];
        if (arrayList.IndexOf((object) column.DataType.ToString().Trim()) >= 0)
        {
          oleDbParameterArray[index] = new OleDbParameter("@" + column.Caption.Trim(), OleDbType.Double);
          oleDbCommand.Parameters.Add(oleDbParameterArray[index]);
          str1 = index + 1 != count ? str1 + column.Caption.Trim() + " Double," : str1 + column.Caption.Trim() + " Double)";
        }
        else
        {
          oleDbParameterArray[index] = new OleDbParameter("@" + column.Caption.Trim(), OleDbType.VarChar);
          oleDbCommand.Parameters.Add(oleDbParameterArray[index]);
          str1 = index + 1 != count ? str1 + column.Caption.Trim() + " VarChar," : str1 + column.Caption.Trim() + " VarChar)";
        }
      }
      try
      {
        oleDbCommand.Connection = oleDbConnection;
        oleDbCommand.CommandText = str1;
        if (oleDbConnection.State == ConnectionState.Closed)
          oleDbConnection.Open();
        oleDbCommand.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      string str2 = "Insert into " + Table.TableName + " (";
      string str3 = " Values (";
      for (int index = 0; index < count; ++index)
      {
        if (index + 1 == count)
        {
          str2 = str2 + Columns[index].ToString().Trim() + ")";
          str3 = str3 + "@" + Columns[index].ToString().Trim() + ")";
        }
        else
        {
          str2 = str2 + Columns[index].ToString().Trim() + ",";
          str3 = str3 + "@" + Columns[index].ToString().Trim() + ",";
        }
      }
      string str4 = str2 + str3;
      DataColumn dataColumn3 = new DataColumn();
      for (int index1 = 0; index1 < Table.Rows.Count; ++index1)
      {
        for (int index2 = 0; index2 < count; ++index2)
        {
          DataColumn column = (DataColumn) Columns[index2];
          if (oleDbParameterArray[index2].DbType == DbType.Double && Table.Rows[index1][column.Caption].ToString().Trim() == "")
            oleDbParameterArray[index2].Value = (object) 0;
          else
            oleDbParameterArray[index2].Value = (object) Table.Rows[index1][column.Caption].ToString().Trim();
        }
        try
        {
          oleDbCommand.CommandText = str4;
          oleDbCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
      }
      try
      {
        if (oleDbConnection.State == ConnectionState.Open)
          oleDbConnection.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return true;
    }

    public static DataTable InputFromExcel(string ExcelFilePath, string TableName)
    {
      if (!File.Exists(ExcelFilePath))
        throw new Exception("Excel文件不存在！");
      ArrayList arrayList = new ArrayList();
      ArrayList excelTables = ExcelManage.GetExcelTables(ExcelFilePath);
      if (TableName.IndexOf(TableName) < 0)
        TableName = excelTables[0].ToString().Trim();
      DataTable dataTable = new DataTable();
      OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0");
      OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand("select * from [" + TableName + "$]", connection));
      try
      {
        if (connection.State == ConnectionState.Closed)
          connection.Open();
        oleDbDataAdapter.Fill(dataTable);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (connection.State == ConnectionState.Open)
          connection.Close();
      }
      return dataTable;
    }
  }
}
