
namespace TaskManagement.Users.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using TaskManagement.Users.Domain.Entities;
    using TaskManagement.Users.Infrastructure.Classes;
    using TaskManagement.Users.Infrastructure.Context;
    using TaskManagement.Users.Infrastructure.Interfaces;


    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {
            var builIdentity = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builIdentity.UserType, builIdentity.Services);
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<UserTaskContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();

            services.AddSingleton(TimeProvider.System);

            
            services.AddScoped<IUserDAL, UserDAL>();

            return services;
        }
    }
}