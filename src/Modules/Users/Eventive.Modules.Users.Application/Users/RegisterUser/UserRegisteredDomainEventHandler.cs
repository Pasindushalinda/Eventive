using Eventive.Common.Application.Exceptions;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Ticketing.PublicApi;
using Eventive.Modules.Users.Application.Users.GetUser;
using Eventive.Modules.Users.Domain.Users;
using MediatR;

namespace Eventive.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, ITicketingApi ticketingApi)
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(GetUserQuery), result.Error);
        }

        await ticketingApi.CreateCustomerAsync(
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName,
            cancellationToken);
    }
}
