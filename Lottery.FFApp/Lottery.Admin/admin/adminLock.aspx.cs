// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.adminLock
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using Lottery.DAL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lottery.Admin
{
  public partial class adminLock : AdminCenter
  {
    protected HtmlForm form1;
    protected TextBox txtOldPass;

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }
  }
}
