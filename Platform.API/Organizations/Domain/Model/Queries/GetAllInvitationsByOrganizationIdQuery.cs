namespace Platform.API.Organizations.Domain.Model.Queries;

/// <summary>
/// Query to retrieve all invitations associated with a specific organization.
/// </summary>
/// <param name="Id">The unique identifier of the organization.</param>
public record GetAllInvitationsByOrganizationIdQuery(long Id);