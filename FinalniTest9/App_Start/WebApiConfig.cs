using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using FinalniTest9.Interfaces;
using FinalniTest9.Repository;
using FinalniTest9.Resolver;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace FinalniTest9
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var container = new UnityContainer();
            container.RegisterType<ILanacRepository, LanacRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IHotelRepository, HotelRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


        }
    }
}
