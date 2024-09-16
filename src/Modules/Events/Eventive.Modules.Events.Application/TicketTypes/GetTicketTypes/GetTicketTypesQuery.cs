using Eventive.Modules.Events.Application.Abstarctions.Messaging;
using Eventive.Modules.Events.Application.TicketTypes.GetTicketType;

namespace Eventive.Modules.Events.Application.TicketTypes.GetTicketTypes;

public sealed record GetTicketTypesQuery(Guid EventId) : IQuery<IReadOnlyCollection<TicketTypeResponse>>;
