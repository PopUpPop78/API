using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Exceptions;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class MappedRepository<TModel, TContext> : Repository<TModel, TContext>, IMappedRepository<TModel> 
        where TModel : class, IEntity
        where TContext : DbContext
    {
        protected IMapper Mapper { get; }

        public MappedRepository(TContext context, IMapper mapper) :
            base(context)
        {
            Mapper = mapper;
        }

        public async Task<TViewModel> Get<TViewModel>(Expression<Func<TModel, bool>> filter, List<string> includes = null)
        {
            var query = Context.Set<TModel>().AsQueryable();

            includes?.ForEach(x => query.Include(x));

            var entity = await query.AsNoTracking().FirstOrDefaultAsync(filter);

            return Mapper.Map<TViewModel>(await query.AsNoTracking().FirstOrDefaultAsync(filter));
        }

        public async Task<IList<TViewModel>> GetAll<TViewModel>(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, List<string> includes = null)
        {
            var query = Context.Set<TModel>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            includes?.ForEach(x => query.Include(x));

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().ProjectTo<TViewModel>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<PagedResults<TViewModel>> GetAll<TViewModel>(QueryParameters parameters)
        {
            var count = await Context.Set<TModel>().CountAsync();

            var results = await Context.Set<TModel>()
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .ProjectTo<TViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResults<TViewModel>
            {
                Items = results,
                Page = parameters.StartIndex,
                RecordCount = results.Count,
                TotalCount = count
            };
        }

        public virtual async Task<TModel> Add<TViewModel>(TViewModel viewModelEntity) 
            where TViewModel : class
        {
            var entity = Mapper.Map<TModel>(viewModelEntity);
            await Context.Set<TModel>().AddAsync(entity);

            return entity;
        }

        public virtual async Task<TModel> Update<TViewModel>(int id, TViewModel viewModelEntity) 
            where TViewModel : class
        {
            var entity = await Get<TModel>(q => q.Id == id) ?? throw new EntityNotFoundException(id, typeof(TModel).Name);

            Mapper.Map(viewModelEntity, entity);

            Context.Set<TModel>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            return entity;
        }
    }
}
