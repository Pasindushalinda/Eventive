using Eventive.Common.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eventive.Common.Infrastructure.Serialization;

namespace Eventive.Common.Infrastructure.Outbox;

//register this singleton
//SaveChangesInterceptor-> override SavedChangesAsync in EF
//IServiceScopeFactory-> create custom scope for publish domain event
public sealed class InsertOutboxMessageInterceptor : SaveChangesInterceptor
{
    //need call this interceptor before call the save changes
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            //publish any domain event if available
            InsertOutboxMessage(eventData.Context);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InsertOutboxMessage(DbContext context)
    {
        var outboxMessages = context //ef coredb context
            .ChangeTracker //access the change tracker
            .Entries<Entity>() //find entries that entity base class
            .Select(entry => entry.Entity) //select entity entry
            .SelectMany(entity =>
            {
                //use entity type and access the domain event
                IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

                //clear domain event after the select domain event
                entity.ClearDomainEvents();

                return domainEvents;
            })
            //create OutboxMessage object using domain event
            .Select(domainEvent => new OutboxMessage
            {
                Id = domainEvent.Id,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, SerializerSettings.Instance),//SerializerSettings
                OccurredOnUtc = domainEvent.OccuredOnUtc
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}
