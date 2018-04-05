// Decompiled with JetBrains decompiler
// Type: Lottery.FFApp.page
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using System;
using System.Web.UI;

namespace Lottery.FFApp
{
    public partial class page : Page
    {
        public string k = "b";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Request.QueryString["k"] ?? ""))
            {
                return;
            }

            this.k = this.Request.QueryString["k"];
        }
    }
}
