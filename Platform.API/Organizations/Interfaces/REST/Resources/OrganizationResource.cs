namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing an organization, including identification, status, and related entities.
/// </summary>
/// <param name="Id">The unique identifier of the organization.</param>
/// <param name="LegalName">The legal name of the organization.</param>
/// <param name="CommercialName">The commercial name of the organization (optional).</param>
/// <param name="Ruc">The unique tax identification number (RUC) of the organization.</param>
/// <param name="CreatedBy">The ID of the person who created the organization.</param>
/// <param name="OrganizationStatusId">The ID of the current status of the organization.</param>
/// <param name="StatusName">The name of the current status of the organization (e.g., ACTIVE, INACTIVE).</param>
/// <param name="OrganizationMemberIds">A list of IDs representing members of the organization.</param>
/// <param name="OrganizationInvitationIds">A list of IDs representing invitations sent by the organization.</param>
/// <param name="CreatedAt">The date and time when the organization was created.</param>
public record OrganizationResource(
    long Id,
    string LegalName,
    string? CommercialName,
    string Ruc,
    long CreatedBy,
    long OrganizationStatusId,
    string StatusName,
    IEnumerable<long> OrganizationMemberIds,
    IEnumerable<long> OrganizationInvitationIds,
    DateTimeOffset CreatedAt);