using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZHS.Configuration.Core;

namespace ZHS.Configuration.DotNetCore
{
    public static class ConfigCoreExtension
    {

        public static IServiceCollection AddIConfigurationGeter(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationGeter, ConfigurationGetter>();
            return services;
        }

        public static IServiceCollection AddConfigModel(this IServiceCollection services)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IConfigModel))))
                .ToArray();
            foreach (var type in types)
            {
                services.AddScoped(type, provider =>
                {
                    var config = provider.GetService<IConfiguration>().GetSection(type.FullName).Get(type);
                    return config;
                });
            }
            return services;
        }

        public static IConfiguration AddConfigurationGeterLocator(this IConfiguration configuration)
        {
            ConfigurationGeterLocator.SetLocatorProvider(new ConfigurationGetter(configuration));
            return configuration;
        }
    }
}