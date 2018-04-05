// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.contract.ContractgzPop
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL.Flex;
using Lottery.Entity;
using System;
using System.Data;

namespace Lottery.WebApp.contract
{
    public partial class ContractgzPop : Lottery.DAL.UserCenterSession
    {
        /// <summary>
        /// 签约下级Id
        /// </summary>
        public int SubUserId = 0;

        /// <summary>
        /// 签约下级会员名称
        /// </summary>
        public string UserName = "0";

        /// <summary>
        /// 签约下级用户级别
        /// </summary>
        public int UserGroup = 0;

        /// <summary>
        /// 签约下级用户级别名称
        /// </summary>
        public string UserGroupName = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");

            if (this.IsPostBack)
            {
                return;
            }

            Int32.TryParse(this.q("id"), out SubUserId);

            //最大工资百分比
            //this.MaxGZPercent = (new UserContractDAL()).GetMaxPercent(String.IsNullOrEmpty(this.AdminId) ? 0 : Convert.ToInt32(this.AdminId), 2);

            //签约用户信息
            UserModel user = (new UserDAL()).GetUserInfo(this.SubUserId);

            if (user != null)
            {
                this.UserGroup = user.UserGroup;
                this.UserGroupName = user.UserGroupName;
                this.UserName = user.UserName;
            }
        }
    }
}
