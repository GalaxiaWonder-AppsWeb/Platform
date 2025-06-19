using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

/// <summary>
///     Repository interface for managing <see cref="UserType"/> entities.
/// </summary>
public interface IUserTypeRepository : IBaseRepository<UserType> 
{
    /// <summary>
    ///     Asynchronously finds a user type by its name.
    /// </summary>
    /// <param name="name">The name of the user type to search for.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserType"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserType?> FindByNameAsync(string name);
    
    /// <summary>
    ///     Determines whether a user type with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the user type to check.</param>
    /// <returns><c>true</c> if a user type with the given name exists; otherwise, <c>false</c>.</returns>
    bool ExistsByName(string name);
}