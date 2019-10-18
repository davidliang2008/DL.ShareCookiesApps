using DL.SCA.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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

            var seedUser = new AppUser
            {
                Id = 1,
                FirstName = "David",
                LastName = "Liang",
                UserName = "david.liang@outlook.com",
                NormalizedUserName = "DAVID.LIANG@OUTLOOK.COM",
                Email = "DAVID.LIANG@OUTLOOK.COM",
                NormalizedEmail = "DAVID.LIANG@OUTLOOK.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            seedUser.PasswordHash = new PasswordHasher<AppUser>()
                .HashPassword(seedUser, "c@KNUsA8q9CLdsfA");

            builder.Entity<AppUser>().ToTable("User").HasData(seedUser);
            builder.Entity<AppRole>().ToTable("Role");

            builder.Entity<AppUserRole>().ToTable("UserRole");
            builder.Entity<AppUserClaim>().ToTable("UserClaim");
            builder.Entity<AppRoleClaim>().ToTable("RoleClaim");
            builder.Entity<AppUserLogin>().ToTable("UserLogin");
            builder.Entity<AppUserToken>().ToTable("UserToken");
        }
    }
}
