using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
namespace Keylab.Utils {
    public static class Cache {
        static ObjectCache cache = MemoryCache.Default;
        /// <summary>
        /// 获取 key=>value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key) {
            return cache.Get(key);
        }
        /// <summary>
        /// 缓存 key=>value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="elem"></param>
        public static void Set(string key, object elem) {
            cache.Set(key, elem, DateTime.Now.AddSeconds(3600));
        }
        /// <summary>
        /// 判断是否包含此 key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Has(string key) {
            return cache.Contains(key);
        }
        /// <summary>
        /// 删除 key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Remove(string key) {
            return cache.Remove(key);
        }
    }
}
