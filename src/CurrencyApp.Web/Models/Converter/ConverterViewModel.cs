using System.ComponentModel.DataAnnotations;

namespace CurrencyApp.Web.Models.Converter
{
    public class ConverterViewModel
    {
        public ConverterViewModel()
        {
            this.Amount = 1;
            this.Date = DateTime.Now.AddDays(-1);
            this.Result = new List<ResultConverViewModel>();
        }
        public string? CurrencyPrincipal { get; set; }

        [Required(ErrorMessage = "Es necesario ingresar un monto.")]
        [Range(1,10000,ErrorMessage = "El monto ingresado no se encuentra dentro del rango valido.")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage ="Es necesario ingresar una fecha.")]
        public DateTime? Date { get; set; }

        public List<ResultConverViewModel> Result { get; set; }
    }

    public class ResultConverViewModel
    {
        public string? Name { get; set; }

        public decimal Amount { get; set; }
    }

}
