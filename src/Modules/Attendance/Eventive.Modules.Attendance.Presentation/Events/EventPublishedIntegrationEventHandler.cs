using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Attendance.Application.Events.CreateEvent;
using Eventive.Modules.Events.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Attendance.Presentation.Events;

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
                integrationEvent.EndsAtUtc),
        cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(CreateEventCommand), result.Error);
        }
    }
}
