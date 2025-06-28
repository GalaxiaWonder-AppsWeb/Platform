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
    
    /// <summary>
    /// Handles the update of the project name.
    /// </summary>
    /// <param name="command">
    /// the command containing the project ID and the new name for the project.
    /// </param>
    /// <returns>
    /// The updated <see cref="Project"/> entity, or null if the update failed.
    /// </returns>
    Task<Project?> Handle(UpdateProjectNameCommand command);
    
    /// <summary>
    /// Handles the update of the project description.
    /// </summary>
    /// <param name="command">
    /// The command containing the project ID and the new description for the project.
    /// </param>
    /// <returns>
    /// The updated <see cref="Project"/> entity, or null if the update failed.
    /// </returns>
    Task<Project?> Handle(UpdateProjectDescriptionCommand command);
    
    /// <summary>
    /// Handles the deletion of an existing project.
    /// </summary>
    /// <param name="command">
    /// The command specifying the project to delete, including the project ID.
    /// </param>
    Task Handle(DeleteProjectCommand command);
}