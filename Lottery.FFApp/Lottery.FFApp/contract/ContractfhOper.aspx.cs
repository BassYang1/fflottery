// Decompiled with JetBrains decompiler
// Type: Lottery.WebApp.contract.ContractfhOper
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;

namespace Lottery.WebApp.contract
{
  public partial class ContractfhOper : UserCenterSession
  {
    public string userId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
      if (this.Request.QueryString["id"] == null)
        return;
      this.userId = this.Request.QueryString["id"].ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }
  }
}
