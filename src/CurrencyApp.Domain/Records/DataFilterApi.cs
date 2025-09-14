using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Domain.Records
{
    public class DataFilterApi
    {
        public string? BaseCurrency { get; set; }

        public IEnumerable<string> FavoritesCurrencies { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
