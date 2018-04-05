// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.LuceneHelp.SearchIndex
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Web;

namespace Lottery.Utils.LuceneHelp
{
  public class SearchIndex
  {
    public static int GetCount(string type, string channelid, string classid, string year, string keywords, string groupname, out Dictionary<string, int> groupAggregate)
    {
      if (keywords.Length == 0)
        keywords = "jUmBoT";
      DateTime now = DateTime.Now;
      string[] strArray = type.Split(',');
      int length = strArray.Length;
      IndexSearcher[] indexSearcherArray = new IndexSearcher[length];
      for (int index = 0; index < length; ++index)
        indexSearcherArray[index] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/data/index/" + strArray[index] + "/"));
      MultiSearcher multiSearcher = new MultiSearcher((Searchable[]) indexSearcherArray);
      BooleanQuery booleanQuery = new BooleanQuery();
      if (channelid != "0")
      {
          TermQuery termQuery = new TermQuery(new Term("channelid", channelid));
        booleanQuery.Add((Query) termQuery, BooleanClause.Occur.MUST);
      }
      if (Validator.StrToInt(year, 0) > 1900)
      {
          TermQuery termQuery = new TermQuery(new Term("nameof", year));
        booleanQuery.Add((Query) termQuery, BooleanClause.Occur.MUST);
      }
      Query query = new MultiFieldQueryParser(new string[5]
      {
        "title",
        "tags",
        "summary",
        "content",
        "fornull"
      }, (Analyzer) new StandardAnalyzer()).Parse(keywords);
      booleanQuery.Add(query, BooleanClause.Occur.MUST);
      Hits hits = multiSearcher.Search((Query) booleanQuery);
      groupAggregate = length != 1 ? (Dictionary<string, int>) null : SimpleFacets.Facet((Query) booleanQuery, indexSearcherArray[0], groupname);
      return hits.Length();
    }

    public static List<SearchItem> Search(string type, string channelid, string classid, string year, string keywords, int pageLen, int pageNo, out int recCount, out double eventTime)
    {
      if (keywords.Length == 0)
        keywords = "jUmBoT";
      DateTime now = DateTime.Now;
      string[] strArray = type.Split(',');
      int length = strArray.Length;
      IndexSearcher[] indexSearcherArray = new IndexSearcher[length];
      for (int index = 0; index < length; ++index)
        indexSearcherArray[index] = new IndexSearcher(HttpContext.Current.Server.MapPath("~/data/index/" + strArray[index] + "/"));
      MultiSearcher multiSearcher = new MultiSearcher((Searchable[]) indexSearcherArray);
      BooleanQuery booleanQuery = new BooleanQuery();
      if (channelid != "0")
      {
          TermQuery termQuery = new TermQuery(new Term("channelid", channelid));
        booleanQuery.Add((Query) termQuery, BooleanClause.Occur.MUST);
      }
      if (Validator.StrToInt(year, 0) > 1900)
      {
          TermQuery termQuery = new TermQuery(new Term("year", year));
        booleanQuery.Add((Query) termQuery, BooleanClause.Occur.MUST);
      }
      Query query = new MultiFieldQueryParser(new string[5]
      {
        "title",
        "tags",
        "summary",
        "content",
        "fornull"
      }, (Analyzer) new StandardAnalyzer()).Parse(keywords);
      booleanQuery.Add(query, BooleanClause.Occur.MUST);
      Sort sort = new Sort(new SortField((string) null, 1, true));
      Hits hits = multiSearcher.Search((Query) booleanQuery, sort);
      List<SearchItem> searchItemList = new List<SearchItem>();
      recCount = hits.Length();
      if (recCount > 0)
      {
        int n = (pageNo - 1) * pageLen;
        while (n < recCount && searchItemList.Count < pageLen)
        {
          SearchItem searchItem = (SearchItem) null;
          try
          {
            searchItem = new SearchItem();
            searchItem.Id = hits.Doc(n).Get("id");
            searchItem.ChannelId = hits.Doc(n).Get("channelid");
            searchItem.ClassId = hits.Doc(n).Get("classid");
            searchItem.TableName = hits.Doc(n).Get("tablename");
            searchItem.Img = hits.Doc(n).Get("img");
            searchItem.Title = hits.Doc(n).Get("title");
            searchItem.Summary = hits.Doc(n).Get("summary");
            searchItem.Tags = hits.Doc(n).Get("tags");
            searchItem.Url = hits.Doc(n).Get("url");
            searchItem.AddDate = hits.Doc(n).Get("adddate");
            searchItem.Year = hits.Doc(n).Get("year");
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
          finally
          {
            searchItemList.Add(searchItem);
            ++n;
          }
        }
        multiSearcher.Close();
        TimeSpan timeSpan = DateTime.Now - now;
        eventTime = (double) Convert.ToInt16(timeSpan.TotalMilliseconds);
        return searchItemList;
      }
      TimeSpan timeSpan1 = DateTime.Now - now;
      eventTime = (double) Convert.ToInt16(timeSpan1.TotalMilliseconds);
      return (List<SearchItem>) null;
    }
  }
}
