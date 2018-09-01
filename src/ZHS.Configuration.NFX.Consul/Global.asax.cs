using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Routing;
using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;
using ZHS.Configuration.NFX;

namespace ZHS.Configuration.NFX.Consul
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static readonly CancellationTokenSource ConfigCancellationTokenSource = new CancellationTokenSource();
        public static Boolean EnableConsulConfig { get; private set; } = false;

        protected void Application_Start()
        {
            AddConsul();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private static void AddConsul()
        {
            var appSettings = ConfigurationManager.AppSettings;
            EnableConsulConfig = Convert.ToBoolean(appSettings["ConsulConfig:Enable"]);
            if (EnableConsulConfig)
            {
                var key = "ZHS.Configuration.NFX.Consul.Development";
                if (!HostingEnvironment.IsDevelopmentEnvironment)
                {
                    key = "ZHS.Configuration.NFX.Consul.Production";
                }

                if (!String.IsNullOrWhiteSpace(appSettings["ConsulConfig:Key"]))
                {
                    key = appSettings["ConsulConfig:Key"];
                }

                if (String.IsNullOrWhiteSpace(appSettings["ConsulConfig:Address"]))
                {
                    throw new ArgumentException("ConsulConfig Address Must Be Not Null!");
                }

                var config = new ConfigurationBuilder();

                config.AddConsul(key, ConfigCancellationTokenSource.Token, options =>
                {
                    options.ConsulConfigurationOptions =
                        co => { co.Address = new Uri(appSettings["ConsulConfig:Address"]);  };
                    options.ReloadOnChange = Convert.ToBoolean(appSettings["ConsulConfig:ReloadOnChange"]);
                    options.Optional = true;
                    options.OnLoadException = exceptionContext => { exceptionContext.Ignore = false; };
                });
                //var test = config.Build();
                config.Build().AddConfigurationGeterLocator();
            }
        }
    }

}
