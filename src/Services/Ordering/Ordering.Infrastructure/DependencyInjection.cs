using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        
        serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        serviceCollection.AddDbContext<ApplicationDbContext>((services, opt) =>
        {
            opt.AddInterceptors(services.GetServices<ISaveChangesInterceptor>());
            opt.UseSqlServer(connectionString);
        });
        
        serviceCollection.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return serviceCollection;
    }
}