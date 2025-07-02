using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class TaskCommandService(
    ITaskRepository taskRepository,
    IMilestoneRepository milestoneRepository,
    IUnitOfWork unitOfWork) : ITaskCommandService
{
    public async Task<Task?> Handle(CreateTaskCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.MilestoneId.Value);
        if (milestone is null)
        {
            throw new Exception($"Milestone {command.MilestoneId.Value} not found");
        }
        var task = new Task(command);
        await taskRepository.AddAsync(task);
        return task;
    }
    
    public async Task<Task?> Handle(UpdateTaskCommand command)
    {
        var task = await taskRepository.FindById(command.Id);
        if (task is null)
        {
            throw new Exception($"Task {command.Id} not found");
        }
        if (command.Name is not null)
        {
            task.ReassignName(command.Name);
        }
        if (command.Description is not null)
        {
            task.ReassignDescription(command.Description);
        }
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync();
        return task;
    }
}