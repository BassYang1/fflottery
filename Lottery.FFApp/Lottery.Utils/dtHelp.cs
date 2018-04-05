// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.dtHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Lottery.Utils
{
  public static class dtHelp
  {
    public static string DT2JSON(DataTable dt)
    {
      return dtHelp.DT2JSON(dt, 0, "recordcount", "table");
    }

    public static string DT2JSON(DataTable dt, int fromCount)
    {
      return dtHelp.DT2JSON(dt, fromCount, "recordcount", "table");
    }

    public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname)
    {
      return dtHelp.DT2JSON(dt, fromCount, "recordcount", "table", true);
    }

    public static string DT2JSON(DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"" + totalCountStr + "\":" + (object) dt.Rows.Count + ",\"" + tbname + "\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (fromCount + index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSON123(DataTable dtsum, DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"" + totalCountStr + "\":" + (object) dt.Rows.Count + ",\"" + tbname + "\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (fromCount + index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append("}");
      }
      stringBuilder.Append(",");
      stringBuilder.Append("{");
      stringBuilder.Append("\"no\":111,");
      for (int index = 0; index < dt.Columns.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(",");
        if (dt.Columns[index].DataType.Equals(typeof (Decimal)))
          stringBuilder.Append("\"" + dt.Columns[index].ColumnName.ToLower() + "\": \"" + dt.Compute("sum(" + dt.Columns[index].ColumnName.ToLower() + ")", "true").ToString() + "\"");
        else if (dt.Columns[index].DataType.Equals(typeof (string)))
          stringBuilder.Append("\"" + dt.Columns[index].ColumnName.ToLower() + "\": \"本页合计\"");
        else
          stringBuilder.Append("\"" + dt.Columns[index].ColumnName.ToLower() + "\": \"0\"");
      }
      stringBuilder.Append("}");
      stringBuilder.Append(",");
      stringBuilder.Append("{");
      stringBuilder.Append("\"no\":112,");
      for (int index = 0; index < dtsum.Columns.Count; ++index)
      {
        if (index > 0)
          stringBuilder.Append(",");
        if (dtsum.Columns[index].DataType.Equals(typeof (Decimal)))
          stringBuilder.Append("\"" + dtsum.Columns[index].ColumnName.ToLower() + "\": \"" + dtsum.Compute("sum(" + dt.Columns[index].ColumnName.ToLower() + ")", "true").ToString() + "\"");
        else if (dtsum.Columns[index].DataType.Equals(typeof (string)))
          stringBuilder.Append("\"" + dtsum.Columns[index].ColumnName.ToLower() + "\": \"全部合计\"");
        else
          stringBuilder.Append("\"" + dtsum.Columns[index].ColumnName.ToLower() + "\": \"0\"");
      }
      stringBuilder.Append("}");
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static List<T> DT2List<T>(DataTable dt)
    {
      if (dt == null)
        return (List<T>) null;
      List<T> objList = new List<T>();
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        T instance = (T) Activator.CreateInstance(typeof (T));
        foreach (PropertyInfo property in instance.GetType().GetProperties())
        {
          for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
          {
            if (property.Name.ToLower().Equals(dt.Columns[index2].ColumnName.ToLower()))
            {
              if (dt.Rows[index1][index2] != DBNull.Value)
              {
                if (property.PropertyType.ToString() == "System.Int32")
                {
                  property.SetValue((object) instance, (object) int.Parse(dt.Rows[index1][index2].ToString()), (object[]) null);
                  break;
                }
                if (property.PropertyType.ToString() == "System.DateTime")
                {
                  property.SetValue((object) instance, (object) Convert.ToDateTime(dt.Rows[index1][index2].ToString()), (object[]) null);
                  break;
                }
                if (property.PropertyType.ToString() == "System.Boolean")
                {
                  property.SetValue((object) instance, (object) Convert.ToBoolean(dt.Rows[index1][index2].ToString()), (object[]) null);
                  break;
                }
                if (property.PropertyType.ToString() == "System.Single")
                {
                  property.SetValue((object) instance, (object) Convert.ToSingle(dt.Rows[index1][index2].ToString()), (object[]) null);
                  break;
                }
                if (property.PropertyType.ToString() == "System.Double")
                {
                  property.SetValue((object) instance, (object) Convert.ToDouble(dt.Rows[index1][index2].ToString()), (object[]) null);
                  break;
                }
                property.SetValue((object) instance, (object) dt.Rows[index1][index2].ToString(), (object[]) null);
                break;
              }
              property.SetValue((object) instance, (object) "", (object[]) null);
              break;
            }
          }
        }
        objList.Add(instance);
      }
      return objList;
    }

    public static string DT2JSONNOHTML(DataTable dt, int fromCount, string totalCountStr, string tbname, bool formatData)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"" + totalCountStr + "\":" + (object) dt.Rows.Count + ",\"" + tbname + "\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (fromCount + index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dtHelp.checkStr(dt.Rows[index1][index2].ToString()) + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dtHelp.checkStr(dt.Rows[index1][index2].ToString()) + "\"");
        }
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string checkStr(string Htmlstring)
    {
      return new Regex("<.*?>", RegexOptions.Compiled).Replace(Htmlstring, string.Empty).Replace("\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace("\t", "");
    }

    public static string DT2JSON(DataTable dt, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray = dt2.Select("Radio=" + dt.Rows[index1]["Id"].ToString(), "Sort asc");
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSON2(DataTable dt, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray = dt2.Select("LotteryId=" + dt.Rows[index1]["Ltype"].ToString(), "Id asc");
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSON3(DataTable dt, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray = dt2.Select("Ltype=" + dt.Rows[index1]["Ltype"].ToString(), "Id asc");
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSON33(DataTable dt, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray = dt2.Select("TypeId=" + dt.Rows[index1]["Ltype"].ToString(), "Id asc");
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSON3Table(DataTable dt, DataTable dt2, DataTable dt3)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{\"result\" :\"1\",\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray1 = dt2.Select("TypeId=" + dt.Rows[index1]["Ltype"].ToString(), "Sort asc");
        for (int index2 = 0; index2 < dataRowArray1.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray1[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray1[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray1[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray1[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append(",\"table3\": [");
          DataRow[] dataRowArray2 = dt3.Select("Radio=" + dt2.Rows[index2]["Id"].ToString(), "Sort asc");
          for (int index3 = 0; index3 < dataRowArray2.Length; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            stringBuilder.Append("{");
            stringBuilder.Append("\"no\":" + (object) (index3 + 1) + ",");
            for (int index4 = 0; index4 < dt3.Columns.Count; ++index4)
            {
              if (index4 > 0)
                stringBuilder.Append(",");
              if (dt3.Columns[index4].DataType.Equals(typeof (DateTime)) && dataRowArray2[index3][index4].ToString() != "")
                stringBuilder.Append("\"" + dt3.Columns[index4].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray2[index3][index4].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
              else if (dt3.Columns[index4].DataType.Equals(typeof (string)))
                stringBuilder.Append("\"" + dt3.Columns[index4].ColumnName.ToLower() + "\": \"" + dataRowArray2[index3][index4].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
              else
                stringBuilder.Append("\"" + dt3.Columns[index4].ColumnName.ToLower() + "\": \"" + dataRowArray2[index3][index4].ToString() + "\"");
            }
            stringBuilder.Append("}");
          }
          stringBuilder.Append("]}");
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]}");
      }
      stringBuilder.Append("]}");
      return stringBuilder.ToString();
    }

    public static string DT2JSONAIR(DataTable dt, int fromCount)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("'no':'" + (object) (fromCount + index1 + 1) + "',");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("'" + dt.Columns[index2].ColumnName.ToLower() + "': '" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "'");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("'" + dt.Columns[index2].ColumnName.ToLower() + "': '" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "'");
          else
            stringBuilder.Append("'" + dt.Columns[index2].ColumnName.ToLower() + "': '" + dt.Rows[index1][index2].ToString() + "'");
        }
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    public static string DT2JSONAdminLeft(DataTable dt, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\"total\":" + (object) dt.Rows.Count + ",\"table\": [");
      for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{");
        stringBuilder.Append("\"no\":" + (object) (index1 + 1) + ",");
        for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          if (dt.Columns[index2].DataType.Equals(typeof (DateTime)) && dt.Rows[index1][index2].ToString() != "")
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dt.Rows[index1][index2].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
          else if (dt.Columns[index2].DataType.Equals(typeof (string)))
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
          else
            stringBuilder.Append("\"" + dt.Columns[index2].ColumnName.ToLower() + "\": \"" + dt.Rows[index1][index2].ToString() + "\"");
        }
        stringBuilder.Append(",\"table2\": [");
        DataRow[] dataRowArray = dt2.Select("Pid=" + dt.Rows[index1]["Id"].ToString(), "Sort asc");
        for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
        {
          if (index2 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          stringBuilder.Append("\"no\":" + (object) (index2 + 1) + ",");
          for (int index3 = 0; index3 < dt2.Columns.Count; ++index3)
          {
            if (index3 > 0)
              stringBuilder.Append(",");
            if (dt2.Columns[index3].DataType.Equals(typeof (DateTime)) && dataRowArray[index2][index3].ToString() != "")
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + Convert.ToDateTime(dataRowArray[index2][index3].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\"");
            else if (dt2.Columns[index3].DataType.Equals(typeof (string)))
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString().Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\t", " ").Replace("\r", " ").Replace("\n", "<br/>") + "\"");
            else
              stringBuilder.Append("\"" + dt2.Columns[index3].ColumnName.ToLower() + "\": \"" + dataRowArray[index2][index3].ToString() + "\"");
          }
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("}");
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }
  }
}
