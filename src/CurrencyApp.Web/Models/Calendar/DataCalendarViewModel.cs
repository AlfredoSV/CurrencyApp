using CurrencyApp.Web.Models.Dashboard;

namespace CurrencyApp.Web.Models.Calendar
{
    public class DataCalendarViewModel
    {
        public string? Base { get; set; }

        public string? Date { get; set; }

        public Dictionary<string, decimal> Rates { set; get; }
    }
}
