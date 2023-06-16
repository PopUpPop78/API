using AutoMapper;
using AutoMapper.QueryableExtensions;
using models = Data.Expense.Models;
using Data.IRepositories;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Expense.Repositories
{
    public class ExpenseRepository : Repository<models.Expense, ExpenseContext>, IExpenseRepository
    {
        public ExpenseRepository(ExpenseContext context, IMapper autoMapper) : base(context, autoMapper)
        {
            
        }

        public async Task<IList<TViewModel>> GetExpensesInRange<TViewModel>(DateTime expensesFrom, DateTime expensesTo) where TViewModel : class
        {
            return await (from x in Context.Expenses 
                                  where x.Date.Date >= expensesFrom.Date || x.Date.Date <= expensesTo.Date
                                  select x)
                                  .ProjectTo<TViewModel>(Mapper.ConfigurationProvider)
                                  .ToListAsync();
        }
    }
}
