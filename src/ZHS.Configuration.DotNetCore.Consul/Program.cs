using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Winton.Extensions.Configuration.Consul;

namespace ZHS.Configuration.DotNetCore.Consul
{
    public class Program
    {
        public static readonly CancellationTokenSource ConfigCancellationTokenSource = new CancellationTokenSource();

        public static Boolean EnableConsulConfig { get; private set; } = false;

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;
                    var tempConfigBuilder = config;
                    var tempConfig = tempConfigBuilder.Build();
                    EnableConsulConfig = Convert.ToBoolean(tempConfig["ConsulConfig:Enable"]);
                    if (EnableConsulConfig)
                    {
                        var key = $"{env.ApplicationName}.{env.EnvironmentName}";
                        if (!String.IsNullOrWhiteSpace(tempConfig["ConsulConfig:Key"]))
                        {
                            key = tempConfig["ConsulConfig:Key"];
                        }

                        if (String.IsNullOrWhiteSpace(tempConfig["ConsulConfig:Address"]))
                        {
                            throw new ArgumentException("ConsulConfig Address Must Be Not Null!");
                        }
                        config.AddConsul(key, ConfigCancellationTokenSource.Token, options =>
                        {
                            options.ConsulConfigurationOptions =
                                co => { co.Address = new Uri(tempConfig["ConsulConfig:Address"]); };
                            options.ReloadOnChange = Convert.ToBoolean(tempConfig["ConsulConfig:ReloadOnChange"]);
                            options.Optional = true;
                            options.OnLoadException = exceptionContext =>
                            {
                                exceptionContext.Ignore = true;
                            };
                        });
                    }
                })
                .UseStartup<Startup>();
    }
}
