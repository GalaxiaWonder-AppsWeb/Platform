namespace Platform.API.Organizations.Interfaces.REST.Resources;

/// <summary>
/// Resource that represents detailed information about an organization invitation,
/// including organization name and inviter details.
/// </summary>
public class OrganizationInvitationWithDetailsResource
{
    /// <summary>
    /// The unique identifier of the invitation.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// The legal name of the organization that sent the invitation.
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;
    
    /// <summary>
    /// The full name of the person who sent the invitation.
    /// </summary>
    public string InvitedByFullName { get; set; } = string.Empty;
    
    /// <summary>
    /// The email address of the person who sent the invitation.
    /// </summary>
    public string InvitedByEmail { get; set; } = string.Empty;
    
    /// <summary>
    /// The current status of the invitation (e.g., PENDING, ACCEPTED, REJECTED).
    /// </summary>
    public string Status { get; set; } = string.Empty;
    
    /// <summary>
    /// The date and time when the invitation was created.
    /// </summary>
    public DateTimeOffset InvitedOn { get; set; }
}
