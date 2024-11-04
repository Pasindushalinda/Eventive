using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Orders.GetOrder;

public sealed record GetOrderQuery(Guid OrderId) : IQuery<OrderResponse>;
