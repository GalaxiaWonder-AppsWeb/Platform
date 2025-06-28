using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Application.Internal.CommandServices;

/// <summary>
/// Service responsible for handling project commands.
/// </summary>
/// <param name="projectRepository">
/// project repository to interact with project data.
/// </param>
/// <param name="projectStatusRepository">
/// project status repository to interact with project status data.
/// </param>
/// <param name="unitOfWork">
/// unit of work to manage transactions.
/// </param>
public class ProjectCommandService(
    IProjectRepository projectRepository,
    IProjectStatusRepository projectStatusRepository,
    IUnitOfWork unitOfWork) : IProjectCommandService
{
    /// <summary>
    /// Handles the creation of a new project.
    /// </summary>
    /// <param name="command">
    /// the command containing project creation details.
    /// </param>
    /// <returns>
    /// the newly created project or null if creation failed.
    /// </returns>
    /// <exception cref="Exception">
    /// thows an exception if the project status is not found in the repository.
    /// </exception>
    public async Task<Project?> Handle(CreateProjectCommand command)
    {
        var project = new Project(command);
        var existingStatus = command.Status.Name.ToString();
        var status = await projectStatusRepository.FindByName(existingStatus);
        if (status == null)
        {
            throw new Exception($"Project status {command.Status.GetName()} not found");
        }
        project.ReassignStatus(status);
        await projectRepository.AddAsync(project);
        await unitOfWork.CompleteAsync();
        return project;
    }

    /// <summary>
    /// Handles the update of a project's name.
    /// </summary>
    /// <param name="command">
    /// The command containing the project ID and new name.
    /// </param>
    /// <returns>
    /// The updated project or null if the project was not found.
    /// </returns>
    /// <exception cref="Exception">
    /// Throws an exception if the project with the specified ID is not found.
    /// </exception>
    public async Task<Project?> Handle(UpdateProjectNameCommand command)
    {
        var project = await projectRepository.FindById(command.Id);
        if (project == null) throw new Exception($"Project with ID {command.Id} not found");
        
        project.UpdateProjectName(command.ProjectName);
        projectRepository.Update(project);
        await unitOfWork.CompleteAsync();
        return project;
    }
    
    /// <summary>
    /// Handles the update of a project's description.
    /// </summary>
    /// <param name="command">
    /// The command containing the project ID and new description.
    /// </param>
    /// <returns>
    /// The updated project or null if the project was not found.
    /// </returns>
    /// <exception cref="Exception">
    /// Throws an exception if the project with the specified ID is not found.
    /// </exception>
    public async Task<Project?> Handle(UpdateProjectDescriptionCommand command)
    {
        var project = await projectRepository.FindById(command.Id);
        if (project == null) throw new Exception($"Project with ID {command.Id} not found");
        
        project.UpdateDescription(command.ProjectDescription);
        projectRepository.Update(project);
        await unitOfWork.CompleteAsync();
        return project;
    }
}