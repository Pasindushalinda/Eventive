using Eventive.Common.Application.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Reflection;

namespace Eventive.Common.Infrastructure.Outbox;

//this class efficiently retrieves and creates instances of all domain event handlers
//for a particular event type, using caching to improve performance and relying on
//dependency injection to create instances
public static class DomainEventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

    public static IEnumerable<IDomainEventHandler> GetHandlers(
        Type type, // domain event type
        IServiceProvider serviceProvider, // get scope type of IDomainEventHandler
        Assembly assembly // scan respective interface implementation
    )
    {
        //HandlersDictionary.GetOrAdd checks if the key already exists in the dictionary.
        //If it does, it returns the cached types of domain event handlers.

        //If the key does not exist, the delegate (_ => { ...}) is executed to add the
        //new key and retrieve the handler types.
        Type[] domainEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}{type.Name}",
            _ =>
            {
            //assembly.GetTypes() retrieves all types defined in the specified assembly.

            //Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(type)))
            //filters these types to include only those that are assignable to
            //IDomainEventHandler<TDomainEvent>, where TDomainEvent is the given domain event type.

            Type[] domainEventHandlerTypes = assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(type)))
                    .ToArray();

                return domainEventHandlerTypes;
            });

        List<IDomainEventHandler> handlers = [];

        //It uses serviceProvider to resolve instances of these handler types
        //and returns them as a list of IDomainEventHandler
        foreach (Type domainEventHandlerType in domainEventHandlerTypes)
        {
            //decorate idemportant domain handler implemetation for each domain event handler
            object domainEventHandler = serviceProvider.GetRequiredService(domainEventHandlerType);

            handlers.Add((domainEventHandler as IDomainEventHandler)!);
        }

        return handlers;
    }
}
