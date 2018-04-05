// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.ExecuteSqlBlock
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Lottery.Utils
{
  public static class ExecuteSqlBlock
  {
    public static bool Go(string dbType, string connectionString, string pathToScriptFile)
    {
      StreamReader _reader = (StreamReader) null;
      Stream stream = (Stream) null;
      if (!File.Exists(pathToScriptFile))
        return false;
      try
      {
        stream = (Stream) File.OpenRead(pathToScriptFile);
        _reader = new StreamReader(stream, Encoding.UTF8);
        if (dbType == "0")
        {
          using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
          {
            using (OleDbCommand oleDbCommand = new OleDbCommand())
            {
              oleDbConnection.Open();
              oleDbCommand.Connection = oleDbConnection;
              oleDbCommand.CommandType = CommandType.Text;
              string str;
              while ((str = ExecuteSqlBlock.ReadNextStatementFromStream(_reader)) != null)
              {
                oleDbCommand.CommandText = str;
                oleDbCommand.ExecuteNonQuery();
              }
            }
          }
        }
        else
        {
          using (SqlConnection sqlConnection = new SqlConnection(connectionString))
          {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
              sqlConnection.Open();
              sqlCommand.Connection = sqlConnection;
              sqlCommand.CommandTimeout = 180;
              sqlCommand.CommandType = CommandType.Text;
              string str;
              while ((str = ExecuteSqlBlock.ReadNextStatementFromStream(_reader)) != null)
              {
                sqlCommand.CommandText = str;
                sqlCommand.ExecuteNonQuery();
              }
            }
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
      finally
      {
        _reader.Close();
        _reader.Dispose();
        stream.Close();
        stream.Dispose();
      }
    }

    private static string ReadNextStatementFromStream(StreamReader _reader)
    {
      StringBuilder stringBuilder = new StringBuilder();
      while (true)
      {
        string str = _reader.ReadLine();
        if (str != null)
        {
          if (!(str.TrimEnd().ToUpper() == "GO"))
            stringBuilder.AppendFormat("{0}\r\n", (object) str);
          else
            goto label_7;
        }
        else
          break;
      }
      if (stringBuilder.Length > 0)
        return stringBuilder.ToString();
      return (string) null;
label_7:
      return stringBuilder.ToString();
    }
  }
}
