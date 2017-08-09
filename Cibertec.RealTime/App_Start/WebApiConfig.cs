using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;

namespace Cibertec.RealTime.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor()
                , new DeflateCompressor()));

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name:"DefaultApi",
                routeTemplate:"{controller}/{id}",
                defaults:new {id=RouteParameter.Optional}
                
                );
        }
}
}