using Quick.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Quick.Api.Controllers
{
    /// <summary>
    ///  基础管理
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前连接IP地址
        /// </summary>
        public string UserIp =>
            Request.Headers["X-Real-IP"].FirstOrDefault() ??
            Request.HttpContext.Connection.RemoteIpAddress.ToString();

        /// <summary>
        /// 当前请求token
        /// </summary>
        public string CurrentToken =>
            Request.Headers["Token"].FirstOrDefault();
    }

    /// <summary>
    ///  用户基础管理
    /// </summary>
    public class UserBaseController : BaseController
    {
        private readonly ITokenManager _tokenManager;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenManager"></param>
        public UserBaseController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        /// <summary>
        ///  当前登录用户信息
        /// </summary>
        //public UsersSimpleDto CurrentUser => _tokenManager.DeserializeToken<UsersSimpleDto>(CurrentToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            //if (string.IsNullOrWhiteSpace(CurrentToken))
            //    throw new MyException("登录已过期,请重新登录");

            //if (CurrentUser == null)
            //    throw new MyException("登录已过期,请重新登录");
        }
    }
}
