using Platform.API.IAM.Domain.Model.Entities;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Model.ValueObjects;

namespace Platform.API.IAM.Domain.Services;

/// <summary>
///     Application service interface for handling user type-related query operations.
/// </summary>
public interface IUserTypeQueryService
{
    /// <summary>
    ///     Handles the query to retrieve all user types available in the system.
    /// </summary>
    /// <param name="query">The query to retrieve all user types.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="UserType"/> entities.
    /// </returns>
    Task<IEnumerable<UserType>> Handle(GetAllUserTypesQuery query);
    
    /// <summary>
    ///     Handles the query to retrieve a user type by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user type ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="UserType"/> if found.
    /// </returns>
    Task<UserType> Handle(GetUserTypeByIdQuery query);
}