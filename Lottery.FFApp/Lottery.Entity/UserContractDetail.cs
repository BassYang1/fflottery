// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserContractDetail
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
  [Serializable]
  public class UserContractDetail
  {
    private int _id;
    private int _ucid;
    private Decimal _minmoney;
    private Decimal _money;
    private int _sort;

    public int Id
    {
      set
      {
        this._id = value;
      }
      get
      {
        return this._id;
      }
    }

    public int UcId
    {
      set
      {
        this._ucid = value;
      }
      get
      {
        return this._ucid;
      }
    }

    public Decimal MinMoney
    {
      set
      {
        this._minmoney = value;
      }
      get
      {
        return this._minmoney;
      }
    }

    public Decimal Money
    {
      set
      {
        this._money = value;
      }
      get
      {
        return this._money;
      }
    }

    public int Sort
    {
      set
      {
        this._sort = value;
      }
      get
      {
        return this._sort;
      }
    }
  }
}
