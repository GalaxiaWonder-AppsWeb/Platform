namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing the response after inviting a person to an organization.
/// </summary>
/// <param name="OrganizationId">The ID of the organization to which the person is invited.</param>
/// <param name="InvitationId">The ID of the created invitation.</param>
/// <param name="Status">The current status of the invitation (e.g., PENDING).</param>
/// <param name="Person">The profile details of the invited person.</param>
public record InvitePersonToOrganizationResponse(
    long OrganizationId,
    long InvitationId,
    string Status,
    ProfileDetailsResource Person
);