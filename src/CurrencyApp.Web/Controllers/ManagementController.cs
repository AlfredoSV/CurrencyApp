using CurrencyApp.Application.IServices;
using CurrencyApp.Domain.Entities;
using CurrencyApp.Domain.Records;
using CurrencyApp.Web.Filters;
using CurrencyApp.Web.Helpers;
using CurrencyApp.Web.Models.Converter;
using CurrencyApp.Web.Models.Management;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApp.Web.Controllers;

[RequestFilter]
public class ManagementController : Controller
{
    private readonly ICurrencyService _currencyService;

    public ManagementController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ChangeCurrency([FromForm] string selectedCurrencyPrincipal)
    {
        if (!string.IsNullOrEmpty(selectedCurrencyPrincipal))
        {
            string[] values = selectedCurrencyPrincipal.Split('-');
            await _currencyService.SaveCurrencyPrincipal(values[0], values[1]);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ModifyCurrencyFavorite([FromForm] ManagementViewModel model, [FromForm] string action)
    {
        await _currencyService.ModifyCurrenciesFavorites(model.SelectedCurrencyFavorite, model.SelectedCurrenciesDelete, action);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ManagementViewModel managementViewModel = new();

        BuildDataManagement? buildDataManagement = await _currencyService.GetBuildDataManagement();

        if (buildDataManagement?.principalCurrency is not null)
            managementViewModel.SelectedCurrencyPrincipal = string.Concat(buildDataManagement.principalCurrency.Base,"-",buildDataManagement.principalCurrency.Description);

        managementViewModel.CurrenciesFavorites = buildDataManagement?.favoriteCurrency.Select(data =>
        {
            return new CurrencyViewModel { Base = data.Base, Name = data.Description };
        });

        managementViewModel.CurrenciesPrincipal?.AddRange(buildDataManagement.ItemsPrincipal.Map());
        managementViewModel.CurrenciesFavorite?.AddRange(buildDataManagement.ItemsFavorites.Map());

        return View(managementViewModel);
    }

    [HttpGet("converter")]
    public async Task<IActionResult> Converter()
    {
        ConverterViewModel converterViewModel = new();
        Currency? principalCurrency = await _currencyService.GetPrincipalCurrency();
        if (principalCurrency is null)
        {
            return View(converterViewModel);
        }
        converterViewModel.CurrencyPrincipal = string.Concat(principalCurrency.Base, "-", principalCurrency.Description);
        return View(converterViewModel);
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> ConvertAmount([FromForm] ConverterViewModel model)
    {
        Currency? principalCurrency = await _currencyService.GetPrincipalCurrency();

        if (principalCurrency is null)
        {
            return View("Converter", new ConverterViewModel());
        }

        model.CurrencyPrincipal = string.Concat(principalCurrency.Base, "-", principalCurrency.Description);

        if (!ModelState.IsValid)
        {
            return View("Converter", model);
        }

        if (!string.IsNullOrEmpty(model.CurrencyPrincipal))
        {
            CurrencyRates? dashboard = await _currencyService.GetDataApi(model.Amount, model.Date);
            model.Result.AddRange(dashboard.Rates.Select(dt => new ResultConverViewModel() { Name = dt.Key, Amount = dt.Value }));
        }

        return View("Converter", model);
    }

}

