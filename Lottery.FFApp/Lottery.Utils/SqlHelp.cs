// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.SqlHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Collections.Specialized;

namespace Lottery.Utils
{
  public class SqlHelp
  {
    public static string GetSql1(string SelectFields, string TblName, int TotalCount, int PageSize, int PageIndex, NameValueCollection Order, string whereStr)
    {
      string str1 = "";
      string str2 = "";
      string str3 = "";
      if (Order.Count > 0)
      {
        foreach (string allKey in Order.AllKeys)
        {
          string str4 = "asc";
          if (Order[allKey].ToString() == "asc")
            str4 = "desc";
          int startIndex = allKey.IndexOf(".") + 1;
          string str5 = allKey.Substring(startIndex);
          str1 = str1 + allKey + " " + Order[allKey] + ",";
          str2 = str2 + str5 + " " + str4 + ",";
          str3 = str3 + str5 + " " + Order[allKey] + ",";
        }
        str1 = str1.Substring(0, str1.Length - 1);
        str2 = str2.Substring(0, str2.Length - 1);
        str3 = str3.Substring(0, str3.Length - 1);
      }
      string str6;
      if (TotalCount > 0 && TotalCount % PageSize > 0 && PageIndex > TotalCount / PageSize)
      {
        string format = "select * from ( select top {5} {0} from {1} ";
        if (whereStr != "")
          format += " where {2} ";
        if (str1 != "")
          format += " order by {4})  as tmp order by {3}";
        str6 = string.Format(format, (object) SelectFields, (object) TblName, (object) whereStr, (object) str1, (object) str2, (object) (TotalCount % PageSize));
      }
      else
      {
        string format = "select * from ( select top {7} * from ( select top {6} {0} from {1} ";
        if (whereStr != "")
          format += " where {2} ";
        if (str1 != "")
          format += " order by {3} ) as tmp order by {4} ) as tmp2 order by {5} ";
        str6 = string.Format(format, (object) SelectFields, (object) TblName, (object) whereStr, (object) str1, (object) str2, (object) str3, (object) (PageIndex * PageSize), (object) PageSize);
      }
      return str6;
    }

