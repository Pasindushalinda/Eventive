using Eventive.Common.Domain;
using MediatR;

namespace Eventive.Common.Application.Messaging;

//handling domain event
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
