using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;

namespace Platform.API.Projects.Domain.Services;

/// <summary>
/// Defines the contract for handling commands related to project lifecycle management.
/// </summary>
public interface IProjectCommandService
{
    /// <summary>
    /// Handles the creation of a new project.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the project, including the organization ID and project name.
    /// </param>
    /// <returns>
    /// The newly created <see cref="Project"/> entity, or null if the creation failed.
    /// </returns>
    Task<Project?> Handle(CreateProjectCommand command);
}