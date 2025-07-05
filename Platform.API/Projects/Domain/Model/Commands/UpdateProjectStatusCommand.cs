using Platform.API.Projects.Domain.Model.Entities;

namespace Platform.API.Projects.Domain.Model.Commands;

/// <summary>
/// UpdateProjectStatusCommand is a command to update the status of a project.
/// </summary>
/// <param name="Status">
/// ProjectStatus is the new status to set for the project.
/// </param>
public record UpdateProjectStatusCommand(long Id, ProjectStatus Status);