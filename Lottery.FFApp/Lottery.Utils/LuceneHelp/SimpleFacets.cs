// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.LuceneHelp.SimpleFacets
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using Lucene.Net.Search;
using Lucene.Net.Util;
using System.Collections.Generic;

namespace Lottery.Utils.LuceneHelp
{
  public class SimpleFacets
  {
    public static void Facet(BooleanQuery bq, IndexSearcher s, string field, Dictionary<string, int> dics)
    {
      StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);
      int[] c = new int[stringIndex.lookup.Length];
      SimpleFacets.FacetCollector facetCollector = new SimpleFacets.FacetCollector(c, stringIndex);
      s.Search((Query) bq, (HitCollector) facetCollector);
      SimpleFacets.DictionaryEntryQueue dictionaryEntryQueue = new SimpleFacets.DictionaryEntryQueue(stringIndex.lookup.Length);
      for (int index = 1; index < stringIndex.lookup.Length; ++index)
      {
        if (c[index] > 0 && stringIndex.lookup[index] != null && stringIndex.lookup[index] != "0")
          dictionaryEntryQueue.Insert((object) new SimpleFacets.FacetEntry(stringIndex.lookup[index], -c[index]));
      }
      for (int index = dictionaryEntryQueue.Size() - 1; index >= 0; --index)
      {
        SimpleFacets.FacetEntry facetEntry = dictionaryEntryQueue.Pop() as SimpleFacets.FacetEntry;
        dics.Add(facetEntry.Value, -facetEntry.Count);
      }
    }

    public static Dictionary<string, int> Facet(Query query, IndexSearcher s, string field)
    {
      StringIndex stringIndex = FieldCache_Fields.DEFAULT.GetStringIndex(s.GetIndexReader(), field);
      int[] c = new int[stringIndex.lookup.Length];
      SimpleFacets.FacetCollector facetCollector = new SimpleFacets.FacetCollector(c, stringIndex);
      s.Search(query, (HitCollector) facetCollector);
      SimpleFacets.DictionaryEntryQueue dictionaryEntryQueue = new SimpleFacets.DictionaryEntryQueue(stringIndex.lookup.Length);
      for (int index = 1; index < stringIndex.lookup.Length; ++index)
      {
        if (c[index] > 0 && stringIndex.lookup[index] != null && stringIndex.lookup[index] != "0")
          dictionaryEntryQueue.Insert((object) new SimpleFacets.FacetEntry(stringIndex.lookup[index], -c[index]));
      }
      int num = dictionaryEntryQueue.Size();
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      for (int index = num - 1; index >= 0; --index)
      {
        SimpleFacets.FacetEntry facetEntry = dictionaryEntryQueue.Pop() as SimpleFacets.FacetEntry;
        dictionary.Add(facetEntry.Value, -facetEntry.Count);
      }
      return dictionary;
    }

    private sealed class DictionaryEntryQueue : PriorityQueue
    {
      internal DictionaryEntryQueue(int size)
      {
        this.Initialize(size);
      }

      public override bool LessThan(object a, object b)
      {
        return ((SimpleFacets.FacetEntry) a).Count < ((SimpleFacets.FacetEntry) b).Count;
      }
    }

    private class FacetEntry
    {
      private int count;
      private string value;

      public FacetEntry(string v, int c)
      {
        this.value = v;
        this.count = c;
      }

      public int Count
      {
        get
        {
          return this.count;
        }
        set
        {
          this.count = value;
        }
      }

      public string Value
      {
        get
        {
          return this.value;
        }
        set
        {
          this.value = value;
        }
      }
    }

    private class FacetCollector : HitCollector
    {
      private int[] counter;
      private StringIndex si;

      public FacetCollector(int[] c, StringIndex s)
      {
        this.counter = c;
        this.si = s;
      }

      public override void Collect(int doc, float score)
      {
        ++this.counter[this.si.order[doc]];
      }
    }
  }
}
