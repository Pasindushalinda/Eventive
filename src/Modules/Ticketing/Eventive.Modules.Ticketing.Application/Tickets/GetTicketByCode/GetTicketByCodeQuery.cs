using Eventive.Common.Application.Messaging;
using Eventive.Modules.Ticketing.Application.Tickets.GetTicket;

namespace Eventive.Modules.Ticketing.Application.Tickets.GetTicketByCode;

public sealed record GetTicketByCodeQuery(string Code) : IQuery<TicketResponse>;
