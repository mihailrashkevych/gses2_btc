using GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider;
using GSES2_BTC_Service.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GSES2_BTC_Service.API.Controllers
{
    [Route("/api")]
    [ApiController]
    public class CoinGateCurrencyController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CoinGateCurrencyController> _logger;

        public CoinGateCurrencyController(
            IExchangeRateService exchangeRateService, 
            IConfiguration configuration, 
            ILogger<CoinGateCurrencyController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("/rate")]
        //public async Task<IActionResult> Get(string from = "BTC", string to = "UAH")
        public async Task<IActionResult> Get()
        {
            string from = "BTC";
            string to = "UAH";
            var result = await _exchangeRateService.GetExchangeRateAsync(
                new UrlBuildHelper().BuildUrlForCoingate(_configuration["CoingateUrl"], from, to));
            if (!String.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest($"Wow!! One of currencies not exist or 'Coingate' not responding...");
            }
        }
    }
}
