using Microsoft.Extensions.Options;
using StooqWebsite.Configuration;
using StooqWebsite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StooqWebsite
{
    public class StooqWebsiteClient
    {
		private readonly HttpClient _httpClient;
		private readonly StooqWebsiteSettings _settings;
		const string BaseUrl = @"https://stooq.pl/";

        public StooqWebsiteClient(HttpClient httpClient, IOptions<StooqWebsiteSettings> settings)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));			
			_httpClient.BaseAddress = new Uri(BaseUrl);
			_settings = settings.Value;
		}

		public async Task<StooqDataFile> GetData(string symbol, DateTime? startDate = null, DateTime? endDate = null)
        {
			var arguments = new List<(string ArgumentName, string Value)>();

			arguments.Add(("s", symbol));
			if(startDate.HasValue && endDate.HasValue)
            {
				arguments.Add(("d1", startDate.Value.ToString("yyyyMMdd")));
				arguments.Add(("d2", endDate.Value.ToString("yyyyMMdd")));
			}
			arguments.Add(("i", "d"));

			arguments.Add(("apikey", _settings.ApiKey));

			return await Get(@"q/d/l/", arguments);
		}

		private async Task<StooqDataFile> Get(string methodAddress, List<(string ArgumentName, string Value)> arguments = null)
		{
			try
			{
				arguments ??= new List<(string ArgumentName, string Value)>();

				string url = BaseUrl + methodAddress + "?" + string.Join("&", arguments.Select(q => $"{q.ArgumentName}={q.Value}"));
				HttpResponseMessage response = await _httpClient.GetAsync(url);

				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new StooqWebsiteException($"Wrong http response code: {response.StatusCode}");
				}


				return new StooqDataFile(await response?.Content?.ReadAsStreamAsync());
			}
			catch (StooqWebsiteException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new StooqWebsiteException("Request failed", ex);
			}
		}
	}
}
