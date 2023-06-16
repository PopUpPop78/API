using Data.Contracts;

namespace Data.IRepositories
{
    public interface IExpenseUnitOfWork : IUnitOfWork
    {
        IExpenseRepository ExpenseRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        ICategoryRepository CategoryRepository { get; }
    }
}
