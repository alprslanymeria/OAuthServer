using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using OAuthServer.Core.Services;
using OAuthServer.Data;
using OAuthServer.Service.ExceptionHandlers;
using OAuthServer.Service.Filters;
using OAuthServer.Service.Services;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace OAuthServer.Service.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services )
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IServiceGeneric<,>), typeof(ServiceGeneric<,>));

            // MAPSTER
            services.AddMapster();

            // FLUENT VALIDATION
            services.AddValidatorsFromAssembly(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation(cfg =>
            {
                cfg.OverrideDefaultResultFactoryWith<FluentValidationFilter>();
            });

            // EXCEPTION HANDLERS
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
