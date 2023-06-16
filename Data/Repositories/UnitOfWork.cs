using Data.Contracts;
using Data.Extensions;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public abstract class UnitOfWork<T> : IDisposable, IUnitOfWork where T : DbContext
    {
        protected T Context { get; }

        public UnitOfWork(T context)
        {
            Context = context;
        }

        public virtual async Task SaveChanges(HttpContext context)
        {
            var userName = context.User.GetUserId();

            foreach (var entry in ModifiedEntries)
            {
                entry.Entity.Updated = DateTime.Now;
                entry.Entity.UpdatedBy = userName;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userName;
                    entry.Entity.Created = DateTime.Now;
                }
            }

            await Context.SaveChangesAsync();
        }

        protected virtual IEnumerable<dynamic> ModifiedEntries => from x in Context.ChangeTracker.Entries()
                                                                  where x.State == EntityState.Modified ||
                                                                  x.State == EntityState.Added
                                                                  select new { Entity = x.Entity as ModelBase, x.State };

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
