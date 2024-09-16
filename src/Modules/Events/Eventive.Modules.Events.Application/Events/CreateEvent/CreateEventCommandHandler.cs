using Eventive.Modules.Events.Application.Abstarctions.Data;
using Eventive.Modules.Events.Application.Abstarctions.Messaging;
using Eventive.Modules.Events.Domain.Abstractions;
using Eventive.Modules.Events.Domain.Events;

namespace Eventive.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandHandler(IEventRepository eventRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateEventCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        //use static factory method for create the object
        Result<Event> result= Event.Create(
            request.Title,
            request.Description,
            request.Location,
            request.StartAtUtc,
            request.EndAtUtc);

        eventRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
