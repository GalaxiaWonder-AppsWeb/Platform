namespace Platform.API.Organizations.Domain.Model.Queries;

/// <summary>
/// Query to retrieve all members belonging to a specific organization.
/// </summary>
/// <param name="Id">The unique identifier of the organization.</param>
public record GetAllMembersByOrganizationIdQuery(long Id);