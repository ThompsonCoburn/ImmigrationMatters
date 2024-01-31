using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Firm.ImmigrationMatters.Models;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Firm.ImmigrationMatters.Controllers
{
    public class HomeController : Controller
    {
        public string _accessToken;

        public string clientId = "3II6MNGQ2J";

        public string clientSecret = "JtAQwpa1w-qbWCAlPdjt294zWE01";

        public string _serverUrl = "https://thompsoncoburn-sand.opensandbox2.intapp.com";

        public string _tokenEndpointPath = "/auth/oauth/token";

        public double numOfDays;



        public HomeController()

        {
            //todo: code to call Intapp with client Id and secret and assign AccessToken  
            TokenRequest request = new TokenRequest();
            request.clientId = clientId;
            request.clientSecret = clientSecret;
            request._serverUrl = _serverUrl;
            request._tokenEndpointPath = _tokenEndpointPath;


            string tokenEndpoint = $"{_serverUrl}{_tokenEndpointPath}";



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
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public async Task<ActionResult> GetMatters([DataSourceRequest] DataSourceRequest request)
        {

            var req = new HttpRequestMessage();

            req.Headers.Add("Accept", "application/json");

            req.Headers.Add("Authorization", $"Bearer {_accessToken}");

            req.RequestUri = new Uri($"{_serverUrl}/api/api/common/v1/matters?&filter.status=O&filter._matteraolcode=Immigration");

            req.Method = HttpMethod.Get;

            List<Matter> matters = new List<Matter>();

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(req); //.Result;

                string content = await response.Content.ReadAsStringAsync(); //.Result;

                matters.AddRange(JsonConvert.DeserializeObject<List<Matter>>(content));

            }

            


            return Json(matters.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

    }

}




