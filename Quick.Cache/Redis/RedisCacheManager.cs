using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Cache.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            return RedisHelper.Get<T>(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await RedisHelper.GetAsync<T>(key);
        }

        public bool IsSet(string key)
        {
            return RedisHelper.Exists(key);
        }

        public void Remove(string key)
        {
            RedisHelper.Del(key);
        }

        public void Set(string key, object data, TimeSpan cacheTime)
        {
            RedisHelper.Set(key, data, Convert.ToInt32(cacheTime.TotalSeconds));
        }

        public async Task SetAsync(string key, object data, TimeSpan cacheTime)
        {
            await RedisHelper.SetAsync(key, data, Convert.ToInt32(cacheTime.TotalSeconds));
        }
    }
}
