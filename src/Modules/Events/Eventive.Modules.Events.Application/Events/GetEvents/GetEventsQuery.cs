using Eventive.Modules.Events.Application.Abstarctions.Messaging;

namespace Eventive.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<EventResponse>>;
