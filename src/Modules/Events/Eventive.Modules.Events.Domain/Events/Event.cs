using Eventive.Common.Domain;

namespace Eventive.Modules.Events.Domain.Events;

//The Event class inherits from Entity and represents an event in your domain
public sealed class Event : Entity
{
    //To create instance for EF core to materialize this entity from database
    public Event()
    {

    }
    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public DateTime StartAtUtc { get; private set; }
    public DateTime? EndAtUtc { get; private set; }
    public EventStatus Status { get; private set; }

    //To create object for prospect, for application layer
    //Create static factory methods
    public static Result<Event> Create(
        string title,
        string description,
        string location,
        DateTime startAtUtc,
        DateTime? endAtUtc)
    {
        if (endAtUtc.HasValue && endAtUtc < startAtUtc)
        {
            return Result.Failure<Event>(EventErrors.EndDatePrecedesStartDate);
        }

        var @event = new Event
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Location = location,
            StartAtUtc = startAtUtc,
            EndAtUtc = endAtUtc
        };

        //When you create a new Event using the Create method, an EventCreatedDomainEvent 
        //is raised.This domain event is added to the _domainEvent list in the Entity 
        //class. Other parts of your domain can then react to this event, allowing you 
        //to handle side effects in a clean and decoupled manner
        @event.Raise(new EventCreatedDomainEvent(@event.Id));

        return @event;
    }

    public Result Publish()
    {
        if (Status != EventStatus.Draft)
        {
            return Result.Failure(EventErrors.NotDraft);
        }

        Status = EventStatus.Published;

        Raise(new EventPublishedDomainEvent(Id));

        return Result.Success();
    }

    public void Reschedule(DateTime startsAtUtc, DateTime? endsAtUtc)
    {
        if (StartAtUtc == startsAtUtc && EndAtUtc == endsAtUtc)
        {
            return;
        }

        StartAtUtc = startsAtUtc;
        EndAtUtc = endsAtUtc;

        Raise(new EventRescheduledDomainEvent(Id, StartAtUtc, EndAtUtc));
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status == EventStatus.Canceled)
        {
            return Result.Failure(EventErrors.AlreadyCanceled);
        }

        if (StartAtUtc < utcNow)
        {
            return Result.Failure(EventErrors.AlreadyStarted);
        }

        Status = EventStatus.Canceled;

        Raise(new EventCanceledDomainEvent(Id));

        return Result.Success();
    }
}
