using Firm.Configuration.Client;
using Firm.Logging.DTO;
using Firm.Logging.Helper.Web;
using Firm.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Firm.ImmigrationMatters
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config, IConfiguration configuration)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "ActionApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			var webApiExceptionLogger = new WebApiExceptionLogger(configuration["LoggingServiceUrl"], configuration["LoggingDestination"].ToEnum<LogDestination>(), Assembly.GetExecutingAssembly());
			config.Services.Add(typeof(IExceptionLogger), webApiExceptionLogger);

		}
	}
}