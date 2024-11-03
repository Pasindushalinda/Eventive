using Eventive.Common.Infrastructure.Interceptors;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Ticketing.Application.Abstractions.Data;
using Eventive.Modules.Ticketing.Application.Carts;
using Eventive.Modules.Ticketing.Domain.Customers;
using Eventive.Modules.Ticketing.Infrastructure.Customers;
using Eventive.Modules.Ticketing.Infrastructure.Database;
using Eventive.Modules.Ticketing.Infrastructure.PublicApi;
using Eventive.Modules.Ticketing.PublicApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static IServiceCollection AddTicketingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketingDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Ticketing))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketingDbContext>());

        services.AddSingleton<CartService>();

        services.AddScoped<ITicketingApi, TicketingApi>();
    }
}

