using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

/// <summary>
///     Value object representing the status of an organization invitation,
///     such as PENDING, ACCEPTED, or REJECTED.
/// </summary>
public class OrganizationInvitationStatus
{
    /// <summary>
    ///     Gets the unique identifier of the invitation status.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    ///     Gets the status name as an enum value.
    /// </summary>
    public OrganizationInvitationStatuses Name { get; private set; }
    
    /// <summary>
    ///     Initializes a new empty instance of <see cref="OrganizationInvitationStatus"/>.
    ///     Required by EF Core.
    /// </summary>
    public OrganizationInvitationStatus(){}

    /// <summary>
    ///     Initializes a new instance of <see cref="OrganizationInvitationStatus"/> with a specific name.
    /// </summary>
    /// <param name="name">The enum value representing the invitation status.</param>
    public OrganizationInvitationStatus(OrganizationInvitationStatuses name)
    {
        Name = name;
    }
}