using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Data.Configuration
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<CoreUser>
    {
        private readonly IConfiguration _config;

        public AdminUserConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public virtual void Configure(EntityTypeBuilder<CoreUser> builder)
        {
            var hasher = new PasswordHasher<CoreUser>();
            builder.HasData(
                new CoreUser
                {
                    
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Id = "4C0CDE15-753D-4863-9759-6FA3CB30846A",
                    PasswordHash = hasher.HashPassword(null, _config["AdminPassword"])
                }
            );
        }
    }
}
