﻿using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Events.IntegrationEvents;
using Eventive.Modules.Ticketing.Application.Events.CreateEvent;
using MediatR;

namespace Eventive.Modules.Ticketing.Presentation.Events;

internal sealed class EventPublishedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<EventPublishedIntegrationEvent>
{
    public override async Task Handle(
        EventPublishedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateEventCommand(
                integrationEvent.EventId,
                integrationEvent.Title,
                integrationEvent.Description,
                integrationEvent.Location,
                integrationEvent.StartsAtUtc,
                integrationEvent.EndsAtUtc,
                integrationEvent.TicketTypes
                    .Select(t => new CreateEventCommand.TicketTypeRequest(
                        t.Id,
                        integrationEvent.EventId,
                        t.Name,
                        t.Price,
                        t.Currency,
                    t.Quantity))
        .ToList()),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(CreateEventCommand), result.Error);
        }
    }
}
