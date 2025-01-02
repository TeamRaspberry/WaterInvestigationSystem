using AuthService.Application.Features.ExternalApplication.Get;
using AuthService.Application.Features.ExternalApplication.Register;
using AuthService.Application.Features.UserAccount.Login;
using AuthService.Application.Features.UserAccount.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ILoginCase, LoginCase>();
            services.AddScoped<IRegisterCase, RegisterCase>();
            services.AddScoped<IGetApplicationCredentials, GetApplicationCredentials>();
            services.AddScoped<IApplicationRegister, ApplicationRegister>();

            return services;
        }
    }
}
