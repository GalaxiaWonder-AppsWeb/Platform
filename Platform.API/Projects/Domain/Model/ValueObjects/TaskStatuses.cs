namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Enumeration that defines the possible statuses of a task
/// within the context of project milestone management.
/// </summary>
public enum TaskStatuses
{
    /// The task has been created but not yet prepared for review
    DRAFT,
    /// The task is ready and awaiting submission
    PENDING,
    /// The task has a currently non-reviewed submission
    SUBMITTED,
    /// The task's last submission has been reviewed and approved
    APPROVED,
    /// The task's last submission has een reviewed and rejected
    REJECTED
}