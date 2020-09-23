using Quick.Cache;
using Quick.Cache.Redis;
using Quick.Core;
using Quick.Interface;
using Quick.IRepositories;
using Quick.Repositories;
using Quick.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace Quick.DI
{
    public static class QuickDi
    {
        public static IServiceCollection AddQuickServices(this IServiceCollection services)
        {
            services.AddTransient<IQuickContext, QuickContext>()
                    .AddTransient<ICacheManager, RedisCacheManager>()
                    .AddTransient<ITokenManager, TokenManager>()
                    .AddTransient<IRepositoryFactory, RepositoryFactory>()
                    
                    .AddTransient<IHelloRepository, HelloRepository>();
            return services;
        }
    }
}
