namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Enumeration that defines the possible lifecycle statuses of a project
/// within the Project Management domain.
/// </summary>
public enum ProjectStatuses
{
    /// The project is in the basic studies stage
    BASIC_STUDIES,
    /// The project is currently in the design process
    DESIGN_IN_PROCESS,
    /// The project is under review by relevant authority
    UNDER_REVIEW,
    /// A change to the project has been formally requested
    CHANGE_REQUESTED,
    /// A requested change is pending approval
    CHANGE_PENDING,
    /// The project has been officially approved
    APPROVED
}