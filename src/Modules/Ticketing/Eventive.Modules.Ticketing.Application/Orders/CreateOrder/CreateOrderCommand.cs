using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
