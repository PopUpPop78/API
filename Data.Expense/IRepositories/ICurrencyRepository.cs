using Data.Contracts;
using Data.Expense.Models;

namespace Data.IRepositories
{
    public interface ICurrencyRepository : IMappedRepository<Currency>, IRepository<Currency>
    {

    }
}
