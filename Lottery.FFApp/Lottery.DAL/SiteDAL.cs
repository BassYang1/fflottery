// Decompiled with JetBrains decompiler
// Type: Lottery.DAL.SiteDAL
// Assembly: Lottery.DAL, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 7C79BA5B-21B3-40F1-B96A-84E656E22E29
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DAL.dll

using Lottery.DBUtility;
using Lottery.Entity;
using Lottery.Utils;
using System;
using System.Collections;
using System.Data;
using System.Web;

namespace Lottery.DAL
{
  public class SiteDAL : ComData
  {
    public void GetListJSON(ref string _jsonstr)
    {
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "select top 1 * from Sys_Info";
        DataTable dataTable = dbOperHandler.GetDataTable();
        _jsonstr = this.ConverTableToJSON(dataTable);
        dataTable.Clear();
        dataTable.Dispose();
      }
    }

    public SiteModel CreateEntity()
    {
      SiteModel siteModel = new SiteModel();
      using (DbOperHandler dbOperHandler = new ComData().Doh())
      {
        dbOperHandler.Reset();
        dbOperHandler.SqlCmd = "SELECT * FROM [Sys_Info] where Id=1";
        DataRow row = dbOperHandler.GetDataTable().Rows[0];
        siteModel.Name = "时时彩";
        siteModel.Dir = App.Path;
        siteModel.WebIsOpen = Convert.ToInt32(row["WebIsOpen"].ToString());
        siteModel.WebCloseSeason = row["WebCloseSeason"].ToString();
        siteModel.ZHIsOpen = Convert.ToInt32(row["ZHIsOpen"].ToString());
        siteModel.RegIsOpen = Convert.ToInt32(row["RegIsOpen"].ToString());
        siteModel.BetIsOpen = Convert.ToInt32(row["BetIsOpen"].ToString());
        siteModel.CSUrl = row["CSUrl"].ToString();
        siteModel.SignMinTotal = Convert.ToInt32(row["SignMinTotal"].ToString());
        siteModel.SignMaxTotal = Convert.ToInt32(row["SignMaxTotal"].ToString());
        siteModel.SignNum = Convert.ToInt32(row["SignNum"].ToString());
        siteModel.WarnTotal = Convert.ToDecimal(row["WarnTotal"].ToString());
        siteModel.MaxBet = Convert.ToDecimal(row["MaxBet"].ToString());
        siteModel.MaxWin = Convert.ToDecimal(row["MaxWin"].ToString());
        siteModel.MaxWinFK = Convert.ToDecimal(row["MaxWinFK"].ToString());
        siteModel.MaxLevel = Convert.ToDecimal(row["MaxLevel"].ToString());
        siteModel.MinCharge = Convert.ToDecimal(row["MinCharge"].ToString());
        siteModel.Points = Convert.ToInt32(row["Points"].ToString());
        siteModel.PriceOutCheck = Convert.ToDecimal(row["PriceOutCheck"].ToString());
        siteModel.PriceOut = Convert.ToDecimal(row["PriceOut"].ToString());
        siteModel.PriceOut2 = Convert.ToDecimal(row["PriceOut2"].ToString());
        siteModel.PriceNum = Convert.ToInt32(row["PriceNum"].ToString());
        siteModel.PriceOutPerson = Convert.ToInt32(row["PriceOutPerson"].ToString());
        siteModel.AutoLottery = Convert.ToInt32(row["AutoLottery"].ToString());
        siteModel.ProfitModel = Convert.ToInt32(row["ProfitModel"].ToString());
        siteModel.ProfitMargin = Convert.ToInt32(row["ProfitMargin"].ToString());
        siteModel.AutoRanking = Convert.ToInt32(row["AutoRanking"].ToString());
        siteModel.PriceTime1 = row["PriceTime1"].ToString();
        siteModel.PriceTime2 = row["PriceTime2"].ToString();
        siteModel.BankTime = Convert.ToDecimal(row["BankTime"].ToString());
        siteModel.ClientVersion = row["ClientVersion"].ToString();
        siteModel.UpdateTime = Convert.ToDateTime(row["UpdateTime"].ToString());
        siteModel.NewsUpdateTime = Convert.ToDateTime(row["NewsUpdateTime"].ToString());
        siteModel.Version = row["ClientVersion"].ToString();
      }
      return siteModel;
    }

