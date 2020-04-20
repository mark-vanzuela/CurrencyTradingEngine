using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyTradingEngine.Money.Domain.Model;
using NUnit.Framework;

namespace CurrencyTradingEngine.Test
{
    [TestFixture]
    public class BalanceTest
    {
        [Test]
        public void WhenBalanceIsInitialized_ThenBalanceShouldBeZeroAndDefaultCurrencyIsUsd()
        {
            var balance = new Balance();
            balance.CurrencyBalances = new List<CurrencyBalance>();
            balance.CurrencyBalances.Add(new CurrencyBalance(){Amount = 0, Currency = new Currency("USD", 50) });

            Assert.AreEqual(0, balance.GetAllMoney().FirstOrDefault(f=> f.Currency.Name.Equals("USD"))?.Amount);
        }

        [Test]
        public void WhenBalanceIsAdded_ThenBalanceShouldBeEqualToTotalAmountInCurrency()
        {
            var balance = new Balance();
            balance.CurrencyBalances = new List<CurrencyBalance>();
            balance.CurrencyBalances.Add(new CurrencyBalance() { Amount = 50, Currency = new Currency("USD", 50) });

            balance.AddMoney(new Money.Domain.Model.Money(new Currency("USD", 50), 50));

            Assert.AreEqual(100, balance.GetAllMoney().FirstOrDefault(f => f.Currency.Name.Equals("USD"))?.Amount);
        }

        [Test]
        public void WhenBalanceIsAddedWithNoPriorBalance_ThenBalanceShouldBeEqualToTotalAmountInCurrency()
        {
            var balance = new Balance();
            balance.CurrencyBalances = new List<CurrencyBalance>();
            
            balance.AddMoney(new Money.Domain.Model.Money(new Currency("USD", 50), 50));

            Assert.AreEqual(50, balance.GetAllMoney().FirstOrDefault(f => f.Currency.Name.Equals("USD"))?.Amount);
        }

        [Test]
        public void WhenBalanceIsCharged_ThenBalanceShouldBeEqualToTotalAmountInCurrency()
        {
            var balance = new Balance();
            balance.CurrencyBalances = new List<CurrencyBalance>();
            balance.CurrencyBalances.Add(new CurrencyBalance() { Amount = 100, Currency = new Currency("USD", 50) });

            balance.ChargeMoney(new Money.Domain.Model.Money(new Currency("USD", 50), 50));

            Assert.AreEqual(50, balance.GetAllMoney().FirstOrDefault(f => f.Currency.Name.Equals("USD"))?.Amount);
        }

        [Test]
        public void WhenBalanceIsChargedWithNoPriorBalance_ThenBalanceShouldBeEqualToNegativeAmountInCurrency()
        {
            var balance = new Balance();
            balance.CurrencyBalances = new List<CurrencyBalance>();
            
            balance.ChargeMoney(new Money.Domain.Model.Money(new Currency("USD", 50), 50));

            Assert.AreEqual(-50, balance.GetAllMoney().FirstOrDefault(f => f.Currency.Name.Equals("USD"))?.Amount);
        }
    }
}
