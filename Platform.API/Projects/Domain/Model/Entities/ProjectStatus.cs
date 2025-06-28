using Platform.API.Projects.Domain.Model.ValueObjects;

namespace Platform.API.Projects.Domain.Model.Entities;

/// <summary>
/// Entity that represents the status of a project.
/// This class is backed by the <see cref="ProjectStatuses"/> enum and defines
/// a fixed set of lifecycle stages used to track projects progress.
/// </summary>
public class ProjectStatus
{
    /// <summary>
    /// Database identifier for the project status entity.
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Enum description representing the status of the projects.
    /// </summary>
    public ProjectStatuses Name { get; set; }
    
    /// <summary>
    /// Initializes a new empty instance of <see cref="ProjectStatus"/>.
    /// </summary>
    public ProjectStatus() { }
    
    /// <summary>
    /// Initializes a new instance of <see cref="ProjectStatus"/> with the specified status name.
    /// </summary>
    /// <param name="name">
    /// The status name of the project, represented by the <see cref="ProjectStatuses"/> enum.
    /// </param>
    public ProjectStatus(ProjectStatuses name)
    {
        Name = name;
    }
    
    public ProjectStatuses GetName()
    {
        return Name;
    }
}