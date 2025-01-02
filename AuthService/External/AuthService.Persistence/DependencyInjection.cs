using AuthService.Application.Abstractions.Cipher;
using AuthService.Application.Abstractions.Hash;
using AuthService.Core.Abstractions.Repositories;
using AuthService.Persistence.Database;
using AuthService.Persistence.Repositories;
using AuthService.Persistence.Utils.Cipher;
using AuthService.Persistence.Utils.Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(configuration["Database"]));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IHasher, Hasher>();
            services.AddTransient<IEncoder, SampleEncoder>();

            return services;
        }
    }
}
