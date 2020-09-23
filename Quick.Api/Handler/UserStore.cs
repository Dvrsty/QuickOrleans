using Quick.Cache;
using Quick.Common.Consts;
using System.Collections.Generic;
using System.Linq;

namespace Quick.Api.Handler
{
    /// <summary>
    ///  用户权限
    /// </summary>
    public class UserStore
    {
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public UserStore(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <summary>
        ///  权限检查
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool PermissionCheck(long userId, string permission)
        {
            var permissionKey = string.Format(RedisCacheKeyConsts.USER_PERMISSION, userId);
            var cacheData = _cacheManager.Get<IList<string>>(permissionKey);
            if (cacheData.Count <= 0)
                return false;
            return cacheData.Any(s => s.Equals(permission.ToUpper()));
        }
    }
}
