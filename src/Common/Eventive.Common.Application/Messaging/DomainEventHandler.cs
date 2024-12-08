using Eventive.Common.Domain;

namespace Eventive.Common.Application.Messaging;

//Idempotent: to implement explicit de-duplication logic need to drop mediatR support for use our own way
//check the current domain event proccess already

//implemet custom methods in DomainEventHandler

//This class implements the IDomainEventHandler<TDomainEvent> generic interface.

//It also implements the IDomainEventHandler non-generic interface indirectly
//by providing a non-generic Handle method.

//It contains an abstract generic Handle method that must be implemented by
//any subclass to handle a specific type of TDomainEvent.

//It contains a non-generic Handle method that casts the IDomainEvent to
//TDomainEvent and calls the generic Handle method.

//replace this DomainEventHandler in every domain event handler (: DomainEventHandler<UserRegisteredDomainEvent>)
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
