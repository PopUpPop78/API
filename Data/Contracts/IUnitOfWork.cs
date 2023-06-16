using Microsoft.AspNetCore.Http;

namespace Data.Contracts
{
    public interface IUnitOfWork
    {
        Task SaveChanges(HttpContext context);
    }
}
