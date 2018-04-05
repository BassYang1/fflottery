// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserContract
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;
using System.Collections.Generic;

namespace Lottery.Entity
{
  [Serializable]
  public class UserContract
  {
    private int _id;
    private int _type;
    private int _parentid;
    private int _userid;
    private int _isused;
    private DateTime _stime;
    private List<UserContractDetail> _usercontractdetails;

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

    public int Type
    {
      set
      {
        this._type = value;
      }
      get
      {
        return this._type;
      }
    }

    public int ParentId
    {
      set
      {
        this._parentid = value;
      }
      get
      {
        return this._parentid;
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

    public int IsUsed
    {
      set
      {
        this._isused = value;
      }
      get
      {
        return this._isused;
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

    public List<UserContractDetail> UserContractDetails
    {
      set
      {
        this._usercontractdetails = value;
      }
      get
      {
        return this._usercontractdetails;
      }
    }
  }
}
