using Data.Users;
using Microsoft.AspNetCore.Identity;

namespace Data.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(CreateUser user);
        Task<AuthUser> Login(LoginUser user);
        Task<string> CreateRefreshToken();
        Task<AuthUser> VerifyRefreshToken(AuthUser user);
    }
}
