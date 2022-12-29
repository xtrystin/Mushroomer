using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Infrastructure.EF.Context;

namespace Post.Infrastructure.EF
{
    public static class Extensions
    {
        public static IServiceCollection AddMSSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

            services.AddDbContext<ReadPostDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

            return services;
        }
    }
}
