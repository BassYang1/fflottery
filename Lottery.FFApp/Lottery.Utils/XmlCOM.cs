// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.XmlCOM
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Lottery.Utils
{
  public static class XmlCOM
  {
    public static DataSet ReadXml(string path)
    {
      DataSet dataSet = new DataSet();
      FileStream fileStream = (FileStream) null;
      StreamReader streamReader = (StreamReader) null;
      try
      {
        fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8);
        int num = (int) dataSet.ReadXml((TextReader) streamReader);
        return dataSet;
      }
      finally
      {
        fileStream.Close();
        streamReader.Close();
      }
    }

    public static string ReadConfig(string name, string key)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".config"));
      XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(key);
      if (elementsByTagName.Count == 0)
        return "";
      return elementsByTagName[0].InnerText;
    }

    public static void UpdateConfig(string name, string nKey, string nValue)
    {
      if (!(XmlCOM.ReadConfig(name, nKey) != ""))
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".config"));
      xmlDocument.GetElementsByTagName(nKey)[0].InnerText = nValue;
      XmlTextWriter xmlTextWriter = new XmlTextWriter((TextWriter) new StreamWriter(HttpContext.Current.Server.MapPath(name + ".config")));
      xmlTextWriter.Formatting = Formatting.Indented;
      xmlDocument.WriteTo((XmlWriter) xmlTextWriter);
      xmlTextWriter.Close();
    }

    public static string ReadXml(string name, string key)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(HttpContext.Current.Server.MapPath(name + ".xml"));
      XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName(key);
      if (elementsByTagName.Count == 0)
        return "";
      return elementsByTagName[0].InnerText;
    }
  }
}
