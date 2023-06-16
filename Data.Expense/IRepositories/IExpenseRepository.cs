using Data.Contracts;
using MODELS = Data.Expense.Models;

namespace Data.IRepositories
{
    public interface IExpenseRepository : IMappedRepository<MODELS.Expense>, IRepository<MODELS.Expense>
    {
        Task<IList<TViewModel>> GetExpensesInRange<TViewModel>(DateTime from, DateTime to) where TViewModel : class;
    }
}
