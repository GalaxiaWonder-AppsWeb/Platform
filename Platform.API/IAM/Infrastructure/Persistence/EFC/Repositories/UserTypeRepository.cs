using Microsoft.EntityFrameworkCore;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Repository implementation for managing <see cref="UserType"/> entities.
/// </summary>
/// <param name="context">The application's database context.</param>
public class UserTypeRepository(AppDbContext context)
    : BaseRepository<UserType>(context), IUserTypeRepository
{
    /// <summary>
    ///     Determines whether a user type with the specified name exists in the database.
    /// </summary>
    /// <param name="name">The name of the user type to check.</param>
    /// <returns><c>true</c> if a matching user type is found; otherwise, <c>false</c>.</returns>
    public bool ExistsByName(string name)
    {
        return Context.Set<UserType>().Any(ut => ut.Name.ToString() == name);
    }

    /// <summary>
    ///     Asynchronously finds a user type by its name.
    /// </summary>
    /// <param name="name">The name of the user type to find.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserType"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserType?> FindByNameAsync(string name)
    {
        return await Context.Set<UserType>()
            .FirstOrDefaultAsync(ut => ut.Name.ToString() == name);
    }
}