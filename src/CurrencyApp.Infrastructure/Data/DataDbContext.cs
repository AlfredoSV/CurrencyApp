using CurrencyApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyApp.Infrastructure.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions options):base(options)
        {}

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<RequestFilterLog> Requests { get; set; }

        public DbSet<LogBook> LogBooks { get; set; }
    }
}
