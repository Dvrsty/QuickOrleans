using System;

namespace Quick.Tokens
{
    public interface ITokenManager
    {
        /// <summary>
        /// 生成Token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">数据</param>
        /// <param name="expireMinutes">过期时间,如果该时间未设置或设置为null,表示永久有效</param>
        /// <returns></returns>
        string GenerateToken<T>(T value, TimeSpan? expire = null);
        /// <summary>
        /// 解析token数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        T DeserializeToken<T>(string token);
    }
}
