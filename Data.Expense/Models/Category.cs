using System.ComponentModel.DataAnnotations;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Expense.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : ModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
