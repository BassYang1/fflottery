// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.conNumber
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System.Configuration;

namespace Lottery.DAL
{
  public class conNumber : ComData
  {
    public void Create(int loid, string title, string number)
    {
      XmlControl xmlControl = new XmlControl(ConfigurationManager.AppSettings["DataUrl"].ToString() + (object) loid + ".xml");
      xmlControl.Update("Root/Title", title);
      xmlControl.Update("Root/Number", number);
      xmlControl.Save();
      xmlControl.Dispose();
    }
  }
}
