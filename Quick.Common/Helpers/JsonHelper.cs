using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Helpers
{
    public class JsonHelper
    {
        public static string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), DateFormatString = "yyyy-MM-dd HH:mm:ss" });
        }

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject);
        }
    }
}
