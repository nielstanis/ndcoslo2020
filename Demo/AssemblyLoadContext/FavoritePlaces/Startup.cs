using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FavoritePlaces.Load;
using FavoritePlaces.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Services;

namespace FavoritePlaces
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
            services.AddDbContext<PlacesDbContext>();

             //Register the types delivered by our custom AssemblyLoadContext
            var ddlName = "MyPDFLibrary";
            var pdfAssembly = Path.Combine(AppContext.BaseDirectory, ddlName, ddlName + ".dll");
            var isolated = new IsolatedLoadContext(pdfAssembly, new [] { typeof(IPDFService) });
            var loadedPDF = isolated.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pdfAssembly)));
            var types = loadedPDF.GetTypes();
            foreach (Type type in types)
            {
                if (typeof(IPDFService).IsAssignableFrom(type))
                {
                    IPDFService pdf = Activator.CreateInstance(type) as IPDFService;
                    services.AddSingleton<IPDFService>(pdf);
                    break;
                }
            } 
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
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                    RequestPath = "/Images"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
