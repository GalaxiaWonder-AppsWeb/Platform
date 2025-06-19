namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record InvitePersonToOrganizationResponse(
    long OrganizationId,
    long InvitationId,
    string Status,
    ProfileDetailsResource Person
);