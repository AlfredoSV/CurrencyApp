using CurrencyApp.Domain.Entities;

namespace CurrencyApp.Domain.IRepositories;
public interface IRequestRepository
{
    public Task AddAsync(RequestFilterLog filterLog);

    Task SaveChangesAsync();
}

