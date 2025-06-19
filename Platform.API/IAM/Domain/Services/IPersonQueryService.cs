using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;

namespace Platform.API.IAM.Domain.Services;

/// <summary>
///     Application service interface for handling person-related query operations.
/// </summary>
public interface IPersonQueryService
{
    /// <summary>
    ///     Handles the query to retrieve a person by their unique identifier.
    /// </summary>
    /// <param name="query">The query containing the person ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="Person"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<Person?> Handle(GetPersonByIdQuery query);
    
    
    /// <summary>
    ///     Handles the query to retrieve all persons in the system.
    /// </summary>
    /// <param name="query">The query to retrieve all persons.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="Person"/> entities.
    /// </returns>
    Task<IEnumerable<Person>> Handle(GetAllPersonsQuery query);
    
}