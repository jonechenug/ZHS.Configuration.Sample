using Microsoft.Extensions.Configuration;
using ZHS.Configuration.Core;

namespace ZHS.Configuration.NFX
{
    public static class ConfigNFXExtension
    {
        public static IConfiguration AddConfigurationGeterLocator(this IConfiguration configuration)
        {
            ConfigurationGeterLocator.SetLocatorProvider(new ConfigurationGetter(configuration));
            return configuration;
        }
    }
}