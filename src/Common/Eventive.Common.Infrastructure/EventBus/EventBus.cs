using Eventive.Common.Application.EventBus;
using MassTransit;

namespace Eventive.Common.Infrastructure.EventBus;

//add masstransit nuget IBus and register in DI container
internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IIntegrationEvent
    {
        await bus.Publish(integrationEvent, cancellationToken);
    }
}
