using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using OAuthServer.Core.Services;
using OAuthServer.Core.Services.Storage;
using OAuthServer.Service.Services;

namespace OAuthServer.Service
{
    public static class ServiceExt
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IFileStorageHelper, FileStorageHelper>();
            services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));

            // MAPSTER
            services.AddMapster();

            // FLUENT VALIDATION
            services.AddValidatorsFromAssembly(typeof(ServiceAssembly).Assembly);

            return services;
        }
    }
}
