using CurrencyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Application.IServices
{
    public interface ILogBookService
    {
        public Task SaveLog(LogBook logBook);
    }
}
