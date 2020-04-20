using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTradingEngine.Money.Application;
using CurrencyTradingEngine.Money.Application.Request;
using CurrencyTradingEngine.Money.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CurrencyTradingEngine.User.Infrastructure
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly TradingEngineDbContext _context;

        public UserController(ILogger<UserController> logger, TradingEngineDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // temporary endpoint to create user
        // supposedly on another app
        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<Domain.Model.User>> Save(Domain.Model.User user)
        {
            if (user.Balance == null)
            {
                user.Balance = new Balance();
                user.Balance.CurrencyBalances = new List<CurrencyBalance>();
                user.Balance.CurrencyBalances.Add( new CurrencyBalance(){Amount = 0, Currency = new Currency("USD", 1)});
            }

            

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<IList<Domain.Model.User>>> Balance()
        {
            var result = await _context.Users.Include(User => User.Balance)
                .ThenInclude(f => f.CurrencyBalances)
                .ThenInclude(f => f.Currency)
                .Select(f => f).ToListAsync();
            return Ok(result);
        }


    }
}
