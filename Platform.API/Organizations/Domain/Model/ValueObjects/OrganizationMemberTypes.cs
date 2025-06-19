namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Specifies the type of member within an organization.
/// </summary>
public enum OrganizationMemberTypes
{
    /// <summary>
    /// A contractor is an external member with specific, often limited, responsibilities.
    /// </summary>
    CONTRACTOR,
    
    /// <summary>
    /// A worker is an internal member with full access to organizational operations.
    /// </summary>
    WORKER
}