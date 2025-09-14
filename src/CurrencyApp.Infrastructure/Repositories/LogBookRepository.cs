using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Infrastructure.Repositories
{
    public class LogBookRepository : ILogBookRepository
    {
        private readonly DataDbContext _context;

        public LogBookRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LogBook logBook)
        => await _context.LogBooks.AddAsync(logBook);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
