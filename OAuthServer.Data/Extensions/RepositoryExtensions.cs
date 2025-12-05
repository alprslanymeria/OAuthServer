using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.UnitOfWork;
using OAuthServer.Data.Repositories;


namespace OAuthServer.Data.Extensions;

// EXTENSION METOTLAR STATİK OLMAK ZORUNDADIRLAR.
// HANGİ CLASS, INTERFACE İÇİN METOT YAZIYORSAK ONU THIS İLE BELİRTMELİYİZ.
// EXTENSION METOT SONRASINDA ZİNCİRLEME OLARAK DEVAM ETMEK İÇİN GERİYE DEĞER DÖNMELİYİZ.
public static class RepositoryExtensions 
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        // DB CONTEXT
        services.AddDbContext<AppDbContext>(options => {

            var connStrings = configuration.GetSection(ConnStringOption.Key).Get<ConnStringOption>();

            options.UseSqlServer(connStrings!.SqlServer, sqlOptions =>
            {

                sqlOptions.MigrationsAssembly(typeof(DataAssembly).Assembly.FullName);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        return services;
    }
}