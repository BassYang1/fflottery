// Decompiled with JetBrains decompiler
// Type: Lottery.FFApp.Login
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using Lottery.DBUtility;
using Lottery.Utils;
using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Lottery.FFApp
{
    public partial class Login : UserCenterSession
    {
        public string CheckMsg { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string merchantId = this.q("merchantId");
            string userName = this.q("userName");
            string signKey = this.q("signKey");

            if (ChkLoginWebApp(merchantId, userName, signKey))
            {
                Response.Redirect("/", true);
            }
        }

        public bool ChkLoginWebApp(string merchantId, string userName, string signKey)
        {
            if (string.IsNullOrEmpty(merchantId))
            {
                this.CheckMsg = "商户Id不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(userName))
            {
                this.CheckMsg = "会员名称不能为空";
                return false;
            }
            if (string.IsNullOrEmpty(signKey))
            {
                this.CheckMsg = "签名不能为空";
                return false;
            }

            try
            {
                (new UserDAL()).ChkLoginMerchantApp(merchantId, userName, signKey);
            }
            catch (InvalidOperationException e)
            {
                this.CheckMsg = e.Message;
                return false;
            }

            return true;
        }
    }
}
