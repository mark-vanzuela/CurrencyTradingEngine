using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CurrencyTradingEngine.Money.Domain.Exception;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyTradingEngine.Money.Domain.Model
{
    public class Balance
    {
        public int BalanceId { get; set; }
        public List<CurrencyBalance> CurrencyBalances { get; set; }
        public int UserId { get; set; }
        public User.Domain.Model.User User { get; set; }
  

        public void Exchange(Money money, Currency to)
        {
            HasEnoughMoneyInBalance(money);
            var ratioBetweenCurrencies = money.Currency.Ratio / to.Ratio;
            
        }

        public IList<Money> GetAllMoney()
        {
            return CurrencyBalances.Select(f => new Money(f.Currency, f.Amount)).ToList();
        }

        public void AddMoney(Money money)
        {
            var currency = CurrencyBalances.FirstOrDefault(f => f.CurrencyId == money.Currency.CurrencyId);
            if (currency != null)
            {
                currency.Amount += money.Amount;
            }
            else
            {
                CurrencyBalances.Add(new CurrencyBalance() {Amount = money.Amount, Currency = money.Currency});
            }
        }

        public void ChargeMoney(Money money)
        {
            var currency = CurrencyBalances.FirstOrDefault(f => f.CurrencyId == money.Currency.CurrencyId);
            if (currency != null)
            {
                currency.Amount -= money.Amount;
            }
            else
            {
                CurrencyBalances.Add(new CurrencyBalance() { Amount = money.Amount * -1, Currency = money.Currency });
            }
        }

        private void HasEnoughMoneyInBalance(Money money)
        {
            var currency = CurrencyBalances.FirstOrDefault(f => f.CurrencyId == money.Currency.CurrencyId);
            if (currency == null) 
            {
                throw new CurrencyNotFoundException(money.Currency.Name);
            }
            else 
            {
                var moneyInBalance = currency.Amount;
                if (moneyInBalance < money.Amount)
                    throw new InsufficientBalanceException();
            }
        }
    }
    
}
