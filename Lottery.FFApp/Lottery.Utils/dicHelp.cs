// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.dicHelp
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections.Generic;

namespace Lottery.Utils
{
  public static class dicHelp
  {
    public static void Order(ref Dictionary<string, string> dic, int type)
    {
      if (dic == null || dic.Count < 1)
        return;
      List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>) dic);
      switch (type)
      {
        case 0:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, string>>) ((s1, s2) => s1.Value.CompareTo(s2.Value)));
          break;
        case 1:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, string>>) ((s1, s2) => s2.Value.CompareTo(s1.Value)));
          break;
        case 2:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, string>>) ((s1, s2) => s1.Key.CompareTo(s2.Key)));
          break;
        default:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, string>>) ((s1, s2) => s2.Key.CompareTo(s1.Key)));
          break;
      }
      dic.Clear();
      foreach (KeyValuePair<string, string> keyValuePair in keyValuePairList)
        dic.Add(keyValuePair.Key, keyValuePair.Value);
    }

    public static void Order(ref Dictionary<string, int> dic, int type)
    {
      if (dic == null || dic.Count < 1)
        return;
      List<KeyValuePair<string, int>> keyValuePairList = new List<KeyValuePair<string, int>>((IEnumerable<KeyValuePair<string, int>>) dic);
      switch (type)
      {
        case 0:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, int>>) ((s1, s2) => s1.Value.CompareTo(s2.Value)));
          break;
        case 1:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, int>>) ((s1, s2) => s2.Value.CompareTo(s1.Value)));
          break;
        case 2:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, int>>) ((s1, s2) => s1.Key.CompareTo(s2.Key)));
          break;
        default:
          keyValuePairList.Sort((Comparison<KeyValuePair<string, int>>) ((s1, s2) => s2.Key.CompareTo(s1.Key)));
          break;
      }
      dic.Clear();
      foreach (KeyValuePair<string, int> keyValuePair in keyValuePairList)
        dic.Add(keyValuePair.Key, keyValuePair.Value);
    }
  }
}
