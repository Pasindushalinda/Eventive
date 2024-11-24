using Eventive.Common.Domain;

namespace Eventive.Common.Application.Messaging;

//Idempotent: to implement explicit de-duplication logic need to drop mediatR support for use our own way
//check the current domain event proccess already

//implemet custom methods in DomainEventHandler
public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
