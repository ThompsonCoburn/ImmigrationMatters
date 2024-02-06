using System;
using System.Web;
using Firm.Logging.DTO;
using Firm.Logging.Helper.Web;
using Firm.Utilities;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Reflection;
using Firm.Configuration.Client;

namespace Firm.ImmigrationMatters
{
    public class MvcApplication : HttpApplication
    {
        private IConfiguration _configuration;
        protected void Application_Start()
        {
            _configuration = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IConfiguration)) as IConfiguration;
            GlobalConfiguration.Configure(httpConfiguration => WebApiConfig.Register(httpConfiguration, _configuration));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            _configuration = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IConfiguration)) as IConfiguration;
            this.HandleUnhandledWebExceptions(_configuration["LoggingServiceUrl"], _configuration["LoggingDestination"].ToEnum<LogDestination>(), Assembly.GetExecutingAssembly());
        }
    }
}
