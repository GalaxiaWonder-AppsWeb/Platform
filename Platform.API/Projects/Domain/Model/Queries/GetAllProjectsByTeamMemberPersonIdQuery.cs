namespace Platform.API.Projects.Domain.Model.Queries;

/// <summary>
/// Query object to retrieve all projects associated with a team member by their person ID.
/// </summary>
/// <param name="PersonId">
/// the unique identifier of the person whose projects are to be retrieved.
/// </param>
/// <param name="OrganizationId">
/// The unique identifier of the organization to which the person belongs.
/// </param>
public record GetAllProjectsByTeamMemberPersonIdQuery(long PersonId, long OrganizationId);