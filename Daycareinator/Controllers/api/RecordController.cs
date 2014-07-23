using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Daycareinator.Controllers.api
{
    [Authorize]
    public class RecordController : ApiController
    {
        public HttpResponseMessage Post()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
