using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyApp.Application.Services;
public class RequestService : IRequestService
{
    public readonly IRequestRepository _requestRepository;
    public readonly ILogBookService _logBookService;

    public RequestService(IRequestRepository requestRepository, ILogBookService logBookService)
    {
        _logBookService = logBookService;
        _requestRepository = requestRepository;   
    }

    public async Task SaveRequest(RequestFilterLog requestFilterLog)
    {
        try
        {
            await _requestRepository.AddAsync(requestFilterLog);
            await _requestRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _logBookService?.SaveLog(LogBook.Create(ex))!;
        }
    }
}

