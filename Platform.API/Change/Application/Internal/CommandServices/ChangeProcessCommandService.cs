using Platform.API.Change.Domain.Model.Aggregates;
using Platform.API.Change.Domain.Model.Commands;
using Platform.API.Change.Domain.Model.ValueObjects;
using Platform.API.Change.Domain.Repositories;
using Platform.API.Change.Domain.Services;
using Platform.API.Projects.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Change.Application.Internal.CommandServices;

public class ChangeProcessCommandService(
    IChangeProcessRepository changeProcessRepository,
    IChangeProcessStatusRepository changeProcessStatusRepository,
    IChangeOriginRepository changeOriginRepository,
    IProjectRepository projectFacade,
    IProjectStatusRepository projectStatusFacade,
    IUnitOfWork unitOfWork) : IChangeProcessCommandService
{
    public async Task<ChangeProcess?> Handle(CreateChangeProcessCommand command)
    {
        var changeProcess = new ChangeProcess(command);
        var project = await projectFacade.FindById(command.ProjectId.Value);
        if (project == null)
        {
            throw new Exception($"Project with ID {command.ProjectId.Value} not found");
        }

        var existingChangeProcess = await changeProcessRepository.FindByProjectId(command.ProjectId.Value);
        if (existingChangeProcess != null)
        {
            throw new Exception($"A Change process for project with id {command.ProjectId.Value} is in progress");
        }
        
        var originName = project.Status.Name is ProjectStatuses.APPROVED
            ? "TECHNICAL_QUERY"
            : "CHANGE_REQUEST";

        var origin = await changeOriginRepository.FindByName(originName);
        if (origin == null)
        {
            throw new Exception($"Change origin with name {originName} not found");
        }

        
        var status = await changeProcessStatusRepository.FindByName("PENDING");
        if (status == null)
        {
            throw new Exception("Change process status not found");
        }
        
        changeProcess.SetInformation(origin, status);
        await changeProcessRepository.AddAsync(changeProcess);


        var projectStatus = await projectStatusFacade.FindByName("CHANGE_REQUESTED");
        if (projectStatus == null)
        {
            throw new Exception("Project status for CHANGE_REQUESTED not found");
        }
        project.ReassignStatus(projectStatus);
        projectFacade.Update(project);
        
        await unitOfWork.CompleteAsync();
        return changeProcess;
    }

    public async Task<ChangeProcess?> Handle(RespondToChangeProcessCommand command)
    {
        if (command.Id <= 0)
        {
            throw new ArgumentException("Invalid ChangeProcess ID.");
        }
        
        var changeProcess = await changeProcessRepository.FindById(command.Id);
        if (changeProcess == null)
        {
            throw new Exception($"Change process with ID {command.Id} not found");
        }
        var project = await projectFacade.FindById(changeProcess.ProjectId.Value);
        if (project == null)
        {
            throw new Exception($"Project with ID {changeProcess.ProjectId.Value} not found");
        }

        var newStatus = command.Status.Name.ToString();
        var status = await changeProcessStatusRepository.FindByName(newStatus);
        if (status == null)
        {
            throw new Exception($"Change process status with name {newStatus} not found");
        }
        changeProcess.RespondToChange(command.Response, status);
        
        var newProjectStatus = command.Status.Name is ChangeProcessStatuses.APPROVED
            ? "CHANGE_PENDING"
            : "APPROVED";
        
        var projectStatus = await projectStatusFacade.FindByName(newProjectStatus);
        if (projectStatus == null)
        {
            throw new Exception($"Project status with name {newProjectStatus} not found");
        }
        project.ReassignStatus(projectStatus);
        
        projectFacade.Update(project);
        changeProcessRepository.Update(changeProcess);
        await unitOfWork.CompleteAsync();
        return changeProcess;
    }
}