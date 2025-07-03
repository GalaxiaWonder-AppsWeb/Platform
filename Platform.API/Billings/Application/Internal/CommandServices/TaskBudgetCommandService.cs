using Platform.API.Billings.Domain.Model.Aggregates;
using Platform.API.Billings.Domain.Model.Commands;
using Platform.API.Billings.Domain.Repositories;
using Platform.API.Billings.Domain.Services;
using Platform.API.Projects.Interfaces.ACL;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Billings.Application.Internal.CommandServices;

public class TaskBudgetCommandService(
    ITaskBudgetRepository taskBudgetRepository,
    ITaskFacade taskFacade,
    IUnitOfWork unitOfWork) : ITaskBudgetCommandService
{
    public async Task<TaskBudget?> Handle(CreateTaskBudgetCommand command)
    {
        var taskExists = await taskFacade.TaskExists(command.TaskId.taskId);
        if (!taskExists)
        {
            throw new Exception($"Task with ID {command.TaskId.taskId} not found");
        }
        var taskBudget = new TaskBudget(command);
        await taskBudgetRepository.AddAsync(taskBudget);
        await unitOfWork.CompleteAsync();
        return taskBudget;
    }
}