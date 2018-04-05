// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.fastJSON.JSONSerializer
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;

namespace Lottery.Utils.fastJSON
{
  internal class JSONSerializer
  {
    private readonly StringBuilder _output = new StringBuilder();

    public static string ToJSON(object obj)
    {
      return new JSONSerializer().ConvertToJSON(obj);
    }

    internal string ConvertToJSON(object obj)
    {
      this.WriteValue(obj);
      return this._output.ToString();
    }

    private void WriteValue(object obj)
    {
      if (obj == null)
        this._output.Append("null");
      else if (obj is sbyte || obj is byte || (obj is short || obj is ushort) || (obj is int || obj is uint || (obj is long || obj is ulong)) || (obj is Decimal || obj is double || obj is float))
        this._output.Append(Convert.ToString(obj, (IFormatProvider) NumberFormatInfo.InvariantInfo));
      else if (obj is bool)
        this._output.Append(obj.ToString().ToLower());
      else if (obj is char || obj is Enum || (obj is Guid || obj is string))
        this.WriteString(obj.ToString());
      else if (obj is DateTime)
      {
        this._output.Append("\"");
        this._output.Append(((DateTime) obj).ToString("yyyy-MM-dd HH:mm:ss"));
        this._output.Append("\"");
      }
      else if (obj is DataSet)
        this.WriteDataset((DataSet) obj);
      else if (obj is byte[])
        this.WriteByteArray((byte[]) obj);
      else if (obj is IDictionary)
        this.WriteDictionary((IDictionary) obj);
      else if (obj is Array || obj is IList || obj is ICollection)
        this.WriteArray((IEnumerable) obj);
      else
        this.WriteObject(obj);
    }

    private void WriteByteArray(byte[] bytes)
    {
      this.WriteString(Convert.ToBase64String(bytes));
    }

    private void WriteDataset(DataSet ds)
    {
      this._output.Append("{");
      this.WritePair("$schema", ds.GetXmlSchema());
      this._output.Append(",");
      foreach (DataTable table in (InternalDataCollectionBase) ds.Tables)
      {
        this._output.Append("\"");
        this._output.Append(table.TableName);
        this._output.Append("\":[");
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          this._output.Append("{");
          foreach (DataColumn column in (InternalDataCollectionBase) row.Table.Columns)
            this.WritePair(column.ColumnName, row[column]);
          this._output.Append("}");
        }
        this._output.Append("]");
      }
      this._output.Append("}");
    }

    private void WriteObject(object obj)
    {
      this._output.Append("{");
      Type type = obj.GetType();
      this.WritePair("$type", type.AssemblyQualifiedName);
      this._output.Append(",");
      foreach (Getters getter in JSON.Instance.GetGetters(type))
        this.WritePair(getter.Name, getter.Getter(obj));
      this._output.Append("}");
    }

    private void WritePair(string name, string value)
    {
      this.WriteString(name);
      this._output.Append(":");
      this.WriteString(value);
    }

    private void WritePair(string name, object value)
    {
      this.WriteString(name);
      this._output.Append(":");
      this.WriteValue(value);
    }

    private void WriteArray(IEnumerable array)
    {
      this._output.Append("[");
      bool flag = false;
      foreach (object obj in array)
      {
        if (flag)
          this._output.Append(',');
        this.WriteValue(obj);
        flag = true;
      }
      this._output.Append("]");
    }

    private void WriteDictionary(IDictionary dic)
    {
      this._output.Append("[");
      bool flag = false;
      foreach (DictionaryEntry dictionaryEntry in dic)
      {
        if (flag)
          this._output.Append(",");
        this._output.Append("{");
        this.WritePair("k", dictionaryEntry.Key);
        this._output.Append(",");
        this.WritePair("v", dictionaryEntry.Value);
        this._output.Append("}");
        flag = true;
      }
      this._output.Append("]");
    }

    private void WriteString(string s)
    {
      this._output.Append('"');
      foreach (char ch in s)
      {
        switch (ch)
        {
          case '\t':
            this._output.Append("\\t");
            break;
          case '\n':
            this._output.Append("\\n");
            break;
          case '\r':
            this._output.Append("\\r");
            break;
          case '"':
          case '\\':
            this._output.Append("\\");
            this._output.Append(ch);
            break;
          default:
            if ((int) ch >= 32 && (int) ch < 128)
            {
              this._output.Append(ch);
              break;
            }
            this._output.Append("\\u");
            this._output.Append(((int) ch).ToString("X4"));
            break;
        }
      }
      this._output.Append('"');
    }
  }
}
