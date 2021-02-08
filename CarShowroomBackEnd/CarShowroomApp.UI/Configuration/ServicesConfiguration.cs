using AutoMapper;
using CarShowroom.Application.Interfaces;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Infra.Data.Context;
using CarShowroomApp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarShowroom.UI.Configuration
{
    public static class ServicesConfiguration
    {
        public  static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDb(configuration);

            services.AddCorsService();

            services.AddIdentity();

            services.AddSwagger();

            services.AddCaching(configuration);

            services.AddAutoMapper();

            services.AddAuthentication(configuration);

            services.AddAuthorization();

            services.AddSignalRService();

            services.AddControllersServices();
        }
        public static void AddDb(this IServiceCollection services, IConfiguration configuration)
        {
            var isTesting = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Testing", StringComparison.InvariantCultureIgnoreCase);

            if (isTesting)
            {
                services.AddDbContext<DatabaseContext<User, Role>>(options =>
                    options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                services.AddDbContext<DatabaseContext<User, Role>>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }

            services.Configure<CarShowroomMongoSettings>(configuration.GetSection(nameof(CarShowroomMongoSettings)));

            services.AddSingleton<ICarShowroomMongoSettings>(ms => ms.GetRequiredService<IOptions<CarShowroomMongoSettings>>().Value);
        }

        public static void AddCorsService(this IServiceCollection services)
        {
            var isDevelopment = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "development", StringComparison.InvariantCultureIgnoreCase);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(isDevelopment ? "http://localhost:4200" : "https://agajek777.github.io")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DatabaseContext<User, Role>>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
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
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
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
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/chat")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, UserRolesEnum.Admin));
                options.AddPolicy("Moderator", policy => policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, new[] { UserRolesEnum.Admin, UserRolesEnum.Mod }));
            });
        }

        public static void AddSignalRService(this IServiceCollection services)
        {
            services.AddSignalR();
        }

        public static void AddControllersServices(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
                return;

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
        }
    }
}
