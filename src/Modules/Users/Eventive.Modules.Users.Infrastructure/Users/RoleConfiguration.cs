using Eventive.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Eventive.Modules.Users.Infrastructure.Users;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        //Maps the Role entity to the roles table in the database.
        builder.ToTable("roles");

        //Specifies that the Name property is the primary key for the Role entity
        builder.HasKey(r => r.Name);

        //Configures the Name property to have a maximum length of 50 characters
        builder.Property(r => r.Name).HasMaxLength(50);

        //Sets up a many-to-many relationship between Role and User.
        //The intermediate join table is called user_roles, and the RolesName
        //column in the join table is renamed to role_name
        builder
            .HasMany<User>()
            .WithMany(u => u.Roles)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("user_roles");

                joinBuilder.Property("RolesName").HasColumnName("role_name");
            });

        //Seeds the roles table with initial data: Role.Member and Role.Administrator
        builder.HasData(
            Role.Member,
            Role.Administrator);
    }
}
