using QLab.Database.Models;
using QLab.Database.Repositories;
using QLab.Services.Contracts;

namespace QLab.Helpers.DependncyInjection
{
    public static class DependncyInjectionHelper
    {


        public static void AddDependencyInjection(this IServiceCollection services)
        {
            AdRepositories(services);
            AddAppServices(services);
        }

        private static void AddAppServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
        }

        private static void AdRepositories(IServiceCollection services)
        {
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


        }
    }
}



