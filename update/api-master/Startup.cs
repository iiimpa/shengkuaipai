using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;
using WebApi.Helpers;
using YYApi;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using System;

namespace WebApi
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
            services.AddControllers();
            services.AddStackExchangeRedisCache(x => x.Configuration = Configuration.GetConnectionString("Redis"));
            services.AddRankHelper(Configuration["Chinaz:Key"]);
            services.AddYYApi("SEO优化", "SEO优化");
            services.AddDbContext<DataContext>(x =>
            {
                x.UseMySql(Configuration.GetConnectionString("Mysql"));
                x.EnableSensitiveDataLogging();
            });
            services.AddSingleton(new Alipay(Configuration["Alipay:AppId"], Configuration["Alipay:PrivateKey"], Configuration["Alipay:AlipayPublicKey"], Configuration["Alipay:NotifyUrl"]));
            services.AddSingleton(new Methods());
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext db)
        {
            db.Database.Migrate();
            app.UseForwardedHeaders();
            app.UseRouting();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/uploads"
            });

            app.UseYYApiMiddleware("SEO优化");
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}