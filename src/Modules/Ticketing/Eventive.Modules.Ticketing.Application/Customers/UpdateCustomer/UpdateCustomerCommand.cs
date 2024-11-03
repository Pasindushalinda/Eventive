using Eventive.Common.Application.Messaging;

namespace Eventive.Modules.Ticketing.Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
