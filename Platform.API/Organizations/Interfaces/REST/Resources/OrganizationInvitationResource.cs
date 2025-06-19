namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing an organization invitation with person profile information.
/// </summary>
/// <param name="Id">The unique identifier of the invitation.</param>
/// <param name="OrganizationId">The ID of the organization that sent the invitation.</param>
/// <param name="PersonId">The ID of the person who was invited.</param>
/// <param name="FirstName">The first name of the invited person.</param>
/// <param name="LastName">The last name of the invited person.</param>
/// <param name="Email">The email address of the invited person.</param>
/// <param name="Status">The current status of the invitation (e.g., PENDING, ACCEPTED).</param>
/// <param name="CreatedAt">The date and time the invitation was created.</param>
public record OrganizationInvitationResource(
    long Id,
    long OrganizationId,
    long PersonId,
    string FirstName,
    string LastName,
    string Email,
    string Status,
    DateTimeOffset CreatedAt
);
