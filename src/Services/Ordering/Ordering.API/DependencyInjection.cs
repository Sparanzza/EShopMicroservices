namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // service.AddCarter();
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Add Carter, Health Checks, etc.
        return app;
    }
}