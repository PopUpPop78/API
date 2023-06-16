using System.Linq.Expressions;
using Data.Models;

namespace Data.Contracts
{
    public interface IMappedRepository<T> where T : IEntity
    {
        Task<TViewModel> Get<TViewModel>(Expression<Func<T, bool>> filter, List<string> includes = null);
        Task<IList<TViewModel>> GetAll<TViewModel>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<PagedResults<TViewModel>> GetAll<TViewModel>(QueryParameters parameters);
        Task<T> Update<TViewModel>(int id, TViewModel viewModelEntity) where TViewModel : class;
        Task<T> Add<TViewModel>(TViewModel viewModelEntity) where TViewModel : class;
    }
}
