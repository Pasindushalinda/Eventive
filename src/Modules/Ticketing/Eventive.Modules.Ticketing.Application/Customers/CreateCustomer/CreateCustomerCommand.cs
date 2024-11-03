using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string FirstName, string LastName)
    : ICommand;
