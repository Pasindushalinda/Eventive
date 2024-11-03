using Eventive.Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventive.Common.Infrastructure.Interceptors;

//register this singleton
//SaveChangesInterceptor-> override SavedChangesAsync in EF
//IServiceScopeFactory-> create custom scope for publish domain event
public sealed class PublishDomainEventsInterceptor(IServiceScopeFactory serviceScopeFactory) : SaveChangesInterceptor
{
    //execute after the initial database transaction committed
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            //publish any domain event if available
            await PublishDomainEventsAsync(eventData.Context);
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext context)
    {
        var domainEvents = context //ef coredb context
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
            .ToList();

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        //inherit INotification using MediatR.Contracts in IDomainEvent
        //Create IDomainEventHandler in Application/Message
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}
