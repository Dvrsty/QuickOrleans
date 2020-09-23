using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Quick.Api.Attributes
{
    ///// <summary>
    /////  用户权限
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    //public class UserAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    //{
    //    /// <summary>
    //    ///  当前权限
    //    /// </summary>
    //    public string Name { get; set; }

    //    /// <summary>
    //    ///  
    //    /// </summary>
    //    public UserAuthorizeAttribute(string name)
    //    {
    //        Name = name;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    //    {
    //        var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
    //        var authorizationResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, new UserAuthorizeRequirement(Name));
    //        if (!authorizationResult.Succeeded)
    //        {
    //            context.Result = new ForbidResult();
    //        }
    //    }

    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public class UserAuthorizeRequirement : IAuthorizationRequirement
    //{
    //    /// <summary>
    //    ///  权限
    //    /// </summary>
    //    public string Permission { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public UserAuthorizeRequirement(string permission)
    //    {
    //        Permission = permission;
    //    }
    //}

    //public class UserAuthorizeAttribute 
}
