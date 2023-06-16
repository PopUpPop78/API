using models = Data.Expense.Models;

namespace Data.IRepositories
{
    public interface IExpenseRepository : IRepository<models.Expense>
    {
        Task<IList<TViewModel>> GetExpensesInRange<TViewModel>(DateTime from, DateTime to) where TViewModel : class;
    }
}
