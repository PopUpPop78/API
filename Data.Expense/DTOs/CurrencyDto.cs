using System.ComponentModel.DataAnnotations;

namespace Data.Expense.DTOs
{
    public class CurrencyDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Currency code can only have maximum length of 3 characters")]
        public string Code { get; set; }
    }
}
