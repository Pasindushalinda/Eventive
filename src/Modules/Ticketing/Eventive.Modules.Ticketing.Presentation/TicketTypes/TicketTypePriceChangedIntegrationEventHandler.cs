using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
using Eventive.Modules.Events.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Ticketing.Presentation.TicketTypes;

internal sealed class TicketTypePriceChangedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketTypePriceChangedIntegrationEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateTicketTypePriceCommand(integrationEvent.TicketTypeId, integrationEvent.Price),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(UpdateTicketTypePriceCommand), result.Error);
        }
    }
}
