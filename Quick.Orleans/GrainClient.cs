using Orleans;
using Orleans.Configuration;
using System;

namespace Merchant.Orleans
{
    public class GrainClient
    {
        private static IClusterClient client = null;

        private static object clientLock = new object();

        private GrainClient()
        {

        }

        public static IClusterClient Factory
        {
            get
            {
                lock (clientLock)
                {
                    if (client == null)
                    {
                        client = CreateClientBuilder();
                        client.Connect();
                    }
                    

                    return client;
                }
            }
        }

        private static IClusterClient CreateClientBuilder()
        {
            return new ClientBuilder()
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "MERCHANT";
                            options.ServiceId = "MERCHANT_SERVICE";
                        })
                        .Build();
        }
    }
}
