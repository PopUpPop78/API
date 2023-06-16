using AutoMapper;
using Data.IRepositories;
using Data.Repositories;

namespace Data.Expense.Repositories
{
    public class ExpenseUnitOfWork : UnitOfWork<ExpenseContext>, IExpenseUnitOfWork
    {
        public IExpenseRepository ExpenseRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }

        public ExpenseUnitOfWork(ExpenseContext context, IMapper mapper) : base(context)
        {
            ExpenseRepository = new ExpenseRepository(context, mapper);
            CategoryRepository = new CategoryRepository(context, mapper);
            CurrencyRepository = new CurrencyRepository(context, mapper);
        }
    }
}
