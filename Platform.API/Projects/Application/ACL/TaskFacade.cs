using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Interfaces.ACL;

namespace Platform.API.Projects.Application.ACL;

public class TaskFacade(
    ITaskRepository taskRepository) : ITaskFacade
{
    public async Task<bool> TaskExists(long taskId)
    {
        var task = await taskRepository.FindById(taskId);
        return task is not null;
    }
}