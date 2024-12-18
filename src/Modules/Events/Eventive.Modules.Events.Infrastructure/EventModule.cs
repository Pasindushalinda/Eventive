using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Infrastructure.Outbox;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Events.Application.Abstarctions.Data;
using Eventive.Modules.Events.Domain.Categories;
using Eventive.Modules.Events.Domain.Events;
using Eventive.Modules.Events.Domain.TicketTypes;
using Eventive.Modules.Events.Infrastructure.Categories;
using Eventive.Modules.Events.Infrastructure.Database;
using Eventive.Modules.Events.Infrastructure.Events;
using Eventive.Modules.Events.Infrastructure.Inbox;
using Eventive.Modules.Events.Infrastructure.Outbox;
using Eventive.Modules.Events.Infrastructure.TicketTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Eventive.Modules.Events.Infrastructure;

public static class EventModule
{
    public static IServiceCollection AddEventModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDomainEventHandlers();
        services.AddIntegrationEventHandlers();

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        //adding database context
        services.AddDbContext<EventDbContext>((sp, options) =>
            options.UseNpgsql(databaseConnectionString,
                npgsqlOption => npgsqlOption
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Event))
                    .UseSnakeCaseNamingConvention()
                    //if save change call, then publish domain event using mediatR
                    .AddInterceptors(sp.GetRequiredService<InsertOutboxMessageInterceptor>())
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventDbContext>());

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.Configure<OutboxOptions>(configuration.GetSection("Events:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob>();

        services.Configure<InboxOptions>(configuration.GetSection("Events:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}