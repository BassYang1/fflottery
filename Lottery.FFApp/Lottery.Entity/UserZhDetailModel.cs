// Decompiled with JetBrains decompiler
// Type: Lottery.Entity.UserZhDetailModel
// Assembly: Lottery.Entity, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 46AD28DD-7094-42F3-8479-C39F629CA84C
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Entity.dll

using System;
using System.Collections.Generic;

namespace Lottery.Entity
{
  [Serializable]
  public class UserZhDetailModel
  {
    private List<UserBetModel> list = new List<UserBetModel>();
    private int _id;
    private string _issuenum;
    private int _times;
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

    public int Times
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

    public List<UserBetModel> Lists
    {
      set
      {
        this.list = value;
      }
      get
      {
        return this.list;
      }
    }
  }
}
