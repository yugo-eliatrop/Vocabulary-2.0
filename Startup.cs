using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Vocabulary
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
            services.AddControllersWithViews();
            // Add service to connect with database context
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseNpgsql(connection).EnableSensitiveDataLogging());
            
            services.AddTransient<IWordService, WordService>();
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

            app.UsePHPFilter();

            app.UseHttpsRedirection();

            // app.Map("/admin", (app) => {
            //     // app.UseAuthorization();
            //     app.UseStaticFiles();
            //     app.UseRouting();
            //     app.UseEndpoints(endpoints =>
            //     {
            //         endpoints.MapControllerRoute(
            //             name: "default",
            //             pattern: "{controller}/{id?}");
            //     });
            // });

            app.UseStaticFiles();

            app.UseRouting();

            // app.Use(async (context, next) => {
            //     if (context.Request.Path.Value == "/")
                    
            //     await next();
            // });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action?}/{id?}");
            });

            app.Run(async (context) => {
                string agent = context.Request.Headers["User-Agent"];
                context.Response.Redirect("/Words");
                await Task.CompletedTask;
            });
        }
    }
}
