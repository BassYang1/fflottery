// Decompiled with JetBrains decompiler
// Type: Lottery.Admin.Temp
// Assembly: Lottery.Admin, Version=7.0.1.203, Culture=neutral, PublicKeyToken=null
// MVID: 838B9BD2-8091-4C2A-B624-E2A206486676
// Assembly location: F:\pros\tianheng\bf\admin\bin\Lottery.Admin.dll

using System;
using System.Configuration;
using System.Web.UI;

namespace Lottery.Admin
{
  public partial class Temp : Page
  {
    public string strRootUrl = "";
    public string strIphoneUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
      this.strRootUrl = ConfigurationManager.AppSettings["RootUrl"].ToString();
      this.strIphoneUrl = ConfigurationManager.AppSettings["IphoneUrl"].ToString();
    }
  }
}
