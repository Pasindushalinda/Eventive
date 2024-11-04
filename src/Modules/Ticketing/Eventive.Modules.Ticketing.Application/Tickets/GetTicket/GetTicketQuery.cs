using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Tickets.GetTicket;

public sealed record GetTicketQuery(Guid TicketId) : IQuery<TicketResponse>;
