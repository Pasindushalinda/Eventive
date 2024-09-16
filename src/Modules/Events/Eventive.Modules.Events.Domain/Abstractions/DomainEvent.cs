﻿namespace Eventive.Modules.Events.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
    public DateTime OccuredOnUtc { get; init; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }
}
