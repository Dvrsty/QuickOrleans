using Quick.Cache;
using Quick.Core.Runtime.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quick.Api.Filters
{
    /// <summary>
    ///  权限过滤
    /// </summary>
    public class UserAuthorizeFilter : ActionFilterAttribute
    {
        //private readonly ICacheManager _cacheManager;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        //public UserAuthorizeFilter(ICacheManager cacheManager)
        //{
        //    _cacheManager = cacheManager;
        //}

        public UserAuthorizeFilter(string name)
        {
            Name = name;
        }

        /// <summary>
        ///  权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = Name;
            var token = filterContext.HttpContext.Request.Headers["token"].ToString();
            if (string.IsNullOrEmpty(token))
                throw new MyException("权限不足");

        }
    }
}
