using Platform.API.IAM.Domain.Model.Aggregates;
using Platform.API.IAM.Domain.Model.Queries;
using Platform.API.IAM.Domain.Repositories;
using Platform.API.IAM.Domain.Services;

namespace Platform.API.IAM.Application.Internal.QueryServices;

/// <summary>
///     Application service that handles queries related to <see cref="Person"/> entities,
///     such as retrieving a person by ID or listing all persons.
/// </summary>
/// <param name="personRepository">The repository used to access person data.</param>
public class PersonQueryService(IPersonRepository personRepository) : IPersonQueryService
{
    /// <summary>
    ///     Handles the query to retrieve a person by their unique identifier.
    /// </summary>
    /// <param name="query">The query containing the person ID.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains the <see cref="Person"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Person?> Handle(GetPersonByIdQuery query)
    {
        return await personRepository.FindByIdAsync(query.Id);
    }
    
    /// <summary>
    ///     Handles the query to retrieve all person entities from the system.
    /// </summary>
    /// <param name="query">The query to retrieve all persons.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The task result contains a collection of <see cref="Person"/> entities.
    /// </returns>
    public async Task<IEnumerable<Person>> Handle(GetAllPersonsQuery query)
    {
        return await personRepository.ListAsync();
    }
}