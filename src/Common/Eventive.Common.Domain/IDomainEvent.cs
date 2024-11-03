using MediatR;

namespace Eventive.Common.Domain;

//to use INotification install MediatR.Contracts
public interface IDomainEvent: INotification
{
    Guid Id { get; }
    DateTime OccuredOnUtc { get; }
}
