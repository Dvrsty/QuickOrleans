using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quick.Common.Helpers
{
    public class HttpHelper
    {
        public static Dictionary<string, string> GetObjectDictionary(object obj)
        {
            if (obj == null)
                return new Dictionary<string, string>();

            var type = obj.GetType();
            var properties = type.GetProperties();
            var dic = new Dictionary<string, string>();

            foreach (var p in properties)
            {
                dic.Add(p.Name, p.GetValue(obj)?.ToString());
            }

            return dic;
        }

        public static string GetHttpQueryString(Dictionary<string, string> keyValues)
        {
            if (keyValues == null || keyValues.Count == 0)
                return "";

            var stringBuilder = new StringBuilder();

            foreach (var val in keyValues)
            {
                if (!string.IsNullOrWhiteSpace(val.Value))
                    stringBuilder.Append(val.Key).Append("=").Append(val.Value).Append("&");
            }

            return stringBuilder.ToString().TrimEnd('&');
        }
    }
}
