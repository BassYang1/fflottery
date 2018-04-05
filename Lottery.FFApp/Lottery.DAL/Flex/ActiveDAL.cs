// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.ActiveDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.DAL.Flex
{
  public class ActiveDAL : ComData
  {
    public void GetListJSON(int page, int PSize, string whereStr, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = whereStr;
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("Act_ActiveRecord").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,[Id],[UserId],dbo.f_GetUserName(UserId) as UserName,[ActiveType],[ActiveName],[InMoney],[STime],[CheckIp],[CheckMachine],[FromUserId],[Remark]", "Act_ActiveRecord", "Id", PSize, page, "desc", whereStr);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetHBInfoJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet,\r\n                                            case when FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888)>8 then 8 else FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888) end as HbNum,\r\n                                            (select count(Id) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId={0} and DateDiff(dd,STime,getdate())=0) as TodayHbNum,\r\n                                            (select isnull(sum(InMoney),0) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId={0} and DateDiff(dd,STime,getdate())=0) as TodayHbMoney\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where UserId={0} and DateDiff(dd,STime,getdate())=0 ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveHbActive(string UserId, string ActiveType, string ActiveName, Decimal InMoney, string Remark)
    {
      string clientIp = IPHelp.ClientIP;
      string str = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActHongBao' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0 && Convert.ToInt32(dataTable1.Rows[0]["count"]) > 3000)
          return this.GetJsonResult(0, "今日3000个红包也派发完毕，请明日继续参加！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet,\r\n                                           case when FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888)>8 then 8 else FLOOR(((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)))/1888) end as HbNum,\r\n                                            (select count(Id) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId=a.UserId and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)) as TodayHbNum,\r\n                                            (select isnull(sum(InMoney),0) from Act_ActiveRecord where ActiveType='ActHongBao' and UserId=a.UserId and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)) as TodayHbMoney\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) \r\n                                            group by UserId", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        if (dataTable2.Rows.Count <= 0)
          return this.GetJsonResult(0, "您没有可用的红包！");
        if (Convert.ToInt32(dataTable2.Rows[0]["HbNum"]) - Convert.ToInt32(dataTable2.Rows[0]["TodayHbNum"]) < 1)
          return this.GetJsonResult(0, "您没有可用的红包！");
        string act = SsId.Act;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) ActiveType);
        dbOperHandler.AddFieldItem("ActiveName", (object) ActiveName);
        dbOperHandler.AddFieldItem("InMoney", (object) InMoney);
        dbOperHandler.AddFieldItem("Remark", (object) Remark);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "红包领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, InMoney, 0, 0, 0, 9, 99, "", "", "红包领取", "");
        return this.GetJsonResult(1, string.Concat((object) InMoney));
      }
    }

    public void GetHistoryInfoJSON(ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 15 '恭喜 '+substring(dbo.f_GetUserName(UserId),1,2)+'***'+substring(dbo.f_GetUserName(UserId),len(dbo.f_GetUserName(UserId))-2,3)+' 获得红包 '+Convert(varchar(10),[InMoney])+' 元' as info FROM [Act_ActiveRecord] where ActiveType='ActHongBao' order by STime desc");
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetBetActiveInfoJSON(string UserId, ref string _jsonstr)
    {
      string str = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=0 and UserId={0}", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (!string.IsNullOrEmpty(string.Concat(dataTable1.Rows[0]["StartTime"])))
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 10 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=0 and UserId={0} and StartTime='{1}' order by STime desc", (object) UserId, (object) dataTable1.Rows[0]["StartTime"].ToString());
          dataTable1 = dbOperHandler.GetDataTable();
          if (dataTable1.Rows.Count > 0)
          {
            if (dataTable1.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "'", "STime desc").Length > 0)
            {
              DataRow[] dataRowArray = dataTable1.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc");
              if (dataRowArray.Length > 0)
              {
                TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(dataRowArray[0]["StartTime"]);
                DataTable dataTable2 = dataTable1;
                string filterExpression = "STime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and IsFlag=1";
                string sort = "STime desc";
                _jsonstr = dataTable2.Select(filterExpression, sort).Length <= 0 ? "[{\"result\" :\"" + (object) (timeSpan.Days + 1) + "\",\"flag\" :\"0\"}]" : "[{\"result\" :\"" + (object) (timeSpan.Days + 1) + "\",\"flag\" :\"1\"}]";
              }
              else
                _jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
            }
            else
            {
              DataTable dataTable2 = dataTable1;
              string filterExpression = "STime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and IsFlag=1";
              string sort = "STime desc";
              _jsonstr = dataTable2.Select(filterExpression, sort).Length <= 0 ? "[{\"result\" :\"1\",\"flag\" :\"0\"}]" : "[{\"result\" :\"1\",\"flag\" :\"1\"}]";
            }
          }
          else
            _jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
        }
        else
          _jsonstr = "[{\"result\" :\"1\",\"flag\" :\"0\"}]";
        dataTable1.Clear();
        dataTable1.Dispose();
      }
    }

    public string SaveBetActive(string UserId, string ActiveType, string ActiveName, string Remark)
    {
      string clientIp = IPHelp.ClientIP;
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActBet'  and UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0 && Convert.ToInt32(dataTable1.Rows[0]["count"]) > 0)
          return this.GetJsonResult(0, "今日已领取，请明日继续参加！");
        string str2 = DateTime.Now.ToString("yyyy-MM-dd");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=0 and UserId={0}", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        int num1;
        bool flag;
        if (!string.IsNullOrEmpty(string.Concat(dataTable2.Rows[0]["StartTime"])))
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 10 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [Act_BetRecond] where type=0 and UserId={0} and StartTime='{1}' order by STime desc", (object) UserId, (object) dataTable2.Rows[0]["StartTime"].ToString());
          DataTable dataTable3 = dbOperHandler.GetDataTable();
          if (dataTable3.Rows.Count > 0)
          {
            if (dataTable3.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "'", "STime desc").Length > 0)
            {
              DataRow[] dataRowArray = dataTable3.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc");
              if (dataRowArray.Length > 0)
              {
                num1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(dataRowArray[0]["StartTime"])).Days + 1;
                flag = true;
              }
              else
              {
                flag = false;
                num1 = 1;
              }
            }
            else
            {
              flag = false;
              num1 = 1;
            }
          }
          else
          {
            flag = false;
            num1 = 1;
          }
        }
        else
        {
          flag = false;
          num1 = 1;
        }
        Decimal Money = new Decimal(0);
        Decimal num2 = new Decimal(0);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet FROM [N_UserMoneyStatAll] a \r\n                                              where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) ", (object) UserId);
        DataTable dataTable4 = dbOperHandler.GetDataTable();
        if (dataTable4.Rows.Count > 0)
          num2 = Convert.ToDecimal(dataTable4.Rows[0]["bet"]);
        if (num1 == 1)
        {
          if (!(num2 >= new Decimal(1888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(8);
        }
        if (num1 == 2)
        {
          if (!(num2 >= new Decimal(5888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(28);
        }
        if (num1 == 3)
        {
          if (!(num2 >= new Decimal(8888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(38);
        }
        if (num1 == 4)
        {
          if (!(num2 >= new Decimal(18888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(88);
        }
        if (num1 == 5)
        {
          if (!(num2 >= new Decimal(38888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(128);
        }
        if (num1 == 6)
        {
          if (!(num2 >= new Decimal(58888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(238);
        }
        if (num1 == 7)
        {
          if (!(num2 >= new Decimal(88888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(358);
        }
        if (num1 == 8)
        {
          if (!(num2 >= new Decimal(188888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(588);
        }
        if (num1 == 9)
        {
          if (!(num2 >= new Decimal(388888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(1288);
        }
        if (num1 == 10)
        {
          if (!(num2 >= new Decimal(588888)))
            return this.GetJsonResult(0, "消费未达标，请继续努力！");
          Money = new Decimal(2588);
        }
        if (!flag)
        {
          for (int index = 0; index < 10; ++index)
          {
            dbOperHandler.Reset();
            dbOperHandler.AddFieldItem("UserId", (object) UserId);
            dbOperHandler.AddFieldItem("Type", (object) 0);
            dbOperHandler.AddFieldItem("StartTime", (object) DateTime.Now.ToString("yyyy-MM-dd"));
            dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.AddDays((double) index).ToString("yyyy-MM-dd"));
            if (index == 0)
              dbOperHandler.AddFieldItem("IsFlag", (object) 1);
            else
              dbOperHandler.AddFieldItem("IsFlag", (object) 0);
            dbOperHandler.Insert("Act_BetRecond");
          }
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "UserId=@UserId and STime=@STime and Type=0";
          dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
          dbOperHandler.AddConditionParameter("@STime", (object) str2);
          dbOperHandler.AddFieldItem("IsFlag", (object) 1);
          dbOperHandler.Update("Act_BetRecond");
        }
        string act = SsId.Act;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) ActiveType);
        dbOperHandler.AddFieldItem("ActiveName", (object) ActiveName);
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("Remark", (object) Remark);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str1);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "消费大闯关领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, Money, 0, 0, 0, 9, 0, "消费大闯关领取", "您消费大闯关领取" + (object) Money + "元", "消费大闯关", "");
        return this.GetJsonResult(1, string.Concat((object) Money));
      }
    }

    public void GetChargeJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                                                case when isnull([InMoney],0)>100 then '50.00' else '0' end as money,\r\n                                                                (SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) FROM [N_UserMoneyStatAll]\r\n                                                                where UserId=a.UserId and STime>a.STime) as bet\r\n                                                                FROM [N_UserCharge] a where UserId={0}  and DateDiff(dd,STime,getdate())=0 \r\n                                                                order by STime asc ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveChargeActive(string UserId)
    {
      return this.GetJsonResult(0, "活动已关闭！");
    }

    public void GetGroup3GzJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        string str1 = dataTable.Rows[0]["bet"].ToString();
        string str2 = dataTable.Rows[0]["hyNum"].ToString();
        if (dataTable.Rows.Count > 0)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", (object) str1, (object) str2);
          dataTable = dbOperHandler.GetDataTable();
        }
        if (dataTable.Rows.Count < 1)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", (object) str1);
          dataTable = dbOperHandler.GetDataTable();
        }
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetGroup3IphoneGzJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        string str1 = dataTable.Rows[0]["bet"].ToString();
        string str2 = dataTable.Rows[0]["hyNum"].ToString();
        if (dataTable.Rows.Count > 0)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", (object) str1, (object) str2);
          dataTable = dbOperHandler.GetDataTable();
        }
        if (dataTable.Rows.Count < 1)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", (object) str1);
          dataTable = dbOperHandler.GetDataTable();
        }
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveGroup3Active(string UserId)
    {
      string clientIp = IPHelp.ClientIP;
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("  select Id from Act_ActiveSet where Code='ActGongZi' and [IsUse]=0 and getdate()>=StartTime and getdate()<=EndTime");
        if (dbOperHandler.GetDataTable().Rows.Count < 1)
          return this.GetJsonResult(0, "活动已关闭，请等待活动开放！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserGroup=3 and Id={0}", (object) UserId);
        if (dbOperHandler.GetDataTable().Rows.Count < 1)
          return this.GetJsonResult(0, "您无权领取日奖励工资！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where UserId={0} and  ActiveType='ActGongZi3' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0 && Convert.ToInt32(dataTable1.Rows[0]["count"]) > 0)
          return this.GetJsonResult(0, "您已领取活动，请明日再参加！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        string str2 = dataTable2.Rows[0]["bet"].ToString();
        string str3 = dataTable2.Rows[0]["hyNum"].ToString();
        if (dataTable2.Rows.Count <= 0)
          return this.GetJsonResult(0, "您的团队昨日消费未消费，不能领取！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", (object) str2, (object) str3);
        DataTable dataTable3 = dbOperHandler.GetDataTable();
        if (dataTable3.Rows.Count < 1)
          return this.GetJsonResult(0, "未得到最低消费标准或活跃人数不足，不能领取！");
        string act = SsId.Act;
        Decimal Money = Convert.ToDecimal(dataTable3.Rows[0]["money"].ToString());
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) "ActGongZi3");
        dbOperHandler.AddFieldItem("ActiveName", (object) "日奖励工资");
        dbOperHandler.AddFieldItem("Bet", (object) Convert.ToDecimal(dataTable3.Rows[0]["bet"]));
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("Remark", (object) "日奖励工资");
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str1);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "日奖励工资领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, Money, 0, 0, 0, 9, 99, "", "", "日奖励工资领取", "");
        return this.GetJsonResult(1, "您成功领取日奖励工资" + (object) Money + "元");
      }
    }

    public void GetGroup2GzJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        string str1 = dataTable.Rows[0]["bet"].ToString();
        string str2 = dataTable.Rows[0]["hyNum"].ToString();
        if (dataTable.Rows.Count > 0)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail2] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", (object) str1, (object) str2);
          dataTable = dbOperHandler.GetDataTable();
        }
        if (dataTable.Rows.Count < 1)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,'未得到条件' as money", (object) str1);
          dataTable = dbOperHandler.GetDataTable();
        }
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveGroup2Active(string UserId)
    {
      string clientIp = IPHelp.ClientIP;
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("  select Id from Act_ActiveSet where Code='ActGongZi2' and [IsUse]=0 and getdate()>=StartTime and getdate()<=EndTime");
        if (dbOperHandler.GetDataTable().Rows.Count < 1)
          return this.GetJsonResult(0, "活动已关闭，请等待活动开放！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserGroup=2 and Id={0}", (object) UserId);
        if (dbOperHandler.GetDataTable().Rows.Count < 1)
          return this.GetJsonResult(0, "您无权领取日奖励工资！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where UserId={0} and  ActiveType='ActGongZi2' and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0 && Convert.ToInt32(dataTable1.Rows[0]["count"]) > 0)
          return this.GetJsonResult(0, "您已领取活动，请明日再参加！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=1 ", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        string str2 = dataTable2.Rows[0]["bet"].ToString();
        string str3 = dataTable2.Rows[0]["hyNum"].ToString();
        if (dataTable2.Rows.Count <= 0)
          return this.GetJsonResult(0, "您的团队昨日消费未消费，不能领取！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT top 1 {0} as bet,cast(round([Money]*{0}*0.01,4) as numeric(10,4)) as money FROM [Act_SetGZDetail2] where IsUsed=0 and MinMoney*10000<={0} and MinUsers<={1} order by Id desc", (object) str2, (object) str3);
        DataTable dataTable3 = dbOperHandler.GetDataTable();
        if (dataTable3.Rows.Count < 1)
          return this.GetJsonResult(0, "未得到最低消费标准或活跃人数不足，不能领取！");
        string act = SsId.Act;
        Decimal Money = Convert.ToDecimal(dataTable3.Rows[0]["money"].ToString());
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) "ActGongZi2");
        dbOperHandler.AddFieldItem("ActiveName", (object) "日奖励工资");
        dbOperHandler.AddFieldItem("Bet", (object) Convert.ToDecimal(dataTable3.Rows[0]["bet"]));
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("Remark", (object) "日奖励工资");
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str1);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "日奖励工资领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, Money, 0, 0, 0, 9, 99, "", "", "日奖励工资领取", "");
        return this.GetJsonResult(1, "您成功领取日奖励工资" + (object) Money + "元");
      }
    }

    public void GetHyChargeJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case \r\n                                            when isnull([InMoney],0)>=500 and isnull([InMoney],0)<1000 then '8.00' \r\n                                            when isnull([InMoney],0)>=1000 and isnull([InMoney],0)<2000 then '18.00' \r\n                                            when isnull([InMoney],0)>=2000 and isnull([InMoney],0)<5000 then '28.00' \r\n                                            when isnull([InMoney],0)>=5000 and isnull([InMoney],0)<10000 then '38.00' \r\n                                            when isnull([InMoney],0)>=10000 and isnull([InMoney],0)<20000 then '68.00' \r\n                                            when isnull([InMoney],0)>=20000 and isnull([InMoney],0)<30000 then '128.00' \r\n                                            when isnull([InMoney],0)>=30000 and isnull([InMoney],0)<50000 then '188.00' \r\n                                            when isnull([InMoney],0)>=50000 then '288.00' \r\n                                            else '0' end as money\r\n                                            FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 \r\n                                            order by STime asc ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveHyChargeActive(string UserId)
    {
      return this.GetJsonResult(0, "活动已关闭！");
    }

    public void GetChargeActiveInfoJSON(string UserId, ref string _jsonstr)
    {
      string str1 = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=1 and UserId={0}", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        int num1;
        int num2;
        if (!string.IsNullOrEmpty(string.Concat(dataTable1.Rows[0]["StartTime"])))
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 7 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=1 and UserId={0} and StartTime='{1}' order by STime desc", (object) UserId, (object) dataTable1.Rows[0]["StartTime"].ToString());
          DataTable dataTable2 = dbOperHandler.GetDataTable();
          if (dataTable2.Rows.Count > 0)
          {
            if (dataTable2.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "'", "STime desc").Length > 0)
            {
              DataRow[] dataRowArray = dataTable2.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc");
              if (dataRowArray.Length > 0)
              {
                TimeSpan timeSpan = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(dataRowArray[0]["StartTime"]);
                if (dataTable2.Select("STime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc").Length > 0)
                {
                  num1 = timeSpan.Days + 1;
                  num2 = 1;
                }
                else
                {
                  num1 = timeSpan.Days + 1;
                  num2 = 0;
                }
              }
              else
              {
                num1 = 1;
                num2 = 0;
              }
            }
            else if (dataTable2.Select("STime ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc").Length > 0)
            {
              num1 = 1;
              num2 = 1;
            }
            else
            {
              num1 = 1;
              num2 = 0;
            }
          }
          else
          {
            num1 = 1;
            num2 = 0;
          }
        }
        else
        {
          num1 = 1;
          num2 = 0;
        }
        Decimal num3 = new Decimal(0);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney] FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 order by STime asc ", (object) UserId);
        DataTable dataTable3 = dbOperHandler.GetDataTable();
        if (dataTable3.Rows.Count > 0)
          num3 = Convert.ToDecimal(dataTable3.Rows[0]["InMoney"].ToString());
        string str2 = "M0";
        if (num3 >= new Decimal(500))
          str2 = "M500";
        if (num3 >= new Decimal(1000))
          str2 = "M1000";
        if (num3 >= new Decimal(3000))
          str2 = "M3000";
        if (num3 >= new Decimal(5000))
          str2 = "M5000";
        if (num3 >= new Decimal(10000))
          str2 = "M10000";
        if (num3 >= new Decimal(20000))
          str2 = "M20000";
        if (num3 >= new Decimal(30000))
          str2 = "M30000";
        if (num3 >= new Decimal(50000))
          str2 = "M50000";
        Decimal num4 = new Decimal(0);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 {0} as give FROM [Act_SetChargeDetail] where Name='{1}'", (object) str2, (object) num1);
        DataTable dataTable4 = dbOperHandler.GetDataTable();
        if (dataTable4.Rows.Count > 0)
          num4 = Convert.ToDecimal(dataTable4.Rows[0]["give"].ToString());
        dataTable4.Clear();
        dataTable4.Dispose();
        _jsonstr = "[{\"day\" :\"" + (object) num1 + "\",\"flag\" :\"" + (object) num2 + "\",\"charge\" :\"" + (object) num3 + "\",\"give\" :\"" + (object) num4 + "\"}]";
      }
    }

    public string SaveChargeActive(string UserId, string ActiveType, string ActiveName, string Remark)
    {
      string clientIp = IPHelp.ClientIP;
      string str1 = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='ActCharge'  and UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120)", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0 && Convert.ToInt32(dataTable1.Rows[0]["count"]) > 0)
          return this.GetJsonResult(0, "今日已领取，请明日继续参加！");
        string str2 = DateTime.Now.ToString("yyyy-MM-dd");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT isnull(max(StartTime),'') as StartTime FROM [act_BetRecond] where type=1 and UserId={0}", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        int num1;
        bool flag;
        if (!string.IsNullOrEmpty(string.Concat(dataTable2.Rows[0]["StartTime"])))
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 7 [Id],[UserId],[StartTime],[STime],[IsFlag] FROM [act_BetRecond] where type=1 and UserId={0} and StartTime='{1}' order by STime desc", (object) UserId, (object) dataTable2.Rows[0]["StartTime"].ToString());
          DataTable dataTable3 = dbOperHandler.GetDataTable();
          if (dataTable3.Rows.Count > 0)
          {
            if (dataTable3.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "'", "STime desc").Length > 0)
            {
              DataRow[] dataRowArray = dataTable3.Select("STime ='" + DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd") + "' and IsFlag=1", "STime desc");
              if (dataRowArray.Length > 0)
              {
                num1 = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(dataRowArray[0]["StartTime"])).Days + 1;
                flag = true;
              }
              else
              {
                flag = false;
                num1 = 1;
              }
            }
            else
            {
              flag = false;
              num1 = 1;
            }
          }
          else
          {
            flag = false;
            num1 = 1;
          }
        }
        else
        {
          flag = false;
          num1 = 1;
        }
        Decimal num2 = new Decimal(0);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney] FROM [N_UserCharge] a where UserId={0} and State=1 and DateDiff(dd,STime,getdate())=0 order by STime asc ", (object) UserId);
        DataTable dataTable4 = dbOperHandler.GetDataTable();
        if (dataTable4.Rows.Count > 0)
        {
          num2 = Convert.ToDecimal(dataTable4.Rows[0]["InMoney"].ToString());
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT (isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)) as bet FROM [N_UserMoneyStatAll] a \r\n                                              where UserId={0} and Convert(varchar(10),STime,120)=Convert(varchar(10),Getdate(),120) ", (object) UserId);
          DataTable dataTable3 = dbOperHandler.GetDataTable();
          if (dataTable3.Rows.Count <= 0)
            return this.GetJsonResult(0, "消费未得到首次充值金额的一倍，不能领取！");
          if (Convert.ToDecimal(dataTable3.Rows[0]["bet"].ToString()) < num2)
            return this.GetJsonResult(0, "消费未得到首次充值金额的一倍，不能领取！");
        }
        string str3 = "M0";
        if (num2 >= new Decimal(500))
          str3 = "M500";
        if (num2 >= new Decimal(1000))
          str3 = "M1000";
        if (num2 >= new Decimal(3000))
          str3 = "M3000";
        if (num2 >= new Decimal(5000))
          str3 = "M5000";
        if (num2 >= new Decimal(10000))
          str3 = "M10000";
        if (num2 >= new Decimal(20000))
          str3 = "M20000";
        if (num2 >= new Decimal(30000))
          str3 = "M30000";
        if (num2 >= new Decimal(50000))
          str3 = "M50000";
        Decimal Money = new Decimal(0);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 {0} as give FROM [Act_SetChargeDetail] where Name='{1}'", (object) str3, (object) num1);
        DataTable dataTable5 = dbOperHandler.GetDataTable();
        if (dataTable5.Rows.Count > 0)
          Money = Convert.ToDecimal(dataTable5.Rows[0]["give"].ToString());
        if (!(Money > new Decimal(0)))
          return this.GetJsonResult(0, "首充未达标，请继续努力！");
        if (!flag)
        {
          for (int index = 0; index < 7; ++index)
          {
            dbOperHandler.Reset();
            dbOperHandler.AddFieldItem("UserId", (object) UserId);
            dbOperHandler.AddFieldItem("Type", (object) 1);
            dbOperHandler.AddFieldItem("StartTime", (object) DateTime.Now.ToString("yyyy-MM-dd"));
            dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.AddDays((double) index).ToString("yyyy-MM-dd"));
            if (index == 0)
              dbOperHandler.AddFieldItem("IsFlag", (object) 1);
            else
              dbOperHandler.AddFieldItem("IsFlag", (object) 0);
            dbOperHandler.Insert("Act_BetRecond");
          }
        }
        else
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "UserId=@UserId and STime=@STime and Type=1";
          dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
          dbOperHandler.AddConditionParameter("@STime", (object) str2);
          dbOperHandler.AddFieldItem("IsFlag", (object) 1);
          dbOperHandler.Update("Act_BetRecond");
        }
        string act = SsId.Act;
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) ActiveType);
        dbOperHandler.AddFieldItem("ActiveName", (object) ActiveName);
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("Remark", (object) Remark);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str1);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "首充大闯关领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, Money, 0, 0, 0, 9, 0, "首充大闯关领取", "您首充大闯关领取" + (object) Money + "元", "首充大闯关", "");
        return this.GetJsonResult(1, string.Concat((object) Money));
      }
    }

    public void GetGroupGzJSON(string GroupId, string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)) as bet,\r\n                                            (select count(*) from (select Userid FROM [N_UserMoneyStatAll] where dbo.f_GetUserCode(UserId) like '%,{0},%' and (Bet-Cancellation)>1000 and DateDiff(dd,STime,getdate())=0 group by Userid) A) as hyNum\r\n                                            FROM [N_UserMoneyStatAll] a \r\n                                            where dbo.f_GetUserCode(UserId) like '%,{0},%' and DateDiff(dd,STime,getdate())=0 ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        string str1 = dataTable.Rows[0]["bet"].ToString();
        string str2 = dataTable.Rows[0]["hyNum"].ToString();
        if (dataTable.Rows.Count > 0)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT top 1 {1} as bet,{2} as hynum,cast(round([Money]*{1}*0.01,4) as numeric(10,4)) as money FROM [Act_DayGzSet] where GroupId={0} and IsUsed=0 and MinMoney*10000<={1} and MinUsers<={2} order by Id desc", (object) GroupId, (object) str1, (object) str2);
          dataTable = dbOperHandler.GetDataTable();
        }
        if (dataTable.Rows.Count < 1)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT {0} as bet,{1} as hynum,'未得到条件' as money", (object) str1, (object) str2);
          dataTable = dbOperHandler.GetDataTable();
        }
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public void GetRegChargeJSON(string UserId, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case when isnull([InMoney],0)>100 then '18.00' else '0' end as money\r\n                                            FROM [N_UserCharge] a where state=1 and UserId={0}\r\n                                            order by STime asc ", (object) UserId);
        DataTable dataTable = dbOperHandler.GetDataTable();
        if (dataTable.Rows.Count < 1)
        {
          dbOperHandler.Reset();
          dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 0 as [InMoney],0 as money");
          dataTable = dbOperHandler.GetDataTable();
        }
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string SaveRegCharge(string UserId)
    {
      string clientIp = IPHelp.ClientIP;
      string str = "";
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT [Question],[Answer],[TrueName],b.* FROM [N_User] a left join N_UserBank b on a.Id=b.UserId where a.Id={0}", (object) UserId);
        DataTable dataTable1 = dbOperHandler.GetDataTable();
        if (dataTable1.Rows.Count > 0)
        {
          if (string.IsNullOrEmpty(dataTable1.Rows[0]["TrueName"].ToString()))
            return this.GetJsonResult(0, "请您绑定真实姓名！");
          if (string.IsNullOrEmpty(dataTable1.Rows[0]["Question"].ToString()))
            return this.GetJsonResult(0, "请您绑定密保问题！");
          if (string.IsNullOrEmpty(dataTable1.Rows[0]["PayAccount"].ToString()))
            return this.GetJsonResult(0, "请您绑定银行资料！");
        }
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("select count(Id) as count from Act_ActiveRecord where ActiveType='RegCharge'", (object) UserId);
        DataTable dataTable2 = dbOperHandler.GetDataTable();
        if (dataTable2.Rows.Count > 0 && Convert.ToInt32(dataTable2.Rows[0]["count"]) > 0)
          return this.GetJsonResult(0, "您也领取，本活动只有首次绑定资料后充值领取！");
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = string.Format("SELECT TOP 1 [InMoney],\r\n                                            case when isnull([InMoney],0)>100 then '18.00' else '0' end as money\r\n                                            FROM [N_UserCharge] a where state=1 and UserId={0}\r\n                                            order by STime asc ", (object) UserId);
        DataTable dataTable3 = dbOperHandler.GetDataTable();
        if (dataTable3.Rows.Count <= 0)
          return this.GetJsonResult(0, "您还未充值，不能领取！");
        if (Convert.ToDecimal(dataTable3.Rows[0]["InMoney"]) < new Decimal(100))
          return this.GetJsonResult(0, "您的首充金额不足100元，不能领取！");
        string act = SsId.Act;
        Decimal Money = Convert.ToDecimal(dataTable3.Rows[0]["money"].ToString());
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("SsId", (object) act);
        dbOperHandler.AddFieldItem("UserId", (object) UserId);
        dbOperHandler.AddFieldItem("ActiveType", (object) "RegCharge");
        dbOperHandler.AddFieldItem("ActiveName", (object) "注册首充佣金");
        dbOperHandler.AddFieldItem("InMoney", (object) Money);
        dbOperHandler.AddFieldItem("Remark", (object) "注册首充佣金");
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        dbOperHandler.AddFieldItem("CheckIp", (object) clientIp);
        dbOperHandler.AddFieldItem("CheckMachine", (object) str);
        if (dbOperHandler.Insert("Act_ActiveRecord") <= 0)
          return this.GetJsonResult(0, "注册首充佣金领取失败！");
        new UserTotalTran().MoneyOpers(act, UserId, Money, 0, 0, 0, 9, 99, "", "", "注册首充佣金", "");
        return this.GetJsonResult(1, "您成功领取注册首充佣金" + (object) Money + "元");
      }
    }
  }
}
