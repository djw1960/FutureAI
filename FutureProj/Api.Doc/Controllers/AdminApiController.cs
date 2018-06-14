using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Doc.Controllers
{
    [RoutePrefix("v1")]
    public class AdminApiController : ApiController
    {
        private string apiUrl = System.Configuration.ConfigurationManager.AppSettings["adminApiUrl"];// "http://192.168.1.60:8089";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("3000")]
        public IHttpActionResult Test()
        {
            return Json<dynamic>(new
            {
                Code = 0,
                Result = ""
            });

        }

    }
}
