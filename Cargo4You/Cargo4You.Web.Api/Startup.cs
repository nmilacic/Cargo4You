using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Data.Database.Cargo4You.Model;
using Cargo4You.Services;
using Cargo4You.Services.Sequrity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Web.Api
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
            services.AddControllers();

            services.AddDbContext<Courier4YouContext>((builder) =>
            {
                builder.UseInMemoryDatabase("Courier4YouContext");
            });

            var tokenValidationParameters =

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:5001/",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nevenanevenanevenanevena")),
                    };
                });

            services.AddCors((s) =>
            {
                s.AddPolicy("all", cp =>
                {
                    cp.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            services.AddScoped<CourierService>();
            services.AddScoped<OrderService>();
            services.AddScoped<UserService>();
            services.AddScoped<PasswordManager>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cargo4You.Web.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Courier4YouContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cargo4You.Web.Api v1"));
            }

            Seed(context);

            app.UseHttpsRedirection();

            app.UseCors("all");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void Seed(Courier4YouContext context)
        {
            var user = new User
            {
                Name = "nevena",
                Password = "nevena"
            };

            context.Add(user);
            context.SaveChanges();

            var courierCargo4You = new Courier
            {
                Name = "Cargo4You",
                DimensionFrom = null,
                DimensionTo = 2000,
                WeightFrom = null,
                WeightTo = 20
            };
            context.Add(courierCargo4You);

            context.SaveChanges();

            var courierShipFasrer = new Courier
            {
                Name = "ShipFaster",
                DimensionFrom = null,
                DimensionTo = 1700,
                WeightFrom = 10,
                WeightTo = 30
            };

            context.Add(courierShipFasrer);

            context.SaveChanges();

            var courierMaltaShip = new Courier
            {
                Name = "MaltaShip",
                DimensionFrom = 500,
                DimensionTo = null,
                WeightFrom = 10,
                WeightTo = null
            };

            context.Add(courierMaltaShip);

            context.SaveChanges();

            context.SaveChanges();

            var courierCalcs = new List<CourierPrice> {
                new CourierPrice
            {
                CourierId = courierCargo4You.Id,
                From = 0,
                To = 1000,
                Price = 10,
                IsWeight = false,
            },
                 new CourierPrice
            {
                CourierId = courierCargo4You.Id,
                From = 1000,
                To = 2000,
                Price = 20,
                IsWeight = false,
            },
                  new CourierPrice
            {
                CourierId = courierCargo4You.Id,
                From = 0,
                To = 2,
                Price = 15,
                IsWeight = true,
            },
                   new CourierPrice
            {
                CourierId = courierCargo4You.Id,
                From = 2,
                To = 15,
                Price = 18,
                IsWeight = true,
            },
                    new CourierPrice
            {
                CourierId = courierCargo4You.Id,
                From = 15,
                To = 20,
                Price = 35,
                IsWeight = true,
            }
            };

            context.AddRange(courierCalcs);

            context.SaveChanges();
        }
    }
}
