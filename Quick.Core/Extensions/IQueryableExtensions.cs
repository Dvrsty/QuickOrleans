using Quick.Common.Dtos;
using Quick.Common.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<IPagedOutput<T>> Paged<T, TOrder>(this IQueryable<T> query, Expression<Func<T, TOrder>> order, int pageIndex, int pageSize, bool isDesc = true) where T : class
        {
            var output = new PagedOutput<T>();
            output.TotalCount = await query.CountAsync();

            if (isDesc)
            {
                query = query.OrderByDescending(order);
            }
            else
            {
                query = query.OrderBy(order);
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            output.Items = await query.ToListAsync();



            return output;
        }
    }

}
