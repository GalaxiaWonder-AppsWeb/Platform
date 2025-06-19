namespace Platform.API.Organizations.Interfaces.REST.Resources;

public record InvitePersonToOrganizationResource(long OrganizationId, string Email);