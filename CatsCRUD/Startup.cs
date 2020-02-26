using AutoMapper;
using CatsCRUD.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CatsCRUD.Services;
using CatsCRUD.Services.DAL;
using CatsCRUD.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            services.AddScoped<ICatService, CatService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc();


            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<CatRequest, Cat>();
                    cfg.CreateMap<Cat, CatResponse>();
                });

                return config.CreateMapper();
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = false,

                        ValidIssuer = AuthOptions.Issuer,


                        ValidateAudience = false,

                        ValidAudience = AuthOptions.Audience,

                        ValidateLifetime = false,


                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                        ValidateIssuerSigningKey = false,
                    };
                });

            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => { 
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
            });


            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Home");
            });
        }
    }
}
