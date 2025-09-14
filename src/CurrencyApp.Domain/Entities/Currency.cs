using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CurrencyApp.Domain.Entities
{
    public  class Currency
    {
        public Currency(){ }
        private Currency( string? baseCurrency, string? description, string? type, DateTime createdAt)
        {
            Base = baseCurrency;
            Description = description;
            Type = type;
            CreatedAt = createdAt;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [NotNull]
        public string? Base { get; set; }

        [NotNull]   
        public string? Description { get; set; }

        //Favorite or Principal
        [NotNull]
        public string? Type {  get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; }

        public static Currency Create(string? baseCurrency, string? description, string type)
        {
            return new Currency(baseCurrency, description, type, DateTime.Now);
        }

    }
}
