using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) => user.FindFirstValue(SD.UserId) ?? throw new Exception("No user logged in");
    }
}
