using System.ComponentModel.DataAnnotations;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Expense.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : ModelBase
    {        
        [Required]
        [StringLength(50, ErrorMessage = "Category name can only have a maximum length of 50 characters")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
