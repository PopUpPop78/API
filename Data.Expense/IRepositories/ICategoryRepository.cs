using Data.Contracts;
using Data.Expense.Models;

namespace Data.IRepositories
{
    public interface ICategoryRepository : IMappedRepository<Category>, IRepository<Category>
    {

    }
}
