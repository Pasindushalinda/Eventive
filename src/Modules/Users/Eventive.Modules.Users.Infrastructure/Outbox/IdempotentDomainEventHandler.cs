using Dapper;
using Eventive.Common.Application.Data;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using Eventive.Common.Infrastructure.Outbox;
using System.Data.Common;

namespace Eventive.Modules.Users.Infrastructure.Outbox;

//class decorates an existing domain event handler to add idempotency.
//This ensures that an event is processed only once.
//register this in coomn infra
internal sealed class IdempotentDomainEventHandler<TDomainEvent>(
    IDomainEventHandler<TDomainEvent> decorated,
    IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public override async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        //creates an OutboxMessageConsumer instance with the event's ID and the name of the decorated handler.
        var outboxMessageConsumer = new OutboxMessageConsumer(domainEvent.Id, decorated.GetType().Name);

        //checks if the event has already been processed
        if (await OutboxConsumerExistsAsync(connection, outboxMessageConsumer))
        {
            return;
        }

        //If the event has not been processed, it calls the Handle
        //method of the decorated handler to process the event.
        await decorated.Handle(domainEvent, cancellationToken);

        //After processing the event, it calls InsertOutboxConsumerAsync to
        //mark the event as processed in the database.
        await InsertOutboxConsumerAsync(connection, outboxMessageConsumer);
    }

    private static async Task<bool> OutboxConsumerExistsAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            """
            SELECT EXISTS(
                SELECT 1
                FROM users.outbox_message_consumers
                WHERE outbox_message_id = @OutboxMessageId AND
                      name = @Name
            )
            """;

        return await dbConnection.ExecuteScalarAsync<bool>(sql, outboxMessageConsumer);
    }

    private static async Task InsertOutboxConsumerAsync(
        DbConnection dbConnection,
        OutboxMessageConsumer outboxMessageConsumer)
    {
        const string sql =
            """
            INSERT INTO users.outbox_message_consumers(outbox_message_id, name)
            VALUES (@OutboxMessageId, @Name)
            """;

        await dbConnection.ExecuteAsync(sql, outboxMessageConsumer);
    }
}
