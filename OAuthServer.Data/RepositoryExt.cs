using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAuthServer.Core.Configuration;
using OAuthServer.Core.Repositories;
using OAuthServer.Core.UnitOfWork;
using OAuthServer.Data.Repositories;

namespace OAuthServer.Data;

// EXTENSION METHODS MUST BE STATIC, AND THE CLASS THAT CONTAINS THEM MUST ALSO BE STATIC.
// WE SHOULD SPECIFY THE CLASS OR INTERFACE FOR WHICH WE ARE WRITING THE METHOD WITH THIS.

public static class RepositoryExt 
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

    public static IdentityBuilder AddIdentityEntityFrameworkStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<AppDbContext>();
    }
}