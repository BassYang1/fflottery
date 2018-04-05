// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.user.useradd
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.WebApp.user
{
    public partial class useradd : UserCenterSession
    {
        protected HtmlForm form;
        protected DropDownList ddlType;
        protected Label lblPoint2;
        protected Label lblPoint3;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Admin_Load("", "html");
            this.getUserGroupDropDownList(ref this.ddlType, 0);
            if (Convert.ToDecimal(this.AdminPoint) < new Decimal(131))
            {
                this.lblPoint2.Text = "可分配范围0-" + (object)(Convert.ToDecimal(this.AdminPoint) / new Decimal(10));
                this.lblPoint3.Text = "可分配范围0-" + (object)(Convert.ToDecimal(this.AdminPoint) / new Decimal(10));
            }
            else
            {
                this.lblPoint2.Text = "可分配范围0-" + (object)(Convert.ToDecimal(this.AdminPoint) / new Decimal(10) - Convert.ToDecimal(0.1));
                this.lblPoint3.Text = "可分配范围0-" + (object)(Convert.ToDecimal(this.AdminPoint) / new Decimal(10) - Convert.ToDecimal(0.1));
            }
        }
    }
}
