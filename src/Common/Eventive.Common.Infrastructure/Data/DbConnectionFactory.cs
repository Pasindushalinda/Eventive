using Eventive.Common.Application.Data;
using Npgsql;
using System.Data.Common;

namespace Eventive.Common.Infrastructure.Data;

//To inject datasource configure NpgsqlDataSource in EventModules
public class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        //if you are using Sql server use following
        // new SqlConnection("Connection String")
        return await dataSource.OpenConnectionAsync();
    }
}
