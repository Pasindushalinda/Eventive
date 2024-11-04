using Eventive.Common.Application.Messaging;
using Eventive.Modules.Ticketing.Application.Orders.GetOrder;

namespace Eventive.Modules.Ticketing.Application.Orders.GetOrders;

public sealed record GetOrdersQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<OrderResponse>>;
