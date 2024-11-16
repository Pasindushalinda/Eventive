using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Modules.Attendance.Application.Abstractions.Data;
using Eventive.Modules.Attendance.Domain.Attendees;
using Eventive.Modules.Attendance.Domain.Events;
using Eventive.Modules.Attendance.Domain.Tickets;

namespace Eventive.Modules.Attendance.Application.Tickets.CreateTicket;

internal sealed class CreateTicketCommandHandler(
    IAttendeeRepository attendeeRepository,
    IEventRepository eventRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTicketCommand>
{
    public async Task<Result> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        Attendee? attendee = await attendeeRepository.GetAsync(request.AttendeeId, cancellationToken);

        if (attendee is null)
        {
            return Result.Failure(AttendeeErrors.NotFound(request.AttendeeId));
        }

        Event? @event = await eventRepository.GetAsync(request.EventId, cancellationToken);

        if (@event is null)
        {
            return Result.Failure(EventErrors.NotFound(request.EventId));
        }

        var ticket = Ticket.Create(request.TicketId, attendee, @event, request.Code);

        ticketRepository.Insert(ticket);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
