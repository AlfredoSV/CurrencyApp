using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyApp.Web.Models.Management
{
    public class ManagementViewModel
    {
        public string? SelectedCurrencyPrincipal { get; set; }

        public string? SelectedCurrencyFavorite { get; set; }

        public string? SelectedCurrenciesDelete { get; set; }

        public List<SelectListItem>? CurrenciesPrincipal { get; set; } = new();

        public List<SelectListItem>? CurrenciesFavorite { get; set; } = new();

        public IEnumerable<CurrencyViewModel>? CurrenciesFavorites { get; set; }

    }

    public class CurrencyViewModel
    {
        public string? Base { get; set; }

        public string? Name { get; set; }
    }

}
