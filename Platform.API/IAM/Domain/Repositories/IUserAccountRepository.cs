using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.IAM.Domain.Repositories;

/// <summary>
///     Repository interface for managing <see cref="UserAccount"/> entities.
/// </summary>
public interface IUserAccountRepository : IBaseRepository<UserAccount>
{
    /// <summary>
    ///     Determines whether a user account exists with the specified username.
    /// </summary>
    /// <param name="userName">The username to check for existence.</param>
    /// <returns><c>true</c> if a user account with the given username exists; otherwise, <c>false</c>.</returns>
    bool ExistsByUserName(UserName userName);

    /// <summary>
    ///     Asynchronously finds a user account by the specified email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> if found; otherwise, <c>null</c>.
    /// </returns>
    new Task<UserAccount?> FindByEmailAsync(string email);
    
    /// <summary>
    ///     Asynchronously finds a user account by ID, including its associated user type.
    /// </summary>
    /// <param name="id">The unique identifier of the user account.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> with its user type if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserAccount?> FindByIdWithUserTypeAsync(long id);
    
    /// <summary>
    ///     Asynchronously retrieves all user accounts with their associated user types included.
    /// </summary>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserAccount"/> entities with user types.
    /// </returns>
    Task<IEnumerable<UserAccount>> ListWithUserTypeAsync();

}