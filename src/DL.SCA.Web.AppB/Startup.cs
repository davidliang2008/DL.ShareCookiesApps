using DL.SCA.Security;
using DL.SCA.Security.Entities;
using DL.SCA.Web.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DL.SCA.Web.AppB
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string dbConnectionString = this.Configuration.GetConnectionString("AppDbConnection");

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(dbConnectionString, optionsBuilder =>
                    optionsBuilder.MigrationsAssembly(typeof(AppIdentityDbContext).Namespace)
                )
            );

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(SharedConstants.PathToDataProtectionKeys))
                .SetApplicationName(SharedConstants.ApplicationName);

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = SharedConstants.CookieName;
                options.Cookie.Path = "/";
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                //options.LoginPath = SharedConstants.LoginPath;
                //options.SlidingExpiration = true;
            });

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddMvc(options =>
                {
                    var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy));
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
                app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
