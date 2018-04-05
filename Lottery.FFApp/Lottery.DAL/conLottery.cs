// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.conLottery
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System.IO;
using System.Text;
using System.Web;

namespace Lottery.DAL
{
  public class conLottery : ComData
  {
    public void Create(string url)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT [Id],[Title],[Code],[Sort],[IndexType] FROM [Sys_Lottery] where IsOpen=0 order by Sort ";
        this.SaveJsFile(this.ConverTableToXML(dbOperHandler.GetDataTable()), HttpContext.Current.Server.MapPath(url), "2");
      }
    }

    protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
    {
      Encoding encoding = Encoding.Default;
      switch (Edcode)
      {
        case "1":
          encoding = Encoding.GetEncoding("GB2312");
          break;
        case "2":
          encoding = Encoding.UTF8;
          break;
        case "3":
          encoding = Encoding.Unicode;
          break;
      }
      DirFile.CreateFolder(DirFile.GetFolderPath(false, TxtFile));
      StreamWriter streamWriter = new StreamWriter(TxtFile, false, encoding);
      streamWriter.Write(TxtStr);
      streamWriter.Close();
    }
  }
}
