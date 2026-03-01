using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.Configuration.Storage;
using OAuthServer.Core.Services;
using OAuthServer.Core.Services.Storage;
using OAuthServer.Infrastructure.OpenTelemetry;
using OAuthServer.Infrastructure.Security;
using OAuthServer.Infrastructure.Storage;

namespace OAuthServer.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenTelemetryServicesExt(configuration)
            .AddSecretManagerServices(configuration)
            .AddStorageServices(configuration);

        return services;
    }

    private static IServiceCollection AddSecretManagerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SecretManagerOption>(configuration.GetSection(SecretManagerOption.Key));
        services.AddSingleton<ISecretProvider, GoogleSecretManagerProvider>();

        // RESOLVE GOOGLE CREDENTIAL FROM SECRET MANAGER AT STARTUP (SINGLETON)
        services.AddSingleton(sp =>
        {
            var secretProvider = sp.GetRequiredService<ISecretProvider>();
            var options = sp.GetRequiredService<IOptions<SecretManagerOption>>().Value;
            var json = secretProvider.GetSecretAsync(options.ServiceAccountSecretName).GetAwaiter().GetResult();
            return CredentialFactory.FromJson<ServiceAccountCredential>(json).ToGoogleCredential();
        });

        return services;
    }

    private static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        // LOAD STORAGE CONFIGURATION AND VALIDATE
        var storageConfig = configuration
            .GetRequiredSection(StorageOption.Key)
            .Get<StorageOption>()
            ?? throw new InvalidOperationException("StorageConfig is missing in appsettings");

        // CONFIGURATION BINDINGS
        services.Configure<StorageOption>(configuration.GetSection(StorageOption.Key));
        services.Configure<GoogleCloudStorageOption>(configuration.GetSection(GoogleCloudStorageOption.Key));

        // COMMON STORAGE SERVICES
        services.AddScoped<IStorageService, StorageService>();

        // REGISTRATION BASED ON STORAGE TYPE
        switch (storageConfig.StorageType)
        {
            case StorageType.GoogleCloud:
                services.AddScoped<IStorageProvider, GoogleCloudStorageProvider>();
                break;

            default:
                throw new NotSupportedException($"Storage type '{storageConfig.StorageType}' is not supported.");
        }

        return services;
    }
}
