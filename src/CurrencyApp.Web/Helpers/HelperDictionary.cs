using Microsoft.AspNetCore.Mvc.Rendering;

namespace CurrencyApp.Web.Helpers
{
    public static class HelperDictionary
    {
        public static IEnumerable<SelectListItem> Map(this IDictionary<string, string> map)
        {
            return map.Select(data => new SelectListItem()
            {
                Value = $"{data.Key}-{data.Value}",
                Text = $"{data.Key} - {data.Value}"

            }).ToList();
        }
    }
}
