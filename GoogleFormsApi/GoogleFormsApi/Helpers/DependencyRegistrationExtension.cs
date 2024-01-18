using Application.Abstractions;
using Application.CQRS.Commands.FormActions;
using Domain.Models;
using GoogleFormsApi.MapperProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Helpers;
using Persistence.Implementations;
using Persistence.Implementations.CachingServices;
using System.Reflection;
using System.Text;

namespace GoogleFormsApi.Helpers
{
    internal static class DependencyRegistrationExtension
    {
        internal static void RegisterServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RegisterProfile>();
                cfg.AddProfile<FormProfile>();
                cfg.AddProfile<QuestionProfile>();
                cfg.AddProfile<ResponseProfile>();
            });

            builder.Services.RegisterDbContext(options =>
            options.UseLazyLoadingProxies(true).UseSqlServer(builder.Configuration.GetConnectionString("GoogleForms")));

            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<GoogleFormsDbContext>();

            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtIssuer,
                     ValidAudience = jwtIssuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                 };
             });

            builder.Services.AddScoped<JwtHelper>();
            builder.Services.RegisterServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Update))));
        }
    }
}
