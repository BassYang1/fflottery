// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.ajaxsysDelData
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;

namespace Lottery.Admin
{
  public class ajaxsysDelData : AdminCenter
  {
    private string _operType = string.Empty;
    private string _response = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!this.CheckFormUrl())
        this.Response.End();
      this.Admin_Load("master", "json");
      this._operType = this.q("oper");
      switch (this._operType)
      {
        case "ajaxDel":
          this.ajaxDel();
          break;
        default:
          this.DefaultResponse();
          break;
      }
      this.Response.Write(this._response);
    }

    private void DefaultResponse()
    {
      this._response = this.JsonResult(0, "未知操作");
    }

    private void ajaxDel()
    {
      string d1 = this.f("d1");
      string d2 = this.f("d2");
      string str1 = this.f("flag");
      string str2 = Convert.ToDateTime(d1).ToString("yyyy-MM-dd HH:mm:ss");
      if (str1 == "1")
      {
        new SysDelDataDAL().DeleteUserBet(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员投注记录");
      }
      if (str1 == "2")
      {
        new SysDelDataDAL().DeleteUserGetCash(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员取款记录");
      }
      if (str1 == "3")
      {
        new SysDelDataDAL().DeleteUserMoneyLog(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员账变记录");
      }
      if (str1 == "4")
      {
        new SysDelDataDAL().DeleteUserMoneyStat(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员统计记录");
      }
      if (str1 == "5")
      {
        new SysDelDataDAL().DeleteLotteryData(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的开奖记录");
      }
      if (str1 == "6")
      {
        new SysDelDataDAL().DeleteUserLogs(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员登录记录");
      }
      if (str1 == "7")
      {
        new SysDelDataDAL().DeleteLogs(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的系统日志记录");
      }
      if (str1 == "8")
      {
        new SysDelDataDAL().DeleteUserBetZh(d1, d2);
        new LogAdminOperDAL().SaveLog(this.AdminId, "0", "系统设置", "清理了" + str2 + "及之前的会员追号记录");
      }
      this._response = this.JsonResult(1, "删除成功");
    }
  }
}
