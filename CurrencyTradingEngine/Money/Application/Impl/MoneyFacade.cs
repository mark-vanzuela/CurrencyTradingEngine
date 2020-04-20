using CurrencyTradingEngine.Money.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTradingEngine.Money.Domain.Model;

namespace CurrencyTradingEngine.Money.Application.Impl
{
    public class MoneyFacade : IMoneyFacade
    {
        public void Exchange(MoneyExchangeRequest moneyExchangeRequest, string token)
        {
            //Currency from = findCurrency(moneyExchangeRequest.FromCurrencyName);
            //Currency to = findCurrency(moneyExchangeRequest.ToCurrencyName);
            //User.Domain.Model.User user = findUserByToken(token);
            //user.Exchange(new Domain.Model.Money(from, moneyExchangeRequest.Amount), to);
            throw new NotImplementedException();
        }

        public void Send(SendMoneyRequest sendMoneyRequest, string token)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, double> UserBalance(string token)
        {
            throw new NotImplementedException();
        }

        private Currency findCurrency(string currencyName)
        {
            throw new NotImplementedException();
        }
        
    }
}
