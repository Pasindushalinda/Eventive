using Dapper;
using Eventive.Modules.Events.Application.Abstarctions.Data;
using Eventive.Modules.Events.Application.Abstarctions.Messaging;
using Eventive.Modules.Events.Domain.Abstractions;
using System.Data.Common;

namespace Eventive.Modules.Events.Application.Events.GetEvent;

internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventQuery, EventResponse>
{
    public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(EventResponse.Id)},
                 title AS {nameof(EventResponse.Title)},
                 description AS {nameof(EventResponse.Description)},
                 location AS {nameof(EventResponse.Location)},
                 starts_at_utc AS {nameof(EventResponse.StartAtUtc)},
                 ends_at_utc AS {nameof(EventResponse.EndAtUtc)}
             FROM events.events
             WHERE id = @EventId
             """;

        EventResponse? @event = await connection.QuerySingleOrDefaultAsync<EventResponse>(sql, request);

        return @event;
    }
}
