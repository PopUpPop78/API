using AutoMapper;
using Data.IRepositories;
using Data.UnitOfWork;

namespace Data.Expense.Repositories
{
    public class ExpenseUnitOfWork : UnitOfWork<ExpenseContext>
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
