using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHS.Configuration.Core;
using Newtonsoft.Json;

namespace ZHS.Configuration.NFX.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("my.json", optional: true, reloadOnChange: true)
                                .AddInMemoryCollection(new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String,String>("myString","myString"),
                    new KeyValuePair<String,String>("otherString","otherString")
                });
            IConfiguration config = configurationBuilder.Build();
            String myString = config["myString"]; //myString
            TestConfig testConfig = config.GetSection("TestConfig").Get<TestConfig>();
            var length = testConfig.Starship.Length;//304.8
            Console.WriteLine($"myString:{myString}");
            Console.WriteLine($"myString:{JsonConvert.SerializeObject(testConfig)}");
            Console.ReadKey();
            
        }
    }
}
