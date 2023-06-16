using AutoMapper;
using Data.IRepositories;
using Data.Repositories;
using Data.Expense.Models;

namespace Data.Expense.Repositories
{
    public class CurrencyRepository : Repository<Currency, ExpenseContext>, ICurrencyRepository
    {
        public CurrencyRepository(ExpenseContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
