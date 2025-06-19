namespace Platform.API.Organizations.Domain.Model.Commands;

/// <summary>
///     Command to delete an organization member by their unique identifier.
/// </summary>
public record DeleteOrganizationMemberCommand(long OrganizationMemberId);