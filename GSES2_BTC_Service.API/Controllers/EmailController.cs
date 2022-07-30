using GSES2_BTC.Core.Contracts.Service.ExchangeRateProvider;
using GSES2_BTC.Core.Contracts.Service.MessagingProvider;
using GSES2_BTC_Service.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GSES2_BTC_Service.API.Controllers
{
    [Route("/api")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CoinGateCurrencyController> _logger;

        public EmailController(IMessageService messageService, IExchangeRateService exchangeRateService, IConfiguration configuration, ILogger<CoinGateCurrencyController> logger)
        {
            _messageService = messageService;
            _exchangeRateService = exchangeRateService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("/subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] string email)
        {
            string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            if(!Regex.IsMatch(email, regex, RegexOptions.IgnoreCase))
            {
                return Conflict($"'{email}' is not a valid email address...");
            }

            var result = await _messageService.Subscribe(email);

            if (!result)
            {
                return Conflict($"Email address '{email}' subscribed already!");
            }

            return Ok("Email added!");
        }

        // for check if email sent go to https://ethereal.email/
        // use for login data from appsettings
        // "Mail": "reid.reinger16@ethereal.email"
        // "Password": "PRRDAPjAnmXmkXuhTf"
        [HttpPost("/sendEmails")]
        //public async Task<IActionResult> SendEmails(string from = "BTC", string to = "UAH")
        public async Task<IActionResult> SendEmails()
        {
            var message = new Message();
            message.From = "BTC";
            message.To = "UAH";
            message.ToSourceUrl = new UrlBuildHelper().BuildUrlForCoingate(_configuration["CoingateUrl"], message.From, message.To);

            var result = await _messageService.SendMessage(message);
            if (result.Count()>0)
            {
                return Ok(result);
            }
            return Ok("Emails have been sent!");
        }
    }
}