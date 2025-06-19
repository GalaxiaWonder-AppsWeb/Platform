namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents the current status of an organization.
/// </summary>
public enum OrganizationStatuses
{
    /// <summary>
    /// Indicates that the organization is currently active and operational.
    /// </summary>
    ACTIVE,
    
    /// <summary>
    /// Indicates that the organization is currently inactive or suspended.
    /// </summary>
    INACTIVE
}