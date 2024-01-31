using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Firm.ImmigrationMatters.Models
{
    public class TokenRequest
    {

        public string clientId { get; set; }

        public string clientSecret { get; set; }

        public string _serverUrl { get; set; }

        public string _tokenEndpointPath { get; set; }
    }
}