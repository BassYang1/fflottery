// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.user.userindex
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.Utils;
using System;

namespace Lottery.WebApp.user
{
    public partial class userindex : UserCenterSession
    {
        public string UserSum = "0";
        public string UserZsSum = "0";
        public string UserOnLineSum = "0";
        public string UserMoneySum = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");
            this.doh.Reset();
            this.doh.ConditionExpress = "usercode like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId + " and ISNULL(IsDel,0)=0";
            this.UserSum = string.Concat((object)this.doh.Count("N_User"));
            this.doh.Reset();
            this.doh.ConditionExpress = "ParentId=" + this.AdminId + " and ISNULL(IsDel,0)=0";
            this.UserZsSum = string.Concat((object)this.doh.Count("N_User"));
            this.doh.Reset();
            this.doh.ConditionExpress = "IsOnline=1 and usercode like '%" + Strings.PadLeft(this.AdminId) + "%'" + " and ISNULL(IsDel,0)=0";
            this.UserOnLineSum = string.Concat((object)this.doh.Count("Flex_User"));
            this.doh.Reset();
            this.doh.SqlCmd = "SELECT isnull(sum(money),0) as money FROM [N_User] with(nolock) where usercode like '%" + Strings.PadLeft(this.AdminId) + "%' and Id<>" + this.AdminId + " and ISNULL(IsDel,0)=0";
            this.UserMoneySum = Convert.ToDecimal(this.doh.GetDataTable().Rows[0]["money"].ToString()).ToString("0.00");
        }
    }
}
