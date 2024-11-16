using Eventive.Modules.Users.Domain.Users;
using Eventive.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Users.Infrastructure.Users;

internal sealed class UserRepository(UsersDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        //If the roles are already in the database and you simply want to associate them with
        //the user without adding new roles, this is necessary 
        foreach (Role role in user.Roles)
        {
            context.Attach(role);
        }

        context.Users.Add(user);
    }
}
