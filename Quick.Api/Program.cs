using Quick.Core;
using Quick.Core.Runtime.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Net;
using Quick.Core.Data;
using Quick.Service;

namespace Quick.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configName = "appsettings.json";
#if DEBUG
            configName = "appsettings.Development.json";
#endif

            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile(configName, optional: true)
                 .Build();
            var host = CreateWebHostBuilder(args, config).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<QuickContext>();

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        // Ç¨ÒÆÊý¾Ý¿â
                        context.Database.Migrate();
                    }

                    QuickSeedData.Init(context);
                }
                catch (Exception ex)
                {
                    throw new MyException(ex.Message);
                }
            };
            host.Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder
                     .UseStartup<Startup>();
                 })
                 .UseOrleans(builder =>
                 {
                     builder
                         .UseLocalhostClustering()
                         .ConfigureEndpoints(config.GetValue<int?>("Ports:OrleansSiloPort") ?? 11111, config.GetValue<int?>("Ports:OrleansGetewayPort") ?? 33333)
                         .UseDashboard(options =>
                         {
                             options.Port = config.GetValue<int?>("Ports:OrleansDashboardPort") ?? 8999;
                             options.HostSelf = config.GetValue<bool?>("OrleansDashboard:HostSelf") ?? false;
                             options.Host = config.GetValue<string>("OrleansDashboard:Host");
                             options.Username = config.GetValue<string>("OrleansDashboard:Username");
                             options.Password = config.GetValue<string>("OrleansDashboard:Password");
                             options.CounterUpdateIntervalMs = config.GetValue<int?>("OrleansDashboard:CounterUpdateIntervalMs") ?? 5000;
                         })
                         .Configure<ClusterOptions>(options =>
                         {
                             options.ClusterId = config.GetValue<string>("Orleans:ClusterId");
                             options.ServiceId = config.GetValue<string>("Orleans:ServiceId");
                         })
                         //.UseRedisClustering(options =>
                         //{
                         //    options.ConnectionString = config.GetValue<string>("Orleans:MembershipRedisConnection");
                         //    options.Database = config.GetValue<int?>("Orleans:MembershipRedisDatabase") ?? 0;
                         //})
                         //.AddRedisGrainStorage("Redis", optionsBuilder => optionsBuilder.Configure(options =>
                         //{
                         //    options.DataConnectionString = config.GetValue<string>("Orleans:GrainStorageRedisConnection");
                         //    options.DatabaseNumber = config.GetValue<int?>("Orleans:GrainStorageRedisDatabase") ?? 0;
                         //}))
                         .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                         .ConfigureApplicationParts(parts =>
                         {
                             parts.AddApplicationPart(typeof(HelloService).Assembly).WithReferences();
                         });
                 });
    }
}
