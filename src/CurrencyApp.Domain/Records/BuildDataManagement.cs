using CurrencyApp.Domain.Entities;

namespace CurrencyApp.Domain.Records
{
    public record BuildDataManagement(IDictionary<string, string> ItemsFavorites, IDictionary<string, string> ItemsPrincipal, Currency? principalCurrency, IEnumerable<Currency?> favoriteCurrency);
}
