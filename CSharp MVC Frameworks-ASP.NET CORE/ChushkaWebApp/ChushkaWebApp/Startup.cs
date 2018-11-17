using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChushkaWebApp.Data;
using ChushkaWebApp.Infrastructure.Extensions;
using ChushkaWebApp.Models;
using ChushkaWebApp.Services;
using ChushkaWebApp.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChushkaWebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<ChushkaUser, IdentityRole>(opt =>
                 {
                     opt.SignIn.RequireConfirmedEmail = false;
                     opt.Password.RequireDigit = false;
                     opt.Password.RequireLowercase = false;
                     opt.Password.RequireNonAlphanumeric = false;
                     opt.Password.RequireUppercase = false;
                     opt.Password.RequiredLength = 3;
                     opt.Password.RequiredUniqueChars = 0;
                 })
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options => { options.SignIn.RequireConfirmedEmail = false; });

            var mappingConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new MappingProfile())
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IOrdersService, OrdersService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {
            app.UseDatabaseMigration();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
          
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
