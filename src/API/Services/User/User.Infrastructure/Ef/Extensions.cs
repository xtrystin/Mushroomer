using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Infrastructure.Ef.Context;

namespace User.Infrastructure.Ef;

public static class Extensions
{
    public static IServiceCollection AddMSSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

        services.AddDbContext<ReadUserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

        return services;
    }

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresResourceDb")));

        services.AddDbContext<ReadUserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresResourceDb")));

        return services;
    }
}
