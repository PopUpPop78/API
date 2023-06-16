using Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public abstract class CoreContext<T> : IdentityDbContext<T> where T : CoreUser
    {
        private readonly IConfiguration _configuration;

        public CoreContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new AdminUserConfiguration(_configuration));
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
