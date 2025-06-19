using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

/// <summary>
///     Application service that handles queries related to <see cref="UserAccount"/> entities,
///     such as retrieving a user account by ID or listing all user accounts with their user types.
/// </summary>
/// <param name="userAccountRepository">The repository used to access user account data.</param>
public class UserAccountQueryService(IUserAccountRepository userAccountRepository) : IUserAccountQueryService
{
    /// <summary>
    ///     Handles the query to retrieve a user account by its unique identifier,
    ///     including the associated user type.
    /// </summary>
    /// <param name="query">The query containing the user account ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserAccount"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<UserAccount?> Handle(GetUserAccountByIdQuery query)
    {
        return await userAccountRepository.FindByIdWithUserTypeAsync(query.Id);
    }

    /// <summary>
    ///     Handles the query to retrieve all user accounts in the system,
    ///     including their associated user types.
    /// </summary>
    /// <param name="query">The query to retrieve all user accounts.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserAccount"/> entities.
    /// </returns>
    public async Task<IEnumerable<UserAccount>> Handle(GetAllUsersAccountQuery query)
    {
        return await userAccountRepository.ListWithUserTypeAsync();
    }
}
