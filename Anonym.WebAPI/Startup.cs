using Anonym.Business.Mappings.AutoMapper;
using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.Concrete;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Anonym.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AnonymContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SQLProvider"), y => y.MigrationsAssembly("Anonym.DataAccess")));

            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SQLProvider"), y => y.MigrationsAssembly("Anonym.DataAccess")));

            services.AddAutoMapper(typeof(BusinessProfile));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //options.Lockout = lockoutOptions;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöpqrsþtuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            })
              .AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
