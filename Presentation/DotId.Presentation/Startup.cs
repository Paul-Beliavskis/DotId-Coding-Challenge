using System.Reflection;
using AutoMapper;
using DotId.Application;
using DotId.Persistence;
using DotId.Persistence.Constants;
using DotId.Persistence.DTO;
using DotId.Persistence.Repositories;
using DotId.Persistence.Seeding.Interfaces;
using DotId.Persistence.Seeding.Services;
using DotId.Persistence.Services;
using DotId.Presentation.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.ImportData.SeedingData;

namespace DotId.Presentation
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

            var assembly = Assembly.GetAssembly(typeof(IApplicationLayer));
            services.AddDbContext<DotIdContext>(options => options.UseSqlServer(Configuration.GetValue<string>(ConfigurationConstants.SqlServer)));

            services.Configure<ConnectionStrings>(connectionStrings =>
            Configuration.Bind(ConfigurationConstants.ConnectionStrings, connectionStrings)
            );

            services.AddScoped<IQueryRepository, QueryRepository>();

            services.AddScoped<ILocationImportStrategy, LocationImportStrategy>();
            services.AddScoped<IScoreImportStrategy, ScoreImportStrategy>();
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<DotId.Application.Models.ReportModel, ReportModel>();
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddScoped<IDataSeeder, DataSeeder>();

            services.AddMediatR(assembly);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DotIdContext dotIdContext, IDataSeeder dataSeeder)
        {
            //dotIdContext.Database.EnsureDeleted();

            //dotIdContext.Database.Migrate();
            //dataSeeder.SeedData();

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Report}/{action=Index}/{id?}");
            });
        }
    }
}
