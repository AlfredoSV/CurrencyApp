using CurrencyApp.Domain.Entities;

namespace CurrencyApp.Application.IServices;
public interface IRequestService
{
    public Task SaveRequest(RequestFilterLog requestFilterLog);
}

