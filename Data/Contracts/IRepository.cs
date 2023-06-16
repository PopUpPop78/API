using System.Linq.Expressions;
using Data.Models;

namespace Data.Contracts
{
    public interface IRepository<T> : IRepositoryBase where T : IEntity
    {
        Task<T> Get(Expression<Func<T, bool>> filter, List<string> includes = null);
        Task<IList<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<PagedResults<T>> GetAll(QueryParameters parameters);
        Task<T> Update(int id, T entity);
        Task<T> Add(T entity);
    }
}
