using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quick.Common.Helpers;
using Quick.Common.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Quick.Tokens
{
    public class TokenManager : ITokenManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        public TokenManager(IConfiguration configuration)
        {
            Secret = configuration.GetValue<string>("Token:Secret");
        }

        private readonly string Secret;

        public string GenerateToken<T>(T value, TimeSpan? expire = null)
        {
            // 生成json数据
            var jsonData = JsonHelper.SerializeObject(value);
            // 生成二进制字节数组
            var symmetricKey = Convert.FromBase64String(Secret);
            // 创建一个JwtSecurityTokenHandler类用来生成Token
            var tokenHandler = new JwtSecurityTokenHandler();
            // 获取当前时间
            var now = DateTime.UtcNow;
            // 生成过期时间
            var expireDate = null as DateTime?;
            if (expire != null)
            {
                expireDate = now.Add(expire.Value);
            }
            // 创建一个 Token 的原始对象
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(QuickClaimsType.Data, jsonData),
                }),
                // 设置Token有效期
                Expires = expireDate,
                // 生成Token证书
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        public T DeserializeToken<T>(string token)
        {
            var simplePrinciple = GetPrincipal(token);

            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return default(T);
            }

            if (!!!identity.IsAuthenticated)
            {
                return default(T);
            }

            var data = identity.FindFirst(QuickClaimsType.Data);

            if (data == null)
            {
                return default(T);
            }

            if (string.IsNullOrWhiteSpace(data.Value))
            {
                return default(T);
            }

            return JsonHelper.DeserializeObject<T>(data.Value);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                // 将字符串token解码成token对象
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return null;
                }
                // 生成编码对应的字节数组
                var symmetricKey = Convert.FromBase64String(Secret);
                // 生成验证token的参数
                var validationParameters = new TokenValidationParameters()
                {
                    // token是否包含有效期
                    RequireExpirationTime = true,
                    // 验证秘钥发行人，如果要验证在这里指定发行人字符串即可
                    ValidateIssuer = false,
                    // 验证秘钥的接受人，如果要验证在这里提供接收人字符串即可
                    ValidateAudience = false,
                    // 生成token时的安全秘钥
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };
                // 接受解码后的token对象
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                // 返回秘钥的主体对象，包含秘钥的所有相关信息
                return principal;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetType().Name);
            }
        }
    }
}
