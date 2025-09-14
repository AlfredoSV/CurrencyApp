using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Infrastructure.Data;

namespace CurrencyApp.Infrastructure.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly DataDbContext _context;

    public RequestRepository(DataDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RequestFilterLog requestFilter)
    => await _context.Requests.AddAsync(requestFilter);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}

