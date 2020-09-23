using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quick.Api.Attributes
{
    /// <summary>
    ///  filter注入
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //public class ServiceFilterAttribute : Attribute, IFilterFactory, IFilterMetadata, IOrderedFilter
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="type"></param>
    //    public ServiceFilterAttribute(Type type);

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int Order { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public Type ServiceType { get; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public bool IsReusable { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="serviceProvider"></param>
    //    /// <returns></returns>
    //    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider);
    //}
}
