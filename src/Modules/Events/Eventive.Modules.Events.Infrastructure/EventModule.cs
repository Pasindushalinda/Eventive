using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Events.Application.Abstarctions.Data;
using Eventive.Modules.Events.Domain.Categories;
using Eventive.Modules.Events.Domain.Events;
using Eventive.Modules.Events.Domain.TicketTypes;
using Eventive.Modules.Events.Infrastructure.Categories;
using Eventive.Modules.Events.Infrastructure.Database;
using Eventive.Modules.Events.Infrastructure.Events;
using Eventive.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventive.Modules.Events.Infrastructure;

public static class EventModule
{
    public static IServiceCollection AddEventModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;       

        //adding database context
        services.AddDbContext<EventDbContext>(options =>
            options.UseNpgsql(databaseConnectionString,
                npgsqlOption => npgsqlOption
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Event))
                    .UseSnakeCaseNamingConvention()
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventDbContext>());

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}