using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Common.Consts
{
    public class RedisCacheKeyConsts
    {
        /// <summary>
        ///  发送验证码
        /// </summary>
        public const string SMSLOGINORFINDPWD = "tel:smscode:{0}";

        /// <summary>
        ///  用户权限
        /// </summary>
        public const string USER_PERMISSION = "user:permission:{0}";

        /// <summary>
        /// 企查查查询缓存
        /// 0: 接口名称
        /// 1: 查询参数相加之后的md5值
        /// </summary>
        public const string QCC_QUERY_SEARCH = "qcc:query:{0}:{1}";
    }
}
