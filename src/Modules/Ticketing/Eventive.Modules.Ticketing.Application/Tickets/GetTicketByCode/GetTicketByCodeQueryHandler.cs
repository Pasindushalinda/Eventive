﻿using Dapper;
using Eventive.Common.Application.Data;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Ticketing.Application.Tickets.GetTicket;
using Eventive.Modules.Ticketing.Domain.Tickets;
using System.Data.Common;

namespace Eventive.Modules.Ticketing.Application.Tickets.GetTicketByCode;

internal sealed class GetTicketByCodeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketByCodeQuery, TicketResponse>
{
    public async Task<Result<TicketResponse>> Handle(GetTicketByCodeQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
            SELECT
                id AS {nameof(TicketResponse.Id)},
                customer_id AS {nameof(TicketResponse.CustomerId)},
                order_id AS {nameof(TicketResponse.OrderId)},
                event_id AS {nameof(TicketResponse.EventId)},
                ticket_type_id AS {nameof(TicketResponse.TicketTypeId)},
                code AS {nameof(TicketResponse.Code)},
                created_at_utc AS {nameof(TicketResponse.CreatedAtUtc)}
            FROM ticketing.tickets
            WHERE code = @Code
            """;

        TicketResponse? ticket = await connection.QuerySingleOrDefaultAsync<TicketResponse>(sql, request);

        if (ticket is null)
        {
            return Result.Failure<TicketResponse>(TicketErrors.NotFound(request.Code));
        }

        return ticket;
    }
}
