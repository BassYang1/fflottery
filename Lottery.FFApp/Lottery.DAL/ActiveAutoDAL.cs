// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.ActiveAutoDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using System;
using System.Data;

namespace Lottery.DAL
{
  public class ActiveAutoDAL : ComData
  {
    public bool Exists(string _type)
    {
      int num = 0;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "ActiveType=@ActiveType and STime=@STime";
        dbOperHandler.AddConditionParameter("@ActiveType", (object) _type);
        dbOperHandler.AddConditionParameter("@STime", (object) DateTime.Now.ToString("yyyy-MM-dd"));
        if (dbOperHandler.Exist("Act_AutoRecord"))
          num = 1;
      }
      return num == 1;
    }

    public bool Add(string type)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.AddFieldItem("ActiveType", (object) type);
        dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd"));
        dbOperHandler.Insert("Act_AutoRecord");
      }
      return true;
    }

    public void AutoActiveOper(string AdminId, string ActiveCode, string ActiveName, string ProcName)
    {
      if (new ActiveAutoDAL().Exists(ActiveCode))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ExecuteProcActive(ProcName);
      }
      this.Add(ActiveCode);
      new LogSysDAL().Save("系统自动", "活动补发" + ActiveName);
      new LogAdminOperDAL().SaveLog(AdminId, "0", "活动管理", "手动补发" + ActiveName);
    }

    public void AutoActive1Money()
    {
      if (new ActiveAutoDAL().Exists("Active1"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id from N_User with(nolock) where IsDel=0";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoConsumeMoney", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Active1");
      new LogSysDAL().Save("系统自动", "自动派发三级消费佣金");
    }

    public void AutoActive1ToMoney()
    {
      if (new ActiveAutoDAL().Exists("Active1"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id,UserCode from N_User with(nolock) where IsDel=0 and len(UserCode)=16";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoConsumeMoney", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Active1");
      new LogSysDAL().Save("系统自动", "自动派发三级消费佣金");
    }

    public void AutoActive2Money()
    {
      if (new ActiveAutoDAL().Exists("Active2"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id from N_User with(nolock) where IsDel=0";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoChargeMoney", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Active2");
      new LogSysDAL().Save("系统自动", "自动派发三级充值佣金");
    }

    public void AutoActive6Money()
    {
      if (new ActiveAutoDAL().Exists("Active6"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id from N_User with(nolock) where IsDel=0";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoLossMoney", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Active6");
      new LogSysDAL().Save("系统自动", "自动派发三级亏损佣金");
    }

    public void AutoAgent1Money()
    {
      if (new ActiveAutoDAL().Exists("Agent1"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id from N_User with(nolock) where IsDel=0 and AgentId=1";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoAgent1", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Agent1");
      new LogSysDAL().Save("系统自动", "自动派发一级分红工资");
    }

    public void AutoAgent2Money()
    {
      if (new ActiveAutoDAL().Exists("Agent2"))
        return;
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select Id from N_User with(nolock) where IsDel=0 and AgentId=2";
        DataTable dataTable = dbOperHandler.GetDataTable();
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          dbOperHandler.ExecuteProcAuto("proc_AutoAgent2", dataTable.Rows[index]["Id"].ToString());
      }
      this.Add("Agent2");
      new LogSysDAL().Save("系统自动", "自动派发二级分红工资");
    }
  }
}
