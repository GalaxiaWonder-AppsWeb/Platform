namespace Platform.API.Projects.Domain.Model.Queries;

/// <summary>
/// Query object to retrieve all projects associated with a team member by their person ID.
/// </summary>
/// <param name="PersonId">
/// the unique identifier of the person whose projects are to be retrieved.
/// </param>
public record GetAllProjectsByTeamMemberPersonIdQuery(long PersonId);