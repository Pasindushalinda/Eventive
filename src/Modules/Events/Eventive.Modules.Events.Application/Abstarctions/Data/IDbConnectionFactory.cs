using System.Data.Common;

namespace Eventive.Modules.Events.Application.Abstarctions.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
