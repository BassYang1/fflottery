// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.Flex.UserEmailDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Collections;
using System.Data;

namespace Lottery.DAL.Flex
{
  public class UserEmailDAL : ComData
  {
    public void GetListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("N_UserEmail").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(SendId) as SendName,dbo.f_GetUserName(ReceiveId) as ReceiveName,*", "N_UserEmail", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON2(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public string Save(string code, string SendId, string ReceiveId, string Title, string Contents)
    {
      string str1 = "";
      ArrayList arrayList = new ArrayList();
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        if (!string.IsNullOrEmpty(ReceiveId))
        {
          dbOperHandler.Reset();
          dbOperHandler.ConditionExpress = "UserName=@UserName";
          dbOperHandler.AddConditionParameter("@UserName", (object) ReceiveId);
          ReceiveId = string.Concat(dbOperHandler.GetField("N_User", "Id"));
          arrayList.Add((object) ReceiveId);
        }
        else
        {
          switch (code)
          {
            case "0":
              dbOperHandler.Reset();
              dbOperHandler.ConditionExpress = "Id=@Id";
              dbOperHandler.AddConditionParameter("@Id", (object) SendId);
              string str2 = string.Concat(dbOperHandler.GetField("N_User", "ParentId"));
              if (str2.Equals("0"))
              {
                str1 = "您没有上级不能发送!";
                break;
              }
              arrayList.Add((object) str2);
              break;
            case "1":
              dbOperHandler.Reset();
              dbOperHandler.SqlCmd = string.Format("select Id from N_User where ParentId={0}", (object) SendId);
              DataTable dataTable1 = dbOperHandler.GetDataTable();
              if (dataTable1.Rows.Count < 1)
              {
                str1 = "您没有直属下级不能发送!";
                break;
              }
              for (int index = 0; index < dataTable1.Rows.Count; ++index)
                arrayList.Add((object) dataTable1.Rows[index]["Id"].ToString());
              break;
            case "2":
              dbOperHandler.Reset();
              dbOperHandler.SqlCmd = string.Format("select Id from N_User where UserCode like '%{0}%' and Id<>{0}", (object) Strings.PadLeft(SendId));
              DataTable dataTable2 = dbOperHandler.GetDataTable();
              if (dataTable2.Rows.Count < 1)
              {
                str1 = "您没有下级不能发送!";
                break;
              }
              for (int index = 0; index < dataTable2.Rows.Count; ++index)
                arrayList.Add((object) dataTable2.Rows[index]["Id"].ToString());
              break;
          }
        }
        foreach (string str3 in arrayList)
        {
          dbOperHandler.Reset();
          dbOperHandler.AddFieldItem("SendId", (object) SendId);
          dbOperHandler.AddFieldItem("ReceiveId", (object) str3);
          dbOperHandler.AddFieldItem("Title", (object) Title);
          dbOperHandler.AddFieldItem("Contents", (object) Contents);
          dbOperHandler.AddFieldItem("STime", (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
          dbOperHandler.AddFieldItem("IsRead", (object) "0");
          str1 = dbOperHandler.Insert("N_UserEmail") <= 0 ? "发送失败！" : "发送成功！";
        }
        return str1;
      }
    }

    public void UpdateState(string _id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=@id";
        dbOperHandler.AddConditionParameter("@id", (object) _id);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        dbOperHandler.Update("N_UserEmail");
      }
    }

    public int DeletesSend(string _id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=@id";
        dbOperHandler.AddConditionParameter("@id", (object) _id);
        dbOperHandler.AddFieldItem("IsDelSend", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public int DeletesReceive(string _id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "id=@id";
        dbOperHandler.AddConditionParameter("@id", (object) _id);
        dbOperHandler.AddFieldItem("IsDelReceive", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public int DeletesByUserSend(string UserId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "UserId=@UserId";
        dbOperHandler.AddConditionParameter("@SendId", (object) UserId);
        dbOperHandler.AddFieldItem("IsDelSend", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public int DeletesByUserReceive(string UserId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "ReceiveId=@UserId";
        dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        dbOperHandler.AddFieldItem("IsDelReceive", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public int ReadedUserReceive(string UserId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "ReceiveId=@UserId";
        dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public int UpdateIsRead(string Id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) Id);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        return dbOperHandler.Update("N_UserEmail");
      }
    }

    public void GetMessageListJSON(int _thispage, int _pagesize, string _wherestr1, ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = _wherestr1;
        string sql0 = SqlHelp.GetSql0(dbOperHandler.Count("N_UserMessage").ToString() + " as totalcount,row_number() over (order by Id desc) as rowid,dbo.f_GetUserName(UserId) as UserName,*", "N_UserMessage", "Id", _pagesize, _thispage, "desc", _wherestr1);
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = sql0;
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON2(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public int DeletesMessage(string UserId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "UserId=@UserId";
        dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
        return dbOperHandler.Delete("N_UserMessage");
      }
    }

    public int DeletesMessageById(string Id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) Id);
        return dbOperHandler.Delete("N_UserMessage");
      }
    }

    public int ReadedUserMessage(string UserId)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "UserId=@UserId";
        dbOperHandler.AddConditionParameter("@UserId", (object) UserId);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        return dbOperHandler.Update("N_UserMessage");
      }
    }

    public int UpdateMessageIsRead(string Id)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) Id);
        dbOperHandler.AddFieldItem("IsRead", (object) "1");
        return dbOperHandler.Update("N_UserMessage");
      }
    }
  }
}