    public SiteModel GetEntity()
    {
      return new SiteModel()
      {
        Name = XmlCOM.ReadXml("~/WEB-INF/site", "Name"),
        Dir = App.Path,
        WebIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "WebIsOpen")),
        WebCloseSeason = XmlCOM.ReadXml("~/WEB-INF/site", "WebCloseSeason"),
        ZHIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ZHIsOpen")),
        RegIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "RegIsOpen")),
        BetIsOpen = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "BetIsOpen")),
        CSUrl = XmlCOM.ReadXml("~/WEB-INF/site", "CSUrl"),
        SignMinTotal = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignMinTotal")),
        SignMaxTotal = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignMaxTotal")),
        SignNum = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "SignNum")),
        WarnTotal = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "WarnTotal")),
        MaxBet = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxBet")),
        MaxWin = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxWin")),
        MaxWinFK = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxWinFK")),
        MaxLevel = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MaxLevel")),
        MinCharge = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "MinCharge")),
        Points = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "Points")),
        PriceOutCheck = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOutCheck")),
        PriceOut = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOut")),
        PriceOut2 = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOut2")),
        PriceNum = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "PriceNum")),
        PriceOutPerson = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "PriceOutPerson")),
        AutoLottery = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "AutoLottery")),
        ProfitModel = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ProfitModel")),
        ProfitMargin = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "ProfitMargin")),
        AutoRanking = Convert.ToInt32(XmlCOM.ReadXml("~/WEB-INF/site", "AutoRanking")),
        PriceTime1 = XmlCOM.ReadXml("~/WEB-INF/site", "PriceTime1"),
        PriceTime2 = XmlCOM.ReadXml("~/WEB-INF/site", "PriceTime2"),
        BankTime = Convert.ToDecimal(XmlCOM.ReadXml("~/WEB-INF/site", "BankTime")),
        ClientVersion = XmlCOM.ReadXml("~/WEB-INF/site", "ClientVersion"),
        UpdateTime = Convert.ToDateTime(XmlCOM.ReadXml("~/WEB-INF/site", "UpdateTime")),
        NewsUpdateTime = Convert.ToDateTime(XmlCOM.ReadXml("~/WEB-INF/site", "NewsUpdateTime")),
        CookieDomain = XmlCOM.ReadXml("~/WEB-INF/site", "CookieDomain"),
        CookiePath = XmlCOM.ReadXml("~/WEB-INF/site", "CookiePath"),
        CookiePrev = XmlCOM.ReadXml("~/WEB-INF/site", "CookiePrev"),
        CookieKeyCode = XmlCOM.ReadXml("~/WEB-INF/site", "CookieKeyCode"),
        Version = XmlCOM.ReadXml("~/WEB-INF/site", "Version"),
        DebugKey = XmlCOM.ReadXml("~/WEB-INF/site", "DebugKey")
      };
    }

    public void CreateSiteConfig()
    {
      SiteModel entity = this.CreateEntity();
      XmlControl xmlControl = new XmlControl(HttpContext.Current.Server.MapPath("~/WEB-INF/site.xml"));
      xmlControl.Update("Root/Name", entity.Name);
      xmlControl.Update("Root/Dir", entity.Dir);
      xmlControl.Update("Root/WebIsOpen", string.Concat((object) entity.WebIsOpen));
      xmlControl.Update("Root/WebCloseSeason", entity.WebCloseSeason);
      xmlControl.Update("Root/ZHIsOpen", string.Concat((object) entity.ZHIsOpen));
      xmlControl.Update("Root/RegIsOpen", string.Concat((object) entity.RegIsOpen));
      xmlControl.Update("Root/BetIsOpen", string.Concat((object) entity.BetIsOpen));
      xmlControl.Update("Root/CSUrl", entity.CSUrl);
      xmlControl.Update("Root/SignMinTotal", string.Concat((object) entity.SignMinTotal));
      xmlControl.Update("Root/SignMaxTotal", string.Concat((object) entity.SignMaxTotal));
      xmlControl.Update("Root/SignNum", string.Concat((object) entity.SignNum));
      xmlControl.Update("Root/WarnTotal", string.Concat((object) entity.WarnTotal));
      xmlControl.Update("Root/MaxBet", string.Concat((object) entity.MaxBet));
      xmlControl.Update("Root/MaxWin", string.Concat((object) entity.MaxWin));
      xmlControl.Update("Root/MaxWinFK", string.Concat((object) entity.MaxWinFK));
      xmlControl.Update("Root/MaxLevel", string.Concat((object) entity.MaxLevel));
      xmlControl.Update("Root/MinCharge", string.Concat((object) entity.MinCharge));
      xmlControl.Update("Root/Points", string.Concat((object) entity.Points));
      xmlControl.Update("Root/PriceOutCheck", string.Concat((object) entity.PriceOutCheck));
      xmlControl.Update("Root/PriceOut", string.Concat((object) entity.PriceOut));
      xmlControl.Update("Root/PriceOut2", string.Concat((object) entity.PriceOut2));
      xmlControl.Update("Root/PriceNum", string.Concat((object) entity.PriceNum));
      xmlControl.Update("Root/PriceTime1", entity.PriceTime1);
      xmlControl.Update("Root/PriceTime2", entity.PriceTime2);
      xmlControl.Update("Root/BankTime", string.Concat((object) entity.BankTime));
      xmlControl.Update("Root/PriceOutPerson", string.Concat((object) entity.PriceOutPerson));
      xmlControl.Update("Root/ClientVersion", entity.ClientVersion);
      xmlControl.Update("Root/UpdateTime", string.Concat((object) entity.UpdateTime));
      xmlControl.Update("Root/NewsUpdateTime", string.Concat((object) entity.NewsUpdateTime));
      xmlControl.Update("Root/AutoLottery", string.Concat((object) entity.AutoLottery));
      xmlControl.Update("Root/ProfitModel", string.Concat((object) entity.ProfitModel));
      xmlControl.Update("Root/ProfitMargin", string.Concat((object) entity.ProfitMargin));
      xmlControl.Update("Root/AutoRanking", string.Concat((object) entity.AutoRanking));
      xmlControl.Update("Root/Version", entity.Version);
      xmlControl.Save();
      xmlControl.Dispose();
    }

    public void CreateSiteFiles()
    {
      SiteModel entity = this.GetEntity();
      string empty = string.Empty;
      string str1 = "var site = new Object();\r\nsite.Dir = '" + entity.Dir + "';\r\nsite.WebIsOpen = '" + (object) entity.WebIsOpen + "';\r\nsite.WebCloseSeason = '" + entity.WebCloseSeason + "';\r\nsite.ZHIsOpen = '" + (object) entity.ZHIsOpen + "';\r\nsite.RegIsOpen = '" + (object) entity.RegIsOpen + "';\r\nsite.BetIsOpen = '" + (object) entity.BetIsOpen + "';\r\nsite.CSUrl = '" + entity.CSUrl + "';\r\nsite.SignMinTotal = '" + (object) entity.SignMinTotal + "';\r\nsite.SignMaxTotal = '" + (object) entity.SignMaxTotal + "';\r\nsite.SignNum = '" + (object) entity.SignNum + "';\r\nsite.WarnTotal = '" + (object) entity.WarnTotal + "';\r\nsite.MaxBet = '" + (object) entity.MaxBet + "';\r\nsite.MaxWin = '" + (object) entity.MaxWin + "';\r\nsite.MaxLevel = '" + (object) entity.MaxLevel + "';\r\nsite.MinCharge = '" + (object) entity.MinCharge + "';\r\nsite.Points = '" + (object) entity.Points + "';\r\nsite.PriceOutCheck = '" + (object) entity.PriceOutCheck + "';\r\nsite.PriceOut = '" + (object) entity.PriceOut + "';\r\nsite.PriceOut2 = '" + (object) entity.PriceOut2 + "';\r\nsite.PriceNum = '" + (object) entity.PriceNum + "';\r\nsite.PriceTime1 = '" + entity.PriceTime1 + "';\r\nsite.PriceTime2 = '" + entity.PriceTime2 + "';\r\nsite.BankTime = '" + (object) entity.BankTime + "';\r\nsite.PriceOutPerson = '" + (object) entity.PriceOutPerson + "';\r\nsite.ClientVersion = '" + entity.ClientVersion + "';\r\nsite.UpdateTime = '" + (object) entity.UpdateTime + "';\r\nsite.NewsUpdateTime = '" + (object) entity.NewsUpdateTime + "';\r\nsite.AutoLottery = '" + (object) entity.AutoLottery + "';\r\nsite.ProfitModel = '" + (object) entity.ProfitModel + "';\r\nsite.ProfitMargin = '" + (object) entity.ProfitMargin + "';\r\nsite.AutoRanking = '" + (object) entity.AutoRanking + "';\r\nsite.CookieDomain = '" + entity.CookieDomain + "';\r\nsite.CookiePath = '" + entity.CookiePath + "';\r\nsite.CookiePrev = '" + entity.CookiePrev + "';\r\nsite.CookieKeyCode = '" + entity.CookieKeyCode + "';\r\nsite.Version = '" + entity.Version + "';\r\n";
      string str2 = DirFile.ReadFile("~/statics/global.js");
      string strStart = "//<!--网站参数begin";
      string strEnd = "//-->网站参数end";
      ArrayList htmls = Strings.GetHtmls(str2, strStart, strEnd, true, true);
      if (htmls.Count > 0)
        str2 = str2.Replace(htmls[0].ToString(), strStart + "\r\n\r\n" + str1 + "\r\n\r\n" + strEnd);
      DirFile.SaveFile(str2, "~/statics/global.js");
    }

    public SiteModel GetSite()
    {
      if (HttpContext.Current.Application["Site"] == null)
      {
        HttpContext.Current.Application.Lock();
        HttpContext.Current.Application["Site"] = (object) new conSite().GetEntity();
        HttpContext.Current.Application.UnLock();
      }
      return (SiteModel) HttpContext.Current.Application["Site"];
    }
  }
}
