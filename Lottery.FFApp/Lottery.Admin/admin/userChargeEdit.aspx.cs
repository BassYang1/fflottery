// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.userChargeEdit
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using Lottery.Utils;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
    public partial class userChargeEdit : AdminCenter
    {
        public string url = "";
        protected HtmlForm form1;
        protected Label lblSsid;
        protected Label lblUserName;
        protected Label lblPayMoney;
        protected TextBox txtPwd;
        protected Label lblMsg;
        protected TextBox txtId;
        protected TextBox txtUserId;
        protected TextBox txtTime;
        protected Button btnSave;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");
            string str = this.txtId.Text = this.Str2Str(this.q("id"));
            this.doh.Reset();
            this.doh.SqlCmd = "select top 1 *,dbo.f_GetUserName(UserID) as UserName from N_UserCharge where Id=" + str;
            DataTable dataTable = this.doh.GetDataTable();
            this.txtUserId.Text = dataTable.Rows[0]["UserId"].ToString();
            this.lblUserName.Text = dataTable.Rows[0]["UserName"].ToString();
            this.lblPayMoney.Text = dataTable.Rows[0]["InMoney"].ToString();
            this.lblSsid.Text = dataTable.Rows[0]["SsId"].ToString();
            this.txtTime.Text = dataTable.Rows[0]["STime"].ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=@Id";
            this.doh.AddConditionParameter("@Id", (object)this.AdminId);
            object field = this.doh.GetField("Sys_Admin", "Password");
            if (field == null)
                return;
            if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtPwd.Text)))
            {
                if (!new SFTDAL().Exists("PayRequestId ='" + this.lblSsid.Text + "'"))
                {
                    if (new SFTDAL().SavePayInfo(this.txtUserId.Text, "9999", this.lblSsid.Text, this.lblPayMoney.Text, Convert.ToDouble(this.lblPayMoney.Text).ToString(), this.txtTime.Text, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "成功") > 0)
                    {
                        new LogAdminOperDAL().SaveLog(this.AdminId, this.txtUserId.Text, "会员充值", "对" + this.lblSsid.Text + "进行补单，金额" + this.lblPayMoney.Text + "元");
                        this.FinalMessage("操作成功", "/admin/close.htm", 0);
                    }
                    else
                        this.lblMsg.Text = "补单失败！";
                }
                else
                    this.lblMsg.Text = "补单失败，此订单已到账！";
            }
            else
                this.lblMsg.Text = "安全密码错误！";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.doh.Reset();
            this.doh.ConditionExpress = "Id=@Id";
            this.doh.AddConditionParameter("@Id", (object)this.AdminId);
            object field = this.doh.GetField("Sys_Admin", "Password");
            if (field == null)
                return;
            if (field.ToString().ToLower() == MD5.Last64(MD5.Lower32(this.txtPwd.Text)))
            {
                if (!new SFTDAL().Exists("PayRequestId ='" + this.lblSsid.Text + "'"))
                {
                    if (new SFTDAL().SavePayInfo(this.lblSsid.Text) > 0)
                    {
                        new LogAdminOperDAL().SaveLog(this.AdminId, this.txtUserId.Text, "会员充值", "对" + this.lblSsid.Text + "注销补单，金额" + this.lblPayMoney.Text + "元");
                        this.FinalMessage("操作成功", "/admin/close.htm", 0);
                    }
                    else
                        this.lblMsg.Text = "注销补单失败！";
                }
                else
                    this.lblMsg.Text = "注销补单失败，此订单已到账！";
            }
            else
                this.lblMsg.Text = "安全密码错误！";
        }
    }
}
