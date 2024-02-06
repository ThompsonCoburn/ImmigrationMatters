using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Firm.Configuration.Client;
using Firm.Logging.DTO;
using Firm.Utilities;
using System.Reflection;
using System.Web.Http;
using Firm.ImmigrationMatters.Models;
using Firm.ImmigrationMatters.Interfaces;

namespace Firm.ImmigrationMatters.Services
{
	public class MatterRepository : IMatterRepository
	{
		private readonly IDistributedCache _cache;
		private readonly IConfiguration _configuration;

		public MatterRepository() 
		{
			_configuration = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IConfiguration)) as IConfiguration;
			_cache = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IDistributedCache)) as IDistributedCache;
		}

		public List<Matter> GetMatters(HttpRequestMessage req, bool useCache = true)
		{
			string cacheKey = string.Format("Matters");

			var matters = GetItemFromCache(cacheKey, useCache, req, () =>
			{
				return GetMattersFromIntapp(req);
			});

			return matters;
		}

		private List<Matter> GetMattersFromIntapp(HttpRequestMessage req) {

			using (var client = new HttpClient())
			{
				var response = client.SendAsync(req).Result;
				var matters = new List<Matter>();

				var content =  response.Content.ReadAsStringAsync().Result;

				matters.AddRange(JsonConvert.DeserializeObject<List<Matter>>(content));

				return matters;
			}
		}

		private T GetItemFromCache<T>(string key, bool useCache, HttpRequestMessage req, Func<T> fallbackMethod) where T : class
		{
			T item = null;
			try
			{
				var jsonSerializerSettings = new JsonSerializerSettings()
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
					PreserveReferencesHandling = PreserveReferencesHandling.All
				};

				if (useCache)
				{
					string data = _cache.GetString(key);
					if (data != null)
						item = JsonConvert.DeserializeObject<T>(data, jsonSerializerSettings);
				}

				if (item == null)
				{
					item = fallbackMethod();
					string data = JsonConvert.SerializeObject(item, jsonSerializerSettings);
					_cache.SetString(key, data);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex, LogDestination.Database);
				item = fallbackMethod();
			}

			return item;
		}

		private void RemoveItemFromCache(string key)
		{
			_cache.Remove(key);
		}

		//will want to call this after we add, update, or delete any matter from the grid, so the cache is reloaded from the db.
		private void ClearMattersCache() //string userId)
		{
			string mattersCacheKey = string.Format("Matters"); //, userId.ToUpper());

			RemoveItemFromCache(mattersCacheKey);
		}

		private void HandleException(Exception ex, LogDestination? loggingDestination) //, string userId)
		{
			LogDestination loggingDestinationValue = loggingDestination ?? _configuration["LoggingDestination"].ToEnum<LogDestination>();

			var logger = new Logging.Helper.Logger(_configuration["LoggingServiceUrl"], Assembly.GetExecutingAssembly());
			logger.LogException(ex, loggingDestinationValue, null, HttpContext.Current.Request.Url.AbsoluteUri);
		}
	}
}