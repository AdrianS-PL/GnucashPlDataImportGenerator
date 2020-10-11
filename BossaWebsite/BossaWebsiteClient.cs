using BossaWebsite.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BossaWebsite
{
    public class BossaWebsiteClient
    {
		private readonly HttpClient _httpClient;
		private readonly BossaWebsiteSettings _apiSettings;

		public BossaWebsiteClient(HttpClient httpClient, IOptionsMonitor<BossaWebsiteSettings> settings)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
			_apiSettings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
			if (string.IsNullOrWhiteSpace(_apiSettings.BaseUrl))
				throw new ArgumentNullException(nameof(BossaWebsiteSettings.BaseUrl));
			_httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
		}

		public async Task<BossaCurrentNbpCurrenciesDataFile> GetCurrentNbpCurrenciesData()
		{
			return await Get<BossaCurrentNbpCurrenciesDataFile>("pub/waluty/mstock/sesjanbp/sesjanbp.prn");
		}

		public async Task<BossaHistoricNbpCurrenciesDataFile> GetHistoricNbpCurrenciesData()
		{
			return await Get<BossaHistoricNbpCurrenciesDataFile>("pub/waluty/mstock/mstnbp.zip");
		}

		public async Task<BossaHistoricInvestmentFundsDataFile> GetHistoricInvestmentFundsData()
		{
			return await Get<BossaHistoricInvestmentFundsDataFile>("pub/fundinwest/mstock/mstfun.zip");
		}

		private async Task<ResponseType> Get<ResponseType>(string methodAddress, List<(string ArgumentName, string Value)> arguments = null) where ResponseType : BossaDataFile, new()
		{
			try
			{
				arguments ??= new List<(string ArgumentName, string Value)>();

				string url = _apiSettings.BaseUrl + methodAddress + "?" + string.Join("&", arguments.Select(q => $"{q.ArgumentName}={q.Value}"));
				HttpResponseMessage response = await _httpClient.GetAsync(url);
				
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new BossaWebsiteException($"Wrong http response code: {response.StatusCode}");
				}

				return BossaDataFile.CreateBossaFile<ResponseType>(await response?.Content?.ReadAsStreamAsync());
			}
			catch (BossaWebsiteException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new BossaWebsiteException("Request failed", ex);
			}
		}
	}
}
