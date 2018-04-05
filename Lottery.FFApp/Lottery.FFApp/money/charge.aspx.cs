// Decompiled with JetBrains decompiler
// Type: Lottery.Web.money.charge
// Assembly: Lottery.FFApp, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: CD5F1C7F-2EB9-4806-9452-C9F3634A8986
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.FFApp.dll

using Lottery.DAL;
using System;

namespace Lottery.Web.money
{
  public partial class charge : UserCenterSession
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      this.Admin_Load("", "html");
    }
  }
}
