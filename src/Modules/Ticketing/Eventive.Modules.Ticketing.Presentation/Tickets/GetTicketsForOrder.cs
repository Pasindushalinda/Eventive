﻿using Eventive.Common.Domain;
using Eventive.Common.Presentation.ApiResults;
using Eventive.Common.Presentation.Endpoints;
using Eventive.Modules.Ticketing.Application.Tickets.GetTicket;
using Eventive.Modules.Ticketing.Application.Tickets.GetTicketForOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventive.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicketsForOrder : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tickets/order/{orderId}", async (Guid orderId, ISender sender) =>
        {
            Result<IReadOnlyCollection<TicketResponse>> result = await sender.Send(
                new GetTicketsForOrderQuery(orderId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Tickets);
    }
}
