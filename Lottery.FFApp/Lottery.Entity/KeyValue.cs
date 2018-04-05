// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.KeyValue
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
  public class KeyValue
  {
    private string m_Key;
    private Decimal m_Value;

    public string tKey
    {
      set
      {
        this.m_Key = value;
      }
      get
      {
        return this.m_Key;
      }
    }

    public Decimal tValue
    {
      set
      {
        this.m_Value = value;
      }
      get
      {
        return this.m_Value;
      }
    }
  }
}
