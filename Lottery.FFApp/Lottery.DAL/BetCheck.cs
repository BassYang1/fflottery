// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.BetCheck
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

namespace Lottery.DAL
{
  public class BetCheck
  {
    private int _BetErr;
    private int _BetNum;
    private int _BetPoint;
    private int _IsOpen;
    private int _IsEnable;

    public int BetErr
    {
      get
      {
        return this._BetErr;
      }
      set
      {
        this._BetErr = value;
      }
    }

    public int BetNum
    {
      get
      {
        return this._BetNum;
      }
      set
      {
        this._BetNum = value;
      }
    }

    public int BetPoint
    {
      get
      {
        return this._BetPoint;
      }
      set
      {
        this._BetPoint = value;
      }
    }

    public int IsOpen
    {
      get
      {
        return this._IsOpen;
      }
      set
      {
        this._IsOpen = value;
      }
    }

    public int IsEnable
    {
      get
      {
        return this._IsEnable;
      }
      set
      {
        this._IsEnable = value;
      }
    }
  }
}
