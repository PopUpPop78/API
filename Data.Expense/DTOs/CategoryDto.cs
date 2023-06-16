using System.ComponentModel.DataAnnotations;

namespace Data.Expense.DTOs
{
    public class CategoryDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Category name can only have a maximum length of 50 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
