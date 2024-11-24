using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Users.Application.Users.GetUser;
using Eventive.Modules.Users.Domain.Users;
using Eventive.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        //UserRegisteredDomainEvent take care of internal event transaction
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(GetUserQuery), result.Error);
        }

        //not call for public api method customer insert in ticketingmodule
        //publish the integration event to message bus(event bus) using UserRegisteredDomainEvent
        //consume this in ticketing module presentation layer
        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccuredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
