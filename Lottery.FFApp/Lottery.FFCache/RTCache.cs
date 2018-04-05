using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Lottery.FFCache
{
    /// <summary>
    /// .NET运行时缓存
    /// </summary>
    public static class RTCache
    {
        private static ObjectCache _cache = MemoryCache.Default;

        /// <summary>
        /// 获取Cache
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object Get(string key)
        {

            if (_cache.Contains(key) == false)
            {
                return null;
            }

            return _cache[key];
        }

        /// <summary>
        /// 添加缓存项
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiredMins">过期时间(单位分), 0默认不删除缓存</param>
        public static void Insert(string key, object value, long expiredMins = 0)
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            if (expiredMins == 0)
            {
                policy.Priority = CacheItemPriority.NotRemovable;
            }
            else
            {
                policy.AbsoluteExpiration = DateTime.Now.AddMinutes(expiredMins);//取得或设定值，这个值会指定是否应该在指定期间过后清除
            }

            _cache.Set(key, value, policy);
        }

        /// <summary>
        /// 删除缓存项
        /// </summary>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            if (_cache.Contains(key) == false)
            {
                _cache.Remove(key);
            }
        }
    }
}
