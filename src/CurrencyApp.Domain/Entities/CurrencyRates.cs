using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Domain.Entities
{
    public class CurrencyRates
    {
        public decimal Amount { get; set; }

        public string? Base { get; set; }

        public string? Date { get; set; }

        public Dictionary<string,decimal> Rates { get; set; }
    }
}
