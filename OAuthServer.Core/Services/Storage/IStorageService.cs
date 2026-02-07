using OAuthServer.Core.Configuration.Storage;

namespace OAuthServer.Core.Services.Storage;

/// <summary>
/// MAIN STORAGE SERVICE INTERFACE THAT DELEGATES TO THE CONFIGURED STORAGE PROVIDER
/// </summary>
public interface IStorageService : IStorageProvider
{
    /// <summary>
    /// GETS THE CURRENT STORAGE TYPE BEING USED
    /// </summary>
    StorageType CurrentStorageType { get; }

    /// <summary>
    /// GETS THE UNDERLYING STORAGE PROVIDER
    /// </summary>
    IStorageProvider Provider { get; }
}
