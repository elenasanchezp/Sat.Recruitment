using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Application.Interfaces;
using Sat.Recruitment.Application.Services;

namespace Sat.Recruitment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IUserCreationService, UserCreationService>();
            services.AddTransient<IUserValidationService, UserValidationService>();
            services.AddTransient<IUsersService, UsersService>();

            return services;
        }

    }
}
