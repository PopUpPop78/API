namespace Data.Expense.ViewModels.Create
{
    public class CreateExpense
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
    }
}
