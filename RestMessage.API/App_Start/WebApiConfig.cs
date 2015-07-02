using System.Web.Http;

namespace RestMessage.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Attribute Routing
            config.MapHttpAttributeRoutes();

            //Default API Route
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}