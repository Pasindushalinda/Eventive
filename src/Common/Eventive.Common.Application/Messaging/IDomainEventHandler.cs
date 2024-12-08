using Eventive.Common.Domain;

namespace Eventive.Common.Application.Messaging;

//handling domain event
//check current domain event processed and then ---
//use scrutor excellent nuget packages and not work with INotification in mediatR
//drop the mediatR support for publishing and handling domain event


//genaric domain event
public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

//non-genaric domain event
public interface IDomainEventHandler
{
    Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
