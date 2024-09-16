namespace Eventive.Modules.Events.Application.Abstarctions.Data;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
