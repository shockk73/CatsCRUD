using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CatsCRUD.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CatsCRUD.Services;
using CatsCRUD.Services.Models;

namespace CatsCRUD
{
    public class Startup
    {

        public Startup(IConfiguration config)
        {
            AppConfiguration = config;

        }

        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var con = AppConfiguration["ConnectionString:MSSQL:local"];

            services.AddDbContext<CatsContext>(options => options.UseSqlServer(con, b => b.MigrationsAssembly("CatsCRUD")));
            services.AddScoped<CatService>();
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CatRequest, Cat>();
                    cfg.CreateMap<Cat, CatResponse>();
                });

                return config.CreateMapper();
            });

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
