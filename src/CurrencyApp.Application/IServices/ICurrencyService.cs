using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.Records;

namespace CurrencyApp.Application.IServices
{
    public interface ICurrencyService
    {
        Task SaveCurrencyPrincipal(string baseCurrency, string description);

        Task<(Dictionary<string,string>, Dictionary<string, string>)> GetDataApiFilter();

        Task<CurrencyRates?> GetDataApi(decimal? amount = null, DateTime? date = null);

        //Task<RatesRange?> GetDataHistorial(DateTime? date);

        Task<Currency?> GetPrincipalCurrency();

        Task AddCurrencyFavorite(string baseCurrency, string description);

        Task RemoveAsync(string baseCurrency);

        Task<BuildDataManagement?> GetBuildDataManagement();

        Task ModifyCurrenciesFavorites(string? selectedCurrencyFavorite, string? selectedCurrenciesDelete, string action);

    }
}
