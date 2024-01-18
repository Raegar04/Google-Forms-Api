using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Implementations.CachingServices;
using Persistence.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Helpers
{
    public static class DependencyRegistration
    {
        public static void RegisterDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<GoogleFormsDbContext>(options);
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFormService, FormService>();
            services.Decorate<IFormService, CachingFormService>();

            services.AddScoped<IQuestionService, QuestionService>();
            services.Decorate<IQuestionService, CachingQuestionService>();

            services.AddScoped<IResponseService, ResponseService>();

            services.AddScoped<IUserFormService, UserFormService>();
            services.Decorate<IUserFormService, CachingUserFormService>();
        }
    }
}
