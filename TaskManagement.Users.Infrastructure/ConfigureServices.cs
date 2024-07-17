
namespace TaskManagement.Users.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using TaskManagement.Users.Domain.Entities;
    using TaskManagement.Users.Infrastructure.Classes;
    using TaskManagement.Users.Infrastructure.Context;
    using TaskManagement.Users.Infrastructure.Interfaces;
    using TaskManagement.Users.Infrastructure.TestIdentity;

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

            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<UserTaskContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            Task.WaitAll(TestData.InsertData(context, userManager));

            services.AddSingleton(TimeProvider.System);


            services.AddScoped<IUserDAL, UserDAL>();

            return services;
        }
    }
}