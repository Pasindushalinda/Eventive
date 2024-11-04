using Dapper;
using Eventive.Common.Application.Data;
using Eventive.Common.Application.Messaging;
using Eventive.Common.Domain;
using System.Data.Common;

namespace Eventive.Modules.Ticketing.Application.Orders.GetOrders;

internal sealed class GetOrdersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetOrdersQuery, IReadOnlyCollection<OrderResponse>>
{
    public async Task<Result<IReadOnlyCollection<OrderResponse>>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(OrderResponse.Id)},
                 customer_id AS {nameof(OrderResponse.CustomerId)},
                 status AS {nameof(OrderResponse.Status)},
                 total_price AS {nameof(OrderResponse.TotalPrice)},
                 created_at_utc AS {nameof(OrderResponse.CreatedAtUtc)}
             FROM ticketing.orders
             WHERE customer_id = @CustomerId
             """;

        List<OrderResponse> orders = (await connection.QueryAsync<OrderResponse>(sql, request)).AsList();

        return orders;
    }
}
