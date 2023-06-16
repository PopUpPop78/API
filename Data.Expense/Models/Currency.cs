using System.ComponentModel.DataAnnotations;
using Data.Contracts;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Expense.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Code), IsUnique = true)]
    public class Currency : ModelBase
    {        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [StringLength(3, ErrorMessage = "Currency code can only have maximum length of 3 characters")]
        public string Code { get; set; }
    }
}
