using Eventive.Common.Application.Messaging;
using Eventive.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Eventive.Modules.Ticketing.Application.Tickets.GetTicketForOrder;

public sealed record GetTicketsForOrderQuery(Guid OrderId) : IQuery<IReadOnlyCollection<TicketResponse>>;
