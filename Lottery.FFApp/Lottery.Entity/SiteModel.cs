// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.SiteModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;

namespace Lottery.Entity
{
  [Serializable]
  public class SiteModel
  {
    private int _profitmodel = 1;
    private int _profitmargin = 10;
    private string m_Name;
    private string m_Dir;
    private int _webisopen;
    private string _webcloseseason;
    private int _zhisopen;
    private int _regisopen;
    private int _betisopen;
    private string _csurl;
    private int _SignMinTotal;
    private int _SignMaxTotal;
    private int _SignNum;
    private Decimal _warntotal;
    private Decimal _maxbet;
    private Decimal _maxwin;
    private Decimal _maxwinfk;
    private Decimal _maxlevel;
    private Decimal _mincharge;
    private int _points;
    private Decimal _priceoutcheck;
    private Decimal _priceout;
    private Decimal _priceout2;
    private int _pricenum;
    private string _pricetime1;
    private string _pricetime2;
    private Decimal _bankTime;
    private int _priceoutperson;
    private string _clientversion;
    private DateTime _updatetime;
    private DateTime _newsupdatetime;
    private int _autolottery;
    private int _autoranking;
    private string m_CookieDomain;
    private string m_CookiePath;
    private string m_CookiePrev;
    private string m_CookieKeyCode;
    private string m_Version;
    private string m_DebugKey;
    private int _betsq;
    private Decimal _betsqmoney;

    public string Name
    {
      set
      {
        this.m_Name = value;
      }
      get
      {
        return this.m_Name;
      }
    }

    public string Dir
    {
      set
      {
        this.m_Dir = value;
      }
      get
      {
        return this.m_Dir;
      }
    }

    public int WebIsOpen
    {
      set
      {
        this._webisopen = value;
      }
      get
      {
        return this._webisopen;
      }
    }

    public string WebCloseSeason
    {
      set
      {
        this._webcloseseason = value;
      }
      get
      {
        return this._webcloseseason;
      }
    }

    public int ZHIsOpen
    {
      set
      {
        this._zhisopen = value;
      }
      get
      {
        return this._zhisopen;
      }
    }

    public int RegIsOpen
    {
      set
      {
        this._regisopen = value;
      }
      get
      {
        return this._regisopen;
      }
    }

    public int BetIsOpen
    {
      set
      {
        this._betisopen = value;
      }
      get
      {
        return this._betisopen;
      }
    }

    public string CSUrl
    {
      set
      {
        this._csurl = value;
      }
      get
      {
        return this._csurl;
      }
    }

    public int SignMinTotal
    {
      set
      {
        this._SignMinTotal = value;
      }
      get
      {
        return this._SignMinTotal;
      }
    }

    public int SignMaxTotal
    {
      set
      {
        this._SignMaxTotal = value;
      }
      get
      {
        return this._SignMaxTotal;
      }
    }

    public int SignNum
    {
      set
      {
        this._SignNum = value;
      }
      get
      {
        return this._SignNum;
      }
    }

    public Decimal WarnTotal
    {
      set
      {
        this._warntotal = value;
      }
      get
      {
        return this._warntotal;
      }
    }

    public Decimal MaxBet
    {
      set
      {
        this._maxbet = value;
      }
      get
      {
        return this._maxbet;
      }
    }

    public Decimal MaxWin
    {
      set
      {
        this._maxwin = value;
      }
      get
      {
        return this._maxwin;
      }
    }

    public Decimal MaxWinFK
    {
      set
      {
        this._maxwinfk = value;
      }
      get
      {
        return this._maxwinfk;
      }
    }

    public Decimal MaxLevel
    {
      set
      {
        this._maxlevel = value;
      }
      get
      {
        return this._maxlevel;
      }
    }

    public Decimal MinCharge
    {
      set
      {
        this._mincharge = value;
      }
      get
      {
        return this._mincharge;
      }
    }

    public int Points
    {
      set
      {
        this._points = value;
      }
      get
      {
        return this._points;
      }
    }

    public Decimal PriceOutCheck
    {
      set
      {
        this._priceoutcheck = value;
      }
      get
      {
        return this._priceoutcheck;
      }
    }

    public Decimal PriceOut
    {
      set
      {
        this._priceout = value;
      }
      get
      {
        return this._priceout;
      }
    }

    public Decimal PriceOut2
    {
      set
      {
        this._priceout2 = value;
      }
      get
      {
        return this._priceout2;
      }
    }

    public int PriceNum
    {
      set
      {
        this._pricenum = value;
      }
      get
      {
        return this._pricenum;
      }
    }

    public string PriceTime1
    {
      set
      {
        this._pricetime1 = value;
      }
      get
      {
        return this._pricetime1;
      }
    }

    public string PriceTime2
    {
      set
      {
        this._pricetime2 = value;
      }
      get
      {
        return this._pricetime2;
      }
    }

    public Decimal BankTime
    {
      set
      {
        this._bankTime = value;
      }
      get
      {
        return this._bankTime;
      }
    }

    public int PriceOutPerson
    {
      set
      {
        this._priceoutperson = value;
      }
      get
      {
        return this._priceoutperson;
      }
    }

    public string ClientVersion
    {
      set
      {
        this._clientversion = value;
      }
      get
      {
        return this._clientversion;
      }
    }

    public DateTime UpdateTime
    {
      set
      {
        this._updatetime = value;
      }
      get
      {
        return this._updatetime;
      }
    }

    public DateTime NewsUpdateTime
    {
      set
      {
        this._newsupdatetime = value;
      }
      get
      {
        return this._newsupdatetime;
      }
    }

    public int AutoLottery
    {
      set
      {
        this._autolottery = value;
      }
      get
      {
        return this._autolottery;
      }
    }

    public int ProfitModel
    {
      set
      {
        this._profitmodel = value;
      }
      get
      {
        return this._profitmodel;
      }
    }

    public int ProfitMargin
    {
      set
      {
        this._profitmargin = value;
      }
      get
      {
        return this._profitmargin;
      }
    }

    public int AutoRanking
    {
      set
      {
        this._autoranking = value;
      }
      get
      {
        return this._autoranking;
      }
    }

    public string CookieDomain
    {
      set
      {
        this.m_CookieDomain = value;
      }
      get
      {
        return this.m_CookieDomain;
      }
    }

    public string CookiePath
    {
      set
      {
        this.m_CookiePath = value;
      }
      get
      {
        return this.m_CookiePath;
      }
    }

    public string CookiePrev
    {
      set
      {
        this.m_CookiePrev = value;
      }
      get
      {
        return this.m_CookiePrev;
      }
    }

    public string CookieKeyCode
    {
      set
      {
        this.m_CookieKeyCode = value;
      }
      get
      {
        return this.m_CookieKeyCode;
      }
    }

    public string Version
    {
      set
      {
        this.m_Version = value;
      }
      get
      {
        return this.m_Version;
      }
    }

    public string DebugKey
    {
      set
      {
        this.m_DebugKey = value;
      }
      get
      {
        return this.m_DebugKey;
      }
    }

    public int BetSQ
    {
      set
      {
        this._betsq = value;
      }
      get
      {
        return this._betsq;
      }
    }

    public Decimal BetSQMoney
    {
      set
      {
        this._betsqmoney = value;
      }
      get
      {
        return this._betsqmoney;
      }
    }
  }
}
