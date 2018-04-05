// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserZhBetModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
  [Serializable]
  public class UserZhBetModel
  {
    private int _id;
    private int _userid;
    private int _lotteryid;
    private int _playid;
    private string _startissuenum;
    private int _totalnums;
    private Decimal _totalsums;
    private int _isstop;
    private DateTime _stime;

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

    public int UserId
    {
      set
      {
        this._userid = value;
      }
      get
      {
        return this._userid;
      }
    }

    public int LotteryId
    {
      set
      {
        this._lotteryid = value;
      }
      get
      {
        return this._lotteryid;
      }
    }

    public int PlayId
    {
      set
      {
        this._playid = value;
      }
      get
      {
        return this._playid;
      }
    }

    public string StartIssueNum
    {
      set
      {
        this._startissuenum = value;
      }
      get
      {
        return this._startissuenum;
      }
    }

    public int TotalNums
    {
      set
      {
        this._totalnums = value;
      }
      get
      {
        return this._totalnums;
      }
    }

    public Decimal TotalSums
    {
      set
      {
        this._totalsums = value;
      }
      get
      {
        return this._totalsums;
      }
    }

    public int IsStop
    {
      set
      {
        this._isstop = value;
      }
      get
      {
        return this._isstop;
      }
    }

    public DateTime STime
    {
      set
      {
        this._stime = value;
      }
      get
      {
        return this._stime;
      }
    }
  }
}
