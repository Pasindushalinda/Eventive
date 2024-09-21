namespace Eventive.Common.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvent = [];

    protected Entity() { }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvent.ToList();

    public void ClearDomainEvents()
    {
        _domainEvent.Clear();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvent.Add(domainEvent);
    }
}
