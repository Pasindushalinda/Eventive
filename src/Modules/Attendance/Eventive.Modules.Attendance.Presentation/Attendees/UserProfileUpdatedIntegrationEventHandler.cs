using Eventive.Common.Application.EventBus;
using Eventive.Common.Application.Exceptions;
using Eventive.Common.Domain;
using Eventive.Modules.Attendance.Application.Attendees.UpdateAttendee;
using Eventive.Modules.Users.IntegrationEvents;
using MediatR;

namespace Eventive.Modules.Attendance.Presentation.Attendees;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateAttendeeCommand(
                integrationEvent.UserId,
        integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventiveException(nameof(UpdateAttendeeCommand), result.Error);
        }
    }
}
