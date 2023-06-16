using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;

namespace Data.Expense.Models
{
    public class Expense : ModelBase
    {
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
