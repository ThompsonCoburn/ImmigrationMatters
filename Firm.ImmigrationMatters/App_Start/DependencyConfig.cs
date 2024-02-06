using System.Web.Http;
using System.Reflection;
using System.Configuration;
using Firm.DependencyResolver;
using Firm.Configuration.Client;
using Firm.Configuration.Utilities;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Firm.ImmigrationMatters;
using Firm.ImmigrationMatters.Interfaces;
using Firm.ImmigrationMatters.Services;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(DependencyConfig), "RegisterComponents", Order = 10)]
namespace Firm.ImmigrationMatters
{
	public static class DependencyConfig
	{
		public static void RegisterComponents()
		{
			var services = new ServiceCollection();

			var configuration = new Configuration.Client.Configuration(ConfigurationManager.AppSettings["ConfigurationServiceUrl"], Assembly.GetExecutingAssembly());

			services.AddSingleton<IConfiguration>(configuration);

			services.AddStackExchangeRedisCache(options =>
			{
				ConfigurationOptions cacheConfigurationOptions = ConfigurationOptions.Parse(configuration["RedisConnectionString"]);
				cacheConfigurationOptions.Password = configuration.GetEncryptedValue("RedisPassword");

				options.ConfigurationOptions = cacheConfigurationOptions;
				options.InstanceName = configuration["CacheName"];
			});

			services.AddTransient<IMatterRepository, MatterRepository>();

			services.AddApiControllersAsServices(Assembly.GetExecutingAssembly());

			var dependencyResolver = new ServiceCollectionDependencyResolver(services.BuildServiceProvider());
			GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;
		}
	}
}