using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}", //it can be like routeTemplate: "api/{controller}/{action}/{id}", //we can include action as well
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
