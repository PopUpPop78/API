using Microsoft.AspNetCore.Http;

namespace Data.IRepositories
{
    public interface IUnitOfWorkBase
    {
        Task SaveChanges(HttpContext context);
    }
}
