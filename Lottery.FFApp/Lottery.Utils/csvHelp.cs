// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.csvHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Data;
using System.IO;
using System.Text;

namespace Lottery.Utils
{
  public static class csvHelp
  {
    public static bool dt2csv(DataTable dt, string strFilePath, string tableheader, string columname)
    {
      try
      {
        StreamWriter streamWriter = new StreamWriter(strFilePath, false, Encoding.UTF8);
        streamWriter.WriteLine(tableheader);
        streamWriter.WriteLine(columname);
        for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
        {
          string str = "";
          for (int index2 = 0; index2 < dt.Columns.Count; ++index2)
          {
            if (index2 > 0)
              str += ",";
            str += dt.Rows[index1][index2].ToString();
          }
          streamWriter.WriteLine(str);
        }
        streamWriter.Close();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static DataTable csv2dt(string filePath, int n, DataTable dt)
    {
      StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8, false);
      int num = 0;
      streamReader.Peek();
      while (streamReader.Peek() > 0)
      {
        ++num;
        string str = streamReader.ReadLine();
        if (num >= n + 1)
        {
          string[] strArray = str.Split(',');
          DataRow row = dt.NewRow();
          for (int index = 0; index < strArray.Length; ++index)
            row[index] = (object) strArray[index];
          dt.Rows.Add(row);
        }
      }
      return dt;
    }
  }
}
