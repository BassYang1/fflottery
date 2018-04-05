// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.PageBar
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System.Collections.Generic;
using System.Text;

namespace Lottery.Utils
{
  public static class PageBar
  {
    private static string getbar1(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='p_btns'>");
      if (totalCount > pageSize)
      {
        if (currentPage != 1)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>首页</a>");
          stringBuilder.Append("<a class='disabled'>上一页</a>");
        }
        if (stepNum > 0)
        {
          for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
          {
            if (chkPage == currentPage)
              stringBuilder.Append("<span class='active'>" + chkPage.ToString() + "</span>");
            else
              stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
            if (chkPage == pageCount)
              break;
          }
        }
        if (currentPage != pageCount)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>下一页</a>");
          stringBuilder.Append("<a class='disabled'>末页</a>");
        }
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbar2(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='p_btns'>");
      stringBuilder.Append("<span class='total_count'>共" + totalCount.ToString() + "条记录/" + pageCount.ToString() + "页&nbsp;</span>");
      if (totalCount > pageSize)
      {
        if (currentPage != 1)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>首页</a>");
          stringBuilder.Append("<a class='disabled'>上一页</a>");
        }
        if (stepNum > 0)
        {
          for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
          {
            if (chkPage == currentPage)
              stringBuilder.Append("<span class='active'>" + chkPage.ToString() + "</span>");
            else
              stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
            if (chkPage == pageCount)
              break;
          }
        }
        if (currentPage != pageCount)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>下一页</a>");
          stringBuilder.Append("<a class='disabled'>末页</a>");
        }
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbar3(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='page-info'>第" + (object) currentPage + "页/共" + (object) pageCount + "页 每页" + (object) pageSize + "条记录 共" + totalCount.ToString() + "条记录</div>");
      stringBuilder.Append("<div class='pages'>");
      if (currentPage != 1)
      {
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
      }
      else
      {
        stringBuilder.Append("<span>首页</span>");
        stringBuilder.Append("<span>上一页</span>");
      }
      if (pageRoot > 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<span class='active'>" + chkPage.ToString() + "</span>");
          else
            stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
          if (chkPage == pageCount)
            break;
        }
      }
      if (pageFoot < pageCount)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>.." + (object) pageCount + "</a>");
      if (currentPage != pageCount)
      {
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
      }
      else
      {
        stringBuilder.Append("<span>下一页</span>");
        stringBuilder.Append("<span>末页</span>");
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbar4(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int countNum, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage, string language)
    {
      Dictionary<string, object> entity = new LanguageHelp().GetEntity(language);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='p_btns'>");
      if (countNum > pageSize)
      {
        stringBuilder.Append("<span class='total_count'>" + ((string) entity["page_totalinfo"]).Replace("{totalcount}", countNum.ToString()).Replace("{currentpage}", currentPage.ToString()).Replace("{totalpage}", pageCount.ToString()) + "</span>");
        if (currentPage != 1)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>" + (string) entity["page_first"] + "</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>" + (string) entity["page_prev"] + "</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>" + (string) entity["page_first"] + "</a>");
          stringBuilder.Append("<a class='disabled'>" + (string) entity["page_prev"] + "</a>");
        }
        if (pageRoot > 1)
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
        if (stepNum > 0)
        {
          for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
          {
            if (chkPage == currentPage)
              stringBuilder.Append("<span class='active'>" + chkPage.ToString() + "</span>");
            else
              stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
            if (chkPage == pageCount)
              break;
          }
        }
        if (pageFoot < pageCount)
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>.." + (object) pageCount + "</a>");
        if (currentPage != pageCount)
        {
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>" + (string) entity["page_next"] + "</a>");
          stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>" + (string) entity["page_last"] + "</a>");
        }
        else
        {
          stringBuilder.Append("<a class='disabled'>" + (string) entity["page_next"] + "</a>");
          stringBuilder.Append("<a class='disabled'>" + (string) entity["page_last"] + "</a>");
        }
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbar6(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='pages'>");
      if (currentPage != 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上一页</a>");
      else
        stringBuilder.Append("<span>上一页</span>");
      if (currentPage != pageCount)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下一页</a>");
      else
        stringBuilder.Append("<span>下一页</span>");
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbarWebApp(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='pages'>");
      if (currentPage != 1)
      {
        stringBuilder.Append("<a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-first'></i></a>");
        stringBuilder.Append("<a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-prev'></i></a>");
      }
      else
      {
        stringBuilder.Append("<a href='#' class='first'><i class='icon icon-first'></i></a>");
        stringBuilder.Append("<a href='#' class='prev'><i class='icon icon-prev'></i></a>");
      }
      if (pageRoot > 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<a href='#' class='page current'>" + chkPage.ToString() + "</a>");
          else
            stringBuilder.Append("<a target='_self' class='page' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
          if (chkPage == pageCount)
            break;
        }
      }
      if (pageFoot < pageCount)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>.." + (object) pageCount + "</a>");
      if (currentPage != pageCount)
      {
        stringBuilder.Append("<a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-next'></i></a>");
        stringBuilder.Append("<a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'><i class='icon icon-last'></i></a>");
      }
      else
      {
        stringBuilder.Append("<a href='#' class='last'><i class='icon icon-last'></i></a>");
        stringBuilder.Append("<a href='#' class='next'><i class='icon icon-next'></i></a>");
      }
      stringBuilder.Append("</div>");
      stringBuilder.Append("<div class='page-info'>第" + (object) currentPage + "页/共" + (object) pageCount + "页 每页" + (object) pageSize + "条记录 共" + totalCount.ToString() + "条记录</div>");
      return stringBuilder.ToString();
    }

    private static string getbarFFApp(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='pages'>");
      if (currentPage != 1)
      {
        stringBuilder.Append("<a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>首页</a>");
        stringBuilder.Append("<a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>上页</a>");
      }
      else
      {
        stringBuilder.Append("<a href='#' class='first'>首页</a>");
        stringBuilder.Append("<a href='#' class='prev'>上页</a>");
      }
      if (pageRoot > 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<a href='#' class='page current'>" + chkPage.ToString() + "</a>");
          else
            stringBuilder.Append("<a target='_self' class='page' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
          if (chkPage == pageCount)
            break;
        }
      }
      if (pageFoot < pageCount)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>.." + (object) pageCount + "</a>");
      if (currentPage != pageCount)
      {
        stringBuilder.Append("<a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'>下页</a>");
        stringBuilder.Append("<a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'>末页</a>");
      }
      else
      {
        stringBuilder.Append("<a href='#' class='last'>下页</a>");
        stringBuilder.Append("<a href='#' class='next'>末页</a>");
      }
      stringBuilder.Append("</div>");
      stringBuilder.Append("<div class='page-info'>第" + (object) currentPage + "页/共" + (object) pageCount + "页 每页" + (object) pageSize + "条记录 共" + totalCount.ToString() + "条记录</div>");
      return stringBuilder.ToString();
    }

    private static string getbarHanGuo(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<ul><li>");
      if (currentPage != 1)
      {
        stringBuilder.Append("<span><a target='_self' class='first' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_01.gif' border='0'></a></span>&nbsp;");
        stringBuilder.Append("<span><a target='_self' class='prev' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_02.gif' border='0'></a></span>&nbsp;");
      }
      else
      {
        stringBuilder.Append("<span><a href='#' class='first'><img src='img/arrow_01.gif' border='0'></a></span>&nbsp;");
        stringBuilder.Append("<span><a href='#' class='prev'><img src='img/arrow_02.gif' border='0'></a></span>&nbsp;");
      }
      if (pageRoot > 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a></span>&nbsp;");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<span style='font-weight:bold;'><a href='#' class='now'>" + chkPage.ToString() + "</a></span>&nbsp;");
          else
            stringBuilder.Append("<span style='font-weight:bold;'><a target='_self' class='page' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a></span>&nbsp;");
          if (chkPage == pageCount)
            break;
        }
      }
      if (currentPage != pageCount)
      {
        stringBuilder.Append("<span><a target='_self' class='last' href='" + PageBar.GetPageUrl(currentPage + 1, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_03.gif' border='0'></a></span>&nbsp;");
        stringBuilder.Append("<span><a target='_self' class='next' href='" + PageBar.GetPageUrl(pageCount, Http1, HttpM, HttpN, limitPage) + "'><img src='img/arrow_04.gif' border='0'></a></span>&nbsp;");
      }
      else
      {
        stringBuilder.Append("<span><a href='#' class='last'><img src='img/arrow_03.gif' border='0'></a></span>&nbsp;");
        stringBuilder.Append("<span><a href='#' class='next'><img src='img/arrow_04.gif' border='0'></a></span>&nbsp;");
      }
      stringBuilder.Append("</li></ul>");
      return stringBuilder.ToString();
    }

    private static string getbarXinJiaPo(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='view view-aktualnosci view-id-aktualnosci view-display-id-page_1 view-dom-id-1'>");
      if (currentPage != 1)
        stringBuilder.Append("<div class='navigation'><div class='next'><a target='_self' class='active' href='" + PageBar.GetPageUrl(currentPage - 1, Http1, HttpM, HttpN, limitPage) + "'>next &gt;</a></div></div>");
      else
        stringBuilder.Append("<div class='navigation'><div class='next'><a href='#' class='active'>next &gt;</a></div></div>");
      stringBuilder.Append("<div class='navigation_li'><div class='item-list'><ul class='pager'>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<li class='pager-current'>" + chkPage.ToString() + "</li>");
          else
            stringBuilder.Append("<li class='pager-item'><a target='_self' class='active' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a></li>");
          if (chkPage == pageCount)
            break;
        }
      }
      stringBuilder.Append("</ul></div></div>");
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbarDongjing(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<div class='page'>");
      if (pageRoot > 1)
        stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<a href='#' class='pageHover'>" + chkPage.ToString() + "</a>");
          else
            stringBuilder.Append("<a target='_self' href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
          if (chkPage == pageCount)
            break;
        }
      }
      stringBuilder.Append("</div>");
      return stringBuilder.ToString();
    }

    private static string getbarNiuYue(string stype, int stepNum, int pageRoot, int pageFoot, int pageCount, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<ul><li>");
      if (currentPage != 1)
        stringBuilder.Append("<a href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "' class='prev'>Prev</span>");
      else
        stringBuilder.Append("<span class='prev'>Prev</span>");
      if (pageRoot > 1)
        stringBuilder.Append("<span><a target='_self' href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "'>1..</a></span>");
      if (stepNum > 0)
      {
        for (int chkPage = pageRoot; chkPage <= pageFoot; ++chkPage)
        {
          if (chkPage == currentPage)
            stringBuilder.Append("<span class='current'>" + (object) chkPage + "</span>");
          else
            stringBuilder.Append("<a href='" + PageBar.GetPageUrl(chkPage, Http1, HttpM, HttpN, limitPage) + "'>" + chkPage.ToString() + "</a>");
          if (chkPage == pageCount)
            break;
        }
      }
      if (currentPage != pageCount)
        stringBuilder.Append("<a href='" + PageBar.GetPageUrl(1, Http1, HttpM, HttpN, limitPage) + "' class='next'>Next</span>");
      else
        stringBuilder.Append("<span class='next'>Next</span>");
      stringBuilder.Append("</li></ul>");
      return stringBuilder.ToString();
    }

    public static string GetPageBar(int mode, string stype, int stepNum, int totalCount, int pageSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      string str = "";
      int num1 = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
      currentPage = currentPage > num1 ? num1 : currentPage;
      currentPage = currentPage < 1 ? 1 : currentPage;
      int num2 = stepNum * 2;
      int pageCount = num1 == 0 ? 1 : num1;
      int pageRoot;
      int pageFoot;
      if (pageCount - num2 < 1)
      {
        pageRoot = 1;
        pageFoot = pageCount;
      }
      else
      {
        int num3 = currentPage - stepNum > 1 ? currentPage - stepNum : 1;
        pageFoot = num3 + num2 > pageCount ? pageCount : num3 + num2;
        pageRoot = pageFoot - num2 < num3 ? pageFoot - num2 : num3;
      }
      switch (mode)
      {
        case 1:
          str = PageBar.getbar1(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 2:
          str = PageBar.getbar2(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 3:
          str = PageBar.getbar3(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 4:
          str = PageBar.getbar4(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage, "cn");
          break;
        case 5:
          str = PageBar.getbar4(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage, "en");
          break;
        case 6:
          str = PageBar.getbar6(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 80:
          str = PageBar.getbarFFApp(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 81:
          str = PageBar.getbarFFApp(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 1004:
          str = PageBar.getbarNiuYue(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 1010:
          str = PageBar.getbarHanGuo(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 1012:
          str = PageBar.getbarXinJiaPo(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
        case 1016:
          str = PageBar.getbarDongjing(stype, stepNum, pageRoot, pageFoot, pageCount, totalCount, pageSize, currentPage, Http1, HttpM, HttpN, limitPage);
          break;
      }
      return str;
    }

    public static string GetPageBar(int mode, string stype, int stepNum, int totalCount, int pageSize, int currentPage, string HttpN)
    {
      return PageBar.GetPageBar(mode, stype, stepNum, totalCount, pageSize, currentPage, HttpN, HttpN, HttpN, 0);
    }

    public static string GetPageUrl(int chkPage, string Http1, string HttpM, string HttpN, int limitPage)
    {
      string empty = string.Empty;
      return (chkPage != 1 ? (chkPage > limitPage || limitPage == 0 ? HttpN : HttpM) : Http1).Replace("<#page#>", chkPage.ToString());
    }
  }
}
