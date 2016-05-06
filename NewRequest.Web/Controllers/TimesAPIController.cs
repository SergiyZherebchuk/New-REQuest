using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewRequest.Web.Controllers
{
    [RoutePrefix("api/TimesAPI")]
    public class TimesAPIController : ApiController
    {
        [HttpGet]
        [Route("Time")]
        public string GetNowTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
