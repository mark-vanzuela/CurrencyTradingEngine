using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CurrencyTradingEngine.Money.Domain.Event;
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


        public event EventHandler<OnAddMoneyEventArgs> OnAddMoneyEvent;
        public event EventHandler<OnChargeMoneyEventArgs> OnChargeMoneyEvent;

        public Balance()
        {
            OnAddMoneyEvent += RespondToOnAddMoneyEvent;
            OnChargeMoneyEvent += RespondToOnChargeEvent;
        }

        public void Exchange(Money money, Currency to)
        {
            HasEnoughMoneyInBalance(money);
            var ratioBetweenCurrencies = money.Currency.Ratio / to.Ratio;
            AddMoney(new Money(to, ((double) Math.Round(money.Amount * ratioBetweenCurrencies * 100) / 100)));
            ChargeMoney(money);
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

            OnAddMoneyEvent?.Invoke(this, new OnAddMoneyEventArgs() { UserId = UserId, CurrencyName = money.Currency.Name, Amount = money.Amount });
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

            OnChargeMoneyEvent?.Invoke(this, new OnChargeMoneyEventArgs() { UserId = UserId, CurrencyName = money.Currency.Name, Amount = money.Amount });
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

        private void RespondToOnChargeEvent(object sender, OnChargeMoneyEventArgs e)
        {
            //process event args here
        }

        private void RespondToOnAddMoneyEvent(object sender, OnAddMoneyEventArgs e)
        {
            //process event args here
        }
    }
    
}
