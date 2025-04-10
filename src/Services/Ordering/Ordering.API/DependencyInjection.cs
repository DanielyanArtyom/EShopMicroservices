using BuildingBlocks.Exceptions.Handler;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddCarter();
        
        serviceCollection.AddExceptionHandler<CustomExceptionHandler>();

        serviceCollection.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);
        
        return serviceCollection;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        
        app.UseExceptionHandler(options => { });

        app.UseHealthChecks("/health");
        
        return app;
    }
}