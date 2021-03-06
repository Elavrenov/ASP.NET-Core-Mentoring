﻿using System;
using System.IO;
using BLL.Interfaces.Interfaces;
using BLL.Services;
using DAL.EF;
using DAL.EF.Identity;
using DAL.EF.Repositories;
using DAL.Interfaces.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using SmartBreadcrumbs.Extensions;

namespace PL.WebAppMVC
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var connection = Configuration.GetConnectionString("DefaultConnection");
            var identityConnection = Configuration.GetConnectionString("IdentityConnection");
            services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(connection));
            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(identityConnection));

            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductRepository, EfProductRepository>() ;
            services.AddScoped<IProductService, ProductService>();

            if (!_environment.IsDevelopment())
            {
                services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));
            }

            services.AddMvc();
            services.AddDataProtection();
            services.AddRouting();
            services.AddBreadcrumbs(GetType().Assembly, options =>
            {
                options.TagName = "nav";
                options.TagClasses = "bg-info";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<NorthwindUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();


            env.ConfigureNLog(Path.Combine("nlog.config"));


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500;
            //        context.Response.ContentType = "text/html";

            //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

            //        var exceptionHandlerPathFeature =
            //            context.Features.Get<IExceptionHandlerPathFeature>();

            //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            //        {
            //            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
            //        }

            //        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
            //        await context.Response.WriteAsync("</body></html>\r\n");
            //        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
            //    });
            //});

            app.UseStatusCodePages("text/plain", "Status code page, status code: {0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}