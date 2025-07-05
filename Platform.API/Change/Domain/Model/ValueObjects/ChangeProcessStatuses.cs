namespace Platform.API.Change.Domain.Model.ValueObjects;

/// <summary>
/// Represents the possible statuses of a change process.
/// </summary>
public enum ChangeProcessStatuses
{
    /// <summary>
    /// When the change process is in progress and awaiting further actions.
    /// </summary>
    PENDING,
    /// <summary>
    /// When the change process has been completed successfully.
    /// </summary>
    APPROVED,
    /// <summary>
    /// When the change process has been completed but not approved.
    /// </summary>
    REJECTED
}