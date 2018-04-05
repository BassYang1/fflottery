// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserBetModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
  [Serializable]
  public class UserBetModel
  {
    private Decimal _usermoney = new Decimal(0, 0, 0, false, (byte) 4);
    private Decimal _singlemoney = new Decimal(0, 0, 0, false, (byte) 4);
    private Decimal _times = new Decimal(1);
    private int _num = 1;
    private Decimal _total = new Decimal(0, 0, 0, false, (byte) 4);
    private Decimal _point = new Decimal(0, 0, 0, false, (byte) 4);
    private Decimal _pointmoney = new Decimal(0, 0, 0, false, (byte) 4);
    private Decimal _bonus = new Decimal(0, 0, 0, false, (byte) 4);
    private DateTime _stime = DateTime.Now;
    private DateTime _stime2 = DateTime.Now;
    private int _iswin = -1;
    private int _id;
    private int _userid;
    private int _playid;
    private string _playcode;
    private int _lotteryid;
    private string _issuenum;
    private string _detail;
    private int _dx;
    private int _ds;
    private int _winnum;
    private Decimal _winbonus;
    private Decimal _realget;
    private string _pos;
    private int _isopen;
    private int _state;
    private int _isdelay;
    private DateTime _stime9;
    private bool _ischeat;
    private int _zhid;
    private int _zhid2;

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

    public Decimal UserMoney
    {
      set
      {
        this._usermoney = value;
      }
      get
      {
        return this._usermoney;
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

    public string PlayCode
    {
      set
      {
        this._playcode = value;
      }
      get
      {
        return this._playcode;
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

    public string IssueNum
    {
      set
      {
        this._issuenum = value;
      }
      get
      {
        return this._issuenum;
      }
    }

    public Decimal SingleMoney
    {
      set
      {
        this._singlemoney = value;
      }
      get
      {
        return this._singlemoney;
      }
    }

    public Decimal Times
    {
      set
      {
        this._times = value;
      }
      get
      {
        return this._times;
      }
    }

    public int Num
    {
      set
      {
        this._num = value;
      }
      get
      {
        return this._num;
      }
    }

    public string Detail
    {
      set
      {
        this._detail = value;
      }
      get
      {
        return this._detail;
      }
    }

    public int DX
    {
      set
      {
        this._dx = value;
      }
      get
      {
        return this._dx;
      }
    }

    public int DS
    {
      set
      {
        this._ds = value;
      }
      get
      {
        return this._ds;
      }
    }

    public Decimal Total
    {
      set
      {
        this._total = value;
      }
      get
      {
        return this._total;
      }
    }

    public Decimal Point
    {
      set
      {
        this._point = value;
      }
      get
      {
        return this._point;
      }
    }

    public Decimal PointMoney
    {
      set
      {
        this._pointmoney = value;
      }
      get
      {
        return this._pointmoney;
      }
    }

    public Decimal Bonus
    {
      set
      {
        this._bonus = value;
      }
      get
      {
        return this._bonus;
      }
    }

    public int WinNum
    {
      set
      {
        this._winnum = value;
      }
      get
      {
        return this._winnum;
      }
    }

    public Decimal WinBonus
    {
      set
      {
        this._winbonus = value;
      }
      get
      {
        return this._winbonus;
      }
    }

    public Decimal RealGet
    {
      set
      {
        this._realget = value;
      }
      get
      {
        return this._realget;
      }
    }

    public string Pos
    {
      set
      {
        this._pos = value;
      }
      get
      {
        return this._pos;
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

    public DateTime STime2
    {
      set
      {
        this._stime2 = value;
      }
      get
      {
        return this._stime2;
      }
    }

    public int IsOpen
    {
      set
      {
        this._isopen = value;
      }
      get
      {
        return this._isopen;
      }
    }

    public int State
    {
      set
      {
        this._state = value;
      }
      get
      {
        return this._state;
      }
    }

    public int IsDelay
    {
      set
      {
        this._isdelay = value;
      }
      get
      {
        return this._isdelay;
      }
    }

    public int IsWin
    {
      set
      {
        this._iswin = value;
      }
      get
      {
        return this._iswin;
      }
    }

    public DateTime STime9
    {
      set
      {
        this._stime9 = value;
      }
      get
      {
        return this._stime9;
      }
    }

    public bool IsCheat
    {
      set
      {
        this._ischeat = value;
      }
      get
      {
        return this._ischeat;
      }
    }

    public int ZHID
    {
      set
      {
        this._zhid = value;
      }
      get
      {
        return this._zhid;
      }
    }

    public int zhid2
    {
      set
      {
        this._zhid2 = value;
      }
      get
      {
        return this._zhid2;
      }
    }
  }
}
