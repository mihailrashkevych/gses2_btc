using GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider;

namespace CoingateProvider
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _client;

        public ExchangeRateService()
        {
            _client = new HttpClient();
        }

        public async Task<string?> GetExchangeRateAsync(string url)
        {
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}