using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Events.GetEvents;

public sealed record GetEventsQuery : IQuery<IReadOnlyCollection<EventResponse>>;
