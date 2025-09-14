using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Infrastructure.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataDbContext _context;

        public CurrencyRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
            => await _context.Currencies.ToListAsync();

        public async Task<Currency?> GetByIdAsync(int id)
            => await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Currency?> GetByBaseAsync(string baseCurrency)
            => await _context.Currencies.FirstOrDefaultAsync(c => c.Base == baseCurrency);

        public async Task AddAsync(Currency currency)
            => await _context.Currencies.AddAsync(currency);

        public Task UpdateAsync(Currency currency)
        {
            _context.Currencies.Update(currency);
            return Task.CompletedTask;
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                _context.Currencies.Remove(entity);
        }

        public async Task RemoveAsync(string baseCurrency)
        {
            var entity = await GetByBaseAsync(baseCurrency);
            if (entity != null)
                _context.Currencies.Remove(entity);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public async Task<Currency?> GetByTypeAsync(string type)
        {
            Currency? currency = await _context.Currencies.FirstOrDefaultAsync(data => data.Type.Equals(type));
            return currency;
        }

        public IEnumerable<Currency> GetAllByType(string type)
        {
            return _context.Currencies.Where(data => data.Type == type);
        }
    }
}
