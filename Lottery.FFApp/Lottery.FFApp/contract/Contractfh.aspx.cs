// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.contract.Contractfh
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL.Flex;
using Lottery.Entity;
using System;

namespace Lottery.WebApp.contract
{
    public partial class Contractfh : Lottery.DAL.UserCenterSession
    {
        /// <summary>
        /// 会员Id
        /// </summary>
        public int UserId = 0;

        /// <summary>
        /// 会员名称
        /// </summary>
        public string UserName = "0";

        /// <summary>
        /// 会员级别
        /// </summary>
        public int UserGroup = 0;

        /// <summary>
        /// 会员级别名称
        /// </summary>
        public string UserGroupName = "0";

        /// <summary>
        /// 是否是管理员账户
        /// </summary>
        public Boolean IsAdminUser = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");

            //会员信息
            Int32.TryParse(this.AdminId, out UserId);
            UserModel user = (new UserDAL()).GetUserInfo(UserId);

            if (user != null)
            {
                this.UserGroup = user.UserGroup;
                this.UserGroupName = user.UserGroupName;
                this.UserName = user.UserName;
                this.IsAdminUser = (user.UserGroup >= 5 && user.ParentId == 0);
            }
        }
    }
}
