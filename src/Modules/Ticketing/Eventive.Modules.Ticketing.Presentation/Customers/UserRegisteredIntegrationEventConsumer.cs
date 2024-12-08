using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Ticketing.Application.Customers.CreateCustomer;
using Eventive.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Ticketing.Presentation.Customers;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
