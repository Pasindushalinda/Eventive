namespace Eventive.Common.Infrastructure.Outbox;

//this will check given msg id process with respective handler
public sealed class OutboxMessageConsumer(Guid outboxMessageId, string name)
{
    public Guid OutboxMessageId { get; init; } = outboxMessageId;

    public string Name { get; init; } = name; //handler name
}
