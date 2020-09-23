using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Quick.Common.Encrypt
{
    public class Md5Crypt
    {
        /// <summary>
        /// 生成MD5 hash码，返回32个字符的16进制格式字符串，默认UTF8编码，默认字符是小写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="encoding">编码</param>
        /// <param name="isUpperCase">是否大写</param>
        /// <returns></returns>
        public static string MD5(string input, Encoding encoding = null, bool isUpperCase = false)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(encoding.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            var str = sBuilder.ToString();
            if (isUpperCase)
                str = str.ToUpper();
            return str;

        }
    }
}
