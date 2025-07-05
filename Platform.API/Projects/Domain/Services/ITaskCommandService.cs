using Platform.API.Projects.Domain.Model.Commands;
using TaskD = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Domain.Services;

public interface ITaskCommandService
{
    Task<TaskD?> Handle(CreateTaskCommand command);
    
    Task<TaskD?> Handle(UpdateTaskCommand command);
    
    Task Handle (DeleteTaskCommand command);
}