    public static string GetSql0(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
    {
      string str1;
      string str2;
      if (OrderType.ToUpper() == "ASC")
      {
        str1 = "> (SELECT MAX(" + FldName + ")";
        str2 = " ORDER BY " + FldName + " ASC";
      }
      else
      {
        str1 = "< (SELECT MIN(" + FldName + ")";
        str2 = " ORDER BY " + FldName + " DESC";
      }
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      string str3;
      if (PageIndex == 1)
      {
        string str4 = "";
        if (whereStr != "")
          str4 = " Where " + whereStr;
        str3 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " From " + TblName + " with(nolock) " + str4 + str2;
      }
      else
      {
        string str4 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " From " + TblName + " with(nolock)  WHERE " + FldName + str1 + " From (SELECT TOP " + (object) ((PageIndex - 1) * PageSize) + " " + FldName + " From " + TblName + " with(nolock) ";
        if (whereStr != "")
          str4 = str4 + " Where " + whereStr;
        string str5 = str4 + str2 + ") As Tbltemp)";
        if (whereStr != "")
          str5 = str5 + " And " + whereStr;
        str3 = str5 + str2;
      }
      return str3;
    }

    public static string GetSql0(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr, string groupStr)
    {
      string str1;
      string str2;
      if (OrderType.ToUpper() == "ASC")
      {
        str1 = "> (SELECT MAX(" + FldName + ")";
        str2 = " ORDER BY " + FldName + " ASC";
      }
      else
      {
        str1 = "< (SELECT MIN(" + FldName + ")";
        str2 = " ORDER BY " + FldName + " DESC";
      }
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      string str3;
      if (PageIndex == 1)
      {
        string str4 = "";
        if (whereStr != "")
          str4 = " Where " + whereStr;
        str3 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " From " + TblName + " with(nolock) " + str4 + " group by " + groupStr + " " + str2;
      }
      else
      {
        string str4 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " From " + TblName + " with(nolock) WHERE " + FldName + str1 + " From (SELECT TOP " + (object) ((PageIndex - 1) * PageSize) + " " + FldName + " From " + TblName + " with(nolock) ";
        if (whereStr != "")
          str4 = str4 + " Where " + whereStr;
        string str5 = str4 + " group by " + groupStr + " " + str2 + ") As Tbltemp)";
        if (whereStr != "")
          str5 = str5 + " And " + whereStr;
        str3 = str5 + " group by " + groupStr + " " + str2;
      }
      return str3;
    }

    public static string GetSql0(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2)
    {
      string str1;
      string str2;
      string str3;
      if (OrderType.ToUpper() == "ASC")
      {
        str1 = "> (SELECT MAX(" + FldName + ")";
        str2 = " ORDER BY A." + FldName + " ASC";
        str3 = " ORDER BY " + FldName + " ASC";
      }
      else
      {
        str1 = "< (SELECT MIN(" + FldName + ")";
        str2 = " ORDER BY A." + FldName + " DESC";
        str3 = " ORDER BY " + FldName + " DESC";
      }
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      string str4;
      if (PageIndex == 1)
      {
        string str5 = "";
        if (whereStr1 != "")
          str5 = " WHERE " + whereStr1;
        str4 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A  with(nolock) LEFT JOIN [" + TblNameB + "] B  with(nolock) on " + joinStr + " " + str5 + str2;
      }
      else
      {
        string str5 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A  with(nolock) LEFT JOIN [" + TblNameB + "] B  with(nolock) on " + joinStr + " WHERE A." + FldName + str1 + " From (SELECT TOP " + (object) ((PageIndex - 1) * PageSize) + " " + FldName + " From [" + TblNameA + "] ";
        if (whereStr2 != "")
          str5 = str5 + " Where " + whereStr2;
        string str6 = str5 + str3 + ") As Tbltemp)";
        if (whereStr1 != "")
          str6 = str6 + " And " + whereStr1;
        str4 = str6 + str2;
      }
      return str4;
    }

    public static string GetSql0(string SelectFields, string TblNameA, string TblNameB, string FldName, int PageSize, int PageIndex, string OrderType, string joinStr, string whereStr1, string whereStr2, string groupStr)
    {
      string str1;
      string str2;
      string str3;
      if (OrderType.ToUpper() == "ASC")
      {
        str1 = "> (SELECT MAX(" + FldName + ")";
        str2 = " ORDER BY A." + FldName + " ASC";
        str3 = " ORDER BY " + FldName + " ASC";
      }
      else
      {
        str1 = "< (SELECT MIN(" + FldName + ")";
        str2 = " ORDER BY A." + FldName + " DESC";
        str3 = " ORDER BY " + FldName + " DESC";
      }
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      string str4;
      if (PageIndex == 1)
      {
        string str5 = "";
        if (whereStr1 != "")
          str5 = " WHERE " + whereStr1;
        str4 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A  with(nolock) LEFT JOIN [" + TblNameB + "] B  with(nolock) on " + joinStr + " " + str5 + " group by " + groupStr + " " + str2;
      }
      else
      {
        string str5 = "SELECT TOP " + (object) PageSize + " " + SelectFields + " FROM [" + TblNameA + "] A  with(nolock) LEFT JOIN [" + TblNameB + "] B  with(nolock) on " + joinStr + " WHERE A." + FldName + str1 + " From (SELECT TOP " + (object) ((PageIndex - 1) * PageSize) + " " + FldName + " From [" + TblNameA + "] ";
        if (whereStr2 != "")
          str5 = str5 + " Where " + whereStr2;
        string str6 = str5 + str3 + ") As Tbltemp)";
        if (whereStr1 != "")
          str6 = str6 + " And " + whereStr1;
        str4 = str6 + " group by " + groupStr + " " + str2;
      }
      return str4;
    }

    public static string GetSqlRow(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr)
    {
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      return "SELECT TOP (" + (object) PageSize + ") * FROM (SELECT top(1000) ROW_NUMBER() OVER (ORDER BY " + FldName + " " + OrderType + ") AS rowNember," + SelectFields + " FROM " + TblName + " where " + whereStr + " order by rowNember) as t WHERE t.rowNember > (" + (object) PageSize + "*(" + (object) PageIndex + "-1)) order by rowNember;";
    }

    public static string GetSqlRow(string SelectFields, string TblName, string FldName, int PageSize, int PageIndex, string OrderType, string whereStr, string groupStr)
    {
      PageIndex = Validator.StrToInt(PageIndex.ToString(), 0);
      PageIndex = PageIndex == 0 ? 1 : PageIndex;
      return "SELECT TOP (" + (object) PageSize + ") * FROM (SELECT top(1000) ROW_NUMBER() OVER (ORDER BY " + FldName + " " + OrderType + ") AS rowNember," + SelectFields + " FROM " + TblName + " where " + whereStr + " group by " + groupStr + " order by rowNember) as t WHERE t.rowNember > (" + (object) PageSize + "*(" + (object) PageIndex + "-1)) order by rowNember;";
    }
  }
}
