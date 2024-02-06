using System.Collections.Generic;
using Firm.ImmigrationMatters.Models;
using System.Net.Http;

namespace Firm.ImmigrationMatters.Interfaces
{
	public interface IMatterRepository
	{
		List<Matter> GetMatters(HttpRequestMessage req, bool useCache = true);
	}
}