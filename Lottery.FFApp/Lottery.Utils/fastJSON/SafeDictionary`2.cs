// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.fastJSON.SafeDictionary`2
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Collections.Generic;

namespace Lottery.Utils.fastJSON
{
  public class SafeDictionary<TKey, TValue>
  {
    private readonly object _Padlock = new object();
    private readonly Dictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();

    public bool ContainsKey(TKey key)
    {
      return this._Dictionary.ContainsKey(key);
    }

    public TValue this[TKey key]
    {
      get
      {
        return this._Dictionary[key];
      }
    }

    public void Add(TKey key, TValue value)
    {
      lock (this._Padlock)
        this._Dictionary.Add(key, value);
    }
  }
}
