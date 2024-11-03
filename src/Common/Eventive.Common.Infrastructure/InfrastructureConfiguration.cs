using Eventive.Common.Application.Clock;
using Eventive.Common.Application.Data;
using Eventive.Common.Application.ICacheService;
using Eventive.Common.Infrastructure.Caching;
using Eventive.Common.Infrastructure.Clock;
using Eventive.Common.Infrastructure.Data;
using Eventive.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace Eventive.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
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

        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.TryAddSingleton(connectionMultiplexer);

        services.AddStackExchangeRedisCache(options =>
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));

        services.TryAddSingleton<ICacheService, CacheService>();

        return services;
    }
}
