using Platform.API.Projects.Domain.Model.Commands;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Domain.Services;

public interface ITaskCommandService
{
    Task<Task?> Handle(CreateTaskCommand command);
    
    Task<Task?> Handle(UpdateTaskCommand command);
}