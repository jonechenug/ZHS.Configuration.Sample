using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ZHS.Configuration.Core;

namespace ZHS.Configuration.DotNetCore.Consul.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IConfigurationGeter _configurationGeter;
        private readonly TestConfig _testConfig;

        public ValuesController(TestConfig testConfig, IConfigurationGeter configurationGeter)
        {
            _testConfig = testConfig;
            _configurationGeter = configurationGeter;
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var data = new
            {
                TestConfigStarshipName = _configurationGeter["ZHS.Configuration.Core.TestConfig:starship:name"],
                FromClass = _testConfig,
                FromGetter = _configurationGeter.Get<TestConfig>(),
                FromLocator = ConfigurationGeterLocator.Current.Get<TestConfig>()
            };
            return new JsonResult(data);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
