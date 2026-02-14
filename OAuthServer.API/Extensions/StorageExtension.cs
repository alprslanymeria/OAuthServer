using OAuthServer.Core.Configuration;
using OAuthServer.Core.Configuration.Storage;
using OAuthServer.Core.Services.Storage;
using OAuthServer.Service.Services.Storage;

namespace OAuthServer.API.Extensions;

public static class StorageExtension
{
    public static IServiceCollection AddStorageServicesExt(this IServiceCollection services, IConfiguration configuration)
    {
        // LOAD STORAGE CONFIGURATION AND VALIDATE
        var storageConfig = configuration
            .GetRequiredSection(StorageOption.Key)
            .Get<StorageOption>()
            ?? throw new InvalidOperationException("StorageConfig is missing in appsettings");

        // CONFIGURATION BINDINGS
        services.Configure<StorageOption>(configuration.GetSection(StorageOption.Key));
        services.Configure<GoogleCloudConfig>(configuration.GetSection(GoogleCloudConfig.Key));
        services.Configure<GoogleCloudStorageOption>(configuration.GetSection(GoogleCloudStorageOption.Key));

        // COMMON STORAGE SERVICES
        services.AddScoped<IStorageService, StorageService>();

        // REGISTRATION BASED ON STORAGE TYPE
        switch (storageConfig.StorageType)
        {
            case StorageType.GoogleCloud:
                AddGoogleCloudStorage(services);
                break;

            default:
                throw new NotSupportedException($"Storage type '{storageConfig.StorageType}' is not supported.");
        }

        return services;
    }

    private static void AddGoogleCloudStorage(IServiceCollection services)
    {
        services.AddScoped<IStorageProvider, GoogleCloudStorageProvider>();
    }
}
