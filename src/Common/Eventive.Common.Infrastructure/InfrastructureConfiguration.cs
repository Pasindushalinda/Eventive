using Eventive.Common.Application.Clock;
using Eventive.Common.Application.Data;
using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.ICacheService;
using Eventive.Common.Infrastructure.Caching;
using Eventive.Common.Infrastructure.Clock;
using Eventive.Common.Infrastructure.Data;
using Eventive.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace Eventive.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    //The Action<T> delegate represents a method that takes a single parameter and does not return a value. In this case, T is IRegistrationConfigurator
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnectionString,
        string redisConnectionString)
    {
        //To inject datasource for DbConnectionFactory
        //Create NpgsqlDataSource object and register
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();

        //Make above NpgsqlDataSource instance create at once
        services.TryAddSingleton(npgsqlDataSource);

        //use NpgsqlDataSource connection Scoped 
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        //Domain Event register and should introduced to Dbcontext in Event Module
        services.TryAddSingleton<PublishDomainEventsInterceptor>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        //when creating db migration redis error comes beacuse redis instance not run
        //at this time. Add memory instance to prevent error
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            services.TryAddSingleton(connectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        services.TryAddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configure =>
        {
            //add consumers
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configure);
            }

            //make endpoinds nicely readable
            configure.SetKebabCaseEndpointNameFormatter();

            //register the consumer and register required broker topology
            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
