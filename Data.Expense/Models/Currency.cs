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
        public string Name { get; set; }
        
        public string Code { get; set; }
    }
}
