using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.Enums;
using CurrencyApp.Domain.IRepositories;
using CurrencyApp.Domain.Records;
using System.Net.Http.Json;
using System.Text;

namespace CurrencyApp.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly HttpClient _httpClient;
        public readonly ILogBookService _logBookService;

        public CurrencyService(IHttpClientFactory httpClientFactory, ICurrencyRepository currencyRepository, ILogBookService logBookService)
        {
            _httpClient = httpClientFactory.CreateClient("FrankFurterApi");
            _currencyRepository = currencyRepository;
            _logBookService = logBookService;
        }

        public async Task SaveCurrencyPrincipal(string baseCurrency, string description)
        {
            try
            {
                string typeCurrency = TypeCurrency.Principal.ToString();
                Currency? currency = await _currencyRepository.GetByTypeAsync(typeCurrency);

                if (currency is null)
                {
                    await _currencyRepository.AddAsync(Currency.Create(baseCurrency, description, typeCurrency));
                }
                else
                {
                    currency.CreatedAt = DateTime.Now;
                    currency.Description = description;
                    currency.Base = baseCurrency;
                    await _currencyRepository.UpdateAsync(currency);
                }

                await _currencyRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
            }
        }

        public async Task AddCurrencyFavorite(string baseCurrency, string description)
        {
            try
            {
                if (await _currencyRepository.GetByBaseAsync(baseCurrency) is null)
                {
                    Currency currency = Currency.Create(baseCurrency, description, TypeCurrency.Favorite.ToString());
                    await _currencyRepository.AddAsync(currency);
                    await _currencyRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
            }
        }

        public async Task<(Dictionary<string, string>, Dictionary<string, string>)> GetDataApiFilter()
        {
            try
            {
                Dictionary<string, string>? favorites;
                Dictionary<string, string>? principal;
                Dictionary<string, string> dataResponse;
                HttpResponseMessage response = await this._httpClient.GetAsync("currencies");//All currencies
                response.EnsureSuccessStatusCode();
                dataResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>() ?? new Dictionary<string, string>();

                if (!dataResponse.Any())
                {
                    return (new(), new());
                }

                var dataAddedFavorites = _currencyRepository.GetAllByType(TypeCurrency.Favorite.ToString()).ToDictionary(data => data.Base, data => data.Description);
                var dataAddedPrincipal = _currencyRepository.GetAllByType(TypeCurrency.Principal.ToString()).ToDictionary(data => data.Base, data => data.Description);

                if (!dataAddedFavorites.Any() && !dataAddedPrincipal.Any())
                {
                    return (dataResponse, dataResponse);
                }

                favorites = dataResponse.ExceptBy(dataAddedFavorites.Keys, kpv => kpv.Key)
                                        .ExceptBy(dataAddedPrincipal.Keys, kpv => kpv.Key)
                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                principal = dataResponse.ExceptBy(dataAddedFavorites.Keys, kpv => kpv.Key)
                                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                return (favorites, principal);
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
                return default!;
            }
        }

        public async Task<Currency?> GetPrincipalCurrency()
        {
            try
            {
                return await _currencyRepository.GetByTypeAsync(TypeCurrency.Principal.ToString());
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
                return default;
            }
        }

        public async Task RemoveAsync(string baseCurrency)
        {
            try
            {
                await _currencyRepository.RemoveAsync(baseCurrency);
                await _currencyRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
            }
        }

        public async Task<CurrencyRates?> GetDataApi(decimal? amount = null, DateTime? date = null)
        {
            CurrencyRates? data = default;
            try
            {
                HttpResponseMessage httpResponseMessage = default!;
                Currency? principalCurrency = await GetPrincipalCurrency();

                if (principalCurrency is null)
                    return data;

                string type = TypeCurrency.Favorite.ToString();
                IEnumerable<string> favoritesCurrenciesList = _currencyRepository.GetAllByType(type).Select(dt => dt.Base);
                string url = BuildUrl(new DataFilterApi() { BaseCurrency = principalCurrency.Base, FavoritesCurrencies = favoritesCurrenciesList, Amount = amount, StartDate = date });
                httpResponseMessage = await this._httpClient.GetAsync(url);
                httpResponseMessage.EnsureSuccessStatusCode();
                data = await httpResponseMessage.Content.ReadFromJsonAsync<CurrencyRates>() ?? new CurrencyRates();
                if (!favoritesCurrenciesList.Any())
                    data.Rates = new Dictionary<string, decimal>();

                return data;
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
                return data;
            }
        }

        public async Task<BuildDataManagement?> GetBuildDataManagement()
        {
            try
            {
                (Dictionary<string, string> ItemsFavorites, Dictionary<string, string> ItemsPrincipal) dataResponse = await GetDataApiFilter();
                Currency? principalCurrency = await GetPrincipalCurrency();
                IEnumerable<Currency?> favoriteCurrency = _currencyRepository.GetAllByType(TypeCurrency.Favorite.ToString());
                return new BuildDataManagement(dataResponse.ItemsFavorites, dataResponse.ItemsPrincipal, principalCurrency, favoriteCurrency);
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
                return default;
            }
        }

        public async Task ModifyCurrenciesFavorites(string? selectedCurrencyFavorite, string? selectedCurrenciesDelete, string action)
        {
            try
            {
                if (action.Equals("add") && !string.IsNullOrEmpty(selectedCurrencyFavorite))
                {
                    string[] values = selectedCurrencyFavorite?.Split('-')!;
                    await AddCurrencyFavorite(values[0], values[1]);
                }

                if (action.Equals("remove") && !string.IsNullOrEmpty(selectedCurrenciesDelete))
                {
                    await RemoveAsync(selectedCurrenciesDelete ?? string.Empty);
                }
            }
            catch (Exception ex)
            {
                await _logBookService?.SaveLog(LogBook.Create(ex))!;
            }
        }


        //https://api.frankfurter.dev/v1/2025-01-02..2025-01-03?base=USD&symbols=MXN
        private string BuildUrl(DataFilterApi dataFilterApi)
        {
            StringBuilder complementUrl = new StringBuilder();
            complementUrl.Append(dataFilterApi.StartDate is null ? "latest" : ((DateTime)dataFilterApi.StartDate).ToString("yyyy-MM-dd"));
            complementUrl.Append(dataFilterApi.EndDate is null ? string.Empty : $"..{((DateTime)dataFilterApi.EndDate).ToString("yyyy-MM-dd")}");
            complementUrl.Append(dataFilterApi.Amount is null ? "?" : $"?amount={dataFilterApi.Amount}&");
            complementUrl.Append($"base={dataFilterApi.BaseCurrency}");
            complementUrl.Append(dataFilterApi.FavoritesCurrencies.Any() ? $"&symbols={string.Join(",", dataFilterApi.FavoritesCurrencies)}" : string.Empty);
            return complementUrl.ToString();
        }
    }
}
