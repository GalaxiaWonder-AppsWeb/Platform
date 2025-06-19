using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;

namespace Platform.API.IAM.Domain.Services;

/// <summary>
///     Application service interface for handling user account-related query operations.
/// </summary>
public interface IUserAccountQueryService
{
    /// <summary>
    ///     Handles the query to retrieve a user account by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user account ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserAccount?> Handle(GetUserAccountByIdQuery query);
    
    /// <summary>
    ///     Handles the query to retrieve all user accounts in the system.
    /// </summary>
    /// <param name="query">The query to retrieve all user accounts.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserAccount"/> entities.
    /// </returns>
    Task<IEnumerable<UserAccount>> Handle(GetAllUsersAccountQuery query);
}