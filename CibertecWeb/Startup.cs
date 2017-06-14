﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cibertec.Web.Models;
using Microsoft.EntityFrameworkCore;
using Cibertec.UnitOfWork;
using NLog.Web;
using Microsoft.AspNetCore.Http;
using NLog.Extensions.Logging;

namespace Cibertec
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            env.ConfigureNLog("NLogConfig.config");
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddDbContext<NorthwindDbContext>(
            //    options => 
            //    options.UseSqlServer(Configuration.GetConnectionString("Northwind"))
            //    );


            //services.AddTransient<IUnitOfWork>(
            //    option => new EFUnitOfWork(
            //        new NorthwindDbContext(
            //            new DbContextOptionsBuilder<NorthwindDbContext>()
            //                .UseSqlServer(Configuration.GetConnectionString("Northwind"))
            //                .Options
            //            )
            //        )
            //        );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

             services.AddSingleton<IUnitOfWork>(option => new CibertecUnitOfWork(Configuration.GetConnectionString("Northwind")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //  loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
