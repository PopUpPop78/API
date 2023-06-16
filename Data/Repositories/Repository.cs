using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Exceptions;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class Repository<TModel, TContext> : IRepository<TModel>
        where TModel : class, IEntity
        where TContext : DbContext
    {
        protected TContext Context { get; }

        public Repository(TContext context)
        {
            Context = context;
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter, List<string> includes = null)
        {
            var query = Context.Set<TModel>().AsQueryable();

            includes?.ForEach(x => query.Include(x));

            var entity = await query.AsNoTracking().FirstOrDefaultAsync(filter);

            return await query.AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<IList<TModel>> GetAll(Expression<Func<TModel, bool>> filter = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, List<string> includes = null)
        {
            var query = Context.Set<TModel>().AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            includes?.ForEach(x => query.Include(x));

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<PagedResults<TModel>> GetAll(QueryParameters parameters)
        {
            var count = await Context.Set<TModel>().CountAsync();

            var results = await Context.Set<TModel>()
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .ToListAsync();

            return new PagedResults<TModel>
            {
                Items = results,
                Page = parameters.StartIndex,
                RecordCount = results.Count,
                TotalCount = count
            };
        }

        public virtual async Task<TModel> Add(TModel entity)
        {
            await Context.Set<TModel>().AddAsync(entity);

            return entity;
        }

        public virtual async Task<TModel> Update(int id, TModel entity)
        {
            var dbEntity = await Get(q => q.Id == id) ?? throw new EntityNotFoundException(id, typeof(TModel).Name);

            Context.Set<TModel>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            Context.Set<TModel>().Update(entity);

            return entity;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await Get(q => q.Id == id) ?? throw new EntityNotFoundException(id, typeof(TModel).Name);
            Context.Set<TModel>().Remove(entity);
        }
    }
}
