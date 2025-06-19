namespace Platform.API.Organizations.Domain.Model.Queries;

/// <summary>
/// Query to retrieve all organizations where a person is registered as a member.
/// </summary>
/// <param name="Id">The ID of the person whose organization memberships are being queried.</param>
public record GetAllOrganizationsByMemberPersonIdQuery(long Id);