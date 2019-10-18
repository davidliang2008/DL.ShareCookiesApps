using DL.SCA.Security.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DL.SCA.Security
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser, AppRole, int, 
        AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().ToTable("User");
            builder.Entity<AppRole>().ToTable("Role");

            builder.Entity<AppUserRole>().ToTable("UserRole");
            builder.Entity<AppUserClaim>().ToTable("UserClaim");
            builder.Entity<AppRoleClaim>().ToTable("RoleClaim");
            builder.Entity<AppUserLogin>().ToTable("UserLogin");
            builder.Entity<AppUserToken>().ToTable("UserToken");
        }
    }
}
