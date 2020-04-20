using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyTradingEngine.Money.Domain.Model
{
    public class Currency
    {
        public string CurrencyId { get; set; }
        public  string Name { get; set; }
        public decimal Ratio { get; set; }

        public Currency(string name, decimal ratio)
        {
            Name = name;
            Ratio = ratio;
        }

    }
}
