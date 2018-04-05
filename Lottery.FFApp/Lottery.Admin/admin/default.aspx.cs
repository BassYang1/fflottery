// Decompiled with JetBrains decompiler
// Type: Lottery.Admin._default
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;

namespace Lottery.Admin
{
    public class _default : AdminCenter
    {
        public string act0 = "style=\"display:none;\"";
        public string act1 = "style=\"display:none;\"";
        public string act2 = "style=\"display:none;\"";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");
            if (!"woshishui".Equals(Cookie.GetValue(this.site.CookiePrev + "admin", "name").Trim()))
            {
                //string clientIp = IPHelp.ClientIP;
                //this.doh.Reset();
                //this.doh.SqlCmd = "select * from Sys_LoginCheck where CheckType=0 and IsUsed=1";
                //DataTable dataTable = this.doh.GetDataTable();
                //bool flag = false;

                //if (dataTable.Rows.Count > 0)
                //{
                //    for (int index = 0; index < dataTable.Rows.Count; ++index)
                //    {
                //        if (IPHelp.domain2ip(string.Concat(dataTable.Rows[index]["CheckTitle"])).Equals(clientIp))
                //            flag = true;
                //    }
                //}

                //if (!flag)
                //{
                //    this.Response.Clear();
                //    this.Response.Write("您的网络环境不合法，请联系管理员!");
                //    this.Response.End();
                //    return;
                //}
            }
            if (this.IsPostBack)
                return;
            this.AdminId = this.Str2Str(Cookie.GetValue(this.site.CookiePrev + "admin", "id"));
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT top 1 * FROM [Sys_Admin] a left join [Sys_Role] b on a.RoleId=b.Id where a.Id=" + this.AdminId;
            DataTable dataTable1 = this.doh.GetDataTable();
            if (dataTable1.Rows.Count > 0)
            {
                this.AdminIsSuper = "1".Equals(dataTable1.Rows[0]["IsSuper"].ToString().Trim());
                this.AdminSetting = dataTable1.Rows[0]["Setting"].ToString();
            }
            if (this.AdminIsSuper)
            {
                this.act0 = "";
                this.act1 = "";
                this.act2 = "";
            }
            else
            {
                if (this.AdminSetting.Contains(",99001,"))
                    this.act0 = "";
                if (this.AdminSetting.Contains(",99002,"))
                    this.act1 = "";
                if (this.AdminSetting.Contains(",99003,"))
                    this.act2 = "";
            }
        }
    }
}
