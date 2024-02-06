using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Firm.ImmigrationMatters.Models;
using Firm.ImmigrationMatters.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Firm.Configuration.Client;
using System.Configuration;
using System.Reflection;

namespace Firm.ImmigrationMatters.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private IMatterRepository _matterRepository;
        private string _accessToken;
        private string _serverUrl;

        public HomeController()
        {
            _configuration = new Configuration.Client.Configuration(ConfigurationManager.AppSettings["ConfigurationServiceUrl"], Assembly.GetExecutingAssembly());
            _matterRepository = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IMatterRepository)) as IMatterRepository;
            _serverUrl = _configuration["ServerUrl"];

            string clientId = _configuration["ClientId"];
            string clientSecret = _configuration["ClientSecret"];
            string tokenEndpointPath = _configuration["TokenEndpointPath"];

            TokenRequest request = new TokenRequest();
            request.clientId = clientId;
            request.clientSecret = clientSecret;
            request._serverUrl = _serverUrl;
            request._tokenEndpointPath = tokenEndpointPath;

            string tokenEndpoint = $"{_serverUrl}{tokenEndpointPath}";

            var payload = new Dictionary<string, string>();
            payload.Add("grant_type", "client_credentials");
            payload.Add("client_id", clientId);
            payload.Add("client_secret", clientSecret);

            using (var client = new HttpClient())
            {
                var result = client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(payload)).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<TokenResponse>(content);
                _accessToken = token.access_token;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        //public async Task<ActionResult> GetMatters([DataSourceRequest] DataSourceRequest request)
        public ActionResult GetMatters([DataSourceRequest] DataSourceRequest request)
        {
            var req = new HttpRequestMessage();

            req.Headers.Add("Accept", "application/json");
            req.Headers.Add("Authorization", $"Bearer {_accessToken}");
            req.RequestUri = new Uri($"{_serverUrl}/api/api/common/v1/matters?&filter.status=O&filter._matteraolcode=Immigration");
            req.Method = HttpMethod.Get;

            var matters = _matterRepository.GetMatters(req);

            return Json(matters.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}




