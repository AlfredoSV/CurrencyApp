using CurrencyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Domain.IRepositories
{
    public interface ICurrencyRepository
    {
        Task<Currency?> GetByTypeAsync(string type);
        IEnumerable<Currency> GetAllByType(string type);
        Task AddAsync(Currency currency);
        Task UpdateAsync(Currency currency);
        Task SaveChangesAsync();
        Task<Currency?> GetByBaseAsync(string baseCurrency);
        Task RemoveAsync(string baseCurrency);
    }
}
