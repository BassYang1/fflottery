// Decompiled with JetBrains decompiler
// Type: Lottery.EMWeb.plus.GetNumber
// Assembly: Lottery.IPhone, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 6A500EAD-7331-41B6-AA69-9D37BCA394DC
// Assembly location: F:\pros\tianheng\bf\Iphone\bin\Lottery.IPhone.dll

using Lottery.DAL;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;

namespace Lottery.EMWeb.plus
{
  public partial class GetNumber : Page
  {
    private string strNumberUrl = ConfigurationManager.AppSettings["NumberUrl"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
      this.Response.ContentType = "text/html; charset=utf-8";
      this.Response.Write(GetNumber.GetHtml(this.strNumberUrl + "/Data/GetJsonData.aspx?lid=" + this.Request.QueryString["lid"].ToString() + "&callback=" + this.Request.QueryString["callback"].ToString()));
    }

    public static string GetHtml(string Url)
    {
      string str = "";
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.UserAgent = "MSIE";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        str = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd();
      }
      catch
      {
        new LogExceptionDAL().Save("采集异常", "数据源地址：" + Url);
      }
      return str;
    }
  }
}
