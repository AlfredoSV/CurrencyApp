using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Application.Services
{
    public class LogBookService : ILogBookService
    {
        public readonly ILogBookRepository _logBookRepository;

        public LogBookService(ILogBookRepository logBookRepository)
        {
            _logBookRepository = logBookRepository;
        }
        public async Task SaveLog(LogBook logBook)
        {
            try
            {
                await _logBookRepository.AddAsync(logBook);
                await _logBookRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
