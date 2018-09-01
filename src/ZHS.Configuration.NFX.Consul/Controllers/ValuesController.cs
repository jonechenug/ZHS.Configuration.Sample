using System;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZHS.Configuration.Core;

namespace ZHS.Configuration.NFX.Consul.Controllers
{
   
    public class ValuesController : ApiController
    {

        // GET api/values
        [HttpGet]
        public JsonResult<Object> Index()
        {
            var data = new
            {
                FromLocator= ConfigurationGeterLocator.Current.Get<TestConfig>()
            };
            return new JsonResult<Object>(data,new JsonSerializerSettings(), Encoding.UTF8, Request);
        }
    }
}
