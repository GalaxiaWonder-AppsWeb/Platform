using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

/// <summary>
///     Application service that handles queries related to <see cref="UserType"/> entities,
///     such as retrieving a user type by ID or listing all user types.
/// </summary>
/// <param name="userTypeRepository">The repository used to access user type data.</param>
public class UserTypeQueryService(IUserTypeRepository userTypeRepository) : IUserTypeQueryService
{
    /// <summary>
    ///     Handles the query to retrieve all user types in the system.
    /// </summary>
    /// <param name="query">The query to retrieve all user types.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserType"/> entities.
    /// </returns>
    public async Task<IEnumerable<UserType>> Handle(GetAllUserTypesQuery query)
    {
        return await userTypeRepository.ListAsync();
    }

    /// <summary>
    ///     Handles the query to retrieve a specific user type by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user type ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserType"/> if found.
    /// </returns>
    public async Task<UserType> Handle(GetUserTypeByIdQuery query)
    {
        return await userTypeRepository.FindByIdAsync(query.Id);
    }
}