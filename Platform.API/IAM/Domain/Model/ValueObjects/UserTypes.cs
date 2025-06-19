namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Enumeration of possible user types in the system.
/// </summary>
public enum UserTypes
{
    /// <summary>
    ///     Represents a user who is part of the organization (e.g., admin, staff).
    /// </summary>
    TYPE_WORKER,
    
    /// <summary>
    ///     Represents a client user (e.g., external customer or stakeholder).
    /// </summary>
    TYPE_CLIENT
}