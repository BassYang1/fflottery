// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.conPlayType
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace Lottery.DAL
{
  public class conPlayType : ComData
  {
    public void Create(int typeId, string url)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT [Id],[TypeId],[Title] FROM [Sys_PlayBigType] where TypeId=" + (object) typeId + " and IsOpen=0 order by sort ";
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT [Id],[Type],[Title0],[Title],[Title2],[Radio] FROM [Sys_PlaySmallType] where IsOpen=0 and flag=0 order by Id ";
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        this.SaveJsFile(this.ConverTableToXML(dataTable1, dataTable2), HttpContext.Current.Server.MapPath(url), "2");
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
