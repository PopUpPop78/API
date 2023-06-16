using AutoMapper;
using Data.IRepositories;
using Data.Repositories;
using Data.Expense.Models;

namespace Data.Expense.Repositories
{
    public class CategoryRepository : MappedRepository<Category, ExpenseContext>, ICategoryRepository
    {
        public CategoryRepository(ExpenseContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
    }
}
