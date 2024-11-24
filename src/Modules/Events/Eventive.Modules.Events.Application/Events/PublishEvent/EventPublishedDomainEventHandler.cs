using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Events.Application.Events.GetEvent;
using Eventive.Modules.Events.Domain.Events;
using Eventive.Modules.Events.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Events.Application.Events.PublishEvent;

internal sealed class EventPublishedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<EventPublishedDomainEvent>
{
    public override async Task Handle(
        EventPublishedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<EventResponse> result = await sender.Send(new GetEventQuery(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(GetEventQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new EventPublishedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccuredOnUtc,
                result.Value.Id,
                result.Value.Title,
                result.Value.Description,
                result.Value.Location,
                result.Value.StartsAtUtc,
                result.Value.EndsAtUtc,
                result.Value.TicketTypes.Select(t => new TicketTypeModel
                {
                    Id = t.TicketTypeId,
                    EventId = result.Value.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Currency = t.Currency,
                    Quantity = t.Quantity
                }).ToList()),
            cancellationToken);
    }
}
