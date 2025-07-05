namespace Platform.API.Projects.Domain.Model.Queries;

/// <summary>
/// Query object to retrieve all projects associated with a contracting entity by its ID.
/// </summary>
/// <param name="Id">
/// The unique identifier of the contracting entity (person id) whose projects are to be retrieved.
/// </param>
public record GetAllProjectsByContractingEntityIdQuery(long Id);