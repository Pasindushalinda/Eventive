using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>;
