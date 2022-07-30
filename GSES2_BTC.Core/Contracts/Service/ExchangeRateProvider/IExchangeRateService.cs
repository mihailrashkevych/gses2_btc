using GSES2_BTC.Core.Contracts.Data.Currency;

namespace GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider
{
    public interface IExchangeRateService
    {
        Task<string?> GetExchangeRateAsync(string url);
    }
}