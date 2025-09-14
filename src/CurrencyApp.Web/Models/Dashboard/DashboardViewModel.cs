namespace CurrencyApp.Web.Models.Dashboard
{
    public class DashboardViewModel
    {
        public decimal Amount { get; set; }

        public string? Base { get; set; }

        public string? Date { get; set; }

        public IEnumerable<RateViewModel>? Rates { get; set; }
    }

    public class RateViewModel
    {
        public decimal Amount { get; set; }
        public string? Base { get; set; }
    }
}
