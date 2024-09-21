using Eventive.Common.Application.Clock;
using Eventive.Common.Application.Data;
using Eventive.Common.Infrastructure.Clock;
using Eventive.Common.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Eventive.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnectionString)
    {
        //To inject datasource for DbConnectionFactory
        //Create NpgsqlDataSource object and register
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();

        //Make above NpgsqlDataSource instance create at once
        services.TryAddSingleton(npgsqlDataSource);

        //use NpgsqlDataSource connection Scoped 
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
