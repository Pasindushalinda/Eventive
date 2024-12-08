using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Attendance.Application.Attendees.CreateAttendee;
using Eventive.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Attendance.Presentation.Attendees;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateAttendeeCommand(
                integrationEvent.UserId,
        integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(CreateAttendeeCommand), result.Error);
        }
    }
}
