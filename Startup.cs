using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OglasAutomobila.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OglasAutomobila
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OglasiContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<AspNetUser>().AddRoles<AspNetRole>()
                .AddEntityFrameworkStores<OglasiContext>();
                
            services.AddControllersWithViews();
            
            services.Configure<IdentityOptions>(opcije => {
                opcije.Password.RequireDigit = false;
                opcije.Password.RequiredLength = 3;
                opcije.Password.RequireNonAlphanumeric = false;
                opcije.Password.RequireUppercase = false;
                opcije.Password.RequireLowercase = false;
                opcije.Password.RequiredUniqueChars = 1;
                opcije.User.RequireUniqueEmail = false;
                opcije.SignIn.RequireConfirmedEmail = false;
                opcije.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(opcije =>
            {
                opcije.Cookie.HttpOnly = true;
                opcije.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                opcije.LoginPath = "/Account/Login";
                opcije.AccessDeniedPath = "/Account/AccessDenied";
                opcije.SlidingExpiration = true;
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
                    name: "default",
                    pattern: "{controller=Oglasi}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
