using Platform.API.Organizations.Domain.Model.ValueObjects;

namespace Platform.API.Organizations.Domain.Model.Entities;

/// <summary>
///     Value object representing the current status of an organization,
///     such as ACTIVE, INACTIVE, or SUSPENDED.
/// </summary>
public class OrganizationStatus
{
    /// <summary>
    ///     Gets the unique identifier of the organization status.
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    ///     Gets the name of the status as an enum value.
    /// </summary>
    public OrganizationStatuses Name { get; private set; }
    
    /// <summary>
    ///     Initializes a new empty instance of <see cref="OrganizationStatus"/>.
    ///     Required by EF Core.
    /// </summary>
    public OrganizationStatus(){}
    
    /// <summary>
    ///     Initializes a new instance of <see cref="OrganizationStatus"/> with the specified status name.
    /// </summary>
    /// <param name="name">The enum value representing the organization's status.</param>
    public OrganizationStatus(OrganizationStatuses name)
    {
        Name = name;
    }
}