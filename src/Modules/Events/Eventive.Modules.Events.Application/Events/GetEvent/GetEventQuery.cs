using Eventive.Modules.Events.Application.Abstarctions.Messaging;

namespace Eventive.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse?>;
