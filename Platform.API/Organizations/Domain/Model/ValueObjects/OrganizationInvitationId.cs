namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents the unique identifier for an organization invitation.
/// </summary>
public record OrganizationInvitationId(long organizationInvitationId)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationInvitationId"/> record with a default value of 0.
    /// </summary>
    public OrganizationInvitationId() : this(0){}
}