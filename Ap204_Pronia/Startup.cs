using Ap204_Pronia.DAL;
using Ap204_Pronia.Models;
using Ap204_Pronia.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ap204_Pronia
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LayoutService>();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration.GetConnectionString("Default"));
            });

            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireUppercase = true;
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.AllowedForNewUsers = true;
                option.SignIn.RequireConfirmedEmail = false;
                //option.User.AllowedUserNameCharacters="qwertyuiopasdfghjklzxcvbnm_.1234567890";

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
         

            app.UseEndpoints(endpoints =>
            {
               
                    endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=dashbroad}/{action=index}/{id?}"
                    );
               
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
