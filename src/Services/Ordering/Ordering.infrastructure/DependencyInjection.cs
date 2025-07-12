using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        var connectionString = configuration.GetConnectionString("Database");
        // services.addDbContext<OrderingDbContext>(options => options.UseSqlServer(connectionString));
        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }

}