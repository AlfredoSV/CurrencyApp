using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Web.Filters;
using CurrencyApp.Web.Models.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Web.Controllers;
public class DashboardController : Controller
{
    public readonly ICurrencyService _currencyService;

    public DashboardController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [RequestFilter]
    [HttpGet("dashboard")]
    public async Task<IActionResult> Index()
    {
        DashboardViewModel dataResponse = new DashboardViewModel();
        CurrencyRates? dashboardResponse = await _currencyService.GetDataApi();

        if (dashboardResponse is not null)
        {
            dataResponse = new DashboardViewModel()
            {
                Amount = dashboardResponse.Amount,
                Base = dashboardResponse.Base,
                Date = dashboardResponse.Date,
                Rates = dashboardResponse.Rates.Select(data => new RateViewModel() { Amount = data.Value, Base = data.Key })
            };
        }

        return View(dataResponse);
    }
}
