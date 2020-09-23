using CSRedis;
using Quick.Api.Swagger;
using Quick.Core;
using Quick.DI;
using Quick.Web.Framework;
using Quick.Web.Framework.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Quick.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //注入DbContext
            services.AddDbContext<QuickContext>(options => 
            {
                options.UseMySql(Configuration.GetConnectionString("DbConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //注入service
            services.AddQuickServices();

            //swagger
            if (Configuration.GetValue<bool?>("Swagger:Enable") == true)
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1.0", new OpenApiInfo { Title = "Quick API", Version = "v1.0" });
                    options.OperationFilter<AddTokenHeaderParameter>();
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Quick.Api.xml"), true);
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Quick.Dto.xml"), true);
                });
            }

            //redis缓存
            var redisConnection = Configuration.GetConnectionString("RedisConnection");
            var csredis = new CSRedisClient(redisConnection);
            RedisHelper.Initialization(csredis);

            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ApiResultAttribute)); //api接口返回类型统一处理
                //option.Filters.Add(typeof(UserAuthorizeFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy/MM/dd HH:mm:ss";
            });

            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            //权限
            //services.AddScoped<UserAuthorizeFilter>();

            //三方接口调用注入
            //services.AddHttpClient<ICommonClientService,CommonClientService>();
            services.AddHttpClient();
            services.AddControllers();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            if (Configuration.GetValue<bool?>("Swagger:Enable") == true)
            {
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.RoutePrefix = Configuration.GetValue<string>("Swagger:RoutePrefix") ?? "docs";
                    c.SwaggerEndpoint($"/swagger/v1.0/swagger.json", "API");
                    c.DocumentTitle = "Quick API";
                });
            }

            app.UseCors("AllowAllOrigins");

            // 检查是否有文件夹
            var baseDir = Path.Combine(Directory.GetCurrentDirectory(), @"static/upload");
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }

            //文件下载配置
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(baseDir),
                RequestPath = new PathString("/static/upload"),
                EnableDirectoryBrowsing = true
            });

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
