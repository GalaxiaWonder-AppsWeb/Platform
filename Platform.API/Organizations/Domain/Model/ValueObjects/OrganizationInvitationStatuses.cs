namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible statuses of an organization invitation.
/// </summary>
public enum OrganizationInvitationStatuses
{
    /// <summary>
    /// The invitation has been sent but not yet responded to.
    /// </summary>
    PENDING,
    
    /// <summary>
    /// The invitation has been accepted by the invited person.
    /// </summary>
    ACCEPTED,
    
    /// <summary>
    /// The invitation has been rejected by the invited person.
    /// </summary>
    REJECTED
}