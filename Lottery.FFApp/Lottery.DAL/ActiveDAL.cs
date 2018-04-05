// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.ActiveDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class ActiveDAL : ComData
  {
    protected SiteModel site;

    public ActiveDAL()
    {
      this.site = new conSite().GetSite();
    }

    public void GetListJSON(string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        string str = "select top 1 * from Act_ActiveSet where " + _wherestr1 + " order by Id asc";
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = str;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON_ActiveRecord(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Act_ActiveRecord");
        string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,ActiveType,ActiveName,InMoney,Remark,STime", "Act_ActiveRecord", "STime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON_AgentFHRecord(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        int totalCount = dbOperHandler.Count("Act_AgentFHRecord");
        string sql0 = SqlHelp.GetSql0("Id,UserId,dbo.f_GetUserName(UserId) as UserName,*", "Act_AgentFHRecord", "STime", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"pagebar\" :\"" + PageBar.GetPageBar(6, "js", 2, totalCount, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," + dtHelp.DT2JSON(dataTable, _pagesize * (_thispage - 1)) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON2(string id, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT [ID],[Code],[Name],\r\n                        Convert(varchar(10),[StartTime],120) as StartTime,Convert(varchar(10),[EndTime],120) as EndTime,\r\n                        [IsFirst],[IsUse],[Remark]\r\n                        FROM [Act_ActiveSet] where IsUse=1 and id=" + id + " order by id asc";
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\"," + dtHelp.DT2JSON(dataTable) + "}";
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetListJSON_Active1(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        object[] objArray = dbOperHandler.ExecuteProc("proc_Active1Bet", userId);
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"bet\" :\"" + objArray[0] + "\",\"bet2\" :\"" + objArray[1] + "\",\"bet3\" :\"" + objArray[2] + "\"}";
      }
    }

    public void GetListJSON_Active1Own(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        object[] objArray = dbOperHandler.ExecuteProc("proc_Active1OwnBet", userId);
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"bet\" :\"" + objArray[0] + "\",\"bet2\" :\"" + objArray[1] + "\",\"bet3\" :\"" + objArray[2] + "\"}";
      }
    }

    public void GetListJSON_Active2(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        object[] objArray = dbOperHandler.ExecuteProc("proc_Active2Bet", userId);
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"bet\" :\"" + objArray[0] + "\",\"bet2\" :\"" + objArray[1] + "\",\"bet3\" :\"" + objArray[2] + "\"}";
      }
    }

    public void GetListJSON_Active6(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        object[] objArray = dbOperHandler.ExecuteProc("proc_Active6Bet", userId);
        _jsonstr = "{\"result\" :\"1\",\"returnval\" :\"操作成功\",\"bet\" :\"" + objArray[0] + "\",\"bet2\" :\"" + objArray[1] + "\",\"bet3\" :\"" + objArray[2] + "\"}";
      }
    }

    public void GetActive2Money(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        string clientIp = IPHelp.ClientIP;
        _jsonstr = string.Concat(dbOperHandler.ExecuteProcAuto("proc_AutoChargeMoney", userId));
      }
      new LogSysDAL().Save("系统活动", userId + "领取首次充值佣金");
    }

    public void GetActive1OwnMoney(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        string clientIp = IPHelp.ClientIP;
        _jsonstr = string.Concat(dbOperHandler.ExecuteProc_Active1("proc_Active1Own", userId, "", ""));
      }
      new LogSysDAL().Save("系统活动", userId + "领取个人消费佣金");
    }

    public void GetActive7Money(string userId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Code=@Code";
        dbOperHandler.AddConditionParameter("@Code", (object) "Active7");
        object[] fields = dbOperHandler.GetFields("Act_ActiveSet", "StartTime,EndTime,IsUse");
        if (Convert.ToInt32(fields[2]) == 1)
        {
          if (new DateTimePubDAL().GetDateTime() >= Convert.ToDateTime(fields[0]) && new DateTimePubDAL().GetDateTime() <= Convert.ToDateTime(fields[1]))
          {
            Decimal _money = (Decimal) new Random().Next(this.site.SignMinTotal, this.site.SignMaxTotal);
            SysActiveRecordDAL sysActiveRecordDal = new SysActiveRecordDAL();
            string clientIp = IPHelp.ClientIP;
            dbOperHandler.Reset();
            dbOperHandler.ConditionExpress = "ActiveType='Active7' and datediff(d,STime,getdate())=0";
            if (dbOperHandler.Count("Act_ActiveRecord") <= this.site.SignNum)
            {
              if (!sysActiveRecordDal.Exists("UserId=" + userId + " and ActiveType='Active7' and datediff(d,STime,getdate())=0"))
              {
                if (!sysActiveRecordDal.Exists("CheckIp='" + clientIp + "' and ActiveType='Active7' and datediff(d,STime,getdate())=0"))
                {
                  dbOperHandler.Reset();
                  dbOperHandler.ConditionExpress = "UserId=" + userId;
                  if (dbOperHandler.Count("N_UserBank") > 0)
                  {
                    dbOperHandler.Reset();
                    dbOperHandler.ConditionExpress = "UserId=" + userId + " and state=0";
                    if (dbOperHandler.Count("N_UserBet") > 0)
                    {
                      _jsonstr = "您还有未开奖的订单，请开奖后再领取！";
                    }
                    else
                    {
                      dbOperHandler.Reset();
                      dbOperHandler.SqlCmd = "select Bet,Cancellation from N_UserMoneyStatAll where UserId=" + userId + " and datediff(d,STime,getdate())=0";
                      DataTable dataTable = dbOperHandler.GetDataTable();
                      if (dataTable.Rows.Count > 0)
                      {
                        if (Convert.ToDecimal(dataTable.Rows[0]["Bet"]) - Convert.ToDecimal(dataTable.Rows[0]["Cancellation"]) < new Decimal(50))
                        {
                          _jsonstr = "签到领现失败，您的流水未得到50元！";
                        }
                        else
                        {
                          sysActiveRecordDal.SaveLog(userId, "Active7", "签到领现", _money, "您签到领取了" + (object) _money + "元");
                          _jsonstr = "签到领现成功" + (object) _money + "元";
                          new LogSysDAL().Save("系统活动", userId + "领取签到佣金" + (object) _money + "元");
                        }
                      }
                      else
                        _jsonstr = "签到领现失败，您的流水未得到50元！";
                    }
                  }
                  else
                    _jsonstr = "签到领现失败，您未绑定银行！";
                }
                else
                  _jsonstr = "签到领现失败，您当前IP今天已领取！";
              }
              else
                _jsonstr = "签到领现失败，您今天已领取！";
            }
            else
              _jsonstr = "签到领现失败，今天领取已得到名额！";
          }
          else
            _jsonstr = "领取失败，活动时间已过！";
        }
        else
          _jsonstr = "领取失败，活动已关闭！";
      }
    }
  }
}
