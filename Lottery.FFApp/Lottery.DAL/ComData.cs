// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.ComData
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace Lottery.DAL
{
  public class ComData
  {
    public static string connectionString = Const.ConnectionString;

    public DbOperHandler Doh()
    {
      return (DbOperHandler) new SqlDbOperHandler(new SqlConnection(ComData.connectionString));
    }

    public string GetJsonResult(int result, string Message)
    {
      return "[{\"result\":\"" + (object) result + "\",\"message\":\"" + Message + "\"}]";
    }

    public string JsonResult(int result, string Message)
    {
      return "{\"result\":\"" + (object) result + "\",\"message\":\"" + Message + "\"}";
    }

    public string AddZero(int Num, int Len)
    {
      string str1 = "";
      for (int index = 1; index <= Len; ++index)
        str1 += "0";
      string str2 = str1 + Num.ToString();
      return str2.Substring(str2.Length - Len);
    }

    public static DataTable LotteryTime { get; set; }

    protected string ConverTableToJSON(DataTable table)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[");
      if (table != null && table.Rows.Count > 0)
      {
        for (int index1 = 0; index1 < table.Rows.Count; ++index1)
        {
          if (index1 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          for (int index2 = 0; index2 < table.Columns.Count; ++index2)
          {
            if (!string.IsNullOrEmpty(string.Concat(table.Rows[index1][index2])))
            {
              if (index2 > 0)
                stringBuilder.Append(",");
              stringBuilder.Append("\"" + table.Columns[index2].ColumnName.ToLower() + "\":\"");
              stringBuilder.Append(table.Rows[index1][index2].ToString() + "\"");
            }
          }
          stringBuilder.Append("}");
        }
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    protected string ConverTableToJSON2(DataTable table)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[");
      if (table != null && table.Rows.Count > 0)
      {
        for (int index1 = 0; index1 < table.Rows.Count; ++index1)
        {
          if (index1 > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("{");
          for (int index2 = 0; index2 < table.Columns.Count; ++index2)
          {
            if (!string.IsNullOrEmpty(string.Concat(table.Rows[index1][index2])))
            {
              if (index2 > 0)
                stringBuilder.Append(",");
              stringBuilder.Append("\"" + table.Columns[index2].ColumnName.ToLower() + "\":\"");
              stringBuilder.Append(ComData.NoHTML(table.Rows[index1][index2].ToString()) + "\"");
            }
          }
          stringBuilder.Append("}");
        }
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    protected string ConverTableToLotteryXML(DataTable dt)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      stringBuilder.Append("<xml rows=\"5\" code=\"ssc\" remain=\"10hrs\">");
      if (dt != null && dt.Rows.Count > 0)
      {
        for (int index = 0; index < dt.Rows.Count; ++index)
          stringBuilder.Append("<row expect=\"" + dt.Rows[index]["Title"] + "\" opencode=\"" + dt.Rows[index]["NumberAll"] + "\" opentime=\"" + dt.Rows[index]["STime"] + "\" />");
      }
      stringBuilder.Append("</xml>");
      return stringBuilder.ToString();
    }

    protected string ConverTableToXML(DataTable dt1, DataTable dt2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      stringBuilder.Append("<lotterys>");
      if (dt1 != null && dt1.Rows.Count > 0)
      {
        for (int index1 = 0; index1 < dt1.Rows.Count; ++index1)
        {
          stringBuilder.Append("<lottery>");
          for (int index2 = 0; index2 < dt1.Columns.Count; ++index2)
          {
            if (!string.IsNullOrEmpty(string.Concat(dt1.Rows[index1][index2])))
            {
              stringBuilder.Append("<" + dt1.Columns[index2].ColumnName.ToLower() + ">");
              stringBuilder.Append(dt1.Rows[index1][index2].ToString().Replace("\n", "").Replace(" ", ""));
              stringBuilder.Append("</" + dt1.Columns[index2].ColumnName.ToLower() + ">");
            }
          }
          DataRow[] dataRowArray = dt2.Select("Radio=" + dt1.Rows[index1]["Id"].ToString(), "Id asc");
          stringBuilder.Append("<plays>");
          for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
          {
            stringBuilder.Append("<play>");
            for (int index3 = 0; index3 < dataRowArray[index2].ItemArray.Length; ++index3)
            {
              if (!string.IsNullOrEmpty(string.Concat(dataRowArray[index2].ItemArray[index3])))
              {
                stringBuilder.Append("<" + dt2.Columns[index3].ColumnName.ToLower() + ">");
                stringBuilder.Append(dataRowArray[index2].ItemArray[index3].ToString().Replace("\n", "").Replace(" ", ""));
                stringBuilder.Append("</" + dt2.Columns[index3].ColumnName.ToLower() + ">");
              }
            }
            stringBuilder.Append("</play>");
          }
          stringBuilder.Append("</plays>");
          stringBuilder.Append("</lottery>");
        }
      }
      stringBuilder.Append("</lotterys>");
      return stringBuilder.ToString();
    }

    protected string ConverTableToXML(DataTable dt)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
      stringBuilder.Append("<lotterys>");
      if (dt != null && dt.Rows.Count > 0)
      {
        for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
        {
          stringBuilder.Append("<lottery>");
          for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
          {
            if (!string.IsNullOrEmpty(string.Concat(dt.Rows[index1][index2])))
            {
              stringBuilder.Append("<" + dt.Columns[index2].ColumnName.ToLower() + ">");
              stringBuilder.Append(dt.Rows[index1][index2].ToString().Replace("\n", "").Replace(" ", ""));
              stringBuilder.Append("</" + dt.Columns[index2].ColumnName.ToLower() + ">");
            }
          }
          stringBuilder.Append("</lottery>");
        }
      }
      stringBuilder.Append("</lotterys>");
      return stringBuilder.ToString();
    }

    protected string ConvertDataTableToXML(DataTable xmlDS)
    {
      XmlTextWriter xmlTextWriter = (XmlTextWriter) null;
      try
      {
        MemoryStream memoryStream = new MemoryStream();
        xmlTextWriter = new XmlTextWriter((Stream) memoryStream, Encoding.Default);
        xmlDS.WriteXml((XmlWriter) xmlTextWriter);
        int length = (int) memoryStream.Length;
        byte[] numArray = new byte[length];
        memoryStream.Seek(0L, SeekOrigin.Begin);
        memoryStream.Read(numArray, 0, length);
        return new UTF8Encoding().GetString(numArray).Trim();
      }
      catch
      {
        return string.Empty;
      }
      finally
      {
        if (xmlTextWriter != null)
          xmlTextWriter.Close();
      }
    }

    protected string CDataToXml(DataTable dt)
    {
      if (dt == null)
        return "";
      MemoryStream memoryStream = (MemoryStream) null;
      XmlTextWriter xmlTextWriter = (XmlTextWriter) null;
      try
      {
        memoryStream = new MemoryStream();
        xmlTextWriter = new XmlTextWriter((Stream) memoryStream, Encoding.Unicode);
        dt.WriteXml((XmlWriter) xmlTextWriter);
        int length = (int) memoryStream.Length;
        byte[] numArray = new byte[length];
        memoryStream.Seek(0L, SeekOrigin.Begin);
        memoryStream.Read(numArray, 0, length);
        return new UnicodeEncoding().GetString(numArray).Trim();
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (xmlTextWriter != null)
        {
          xmlTextWriter.Close();
          memoryStream.Close();
          memoryStream.Dispose();
        }
      }
    }

    private DataSet ConvertXMLToDataSet(string xmlData)
    {
      XmlTextReader xmlTextReader = (XmlTextReader) null;
      try
      {
        DataSet dataSet = new DataSet();
        xmlTextReader = new XmlTextReader((TextReader) new StringReader(xmlData));
        int num = (int) dataSet.ReadXml((XmlReader) xmlTextReader);
        return dataSet;
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        return (DataSet) null;
      }
      finally
      {
        if (xmlTextReader != null)
          xmlTextReader.Close();
      }
    }

    public static string NoHTML(string Htmlstring)
    {
      Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
      Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
      Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
      return Htmlstring;
    }
  }
}
