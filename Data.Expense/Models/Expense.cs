using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;

namespace Data.Expense.Models
{
    public class Expense : ModelBase
    {
        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public CoreUser User { get; set; }
        
        public string ImageName { get; set; }
        [NotMapped]
        public byte[] Image { get; set; }
    }
}
