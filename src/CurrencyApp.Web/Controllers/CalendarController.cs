using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Web.Filters;
using CurrencyApp.Web.Models.Calendar;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Web.Controllers
{
    [RequestFilter]
    public class CalendarController : Controller
    {
        public readonly ICurrencyService _currencyService;

        public CalendarController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        [HttpGet("calendar")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("calendar/historial")]
        public async Task<IActionResult> GetHistorial([FromBody] string date)
        {
            DataCalendarViewModel data = new DataCalendarViewModel();
            CurrencyRates? dataCalendar = await _currencyService.GetDataApi(date: DateTime.Parse(date));

            if (dataCalendar is not null)
            {
                data.Date = date;
                data.Rates = dataCalendar.Rates;
                data.Base = dataCalendar.Base;
            }

            return PartialView("_DataCalendar", data);
        }

    }
}
