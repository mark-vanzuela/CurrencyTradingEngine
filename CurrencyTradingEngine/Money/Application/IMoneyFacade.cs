using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTradingEngine.Money.Application.Request;

namespace CurrencyTradingEngine.Money.Application
{
    public interface IMoneyFacade
    {
        public void Exchange(MoneyExchangeRequest moneyExchangeRequest, string token);
        public Dictionary<string, double> UserBalance(string token);
        void Send(SendMoneyRequest sendMoneyRequest, string token);
    }
}
