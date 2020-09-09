using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using CarShowroom.Infra.Data.Repositories;
using CarShowroom.Infra.IoC;
using CarShowroomApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CarShowroomApp
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
            services.AddDbContext<DatabaseContext<User, Role>>(options => 
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DatabaseContext<User, Role>>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                    {
                        Title = "CarShowroom WEB API",
                        Contact = new OpenApiContact
                        {
                            Email = "adam.gajek777@gmail.com",
                            Name = "Adam Gajek",
                            Url = new Uri("https://agajek777.github.io/Portfolio/")
                        }
                    }
                );
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(Startup));

            DependencyContainer.RegisterServices(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, UserRolesEnum.Admin));
                options.AddPolicy("Moderator", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, new[] { UserRolesEnum.Admin, UserRolesEnum.Mod }));
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Carshowroom WEB API"));
        }
    }
}
