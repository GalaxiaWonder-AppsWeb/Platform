namespace Platform.API.Organizations.Interfaces.REST.Resources;

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
