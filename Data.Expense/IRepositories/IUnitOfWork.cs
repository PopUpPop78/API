using Microsoft.AspNetCore.Http;

namespace Data.IRepositories
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        IExpenseRepository ExpenseRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        ICategoryRepository CategoryRepository { get; }
    }
}
