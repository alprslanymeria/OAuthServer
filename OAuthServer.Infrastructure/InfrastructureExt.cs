using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuthServer.Core.Configuration.Storage;
using OAuthServer.Core.Services.Storage;
using OAuthServer.Infrastructure.OpenTelemetry;
using OAuthServer.Infrastructure.Storage;

namespace OAuthServer.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenTelemetryServicesExt(configuration)
            .AddStorageServices(configuration);

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
