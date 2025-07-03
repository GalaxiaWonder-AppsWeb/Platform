namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Enumeration that defines the specific roles that a team member can have within a project.
/// </summary>
public enum Roles
{
    /// <summary>
    /// Role representing a team member who is
    /// responsible for coordinating the project activities
    /// and ensuring that the team works effectively together.
    /// Assigned to the owner and some other key members of the team.
    /// </summary>
    COORDINATOR,
    /// <summary>
    /// Role representing a team member who is
    /// responsible for tasks related to the design and planning of the project.
    /// </summary>
    SPECIALIST
}