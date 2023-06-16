using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Data.Expense.Models;

namespace Data.Expense.DTOs
{
    public class ExpenseDto : ImageModel
    {
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than zero")]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
