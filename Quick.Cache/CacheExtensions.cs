using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Cache
{
    public static class CacheExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly object _syncObject = new object();

        /// <summary>
        /// 获取缓存,如果缓存不存在,则通过Func加载缓存
        /// 默认为60分钟
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire">如果缓存不存在,通过该方法加载缓存数据</param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, TimeSpan.FromMinutes(60), acquire);
        }

        /// <summary>
        /// 获取缓存,如果缓存不存在,则通过Func加载缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime">缓存时间,单位(分钟)</param>
        /// <param name="acquire">如果缓存不存在,通过该方法加载缓存数据</param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, TimeSpan cacheTime, Func<T> acquire)
        {
            lock (_syncObject)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var result = acquire();
                cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }

        /// <summary>
        /// 获取缓存,如果缓存不存在,则通过Func加载缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire">如果缓存不存在,通过该方法加载缓存数据</param>
        /// <returns></returns>
        public async static Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, Func<Task<T>> acquire)
        {
            return await GetAsync(cacheManager, key, TimeSpan.FromMinutes(60), acquire);
        }

        /// <summary>
        /// 获取缓存,如果缓存不存在,则通过Func加载缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime">缓存时间</param>
        /// <param name="acquire">如果缓存不存在,通过该方法加载缓存数据</param>
        /// <returns></returns>
        public async static Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, TimeSpan cacheTime, Func<Task<T>> acquire)
        {
            if (cacheManager.IsSet(key))
            {
                return await cacheManager.GetAsync<T>(key);
            }

            var result = await acquire();
            await cacheManager.SetAsync(key, result, cacheTime);
            return result;
        }
    }
}
