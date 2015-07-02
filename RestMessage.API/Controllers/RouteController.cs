using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RestMessage.API.Controllers
{
    public class RouteController : ApiController
    {
        /// <summary>
        ///     Redirect root to API Documentation
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public HttpResponseMessage RedirectToDocumentation()
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(Request.RequestUri, "/swagger/");
            return response;
        }
    }
}