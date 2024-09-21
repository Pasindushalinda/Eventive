using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse?>;
