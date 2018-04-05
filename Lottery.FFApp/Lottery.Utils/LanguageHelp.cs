// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.LanguageHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using Lottery.Utils.fastJSON;
using System.Collections.Generic;

namespace Lottery.Utils
{
  public class LanguageHelp
  {
    public Dictionary<string, object> GetEntity(string _lng)
    {
      string html = Strings.GetHtml(DirFile.ReadFile("~/data/languages/" + _lng + ".js"), "//<!--语言包begin", "//-->语言包end");
      return (Dictionary<string, object>) JSON.Instance.ToObject(html);
    }
  }
}
