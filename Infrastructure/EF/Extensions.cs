using Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddMSSqlServer(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<WarningDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

            return services;
        }
    }
}
