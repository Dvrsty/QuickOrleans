using Mapster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Extensions
{
    public static class ObjectMapExtensions
    {
        public static T MapTo<T>(this object obj)
        {
            if (obj == null)
                return default(T);

            return obj.Adapt<T>();
        }

        public static IList<T> MapToList<T>(this IEnumerable list)
        {
            if (list == null)
                return new List<T>();

            var newList = new List<T>();
            foreach (var item in list)
            {
                newList.Add(item.MapTo<T>());
            }

            return newList;
        }
    }
}
