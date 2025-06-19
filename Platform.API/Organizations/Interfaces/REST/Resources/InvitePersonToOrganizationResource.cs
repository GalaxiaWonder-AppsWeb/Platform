namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource representing the input data required to invite a person to an organization by email.
/// </summary>
/// <param name="OrganizationId">The ID of the organization sending the invitation.</param>
/// <param name="Email">The email address of the person being invited.</param>
public record InvitePersonToOrganizationResource(long OrganizationId, string Email);