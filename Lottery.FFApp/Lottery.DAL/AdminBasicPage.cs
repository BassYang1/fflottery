// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.AdminBasicPage
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.DBUtility.UI;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Lottery.DAL
{
  public class AdminBasicPage : PageUI
  {
    protected SiteModel site = new SiteModel();

    protected override void OnInit(EventArgs e)
    {
      this.Server.ScriptTimeout = 90;
      this.ConnectDb();
      this.site = new conSite().GetSite();
      base.OnInit(e);
    }

    public string ORDER_BY_RND()
    {
      return "newid()";
    }

    public override void ConnectDb()
    {
      if (this.doh != null)
        return;
      try
      {
        this.doh = (DbOperHandler) new SqlDbOperHandler(new SqlConnection(Const.ConnectionString));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void CloseDB()
    {
      if (this.doh == null)
        return;
      this.doh.Dispose();
    }

    public void CheckClientIP()
    {
    }

    public string GetRandomNumberString(int int_NumberLength)
    {
      return this.GetRandomNumberString(int_NumberLength, false);
    }

    public string GetRandomNumberString(int int_NumberLength, bool onlyNumber)
    {
      Random random = new Random();
      return this.GetRandomNumberString(int_NumberLength, onlyNumber, random);
    }

    public string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random)
    {
      string str = "123456789";
      if (!onlyNumber)
        str += "abcdefghjkmnpqrstuvwxyz";
      char[] charArray = str.ToCharArray();
      string empty = string.Empty;
      for (int index = 0; index < int_NumberLength; ++index)
        empty += charArray[random.Next(0, charArray.Length)].ToString();
      return empty;
    }

    public string GetProductOrderNum()
    {
      return DateTime.Now.ToString("yyyyMMddHHmmss") + this.GetRandomNumberString(4, true);
    }

    public bool CheckFormUrl(bool checkHost)
    {
      if (this.q("debugkey") == this.site.DebugKey)
        return true;
      if (HttpContext.Current.Request.UrlReferrer == (Uri) null)
      {
        this.SaveVisitLog(2, 0);
        return false;
      }
      if (!checkHost || !(HttpContext.Current.Request.UrlReferrer.Host != HttpContext.Current.Request.Url.Host))
        return true;
      this.SaveVisitLog(2, 0);
      return false;
    }

    public bool CheckFormUrl()
    {
      return this.CheckFormUrl(true);
    }

    protected void FinalMessage(string pageMsg, string go2Url, int BackStep)
    {
      this.FinalMessage(pageMsg, go2Url, BackStep, 2);
    }

    protected void FinalMessage(string pageMsg, string go2Url, int BackStep, int Seconds)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n");
      stringBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'>\r\n");
      stringBuilder.Append("<head>\r\n");
      stringBuilder.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />\r\n");
      stringBuilder.Append("<title>系统提示</title>\r\n");
      stringBuilder.Append("<style>\r\n");
      stringBuilder.Append("body {padding:0; margin:0; }\r\n");
      stringBuilder.Append("#info{padding:0; margin:0;position: absolute;width:320px;height:120px;margin-top:-60px;margin-left:-160px; left:50%;top:50%; border:0px #B4E0F7 solid; text-align:center;}\r\n");
      stringBuilder.Append("</style>\r\n");
      stringBuilder.Append("<script language=\"javascript\">\r\n");
      stringBuilder.Append("var seconds=" + (object) Seconds + ";\r\n");
      stringBuilder.Append("for(i=1;i<=seconds;i++)\r\n");
      stringBuilder.Append("{window.setTimeout(\"update(\" + i + \")\", i * 1000);}\r\n");
      stringBuilder.Append("function update(num)\r\n");
      stringBuilder.Append("{\r\n");
      stringBuilder.Append("if(num == seconds)\r\n");
      if (BackStep > 0)
        stringBuilder.Append("{ history.go(" + (object) -BackStep + "); }\r\n");
      else if (go2Url != "")
        stringBuilder.Append("{ self.location.href='" + go2Url + "'; }\r\n");
      else
        stringBuilder.Append("{window.close();}\r\n");
      stringBuilder.Append("else\r\n");
      stringBuilder.Append("{ }\r\n");
      stringBuilder.Append("}\r\n");
      stringBuilder.Append("</script>\r\n");
      stringBuilder.Append("<base target='_self' />\r\n");
      stringBuilder.Append("</head>\r\n");
      stringBuilder.Append("<body>\r\n");
      stringBuilder.Append("<div id='info'>\r\n");
      stringBuilder.Append("<div style='text-align:center;margin:0 auto;width:320px; line-height:26px;height:26px;font-weight:bold;color:#444444;font-size:14px;border:1px #D1D1D1 solid;background:#F5F5F5;'>提示信息</div>\r\n");
      stringBuilder.Append("<div style='text-align:center;padding:20px 0 20px 0;margin:0 auto;width:320px;font-size:14px;background:#FFFFFF;border-right:1px #D1D1D1 solid;border-bottom:1px #D1D1D1 solid;border-left:1px #D1D1D1 solid;'>\r\n");
      stringBuilder.Append(pageMsg + "<br /><br />\r\n");
      if (BackStep > 0)
        stringBuilder.Append("        <a href=\"javascript:history.go(" + (object) -BackStep + ")\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
      else
        stringBuilder.Append("        <a href=\"" + go2Url + "\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
      stringBuilder.Append("    </div>\r\n");
      stringBuilder.Append("</div>\r\n");
      stringBuilder.Append("</body>\r\n");
      stringBuilder.Append("</html>\r\n");
      HttpContext.Current.Response.Write(stringBuilder.ToString());
      HttpContext.Current.Response.End();
    }

    public int Int_ThisPage()
    {
      return this.Str2Int(this.q("page"), 0) < 1 ? 1 : this.Str2Int(this.q("page"), 0);
    }

    public bool ExecuteSqlInFile(string pathToScriptFile)
    {
      return ExecuteSqlBlock.Go("1", HttpContext.Current.Application["Lottery_dbConnStr"].ToString(), pathToScriptFile);
    }

    public static string JoinFields(string _fields)
    {
      if (_fields.Trim().Length == 0)
        return "";
      return "," + _fields;
    }

    public void SavePageLog(int _second)
    {
      this.SaveVisitLog(1, _second);
    }

    public void SaveVisitLog(int _type, int _second)
    {
      this.SaveVisitLog(_type, _second, "");
    }

    public void SaveVisitLog(int _type, int _second, string _logfilename)
    {
      if (_type == 1)
      {
        string path = _logfilename == "" ? "~/statics/log/vister_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
        if (DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds <= (double) _second)
          return;
        StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath(path), true, Encoding.UTF8);
        streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        streamWriter.WriteLine("\tIP 地 址：" + Const.GetUserIp);
        streamWriter.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
        streamWriter.WriteLine("\t耗    时：" + ((float) DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds).ToString("0.000") + "秒");
        streamWriter.WriteLine("\t地    址：" + this.ServerUrl() + Const.GetCurrentUrl);
        streamWriter.WriteLine("---------------------------------------------------------------------------------------------------");
        streamWriter.Close();
        streamWriter.Dispose();
      }
      else
      {
        StreamWriter streamWriter = new StreamWriter(HttpContext.Current.Server.MapPath(_logfilename == "" ? "~/statics/log/hacker_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename), true, Encoding.UTF8);
        streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        streamWriter.WriteLine("\tIP 地 址：" + Const.GetUserIp);
        streamWriter.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
        streamWriter.WriteLine("\t来    源：" + Const.GetRefererUrl);
        streamWriter.WriteLine("\t地    址：" + this.ServerUrl() + Const.GetCurrentUrl);
        streamWriter.WriteLine("---------------------------------------------------------------------------------------------------");
        streamWriter.Close();
        streamWriter.Dispose();
      }
    }

    protected string ServerUrl()
    {
      if (HttpContext.Current.Request.ServerVariables["Server_Port"].ToString() == "80")
        return "http://" + HttpContext.Current.Request.Url.Host;
      return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.ServerVariables["Server_Port"].ToString();
    }

    public string RandomStr(int Num)
    {
      string empty = string.Empty;
      Random random = new Random();
      for (int index = 0; index < Num; ++index)
      {
        char ch = (char) (48U + (uint) (ushort) (random.Next() % 10));
        empty += ch.ToString();
      }
      return empty;
    }

    protected void SetupSystemDate()
    {
      this.site = new SiteDAL().GetEntity();
      HttpContext.Current.Application.Lock();
      HttpContext.Current.Application["AdminLottery"] = (object) this.site;
      HttpContext.Current.Application.UnLock();
    }

    protected void WriteJs(string sType, string jsContent)
    {
      if (sType == "-1")
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "writejs", jsContent, true);
      else
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "writejs", jsContent, true);
    }

    protected string JsonResult(int success, string str)
    {
      return "{\"result\" :\"" + success.ToString() + "\",\"returnval\" :\"" + str + "\"}";
    }

    protected string p__HighLight(string PageStr, string keys)
    {
      string[] strArray = keys.Split(new string[1]{ " " }, StringSplitOptions.None);
      for (int index = 0; index < strArray.Length; ++index)
        PageStr = PageStr.Replace(strArray[index].Trim(), "<font color=#C60A00>" + strArray[index].Trim() + "</font>");
      return PageStr;
    }

    protected string HighLightKeyWord(string pain, string keys)
    {
      string str = pain + "%%%%%%";
      if (keys.Length < 1)
        return str;
      string[] strArray = keys.Split(new string[1]{ " " }, StringSplitOptions.None);
      if (strArray.Length < 1)
        return str;
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        MatchCollection matchCollection = Regex.Matches(str, strArray[index1].Trim(), RegexOptions.IgnoreCase);
        for (int index2 = 0; index2 < matchCollection.Count; ++index2)
          str = str.Insert(matchCollection[index2].Index + strArray[index1].Trim().Length + index2 * 9, "</em>").Insert(matchCollection[index2].Index + index2 * 9, "<em>");
      }
      return Strings.Left(str, str.Length - 6);
    }

    public string getListName(string sName, string sCode)
    {
      int num = sCode.Length / 4 - 1;
      string str = "";
      if (num > 0)
      {
        for (int index = 0; index < num; ++index)
          str += "├－";
      }
      return str + sName;
    }

    public string PageList(int mode, int totalCount, int PSize, int currentPage, string[] FieldName, string[] FieldValue)
    {
      string str1 = HttpContext.Current.Request.ServerVariables["Script_Name"].ToString();
      string str2 = "";
      for (int index = 0; index < FieldName.Length; ++index)
        str2 = str2 + FieldName[index].ToString() + "=" + FieldValue[index].ToString() + "&";
      string HttpN = str1 + "?" + str2 + "page=<#page#>";
      return PageBar.GetPageBar(mode, "html", 0, totalCount, PSize, currentPage, HttpN);
    }

    public string AutoPageBar(int mode, int stepNum, int totalCount, int PSize, int currentPage)
    {
      string HttpN = this.GetUrlPrefix() + "<#page#>";
      return PageBar.GetPageBar(mode, "html", stepNum, totalCount, PSize, currentPage, HttpN);
    }

    public string GetUrlPrefix()
    {
      HttpRequest request = HttpContext.Current.Request;
      string serverVariable = HttpContext.Current.Request.ServerVariables["Url"];
      if (HttpContext.Current.Request.QueryString.Count == 0 || HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))
        return serverVariable + "?page=";
      string[] strArray = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[1]
      {
        "page="
      }, StringSplitOptions.None);
      if (strArray.Length == 1)
        return serverVariable + "?" + strArray[0] + "&page=";
      return serverVariable + "?" + strArray[0] + "page=";
    }

    public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string HttpN)
    {
      return PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, HttpN);
    }

    public string getPageBar(int mode, string stype, int stepNum, int totalCount, int PSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      return PageBar.GetPageBar(mode, stype, stepNum, totalCount, PSize, currentPage, Http1, HttpM, HttpN, limitPage);
    }

    public string q(string s)
    {
      if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
        return Strings.SafetyQueryS(HttpContext.Current.Request.QueryString[s].ToString());
      return string.Empty;
    }

    public string f(string s)
    {
      if (HttpContext.Current.Request.Form[s] != null && HttpContext.Current.Request.Form[s] != "")
        return HttpContext.Current.Request.Form[s].ToString();
      return string.Empty;
    }

    public int Str2Int(string s, int t)
    {
      return Validator.StrToInt(s, t);
    }

    public int Str2Int(string s)
    {
      return this.Str2Int(s, 0);
    }

    public string Str2Str(string s)
    {
      return Validator.StrToInt(s, 0).ToString();
    }

    protected int GetStringLen(string str)
    {
      return Encoding.UTF8.GetBytes(str).Length;
    }

    protected string GetCutString(string str, int Length)
    {
      Length *= 2;
      byte[] bytes = Encoding.Default.GetBytes(str);
      if (bytes.Length <= Length)
        return str;
      return Encoding.Default.GetString(bytes, 0, Length);
    }

    protected void SaveJsFile(string TxtStr, string TxtFile)
    {
      this.SaveJsFile(TxtStr, TxtFile, "2");
    }

    protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
    {
      Encoding encoding = Encoding.Default;
      switch (Edcode)
      {
        case "3":
          encoding = Encoding.Unicode;
          break;
        case "2":
          encoding = Encoding.UTF8;
          break;
        case "1":
          encoding = Encoding.GetEncoding("GB2312");
          break;
      }
      DirFile.CreateFolder(DirFile.GetFolderPath(false, TxtFile));
      StreamWriter streamWriter = new StreamWriter(TxtFile, false, encoding);
      streamWriter.Write("/*本文件由系统于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
      streamWriter.Close();
    }

    protected void SaveCssFile(string TxtStr, string TxtFile)
    {
      this.SaveCssFile(TxtStr, TxtFile, "2");
    }

    protected void SaveCssFile(string TxtStr, string TxtFile, string Edcode)
    {
      Encoding encoding = Encoding.Default;
      switch (Edcode)
      {
        case "3":
          encoding = Encoding.Unicode;
          break;
        case "2":
          encoding = Encoding.UTF8;
          break;
        case "1":
          encoding = Encoding.GetEncoding("GB2312");
          break;
      }
      DirFile.CreateFolder(DirFile.GetFolderPath(false, TxtFile));
      StreamWriter streamWriter = new StreamWriter(TxtFile, false, encoding);
      streamWriter.Write("/*本文件由系统于 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
      streamWriter.Close();
    }

    protected void SaveCacheFile(string CacheStr, string OutFile)
    {
      this.SaveCacheFile(CacheStr, OutFile, "2");
    }

    protected void SaveCacheFile(string CacheStr, string OutFile, string Edcode)
    {
      Encoding encoding = Encoding.Default;
      switch (Edcode)
      {
        case "3":
          encoding = Encoding.Unicode;
          break;
        case "2":
          encoding = Encoding.UTF8;
          break;
        case "1":
          encoding = Encoding.GetEncoding("GB2312");
          break;
      }
      DirFile.CreateFolder(DirFile.GetFolderPath(false, OutFile));
      try
      {
        StreamWriter streamWriter = new StreamWriter(OutFile, false, encoding);
        streamWriter.Write(CacheStr);
        streamWriter.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool SendMobileMessage(string _ReceiveMobiles, string _Content)
    {
      smsHelp.SendSMS(_ReceiveMobiles, _Content + "[" + this.site.Name + "]");
      return true;
    }
  }
}
