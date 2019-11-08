using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ServiceBase.API.Redis
{
    public static partial class StartupExtensions
    {
        public static IServiceCollection EnableRedis(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var settings = configuration.GetSection("Redis").Get<RedisSettings>();

            var provider = services.BuildServiceProvider();
            var environment = provider.GetService<IHostingEnvironment>();

            //if (environment.IsProduction())
            //{
            //    // get the username and password from Vault
            //    
            //}

            services.AddDistributedRedisCache(options =>
            {
                options.ConfigurationOptions = ConfigurationOptions.Parse(settings.ConnectionString, false);
            });

            return services;
        }

    }
}
