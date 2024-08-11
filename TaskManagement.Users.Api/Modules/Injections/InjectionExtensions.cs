using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Users.Api.Modules.GlobalException;

namespace TaskManagement.Users.Api.Modules.Injections
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandler>();
            return services;
        }
    }
}