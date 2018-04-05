// Decompiled with JetBrains decompiler
// Type: Lottery.DBUtility.UI.PageUI
// Assembly: Lottery.DBUtility, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 41391965-66A5-4DE4-8203-13B298F4A572
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DBUtility.dll

using System;
using System.Web;
using System.Web.UI;

namespace Lottery.DBUtility.UI
{
  public abstract class PageUI : Page
  {
    public DbOperHandler doh;

    protected override void OnError(EventArgs e)
    {
      HttpContext current = HttpContext.Current;
      Exception lastError = current.Server.GetLastError();
      string s = "\r\n<pre>Offending URL: " + current.Request.Url.ToString() + "\r\nSource: " + lastError.Source + "\r\nMessage: " + lastError.Message + "\r\nStack trace: " + lastError.StackTrace + "</pre>";
      current.Response.Write(s);
      current.Server.ClearError();
      base.OnError(e);
    }

    public abstract void ConnectDb();

    protected override void OnInit(EventArgs e)
    {
      this.Unload += new EventHandler(this.Jbpage_Unload);
      base.OnInit(e);
    }

    public void Alert(string msg)
    {
      this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script language=\"javascript\">alert('" + msg + "')</script>");
    }

    public void Alert(string name, string msg)
    {
      this.ClientScript.RegisterClientScriptBlock(this.GetType(), name, "<script language=\"javascript\">alert('" + msg + "');</script>");
    }

    public TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
    {
      return new TimeSpan(DateTime1.Ticks).Subtract(new TimeSpan(DateTime2.Ticks)).Duration();
    }

    private void Jbpage_Unload(object sender, EventArgs e)
    {
      if (this.doh == null)
        return;
      this.doh.Dispose();
    }
  }
}
