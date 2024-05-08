using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Users.Application.Classes;
using TaskManagement.Users.Application.Interfaces;
using TaskManagement.Users.Application.Mapper;

namespace TaskManagement.Users.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingsProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUserBL, UserBL>();

            return services;
        }
    }
}