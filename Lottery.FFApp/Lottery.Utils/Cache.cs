// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.Cache
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Web;
using System.Web.Caching;

namespace Lottery.Utils
{
  public static class Cache
  {
    public static void Insert(string strCacheName, string strValue, int iExpires, int priority)
    {
      TimeSpan timeSpan = new TimeSpan(0, 0, iExpires);
      CacheItemPriority priority1;
      switch (priority)
      {
        case 1:
          priority1 = CacheItemPriority.NotRemovable;
          break;
        case 2:
          priority1 = CacheItemPriority.High;
          break;
        case 3:
          priority1 = CacheItemPriority.AboveNormal;
          break;
        case 4:
          priority1 = CacheItemPriority.Normal;
          break;
        case 5:
          priority1 = CacheItemPriority.BelowNormal;
          break;
        case 6:
          priority1 = CacheItemPriority.Low;
          break;
        default:
          priority1 = CacheItemPriority.Normal;
          break;
      }
      HttpContext.Current.Cache.Insert(strCacheName, (object) strValue, (CacheDependency) null, DateTime.Now.Add(timeSpan), System.Web.Caching.Cache.NoSlidingExpiration, priority1, (CacheItemRemovedCallback) null);
    }

    public static string Get(string strCacheName)
    {
      return HttpContext.Current.Cache[strCacheName].ToString();
    }

    public static void Del(string strCacheName)
    {
      HttpContext.Current.Cache.Remove(strCacheName);
    }
  }
}
