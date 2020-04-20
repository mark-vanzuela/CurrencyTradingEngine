using CurrencyTradingEngine.Money.Application;
using CurrencyTradingEngine.Money.Application.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyTradingEngine.Money.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly ILogger<MoneyController> _logger;
        private readonly IMoneyFacade _moneyFacade;

        public MoneyController(ILogger<MoneyController> logger, IMoneyFacade moneyFacade)
        {
            _moneyFacade = moneyFacade;
            _logger = logger;
        }

        [HttpGet]
        [Route("balance")]
        public IActionResult Balance(string token)
        {
            var result = _moneyFacade.UserBalance(token);
            return Ok(result);
        }

        [HttpPost]
        [Route("exchange")]
        public void Exchange(MoneyExchangeRequest request, string token)
        {
            _moneyFacade.Exchange(request, token);
        }

        [HttpPost]
        [Route("send")]
        public void Send(SendMoneyRequest request, string token)
        {
            _moneyFacade.Send(request, token);
        }
    }
}