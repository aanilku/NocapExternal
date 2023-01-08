using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Routing;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;


/// <summary>
/// Summary description for WebApiConfig
/// </summary>
public static class WebApiConfig
{
    public static void Register(HttpConfiguration config)
    {
       config.EnableCors();
        // Web API configuration and services
        // Configure Web API to use only bearer token authentication.

        // config.SuppressDefaultHostAuthentication();
        // config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

        // Web API routes
        //config.MapHttpAttributeRoutes();

        //config.Routes.MapHttpRoute(
        //    name: "DefaultApi",
        //    routeTemplate: "api/{controller}/{action}/{id}",
        //    defaults: new { id = System.Web.Http.RouteParameter.Optional }
        //);

        RouteTable.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { id = System.Web.Http.RouteParameter.Optional }

          );

       // config.Formatters.Add(new CustomJsonFormatter());

        config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

    }
    
}
//public class CustomJsonFormatter : JsonMediaTypeFormatter
//{
//    public CustomJsonFormatter()
//    {
//        this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));


//    }
//    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
//    {
//        base.SetDefaultContentHeaders(type, headers, mediaType);
//        headers.ContentType = new MediaTypeHeaderValue("application/json");


//    }
//}